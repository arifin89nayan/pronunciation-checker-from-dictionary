using CsvHelper;
using CsvHelper.Configuration;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using WindowsFormsApp1;
using WindowsFormsApp1.Global;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Models.AutoConverters;



namespace WindowsFormsApp1.Helper.Quiz
{
    public class CsvGenerator
    {
        private const int RowCount = 5;
        public void UpdateCSVFile(string filePath, Models.Quiz quiz, int numberOfChoices, int correctAnswerSelection)
        {
            var csvFilePath = Path.Combine(GlobalProperties.PlayConfigFolderPath, "CONTENTINFO.CSV");
            var textDataPath = Path.Combine(GlobalProperties.PlayConfigFolderPath, "TEXTDATA.TXT");
            var extractedIDValue = TextExtrator.ExtractIDFromTextData(textDataPath);

            // NOTE: shift_jis cannot represent Simplified Chinese characters
            // (确, 鱼, 它们 …) — they become "?" like in your screenshot.
            // If the CorrectAnswer text is Chinese, switch to UTF-8 with BOM:
            //   var encoding = new UTF8Encoding(true);
            //var encoding = Encoding.GetEncoding("UTF-8");
            //var encoding = Encoding.GetEncoding("shift_jis");
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                MissingFieldFound = null,
                BadDataFound = null
            };

            try
            {
                string headerLine;
                List<QuizCSVRecord> records;

                if (File.Exists(csvFilePath))
                {
                    //var allLines = File.ReadAllLines(csvFilePath, encoding);
                    var allLines = File.ReadAllLines(csvFilePath);

                    // Preserve the existing header line (contains the ID + column names)
                    headerLine = allLines.Length > 0
                        ? allLines[0]
                        : BuildHeaderLine(extractedIDValue);

                    var dataOnly = string.Join(Environment.NewLine, allLines.Skip(1));

                    using (var reader = new StringReader(dataOnly))
                    using (var csv = new CsvReader(reader, csvConfig))
                    {
                        csv.Context.RegisterClassMap<RecordMap>();
                        records = csv.GetRecords<QuizCSVRecord>().ToList();
                    }
                }
                else
                {
                    headerLine = BuildHeaderLine(extractedIDValue);
                    records = new List<QuizCSVRecord>();
                }

                // Make sure rows 1..5 exist, empty rows get CorrectAnswerSelection = "0"
                for (int i = 1; i <= RowCount; i++)
                {
                    if (records.All(r => r.Id != i.ToString()))
                    {
                        records.Add(new QuizCSVRecord
                        {
                            Id = i.ToString(),
                            Question = string.Empty,
                            CorrectAnswer = string.Empty,
                            CorrectAnswerSelection = "0",
                            AnsNumber2 = string.Empty,
                            AnsNumber3 = string.Empty,
                            AnsNumber4 = string.Empty
                        });
                    }
                }
                records = records.OrderBy(r => int.TryParse(r.Id, out var n) ? n : int.MaxValue).ToList();

                // Update the target rows (same rule as before: rows that already
                // contain a Question + CorrectAnswer)
                var idsToUpdate = records
                    .Where(r => !string.IsNullOrWhiteSpace(r.Question) &&
                                !string.IsNullOrWhiteSpace(r.CorrectAnswer))
                    .Select(r => r.Id)
                    .ToList();

                foreach (var id in idsToUpdate)
                {
                    var rec = records.First(r => r.Id == id);
                    ApplyQuizToRecord(rec, quiz, numberOfChoices, correctAnswerSelection);
                }

                // Write everything back: header line first, then the 5 rows
                //using (var writer = new StreamWriter(filePath, false, encoding))
                using (var writer = new StreamWriter(filePath, false))
               // using (var writer = new StreamWriter(filePath, false, new UTF8Encoding(true)))
                {
                    writer.WriteLine(headerLine);

                    using (var csv = new CsvWriter(writer, csvConfig))
                    {
                        csv.Context.RegisterClassMap<RecordMap>();
                        csv.WriteRecords(records);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error updating CSV file: {ex.Message}", ex);
            }
        }
        

        private static void ApplyQuizToRecord(QuizCSVRecord rec, Models.Quiz quiz,
                                       int numberOfChoices, int correctSelection)
        {
            rec.Question = quiz.Question;
            rec.CorrectAnswer = quiz.CorrectAnsDetails;

            // Column 4 = Number of Choice (from Excel column N) → now correctly "3"
            rec.CorrectAnswerSelection = numberOfChoices.ToString();

            // Columns 5/6/7 = which choice is correct (from Excel column O)
            rec.AnsNumber1 = correctSelection == 1 ? "1" : "0";
            rec.AnsNumber2 = correctSelection == 2 ? "1" : "0";
            rec.AnsNumber3 = correctSelection == 3 ? "1" : "0";
            rec.AnsNumber4 = correctSelection == 4 ? "1" : "0";
        }

        private static string BuildHeaderLine(string id)
        {
            // Matches your screenshot layout. Rename the last three headers
            // if the real file uses different labels.
            return $"{id},Question,CorrectAnswer,CorrectAnswerSelection,Ans Number 1,Ans Number 2,Ans Number 3,Ans Number 4";
        }
       
        public void UpdateCSVFile22(string filePath, Models.Quiz quiz)
        {
            var CSVFilesPath = Path.Combine(GlobalProperties.PlayConfigFolderPath, "CONTENTINFO.CSV");
            var FilesPath = Path.Combine(GlobalProperties.PlayConfigFolderPath, "TEXTDATA.TXT");
            var extractedIDValue = TextExtrator.ExtractIDFromTextData(FilesPath);
            //var SaveCSV = Path.Combine(GlobalProperties.OutputPath);

            if (!File.Exists(CSVFilesPath))
            {
                using (File.Create(CSVFilesPath)) { }
                return;
            }

            var ids = GetIdsWithQuestionAndCorrectAnswer22(CSVFilesPath);
            var records = new List<QuizCSVRecord>();
            var recordWithId = records.FirstOrDefault(r => r.Id == extractedIDValue);
            if (recordWithId == null)
            {
                records.Insert(0, new QuizCSVRecord { Id = extractedIDValue });
            }

            try
            {
                // Read existing records
                using (var reader = new StreamReader(CSVFilesPath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<RecordMap>();
                    records = csv.GetRecords<QuizCSVRecord>().ToList();
                }

                // Update all records that have Question and CorrectAnswer
                foreach (var id in ids)
                {
                    var recordToUpdate = records.FirstOrDefault(r => r.Id == id);
                    if (recordToUpdate != null)
                    {
                        recordToUpdate.Question = quiz.Question;
                        recordToUpdate.CorrectAnswer = quiz.CorrectAnsDetails;
                    }
                }
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false
                };
                // Write all records back
                using (var writer = new StreamWriter(filePath, false))
                using (var csv = new CsvWriter(writer, config))
                {
                    writer.WriteLine(extractedIDValue);
                    csv.Context.RegisterClassMap<RecordMap>();
                    csv.WriteRecords(records);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error updating CSV file: {ex.Message}", ex);
            }
        }
        public void UpdateCSVFile33(string filePath, Models.Quiz quiz)
        {
            var CSVFilesPath = Path.Combine(GlobalProperties.PlayConfigFolderPath, "CONTENTINFO.CSV");
            var FilesPath = Path.Combine(GlobalProperties.PlayConfigFolderPath, "TEXTDATA.TXT");
            var extractedIDValue = TextExtrator.ExtractIDFromTextData(FilesPath);

            if (!File.Exists(CSVFilesPath))
            {
                using (File.Create(CSVFilesPath)) { }
                return;
            }

            // ★ pick ONE encoding and use it for both read and write
            var encoding = Encoding.GetEncoding("shift_jis");   // or new UTF8Encoding(true)

            try
            {
                var ids = GetIdsWithQuestionAndCorrectAnswer33(CSVFilesPath);

                List<QuizCSVRecord> records;

                // ★ Read all lines, skip the first line (the ID header line)
                var allLines = File.ReadAllLines(CSVFilesPath, encoding);
                string dataOnly = string.Join(Environment.NewLine, allLines.Skip(1));

                using (var reader = new StringReader(dataOnly))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false
                }))
                {
                    csv.Context.RegisterClassMap<RecordMap>();
                    records = csv.GetRecords<QuizCSVRecord>().ToList();
                }

                // Update matching records
                foreach (var id in ids)
                {
                    var recordToUpdate = records.FirstOrDefault(r => r.Id == id);
                    if (recordToUpdate != null)
                    {
                        recordToUpdate.Question = quiz.Question;
                        recordToUpdate.CorrectAnswer = quiz.CorrectAnsDetails;
                    }
                }

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false
                };

