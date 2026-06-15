using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class FixedDictionaryService
    {
        private readonly Dictionary<string, DictionaryEntry> _entries =
            new Dictionary<string, DictionaryEntry>(StringComparer.Ordinal);

        public string BackingCsvPath { get; private set; }
        public int Count => _entries.Count;

        public IReadOnlyList<DictionaryEntry> All =>
            _entries.Values.OrderBy(e => e.Word).ToList();

        // ---------- lookups ----------
        public bool Contains(string word) =>
            !string.IsNullOrEmpty(word) && _entries.ContainsKey(word);

        public bool TryGetReading(string word, out string reading)
        {
            reading = null;
            if (!string.IsNullOrEmpty(word) && _entries.TryGetValue(word, out var e))
            {
                reading = e.Hiragana;
                return true;
            }
            return false;
        }

        /// <summary>Words from the dictionary that literally appear in the script
        /// (longest first, so 上米内 isn't eaten by 米内). Offline check.</summary>
        public List<DictionaryEntry> FindMatchesInScript(string script)
        {
            var result = new List<DictionaryEntry>();
            if (string.IsNullOrEmpty(script)) return result;
            foreach (var key in _entries.Keys.OrderByDescending(k => k.Length))
                if (script.Contains(key)) result.Add(_entries[key]);
            return result;
        }

        // ---------- mutations (Screen 4 approve / Screen 5 manage) ----------
        public void AddOrUpdate(DictionaryEntry entry)
        {
            if (entry == null || string.IsNullOrWhiteSpace(entry.Word)) return;
            entry.Hiragana = NormalizeToHiragana(entry.Hiragana);
            entry.Updated = DateTime.Today;
            _entries[entry.Word] = entry;
        }

        public void Remove(string word)
        {
            if (!string.IsNullOrEmpty(word)) _entries.Remove(word);
        }

        // ---------- persistence ----------
        public void LoadCsv(string path)
        {
            _entries.Clear();
            BackingCsvPath = path;
            if (!File.Exists(path)) return;

            foreach (var line in File.ReadAllLines(path, Encoding.UTF8).Skip(1))
            {
                var c = ParseCsvLine(line);
                if (c.Count < 2 || string.IsNullOrWhiteSpace(c[0])) continue;
                var e = new DictionaryEntry
                {
                    Word = c[0],
                    Hiragana = NormalizeToHiragana(c[1]),
                    Category = c.Count > 2 ? c[2] : "General",
                    Status = c.Count > 3 ? c[3] : "Approved",
                    Updated = c.Count > 4 && DateTime.TryParse(c[4], out var d) ? d : DateTime.Today
                };
                _entries[e.Word] = e;
            }
        }

        public void SaveCsv(string path = null)
        {
            if (path == null)
            {
                path = BackingCsvPath;
            }
            if (string.IsNullOrWhiteSpace(path)) return;
            BackingCsvPath = path;

            var sb = new StringBuilder();
            sb.AppendLine("word,hiragana,category,status,updated");
            foreach (var e in All)
                sb.AppendLine(string.Join(",",
                    Csv(e.Word), Csv(e.Hiragana), Csv(e.Category), Csv(e.Status),
                    e.Updated.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)));
            File.WriteAllText(path, sb.ToString(), new UTF8Encoding(true));
        }

        /// <summary>Timestamped copy of the CSV before destructive changes.</summary>
        public string Backup(string folder)
        {
            if (string.IsNullOrWhiteSpace(BackingCsvPath) || !File.Exists(BackingCsvPath))
                return null;
            Directory.CreateDirectory(folder);
            string dst = Path.Combine(folder,
                $"dictionary_backup_{DateTime.Now:yyyyMMdd_HHmmss}.csv");
            File.Copy(BackingCsvPath, dst, true);
            return dst;
        }

        /// <summary>Export the PLS lexicon Azure TTS uses (alphabet="ipa" not needed —
        /// we use grapheme+alias kana so Azure reads the kana reading).</summary>
        public string ExportPlsXml(string path)
        {
            XNamespace ns = "http://www.w3.org/2005/01/pronunciation-lexicon";
            var lex = new XElement(ns + "lexicon",
                new XAttribute("version", "1.0"),
                new XAttribute(XNamespace.Xml + "lang", "ja-JP"),
                new XAttribute("alphabet", "x-microsoft-sapi"));

            foreach (var e in All)
                lex.Add(new XElement(ns + "lexeme",
                    new XElement(ns + "grapheme", e.Word),
                    new XElement(ns + "alias", e.Hiragana)));

            new XDocument(new XDeclaration("1.0", "UTF-8", null), lex).Save(path);
            return path;
        }

        // ---------- helpers ----------
        public string BuildPromptBlock(int max = 500)
        {
            var sb = new StringBuilder("Fixed Dictionary (word=reading), highest priority:\n");
            foreach (var e in All.Take(max)) sb.AppendLine(e.Word + "=" + e.Hiragana);
            return sb.ToString();
        }

        public static string NormalizeToHiragana(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            var sb = new StringBuilder(text.Length);
            foreach (char c in text)
                sb.Append(c >= '\u30A1' && c <= '\u30F6' ? (char)(c - 0x60) : c);
            return sb.ToString().Trim();
        }

        private static string Csv(string v) =>
            string.IsNullOrEmpty(v) ? "" : "\"" + v.Replace("\"", "\"\"") + "\"";

        private static List<string> ParseCsvLine(string line)
        {
            var fields = new List<string>();
            if (line == null) return fields;
            var sb = new StringBuilder();
            bool inQ = false;
            for (int i = 0; i < line.Length; i++)
            {
                char ch = line[i];
                if (inQ)
                {
                    if (ch == '"' && i + 1 < line.Length && line[i + 1] == '"') { sb.Append('"'); i++; }
                    else if (ch == '"') inQ = false;
                    else sb.Append(ch);
                }
                else
                {
                    if (ch == '"') inQ = true;
                    else if (ch == ',') { fields.Add(sb.ToString()); sb.Clear(); }
                    else sb.Append(ch);
                }
            }
            fields.Add(sb.ToString());
            return fields;
        }
    }
}
