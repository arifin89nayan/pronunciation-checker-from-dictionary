using CsvHelper;
using CsvHelper.Configuration;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using WindowsFormsApp1.Global;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Helper.Quiz
{
    public class CsvGenerator
    {
        public void UpdateCSVFile1(string filePath, TextData textData)
        {
            // Read the CSV file into memory
            var records = new List<QuizCSVRecord>();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<RecordMap>();
                records = csv.GetRecords<QuizCSVRecord>().ToList();
            }

            // Find the record with the specific question and update it
            var recordToUpdate = records.FirstOrDefault(r => r.Id == "1");
            if (recordToUpdate != null)
            {
                recordToUpdate.Question = textData.Question;
                recordToUpdate.CorrectAnswer = $"Correct Answer: {textData.CorrectAnswer}";
                recordToUpdate.Column1 = textData.NumberOfOptions.ToString();
                recordToUpdate.Column2 = textData.Option1.Equals(textData.CorrectAnswer, StringComparison.OrdinalIgnoreCase) ? "1" : "0";
                recordToUpdate.Column3 = textData.Option2.Equals(textData.CorrectAnswer, StringComparison.OrdinalIgnoreCase) ? "1" : "0";
                recordToUpdate.Column4 = textData.Option3.Equals(textData.CorrectAnswer, StringComparison.OrdinalIgnoreCase) ? "1" : "0";
                recordToUpdate.Column5 = textData.Option4.Equals(textData.CorrectAnswer, StringComparison.OrdinalIgnoreCase) ? "1" : "0";

            }

            // Write the updated records back to the CSV file
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
        }
        public void UpdateCSVFile(string filePath, Models.Quiz quiz)
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

            var ids = GetIdsWithQuestionAndCorrectAnswer(CSVFilesPath);
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
        public void UpdateCSVFile11(string filePath, Models.Quiz quiz)
        {
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) { }
            }

            var FilesPath = Path.Combine(GlobalProperties.PlayConfigFolderPath, "TEXTDATA.TXT");
            var CSVFilesPath = Path.Combine(GlobalProperties.PlayConfigFolderPath, "CONTENTINFO.CSV");
            var ids = GetIdsWithQuestionAndCorrectAnswer(CSVFilesPath);

            var extractedIDValue = TextExtrator.ExtractIDFromTextData(FilesPath);

            // Read existing records
            var records = new List<QuizCSVRecord>();
            using (var reader = new StreamReader(CSVFilesPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<RecordMap>();
                records = csv.GetRecords<QuizCSVRecord>().ToList();
            }

            // Loop over all ids with Question and CorrectAnswer, and update if you want
            foreach (var id in ids)
            {
                var recordToUpdate = records.FirstOrDefault(r => r.Id == id);
                if (recordToUpdate != null)
                {
                    // Example: only update if the id matches extractedIDValue
                    
                        recordToUpdate.Question = quiz.Question;
                        recordToUpdate.CorrectAnswer = quiz.CorrectAnsDetails;
                   
                    // If you want to update all, remove the if check above
                }
            }

            // Write records back
            using (var writer = new StreamWriter(CSVFilesPath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<RecordMap>();
                csv.WriteRecords(records);
            }
        }
        public List<string> GetIdsWithQuestionAndCorrectAnswer(string filePath)
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


        public void UpdateCSVFile1(string filePath, Models.Quiz quiz)
        {


            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) { }

            }
            var FilesPath = Path.Combine(GlobalProperties.PlayConfigFolderPath, "TEXTDATA.TXT");
            var extractedIDValue = TextExtrator.ExtractIDFromTextData(FilesPath);



            // Read the CSV file into memory
            var records = new List<QuizCSVRecord>();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<RecordMap>();
                records = csv.GetRecords<QuizCSVRecord>().ToList();
            }
            
            var recordWithId = records.FirstOrDefault(r => r.Id == extractedIDValue);
            if (recordWithId == null)
            {
                records.Insert(0,new QuizCSVRecord { Id = extractedIDValue });
            }

            var recordToUpdate = records.FirstOrDefault(r => r.Id == "1");

            var options = quiz.Options;
            // string correctAns = options.Where(c => c.IsCorrectAns).FirstOrDefault()?.Text;
            //string correctAns = quiz.CorrectAnsDetails;
            string correctAns = string.Join(", ", quiz.Options
                                .Where(o => o.IsCorrectAns)
                                .Select(o => o.Text));



            if (recordToUpdate != null)
            {
                recordToUpdate.Question = quiz.Question;
                recordToUpdate.CorrectAnswer = $"{quiz.CorrectAnsDetails}";
                recordToUpdate.Column1 = options.Count.ToString();
                recordToUpdate.Column2 = options.Count > 0 && options[0].IsCorrectAns ? "1" : "0";
                recordToUpdate.Column3 = options.Count > 1 && options[1].IsCorrectAns ? "1" : "0";
                recordToUpdate.Column4 = options.Count > 2 && options[2].IsCorrectAns ? "1" : "0";
                recordToUpdate.Column5 = options.Count > 3 && options[3].IsCorrectAns ? "1" : "0";

            }
            else
            {
                records.Add(new QuizCSVRecord
                {
                    Id = "1",
                    Question = quiz.Question,
                    CorrectAnswer = $"{quiz.CorrectAnsDetails}",
                    Column1 = options.Count.ToString(),
                    Column2 = options.Count > 0 && options[0].IsCorrectAns ? "1" : "0",
                    Column3 = options.Count > 1 && options[1].IsCorrectAns ? "1" : "0",
                    Column4 = options.Count > 2 && options[2].IsCorrectAns ? "1" : "0",
                    Column5 = options.Count > 3 && options[3].IsCorrectAns ? "1" : "0"
                });

                records.AddRange(new[]
                {
            new QuizCSVRecord { Id = "2", Column1 = "0" },
            new QuizCSVRecord { Id = "3", Column1 = "0" },
            new QuizCSVRecord { Id = "4", Column1 = "0" }
        });


            }

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };

            // Write the updated records back to the CSV file
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, config))
            {
                //csv.WriteRecords(records);
                csv.WriteRecords(records
                                   .GroupBy(r => r.Id) // Avoid duplicates
                                   .Select(g => g.First())
                                   .ToList());
                                    }
        }
    }
}
