using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1.UIDesign
{
    /// <summary>
    /// Holds the Fixed Dictionary in memory and masks fixed words before
    /// sending text to ChatGPT.
    ///
    /// Important:
    /// - Fixed words are found locally.
    /// - Fixed words are replaced with 【FIXED】.
    /// - ChatGPT never sees fixed-list words.
    /// - Matching is longest-match-first and non-overlapping.
    /// </summary>
    public sealed class FixedDictionaryService
    {
        public const string Placeholder = "\u3010FIXED\u3011"; // 【FIXED】

        private readonly Dictionary<string, Inputtext.FixedWord> _byWord;
        private readonly int _maxWordLen;

        public int Count
        {
            get { return _byWord.Count; }
        }

        public FixedDictionaryService(IEnumerable<Inputtext.FixedWord> words)
        {
            _byWord = new Dictionary<string, Inputtext.FixedWord>(StringComparer.Ordinal);

            int max = 1;

            foreach (var w in words)
            {
                if (w == null || string.IsNullOrWhiteSpace(w.Word))
                    continue;

                string key = JapaneseTextNormalizer.NormalizeText(w.Word);

                if (key.Length == 0)
                    continue;

                _byWord[key] = w;

                if (key.Length > max)
                    max = key.Length;
            }

            _maxWordLen = max;
        }

        public bool TryGet(string word, out Inputtext.FixedWord fw)
        {
            string key = JapaneseTextNormalizer.NormalizeText(word);
            return _byWord.TryGetValue(key, out fw);
        }

        /// <summary>
        /// Replace fixed dictionary words with 【FIXED】.
        /// Example:
        /// 盛岡の大國神社では、笄を展示しています。
        /// becomes:
        /// 【FIXED】の【FIXED】では、笄を展示しています。
        /// </summary>
        public string MaskFixedWords(string rawText, out List<Inputtext.FixedWord> found)
        {
            found = new List<Inputtext.FixedWord>();

            if (string.IsNullOrWhiteSpace(rawText))
                return "";

            string text = JapaneseTextNormalizer.NormalizeText(rawText);

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

        public List<Inputtext.FixedWord> FindIn(string rawText)
        {
            List<Inputtext.FixedWord> found;
            MaskFixedWords(rawText, out found);
            return found;
        }
    }
}