using ClosedXML.Excel;
using System;
using System.Collections.Generic; 
using WindowsFormsApp1.Models.AutoConverters;
using WindowsFormsApp1.Services.Abstraction;

namespace WindowsFormsApp1.Services
{
    internal class GuideExcelFileParser : IWidgetFileParser
    {
        public IEnumerable<WidgetParsedCommonModel> ParseFile(string filePath)
        {

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var range = worksheet.RangeUsed();
                //bool isFirstRow = true;
                foreach (var row in range.Rows())
                {
                    //if (isFirstRow)
                    //{
                    //    isFirstRow = false; // skip header
                    //    continue;
                    //}
                    int quizOrgUid = Convert.ToInt32(row.Cell(2).GetValue<string>());
                    if (quizOrgUid == 1)  // Quiz row
                    {
                        yield return new GuideParsedModel
                        {
                            GuideId = row.Cell(3).GetString(),
                            GuideName = row.Cell(4).GetString(),
                            GuideTitle = row.Cell(4).GetString(),
                            Guide = row.Cell(6).GetString(),
                            TemplateName = row.Cell(7).GetString(),
                            AudioFile = row.Cell(9).GetString()
                            
                        };
                    }
                    else if (quizOrgUid == 0)  // Guide row
                    {
                        // Process the guide row differently, if needed
                        // For now, you can simply skip or handle it separately as you prefer
                        continue; // Skip the guide row
                    }

                    //    foreach (var row in range.Rows())
                    //{
                    //    yield return new GuideParsedModel
                    //    {
                    //        GuideId = row.Cell(1).GetString(),
                    //        GuideName = row.Cell(2).GetString(),
                    //        GuideTitle = row.Cell(3).GetString(),
                    //        Guide = row.Cell(4).GetString(),
                    //        TemplateName = row.Cell(5).GetString(),
                    //        AudioFile = row.Cell(6).GetString()
                    //    }; 
                }
            }

        }
    }
}
