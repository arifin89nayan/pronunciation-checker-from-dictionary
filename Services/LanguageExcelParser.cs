using ClosedXML.Excel;
using System.Collections.Generic;
using WindowsFormsApp1.Models.AutoConverters;

namespace WindowsFormsApp1.Services
{
    public class LanguageExcelParser
    {
        public IEnumerable<LanguageParsedModel> ParseFile(string filePath)
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var range = worksheet.RangeUsed();

                foreach (var row in range.RowsUsed())
                { 
                    yield return new LanguageParsedModel
                    {
                        LanguageName = row.Cell(1).GetString(),
                        Language = row.Cell(2).GetString(),
                        Voiceoption = row.Cell(3).GetString(),
                        LanguageDictionary = row.Cell(4).GetString(),
                        FontName = row.Cell(5).GetString(),
                        FontSize = row.Cell(6).GetString(),
                        //Voice3 = row.Cell(7).GetString(),
                        //Voice4 = row.Cell(8).GetString(),
                        //Voice5 = row.Cell(9).GetString(),
                    }; 
                }
            }
        }
    }
}
