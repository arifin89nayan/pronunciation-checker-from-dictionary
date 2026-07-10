using System.Text;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1.UIDesign
{
    /// <summary>
    /// English (en-US).
    ///
    /// Key differences vs Japanese:
    /// 1. Word-boundary, case-insensitive matching (so "read" never matches
    ///    inside "bread").
    /// 2. The reading field carries EITHER an IPA transcription between
    ///    slashes (e.g. "/ˈnaɪki/") OR a plain-English respelling
    ///    (e.g. "NY-kee"). IPA readings are emitted as
    ///    &lt;phoneme alphabet="ipa"&gt;; respellings as &lt;sub alias&gt;.
    /// 3. The LLM trigger is conservative: if ANY Latin word survives
    ///    masking, we ask the model. English has no "kanji" signal that
    ///    tells us a word is ambiguous, so over-asking is the safe default.
    ///    (You can tighten this later with the heuristic in the comment.)
    /// </summary>
    public sealed class EnglishProfile : ILanguageProfile
    {
        public string Key { get { return "en"; } }
        public string DisplayName { get { return "English (en-US)"; } }
        public string LocaleCode { get { return "en-US"; } }

        public string[] Voices
        {
            get
            {
                return new[]
                {
                    "en-US-JennyNeural",
                    "en-US-GuyNeural",
                    "en-US-AriaNeural",
                    "en-US-DavisNeural",
                    "en-US-JaneNeural",
                    "en-US-JasonNeural",
                    "en-US-SaraNeural"
                };
            }
        }

        public string DefaultVoice { get { return "en-US-JennyNeural"; } }

        public bool UsesWordBoundaryMatching { get { return true; } }

        public string ReadingColumnHeader { get { return "Correct Pronunciation (IPA or respelling)"; } }

        public string DefaultFixedListFileName { get { return "fixed_list_en.xlsx"; } }

        public string NormalizeText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            return text.Normalize(NormalizationForm.FormKC).Trim();
        }

        public string NormalizeReading(string reading)
        {
            if (string.IsNullOrWhiteSpace(reading))
                return "";

            // IPA and respellings are kept verbatim; just normalize whitespace.
            return reading.Normalize(NormalizationForm.FormC).Trim();
        }

        private static readonly Regex AnyLatinWord =
            new Regex(@"[A-Za-zÀ-ÖØ-öø-ÿ]{2,}", RegexOptions.Compiled);

        public bool ContainsUnresolvedTokens(string maskedText)
        {
            if (string.IsNullOrWhiteSpace(maskedText))
                return false;

            string cleaned = maskedText.Replace(FixedDictionaryService.Placeholder, "");

            // Conservative: any remaining word -> ask the LLM.
            // The prompt tells the model to return ONLY risky tokens, so
            // the review screen stays small even though we always ask.
            //
            // Tighter heuristic (optional, once you trust it):
            //   only return true for ALL-CAPS acronyms, CamelCase tokens,
            //   words with digits or diacritics, or capitalized words that
            //   are not at sentence start.
            return AnyLatinWord.IsMatch(cleaned);
        }

        // Detects whether the reading is IPA (delimited by slashes or
        // containing characteristic IPA symbols).
        private static readonly Regex IpaChars =
            new Regex("[ˈˌːəɪʊɛɔæŋθðʃʒɑɒɜɡʔ]", RegexOptions.Compiled);

        public string BuildSubstitutionTag(string escapedWord, string escapedReading)
        {
            string r = escapedReading.Trim();

            bool looksLikeIpa =
                (r.StartsWith("/") && r.EndsWith("/") && r.Length > 2) ||
                IpaChars.IsMatch(r);

            if (looksLikeIpa)
            {
                string ph = r.Trim('/');
                return "<phoneme alphabet=\"ipa\" ph=\"" + ph + "\">" + escapedWord + "</phoneme>";
            }

            // Respelling fallback: Azure reads the alias text instead of the word.
            return "<sub alias=\"" + escapedReading + "\">" + escapedWord + "</sub>";
        }

        public string BuildLlmPrompt(string maskedText)
        {
            return
@"You are an English TTS pronunciation risk-detection agent.

The marker 【FIXED】 means this word is already handled by the fixed dictionary.
Ignore every 【FIXED】 marker completely.
Never output 【FIXED】.
Never guess the hidden fixed words.

From the remaining visible text, extract ONLY tokens whose pronunciation a
generic English TTS voice is likely to get WRONG or is uncertain about:

Target terms:
- person names (especially non-English origin: Nguyen, Saoirse, Xiomara)
- place names (Worcester, Yosemite, foreign cities)
- brand / product / company names
- acronyms and initialisms (state whether spelled out letter by letter or read as a word)
- loanwords and foreign phrases
- technical / scientific / medical terms
- heteronyms whose intended reading is clear from context (e.g. ""lead"", ""bass"", ""tear"")

Do NOT extract ordinary English words that any TTS engine reads correctly.

For each extracted token provide the pronunciation in ONE of these two forms:
1. IPA between slashes, e.g. ""/ˈwʊstər/""  (PREFERRED)
2. A plain-English respelling using common syllables, e.g. ""WUUS-ter""

Return ONLY a JSON array.
No prose.
No markdown fences.

Format:
[
  {
    ""word"": ""Worcester"",
    ""hiragana"": ""/ˈwʊstər/"",
    ""difficulty"": ""high"",
    ""reason"": ""Place name; spelling does not match pronunciation.""
  }
]

NOTE: the field name is ""hiragana"" for system compatibility — put the IPA
or respelling there.

difficulty:
low = mild risk, most engines get it right
medium = technical term, acronym, common foreign name
high = irregular proper noun, rare loanword, ambiguous heteronym

Input Text:
" + maskedText;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
