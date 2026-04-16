using CsvHelper.Configuration;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using WindowsFormsApp1.Models.AutoConverters;
using WindowsFormsApp1.Services.Abstraction;
using WindowsFormsApp1.Helper.Mapper;

namespace WindowsFormsApp1.Services
{
    internal class GuideCsvFileParser : IWidgetFileParser
    {
        public IEnumerable<WidgetParsedCommonModel> ParseFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    DetectDelimiter = true
                }))
                {
                    csv.Context.RegisterClassMap<WidgetCsvFileParserMapper>();
                    var records = csv.GetRecords<GuideParsedModel>();
                    foreach (var record in records)
                    {
                        yield return record;
                    }
                }
            }
        }
    }
}
