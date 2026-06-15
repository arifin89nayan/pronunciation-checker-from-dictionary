using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WindowsFormsApp1.Models;
using ClosedXML.Excel;

namespace WindowsFormsApp1.Services
{
    public class FixedDictionaryService
    {
        private readonly Dictionary<string, DictionaryEntry> _entries =
            new Dictionary<string, DictionaryEntry>(StringComparer.Ordinal);

        // Kept this name so your existing UI code does not break.
        // It can now store either .csv or .xlsx path.
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

        public List<DictionaryEntry> FindMatchesInScript(string script)
        {
            var result = new List<DictionaryEntry>();

            if (string.IsNullOrEmpty(script))
                return result;

            foreach (var key in _entries.Keys.OrderByDescending(k => k.Length))
            {
                if (script.Contains(key))
                    result.Add(_entries[key]);
            }

            return result;
        }

        // ---------- mutations ----------
        public void AddOrUpdate(DictionaryEntry entry)
        {
            if (entry == null || string.IsNullOrWhiteSpace(entry.Word))
                return;

            entry.Word = entry.Word.Trim();
            entry.Hiragana = NormalizeToHiragana(entry.Hiragana);
            entry.Category = string.IsNullOrWhiteSpace(entry.Category) ? "General" : entry.Category.Trim();
            entry.Status = string.IsNullOrWhiteSpace(entry.Status) ? "Approved" : entry.Status.Trim();
            entry.Updated = DateTime.Today;

            _entries[entry.Word] = entry;
        }

        public void Remove(string word)
        {
            if (!string.IsNullOrEmpty(word))
                _entries.Remove(word);
        }

        // ---------- CSV persistence ----------
        public void LoadCsv(string path)
        {
            string ext = Path.GetExtension(path).ToLowerInvariant();

            if (ext == ".xlsx")
            {
                LoadExcel(path);
                return;
            }

            _entries.Clear();
            BackingCsvPath = path;

            if (!File.Exists(path))
                return;

            foreach (var line in File.ReadAllLines(path, Encoding.UTF8).Skip(1))
            {
                var c = ParseCsvLine(line);

                if (c.Count < 2 || string.IsNullOrWhiteSpace(c[0]))
                    continue;

                var e = new DictionaryEntry
                {
                    Word = c[0].Trim(),
                    Hiragana = NormalizeToHiragana(c[1]),
                    Category = c.Count > 2 && !string.IsNullOrWhiteSpace(c[2]) ? c[2].Trim() : "General",
                    Status = c.Count > 3 && !string.IsNullOrWhiteSpace(c[3]) ? c[3].Trim() : "Approved",
                    Updated = c.Count > 4 && DateTime.TryParse(c[4], out var d) ? d : DateTime.Today
                };

                _entries[e.Word] = e;
            }
        }

        public void SaveCsv(string path = null)
        {
            if (path == null)
                path = BackingCsvPath;

            if (string.IsNullOrWhiteSpace(path))
                return;

            string ext = Path.GetExtension(path).ToLowerInvariant();

            // Safety: HumanReviewScreen currently calls SaveCsv().
            // If the opened fixed list is Excel, save it as Excel instead of corrupting the xlsx file.
            if (ext == ".xlsx")
            {
                SaveExcel(path);
                return;
            }

            BackingCsvPath = path;

            var sb = new StringBuilder();
            sb.AppendLine("word,hiragana,category,status,updated");

            foreach (var e in All)
            {
                sb.AppendLine(string.Join(",",
                    Csv(e.Word),
                    Csv(e.Hiragana),
                    Csv(e.Category),
                    Csv(e.Status),
                    Csv(e.Updated.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))));
            }

            File.WriteAllText(path, sb.ToString(), new UTF8Encoding(true));
        }

        // ---------- Excel persistence ----------
        public void LoadExcel(string path)
        {
            _entries.Clear();
            BackingCsvPath = path;

            if (!File.Exists(path))
                return;

            using (var workbook = new XLWorkbook(path))
            {
                var ws = workbook.Worksheet(1);

                int lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;

                for (int row = 2; row <= lastRow; row++)
                {
                    string word = ws.Cell(row, 1).GetString().Trim();
                    string hiragana = ws.Cell(row, 2).GetString().Trim();
                    string category = ws.Cell(row, 3).GetString().Trim();
                    string status = ws.Cell(row, 4).GetString().Trim();

                    if (string.IsNullOrWhiteSpace(word))
                        continue;

                    var entry = new DictionaryEntry
                    {
                        Word = word,
                        Hiragana = NormalizeToHiragana(hiragana),
                        Category = string.IsNullOrWhiteSpace(category) ? "General" : category,
                        Status = string.IsNullOrWhiteSpace(status) ? "Approved" : status,
                        Updated = ReadDateCell(ws.Cell(row, 5))
                    };

                    _entries[entry.Word] = entry;
                }
            }
        }

