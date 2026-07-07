using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Global;
using WindowsFormsApp1.Helper;
using WindowsFormsApp1.Services.Abstraction;
using WindowsFormsApp1.Services;
using WindowsFormsApp1.Enums;
using WindowsFormsApp1.Helper.Quiz;
using WindowsFormsApp1.Models.AutoConverters;
using WindowsFormsApp1.Models.Widgets;
using DocumentFormat.OpenXml.Vml;

namespace WindowsFormsApp1
{
    public partial class QuizAutoProcess : Form
    {
        private List<LanguageParsedModel> _languageList;
        public QuizAutoProcess()
        {
            InitializeComponent();
            Quiz_txt_template.Text = Properties.Settings.Default.TemplatePath;
            Quiz_txt_FilePath.Text = Properties.Settings.Default.CsvFilePath;
            Quiz_txt_audioFile.Text = Properties.Settings.Default.AudioDataPath;
            Quiz_txt_outputLocation.Text = Properties.Settings.Default.OutPutPath;
        }

       
        private bool ValidateConvert()
        {
            //return txt_audioFile.Text != string.Empty
            //    && txt_FilePath.Text != string.Empty
            //    && txt_template.Text != string.Empty
            //    && txt_outputLocation.Text != string.Empty;
            return Quiz_txt_template.Text != string.Empty
              && Quiz_txt_FilePath.Text != string.Empty;

        }

