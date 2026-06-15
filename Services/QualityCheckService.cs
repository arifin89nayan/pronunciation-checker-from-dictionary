using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class QualityCheckService
    {
        private static readonly HttpClient Http = new HttpClient { Timeout = TimeSpan.FromMinutes(2) };
        private readonly AppConfig _config;

        public QualityCheckService(AppConfig config) => _config = config;

        public async Task<(string recognized, List<QualityCheckRow> rows)> RunAsync(
            string wavPath, string originalScript, IEnumerable<string> dictionaryWords)
        {
            string recognized = await TranscribeAsync(wavPath).ConfigureAwait(false);

            double sttScore = SimilarityPercent(Strip(originalScript), Strip(recognized));
            var dictWords = dictionaryWords?.ToList() ?? new List<string>();
            int present = dictWords.Count(w => recognized.Contains(w) ||
                                               recognized.Contains(FixedDictionaryService.NormalizeToHiragana(w)));
            double dictScore = dictWords.Count == 0 ? 100 : 100.0 * present / dictWords.Count;

            var rows = new List<QualityCheckRow>
            {
                Row("STT Compare", sttScore, 95, 90),
                Row("Dictionary", dictScore, 100, 90),
                // Pronunciation assessment needs the Pronunciation Assessment API;
                // here we reuse STT similarity as a proxy so the screen is functional.
                Row("Pronunciation", Math.Min(sttScore, dictScore), 95, 85)
            };
            return (recognized, rows);
        }

        private static QualityCheckRow Row(string type, double score, double pass, double warn)
        {
            string result = score >= pass ? "Pass" : score >= warn ? "Warning" : "Fail";
            return new QualityCheckRow
            {
                CheckType = type,
                Result = result,
                Score = score.ToString("0.0") + "%",
                Details = result == "Pass" ? "View" : "Review"
            };
        }

        /// <summary>Short-audio STT REST call. Returns recognized text.</summary>
        public async Task<string> TranscribeAsync(string wavPath)
        {
            if (string.IsNullOrWhiteSpace(_config.AzureSpeechKey))
                throw new InvalidOperationException("Azure Speech key is not configured.");
            if (!File.Exists(wavPath))
                throw new FileNotFoundException("Audio file not found.", wavPath);

            string endpoint =
                $"https://{_config.AzureSpeechRegion}.stt.speech.microsoft.com/speech/recognition/conversation/cognitiveservices/v1?language=ja-JP";

            HttpRequestMessage req = null;
            HttpResponseMessage resp = null;
            try
            {
                req = new HttpRequestMessage(HttpMethod.Post, endpoint);
                req.Headers.Add("Ocp-Apim-Subscription-Key", _config.AzureSpeechKey);
                req.Headers.Add("Accept", "application/json");
                byte[] audio;
                using (var fs = new FileStream(wavPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true))
                {
                    audio = new byte[fs.Length];
                    await fs.ReadAsync(audio, 0, (int)fs.Length).ConfigureAwait(false);
                }

                var content = new ByteArrayContent(audio);
                content.Headers.TryAddWithoutValidation("Content-Type",
                    "audio/wav; codecs=audio/pcm; samplerate=24000");
                req.Content = content;

                resp = await Http.SendAsync(req).ConfigureAwait(false);
                string json = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!resp.IsSuccessStatusCode)
                    throw new Exception($"STT failed ({(int)resp.StatusCode}): {json}");

                using (var doc = JsonDocument.Parse(json))
                {
                    return doc.RootElement.TryGetProperty("DisplayText", out var dt)
                        ? dt.GetString() ?? "" : "";
                }
            }
            finally
            {
                req?.Dispose();
                resp?.Dispose();
            }
        }

        // ---- similarity (normalized Levenshtein) ----
        private static double SimilarityPercent(string a, string b)
        {
            if (a.Length == 0 && b.Length == 0) return 100;
            int dist = Levenshtein(a, b);
            int max = Math.Max(a.Length, b.Length);
            return max == 0 ? 100 : 100.0 * (max - dist) / max;
        }

        private static int Levenshtein(string a, string b)
        {
            var d = new int[a.Length + 1, b.Length + 1];
            for (int i = 0; i <= a.Length; i++) d[i, 0] = i;
            for (int j = 0; j <= b.Length; j++) d[0, j] = j;
            for (int i = 1; i <= a.Length; i++)
                for (int j = 1; j <= b.Length; j++)
                {
                    int cost = a[i - 1] == b[j - 1] ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            return d[a.Length, b.Length];
        }

        private static string Strip(string s) =>
            new string((s ?? "").Where(c => !char.IsWhiteSpace(c) && !char.IsPunctuation(c)).ToArray());
    }
}
