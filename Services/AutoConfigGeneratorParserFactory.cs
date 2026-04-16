using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using WindowsFormsApp1.Services.Abstraction;

namespace WindowsFormsApp1.Services
{
    public class AutoConfigGeneratorParserFactory
    {
        public static IWidgetFileParser GetParser(string filePath)
        {
           string fileExtension = Path.GetExtension(filePath);


            if (fileExtension.ToLower() == "CSV".ToLower())
            {
                return new GuideCsvFileParser();
            }
            else if (fileExtension.ToLower() == ".XLSX".ToLower())
                return new GuideExcelFileParser();

            throw new InvalidOperationException($"The file type '{fileExtension}' is not supported for processing.");

        }
        public static IWidgetFileParserQuiz QuizGetParser(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath).ToLower(); 

            //if (fileExtension == ".CSV".ToLower())
            //{
            //    return new QuizCsvFileParser();
            //}
             if (fileExtension == ".XLSX".ToLower())
                return new QuizExcelFileParser();

            throw new InvalidOperationException($"The file type '{fileExtension}' is not supported for processing.");

        }

    }
}

