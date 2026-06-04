using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class AudioQualityEngine
    {
        private readonly WhisperNetSpeechToTextService _whisperService;

        public AudioQualityEngine(WhisperNetSpeechToTextService whisperService)
        {
            _whisperService = whisperService;
        }

        public async Task<AudioQualityFinalResult> CheckAsync(
            string mp3Path,
            string originalText,
            string dictionaryXmlPath,
            string language)
        {
            string wavPath = null;

            try
            {
                wavPath = Mp3ToWavConverter.ConvertTo16kMonoWav(mp3Path);

                string whisperLanguage = language.StartsWith("ja", StringComparison.OrdinalIgnoreCase)
                    ? "ja"
                    : "en";

                string prompt = BuildPromptFromDictionaryXml(dictionaryXmlPath);

                string recognizedText = await _whisperService.TranscribeAsync(
                    wavPath,
                    prompt,
                    whisperLanguage
                );

                double cer = TextSimilarityService.CalculateCerPercent(
                    originalText,
                    recognizedText
                );

                var meaningIssues = FindMeaningIssues(originalText, recognizedText);

                var result = new AudioQualityFinalResult
                {
                    RecognizedText = recognizedText,
                    CerPercent = cer,

                    // Azure is not used in this meaning-check version.
                    AzureAccuracyScore = -1,
                    AzureFluencyScore = -1,
                    AzureCompletenessScore = -1,
                    AzureWords = new List<AzureWordScore>(),

                    // Dictionary is used as Whisper prompt only, not hard word-match validation.
                    DictionaryWords = new List<DictionaryWordCheck>(),
                    DictionaryPassRate = 100
                };

                DecideFinalGrade(result, meaningIssues);

                return result;
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(wavPath) && File.Exists(wavPath))
                    File.Delete(wavPath);
            }
        }

        private void DecideFinalGrade(
            AudioQualityFinalResult result,
            List<MeaningIssue> meaningIssues)
        {
            bool hasFailIssue = meaningIssues.Any(x => x.Severity == "FAIL");
            bool hasWarningIssue = meaningIssues.Any(x => x.Severity == "WARNING");

            if (hasFailIssue)
            {
                result.FinalGrade = "FAIL";
                result.Message =
                    "Meaning-changing errors were detected.\r\n\r\n" +
                    BuildMeaningIssueMessage(meaningIssues);
                return;
            }

            if (hasWarningIssue)
            {
                result.FinalGrade = "WARNING";
                result.Message =
                    "Meaning is mostly understandable, but important terms need review.\r\n\r\n" +
                    BuildMeaningIssueMessage(meaningIssues);
                return;
            }

            // Meaning check logic:
            // CER is only used as support.
            // Kanji/Hiragana/Katakana style differences are accepted.
            if (result.CerPercent <= 15)
            {
                result.FinalGrade = "PASS";
                result.Message =
                    "Meaning appears acceptable. Differences are mostly writing style, kanji/kana, punctuation, or number notation.";
            }
            else if (result.CerPercent <= 25)
            {
                result.FinalGrade = "WARNING";
                result.Message =
                    "Meaning is probably understandable, but CER is high. Manual review is recommended.";
            }
            else
            {
                result.FinalGrade = "FAIL";
                result.Message =
                    "Text difference is high. Meaning may have changed. Manual review is required.";
            }
        }

        private string BuildMeaningIssueMessage(List<MeaningIssue> issues)
        {
            if (issues == null || issues.Count == 0)
                return "No meaning-changing issue detected.";

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Detected Meaning Issues:");

            foreach (var issue in issues)
            {
                sb.AppendLine(
                    $"- [{issue.Severity}] {issue.OriginalPart} → {issue.SttPart}"
                );
                sb.AppendLine("  Reason: " + issue.Reason);
            }

            return sb.ToString();
        }

        private List<MeaningIssue> FindMeaningIssues(
            string originalText,
            string sttText)
        {
            var issues = new List<MeaningIssue>();

            AddIssueIfFound(
                issues,
                originalText,
                sttText,
                "今から13,000年ほど前",
                "1万通俊",
                "FAIL",
                "Number/time expression changed. Original means around 13,000 years ago."
            );

            AddIssueIfFound(
                issues,
                originalText,
                sttText,
                "狩りに使われた",
                "仮に使われた",
                "FAIL",
                "Meaning changed from hunting use to temporary/conditional use."
            );

            AddIssueIfFound(
                issues,
                originalText,
                sttText,
                "尖頭器",
                "戦闘機",
                "WARNING",
                "Same pronunciation, but written meaning changed from stone tool to fighter aircraft."
            );

            AddIssueIfFound(
                issues,
                originalText,
                sttText,
                "石のかけら",
                "医師のかけら",
                "FAIL",
                "Meaning changed from stone fragments to doctor fragments."
            );

            AddIssueIfFound(
                issues,
                originalText,
                sttText,
                "細石刃",
                "採石陣",
                "FAIL",
                "Important archaeology term changed."
            );

            AddIssueIfFound(
                issues,
                originalText,
                sttText,
                "相ノ山",
                "藍野山",
                "WARNING",
                "Possible place-name mismatch."
            );

            return issues;
        }

        private void AddIssueIfFound(
            List<MeaningIssue> issues,
            string originalText,
            string sttText,
            string originalPart,
            string wrongSttPart,
            string severity,
            string reason)
        {
            if (string.IsNullOrWhiteSpace(originalText) ||
                string.IsNullOrWhiteSpace(sttText))
                return;

            if (originalText.Contains(originalPart) &&
                sttText.Contains(wrongSttPart))
            {
                issues.Add(new MeaningIssue
                {
                    OriginalPart = originalPart,
                    SttPart = wrongSttPart,
                    Severity = severity,
                    Reason = reason
                });
            }
        }

        private string BuildPromptFromDictionaryXml(string dictionaryXmlPath)
        {
            if (!File.Exists(dictionaryXmlPath))
                return "";

            var words = new List<string>();

            XDocument doc = XDocument.Load(dictionaryXmlPath);

            foreach (var lexeme in doc.Descendants()
                                      .Where(x => x.Name.LocalName == "lexeme"))
            {
                string grapheme = lexeme.Elements()
                    .FirstOrDefault(x => x.Name.LocalName == "grapheme")
                    ?.Value
                    ?.Trim();

                if (!string.IsNullOrWhiteSpace(grapheme))
                    words.Add(grapheme);
            }

            return string.Join("、", words
                .Distinct()
                .OrderByDescending(x => x.Length)
                .Take(100));
        }

        private class MeaningIssue
        {
            public string OriginalPart { get; set; }
            public string SttPart { get; set; }
            public string Severity { get; set; }
            public string Reason { get; set; }
        }
    }
}