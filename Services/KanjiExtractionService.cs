using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class KanjiExtractionService
    {
        private static readonly HttpClient Http = new HttpClient
        {
            Timeout = TimeSpan.FromMinutes(2)
        };

        private readonly AppConfig _config;
        private readonly FixedDictionaryService _dictionary;

        public KanjiExtractionService(AppConfig config, FixedDictionaryService dictionary)
        {
            _config = config;
            _dictionary = dictionary;
        }

        public async Task<TtsExtractionResult> ExtractAsync(string script)
        {
            if (string.IsNullOrWhiteSpace(script))
                throw new ArgumentException("Script is empty.");

            if (string.IsNullOrWhiteSpace(_config.OpenAiApiKey))
                throw new InvalidOperationException("OpenAI API key is not configured. Please set OPENAI_API_KEY or appsettings.json.");

            string json = await CallOpenAiAsync(script).ConfigureAwait(false);

            OpenAiKanjiResult parsed = JsonSerializer.Deserialize<OpenAiKanjiResult>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (parsed == null || parsed.terms == null)
                throw new Exception("OpenAI returned empty extraction result.");

            var result = new TtsExtractionResult();
            result.Terms = new List<TtsTerm>();

            var seen = new HashSet<string>(StringComparer.Ordinal);

            foreach (var item in parsed.terms)
            {
                if (item == null)
                    continue;

                string word = (item.word ?? "").Trim();
                string hira = FixedDictionaryService.NormalizeToHiragana((item.hiragana ?? "").Trim());

                if (string.IsNullOrWhiteSpace(word))
                    continue;

                if (!ContainsKanji(word))
                    continue;

                if (!seen.Add(word))
                    continue;

                string fixedReading;

                var term = new TtsTerm
                {
                    Word = word,
                    Hiragana = hira,
                    ModelSuggestedReading = hira,
                    SourceSentence = item.source_sentence ?? "",
                    Category = string.IsNullOrWhiteSpace(item.category) ? "general_word" : item.category,
                    ReviewRequired = false,
                    DictionaryStatus = "new"
                };

                if (_dictionary.TryGetReading(word, out fixedReading))
                {
                    fixedReading = FixedDictionaryService.NormalizeToHiragana(fixedReading);

                    if (string.Equals(fixedReading, hira, StringComparison.Ordinal))
                    {
                        term.Hiragana = fixedReading;
                        term.ModelSuggestedReading = hira;
                        term.DictionaryStatus = "matched";
                        term.ReviewRequired = false;
                    }
                    else
                    {
                        term.Hiragana = fixedReading;
                        term.ModelSuggestedReading = hira;
                        term.DictionaryStatus = "conflict";
                        term.ReviewRequired = true;
                    }
                }
                else
                {
                    term.DictionaryStatus = "new";
                    term.ReviewRequired = NeedHumanReview(term, item.confidence);
                }

                result.Terms.Add(term);
            }

            result.Terms = result.Terms
                .OrderByDescending(t => t.Word.Length)
                .ThenBy(t => t.Word)
                .ToList();

            result.Summary = new TtsExtractionSummary
            {
                TotalTerms = result.Terms.Count,
                FixedDictionaryMatches = result.Terms.Count(t => t.DictionaryStatus == "matched"),
                ReviewRequiredCount = result.Terms.Count(t => t.ReviewRequired),
                ConflictCount = result.Terms.Count(t => t.DictionaryStatus == "conflict")
            };

            return result;
        }

        private async Task<string> CallOpenAiAsync(string script)
        {
            string systemPrompt =
@"You are a Japanese TTS dictionary extraction engine.

Task:
Extract important Japanese kanji words from the input script for TTS pronunciation control.

Rules:
1. Extract kanji words, compound nouns, place names, shrine names, museum terms, historical terms, cultural terms, and technical terms.
2. Do not split compound words incorrectly. Example: 献額 must stay 献額, not 献 and 額.
3. Hiragana reading must be natural Japanese pronunciation.
4. Prefer full terms over short contained terms. Example: 上米内 is better than 米内 if the script contains 上米内.
5. Use the fixed dictionary as highest priority if given.
6. Return only JSON that matches the schema.";

            string userPrompt =
                _dictionary.BuildPromptBlock(500) +
                "\n\nJapanese Script:\n" +
                script;

            object requestBody = new
            {
                model = _config.LlmModel,
                input = new object[]
                {
                    new
                    {
                        role = "system",
                        content = new object[]
                        {
                            new
                            {
                                type = "input_text",
                                text = systemPrompt
                            }
                        }
                    },
                    new
                    {
                        role = "user",
                        content = new object[]
                        {
                            new
                            {
                                type = "input_text",
                                text = userPrompt
                            }
                        }
                    }
                },
                temperature = 0,
                text = new
                {
                    format = new
                    {
                        type = "json_schema",
                        name = "kanji_extraction_result",
                        strict = true,
                        schema = BuildJsonSchema()
                    }
                }
            };

            string body = JsonSerializer.Serialize(requestBody);

            using (var req = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/responses"))
            {
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _config.OpenAiApiKey);
                req.Content = new StringContent(body, Encoding.UTF8, "application/json");

                using (var resp = await Http.SendAsync(req).ConfigureAwait(false))
                {
                    string responseText = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!resp.IsSuccessStatusCode)
                    {
                        throw new Exception("OpenAI API failed: " + (int)resp.StatusCode + "\n" + responseText);
                    }

                    return ExtractOutputText(responseText);
                }
            }
        }

        private static object BuildJsonSchema()
        {
            return new
            {
                type = "object",
                additionalProperties = false,
                required = new[] { "terms" },
                properties = new
                {
                    terms = new
                    {
                        type = "array",
                        items = new
                        {
                            type = "object",
                            additionalProperties = false,
                            required = new[]
                            {
                                "word",
                                "hiragana",
                                "source_sentence",
                                "category",
                                "confidence"
                            },
                            properties = new
                            {
                                word = new
                                {
                                    type = "string",
                                    description = "Japanese kanji word or compound term."
                                },
                                hiragana = new
                                {
                                    type = "string",
                                    description = "Correct hiragana reading."
                                },
                                source_sentence = new
                                {
                                    type = "string",
                                    description = "Original sentence where the word appears."
                                },
                                category = new
                                {
                                    type = "string",
                                    description = "place_name, shrine_name, museum_name, cultural_term, historical_term, technical_term, or general_word."
                                },
                                confidence = new
                                {
                                    type = "number",
                                    description = "Confidence from 0 to 1."
                                }
                            }
                        }
                    }
                }
            };
        }

        private static string ExtractOutputText(string responseJson)
        {
            using (JsonDocument doc = JsonDocument.Parse(responseJson))
            {
                JsonElement root = doc.RootElement;

                JsonElement outputText;

                if (root.TryGetProperty("output_text", out outputText))
                {
                    return outputText.GetString();
                }

                JsonElement output;

                if (root.TryGetProperty("output", out output) &&
                    output.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement outputItem in output.EnumerateArray())
                    {
                        JsonElement content;

                        if (!outputItem.TryGetProperty("content", out content) ||
                            content.ValueKind != JsonValueKind.Array)
                        {
                            continue;
                        }

                        foreach (JsonElement contentItem in content.EnumerateArray())
                        {
                            JsonElement text;

                            if (contentItem.TryGetProperty("text", out text))
                            {
                                return text.GetString();
                            }
                        }
                    }
                }

                throw new Exception("Could not read output text from OpenAI response:\n" + responseJson);
            }
        }

        private static bool NeedHumanReview(TtsTerm term, double confidence)
        {
            if (confidence < 0.85)
                return true;

            string cat = term.Category ?? "";

            if (cat == "place_name" ||
                cat == "shrine_name" ||
                cat == "museum_name" ||
                cat == "historical_term" ||
                cat == "cultural_term" ||
                cat == "technical_term")
            {
                return true;
            }

            return false;
        }

        private static bool ContainsKanji(string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            foreach (char c in text)
            {
                if (c >= '\u4E00' && c <= '\u9FFF')
                    return true;
            }

            return false;
        }

        private class OpenAiKanjiResult
        {
            public List<OpenAiKanjiTerm> terms { get; set; }
        }

        private class OpenAiKanjiTerm
        {
            public string word { get; set; }
            public string hiragana { get; set; }
            public string source_sentence { get; set; }
            public string category { get; set; }
            public double confidence { get; set; }
        }
    }
}