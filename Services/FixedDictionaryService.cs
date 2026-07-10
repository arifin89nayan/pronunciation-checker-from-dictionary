using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1.UIDesign
{
    /// <summary>
    /// Holds the Fixed Dictionary in memory and masks fixed words before
    /// sending text to the LLM.
    ///
    /// Language behavior is driven by the ILanguageProfile:
    /// - Japanese / Chinese (no spaces): character-based longest-match scan.
    /// - English / German / Korean:      word-boundary regex, case-insensitive.
    ///
    /// Invariants (unchanged from the Japanese-only version):
    /// - Fixed words are found locally.
    /// - Fixed words are replaced with 【FIXED】.
    /// - The LLM never sees fixed-list words.
    /// - Matching is longest-match-first and non-overlapping.
    /// </summary>
    public sealed class FixedDictionaryService
    {
        public const string Placeholder = "\u3010FIXED\u3011"; // 【FIXED】

        // Private-use-area scan token. Used ONLY during word-boundary masking
        // so that a dictionary entry can never accidentally match inside the
        // human-readable placeholder text ("FIXED" is a valid English word!).
        private const string ScanTokenStart = "\uE000";
        private const string ScanTokenEnd = "\uE001";

        private readonly ILanguageProfile _profile;
        private readonly Dictionary<string, Inputtext.FixedWord> _byWord;
        private readonly List<string> _keysByLengthDesc;
        private readonly int _maxWordLen;

        public int Count
        {
            get { return _byWord.Count; }
        }

        public ILanguageProfile Profile
        {
            get { return _profile; }
        }

        /// <summary>Backward-compatible constructor: defaults to Japanese.</summary>
        public FixedDictionaryService(IEnumerable<Inputtext.FixedWord> words)
            : this(words, LanguageRegistry.Default)
        {
        }

        public FixedDictionaryService(IEnumerable<Inputtext.FixedWord> words,
                                      ILanguageProfile profile)
        {
            _profile = profile ?? LanguageRegistry.Default;

            // Case-insensitive keys for word-boundary languages (English etc.).
            StringComparer comparer = _profile.UsesWordBoundaryMatching
                ? StringComparer.OrdinalIgnoreCase
                : StringComparer.Ordinal;

            _byWord = new Dictionary<string, Inputtext.FixedWord>(comparer);

            int max = 1;

            foreach (var w in words)
            {
                if (w == null || string.IsNullOrWhiteSpace(w.Word))
                    continue;

                string key = _profile.NormalizeText(w.Word);

                if (key.Length == 0)
                    continue;

                _byWord[key] = w;

                if (key.Length > max)
                    max = key.Length;
            }

            _maxWordLen = max;

            // Longest-first list for word-boundary masking so that
            // "New York Times" wins over "New York".
            _keysByLengthDesc = _byWord.Keys
                .OrderByDescending(k => k.Length)
                .ToList();
        }

        public bool TryGet(string word, out Inputtext.FixedWord fw)
        {
            string key = _profile.NormalizeText(word);
            return _byWord.TryGetValue(key, out fw);
        }

        /// <summary>
        /// Replace fixed dictionary words with 【FIXED】.
        ///
        /// Japanese example:
        ///   盛岡の大國神社では、笄を展示しています。
        ///   -> 【FIXED】の【FIXED】では、笄を展示しています。
        ///
        /// English example (word-boundary, case-insensitive):
        ///   "I read the New York Times in Worcester."
        ///   with dictionary entries {"New York Times", "Worcester"}
        ///   -> "I read the 【FIXED】 in 【FIXED】."
        ///   (and "read" can never match inside "bread")
        /// </summary>
        public string MaskFixedWords(string rawText, out List<Inputtext.FixedWord> found)
        {
            found = new List<Inputtext.FixedWord>();

            if (string.IsNullOrWhiteSpace(rawText))
                return "";

            string text = _profile.NormalizeText(rawText);

            return _profile.UsesWordBoundaryMatching
                ? MaskByWordBoundary(text, found)
                : MaskByCharacterScan(text, found);
        }

        // -------------------------------------------------------------
        // Strategy 1: character-based longest match (Japanese / Chinese)
        // -------------------------------------------------------------
        private string MaskByCharacterScan(string text, List<Inputtext.FixedWord> found)
        {
            var emitted = new HashSet<string>(StringComparer.Ordinal);
            var sb = new StringBuilder(text.Length);

            int pos = 0;
            int n = text.Length;

            while (pos < n)
            {
                int maxLen = Math.Min(_maxWordLen, n - pos);
                bool matched = false;

                for (int len = maxLen; len >= 1; len--)
                {
                    string sub = text.Substring(pos, len);

                    Inputtext.FixedWord fw;

                    if (_byWord.TryGetValue(sub, out fw))
                    {
                        sb.Append(Placeholder);

                        if (emitted.Add(sub))
                            found.Add(fw);

                        pos += len;
                        matched = true;
                        break;
                    }
                }

                if (!matched)
                {
                    sb.Append(text[pos]);
                    pos++;
                }
            }

            return sb.ToString();
        }

        // -------------------------------------------------------------
        // Strategy 2: word-boundary regex (English / German / Korean)
        // -------------------------------------------------------------
        private string MaskByWordBoundary(string text, List<Inputtext.FixedWord> found)
        {
            var emitted = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string masked = text;

            // Longest entries first so multi-word phrases win.
            foreach (string key in _keysByLengthDesc)
            {
                // \b works for standard alphanumeric words. Entries that start
                // or end with symbols (e.g. "C++") would need custom handling.
                string pattern = @"\b" + Regex.Escape(key) + @"\b";

                Inputtext.FixedWord fw = _byWord[key];
                bool hitThisKey = false;

                masked = Regex.Replace(
                    masked,
                    pattern,
                    delegate (Match m)
                    {
                        hitThisKey = true;
                        // Emit an unmatchable scan token, NOT the placeholder,
                        // so later dictionary keys cannot match inside it.
                        return ScanTokenStart + "FX" + ScanTokenEnd;
                    },
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

                if (hitThisKey && emitted.Add(key))
                    found.Add(fw);
            }

            // Convert scan tokens to the human-readable placeholder at the end.
            return masked.Replace(ScanTokenStart + "FX" + ScanTokenEnd, Placeholder);
        }

        public List<Inputtext.FixedWord> FindIn(string rawText)
        {
            List<Inputtext.FixedWord> found;
            MaskFixedWords(rawText, out found);
            return found;
        }
    }
}
