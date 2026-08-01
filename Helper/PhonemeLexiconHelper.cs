using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;
using System.Xml.Linq;

public class PhonemeLexiconHelper
{
    private class LexEntry
    {
        public string Value;
        public bool IsAlias;   // true = <alias> (say other words), false = <phoneme> (exact pronunciation)
    }

    private readonly Dictionary<string, LexEntry> _lexicon =
        new Dictionary<string, LexEntry>(StringComparer.OrdinalIgnoreCase);
    private readonly string _alphabet;          // "sapi", "ipa", ... read from the lexicon file
    private readonly bool _useWordBoundaries;   // true for English/French/etc., false for Japanese

    // languageCode is optional so old calls still compile: new PhonemeLexiconHelper(path)
    // But you SHOULD pass it: new PhonemeLexiconHelper(path, "en-US")
    public PhonemeLexiconHelper(string plsXmlPath, string languageCode = null)
    {
        if (!File.Exists(plsXmlPath))
            throw new FileNotFoundException($"Lexicon file not found: {plsXmlPath}");

        string alphabetFromFile = LoadPlsLexicon(plsXmlPath);
        _alphabet = string.IsNullOrEmpty(alphabetFromFile) ? "sapi" : alphabetFromFile;

        // Languages written WITH spaces (English, French, ...) need whole-word matching.
        // Japanese / Chinese / Thai have no spaces, so no word boundaries (old behavior).
        _useWordBoundaries =
            !string.IsNullOrEmpty(languageCode) &&
            !languageCode.StartsWith("ja", StringComparison.OrdinalIgnoreCase) &&
            !languageCode.StartsWith("zh", StringComparison.OrdinalIgnoreCase) &&
            !languageCode.StartsWith("th", StringComparison.OrdinalIgnoreCase);
    }

    // Returns the alphabet declared on the <lexicon> root (e.g. alphabet="ipa"), or null if missing.
    private string LoadPlsLexicon(string xmlPath)
    {
        XDocument xdoc = XDocument.Load(xmlPath);
        XNamespace ns = "http://www.w3.org/2005/01/pronunciation-lexicon";

        string alphabet = xdoc.Root?.Attribute("alphabet")?.Value?.Trim();

        foreach (var lex in xdoc.Descendants(ns + "lexeme"))
        {
            var grapheme = lex.Element(ns + "grapheme")?.Value?.Trim();
            var phoneme = lex.Element(ns + "phoneme")?.Value?.Trim();
            var alias = lex.Element(ns + "alias")?.Value?.Trim();

            if (string.IsNullOrEmpty(grapheme)) continue;

            // IMPORTANT: keep phoneme and alias separate (do not mix them into one value)
            if (!string.IsNullOrEmpty(phoneme))
                _lexicon[grapheme] = new LexEntry { Value = phoneme, IsAlias = false };
            else if (!string.IsNullOrEmpty(alias))
                _lexicon[grapheme] = new LexEntry { Value = alias, IsAlias = true };
        }

        return alphabet;
    }

    public string InjectPhonemes(string input)
    {
        if (_lexicon.Count == 0 || string.IsNullOrEmpty(input)) return input;

        // Longest first prevents 上米内 being corrupted by 米内 replacement
        var keys = _lexicon.Keys
            .OrderByDescending(k => k.Length)
            .Select(Regex.Escape);

        string pattern = string.Join("|", keys);

        // English: only match whole words, so "art" is NOT replaced inside "start".
        // Lookarounds are safer than \b for entries like "Dr." that end with punctuation.
        if (_useWordBoundaries)
            pattern = $@"(?<!\w)(?:{pattern})(?!\w)";

        return Regex.Replace(input, pattern, m =>
        {
            var entry = _lexicon[m.Value];
            var safeValue = SecurityElement.Escape(entry.Value);

            // <alias> entries, or Kana readings (keeps old Japanese behavior) -> <sub>
            if (entry.IsAlias || IsKana(entry.Value))
                return $"<sub alias=\"{safeValue}\">{m.Value}</sub>";

            // Real phoneme string -> use the alphabet declared in the dictionary file
            return $"<phoneme alphabet='{_alphabet}' ph='{safeValue}'>{m.Value}</phoneme>";
        }, RegexOptions.IgnoreCase);
    }
    public string InjectPhonemesJapanese(string input)
    {
        if (_lexicon == null ||
            _lexicon.Count == 0 ||
            string.IsNullOrWhiteSpace(input))
        {
            return input;
        }

        // Normalize Japanese text so visually identical characters match.
        input = NormalizeJapaneseText(input);

        // Longest word first:
        // 上米内 must be matched before 米内.
        var escapedKeys = _lexicon.Keys
            .Where(key => !string.IsNullOrWhiteSpace(key))
            .Select(NormalizeJapaneseText)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderByDescending(key => key.Length)
            .Select(Regex.Escape);

        string pattern = string.Join("|", escapedKeys);

        if (string.IsNullOrWhiteSpace(pattern))
            return input;

        return Regex.Replace(
            input,
            pattern,
            match =>
            {
                string matchedWord =
                    NormalizeJapaneseText(match.Value);

                LexEntry entry;

                if (!_lexicon.TryGetValue(matchedWord, out entry))
                {
                    return SecurityElement.Escape(match.Value);
                }

                string safeWord =
                    SecurityElement.Escape(match.Value);

                string safePronunciation =
                    SecurityElement.Escape(entry.Value);

                // Japanese Kana reading:
                // 上米内 -> かみよない
                if (entry.IsAlias || IsKana(entry.Value))
                {
                    return
                        $"<sub alias=\"{safePronunciation}\">" +
                        $"{safeWord}</sub>";
                }

                // Use this only when the dictionary value is a real
                // SAPI or IPA phoneme string.
                string safeAlphabet =
                    SecurityElement.Escape(_alphabet);

                return
                    $"<phoneme alphabet=\"{safeAlphabet}\" " +
                    $"ph=\"{safePronunciation}\">" +
                    $"{safeWord}</phoneme>";
            },
            RegexOptions.CultureInvariant);
    }
    private static string NormalizeJapaneseText(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        return value
            .Normalize(System.Text.NormalizationForm.FormKC)
            .Replace("\u3000", " ")  // Full-width space
            .Replace("\u200B", "")   // Zero-width space
            .Replace("\uFEFF", "")   // BOM/zero-width no-break space
            .Trim();
    }

    private static bool IsKana(string s)
    {
        foreach (var ch in s)
        {
            if ((ch >= '\u3040' && ch <= '\u309F') ||  // Hiragana
                (ch >= '\u30A0' && ch <= '\u30FF') ||  // Katakana
                (ch >= '\u31F0' && ch <= '\u31FF'))    // Katakana Phonetic Extensions
                return true;
        }
        return false;
    }

    public static string BuildSsml(string textWithPhonemes, string language, string voice)
    {
        return $@"
<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='{language}'>
  <voice name='{voice}'>
    {textWithPhonemes}
  </voice>
</speak>";
    }
}