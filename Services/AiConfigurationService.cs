using System;
using System.IO;
using System.Text.Json;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class AiConfigurationService
    {
        private readonly string _aiConfigFolder;
        private readonly string _promptFolder;

        public AiConfigurationService(string workspace)
        {
            if (string.IsNullOrWhiteSpace(workspace))
                throw new ArgumentException("Workspace is required.", nameof(workspace));

            _aiConfigFolder = Path.Combine(workspace, "AIConfig");
            _promptFolder = Path.Combine(_aiConfigFolder, "Prompts");
        }

        public AiSettings LoadSettings()
        {
            string path = Path.Combine(_aiConfigFolder, "AISettings.json");

            if (!File.Exists(path))
                throw new FileNotFoundException("AISettings.json was not found.", path);

            string json = File.ReadAllText(path);

            AiSettings settings = JsonSerializer.Deserialize<AiSettings>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true
                });

            if (settings == null)
                throw new InvalidOperationException("AISettings.json could not be parsed.");

            ValidateSettings(settings);
            return settings;
        }

        public string LoadPrompt(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Prompt filename is required.", nameof(fileName));

            string path = Path.Combine(_promptFolder, fileName);

            if (!File.Exists(path))
                throw new FileNotFoundException("Prompt file was not found.", path);

            string text = File.ReadAllText(path);

            if (string.IsNullOrWhiteSpace(text))
                throw new InvalidOperationException("Prompt file is empty: " + path);

            return text;
        }

        public void ValidateWorkspaceConfiguration()
        {
            LoadSettings();
            LoadPrompt("GuideSystem.txt");
            LoadPrompt("GuideUser.txt");
            LoadPrompt("QuizSystem.txt");
            LoadPrompt("QuizUser.txt");
        }

        private static void ValidateSettings(AiSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.Model))
                throw new InvalidOperationException("AISettings.Model is required.");

            if (settings.Guide == null)
                throw new InvalidOperationException("AISettings.Guide is required.");

            if (settings.Quiz == null)
                throw new InvalidOperationException("AISettings.Quiz is required.");

            if (settings.Guide.MaximumWords <= 0)
                throw new InvalidOperationException("Guide.MaximumWords must be greater than zero.");

            if (settings.Guide.MaxOutputTokens <= 0)
                throw new InvalidOperationException("Guide.MaxOutputTokens must be greater than zero.");

            if (settings.Quiz.ChoiceCount < 2)
                throw new InvalidOperationException("Quiz.ChoiceCount must be at least 2.");

            if (settings.Quiz.MaxOutputTokens <= 0)
                throw new InvalidOperationException("Quiz.MaxOutputTokens must be greater than zero.");

            if (string.IsNullOrWhiteSpace(settings.Guide.PromptVersion))
                throw new InvalidOperationException("Guide.PromptVersion is required.");

            if (string.IsNullOrWhiteSpace(settings.Quiz.PromptVersion))
                throw new InvalidOperationException("Quiz.PromptVersion is required.");
        }
    }
}
