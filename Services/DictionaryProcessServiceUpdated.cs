using ClosedXML.Excel;
    using global::WindowsFormsApp1.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using WindowsFormsApp1.Models;

    public class DictionaryProcessServiceUpdated
    {
        private readonly Action<string> _log;
        private readonly List<string> _logLines = new List<string>();

        public DictionaryProcessServiceUpdated(Action<string> logger)
        {
            _log = logger;
        }

        public DictionaryProcessResult ProcessDictionaryFile(string inputFilePath, string outputFolderPath)
        {
            var result = new DictionaryProcessResult();
            _logLines.Clear();

            if (string.IsNullOrWhiteSpace(inputFilePath) || !File.Exists(inputFilePath))
                throw new FileNotFoundException("Dictionary Excel file not found.", inputFilePath);

            string extension = Path.GetExtension(inputFilePath);

            if (!string.Equals(extension, ".xlsx", StringComparison.OrdinalIgnoreCase))
                throw new NotSupportedException("Please use .xlsx file. ClosedXML does not safely overwrite .xls files.");

            string inputFolder = Path.GetDirectoryName(inputFilePath);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputFilePath);

            if (string.IsNullOrWhiteSpace(inputFolder))
                throw new Exception("Input folder path could not be detected.");

            string logFolder = string.IsNullOrWhiteSpace(outputFolderPath)
                ? inputFolder
                : outputFolderPath;

            Directory.CreateDirectory(logFolder);

            string backupFilePath = Path.Combine(inputFolder, fileNameWithoutExt + ".bak");
            string cleanedExcelPath = inputFilePath;
            string tempCleanedExcelPath = Path.Combine(
                inputFolder,
                fileNameWithoutExt + "_temp_clean_" + Guid.NewGuid().ToString("N") + ".xlsx"
            );

            string logFilePath = Path.Combine(logFolder, fileNameWithoutExt + ".log");
            string xmlFilePath = Path.Combine(inputFolder, fileNameWithoutExt + ".xml");

            try
            {
                WriteLog("===== DICTIONARY PROCESS START =====");
                WriteLog($"Input Excel : {inputFilePath}");
                WriteLog($"Backup File : {backupFilePath}");
                WriteLog($"XML File    : {xmlFilePath}");
                WriteLog("");

                WriteLog("READ Excel...");
                var rows = ReadDictionaryRows(inputFilePath);
                result.TotalRows = rows.Count;
                WriteLog($"Total rows loaded: {result.TotalRows}");
                WriteLog("");

                WriteLog("VALIDATE...");
                ValidateRows(rows, result);
                WriteLog($"Valid rows: {result.ValidRows}");
                WriteLog($"Invalid rows: {result.InvalidRows}");
                WriteLog("");

                WriteLog("REMOVE DUPLICATES...");
                RemoveDuplicates(rows, result);
                WriteLog($"Removed duplicate rows: {result.RemovedDuplicateRows}");
                WriteLog("");

                WriteLog("REMOVE CONTAINMENT SHORT WORDS...");
                RemoveContainmentShortWords(rows, result);
                WriteLog($"Removed containment rows: {result.RemovedContainmentRows}");
                WriteLog("");

                var finalRows = rows
                    .Where(r => r.Status == "Valid" && !r.IsRemoved)
                    .OrderBy(r => r.RowNumber)
                    .ToList();

                result.FinalRows = finalRows.Count;
                WriteLog($"Final clean rows: {result.FinalRows}");
                WriteLog("");

                WriteLog("CREATE BACKUP...");
                File.Copy(inputFilePath, backupFilePath, true);
                WriteLog($"Backup saved: {backupFilePath}");
                WriteLog("");

                WriteLog("SAVE CLEANED EXCEL TO TEMP FILE...");
                SaveCleanedExcelFromList(tempCleanedExcelPath, finalRows);
                WriteLog($"Temp cleaned Excel saved: {tempCleanedExcelPath}");
                WriteLog("");

                WriteLog("OVERWRITE ORIGINAL EXCEL...");
                File.Copy(tempCleanedExcelPath, cleanedExcelPath, true);
                WriteLog($"Original Excel updated: {cleanedExcelPath}");
                WriteLog("");

                WriteLog("GENERATE XML...");
                GenerateLexiconXmlFromList(finalRows, xmlFilePath);
                WriteLog($"XML file saved: {xmlFilePath}");
                WriteLog("");

                WriteLog("===== PROCESS SUMMARY =====");
                WriteLog($"Total Rows          : {result.TotalRows}");
                WriteLog($"Valid Rows          : {result.ValidRows}");
                WriteLog($"Invalid Rows        : {result.InvalidRows}");
                WriteLog($"Removed Duplicates  : {result.RemovedDuplicateRows}");
                WriteLog($"Removed Containment : {result.RemovedContainmentRows}");
                WriteLog($"Final Clean Rows    : {result.FinalRows}");
                WriteLog($"Backup File         : {backupFilePath}");
                WriteLog($"Clean Excel File    : {cleanedExcelPath}");
                WriteLog($"XML File            : {xmlFilePath}");
                WriteLog("");

                SaveLogFile(logFilePath);
                _log?.Invoke($"{DateTime.Now:HH:mm:ss} - Log file saved: {logFilePath}");

                result.LogFilePath = logFilePath;
                result.CleanedExcelPath = cleanedExcelPath;
                result.XmlFilePath = xmlFilePath;

                return result;
            }
            finally
            {
                if (File.Exists(tempCleanedExcelPath))
                {
                    File.Delete(tempCleanedExcelPath);
                }
            }
        }

        private List<DictionaryRowModel> ReadDictionaryRows(string filePath)
        {
            var rows = new List<DictionaryRowModel>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var range = worksheet.RangeUsed();

                if (range == null)
                    return rows;

                var usedRows = range.RowsUsed().ToList();

                if (usedRows.Count <= 1)
                    return rows;

                var columns = DetectDictionaryColumns(worksheet);

                foreach (var row in usedRows.Skip(1))
                {
                    rows.Add(new DictionaryRowModel
                    {
                        RowNumber = row.RowNumber(),
                        Word = row.Cell(columns.WordColumn).GetString()?.Trim(),
                        Phoneme = row.Cell(columns.PhonemeColumn).GetString()?.Trim(),
                        Status = "Read",
                        IsRemoved = false,
                        RemovedReason = ""
                    });
                }
            }

            return rows;
        }

        private (int WordColumn, int PhonemeColumn) DetectDictionaryColumns(IXLWorksheet worksheet)
        {
            var headerRow = worksheet.FirstRowUsed();

            if (headerRow == null)
                return (1, 2);

            int lastColumn = worksheet.LastColumnUsed()?.ColumnNumber() ?? 2;

            int wordColumn = -1;
            int phonemeColumn = -1;

            for (int col = 1; col <= lastColumn; col++)
            {
                string header = NormalizeHeader(headerRow.Cell(col).GetString());

                if (header == "word" || header == "grapheme")
                    wordColumn = col;

                if (header == "phoneme" || header == "phone" || header == "pronunciation")
                    phonemeColumn = col;
            }

            if (wordColumn > 0 && phonemeColumn > 0)
                return (wordColumn, phonemeColumn);

            string firstHeader = NormalizeHeader(headerRow.Cell(1).GetString());

            if (firstHeader == "sl" || firstHeader == "serial" || firstHeader == "no" || firstHeader == "number")
                return (2, 3);

            return (1, 2);
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

                WriteLog(
                    $"Duplicate found: Word='{row.Word}' at row {row.RowNumber}. " +
                    $"Kept first row {firstRow.RowNumber}, removed row {row.RowNumber}"
                );
            }
        }

        private void RemoveContainmentShortWords(List<DictionaryRowModel> rows, DictionaryProcessResult result)
        {
            var candidates = rows
                .Where(r => r.Status == "Valid" && !r.IsRemoved)
                .OrderByDescending(r => r.Word.Length)
                .ThenBy(r => r.RowNumber)
                .ToList();

            foreach (var longRow in candidates)
            {
                if (longRow.IsRemoved)
                    continue;

                foreach (var shortRow in candidates)
                {
                    if (shortRow.IsRemoved)
                        continue;

                    if (shortRow.RowNumber == longRow.RowNumber)
                        continue;

                    if (shortRow.Word.Length >= longRow.Word.Length)
                        continue;

                    if (longRow.Word.IndexOf(shortRow.Word, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        shortRow.IsRemoved = true;
                        shortRow.RemovedReason = "Containment";
                        shortRow.Status = "Removed - Containment";
                        result.RemovedContainmentRows++;

                        WriteLog(
                            $"Containment found: short word '{shortRow.Word}' at row {shortRow.RowNumber} " +
                            $"is contained in longer word '{longRow.Word}' at row {longRow.RowNumber}. " +
                            $"Removed row {shortRow.RowNumber}"
                        );
                    }
                }
            }
        }

        private void SaveCleanedExcelFromList(string outputFilePath, List<DictionaryRowModel> cleanedRows)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Dictionary");

                worksheet.Cell(1, 1).Value = "Word";
                worksheet.Cell(1, 2).Value = "Phoneme";

                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Cell(1, 2).Style.Font.Bold = true;

                int excelRow = 2;

                foreach (var row in cleanedRows)
                {
                    worksheet.Cell(excelRow, 1).Value = row.Word;
                    worksheet.Cell(excelRow, 2).Value = row.Phoneme;
                    excelRow++;
                }

                worksheet.Columns().AdjustToContents();

                string folder = Path.GetDirectoryName(outputFilePath);

                if (!string.IsNullOrWhiteSpace(folder))
                    Directory.CreateDirectory(folder);

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

            var seenWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var row in rows)
            {
                if (string.IsNullOrWhiteSpace(row.Word) || string.IsNullOrWhiteSpace(row.Phoneme))
                    continue;

                if (!seenWords.Add(row.Word))
                    continue;

                var lexeme = new XElement(ns + "lexeme",
                    new XElement(ns + "grapheme", row.Word),
                    new XElement(ns + "phoneme", row.Phoneme)
                );

                lexicon.Add(lexeme);
            }

            var doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), lexicon);

            string folder = Path.GetDirectoryName(outputXmlPath);

            if (!string.IsNullOrWhiteSpace(folder))
                Directory.CreateDirectory(folder);

            doc.Save(outputXmlPath);
        }

        private void SaveLogFile(string logPath)
        {
            string folder = Path.GetDirectoryName(logPath);

            if (!string.IsNullOrWhiteSpace(folder))
                Directory.CreateDirectory(folder);

            File.WriteAllLines(logPath, _logLines, Encoding.UTF8);
        }

        private void WriteLog(string message, bool saveToFile = true)
        {
            string line = $"{DateTime.Now:HH:mm:ss} - {message}";

            _log?.Invoke(line);

            if (saveToFile)
                _logLines.Add(line);
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

        private string NormalizeHeader(string value)
        {
            return NormalizeKey(value)
                .Replace(" ", "")
                .Replace("_", "")
                .Replace("-", "")
                .ToLowerInvariant();
        }
    }

