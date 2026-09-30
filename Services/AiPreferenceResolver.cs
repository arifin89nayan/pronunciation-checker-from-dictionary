using System;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class AiPreferenceResolver
    {
        // Priority:
        // 1) row-specific ContentItem override
        // 2) runtime/UI AiPreference override
        // 3) AISettings.json default
        // 4) emergency code fallback

        public AiGenerationContext ResolveGuide(
            ContentItem item,
            AiPreference runtimePreference,
            AiSettings settings)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            GuideAiSettings defaults = settings.Guide ?? new GuideAiSettings();

            return new AiGenerationContext
            {
                ContentType = "Guide",
                Topic = item.Topic ?? string.Empty,
                OutputLanguage = item.LanguageCode ?? item.Language ?? string.Empty,
                SourceText = item.SourceText ?? string.Empty,

                AgeGroup = FirstText(
                    item.AiAgeGroupOverride,
                    runtimePreference?.AgeGroup,
                    defaults.AgeGroup,
                    "General Visitor"),

                KnowledgeLevel = FirstText(
                    item.AiKnowledgeLevelOverride,
                    runtimePreference?.KnowledgeLevel,
                    defaults.KnowledgeLevel,
                    "Beginner"),

                Tone = FirstText(
                    item.AiToneOverride,
                    runtimePreference?.Tone,
                    defaults.Tone,
                    "Clear and educational"),

                MaximumWords = FirstPositive(
                    item.AiMaximumWordsOverride,
                    runtimePreference?.MaximumWords ?? 0,
                    defaults.MaximumWords,
                    120),

                PromptVersion = FirstText(
                    defaults.PromptVersion,
                    "GUIDE_V1"),

                Model = FirstText(
                    settings.Model,
                    Environment.GetEnvironmentVariable("OPENAI_MODEL"),
                    "gpt-5.6-luna"),

                MaxOutputTokens =
                    defaults.MaxOutputTokens > 0
                        ? defaults.MaxOutputTokens
                        : 800
            };
        }

        public AiGenerationContext ResolveQuiz(
            ContentItem item,
            AiPreference runtimePreference,
            AiSettings settings)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            QuizAiSettings defaults = settings.Quiz ?? new QuizAiSettings();

            return new AiGenerationContext
            {
                ContentType = "Quiz",
                Topic = item.Topic ?? string.Empty,
                OutputLanguage = item.LanguageCode ?? item.Language ?? string.Empty,
                SourceText = item.SourceText ?? string.Empty,

                AgeGroup = FirstText(
                    item.AiAgeGroupOverride,
                    runtimePreference?.AgeGroup,
                    defaults.AgeGroup,
                    "General Visitor"),

                KnowledgeLevel = FirstText(
                    item.AiKnowledgeLevelOverride,
                    runtimePreference?.KnowledgeLevel,
                    defaults.KnowledgeLevel,
                    "Beginner"),

                Difficulty = FirstText(
                    item.AiDifficultyOverride,
                    runtimePreference?.Difficulty,
                    defaults.Difficulty,
                    "Easy"),

                ChoiceCount = FirstPositive(
                    item.AiChoiceCountOverride,
                    runtimePreference?.ChoiceCount ?? 0,
                    defaults.ChoiceCount,
                    4),

                PromptVersion = FirstText(
                    defaults.PromptVersion,
                    "QUIZ_V1"),

                Model = FirstText(
                    settings.Model,
                    Environment.GetEnvironmentVariable("OPENAI_MODEL"),
                    "gpt-5.6-luna"),

                MaxOutputTokens =
                    defaults.MaxOutputTokens > 0
                        ? defaults.MaxOutputTokens
                        : 1000
            };
        }

        private static string FirstText(params string[] values)
        {
            foreach (string value in values)
            {
                if (!string.IsNullOrWhiteSpace(value))
                    return value.Trim();
            }

            return string.Empty;
        }

        private static int FirstPositive(params int[] values)
        {
            foreach (int value in values)
            {
                if (value > 0)
                    return value;
            }

            return 0;
        }
    }
}
