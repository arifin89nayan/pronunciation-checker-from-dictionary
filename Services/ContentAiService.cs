using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Services.Prompts;

namespace WindowsFormsApp1.Services
{
    // One entry point for the WinForms layer.
    // It routes Guide vs Quiz, resolves configuration, renders prompts,
    // calls OpenAI, and returns an in-memory draft with a full audit snapshot.
    public class ContentAiService
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        private readonly string _apiKey;
        private readonly AiConfigurationService _configuration;
        private readonly AiPreferenceResolver _preferenceResolver;
        private readonly GuidePromptBuilder _guidePromptBuilder;
        private readonly QuizPromptBuilder _quizPromptBuilder;

        public ContentAiService(string workspace)
        {
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

            _configuration = new AiConfigurationService(workspace);
            _configuration.ValidateWorkspaceConfiguration();

            PromptTemplateRenderer renderer = new PromptTemplateRenderer();
            _preferenceResolver = new AiPreferenceResolver();
            _guidePromptBuilder = new GuidePromptBuilder(_configuration, renderer);
            _quizPromptBuilder = new QuizPromptBuilder(_configuration, renderer);
        }

        public bool IsConfigured => !string.IsNullOrWhiteSpace(_apiKey);

        public async Task<AiGeneratedDraft> GenerateAsync(
            ContentItem item,
            AiPreference runtimePreference)
        {
            if (!IsConfigured)
                throw new InvalidOperationException("OPENAI_API_KEY environment variable is not configured.");

            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (string.IsNullOrWhiteSpace(item.SourceText))
                throw new InvalidOperationException("Source text is empty.");

            AiSettings settings = _configuration.LoadSettings();

            if (string.Equals(item.ContentType, "Guide", StringComparison.OrdinalIgnoreCase))
            {
                AiGenerationContext context =
                    _preferenceResolver.ResolveGuide(item, runtimePreference, settings);

                PromptPackage prompt = _guidePromptBuilder.Build(context);
                return await GenerateGuideAsync(prompt);
            }

            if (string.Equals(item.ContentType, "Quiz", StringComparison.OrdinalIgnoreCase))
            {
                AiGenerationContext context =
                    _preferenceResolver.ResolveQuiz(item, runtimePreference, settings);

                PromptPackage prompt = _quizPromptBuilder.Build(context);
                return await GenerateQuizAsync(prompt, context.ChoiceCount);
            }

            throw new NotSupportedException("Unsupported content type: " + item.ContentType);
        }

        private async Task<AiGeneratedDraft> GenerateGuideAsync(PromptPackage prompt)
        {
            object schema = new
            {
                type = "object",
                properties = new
                {
                    guide_text = new { type = "string" }
                },
                required = new[] { "guide_text" },
                additionalProperties = false
            };

            OpenAiCallResult call = await CallStructuredAsync(
                prompt,
                "guide_content",
                schema);

            GuideAiOutput output = JsonSerializer.Deserialize<GuideAiOutput>(call.StructuredJson);

            if (output == null || string.IsNullOrWhiteSpace(output.GuideText))
                throw new InvalidOperationException("OpenAI returned an empty guide.");

            return new AiGeneratedDraft
            {
                ContentType = "Guide",
                DisplayText = output.GuideText,
                PayloadJson = call.StructuredJson,
                ModelName = prompt.Model,
                PromptVersion = prompt.PromptVersion,
                SystemInstruction = prompt.SystemInstruction,
                FinalPrompt = prompt.UserPrompt,
                PreferencesJson = prompt.PreferencesJson,
                RawResponseJson = call.RawResponseJson,
                IsEdited = false
            };
        }

