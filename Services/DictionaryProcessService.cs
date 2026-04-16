using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class DictionaryProcessService
    {
        private readonly Action<string> _log;

        public DictionaryProcessService(Action<string> logger)
        {
            _log = logger;
        }

        public DictionaryProcessResult ProcessDictionaryFile(string inputFilePath, string outputFolderPath)
        {
            var result = new DictionaryProcessResult();

            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputFilePath);
            string cleanedExcelPath = Path.Combine(outputFolderPath, fileNameWithoutExt + "Modified.xlsx");
            string xmlFilePath = Path.Combine(outputFolderPath, fileNameWithoutExt + "Modified.xml");
            string logFilePath = Path.Combine(outputFolderPath, fileNameWithoutExt + ".log");

            _log("READ Excel...");
            var rows = ReadDictionaryRows(inputFilePath);
            result.TotalRows = rows.Count;
            _log($"Total rows loaded: {rows.Count}");
            _log("");

            _log("VALIDATE...");
            ValidateRows(rows);
            result.InvalidRows = rows.Count(r => r.Status.StartsWith("Invalid"));
            result.ValidRows = rows.Count(r => r.Status == "Valid");
            _log($"Valid rows: {result.ValidRows}");
            _log($"Invalid rows: {result.InvalidRows}");
            _log("");

            _log("REMOVE DUPLICATES...");
            RemoveDuplicates(rows);
            result.RemovedDuplicateRows = rows.Count(r => r.IsRemoved && r.RemovedReason == "Duplicate");
            _log($"Removed duplicate rows: {result.RemovedDuplicateRows}");
            _log("");

            _log("REMOVE CONTAINMENT SHORT WORDS...");
            RemoveContainmentShortWords(rows);
            result.RemovedContainmentRows = rows.Count(r => r.IsRemoved && r.RemovedReason == "Containment");
            _log($"Removed containment rows: {result.RemovedContainmentRows}");
            _log("");

            var finalRows = rows
                .Where(r => r.Status == "Valid" && !r.IsRemoved)
                .OrderBy(r => r.RowNumber)
                .ToList();

            result.FinalRows = finalRows.Count;
            _log($"Final clean rows: {result.FinalRows}");
            _log("");

            _log("SAVE cleaned Excel...");
            SaveCleanedExcelFromList(cleanedExcelPath, finalRows);
            _log("Cleaned Excel saved: " + cleanedExcelPath);
            _log("");

            _log("GENERATE XML from final clean list...");
            GenerateLexiconXmlFromList(finalRows, xmlFilePath);
            _log("XML file saved: " + xmlFilePath);
            _log("");

            _log("SAVE Log file...");
            SaveLogFile(logFilePath, rows, result);
            _log("Log file saved: " + logFilePath);
            _log("");

            result.CleanedExcelPath = cleanedExcelPath;
            result.XmlFilePath = xmlFilePath;
            result.LogFilePath = logFilePath;

            return result;
        }

        private List<DictionaryRowModel> ReadDictionaryRows(string filePath)
        {
            var rows = new List<DictionaryRowModel>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var usedRows = worksheet.RangeUsed().RowsUsed().ToList();

                if (usedRows.Count <= 1)
                    return rows;

                foreach (var row in usedRows.Skip(1))
                {
                    var model = new DictionaryRowModel
                    {
                        RowNumber = row.RowNumber(),
                        SerialNo = row.Cell(1).GetString()?.Trim(),   // Column A
                        Word = row.Cell(2).GetString()?.Trim(),       // Column B
                        Phoneme = row.Cell(3).GetString()?.Trim(),    // Column C
                        Status = "Read"
                    };

                    rows.Add(model);
                    _log($"Loaded Row {model.RowNumber}: Word='{model.Word}', Phoneme='{model.Phoneme}'");
                }
            }

            return rows;
        }

        private void ValidateRows(List<DictionaryRowModel> rows)
        {
            foreach (var row in rows)
            {
                row.Word = NormalizeKey(row.Word);
                row.Phoneme = NormalizePhoneme(row.Phoneme);

                if (string.IsNullOrWhiteSpace(row.Word))
                {
                    row.Status = "Invalid - Word empty";
                    _log($"Row {row.RowNumber}: skipped, Word empty");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(row.Phoneme))
                {
                    row.Status = "Invalid - Phoneme empty";
                    _log($"Row {row.RowNumber}: skipped, Phoneme empty");
                    continue;
                }

                row.Status = "Valid";
                _log($"Row {row.RowNumber}: valid");
            }
        }

        private void RemoveDuplicates(List<DictionaryRowModel> rows)
        {
            var validRows = rows
                .Where(r => r.Status == "Valid")
                .OrderBy(r => r.RowNumber)
                .ToList();

            var firstSeen = new Dictionary<string, DictionaryRowModel>(StringComparer.OrdinalIgnoreCase);

            foreach (var row in validRows)
            {
                if (!firstSeen.ContainsKey(row.Word))
                {
                    firstSeen[row.Word] = row;
                    continue;
                }

                row.IsRemoved = true;
                row.RemovedReason = "Duplicate";
                row.Status = "Removed - Duplicate";

                var firstRow = firstSeen[row.Word];
                _log($"Duplicate found: Word='{row.Word}' at row {row.RowNumber}. Kept first row {firstRow.RowNumber}, removed row {row.RowNumber}");
            }
        }

        private void RemoveContainmentShortWords(List<DictionaryRowModel> rows)
        {
            var candidates = rows
                .Where(r => r.Status == "Valid" && !r.IsRemoved)
                .OrderBy(r => r.RowNumber)
                .ToList();

            for (int i = 0; i < candidates.Count; i++)
            {
                var shortRow = candidates[i];

                if (shortRow.IsRemoved)
                    continue;

                for (int j = 0; j < candidates.Count; j++)
                {
                    if (i == j)
                        continue;

                    var longRow = candidates[j];

                    if (longRow.IsRemoved)
                        continue;

                    if (string.Equals(shortRow.Word, longRow.Word, StringComparison.OrdinalIgnoreCase))
                        continue;

                    // remove short word if it is contained in a longer word
                    if (shortRow.Word.Length < longRow.Word.Length &&
                        longRow.Word.IndexOf(shortRow.Word, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        shortRow.IsRemoved = true;
                        shortRow.RemovedReason = "Containment";
                        shortRow.Status = "Removed - Containment";

                        _log($"Containment found: short word '{shortRow.Word}' at row {shortRow.RowNumber} is contained in longer word '{longRow.Word}' at row {longRow.RowNumber}. Removed row {shortRow.RowNumber}");
                        break;
                    }
                }
            }
        }

        private void SaveCleanedExcelFromList(string outputFilePath, List<DictionaryRowModel> cleanedRows)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Dictionary");

                worksheet.Cell(1, 1).Value = "SL";
                worksheet.Cell(1, 2).Value = "Word";
                worksheet.Cell(1, 3).Value = "Phoneme";

                int excelRow = 2;
                foreach (var row in cleanedRows)
                {
                    worksheet.Cell(excelRow, 1).Value = row.SerialNo;
                    worksheet.Cell(excelRow, 2).Value = row.Word;
                    worksheet.Cell(excelRow, 3).Value = row.Phoneme;
                    excelRow++;
                }

                workbook.SaveAs(outputFilePath);
            }
        }

        private void GenerateLexiconXmlFromList(List<DictionaryRowModel> rows, string outputXmlPath)
        {
            XNamespace ns = "http://www.w3.org/2005/01/pronunciation-lexicon";

            var lexicon = new XElement(ns + "lexicon",
                new XAttribute("version", "1.0"),
                new XAttribute("alphabet", "sapi"),
                new XAttribute(XNamespace.Xml + "lang", "ja-JP"),
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(
                    XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance") + "schemaLocation",
                    "http://www.w3.org/2005/01/pronunciation-lexicon http://www.w3.org/TR/2007/CR-pronunciation-lexicon-20071212/pls.xsd")
            );

            foreach (var row in rows)
            {
                if (string.IsNullOrWhiteSpace(row.Word) || string.IsNullOrWhiteSpace(row.Phoneme))
                    continue;

                var lexeme = new XElement(ns + "lexeme",
                    new XElement(ns + "grapheme", row.Word),
                    new XElement(ns + "phoneme", row.Phoneme)
                );

                lexicon.Add(lexeme);
            }

            var doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), lexicon);
            doc.Save(outputXmlPath);
        }

        private void SaveLogFile(string logPath, List<DictionaryRowModel> rows, DictionaryProcessResult result)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Dictionary Processing Log");
            sb.AppendLine("Generated Time: " + DateTime.Now);
            sb.AppendLine("======================================");
            sb.AppendLine($"Total Rows                : {result.TotalRows}");
            sb.AppendLine($"Valid Rows                : {result.ValidRows}");
            sb.AppendLine($"Invalid Rows              : {result.InvalidRows}");
            sb.AppendLine($"Removed Duplicate Rows    : {result.RemovedDuplicateRows}");
            sb.AppendLine($"Removed Containment Rows  : {result.RemovedContainmentRows}");
            sb.AppendLine($"Final Rows                : {result.FinalRows}");
            sb.AppendLine("======================================");
            sb.AppendLine();

            foreach (var row in rows.OrderBy(r => r.RowNumber))
            {
                sb.AppendLine(
                    $"Row {row.RowNumber}: Serial='{row.SerialNo}', Word='{row.Word}', Phoneme='{row.Phoneme}', Status='{row.Status}'");
            }

            File.WriteAllText(logPath, sb.ToString(), Encoding.UTF8);
        }

        private string NormalizeKey(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            value = value.Trim();
            value = value.Replace("\u3000", " ");
            while (value.Contains("  "))
            {
                value = value.Replace("  ", " ");
            }

            return value;
        }

        private string NormalizePhoneme(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
        }
    }
}