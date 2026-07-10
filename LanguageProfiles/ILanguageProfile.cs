using System.Collections.Generic;

namespace WindowsFormsApp1.UIDesign
{
    /// <summary>
    /// A language profile encapsulates EVERYTHING that is language-specific
    /// in the TTS pipeline. The pipeline itself (mask -> LLM -> conflict ->
    /// human review -> SSML -> audio) never needs to know which language
    /// it is running. To add a new language, implement this interface and
    /// register it in LanguageRegistry. Nothing else changes.
    /// </summary>
    public interface ILanguageProfile
    {
        /// <summary>Short stable key, e.g. "ja", "en", "zh-TW". Used for caching and file names.</summary>
        string Key { get; }

        /// <summary>Name shown in the UI dropdown, e.g. "Japanese (ja-JP)".</summary>
        string DisplayName { get; }

        /// <summary>BCP-47 locale used in SSML xml:lang, e.g. "ja-JP".</summary>
        string LocaleCode { get; }

        /// <summary>Azure neural voices for this language.</summary>
        string[] Voices { get; }

        /// <summary>Voice used when the user has not picked one yet.</summary>
        string DefaultVoice { get; }

        /// <summary>
        /// false = character-based longest match (Japanese, Chinese - no spaces).
        /// true  = word-boundary match with case-insensitive compare (English, German, Korean).
        /// Controls BOTH dictionary masking and SSML substitution.
        /// </summary>
        bool UsesWordBoundaryMatching { get; }

        /// <summary>Header text for the reading column in the review grid, e.g. "Correct Hiragana" / "Correct Pronunciation".</summary>
        string ReadingColumnHeader { get; }

        /// <summary>Suggested dictionary file name, e.g. "fixed_list_ja.xlsx". One Excel per language.</summary>
        string DefaultFixedListFileName { get; }

        /// <summary>Unicode/whitespace normalization applied to all surface text.</summary>
        string NormalizeText(string text);

        /// <summary>
        /// Normalization applied to READINGS.
        /// Japanese: katakana -> hiragana. English: trim (respelling or IPA kept as-is).
        /// Chinese: pinyin normalization. Etc.
        /// </summary>
        string NormalizeReading(string reading);

        /// <summary>
        /// After fixed-dictionary masking, decide whether the remaining text
        /// still contains tokens whose pronunciation may be ambiguous
        /// (i.e. whether calling the LLM is necessary at all).
        /// Japanese: "contains kanji". English: "contains any words" (conservative).
        /// </summary>
        bool ContainsUnresolvedTokens(string maskedText);

        /// <summary>Builds the full LLM prompt for this language. Must instruct the
        /// model to return the SAME JSON shape: [{word, hiragana, difficulty, reason}]
        /// where "hiragana" carries the reading in whatever notation this language uses.</summary>
        string BuildLlmPrompt(string maskedText);

        /// <summary>
        /// Builds the SSML fragment that forces the pronunciation of one word.
        /// Inputs are ALREADY XML-escaped.
        /// Japanese: &lt;sub alias="hiragana"&gt;word&lt;/sub&gt;
        /// English : &lt;phoneme alphabet="ipa" ph="..."&gt;word&lt;/phoneme&gt; or &lt;sub&gt; respelling.
        /// </summary>
        string BuildSubstitutionTag(string escapedWord, string escapedReading);
    }
}
