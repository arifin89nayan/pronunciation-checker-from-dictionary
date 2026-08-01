using System;
using System.Collections.Generic;
using System.Drawing;
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
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp1.Services
{
    public class CaptionWidgetProcessor : IAutoWidgetProcessor
    {
 

        private readonly CaptionFormService _captionFormService;
        private readonly FileService _fileService;
        private readonly GlobalConfigPropreties instance;
        private readonly List<LanguageParsedModel> _languageList;
        private string audionFileName;



        string txtT1 = string.Empty;
        string txtTa = string.Empty;
        string txtT3 = string.Empty;

        public string ContentText;
        public string AudioName = $"3000{DateTime.Now:ss}.mp3";

        private int generatedImageHeight;

        public CaptionWidgetProcessor(List<LanguageParsedModel> languageList)
        {
            _captionFormService = new CaptionFormService();
            _fileService = new FileService();
            instance = GlobalConfigPropreties.Instance;
            _languageList = languageList;
        }
        public async Task ProcessWidges(Widget widget, WidgetParsedCommonModel widgetParsedComonModel)
        {
           var widgetParsedModel = widgetParsedComonModel as GuideParsedModel;

            foreach (var line in widget.Lines)
            {
                if (line.Type == Enums.LineTypeEnum.Caption)
                {
                    await ProcessCaptionLine(widget, widgetParsedModel,line);
                }
                else
                {
                    instance.AddLine(line.Text);
                    _fileService.SaveLineFiles(line);
                }
            }
        }
        public async Task QuizCaptionProcessWidges(Widget widget, WidgetParsedCommonModel widgetParsedComonModel)
        {
            var widgetParsedModel = widgetParsedComonModel as GuideParsedModel;
            foreach (var line in widget.Lines)
            {
                if (line.Type == Enums.LineTypeEnum.Caption)
                {

                    await ProcessQuizCaptionLine(widget, widgetParsedModel, line);
                    break;
                }
               
            }
        }
        private async Task ProcessQuizCaptionLine(Widget widget, GuideParsedModel widgetParsedModel, Line line)
        {
            audionFileName = AudioInfo.GetRandomFileName("QD-");
            TextValue languageAndVoice = TextConverterService.MapGuideModelToTextValue(widgetParsedModel, _languageList);
            if (languageAndVoice.fontname != null && languageAndVoice.fontsize !=null)
            { 
                float fontSize = float.Parse(languageAndVoice.fontsize);

                // Set Properties.Settings values
                Properties.Settings.Default.CapFontName = languageAndVoice.fontname;
                Properties.Settings.Default.CapFontSize = fontSize;

                // Save the settings to persist them
                Properties.Settings.Default.Save();
            }
            InitializeFromPlayConfig(line);

            if (!ConvertImage(widgetParsedModel))
            {
                MessageBox.Show("Failed to convert text to image.Please try again.");
                return;
            }
            var mediaDuration = widget.Lines[3];
            var media = widget.Lines[4];

            if (!string.IsNullOrWhiteSpace(widgetParsedModel.AudioFile) && widgetParsedModel.AudioFile.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
            {
                audionFileName = widgetParsedModel.AudioFile;
                ProcessUploadedAudioQuiz(
                    uploadedAudioPath: Path.Combine(GlobalProperties.AutoProcessAudioFolderPath, widgetParsedModel.AudioFile),
                    outputFilePath: Path.Combine(GlobalProperties.OutputPath, widgetParsedModel.AudioFile)
                );
                
            }
            
            else if (!string.IsNullOrWhiteSpace(widgetParsedModel.QuizexpAudioFile) && widgetParsedModel.QuizexpAudioFile.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
            {
                audionFileName = widgetParsedModel.QuizexpAudioFile;
                ProcessUploadedAudioQuiz(
                    uploadedAudioPath: Path.Combine(GlobalProperties.AutoProcessAudioFolderPath, widgetParsedModel.QuizexpAudioFile),
                    outputFilePath: Path.Combine(GlobalProperties.OutputPath, widgetParsedModel.QuizexpAudioFile)
                );
            }
            else if (!string.IsNullOrEmpty(widgetParsedModel.AudioFile) &&  !widgetParsedModel.AudioFile.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
            {
                await GenerateAudioQuizCaption(widgetParsedModel.AudioFile, languageAndVoice.language, languageAndVoice.voice, languageAndVoice.filename, audionFileName);
            }
            else if (!string.IsNullOrEmpty(widgetParsedModel.QuizexpAudioFile) && !widgetParsedModel.QuizexpAudioFile.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
            {
                await GenerateAudioQuizCaption(widgetParsedModel.QuizexpAudioFile, languageAndVoice.language, languageAndVoice.voice, languageAndVoice.filename, audionFileName);
            }
            else
            {
                await GenerateAudioQuizCaption(widgetParsedModel.Guide, languageAndVoice.language, languageAndVoice.voice, languageAndVoice.filename, audionFileName);

            }


            int numChunks = (int)Math.Ceiling((double)generatedImageHeight / 362);
            _captionFormService.GenerateMediaLines(mediaDuration, media, audionFileName);

            var t1 = _captionFormService.ConvertToSecond(txtT1);
            var t3 = _captionFormService.ConvertToSecond(txtT3);
            var t2 = _captionFormService.CalculateT2(txtTa, widget.Lines[3], audionFileName);
            var baseImageHeigt = (int)Math.Ceiling((double)generatedImageHeight);

            string baseFileName = Path.GetFileNameWithoutExtension(GlobalProperties.CaptionBaseImage);

            var text = _captionFormService.GetCalculatedLine(
                line,
                t1, t2, t3,
                numChunks,
                baseFileName,
                baseImageHeigt,
                isChangeT2: mediaDuration.Text.Contains("Duration"));
            GlobalConfigPropreties.Instance.AddLine(text);
        }
        private void ProcessUploadedAudioQuiz(string uploadedAudioPath, string outputFilePath)
        {
            File.Copy(uploadedAudioPath, outputFilePath, overwrite: true);

        }

        private async Task ProcessCaptionLine(Widget widget, GuideParsedModel widgetParsedModel, Line line)
        {
            
            InitializeFromPlayConfig(line);
            TextValue languageAndVoice = TextConverterService.MapGuideModelToTextValue(widgetParsedModel, _languageList);
            if (languageAndVoice.fontname != null && languageAndVoice.fontsize != null)
            {
                float fontSize = float.Parse(languageAndVoice.fontsize);

                // Set Properties.Settings values
                Properties.Settings.Default.CapFontName = languageAndVoice.fontname;
                Properties.Settings.Default.CapFontSize = fontSize;

                // Save the settings to persist them
                Properties.Settings.Default.Save();
            }

            if (!ConvertImage(widgetParsedModel))
            {
                ErrorLogger.LogError("Failed to convert text to image.Please try again.");
                return;
            }

            var mediaDuration = widget.Lines[3];
            var media = widget.Lines[4];

            if (media.Text.Contains("Audio") && mediaDuration.Text.Contains("Duration"))
            {
                await ProcessUploadedAudio(GlobalProperties.AutoProcessAudioFolderPath, GlobalProperties.OutputPath, widgetParsedModel, widgetParsedModel.Guide);

            }

            int numChunks = (int)Math.Ceiling((double)generatedImageHeight / 362);
            _captionFormService.GenerateMediaLines(mediaDuration, media, widgetParsedModel.AudioFile);

            var t1 = _captionFormService.ConvertToSecond(txtT1);
            var t3 = _captionFormService.ConvertToSecond(txtT3);
            var t2 = _captionFormService.CalculateT2(txtTa, widget.Lines[3], widgetParsedModel.AudioFile);
            var baseImageHeigt = (int)Math.Ceiling((double)generatedImageHeight);

            string baseFileName = Path.GetFileNameWithoutExtension(GlobalProperties.CaptionBaseImage);

            var text = _captionFormService.GetCalculatedLine(
                line,
                t1, t2, t3,
                numChunks,
                baseFileName,
                baseImageHeigt,
                isChangeT2: mediaDuration.Text.Contains("Duration"));
            GlobalConfigPropreties.Instance.AddLine(text);
        }

        private async Task ProcessUploadedAudio(string source, string destination, GuideParsedModel widgetParsedModel, string content)
        {
            //TODO : If no source file then generate a audio file.

            if (string.IsNullOrWhiteSpace(widgetParsedModel.AudioFile) )
            {
                widgetParsedModel.AudioFile = "00000" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper() + ".MP3";

                var path = Path.Combine(destination, widgetParsedModel.AudioFile);
                TextValue languageAndVoice = TextConverterService.MapGuideModelToTextValue(widgetParsedModel, _languageList);


                await GenerateAudio(content, languageAndVoice.language, languageAndVoice.voice, languageAndVoice.filename, path);
                return;
            }
            if (!string.IsNullOrEmpty(widgetParsedModel.AudioFile) && widgetParsedModel.AudioFile.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
            {
                // Copy existing mp3 audio file
                DirectoryHelper.CopyFiles(source, destination, (fileName) =>
                {
                    return fileName.Equals(widgetParsedModel.AudioFile, StringComparison.OrdinalIgnoreCase);
                });
                return;
            }
            if (!string.IsNullOrEmpty(widgetParsedModel.AudioFile) && !widgetParsedModel.AudioFile.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
            {
                string guidText = widgetParsedModel.AudioFile;
                widgetParsedModel.AudioFile = "00000" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper() + ".MP3";
                var path = Path.Combine(destination, widgetParsedModel.AudioFile);
                TextValue languageAndVoice = TextConverterService.MapGuideModelToTextValue(widgetParsedModel, _languageList);


                await GenerateAudio(guidText, languageAndVoice.language, languageAndVoice.voice, languageAndVoice.filename, path);
                return;
            }
            
        }

        private async Task GenerateAudioQuizCaption(string text, string language, string voice, string filename, string fileName)
        {
            string outputFilePath = Path.Combine(GlobalProperties.OutputPath, fileName);
            var textConverterService = new TextConverterService(outputFilePath);
            await textConverterService.GenerateTextToSpeechAsync(text, new Models.TextValue
            {
                language = language,
                voice = voice,
                filename = filename,
            });
            if (!File.Exists(outputFilePath))
            {
                throw new FileNotFoundException("Audio file was not created.", outputFilePath);
            }


        }
       
        private async Task GenerateAudio(string text, string language, string voice,string filename, string outputFilePath)
        {
           
            var textConverterService = new TextConverterService(outputFilePath);

            await textConverterService.GenerateTextToSpeechAsync(text, new Models.TextValue
            {
                language = language,
                voice = voice,
                filename = filename,
            });
            if (!File.Exists(outputFilePath))
            {
                throw new FileNotFoundException("Audio file was not created.", outputFilePath);
            }

            
        }

        private void InitializeFromPlayConfig(Line captionLine)
        {
            var scrollProperties = ImageExtrator.GetScrollProperties_V2(captionLine.Text);
            int scrollTimeStartValue, scrollTimeEndValue, scrollTimeIntervalValue;

            if (int.TryParse(scrollProperties.scrollTimeStart, out scrollTimeStartValue))
            {
                txtT1 = (scrollTimeStartValue * 10).ToString();
            }
            if (int.TryParse(scrollProperties.scrollTimeEnd, out scrollTimeEndValue))
            {
                txtTa = (scrollTimeEndValue * 10).ToString();
            }
            if (int.TryParse(scrollProperties.scrollTimeInterval, out scrollTimeIntervalValue))
            {
                txtT3 = (scrollTimeIntervalValue * 10).ToString();
            }
        }
        private bool ConvertImage(GuideParsedModel widgetParsedModel)
        {
            ContentText = widgetParsedModel.Guide;

            if (string.IsNullOrWhiteSpace(ContentText))
            {
                return false;
            }
            

            var textGenerateRequest = new TxtToImage
            {
                InputText = ContentText,

            };
            var imageService = new imageService();
            string generatedImagePath;
            generatedImagePath = imageService.GenerateImage(textGenerateRequest);
            GlobalProperties.CaptionBaseImagePath = generatedImagePath;

            using (var v = new Bitmap(generatedImagePath))
            {
                generatedImageHeight = v.Height;
            } 

            File.Delete(generatedImagePath);

            return true;
        }
    }
}