        private async void Quiz_button2_Click(object sender, EventArgs e)
        {
            string templatepath = Quiz_txt_template.Text.Trim() ?? "";
            Properties.Settings.Default.TemplatePath = templatepath;
            Properties.Settings.Default.Save();
            string csvFilePath = Quiz_txt_FilePath.Text.Trim() ?? "";
            Properties.Settings.Default.CsvFilePath = csvFilePath;
            Properties.Settings.Default.Save();

            string audioFilePath = Quiz_txt_audioFile.Text?.Trim() ?? "";
            Properties.Settings.Default.AudioDataPath = audioFilePath;
            Properties.Settings.Default.Save();

            string outputFilePath = Quiz_txt_outputLocation.Text?.Trim() ?? "";
            Properties.Settings.Default.OutPutPath = outputFilePath;
            Properties.Settings.Default.Save();
            if (!ValidateConvert())
            {
                MessageBox.Show("Please select a valid file CSV path and template file.");
                return;
            }
            IWidgetFileParserQuiz widgetFileParser = AutoConfigGeneratorParserFactory.QuizGetParser(Quiz_txt_FilePath.Text);
            var parsedRecords = widgetFileParser.ParseFile(Quiz_txt_FilePath.Text).ToList();
            string templateRootPath = Properties.Settings.Default.TemplatePath;
            foreach (var item in parsedRecords)
            {
                string templateFolder = item.TemplateName;
                if (string.IsNullOrWhiteSpace(templateFolder))
                {
                    MessageBox.Show($"Template name missing for Guide: {item.GuideName}");
                    continue;
                }

                string playConfigPath = System.IO.Path.Combine(templateRootPath, templateFolder, "PLAY_CONFIG.TXT");

                if (!File.Exists(playConfigPath))
                {
                    MessageBox.Show($"PLAY_CONFIG.TXT not found at {playConfigPath}");
                    continue;
                }
                var analyzer = new PlayConfigAnalyzerService();
                var container = await analyzer.ReadAsync(playConfigPath);
                var quizName = item.GuideName?.Trim() ?? "";
                string OutputPath = Properties.Settings.Default.OutPutPath;
                var outputLocation = OutputPath?.Trim() ?? "";
                string masterFolder = System.IO.Path.Combine(outputLocation, quizName);
                Directory.CreateDirectory(masterFolder);
                // Set directory info
                DirectoryHelper.OutputFolderName = System.IO.Path.Combine(masterFolder, item.GuideId);
                Directory.CreateDirectory(DirectoryHelper.OutputFolderName);

                DirectoryHelper.SelectedPlayconfigFilePath = playConfigPath;

                GlobalProperties.GuideName = item.GuideName;
                GlobalProperties.PlayConfigPath = playConfigPath;
                GlobalProperties.OutputFolderName = item.GuideId;
                GlobalProperties.OutputPath = DirectoryHelper.OutputFolderName;
                string audiopath = Properties.Settings.Default.AudioDataPath;
                GlobalProperties.AutoProcessAudioFolderPath = audiopath;


                await ProcessWidges(container, (QuizParserModel)item);
                var fileService = new FileService();

                foreach (var widget in container.Widgets)
                {
                    foreach (var line in widget.Lines)
                    {
                        // Optional: skip non-text/selection if you only want certain types
                        if (line.Type != LineTypeEnum.Text || line.Type != LineTypeEnum.Selection)
                        {
                            fileService.QuizSaveLineFiles(line);
                        }
                    }
                }


                Quiz_txt_userMessage.AppendText($"Guide {item.GuideName} has been generated successfully. \n");

            }

        }
        private async Task ProcessWidges(WidgetContainer container, QuizParserModel widgetParsedModel)
        {
            bool contentFilesGenerated = false;
            IAutoWidgetProcessorQuiz processor = null;

            var instance = GlobalConfigPropreties.Instance;

            instance.AddLine(container.Version);
            instance.AddLine(container.Widgets.Count.ToString());


            var formService = new FormService();

            foreach (var widget in container.Widgets)
            {
                processor = GetWidgetProcessor(widget);

                if (processor is null)
                {
                    foreach (var line in widget.Lines)
                    {
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
                    continue;
                }
                await processor.ProcessWidges(widget, widgetParsedModel);
                

            }

            instance.SavePlayConfig(System.IO.Path.Combine(GlobalProperties.OutputPath, "PLAY_CONFIG.TXT"));

            if (!contentFilesGenerated && QuizProperties.Instance.Options.Any() && container.Widgets.Any(w => w.Lines.Any(l => l.Type == LineTypeEnum.Text || l.Type == LineTypeEnum.Selection)))
            {
                var quizService = new QuizService();
                quizService.BuildTextData(QuizProperties.Instance, container.Widgets.First());

                var csvGenerator = new CsvGenerator();
                csvGenerator.UpdateCSVFile22(System.IO.Path.Combine(GlobalProperties.OutputPath, "CONTENTINFO.CSV"), QuizProperties.Instance);
               
                contentFilesGenerated = true;
            }

            ContentNameGeneratorService contentNameGeneratorService = new ContentNameGeneratorService();
            contentNameGeneratorService.GenerateContentNameFile(GlobalProperties.GuideName, System.IO.Path.Combine(GlobalProperties.OutputPath, "CONTENTNAME.TXT"));
           
            instance.Clear();
        }
        private IAutoWidgetProcessorQuiz GetWidgetProcessor(Widget widget)
        {
            
            if (widget.Lines.Any(c => c.WidgetType == WidgetTypeEnum.Selection || c.WidgetType == WidgetTypeEnum.Text))
                return new QuizWidgetProcessor(_languageList);


            return null;
        }
        private void CopyQuizTemplateToOutput()
        {
            var destination = DirectoryHelper.GetOutputDirectory();
            var source = DirectoryHelper.GetPlayconfigDirectory();
            
            DirectoryHelper.QuizCopyFiles(source, destination);
        }

        private void Quiz_btn_template_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a Folder";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    Quiz_txt_template.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void Quiz_CSVButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                
                openFileDialog.Title = "Select a File";
                openFileDialog.Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls|CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    Quiz_txt_FilePath.Text = selectedFilePath;
                }
            }
        }

        private void Quiz_btn_audioFile_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a Folder";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    Quiz_txt_audioFile.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void Quiz_btn_output_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a Folder";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    Quiz_txt_outputLocation.Text = folderDialog.SelectedPath;
                }
            }
        }
    } 
}
