using DocumentFormat.OpenXml.EMMA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WindowsFormsApp1.Enums;
using WindowsFormsApp1.Global;
using WindowsFormsApp1.Helper;
using WindowsFormsApp1.Helper.Quiz;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Models.Widgets;
using WindowsFormsApp1.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class StartupForm : Form
    {

        private Font _selectedFont;
        private Color _selectedColor;
        private readonly FileService _fileService;
        public StartupForm()
        {

            InitializeComponent();

            _fileService = new FileService();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            string fontName = (Properties.Settings.Default.AniFontName).ToString();
            FontStyle fontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), Properties.Settings.Default.AniFontStyle.ToString());
            float fontSize = Properties.Settings.Default.AniFontSize;
            string fontColor = (Properties.Settings.Default.AniFontColor).ToString();

            Font savedFont = new Font(fontName, fontSize, fontStyle);
            Color savedColor = ColorTranslator.FromHtml(fontColor);

            //lblSampleText.Font = savedFont;
            //lblSampleText.ForeColor = savedColor;
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            string filepath = textBox2.Text;
            Properties.Settings.Default.PlayConfigFilePath = filepath;
            Properties.Settings.Default.Save();
            String Title = txtTitle.Text;
            Properties.Settings.Default.TextTitle = Title;
            Properties.Settings.Default.Save();

            if (string.IsNullOrWhiteSpace(Title))
            {
                MessageBox.Show("Content name cannot be empty!");
                return;
            }

            // Set directory info
            DirectoryHelper.OutputFolderName = Title;
            DirectoryHelper.SelectedPlayconfigFilePath = filepath;


            GlobalProperties.PlayConfigPath = textBox2.Text;
            GlobalProperties.OutputFolderName = txtTitle.Text;
            GlobalProperties.OutputPath = DirectoryHelper.GetOutputDirectory(GlobalProperties.OutputFolderName);


            var analyzer = new PlayConfigAnalyzerService();
            var container = await analyzer.ReadAsync(filepath);

            //New process
            ProcessWidges(container);

            // Old process
            //FormNavigetor.Navigate(filepath, 0);
        }

        private void ProcessWidges(WidgetContainer container)
        {
            //Close this from
            this.Hide();

            var instance = GlobalConfigPropreties.Instance;

            instance.AddLine(container.Version);
            instance.AddLine(container.Widgets.Count.ToString());



            var formService = new FormService();

            foreach (var widget in container.Widgets)
            {
                int optionOrder = 1;
                foreach (var line in widget.Lines)
                {

                    if (line.WidgetType.IsWidget())
                    {
                        formService.OpenForm(widget, line);
                    }
                    else
                    { 
                        if (line.Type == LineTypeEnum.Option)
                        {
                            //process the options
                            var items = PatternHelper.GetItems(line.Text, @"<([^>]+)>");
                            string filepath=Properties.Settings.Default.PlayConfigFilePath;
                            var RightOrWorng = TextExtrator.ExtractRightOrWorngAns(filepath);

                            if (line.Text.StartsWith("<N><T>"))
                            {
                                //int offset = QuizProperties.Instance.CurrentOffset;
                                int offset = QuizProperties.Instance.CurrentOffset == 0 ? 50 : QuizProperties.Instance.CurrentOffset;
                                int length = QuizProperties.Instance.Options.FirstOrDefault(c => c.Order == optionOrder).Text.Length * 2;

                                items[2] = $"<{offset}:{length }>";

                                QuizProperties.Instance.CurrentOffset = offset + length;
                            }
                            else if (line.Text.StartsWith("<H><T>"))
                            {
                                int length = QuizProperties.Instance.Options.FirstOrDefault(c => c.Order == optionOrder).Text.Length * 2;
                                int offset = QuizProperties.Instance.CurrentOffset;

                                items[2] = $"<{offset}:{length}>";

                                QuizProperties.Instance.CurrentOffset = offset + length;
                                optionOrder++;
                            }
                            else if(line.Text.Length == 3)
                            {
                                
                                int order = QuizProperties.Instance.Options.First(c => c.IsCorrectAns).Order;

                                if (order == line.CorrectAnsIndicatorOrder)
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
                           
                            instance.AddLine(string.Join("", items));
                            continue;
                        }
                        instance.AddLine(line.Text);

                        _fileService.SaveLineFiles(line);
                    }
                }
            }

            instance.SavePlayConfig(Path.Combine(GlobalProperties.OutputPath, "PLAY_CONFIG.TXT"));

            if (QuizProperties.Instance.Options.Any() && container.Widgets.Any(w => w.Lines.Any(l => l.Type == LineTypeEnum.Text || l.Type == LineTypeEnum.Selection)))
            {
                var quizService = new QuizService();
                quizService.BuildTextData(QuizProperties.Instance, container.Widgets.First());

                var csvGenerator = new CsvGenerator();
                csvGenerator.UpdateCSVFile22(Path.Combine(GlobalProperties.OutputPath, "CONTENTINFO.CSV"), QuizProperties.Instance);
               
            }

            var contentNameGenerator = new ContentNameGeneratorService();
            contentNameGenerator.GenerateContentNameFile(txtTitle.Text, Path.Combine(GlobalProperties.OutputPath, "CONTENTNAME.TXT"));

            MessageBox.Show("Config generated.");
            instance.Clear();
            QuizProperties.Instance.Options.Clear();
            this.Show();
        }


        private void CopyQuizTemplateToOutput()
        {
            var destination = DirectoryHelper.GetOutputDirectory();
            var source = DirectoryHelper.GetPlayconfigDirectory();
            DirectoryHelper.QuizCopyFiles(source, destination);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Configuration Files|*.TXT|JPG|*.jpg";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Title = "Select an Image File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                textBox2.Text = selectedFilePath;
            }

        }

        private void SetupStyle_Click(object sender, EventArgs e)
        {
            SetupForCaption CaptionSetup = new SetupForCaption();
            this.Hide();
            CaptionSetup.ShowDialog();
            this.Close();
            SetupAniStyle.Enabled = true;

        }

        private void SetupAniStyle_Click(object sender, EventArgs e)
        {
            FontDialog AnifontDlg = new FontDialog();
            AnifontDlg.ShowColor = true;

            if (AnifontDlg.ShowDialog() == DialogResult.OK)
            {
                // _AnifontDlg = AnifontDlg;

                _selectedFont = AnifontDlg.Font;
                _selectedColor = AnifontDlg.Color;
                // Save properties to settings
                Properties.Settings.Default.AniFontName = _selectedFont.Name;
                Properties.Settings.Default.AniFontStyle = _selectedFont.Style.ToString();
                Properties.Settings.Default.AniFontSize = _selectedFont.Size;
                Properties.Settings.Default.AniFontColor = ColorTranslator.ToHtml(_selectedColor);
                Properties.Settings.Default.Save();
                // Apply font settings to a label or any control
                //lblSampleText.Font = _selectedFont;
                //lblSampleText.ForeColor = _selectedColor;

            }
            //txtTitle.Enabled = true;
            //button2.Enabled = true;
            //textBox2.Enabled = true;
            //button1.Enabled = true;
        }

        private void SetupForCaption_Click(object sender, EventArgs e)
        {
            CaptionSetup CaptionSetup = new CaptionSetup();
            this.Hide();
            CaptionSetup.ShowDialog();
            this.Close();
            AudioSetup.Enabled = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            //NewStartingForm newStaringForm = new NewStartingForm();
            //newStaringForm.Show();
            this.Close();
        }
    }
}
