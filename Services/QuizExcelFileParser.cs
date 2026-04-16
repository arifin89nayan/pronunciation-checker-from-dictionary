using ClosedXML.Excel;
using NAudio.SoundFont;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WindowsFormsApp1.Models.AutoConverters;
using WindowsFormsApp1.Services.Abstraction;

namespace WindowsFormsApp1.Services
{
    public class QuizExcelFileParser : IWidgetFileParserQuiz
    {
        public IEnumerable<WidgetParsedCommonModel> ParseFile(string filePath)
        {

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var range = worksheet.RangeUsed();

                foreach (var row in range.RowsUsed().Skip(2))
                {
                    string quizOrGuide = row.Cell(3).GetString()?.Trim();
                    if (quizOrGuide == "1")  // Quiz
                    {
                        yield return new QuizParserModel
                        {
                            Sl = row.Cell(1).GetString(),
                            Generate=row.Cell(2).GetString(),
                            QuizOrgUid = quizOrGuide,
                            Language = row.Cell(4).GetString(),
                            TemplateName = row.Cell(5).GetString(),
                            QuizTemplate = row.Cell(5).GetString(),
                            QuizFolderName = row.Cell(6).GetString(),
                            GuideName = row.Cell(6).GetString(),
                            //TitleFontSize = row.Cell(7).GetString(),
                            //TitleFontColor = row.Cell(8).GetString(),
                            QuizID = row.Cell(9).GetString(),
                            GuideId = row.Cell(9).GetString(),
                            QuizQuestion = row.Cell(10).GetString(),
                            //QuizPronounce = row.Cell(8).GetString(),
                            QuizQuestionAudio = row.Cell(11).GetString(),

                            QuizCorrectAnswer = row.Cell(12).GetString(),
                            //QuizEXPPronounce = row.Cell(11).GetString(),
                            QuizExplanationAudio = row.Cell(13).GetString(),

                            SelectionNumber = row.Cell(14).GetString(),
                            QuizCorrectAnswerSelection = row.Cell(15).GetString(),
                            SelectionA = row.Cell(16).GetString(),
                            SelectionB = row.Cell(17).GetString(),
                            SelectionC = row.Cell(18).GetString(),
                            SelectionD = row.Cell(19).GetString()
                        };
                    }
                    else if (quizOrGuide == "0")  // Guide
                    {
                        yield return new GuideParsedModel
                        {
                            Generate = row.Cell(2).GetString(),
                            GuideId = row.Cell(9).GetString(),
                            GuideName = row.Cell(6).GetString(),
                            language = row.Cell(4).GetString(),
                            //Voice = row.Cell(6).GetString(),
                            TemplateName = row.Cell(5).GetString(),
                            TitleFontSize = row.Cell(7).GetString(),
                            TitleFontColor = row.Cell(8).GetString(),
                            Guide = row.Cell(10).GetString(),
                            //GuidePronounce = row.Cell(8).GetString(),
                            AudioFile = row.Cell(11).GetString(),
                            QuizexpAudioFile = row.Cell(13).GetString()
                        };
                    }
                }
            }
        }
    }
}


