using System.Text;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1.UIDesign
{
    /// <summary>
    /// Taiwanese Mandarin (zh-TW) — SKELETON / STARTING POINT.
    ///
    /// Scientifically this is the best second language for the paper:
    /// Chinese has the same heteronym problem as Japanese (破音字,
    /// e.g. 行 = xíng / háng), so the whole architecture transfers directly.
    ///
    /// Like Japanese: no spaces -> character-based longest match, and
    /// "contains CJK" is the LLM trigger.
    ///
    /// Reading notation: hanyu pinyin WITH tone numbers (e.g. "xing2").
    /// Substitution uses &lt;phoneme alphabet="sapi"&gt;.
    /// IMPORTANT: verify sapi/pinyin support against the specific zh-TW
    /// voice you use — Microsoft documents sapi pinyin primarily for zh-CN.
    /// If a zh-TW voice ignores it, either switch the profile to zh-CN
    /// voices or fall back to &lt;sub alias&gt; with unambiguous homophone
    /// characters. Test with one sentence before building the dictionary.
    /// </summary>
    public sealed class ChineseTaiwanProfile : ILanguageProfile
    {
        public string Key { get { return "zh-TW"; } }
        public string DisplayName { get { return "Chinese (zh-TW)"; } }
        public string LocaleCode { get { return "zh-TW"; } }

        public string[] Voices
        {
            get
            {
                return new[]
                {
                    "zh-TW-HsiaoChenNeural",
                    "zh-TW-YunJheNeural",
                    "zh-TW-HsiaoYuNeural"
                };
            }
        }

        public string DefaultVoice { get { return "zh-TW-HsiaoChenNeural"; } }

        // No spaces -> character-based longest match, same as Japanese.
        public bool UsesWordBoundaryMatching { get { return false; } }

        public string ReadingColumnHeader { get { return "Correct Pinyin (with tone number)"; } }

        public string DefaultFixedListFileName { get { return "fixed_list_zh-TW.xlsx"; } }

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

            // Pinyin is stored lower-case with tone numbers: "tai2 bei3".
            return reading.Normalize(NormalizationForm.FormKC).Trim().ToLowerInvariant();
        }

        public bool ContainsUnresolvedTokens(string maskedText)
        {
            if (string.IsNullOrWhiteSpace(maskedText))
                return false;

            string cleaned = maskedText.Replace(FixedDictionaryService.Placeholder, "");

            foreach (char c in cleaned)
            {
                if ((c >= '\u4E00' && c <= '\u9FFF') ||
                    (c >= '\u3400' && c <= '\u4DBF'))
                {
                    return true;
                }
            }

            return false;
        }

        public string BuildSubstitutionTag(string escapedWord, string escapedReading)
        {
            // sapi pinyin with tone numbers. VERIFY against your zh-TW voice.
            return "<phoneme alphabet=\"sapi\" ph=\"" + escapedReading + "\">" +
                   escapedWord + "</phoneme>";
        }

        public string BuildLlmPrompt(string maskedText)
        {
            return
@"You are a Mandarin Chinese TTS pronunciation extraction agent for Taiwan Mandarin (zh-TW).

The marker 【FIXED】 means this word is already handled by the fixed dictionary.
Ignore every 【FIXED】 marker completely.
Never output 【FIXED】.
Never guess the hidden fixed words.

From the remaining visible text, extract Chinese words whose pronunciation is
risky for TTS:

Target terms:
- person names
- place names (especially Taiwanese place names with irregular readings)
- 破音字 heteronyms where context decides the reading (行, 長, 重, 樂 ...)
- organization / brand names
- classical, literary, or rare characters
- technical and cultural terms

Prefer full words and phrases over single characters, EXCEPT for heteronym
characters where the single character is the risk.

Provide the reading as hanyu pinyin with tone NUMBERS, syllables separated
by spaces. Example: 臺北 -> ""tai2 bei3"".

Return ONLY a JSON array.
No prose.
No markdown fences.

Format:
[
  {
    ""word"": ""行天宮"",
    ""hiragana"": ""xing2 tian1 gong1"",
    ""difficulty"": ""high"",
    ""reason"": ""Temple name; 行 is a heteronym.""
  }
]

NOTE: the field name is ""hiragana"" for system compatibility — put the
pinyin there.

difficulty:
low = common stable word
medium = compound/technical/cultural word
high = proper noun, heteronym, rare character

Input Text:
" + maskedText;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
