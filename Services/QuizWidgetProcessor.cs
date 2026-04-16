using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Enums;
using WindowsFormsApp1.Global;
using WindowsFormsApp1.Helper;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Models.AutoConverters;
using WindowsFormsApp1.Models.Widgets;
using WindowsFormsApp1.Services.Abstraction;

namespace WindowsFormsApp1.Services
{
    public class QuizWidgetProcessor : IAutoWidgetProcessorQuiz
    {

        private string audioFilePath;
        private string audionFileName;
        private string _filepath;
        private string extractedText;
        private string quizQuesText;
        private readonly FileService _fileService;
        private int optionOrder = 1;
        private readonly List<LanguageParsedModel> _languageList;
        public QuizWidgetProcessor(List<LanguageParsedModel> languageList)
        {
            _fileService = new FileService();
            _languageList = languageList;
        }
        public async Task ProcessWidges(Widget widget, QuizParserModel widgetParsedModel)
        {

            _filepath = GlobalProperties.PlayConfigPath;

            var parserModel = widgetParsedModel as QuizParserModel;
           
            QuizProperties.Instance.Options = GetOptions(parserModel);
           
            foreach (var line in widget.Lines)
            {
                await ProcessLine(line, widget, parserModel, optionOrder);
                

            }
            
        }

