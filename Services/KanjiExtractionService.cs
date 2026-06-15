using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class KanjiExtractionService
    {
        private const string ApiUrl = "https://api.anthropic.com/v1/messages";
        private static readonly HttpClient Http = new HttpClient { Timeout = TimeSpan.FromMinutes(3) };

        private readonly AppConfig _config;
        private readonly FixedDictionaryService _dict;

        public KanjiExtractionService(AppConfig config, FixedDictionaryService dict)
        {
            _config = config;
            _dict = dict;
        }

        public async Task<TtsExtractionResult> ExtractAsync(string script)
        {
            if (string.IsNullOrWhiteSpace(script))
                throw new ArgumentException("Script is empty.");
            if (string.IsNullOrWhiteSpace(_config.LlmApiKey))
                throw new InvalidOperationException("LLM API key is not configured (appsettings.json or ANTHROPIC_API_KEY).");

            string userMessage = _dict.BuildPromptBlock() +
                                  "\n\nInput Japanese Script:\n" + script;

            var body = new
            {
                model = _config.LlmModel,
                max_tokens = 8000,
                system = SystemPrompt,
                messages = new[] { new { role = "user", content = userMessage } }
            };

            HttpRequestMessage req = null;
            HttpResponseMessage resp = null;
            try
            {
                req = new HttpRequestMessage(HttpMethod.Post, ApiUrl);
                req.Headers.Add("x-api-key", _config.LlmApiKey);
                req.Headers.Add("anthropic-version", "2023-06-01");
                req.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

                resp = await Http.SendAsync(req).ConfigureAwait(false);
                string raw = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!resp.IsSuccessStatusCode)
                    throw new Exception("Extraction API failed: " + raw);

                string text = ExtractText(raw);
                string json = StripFences(text);

                var result = JsonSerializer.Deserialize<TtsExtractionResult>(json)
                             ?? throw new Exception("Model returned no parsable list.");
              

                if (result.Terms == null)
                {
                    result.Terms = new List<TtsTerm>();
                }

                EnforceDictionary(result);
                RecomputeSummary(result);
                return result;
            }
            finally
            {
                if (resp != null)
                    resp.Dispose();
                if (req != null)
                    req.Dispose();
            }
        }

        // ---- rules 5 & 6: dictionary wins, conflicts flagged ----
        private void EnforceDictionary(TtsExtractionResult result)
        {
            foreach (var t in result.Terms)
            {
                if (_dict.TryGetReading(t.Word, out var dicReading))
                {
                    string dic = FixedDictionaryService.NormalizeToHiragana(dicReading);
                    string model = FixedDictionaryService.NormalizeToHiragana(t.Hiragana);
                    if (string.Equals(dic, model, StringComparison.Ordinal))
                    {
                        t.DictionaryStatus = "matched";
                        t.ReadingSource = "fixed_dictionary";
                        t.Hiragana = dic;
                        if (string.IsNullOrEmpty(t.Confidence)) t.Confidence = "high";
                    }
                    else
                    {
                        t.ModelSuggestedReading = model;
                        t.Hiragana = dic;                 // keep dictionary as final
                        t.DictionaryStatus = "conflict";
                        t.ReadingSource = "fixed_dictionary";
                        t.ReviewRequired = true;
                        t.Confidence = "low";
                        t.Reason = $"Dictionary reading kept; model suggested {model}.";
                    }
                }
                else
                {
                    t.DictionaryStatus = "new";
                    if (string.IsNullOrEmpty(t.ReadingSource)) t.ReadingSource = "model_suggestion";
                }
            }
        }

        private static void RecomputeSummary(TtsExtractionResult r)
        {
            r.Summary = new TtsExtractionSummary
            {
                TotalTerms = r.Terms.Count,
                FixedDictionaryMatches = r.Terms.Count(t => t.DictionaryStatus == "matched" || t.DictionaryStatus == "conflict"),
                NewTerms = r.Terms.Count(t => t.DictionaryStatus == "new"),
                ReviewRequiredCount = r.Terms.Count(t => t.ReviewRequired),
                ConflictCount = r.Terms.Count(t => t.DictionaryStatus == "conflict")
            };
        }

        private static string ExtractText(string apiJson)
        {
            JsonDocument doc = null;
            try
            {
                doc = JsonDocument.Parse(apiJson);
                var sb = new StringBuilder();
                foreach (var block in doc.RootElement.GetProperty("content").EnumerateArray())
                    if (block.TryGetProperty("type", out var ty) && ty.GetString() == "text")
                        sb.Append(block.GetProperty("text").GetString());
                return sb.ToString();
            }
            finally
            {
                if (doc != null)
                    doc.Dispose();
            }
        }

        private static string StripFences(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;
            string t = text.Trim();
            if (t.StartsWith("```"))
            {
                int nl = t.IndexOf('\n');
                if (nl >= 0) t = t.Substring(nl + 1);
                int last = t.LastIndexOf("```", StringComparison.Ordinal);
                if (last >= 0) t = t.Substring(0, last);
            }
            int s = t.IndexOf('{'), e = t.LastIndexOf('}');
            if (s >= 0 && e > s) t = t.Substring(s, e - s + 1);
            return t.Trim();
        }

        private const string SystemPrompt = @"You are a Japanese TTS pronunciation extraction assistant for a human-in-the-loop TTS script generation system.

Analyze the input Japanese script and generate a pronunciation control list so Azure TTS does not incorrectly guess difficult kanji readings.

Rules:
1. Extract kanji-containing words or meaningful phrases, not single kanji unless the single kanji is an important standalone word. Prefer word/phrase level (e.g. 江戸時代, 大國神社, 都南歴史民俗資料館, 浮世絵, 献額).
2. Provide a hiragana reading for each term.
3. If the term is in the Fixed Dictionary, use that reading (highest priority).
4. If your reading differs from the Fixed Dictionary, mark it a conflict and keep the dictionary reading; set review_required=true.
5. Set review_required=true for: place/shrine/temple/museum/person/organization names, historical/cultural/technical terms, rare kanji, uncertain or multiple-reading words, and conflicts.
6. Never guess silently. Give your best hiragana but set review_required=true when uncertain.
7. Do not add new words to the permanent dictionary; they go to the Confirmation List for human review.
8. Return ONLY valid JSON, no prose, in this structure:
{""summary"":{""total_terms"":0,""fixed_dictionary_matches"":0,""new_terms"":0,""review_required_count"":0,""conflict_count"":0},""terms"":[{""word"":"""",""hiragana"":"""",""source_sentence"":"""",""category"":""fixed_dictionary|place_name|shrine_name|museum_name|cultural_term|historical_term|technical_term|general_word|unknown"",""dictionary_status"":""matched|new|conflict"",""reading_source"":""fixed_dictionary|model_suggestion|uncertain"",""review_required"":true,""confidence"":""high|medium|low"",""reason"":""""}]}
confidence: high=common/dictionary, medium=normal kanji likely reading, low=proper noun/rare/uncertain.
Be conservative: if any risk of wrong pronunciation, set review_required=true.";
    }
}
