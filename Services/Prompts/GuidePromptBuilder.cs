using System.Collections.Generic;
using System.Text.Json;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services.Prompts
{
    public class GuidePromptBuilder
    {
        private readonly AiConfigurationService _configuration;
        private readonly PromptTemplateRenderer _renderer;

        public GuidePromptBuilder(
            AiConfigurationService configuration,
            PromptTemplateRenderer renderer)
        {
            _configuration = configuration;
            _renderer = renderer;
        }

        public PromptPackage Build(AiGenerationContext context)
        {
            string system = _configuration.LoadPrompt("GuideSystem.txt");
            string userTemplate = _configuration.LoadPrompt("GuideUser.txt");

            Dictionary<string, string> values =
                new Dictionary<string, string>
                {
                    ["TOPIC"] = context.Topic ?? string.Empty,
                    ["LANGUAGE"] = context.OutputLanguage ?? string.Empty,
                    ["AGE_GROUP"] = context.AgeGroup ?? string.Empty,
                    ["KNOWLEDGE_LEVEL"] = context.KnowledgeLevel ?? string.Empty,
                    ["TONE"] = context.Tone ?? string.Empty,
                    ["MAX_WORDS"] = context.MaximumWords.ToString(),
                    ["SOURCE_TEXT"] = context.SourceText ?? string.Empty
                };

            string userPrompt = _renderer.Render(userTemplate, values);

            return new PromptPackage
            {
                ContentType = "Guide",
                Model = context.Model,
                PromptVersion = context.PromptVersion,
                SystemInstruction = system,
                UserPrompt = userPrompt,
                MaxOutputTokens = context.MaxOutputTokens,
                PreferencesJson = JsonSerializer.Serialize(new
                {
                    context.AgeGroup,
                    context.KnowledgeLevel,
                    context.Tone,
                    context.MaximumWords,
                    context.OutputLanguage
                })
            };
        }
    }
}
