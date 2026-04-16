using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;
using System.Xml.Linq;

public class PhonemeLexiconHelper
{
    private readonly Dictionary<string, string> _lexicon = new Dictionary<string, string>();

    // Load lexicon at creation
    public PhonemeLexiconHelper(string plsXmlPath)
    {
        if (File.Exists(plsXmlPath))
        {
            LoadPlsLexicon(plsXmlPath);
        }
        else
        {
            throw new FileNotFoundException($"Lexicon file not found: {plsXmlPath}");
        }
    }

    private void LoadPlsLexicon(string xmlPath)
    {
        XDocument xdoc = XDocument.Load(xmlPath);
        XNamespace ns = "http://www.w3.org/2005/01/pronunciation-lexicon";
        foreach (var lex in xdoc.Descendants(ns + "lexeme"))
        {
            var grapheme = lex.Element(ns + "grapheme")?.Value?.Trim();
            var phoneme = lex.Element(ns + "phoneme")?.Value?.Trim();
            var alias = lex.Element(ns + "alias")?.Value?.Trim();

            if (!string.IsNullOrEmpty(grapheme))
            {
                if (!string.IsNullOrEmpty(phoneme))
                    _lexicon[grapheme] = phoneme;
                else if (!string.IsNullOrEmpty(alias))
                    _lexicon[grapheme] = alias;
            }
        }
    }

    // Replace target words with phoneme tags (regex for word boundary safety)
    public string InjectPhonemes(string input)
    {
        //foreach (var kvp in _lexicon)
        //{

        //    input = input.Replace(kvp.Key, $"<phoneme alphabet='sapi' ph='{kvp.Value}'>{kvp.Key}</phoneme>");
        //}
        //return input;
        if (_lexicon.Count == 0 || string.IsNullOrEmpty(input)) return input;

        // Longest first prevents 上米内 being corrupted by 米内 replacement
        var keys = _lexicon.Keys
            .OrderByDescending(k => k.Length)
            .Select(Regex.Escape);

        var pattern = string.Join("|", keys);

        return Regex.Replace(input, pattern, m =>
        {
            var value = _lexicon[m.Value]; // e.g., カミヨナイ or actual phoneme
            var safeValue = SecurityElement.Escape(value);

            // If the value is Kana, treat it as reading (alias), not a SAPI phoneme
            if (IsKana(value))
                return $"<sub alias=\"{safeValue}\">{m.Value}</sub>";

            // Otherwise assume it's a real phoneme string (SAPI/IPA etc.)
            return $"<phoneme alphabet='sapi' ph='{safeValue}'>{m.Value}</phoneme>";
        });
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

    // Generate SSML string
    public static string BuildSsml(string textWithPhonemes, string language , string voice )
            {
                return $@"
        <speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='{language}'>
          <voice name='{voice}'>
            {textWithPhonemes}
          </voice>
        </speak>";
    }
}