        private List<Option> GetOptions(QuizParserModel parserModel)
        {
            int correctIndex = int.TryParse(parserModel.QuizCorrectAnswerSelection, out int result) ? result : -1;


            var options = new List<Option>
                                            {
                                                new Option
                                                {
                                                    Text = parserModel.SelectionA,
                                                    IsCorrectAns = correctIndex == 1,
                                                    Order = 1
                                                }
                                            };

            if (!string.IsNullOrWhiteSpace(parserModel.SelectionB))
            {
                options.Add(new Option
                {
                    Text = parserModel.SelectionB,
                    IsCorrectAns = correctIndex == 2,
                    Order = 2
                });
            }

            if (!string.IsNullOrWhiteSpace(parserModel.SelectionC))
            {
                options.Add(new Option
                {
                    Text = parserModel.SelectionC,
                    IsCorrectAns = correctIndex == 3,
                    Order = 3
                });
            }

            if (!string.IsNullOrWhiteSpace(parserModel.SelectionD))
            {
                options.Add(new Option
                {
                    Text = parserModel.SelectionD,
                    IsCorrectAns = correctIndex == 4,
                    Order = 4
                });
            }

            return options;
        }
        private async Task ProcessLine(Line line, Widget widget, QuizParserModel widgetParsedModel, int order)
        {
            TextValue languageAndVoice = TextConverterService.MapQuizModelToTextValue(widgetParsedModel, _languageList);

            //Process options
            if (line.Type == LineTypeEnum.Option)
            {
                //process the options
                var items = PatternHelper.GetItems(line.Text, @"<([^>]+)>");

                var RightOrWorng = TextExtrator.ExtractRightOrWorngAns(GlobalProperties.PlayConfigPath);

                if (line.Text.StartsWith("<N><T>"))
                {
                    int offset = QuizProperties.Instance.CurrentOffset == 0 ? 50 : QuizProperties.Instance.CurrentOffset;
                    //int length = QuizProperties.Instance.Options.FirstOrDefault(c => c.Order == optionOrder).Text.Length * 2;
                    int length = QuizProperties.Instance.Options.FirstOrDefault(c => c.Order == optionOrder)?.Text?.Trim().Length * 2 ?? 0;
                    items[2] = $"<{offset}:{length}>";

                    QuizProperties.Instance.CurrentOffset = offset + length;
                }
                else if (line.Text.StartsWith("<H><T>"))
                {
                    //int length = QuizProperties.Instance.Options.FirstOrDefault(c => c.Order == optionOrder).Text.Length * 2;
                    int length = QuizProperties.Instance.Options.FirstOrDefault(c => c.Order == optionOrder)?.Text?.Trim().Length * 2 ?? 0;
                    int offset = QuizProperties.Instance.CurrentOffset;

                    items[2] = $"<{offset}:{length}>";

                    QuizProperties.Instance.CurrentOffset = offset + length;                  
                    Properties.Settings.Default.QuizAnsDetailsOffset = QuizProperties.Instance.CurrentOffset;
                    Properties.Settings.Default.Save();
                    optionOrder++;
                   
                }
                else if (line.Text.Length == 3)
                {

                    int correctAnsOrder = QuizProperties.Instance.Options.First(c => c.IsCorrectAns).Order;

                    if (correctAnsOrder == line.CorrectAnsIndicatorOrder)
                    {
                        if (RightOrWorng.HasValue)
                        {
                            items[0] = $"<{RightOrWorng.Value.rightAns.ToString()}>";
                        }
                    }
                    else
                    {
                        items[0] = $"<{RightOrWorng.Value.wrongAns.ToString()}>";
                    }
                }

                GlobalConfigPropreties.Instance.AddLine(string.Join("", items));
            }
            //Process question
            else if (line.IsQuestion && widget.IsContainSelection && line.Type == LineTypeEnum.Text)
            {
                audionFileName = AudioInfo.GetRandomFileName("Q-");
                string OutputPath = Properties.Settings.Default.OutPutPath;
                //audioFilePath = Path.Combine(OutputPath, audionFileName);
                audioFilePath = OutputPath;

                QuizProperties.Instance.Question = widgetParsedModel.QuizQuestion;

                if (!string.IsNullOrWhiteSpace(widgetParsedModel.QuizQuestionAudio) && widgetParsedModel.QuizQuestionAudio.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
                {
                    audionFileName = widgetParsedModel.QuizQuestionAudio;

                    ProcessUploadedAudio(uploadedAudioPath: Path.Combine(GlobalProperties.AutoProcessAudioFolderPath, widgetParsedModel.QuizQuestionAudio),
                      outputFilePath: Path.Combine(GlobalProperties.OutputPath, widgetParsedModel.QuizQuestionAudio));
                }
                
                else if(!string.IsNullOrEmpty(widgetParsedModel.QuizQuestion))
                {
                    await GenerateAudio(widgetParsedModel.QuizQuestion, languageAndVoice.language, languageAndVoice.voice, audionFileName);
                }
                else if (!string.IsNullOrEmpty(widgetParsedModel.QuizQuestionAudio) && !widgetParsedModel.QuizQuestionAudio.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
                {
                    await GenerateAudio(widgetParsedModel.QuizQuestionAudio, languageAndVoice.language, languageAndVoice.voice, audionFileName);
                }
                GenerateQuestionLine(widget);
            }
            //Process question detail
            else if (line.IsQuestion && !line.Text.Contains("Template<T><50") && line.Text.Contains("Template<T>") && line.Type == LineTypeEnum.Text)
            {

                audioFilePath = Properties.Settings.Default.OutPutPath;

                QuizProperties.Instance.CorrectAnsDetails = widgetParsedModel.QuizCorrectAnswer;

                if (!string.IsNullOrWhiteSpace(widgetParsedModel.QuizExplanationAudio) && widgetParsedModel.QuizExplanationAudio.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
                {
                    audionFileName = widgetParsedModel.QuizExplanationAudio;

                    ProcessUploadedAudio(uploadedAudioPath: Path.Combine(GlobalProperties.AutoProcessAudioFolderPath, widgetParsedModel.QuizQuestionAudio),
                      outputFilePath: Path.Combine(GlobalProperties.OutputPath, widgetParsedModel.QuizQuestionAudio));
                }
                else if (!string.IsNullOrEmpty(widgetParsedModel.QuizExplanationAudio) && !widgetParsedModel.QuizQuestionAudio.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
                {
                    audionFileName = AudioInfo.GetRandomFileName("QD-");

                    await GenerateAudio(widgetParsedModel.QuizExplanationAudio, languageAndVoice.language, languageAndVoice.voice, audionFileName);
                }
                else if (string.IsNullOrEmpty(widgetParsedModel.QuizExplanationAudio))
                {
                    audionFileName = AudioInfo.GetRandomFileName("QD-");

                    await GenerateAudio(widgetParsedModel.QuizCorrectAnswer, languageAndVoice.language, languageAndVoice.voice, audionFileName);
                }


                GenerateQuestionDetailLine(widget);
            }
            //Quiz widget can contain Captions, so process the caption widget.
            else if (line.Text.Contains("Template<C>") && widget.IsContainSelection && widget.Lines.Any(c => c.WidgetType == WidgetTypeEnum.Caption))
            {
                GuideParsedModel questionCaption = new GuideParsedModel
                {
                    Guide = widgetParsedModel.QuizQuestion,
                    language = widgetParsedModel.Language,
                    
                    AudioFile = widgetParsedModel.QuizQuestionAudio,
                    
                };
                await new CaptionWidgetProcessor(_languageList).QuizCaptionProcessWidges(widget, questionCaption);

                if (line.Type == LineTypeEnum.Caption && widget.IsContainSelection)
                {
                    QuizProperties.Instance.Question = widgetParsedModel.QuizQuestion;

                }
               
                else if (line.Type == LineTypeEnum.Caption && !widget.IsContainSelection)
                {
                    QuizProperties.Instance.CorrectAnsDetails = widgetParsedModel.QuizCorrectAnswer;
                }
            }
            else if (line.Text.Contains("Template<C>") && !widget.IsContainSelection && widget.Lines.Any(c => c.WidgetType == WidgetTypeEnum.Caption))
            {
                GuideParsedModel AnsDetailsCaption = new GuideParsedModel
                {
                    Guide = widgetParsedModel.QuizCorrectAnswer,
                    language = widgetParsedModel.Language,               
                    QuizexpAudioFile = widgetParsedModel.QuizExplanationAudio,
                    
                };
                await new CaptionWidgetProcessor(_languageList).QuizCaptionProcessWidges(widget, AnsDetailsCaption);
                if (line.Type == LineTypeEnum.Caption && !widget.IsContainSelection)
                {
                    QuizProperties.Instance.CorrectAnsDetails = widgetParsedModel.QuizCorrectAnswer;
                }
            }
            else
            {
                var instance = GlobalConfigPropreties.Instance;

                if (line.Text.Contains("Template<S>"))
                {
                    (int right, int wrong) = TextExtrator.ExtractRightOrWorngAnsFromLine(line.Text);
                    instance.AddLine(line.Text.Replace("Template", "").Replace($"({right},{wrong})", ""));
                }
                else
                {
                    instance.AddLine(line.Text.Replace("Template", ""));
                }

            }


            _fileService.QuizSaveLineFiles(line);
        }

        //private async Task ProcessLine(Line line, Widget widget, QuizParserModel widgetParsedModel, int order)
        //{
        //    TextValue languageAndVoice = TextConverterService.MapQuizModelToTextValue(widgetParsedModel, _languageList);

        //    //Process options
        //    if (line.Type == LineTypeEnum.Option)
        //    {
        //        //process the options
        //        var items = PatternHelper.GetItems(line.Text, @"<([^>]+)>");

        //        var RightOrWorng = TextExtrator.ExtractRightOrWorngAns(GlobalProperties.PlayConfigPath);

        //        if (line.Text.StartsWith("<N><T>"))
        //        {
        //            //int offset = QuizProperties.Instance.CurrentOffset;
        //            int offset = QuizProperties.Instance.CurrentOffset == 0 ? 50 : QuizProperties.Instance.CurrentOffset;
        //            int length = QuizProperties.Instance.Options.FirstOrDefault(c => c.Order == optionOrder).Text.Length * 2;
        //            //var correctOption = QuizProperties.Instance.Options.FirstOrDefault(o => o.IsCorrectAns);
        //            //int length = correctOption != null ? correctOption.Text.Length * 2 : 0;
        //            //int length = QuizProperties.Instance.Options.FirstOrDefault(c => c.Order == order).Text.Length * 2;

        //            items[2] = $"<{offset}:{length}>";

        //            QuizProperties.Instance.CurrentOffset = offset + length;
        //        }
        //        else if (line.Text.StartsWith("<H><T>"))
        //        {
        //            int length = QuizProperties.Instance.Options.FirstOrDefault(c => c.Order == optionOrder).Text.Length * 2;
        //            int offset = QuizProperties.Instance.CurrentOffset;

        //            items[2] = $"<{offset}:{length}>";

        //            QuizProperties.Instance.CurrentOffset = offset + length;
        //            //string templatepath = txt_template.Text.Trim() ?? "";
        //            Properties.Settings.Default.QuizAnsDetailsOffset = QuizProperties.Instance.CurrentOffset;
        //            Properties.Settings.Default.Save();
        //            optionOrder++;
        //            //int length = QuizProperties.Instance.Options.FirstOrDefault(c => c.Order == order).Text.Length * 2;
        //            ///////
        //            //var correctOption = QuizProperties.Instance.Options.FirstOrDefault(o => o.IsCorrectAns);
        //            //int length = correctOption != null ? correctOption.Text.Length * 2 : 0;
        //            //int offset = QuizProperties.Instance.CurrentOffset;

        //            //items[2] = $"<{offset}:{length}>";

        //            //QuizProperties.Instance.CurrentOffset = offset + length;
        //            //optionOrder++;
        //        }
        //        else if (line.Text.Length == 3)
        //        {

        //            int correctAnsOrder = QuizProperties.Instance.Options.First(c => c.IsCorrectAns).Order;

        //            if (correctAnsOrder == line.CorrectAnsIndicatorOrder)
        //            {
        //                if (RightOrWorng.HasValue)
        //                {
        //                    items[0] = $"<{RightOrWorng.Value.rightAns.ToString()}>";
        //                }
        //            }
        //            else
        //            {
        //                items[0] = $"<{RightOrWorng.Value.wrongAns.ToString()}>";
        //            }
        //        }

        //        GlobalConfigPropreties.Instance.AddLine(string.Join("", items));
        //    }
        //    //Process question
        //    else if (line.IsQuestion && widget.IsContainSelection && line.Type == LineTypeEnum.Text)
        //    {
        //        audionFileName = AudioInfo.GetRandomFileName("Q-");
        //        string OutputPath = Properties.Settings.Default.OutPutPath;
        //        //audioFilePath = Path.Combine(OutputPath, audionFileName);
        //        audioFilePath = OutputPath;

        //        QuizProperties.Instance.Question = widgetParsedModel.QuizQuestion;

        //        if (!string.IsNullOrWhiteSpace(widgetParsedModel.QuizQuestionAudio))
        //        {
        //            audionFileName = widgetParsedModel.QuizQuestionAudio;

        //            ProcessUploadedAudio(uploadedAudioPath: Path.Combine(GlobalProperties.AutoProcessAudioFolderPath, widgetParsedModel.QuizQuestionAudio),
        //              outputFilePath: Path.Combine(GlobalProperties.OutputPath, widgetParsedModel.QuizQuestionAudio));
        //        }
        //        else
        //        {
        //            await GenerateAudio(widgetParsedModel.QuizQuestion, languageAndVoice.language, languageAndVoice.voice, audionFileName);
        //        }
        //        GenerateQuestionLine(widget);
        //    }
        //    //Process question detail
        //    else if (line.IsQuestion && !line.Text.Contains("Template<T><50") && line.Text.Contains("Template<T>") && line.Type == LineTypeEnum.Text)
        //    {

        //        audioFilePath = Properties.Settings.Default.OutPutPath;

        //        QuizProperties.Instance.CorrectAnsDetails = widgetParsedModel.QuizCorrectAnswer;

        //        if (!string.IsNullOrWhiteSpace(widgetParsedModel.QuizQuestionAudio))
        //        {
        //            audionFileName = widgetParsedModel.QuizExplanationAudio;

        //            ProcessUploadedAudio(uploadedAudioPath: Path.Combine(GlobalProperties.AutoProcessAudioFolderPath, widgetParsedModel.QuizQuestionAudio),
        //              outputFilePath: Path.Combine(GlobalProperties.OutputPath, widgetParsedModel.QuizQuestionAudio));
        //        }
        //        else
        //        {
        //            audionFileName = AudioInfo.GetRandomFileName("QD-");

        //            await GenerateAudio(widgetParsedModel.QuizCorrectAnswer, languageAndVoice.language, languageAndVoice.voice, audionFileName);
        //        }


        //        GenerateQuestionDetailLine(widget);
        //    }
        //    //Quiz widget can contain Captions, so process the caption widget.
        //    else if (line.Text.Contains("Template<C>") && widget.IsContainSelection && widget.Lines.Any(c => c.WidgetType == WidgetTypeEnum.Caption))
        //    {
        //        GuideParsedModel questionCaption = new GuideParsedModel
        //        {
        //            Guide = widgetParsedModel.QuizQuestion,
        //            language = widgetParsedModel.Language,
        //            Voice = widgetParsedModel.Voice,
        //            AudioFile= widgetParsedModel.QuizQuestionAudio,
        //        };


        //        await new CaptionWidgetProcessor(_languageList).QuizCaptionProcessWidges(widget, questionCaption);


        //        //Process question and question detail lines after caption widget processing.

        //        if (line.Type == LineTypeEnum.Caption && widget.IsContainSelection)
        //        {
        //            QuizProperties.Instance.Question = widgetParsedModel.QuizQuestion;

        //        }
        //        //TODO : 2. Need to fix the correct answer details if the line is a truly caption line.
        //        else if (line.Type == LineTypeEnum.Caption && !widget.IsContainSelection)
        //        {
        //            QuizProperties.Instance.CorrectAnsDetails = widgetParsedModel.QuizCorrectAnswer;
        //        }
        //    }
        //    else if (line.Text.Contains("Template<C>") && !widget.IsContainSelection && widget.Lines.Any(c => c.WidgetType == WidgetTypeEnum.Caption))
        //    {
        //        GuideParsedModel AnsDetailsCaption = new GuideParsedModel
        //        {
        //            Guide = widgetParsedModel.QuizCorrectAnswer,
        //            language = widgetParsedModel.Language,
        //            Voice = widgetParsedModel.Voice,
        //            QuizexpAudioFile= widgetParsedModel.QuizExplanationAudio,
        //        };
        //        await new CaptionWidgetProcessor(_languageList).QuizCaptionProcessWidges(widget, AnsDetailsCaption);
        //        if (line.Type == LineTypeEnum.Caption && !widget.IsContainSelection)
        //        {
        //            QuizProperties.Instance.CorrectAnsDetails = widgetParsedModel.QuizCorrectAnswer;
        //        }

        //        else
        //        {
        //            var instance = GlobalConfigPropreties.Instance;

        //            if (line.Text.Contains("Template<S>"))
        //            {
        //                (int right, int wrong) = TextExtrator.ExtractRightOrWorngAnsFromLine(line.Text);
        //                instance.AddLine(line.Text.Replace("Template", "").Replace($"({right},{wrong})", ""));
        //            }
        //            else
        //            {
        //                instance.AddLine(line.Text.Replace("Template", ""));
        //            }

        //        }



        //    }
        //    _fileService.QuizSaveLineFiles(line);
        //}
        private void LoadTextFromFile(string filepath)
        {

            string directoryFilePath = filepath;
            string directoryPath = Path.GetDirectoryName(directoryFilePath);

            try
            {

                string[] files = Directory.GetFiles(directoryPath, "TEXTDATA.TXT", SearchOption.TopDirectoryOnly);
                if (files.Length > 0)
                {

                    string filePath = files[0];
                    byte[] fileBytes = File.ReadAllBytes(filePath);
                    string fileContent = System.Text.Encoding.Unicode.GetString(fileBytes);
                    // Remove the null bytes
                    string cleanContent = fileContent.Replace("\0", "");
                    var OffsetLenList = TextExtrator.GetOffset_Lengths(filepath);
                    if (OffsetLenList.Count > 0)
                    {

                        var firstOffsetLen = OffsetLenList[0];
                        int startIndex = firstOffsetLen.Offset;
                        int length = firstOffsetLen.Length;
                        if (startIndex + length <= fileContent.Length)
                        {
                            extractedText = cleanContent.Substring(startIndex + 1, length);

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading file: " + ex.Message);
            }
        }

        private void LoadCorrectAns(string filePath)
        {
            quizQuesText = TextExtrator.GetCorrectAns(filePath);
        }

        private void ProcessUploadedAudio(string uploadedAudioPath, string outputFilePath)
        {
            File.Copy(uploadedAudioPath, outputFilePath, overwrite: true);

        }

        private async Task<SoundPlayer> GenerateAudio(string text, string Language, string Voice, string fileName)
        {
            string outputFilePath = Path.Combine(GlobalProperties.OutputPath, fileName);

            var textConverterService = new TextConverterService(outputFilePath);
            await textConverterService.GenerateTextToSpeechAsync(text, new Models.TextValue
            {
                language = Language,
                voice = Voice,
            });
            if (!File.Exists(outputFilePath))
            {
                throw new FileNotFoundException("Audio file was not created.", outputFilePath);
            }
            return new SoundPlayer(outputFilePath);
        }
        
        private void GenerateQuestionLine(Widget widget)
        {
            int questionOffset = 50;
            int questionLength = QuizProperties.Instance.Question.Length * 2;

            string text = $"<T><{questionOffset}:{questionLength}><0><33><240><97><16777088><0><0><0><0><0><4><3000><1><0>";

            QuizProperties.Instance.CurrentOffset = questionOffset + questionLength;

            GlobalConfigPropreties.Instance.AddLine(text);

            var mediaLine = widget.Lines[4];

            var textFormService = new TextFormService();
            textFormService.GenerateMediaLines(mediaLine, audionFileName);


        }
        private void GenerateQuestionDetailLine(Widget widget)
        {
            //int offset=QuizProperties.Instance.CurrentOffset;
            int questionLength = QuizProperties.Instance.CorrectAnsDetails.Length * 2;
            int offset = Properties.Settings.Default.QuizAnsDetailsOffset;

            string text = $"<T><{offset}:{questionLength}><0><0><240><241><8454143><0><0><0><0><0><4><300><1><0>";

            GlobalConfigPropreties.Instance.AddLine(text);

            var mediaDuration = widget.Lines[3];
            var media = widget.Lines[4];
            //QuizProperties.Instance.CurrentOffset += questionLength;

            var textFormService = new TextFormService();
            textFormService.GenerateMediaLines(mediaDuration, media, audionFileName);

        }
    }
}

