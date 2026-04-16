using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Helper;
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
            string logFilePath = Path.Combine(outputFolderPath, fileNameWithoutExt + ".log");
            string xmlFilePath = Path.Combine(outputFolderPath, fileNameWithoutExt + "Modified.xml");

            var rows = ReadDictionaryRows(inputFilePath);

            result.TotalRows = rows.Count;

            _log("Reading rows from dictionary file...");
            foreach (var row in rows)
            {
                _log($"Row {row.RowNumber}: Word='{row.Word}', Phoneme='{row.Phoneme}'");
            }

            _log("");
            _log("Validating rows...");

            foreach (var row in rows)
            {
                if (string.IsNullOrWhiteSpace(row.Word))
                {
                    row.Status = "Invalid - Word empty";
                    result.InvalidRows++;
                    _log($"Row {row.RowNumber}: Invalid - Word is empty");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(row.Phoneme))
                {
                    row.Status = "Invalid - Phoneme empty";
                    result.InvalidRows++;
                    _log($"Row {row.RowNumber}: Invalid - Phoneme is empty");
                    continue;
                }

                row.Word = NormalizeKey(row.Word);
                row.Phoneme = NormalizePhoneme(row.Phoneme);
                row.Status = "Valid";
                result.ValidRows++;
                _log($"Row {row.RowNumber}: OK");
            }

            _log("");
            _log("Checking duplicates...");

            var validRows = rows.Where(r => r.Status == "Valid").ToList();
            var duplicateGroups = FindDuplicates(validRows);

            result.DuplicateGroups = duplicateGroups.Count;
            result.ConflictDuplicates = duplicateGroups.Count(d => d.HasConflict);

            foreach (var dup in duplicateGroups)
            {
                _log($"Duplicate Word Found: {dup.Word}");

                foreach (var item in dup.Rows)
                {
                    _log($"   Row {item.RowNumber} -> {item.Word} = {item.Phoneme}");
                }

                if (dup.HasConflict)
                {
                    _log("   Type: Conflict duplicate (same word, different phoneme)");
                }
                else
                {
                    _log("   Type: Exact duplicate");
                }

                _log("");
            }

            _log("Creating cleaned Excel file...");
            SaveCleanedExcel(inputFilePath, cleanedExcelPath, rows, duplicateGroups);
            _log($"Cleaned Excel saved: {cleanedExcelPath}");

            _log("Creating log file...");
            SaveLogFile(logFilePath, rows, duplicateGroups, result);
            _log($"Log file saved: {logFilePath}");

            _log("Generating XML dictionary...");
            TextConverterService.GenerateLexiconFromExcel(cleanedExcelPath, xmlFilePath);
            _log($"XML file saved: {xmlFilePath}");

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
                        Word = row.Cell(2).GetString()?.Trim(),      // Column B
                        Phoneme = row.Cell(3).GetString()?.Trim(),   // Column C
                        Status = "Read"
                    });
                }
            }

            return rows;
        }

        private List<DuplicateGroupModel> FindDuplicates(List<DictionaryRowModel> rows)
        {
            return rows
                .GroupBy(x => x.Word, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g =>
                {
                    var groupRows = g.OrderBy(x => x.RowNumber).ToList();
                    var phonemes = groupRows
                        .Select(x => x.Phoneme?.Trim())
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToList();

                    return new DuplicateGroupModel
                    {
                        Word = g.Key,
                        Rows = groupRows,
                        HasConflict = phonemes.Count > 1
                    };
                })
                .OrderBy(x => x.Word)
                .ToList();
        }

        private void SaveCleanedExcel(
            string sourceFilePath,
            string outputFilePath,
            List<DictionaryRowModel> allRows,
            List<DuplicateGroupModel> duplicateGroups)
        {
            var wordsToKeep = new HashSet<int>();

            var validRows = allRows.Where(r => r.Status == "Valid").ToList();

            foreach (var group in validRows.GroupBy(r => r.Word, StringComparer.OrdinalIgnoreCase))
            {
                var first = group.OrderBy(r => r.RowNumber).First();
                wordsToKeep.Add(first.RowNumber);
            }

            using (var workbook = new XLWorkbook(sourceFilePath))
            {
                var worksheet = workbook.Worksheet(1);
                var usedRows = worksheet.RangeUsed().RowsUsed().ToList();

                for (int i = usedRows.Count; i >= 2; i--)
                {
                    var excelRow = worksheet.Row(i);

                    var model = allRows.FirstOrDefault(x => x.RowNumber == i);
                    if (model == null)
                    {
                        excelRow.Delete();
                        continue;
                    }

                    if (model.Status != "Valid")
                    {
                        excelRow.Delete();
                        continue;
                    }

                    if (!wordsToKeep.Contains(model.RowNumber))
                    {
                        excelRow.Delete();
                    }
                }

                workbook.SaveAs(outputFilePath);
            }
        }

        private void SaveLogFile(
            string logPath,
            List<DictionaryRowModel> rows,
            List<DuplicateGroupModel> duplicateGroups,
            DictionaryProcessResult result)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Dictionary Processing Log");
            sb.AppendLine("Generated Time: " + DateTime.Now);
            sb.AppendLine("======================================");
            sb.AppendLine($"Total Rows          : {result.TotalRows}");
            sb.AppendLine($"Valid Rows          : {result.ValidRows}");
            sb.AppendLine($"Invalid Rows        : {result.InvalidRows}");
            sb.AppendLine($"Duplicate Groups    : {result.DuplicateGroups}");
            sb.AppendLine($"Conflict Duplicates : {result.ConflictDuplicates}");
            sb.AppendLine("======================================");
            sb.AppendLine();

            sb.AppendLine("Row Details:");
            foreach (var row in rows.OrderBy(r => r.RowNumber))
            {
                sb.AppendLine($"Row {row.RowNumber}: Word='{row.Word}', Phoneme='{row.Phoneme}', Status='{row.Status}'");
            }

            sb.AppendLine();
            sb.AppendLine("Duplicate Details:");
            foreach (var dup in duplicateGroups)
            {
                sb.AppendLine($"Word: {dup.Word}");
                foreach (var item in dup.Rows)
                {
                    sb.AppendLine($"   Row {item.RowNumber}: {item.Phoneme}");
                }
                sb.AppendLine($"   Conflict: {dup.HasConflict}");
                sb.AppendLine();
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
