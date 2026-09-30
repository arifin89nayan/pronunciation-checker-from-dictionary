using System.Collections.Generic;
using System.Text.Json;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services.Prompts
{
    public class QuizPromptBuilder
    {
        private readonly AiConfigurationService _configuration;
        private readonly PromptTemplateRenderer _renderer;

        public QuizPromptBuilder(
            AiConfigurationService configuration,
            PromptTemplateRenderer renderer)
        {
            _configuration = configuration;
            _renderer = renderer;
        }

        public PromptPackage Build(AiGenerationContext context)
        {
            string system = _configuration.LoadPrompt("QuizSystem.txt");
            string userTemplate = _configuration.LoadPrompt("QuizUser.txt");

            Dictionary<string, string> values =
                new Dictionary<string, string>
                {
                    ["TOPIC"] = context.Topic ?? string.Empty,
                    ["LANGUAGE"] = context.OutputLanguage ?? string.Empty,
                    ["AGE_GROUP"] = context.AgeGroup ?? string.Empty,
                    ["KNOWLEDGE_LEVEL"] = context.KnowledgeLevel ?? string.Empty,
                    ["DIFFICULTY"] = context.Difficulty ?? string.Empty,
                    ["CHOICE_COUNT"] = context.ChoiceCount.ToString(),
                    ["SOURCE_TEXT"] = context.SourceText ?? string.Empty
                };

            string userPrompt = _renderer.Render(userTemplate, values);

            return new PromptPackage
            {
                ContentType = "Quiz",
                Model = context.Model,
                PromptVersion = context.PromptVersion,
                SystemInstruction = system,
                UserPrompt = userPrompt,
                MaxOutputTokens = context.MaxOutputTokens,
                PreferencesJson = JsonSerializer.Serialize(new
                {
                    context.AgeGroup,
                    context.KnowledgeLevel,
                    context.Difficulty,
                    context.ChoiceCount,
                    context.OutputLanguage
                })
            };
        }
    }
}