        public void SaveExcel(string path = null)
        {
            if (path == null)
                path = BackingCsvPath;

            if (string.IsNullOrWhiteSpace(path))
                return;

            BackingCsvPath = path;

            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.Worksheets.Add("FixedList");

                ws.Cell(1, 1).Value = "word";
                ws.Cell(1, 2).Value = "hiragana";
                ws.Cell(1, 3).Value = "category";
                ws.Cell(1, 4).Value = "status";
                ws.Cell(1, 5).Value = "updated";

                int row = 2;

                foreach (var e in All)
                {
                    ws.Cell(row, 1).Value = e.Word;
                    ws.Cell(row, 2).Value = e.Hiragana;
                    ws.Cell(row, 3).Value = e.Category;
                    ws.Cell(row, 4).Value = e.Status;
                    ws.Cell(row, 5).Value = e.Updated.ToString("yyyy-MM-dd");

                    row++;
                }

                var header = ws.Range(1, 1, 1, 5);
                header.Style.Font.Bold = true;
                header.Style.Fill.BackgroundColor = XLColor.FromHtml("#1E2761");
                header.Style.Font.FontColor = XLColor.White;

                ws.Columns().AdjustToContents();

                workbook.SaveAs(path);
            }
        }

        // ---------- backup ----------
        public string Backup(string folder)
        {
            if (string.IsNullOrWhiteSpace(BackingCsvPath) || !File.Exists(BackingCsvPath))
                return null;

            Directory.CreateDirectory(folder);

            string ext = Path.GetExtension(BackingCsvPath);

            if (string.IsNullOrWhiteSpace(ext))
                ext = ".csv";

            string dst = Path.Combine(folder,
                $"dictionary_backup_{DateTime.Now:yyyyMMdd_HHmmss}{ext}");

            File.Copy(BackingCsvPath, dst, true);

            return dst;
        }

        // ---------- Azure PLS XML export ----------
        public string ExportPlsXml(string path)
        {
            XNamespace ns = "http://www.w3.org/2005/01/pronunciation-lexicon";

            var lex = new XElement(ns + "lexicon",
                new XAttribute("version", "1.0"),
                new XAttribute(XNamespace.Xml + "lang", "ja-JP"),
                new XAttribute("alphabet", "x-microsoft-sapi"));

            foreach (var e in All)
            {
                lex.Add(new XElement(ns + "lexeme",
                    new XElement(ns + "grapheme", e.Word),
                    new XElement(ns + "alias", e.Hiragana)));
            }

            new XDocument(new XDeclaration("1.0", "UTF-8", null), lex).Save(path);

            return path;
        }

        // ---------- prompt ----------
        public string BuildPromptBlock(int max = 500)
        {
            var sb = new StringBuilder("Fixed Dictionary (word=reading), highest priority:\n");

            foreach (var e in All.Take(max))
                sb.AppendLine(e.Word + "=" + e.Hiragana);

            return sb.ToString();
        }

        // ---------- helpers ----------
        public static string NormalizeToHiragana(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var sb = new StringBuilder(text.Length);

            foreach (char c in text)
            {
                sb.Append(c >= '\u30A1' && c <= '\u30F6'
                    ? (char)(c - 0x60)
                    : c);
            }

            return sb.ToString().Trim();
        }

        private static DateTime ReadDateCell(IXLCell cell)
        {
            if (cell == null || cell.IsEmpty())
                return DateTime.Today;

            DateTime d;

            try
            {
                if (cell.TryGetValue<DateTime>(out d))
                    return d.Date;
            }
            catch
            {
                // ignore and try string parsing
            }

            string text = cell.GetString();

            if (DateTime.TryParse(text, out d))
                return d.Date;

            double oa;

            if (double.TryParse(text, out oa))
            {
                try
                {
                    return DateTime.FromOADate(oa).Date;
                }
                catch
                {
                    return DateTime.Today;
                }
            }

            return DateTime.Today;
        }

        private static string Csv(string v)
        {
            return string.IsNullOrEmpty(v)
                ? ""
                : "\"" + v.Replace("\"", "\"\"") + "\"";
        }

        private static List<string> ParseCsvLine(string line)
        {
            var fields = new List<string>();

            if (line == null)
                return fields;

            var sb = new StringBuilder();
            bool inQ = false;

            for (int i = 0; i < line.Length; i++)
            {
                char ch = line[i];

                if (inQ)
                {
                    if (ch == '"' && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        sb.Append('"');
                        i++;
                    }
                    else if (ch == '"')
                    {
                        inQ = false;
                    }
                    else
                    {
                        sb.Append(ch);
                    }
                }
                else
                {
                    if (ch == '"')
                    {
                        inQ = true;
                    }
                    else if (ch == ',')
                    {
                        fields.Add(sb.ToString());
                        sb.Clear();
                    }
                    else
                    {
                        sb.Append(ch);
                    }
                }
            }

            fields.Add(sb.ToString());

            return fields;
        }
    }
}