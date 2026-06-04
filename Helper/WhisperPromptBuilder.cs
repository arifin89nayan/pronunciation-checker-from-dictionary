using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace WindowsFormsApp1.Services
{
    public static class WhisperPromptBuilder
    {
        public static string BuildFromDictionaryXml(string dictionaryXmlPath)
        {
            if (!File.Exists(dictionaryXmlPath))
                return "";

            var words = new List<string>();

            XDocument doc = XDocument.Load(dictionaryXmlPath);

            foreach (var lexeme in doc.Descendants()
                                      .Where(x => x.Name.LocalName == "lexeme"))
            {
                string grapheme = lexeme.Elements()
                    .FirstOrDefault(x => x.Name.LocalName == "grapheme")
                    ?.Value
                    ?.Trim();

                if (!string.IsNullOrWhiteSpace(grapheme))
                    words.Add(grapheme);
            }

            return string.Join("、", words
                .Distinct()
                .OrderByDescending(x => x.Length)
                .Take(100));
        }
    }
}