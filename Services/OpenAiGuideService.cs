using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class AiGuideResult
    {
        public string GeneratedText { get; set; }

        public string ModelName { get; set; }

        public string PromptVersion { get; set; }
    }


    public class OpenAiGuideService
    {
        private static readonly HttpClient _httpClient =
            new HttpClient();

        private readonly string _apiKey;

        private readonly string _model;

        public bool IsConfigured =>
            !string.IsNullOrWhiteSpace(_apiKey);


        public OpenAiGuideService()
        {
            _apiKey =
                Environment.GetEnvironmentVariable(
                    "OPENAI_API_KEY");

            _model =
                Environment.GetEnvironmentVariable(
                    "OPENAI_MODEL")
                ?? "gpt-5.6-luna";
        }


        public async Task<AiGuideResult> GenerateAsync(
            ContentItem item,
            AiPreference preference)
        {
            if (!IsConfigured)
            {
                throw new InvalidOperationException(
                    "OPENAI_API_KEY environment variable is not configured.");
            }

            if (item == null)
            {
                throw new ArgumentNullException(
                    nameof(item));
            }

            if (string.IsNullOrWhiteSpace(
                    item.SourceText))
            {
                throw new InvalidOperationException(
                    "Source text is empty.");
            }


            string promptVersion =
                "GUIDE_V1";


            string instructions = @"
                            You are an educational exhibition guide content generator.

                            Use only facts supported by the supplied SOURCE TEXT.

                            Do not invent:
                            - names
                            - dates
                            - numbers
                            - organizations
                            - locations
                            - technical facts

                            Rewrite the material for the requested visitor profile.

                            The generated guide should:
                            - be clear and natural
                            - preserve important factual information
                            - be suitable for exhibition/kiosk presentation
                            - avoid unnecessary repetition
                            - use the requested output language
                            - follow the requested word limit.
                            ";


            string input = $@"
                            CONTENT TYPE:
                            Exhibition Guide

                            TOPIC:
                            {item.Topic}

                            OUTPUT LANGUAGE / LOCALE:
                            {item.LanguageCode}

                            VISITOR AGE GROUP:
                            {preference.AgeGroup}

                            KNOWLEDGE LEVEL:
                            {preference.KnowledgeLevel}

                            TONE:
                            {preference.Tone}

                            MAXIMUM WORDS:
                            {preference.MaximumWords}

                            SOURCE TEXT:
                            ----------------
                            {item.SourceText}
                            ----------------

                            Create one visitor-oriented guide based only on this source.
                            ";


            var requestObject = new
            {
                model = _model,

                instructions = instructions,

                input = input,

                max_output_tokens = 800,

                text = new
                {
                    format = new
                    {
                        type = "json_schema",

                        name = "guide_content",

                        strict = true,

                        schema = new
                        {
                            type = "object",

                            properties = new
                            {
                                guide_text = new
                                {
                                    type = "string"
                                }
                            },

                            required = new[]
                            {
                                "guide_text"
                            },

                            additionalProperties = false
                        }
                    }
                }
            };


            string json =
                JsonSerializer.Serialize(
                    requestObject);


            using (var request =
                   new HttpRequestMessage(
                       HttpMethod.Post,
                       "https://api.openai.com/v1/responses"))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue(
                        "Bearer",
                        _apiKey);

                request.Content =
                    new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json");


                using (HttpResponseMessage response =
                       await _httpClient.SendAsync(request))
                {
                    string responseJson =
                        await response.Content
                            .ReadAsStringAsync();


                    if (!response.IsSuccessStatusCode)
                    {
                        throw new InvalidOperationException(
                            "OpenAI API error:\r\n" +
                            responseJson);
                    }


                    string structuredText =
                        ExtractOutputText(
                            responseJson);


                    using (JsonDocument structuredJson =
                           JsonDocument.Parse(
                               structuredText))
                    {
                        string generatedGuide =
                            structuredJson.RootElement
                                .GetProperty(
                                    "guide_text")
                                .GetString();


                        return new AiGuideResult
                        {
                            GeneratedText =
                                generatedGuide,

                            ModelName =
                                _model,

                            PromptVersion =
                                promptVersion
                        };
                    }
                }
            }
        }


        private string ExtractOutputText(
            string responseJson)
        {
            using (JsonDocument document =
                   JsonDocument.Parse(
                       responseJson))
            {
                JsonElement root =
                    document.RootElement;


                if (!root.TryGetProperty(
                        "output",
                        out JsonElement outputs))
                {
                    throw new InvalidOperationException(
                        "OpenAI response does not contain output.");
                }


                foreach (JsonElement output in
                         outputs.EnumerateArray())
                {
                    if (!output.TryGetProperty(
                            "content",
                            out JsonElement content))
                    {
                        continue;
                    }


                    foreach (JsonElement part in
                             content.EnumerateArray())
                    {
                        string type = "";

                        if (part.TryGetProperty(
                                "type",
                                out JsonElement typeElement))
                        {
                            type =
                                typeElement.GetString();
                        }


                        if (type == "output_text" &&
                            part.TryGetProperty(
                                "text",
                                out JsonElement textElement))
                        {
                            return
                                textElement.GetString();
                        }
                    }
                }
            }


            throw new InvalidOperationException(
                "Could not read generated text from OpenAI response.");
        }
    }
}