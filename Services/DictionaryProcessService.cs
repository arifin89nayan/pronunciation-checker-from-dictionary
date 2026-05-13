using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WindowsFormsApp1.Helper;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class DictionaryProcessService
    {
        private readonly Action<string> _log;
        private readonly List<string> _logLines = new List<string>();

        public DictionaryProcessService(Action<string> logger)
        {
            _log = logger;
        }

        private void WriteLog(string message, bool saveToFile = true)
        {
            string line = $"{DateTime.Now:HH:mm:ss} - {message}";
            _log?.Invoke(line);

            if (saveToFile)
            {
                _logLines.Add(line);
            }
        }
        //public DictionaryProcessResult ProcessDictionaryFile(string inputFilePath, string outputFolderPath)
        //{
        //    var result = new DictionaryProcessResult();

        //    string inputFolder = Path.GetDirectoryName(inputFilePath);
        //    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputFilePath);
        //    string extension = Path.GetExtension(inputFilePath);

        //    // Final paths
        //    string backupFilePath = Path.Combine(inputFolder, fileNameWithoutExt + ".bak");
        //    string cleanedExcelPath = inputFilePath; // overwrite original Excel
        //    string tempCleanedExcelPath = Path.Combine(
        //        inputFolder,
        //        fileNameWithoutExt + "_temp_clean_" + Guid.NewGuid().ToString("N") + extension
        //    );

        //    string logFilePath = Path.Combine(inputFolder, fileNameWithoutExt + ".log");
        //    string xmlFilePath = Path.Combine(inputFolder, fileNameWithoutExt + ".xml");

        //    try
        //    {
        //        // 1. Backup old/original file before modifying
        //        File.Copy(inputFilePath, backupFilePath, true);
        //        _log($"Backup saved: {backupFilePath}");

        //        // 2. Read original file
        //        var rows = ReadDictionaryRows(inputFilePath);
        //        result.TotalRows = rows.Count;

        //        _log("Reading rows from dictionary file...");
        //        foreach (var row in rows)
        //        {
        //            _log($"Row {row.RowNumber}: Word='{row.Word}', Phoneme='{row.Phoneme}'");
        //        }

        //        _log("");
        //        _log("Validating rows...");

        //        foreach (var row in rows)
        //        {
        //            if (string.IsNullOrWhiteSpace(row.Word))
        //            {
        //                row.Status = "Invalid - Word empty";
        //                result.InvalidRows++;
        //                _log($"Row {row.RowNumber}: Invalid - Word is empty");
        //                continue;
        //            }

        //            if (string.IsNullOrWhiteSpace(row.Phoneme))
        //            {
        //                row.Status = "Invalid - Phoneme empty";
        //                result.InvalidRows++;
        //                _log($"Row {row.RowNumber}: Invalid - Phoneme is empty");
        //                continue;
        //            }

        //            row.Word = NormalizeKey(row.Word);
        //            row.Phoneme = NormalizePhoneme(row.Phoneme);
        //            row.Status = "Valid";
        //            result.ValidRows++;
        //            _log($"Row {row.RowNumber}: OK");
        //        }

        //        _log("");
        //        _log("Checking duplicates...");

        //        var validRows = rows.Where(r => r.Status == "Valid").ToList();
        //        var duplicateGroups = FindDuplicates(validRows);

        //        result.DuplicateGroups = duplicateGroups.Count;
        //        result.ConflictDuplicates = duplicateGroups.Count(d => d.HasConflict);

        //        foreach (var dup in duplicateGroups)
        //        {
        //            _log($"Duplicate Word Found: {dup.Word}");

        //            foreach (var item in dup.Rows)
        //            {
        //                _log($"   Row {item.RowNumber} -> {item.Word} = {item.Phoneme}");
        //            }

        //            if (dup.HasConflict)
        //                _log("   Type: Conflict duplicate (same word, different phoneme)");
        //            else
        //                _log("   Type: Exact duplicate");

        //            _log("");
        //        }

        //        // 3. Save cleaned file to temp first
        //        _log("Creating cleaned Excel file...");
        //        SaveCleanedExcelFromList(inputFilePath, tempCleanedExcelPath, rows, duplicateGroups);

        //        // 4. Replace old Excel with cleaned Excel
        //        File.Copy(tempCleanedExcelPath, cleanedExcelPath, true);
        //        _log($"Original Excel updated: {cleanedExcelPath}");

        //        // 5. Create log file
        //        _log("Creating log file...");
        //        SaveLogFile(logFilePath, rows, duplicateGroups, result);
        //        _log($"Log file saved: {logFilePath}");

        //        // 6. Generate XML without Modified name
        //        _log("Generating XML dictionary...");
        //        GenerateLexiconXmlFromList(rows, xmlFilePath);
        //        _log($"XML file saved: {xmlFilePath}");

        //        result.LogFilePath = logFilePath;
        //        result.CleanedExcelPath = cleanedExcelPath;
        //        result.XmlFilePath = xmlFilePath;

        //        return result;
        //    }
        //    finally
        //    {
        //        if (File.Exists(tempCleanedExcelPath))
        //        {
        //            File.Delete(tempCleanedExcelPath);
        //        }
        //    }
        //}
        public DictionaryProcessResult ProcessDictionaryFile(string inputFilePath, string outputFolderPath)
        {
            var result = new DictionaryProcessResult();

            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputFilePath);
            string cleanedExcelPath = Path.Combine(outputFolderPath, fileNameWithoutExt + "Modified.xlsx");
            string logFilePath = Path.Combine(outputFolderPath, fileNameWithoutExt + ".log");
            string xmlFilePath = Path.Combine(outputFolderPath, fileNameWithoutExt + "Modified.xml");

            // Read
            WriteLog("READ Excel...");
            var rows = ReadDictionaryRows(inputFilePath);
            result.TotalRows = rows.Count;
            WriteLog($"Total rows loaded: {rows.Count}");
            WriteLog("");

            // Validate
            WriteLog("VALIDATE...");
            ValidateRows(rows, result);
            WriteLog($"Valid rows: {result.ValidRows}");
            WriteLog($"Invalid rows: {result.InvalidRows}");
            WriteLog("");

            // Remove duplicates
            WriteLog("REMOVE DUPLICATES...");
            RemoveDuplicates(rows, result);
            WriteLog($"Removed duplicate rows: {result.RemovedDuplicateRows}");
            WriteLog("");

            // Remove containment short words
            WriteLog("REMOVE CONTAINMENT SHORT WORDS...");
            RemoveContainmentShortWords(rows, result);
            WriteLog($"Removed containment rows: {result.RemovedContainmentRows}");
            WriteLog("");

            // Final clean rows
            var finalRows = rows
                .Where(r => r.Status == "Valid" && !r.IsRemoved)
                .OrderBy(r => r.RowNumber)
                .ToList();

            result.FinalRows = finalRows.Count;
            WriteLog($"Final clean rows: {result.FinalRows}");
            WriteLog("");

            // Save cleaned Excel
            WriteLog("SAVE cleaned Excel...");
            SaveCleanedExcelFromList(cleanedExcelPath, finalRows);
            WriteLog($"Cleaned Excel saved: {cleanedExcelPath}");
            WriteLog("");

            // Generate XML from clean list
            WriteLog("GENERATE XML...");
            GenerateLexiconXmlFromList(finalRows, xmlFilePath);
            WriteLog($"XML file saved: {xmlFilePath}");
            WriteLog("");

            // Save log
            WriteLog("SAVE Log file...", saveToFile: false);
            SaveLogFile(logFilePath);
            WriteLog($"Log file saved: {logFilePath}", saveToFile: false);

            result.LogFilePath = logFilePath;
            result.CleanedExcelPath = cleanedExcelPath;
            result.XmlFilePath = xmlFilePath;

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
                    rows.Add(new DictionaryRowModel
                    {
                        RowNumber = row.RowNumber(),
                        //SerialNo = row.Cell(1).GetString()?.Trim(),   // Column A
                        Word = row.Cell(1).GetString()?.Trim(),       // Column B
                        Phoneme = row.Cell(2).GetString()?.Trim(),    // Column C
                        Status = "Read",
                        IsRemoved = false,
                        RemovedReason = ""
                    });
                }
            }

            return rows;
        }

        private void ValidateRows(List<DictionaryRowModel> rows, DictionaryProcessResult result)
        {
            foreach (var row in rows)
            {
                row.Word = NormalizeKey(row.Word);
                row.Phoneme = NormalizePhoneme(row.Phoneme);

                if (string.IsNullOrWhiteSpace(row.Word))
                {
                    row.Status = "Invalid - Word empty";
                    result.InvalidRows++;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(row.Phoneme))
                {
                    row.Status = "Invalid - Phoneme empty";
                    result.InvalidRows++;
                    continue;
                }

                row.Status = "Valid";
                result.ValidRows++;
            }
        }

        private void RemoveDuplicates(List<DictionaryRowModel> rows, DictionaryProcessResult result)
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
                result.RemovedDuplicateRows++;

                var firstRow = firstSeen[row.Word];
                WriteLog($"Duplicate found: Word='{row.Word}' at row {row.RowNumber}. Kept first row {firstRow.RowNumber}, removed row {row.RowNumber}");
            }
        }

        private void RemoveContainmentShortWords(List<DictionaryRowModel> rows, DictionaryProcessResult result)
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

                    if (shortRow.Word.Length < longRow.Word.Length &&
                        longRow.Word.IndexOf(shortRow.Word, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        shortRow.IsRemoved = true;
                        shortRow.RemovedReason = "Containment";
                        shortRow.Status = "Removed - Containment";
                        result.RemovedContainmentRows++;

                        WriteLog($"Containment found: short word '{shortRow.Word}' at row {shortRow.RowNumber} is contained in longer word '{longRow.Word}' at row {longRow.RowNumber}. Removed row {shortRow.RowNumber}");
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

                //worksheet.Cell(1, 1).Value = "SL";
                worksheet.Cell(1, 1).Value = "Word";
                worksheet.Cell(1, 2).Value = "Phoneme";

                int excelRow = 2;
                foreach (var row in cleanedRows)
                {
                    //worksheet.Cell(excelRow, 1).Value = row.SerialNo;
                    worksheet.Cell(excelRow, 1).Value = row.Word;
                    worksheet.Cell(excelRow, 2).Value = row.Phoneme;
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

        private void SaveLogFile(string logPath)
        {
            File.WriteAllLines(logPath, _logLines, Encoding.UTF8);
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