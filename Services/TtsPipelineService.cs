using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1.UIDesign
{
    public class TtsPronunciationRow
    {
        public string Word { get; set; }
        public string Hiragana { get; set; }
        public string Source { get; set; }
        public string UseType { get; set; }
    }

    public class TtsPipelineResult
    {
        public List<TtsPronunciationRow> FixedList { get; set; }
        public List<TtsPronunciationRow> GeneralList { get; set; }
        public List<TtsPronunciationRow> FinalTtsList { get; set; }
        public string Ssml { get; set; }
    }

    public static class TtsPipelineService
    {
        public static TtsPipelineResult Build(
            string originalScript,
            List<Inputtext.KanjiItem> reviewedItems,
            string voiceName)
        {
            if (reviewedItems == null)
                reviewedItems = new List<Inputtext.KanjiItem>();

            ValidateBeforeTts(reviewedItems);

            List<TtsPronunciationRow> fixedList =
                GenerateFixedList(reviewedItems);

            List<TtsPronunciationRow> generalList =
                GenerateGeneralList(reviewedItems);

            List<TtsPronunciationRow> finalList =
                MergeFixedAndGeneral(fixedList, generalList);

            string ssml =
                BuildAzureSsml(originalScript, finalList, voiceName);

            return new TtsPipelineResult
            {
                FixedList = fixedList,
                GeneralList = generalList,
                FinalTtsList = finalList,
                Ssml = ssml
            };
        }

        private static List<TtsPronunciationRow> GenerateGeneralList(
            List<Inputtext.KanjiItem> items)
        {
            return items
                .Where(x => x.DictionaryStatus == "new" || x.Source == "ChatGPT")
                .Where(x =>
                    !string.IsNullOrWhiteSpace(x.Word) &&
                    !string.IsNullOrWhiteSpace(x.Hiragana))
                .Select(x => new TtsPronunciationRow
                {
                    Word = JapaneseTextNormalizer.NormalizeText(x.Word),
                    Hiragana = JapaneseTextNormalizer.ToHiragana(x.Hiragana),
                    Source = "General",
                    UseType = "Pronunciation"
                })
                .ToList();
        }

        private static List<TtsPronunciationRow> GenerateFixedList(
            List<Inputtext.KanjiItem> items)
        {
            return items
                .Where(x =>
                    x.Source == "Fixed" ||
                    x.DictionaryStatus == "matched" ||
                    x.DictionaryStatus == "conflict")
                .Where(x =>
                    !string.IsNullOrWhiteSpace(x.Word) &&
                    !string.IsNullOrWhiteSpace(x.Hiragana))
                .Select(x => new TtsPronunciationRow
                {
                    Word = JapaneseTextNormalizer.NormalizeText(x.Word),
                    Hiragana = JapaneseTextNormalizer.ToHiragana(x.Hiragana),
                    Source = "Fixed",
                    UseType = "Pronunciation"
                })
                .ToList();
        }

        private static List<TtsPronunciationRow> MergeFixedAndGeneral(
            List<TtsPronunciationRow> fixedList,
            List<TtsPronunciationRow> generalList)
        {
            var result = new List<TtsPronunciationRow>();
            var seen = new HashSet<string>(StringComparer.Ordinal);

            foreach (var row in fixedList)
            {
                string key = JapaneseTextNormalizer.NormalizeText(row.Word);

                if (seen.Add(key))
                    result.Add(row);
            }

            foreach (var row in generalList)
            {
                string key = JapaneseTextNormalizer.NormalizeText(row.Word);

                if (seen.Add(key))
                    result.Add(row);
            }

            return result;
        }

        public static string BuildAzureSsml(
            string originalScript,
            IEnumerable<TtsPronunciationRow> rows,
            string voiceName)
        {
            string body = System.Security.SecurityElement.Escape(originalScript ?? "");

            foreach (var r in rows
                .Where(x => !string.IsNullOrWhiteSpace(x.Word))
                .OrderByDescending(x => x.Word.Length))
            {
                string word = System.Security.SecurityElement.Escape(r.Word);
                string hira = System.Security.SecurityElement.Escape(r.Hiragana);

                string tag = "<sub alias=\"" + hira + "\">" + word + "</sub>";

                body = body.Replace(word, tag);
            }

            var sb = new StringBuilder();

            sb.AppendLine("<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"ja-JP\">");
            sb.AppendLine("  <voice name=\"" + voiceName + "\">");
            sb.AppendLine("    <prosody rate=\"+0%\" pitch=\"+0%\">");
            sb.AppendLine("      " + body);
            sb.AppendLine("    </prosody>");
            sb.AppendLine("  </voice>");
            sb.AppendLine("</speak>");

            return sb.ToString();
        }

        private static void ValidateBeforeTts(List<Inputtext.KanjiItem> items)
        {
            var missing = items
                .Where(x => !string.IsNullOrWhiteSpace(x.Word))
                .Where(x => string.IsNullOrWhiteSpace(x.Hiragana))
                .Select(x => x.Word)
                .ToList();

            if (missing.Count > 0)
            {
                throw new Exception(
                    "Some words have empty hiragana. Please fix them in Kanji Review:\n\n" +
                    string.Join("\n", missing)
                );
            }
        }
    }
}