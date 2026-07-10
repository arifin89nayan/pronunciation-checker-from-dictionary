using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1.UIDesign
{
    public class TtsPronunciationRow
    {
        public string Word { get; set; }
        public string Hiragana { get; set; }   // carries the READING in any language
        public string Source { get; set; }
        public string UseType { get; set; }
    }

    public class TtsPipelineResult
    {
        public List<TtsPronunciationRow> FixedList { get; set; }
        public List<TtsPronunciationRow> GeneralList { get; set; }
        public List<TtsPronunciationRow> FinalTtsList { get; set; }
        public string Ssml { get; set; }
        public ILanguageProfile Profile { get; set; }
    }

    public static class TtsPipelineService
    {
        /// <summary>Backward-compatible overload: Japanese behavior, same signature as before.</summary>
        public static TtsPipelineResult Build(
            string originalScript,
            List<Inputtext.KanjiItem> reviewedItems,
            string voiceName)
        {
            return Build(originalScript, reviewedItems, LanguageRegistry.Default, voiceName);
        }

        public static TtsPipelineResult Build(
            string originalScript,
            List<Inputtext.KanjiItem> reviewedItems,
            ILanguageProfile profile,
            string voiceName)
        {
            if (profile == null)
                profile = LanguageRegistry.Default;

            if (reviewedItems == null)
                reviewedItems = new List<Inputtext.KanjiItem>();

            if (string.IsNullOrWhiteSpace(voiceName))
                voiceName = profile.DefaultVoice;

            ValidateBeforeTts(reviewedItems);

            List<TtsPronunciationRow> fixedList =
                GenerateFixedList(reviewedItems, profile);

            List<TtsPronunciationRow> generalList =
                GenerateGeneralList(reviewedItems, profile);

            List<TtsPronunciationRow> finalList =
                MergeFixedAndGeneral(fixedList, generalList, profile);

            string ssml =
                BuildAzureSsml(originalScript, finalList, profile, voiceName);

            return new TtsPipelineResult
            {
                FixedList = fixedList,
                GeneralList = generalList,
                FinalTtsList = finalList,
                Ssml = ssml,
                Profile = profile
            };
        }

        private static List<TtsPronunciationRow> GenerateGeneralList(
            List<Inputtext.KanjiItem> items,
            ILanguageProfile profile)
        {
            return items
                .Where(x => x.DictionaryStatus == "new" || x.Source == "ChatGPT")
                .Where(x =>
                    !string.IsNullOrWhiteSpace(x.Word) &&
                    !string.IsNullOrWhiteSpace(x.Hiragana))
                .Select(x => new TtsPronunciationRow
                {
                    Word = profile.NormalizeText(x.Word),
                    Hiragana = profile.NormalizeReading(x.Hiragana),
                    Source = "General",
                    UseType = "Pronunciation"
                })
                .ToList();
        }

        private static List<TtsPronunciationRow> GenerateFixedList(
            List<Inputtext.KanjiItem> items,
            ILanguageProfile profile)
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
                    Word = profile.NormalizeText(x.Word),
                    Hiragana = profile.NormalizeReading(x.Hiragana),
                    Source = "Fixed",
                    UseType = "Pronunciation"
                })
                .ToList();
        }

        private static List<TtsPronunciationRow> MergeFixedAndGeneral(
            List<TtsPronunciationRow> fixedList,
            List<TtsPronunciationRow> generalList,
            ILanguageProfile profile)
        {
            var result = new List<TtsPronunciationRow>();

            var seen = new HashSet<string>(
                profile.UsesWordBoundaryMatching
                    ? StringComparer.OrdinalIgnoreCase
                    : StringComparer.Ordinal);

            foreach (var row in fixedList)
            {
                string key = profile.NormalizeText(row.Word);

                if (seen.Add(key))
                    result.Add(row);
            }

            foreach (var row in generalList)
            {
                string key = profile.NormalizeText(row.Word);

                if (seen.Add(key))
                    result.Add(row);
            }

            return result;
        }

        // -------------------------------------------------------------------
        // SSML generation.
        //
        // Two-phase substitution: every matched word is first replaced by a
        // unique private-use token, and tokens are expanded into tags at the
        // very end. This fixes a subtle bug in the old single-pass version:
        // a shorter dictionary word could previously match INSIDE the alias
        // text of an already-inserted <sub> tag and corrupt the SSML.
        //
        // Word-boundary languages use \b + IgnoreCase so "read" cannot match
        // inside "bread" and "worcester" matches "Worcester".
        // -------------------------------------------------------------------
        public static string BuildAzureSsml(
            string originalScript,
            IEnumerable<TtsPronunciationRow> rows,
            ILanguageProfile profile,
            string voiceName)
        {
            if (profile == null)
                profile = LanguageRegistry.Default;

            string body = System.Security.SecurityElement.Escape(originalScript ?? "");

            var tags = new List<string>();

            foreach (var r in rows
                .Where(x => !string.IsNullOrWhiteSpace(x.Word) &&
                            !string.IsNullOrWhiteSpace(x.Hiragana))
                .OrderByDescending(x => x.Word.Length))
            {
                string word = System.Security.SecurityElement.Escape(r.Word);
                string reading = System.Security.SecurityElement.Escape(r.Hiragana);

                string tag = profile.BuildSubstitutionTag(word, reading);

                // \uE000..\uE001 are private-use chars that never occur in text.
                string token = "\uE000" + tags.Count + "\uE001";
                tags.Add(tag);

                if (profile.UsesWordBoundaryMatching)
                {
                    body = Regex.Replace(
                        body,
                        @"\b" + Regex.Escape(word) + @"\b",
                        token.Replace("$", "$$"),
                        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
                }
                else
                {
                    body = body.Replace(word, token);
                }
            }

            for (int i = 0; i < tags.Count; i++)
                body = body.Replace("\uE000" + i + "\uE001", tags[i]);

            var sb = new StringBuilder();

            sb.AppendLine("<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"" + profile.LocaleCode + "\">");
            sb.AppendLine("  <voice name=\"" + voiceName + "\">");
            sb.AppendLine("    <prosody rate=\"+0%\" pitch=\"+0%\">");
            sb.AppendLine("      " + body);
            sb.AppendLine("    </prosody>");
            sb.AppendLine("  </voice>");
            sb.AppendLine("</speak>");

            return sb.ToString();
        }

        /// <summary>Backward-compatible overload (Japanese).</summary>
        public static string BuildAzureSsml(
            string originalScript,
            IEnumerable<TtsPronunciationRow> rows,
            string voiceName)
        {
            return BuildAzureSsml(originalScript, rows, LanguageRegistry.Default, voiceName);
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
                    "Some words have an empty reading. Please fix them in the Review screen:\n\n" +
                    string.Join("\n", missing)
                );
            }
        }
    }
}