        private async Task<AiGeneratedDraft> GenerateQuizAsync(
            PromptPackage prompt,
            int choiceCount)
        {
            object schema = new
            {
                type = "object",
                properties = new
                {
                    question = new { type = "string" },
                    choices = new
                    {
                        type = "array",
                        items = new { type = "string" },
                        minItems = choiceCount,
                        maxItems = choiceCount
                    },
                    correct_index = new
                    {
                        type = "integer",
                        minimum = 0,
                        maximum = choiceCount - 1
                    },
                    explanation = new { type = "string" }
                },
                required = new[]
                {
                    "question",
                    "choices",
                    "correct_index",
                    "explanation"
                },
                additionalProperties = false
            };

            OpenAiCallResult call = await CallStructuredAsync(
                prompt,
                "quiz_content",
                schema);

            QuizAiOutput output = JsonSerializer.Deserialize<QuizAiOutput>(call.StructuredJson);

            if (output == null || string.IsNullOrWhiteSpace(output.Question))
                throw new InvalidOperationException("OpenAI returned an empty quiz.");

            if (output.Choices == null || output.Choices.Count != choiceCount)
                throw new InvalidOperationException("OpenAI returned the wrong number of quiz choices.");

            if (output.CorrectIndex < 0 || output.CorrectIndex >= output.Choices.Count)
                throw new InvalidOperationException("OpenAI returned an invalid correct_index.");

            string displayText = FormatQuiz(output);

            return new AiGeneratedDraft
            {
                ContentType = "Quiz",
                DisplayText = displayText,
                PayloadJson = call.StructuredJson,
                ModelName = prompt.Model,
                PromptVersion = prompt.PromptVersion,
                SystemInstruction = prompt.SystemInstruction,
                FinalPrompt = prompt.UserPrompt,
                PreferencesJson = prompt.PreferencesJson,
                RawResponseJson = call.RawResponseJson,
                IsEdited = false
            };
        }

        private async Task<OpenAiCallResult> CallStructuredAsync(
            PromptPackage prompt,
            string schemaName,
            object schema)
        {
            object requestObject = new
            {
                model = prompt.Model,
                instructions = prompt.SystemInstruction,
                input = prompt.UserPrompt,
                max_output_tokens = prompt.MaxOutputTokens,
                text = new
                {
                    format = new
                    {
                        type = "json_schema",
                        name = schemaName,
                        strict = true,
                        schema = schema
                    }
                }
            };

            string requestJson = JsonSerializer.Serialize(requestObject);

            using (HttpRequestMessage request =
                   new HttpRequestMessage(
                       HttpMethod.Post,
                       "https://api.openai.com/v1/responses"))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", _apiKey);

                request.Content =
                    new StringContent(requestJson, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await HttpClient.SendAsync(request))
                {
                    string rawResponse = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new InvalidOperationException(
                            "OpenAI API error (" + (int)response.StatusCode + "):\r\n" + rawResponse);
                    }

                    return new OpenAiCallResult
                    {
                        RawResponseJson = rawResponse,
                        StructuredJson = ExtractOutputText(rawResponse)
                    };
                }
            }
        }

        private static string FormatQuiz(QuizAiOutput quiz)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Question:");
            builder.AppendLine(quiz.Question ?? string.Empty);
            builder.AppendLine();

            for (int i = 0; i < quiz.Choices.Count; i++)
                builder.AppendLine((i + 1) + ". " + quiz.Choices[i]);

            builder.AppendLine();
            builder.AppendLine("Correct Answer: " + (quiz.CorrectIndex + 1));
            builder.AppendLine();
            builder.AppendLine("Explanation:");
            builder.AppendLine(quiz.Explanation ?? string.Empty);
            return builder.ToString();
        }

        private static string ExtractOutputText(string responseJson)
        {
            using (JsonDocument document = JsonDocument.Parse(responseJson))
            {
                JsonElement root = document.RootElement;

                if (!root.TryGetProperty("output", out JsonElement outputs))
                    throw new InvalidOperationException("OpenAI response does not contain output.");

                foreach (JsonElement output in outputs.EnumerateArray())
                {
                    if (!output.TryGetProperty("content", out JsonElement content))
                        continue;

                    foreach (JsonElement part in content.EnumerateArray())
                    {
                        string type =
                            part.TryGetProperty("type", out JsonElement typeElement)
                                ? typeElement.GetString()
                                : string.Empty;

                        if (type == "output_text" &&
                            part.TryGetProperty("text", out JsonElement textElement))
                        {
                            return textElement.GetString();
                        }
                    }
                }
            }

            throw new InvalidOperationException("Could not read generated text from OpenAI response.");
        }

        private class OpenAiCallResult
        {
            public string StructuredJson { get; set; }
            public string RawResponseJson { get; set; }
        }
    }
}
