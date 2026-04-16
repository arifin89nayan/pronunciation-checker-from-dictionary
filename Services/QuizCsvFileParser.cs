using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO; 
using WindowsFormsApp1.Helper.Mapper;
using WindowsFormsApp1.Models.AutoConverters;
using WindowsFormsApp1.Services.Abstraction;

namespace WindowsFormsApp1.Services
{
    //internal class QuizCsvFileParser : IWidgetFileParserQuiz
    //{
    //    public IEnumerable<QuizParserModel> ParseFile(string filePath)
    //    {
    //        using (var reader = new StreamReader(filePath))
    //        {
    //            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
    //            {
    //                HasHeaderRecord = true,
    //                DetectDelimiter = true
    //            }))
    //            {
    //                csv.Context.RegisterClassMap<Template2CsvFileParserMapper>();
    //                var records = csv.GetRecords<QuizParserModel>();
    //                foreach (var record in records)
    //                {
    //                    yield return record;
    //                }
    //            }
    //        }
    //    }
    //}
}