                // ★ Write: first line = ID, then the records — with explicit encoding
                using (var writer = new StreamWriter(filePath, false, encoding))
                using (var csv = new CsvWriter(writer, config))
                {
                    writer.WriteLine(extractedIDValue);
                    csv.Context.RegisterClassMap<RecordMap>();
                    csv.WriteRecords(records);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error updating CSV file: {ex.Message}", ex);
            }
        }
        public List<string> GetIdsWithQuestionAndCorrectAnswer33(string filePath)
        {
            var ids = new List<string>();
            var encoding = Encoding.GetEncoding("shift_jis");   // same encoding as above

            var allLines = File.ReadAllLines(filePath, encoding);
            string dataOnly = string.Join(Environment.NewLine, allLines.Skip(1)); // skip ID line

            using (var reader = new StringReader(dataOnly))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            }))
            {
                csv.Context.RegisterClassMap<RecordMap>();
                foreach (var record in csv.GetRecords<QuizCSVRecord>())
                {
                    if (!string.IsNullOrWhiteSpace(record.Question) && !string.IsNullOrWhiteSpace(record.CorrectAnswer))
                        ids.Add(record.Id);
                }
            }
            return ids;
        }
        public List<string> GetIdsWithQuestionAndCorrectAnswer22(string filePath)
        {
            var ids = new List<string>();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<RecordMap>();
                var records = csv.GetRecords<QuizCSVRecord>();
                foreach (var record in records)
                {
                    if (!string.IsNullOrWhiteSpace(record.Question) && !string.IsNullOrWhiteSpace(record.CorrectAnswer))
                    {
                        ids.Add(record.Id);
                    }
                }
            }
            return ids;
        }


        
    }
}
