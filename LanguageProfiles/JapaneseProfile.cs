namespace WindowsFormsApp1.UIDesign
{
    /// <summary>
    /// Japanese (ja-JP). This profile reproduces the ORIGINAL behavior of the
    /// app exactly: NFKC normalization, katakana->hiragana readings,
    /// character-based longest match, "contains kanji" LLM trigger,
    /// and &lt;sub alias&gt; substitution.
    /// </summary>
    public sealed class JapaneseProfile : ILanguageProfile
    {
        public string Key { get { return "ja"; } }
        public string DisplayName { get { return "Japanese (ja-JP)"; } }
        public string LocaleCode { get { return "ja-JP"; } }

        public string[] Voices
        {
            get
            {
                return new[]
                {
                    "ja-JP-NanamiNeural",
                    "ja-JP-KeitaNeural",
                    "ja-JP-AoiNeural",
                    "ja-JP-DaichiNeural",
                    "ja-JP-MayuNeural",
                    "ja-JP-NaokiNeural",
                    "ja-JP-ShioriNeural"
                };
            }
        }

        public string DefaultVoice { get { return "ja-JP-NanamiNeural"; } }

        // Japanese has no spaces -> character-based longest match.
        public bool UsesWordBoundaryMatching { get { return false; } }

        public string ReadingColumnHeader { get { return "Correct Hiragana"; } }

        public string DefaultFixedListFileName { get { return "fixed_list_ja.xlsx"; } }

        public string NormalizeText(string text)
        {
            return JapaneseTextNormalizer.NormalizeText(text);
        }

        public string NormalizeReading(string reading)
        {
            return JapaneseTextNormalizer.ToHiragana(reading);
        }

        public bool ContainsUnresolvedTokens(string maskedText)
        {
            return JapaneseTextNormalizer.ContainsKanjiExceptFixedMarker(maskedText);
        }

        public string BuildSubstitutionTag(string escapedWord, string escapedReading)
        {
            return "<sub alias=\"" + escapedReading + "\">" + escapedWord + "</sub>";
        }

        public string BuildLlmPrompt(string maskedText)
        {
            return
@"You are a Japanese TTS pronunciation extraction agent.

The marker 【FIXED】 means this word is already handled by the fixed dictionary.
Ignore every 【FIXED】 marker completely.
Never output 【FIXED】.
Never guess the hidden fixed words.
Extract EVERY kanji word and important term — do not skip any, even if it
seems common. It is better to over-extract than to miss one.

From the remaining visible text, extract ONLY kanji words and important Japanese terms.

Target terms:
- place names
- shrine names
- temple names
- museum names
- person names
- organization names
- historical terms
- cultural terms
- technical terms
- rare kanji words
- difficult pronunciation words

Prefer full words and phrases over single kanji characters.

Return ONLY a JSON array.
No prose.
No markdown fences.

Format:
[
  {
    ""word"": ""笄"",
    ""hiragana"": ""こうがい"",
    ""difficulty"": ""high"",
    ""reason"": ""Rare kanji; reading is not predictable.""
  }
]

difficulty:
low = common stable word
medium = compound/technical/cultural/historical word
high = place/shrine/person name, rare kanji, local term, uncertain reading

Input Text:
" + maskedText;
        }

        public override string ToString()
        {
            return DisplayName; // shown in the ComboBox
        }
    }
}
