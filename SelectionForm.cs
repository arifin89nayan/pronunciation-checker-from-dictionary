using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
    public partial class SelectionForm : Form
    {
        private readonly string _filepath;
        private readonly QuizService _quizService;
        private readonly Widget _widget;
        private readonly Line _line;
        private RichTextBox[] _optionTextBoxes;
        private RadioButton[] _optionRadioButtons;
        public SelectionForm()
        {
            _quizService = new QuizService();
            InitializeComponent();
            //Order is important
            InitializeOptionArray();
            string filepath = Properties.Settings.Default.PlayConfigFilePath;
            _filepath = filepath;
            LoadTextFromFile(_filepath);

        }

        public SelectionForm(Widget widget, Line line)
        {
            _quizService = new QuizService();
            InitializeComponent();
            _filepath = GlobalProperties.PlayConfigPath;
            _widget = widget;
            _line = line;
            LoadTextFromFileV2(widget.FullText, GlobalProperties.PlayConfigPath);
        }

        private void InitializeOptionArray()
        {
            _optionTextBoxes = new RichTextBox[] { option1, option2, option3, option4 };

            _optionRadioButtons = new RadioButton[] { correctAnsOpt1, correctAnsOpt2, correctAnsOpt3, correctAnsOpt4 };
        }
        private void LoadTextFromFile(string filepath)
        {

            string directoryFilePath = filepath;
            string textDataPath = Path.Combine(Path.GetDirectoryName(directoryFilePath), "TEXTDATA.TXT");

            try
            {
                byte[] fileBytes = File.ReadAllBytes(textDataPath);
                string fileContent = System.Text.Encoding.Unicode.GetString(fileBytes);

                string cleanContent = fileContent.Replace("\0", "");
                var OffsetLenList = TextExtrator.GetOffset_Lengths(filepath);

                int optionIndex = 0;
                foreach (var optionConfig in OffsetLenList.Skip(1).Take(4))
                {
                    int startIndex = optionConfig.Offset + 1;
                    int length = optionConfig.Length;

                    string option = cleanContent.Substring(startIndex, length);

                    _optionTextBoxes[optionIndex].Text = option;
                    optionIndex++;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading file: " + ex.Message);
            }
        }
        private void LoadTextFromFileV2(string widgetText, string directoryFilePath)
        {

            InitializeOptionArray();
            string textDataPath = Path.Combine(Path.GetDirectoryName(directoryFilePath), "TEXTDATA.TXT");

            try
            {

                var selections = TextExtrator.GetSelectionMetadata(_widget, textDataPath);

                int optionIndex = 0;
                foreach (var optionConfig in selections)
                {
                    _optionTextBoxes[optionIndex].Text = optionConfig.Text;
                    optionIndex++;
                }

                for (int i = 0; i < _optionTextBoxes.Length; i++)
                {
                    if (i >= selections.Count)
                    {
                        _optionTextBoxes[i].Visible = false;
                        _optionRadioButtons[i].Visible = false;
                    }
                    else
                    {
                        _optionTextBoxes[i].Visible = true;
                        _optionRadioButtons[i].Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading file: " + ex.Message);
            }
        }


        private async void btn_GenerateClick(object sender, EventArgs e)
        {
            if (!IsCorrectAnsSelected())
            {
                MessageBox.Show("Please select any option");
                return;
            }

            TextData textData = GetTextData();
            var options = new List<Option>();
            if (!string.IsNullOrEmpty(option1.Text))
            {
                options.Add(new Option
                {
                    Text = option1.Text,
                    IsCorrectAns = correctAnsOpt1.Checked,
                    Order = 1
                });
            }
            if (!string.IsNullOrEmpty(option2.Text))
            {
                options.Add(new Option
                {
                    Text = option2.Text,
                    IsCorrectAns = correctAnsOpt2.Checked,
                    Order = 2
                });
            }
            if (!string.IsNullOrEmpty(option3.Text))
            {
                options.Add(new Option
                {
                    Text = option3.Text,
                    IsCorrectAns = correctAnsOpt3.Checked,
                    Order = 3
                });
            }
            if (!string.IsNullOrEmpty(option4.Text))
            {
                options.Add(new Option
                {
                    Text = option4.Text,
                    IsCorrectAns = correctAnsOpt4.Checked,
                    Order = 4
                });
            }
            // Now add all to QuizProperties
            QuizProperties.Instance.Options.AddRange(options);

            //var options = new List<Option>
            //{
            //    new Option
            //    {
            //        Text = option1.Text,
            //        IsCorrectAns = correctAnsOpt1.Checked,
            //        Order = 1
            //    }
            //};

            //QuizProperties.Instance.Options.AddRange(options);

            //if (!string.IsNullOrEmpty(option2.Text))
            //{
            //    QuizProperties.Instance.Options.Add(new Option
            //    {
            //        Text = option2.Text,
            //        IsCorrectAns = correctAnsOpt2.Checked,
            //        Order = 2
            //    });
            //}

            //if (!string.IsNullOrEmpty(option3.Text))
            //{
            //    QuizProperties.Instance.Options.Add(new Option
            //    {
            //        Text = option3.Text,
            //        IsCorrectAns = correctAnsOpt3.Checked,
            //        Order = 3
            //    });
            //}

            //if (!string.IsNullOrEmpty(option4.Text))
            //{
            //    QuizProperties.Instance.Options.Add(new Option
            //    {
            //        Text = option4.Text,
            //        IsCorrectAns = correctAnsOpt4.Checked,
            //        Order = 4
            //    });
            //}

            string text = $"<S><0><0><240><320> <{options.Count}> <>";
            GlobalConfigPropreties.Instance.AddLine(text);

            MessageBox.Show("Files generated successfully.");

            var quizService = new QuizService();
            var csvGenerator = new CsvGenerator();

            //quizService.BuildTextData(textData);
            //await BuildPayConfig(textData);
            //csvGenerator.UpdateCSVFile(Path.Combine(GlobalProperties.OutputPath,"CONTENTINFO.CSV"), textData);
        }



        private bool IsCorrectAnsSelected()
        {
            if (correctAnsOpt1.Checked)
            {
                return true;
            }
            else if (correctAnsOpt2.Checked)
            {
                return true;
            }
            else if (correctAnsOpt3.Checked)
            {
                return true;
            }
            else if (correctAnsOpt4.Checked)
            {
                return true;
            }
            return false;
        }

        private TextData GetTextData()
        {
            var textData = new TextData();
            textData.Question = QuizProperties.Instance.Question;
            textData.Option1 = option1.Text;
            textData.Option2 = option2.Text;
            textData.Option3 = option3.Text;
            textData.Option4 = option4.Text;

            if (correctAnsOpt1.Checked)
            {
                textData.CorrectAnswer = option1.Text;
            }
            else if (correctAnsOpt2.Checked)
            {
                textData.CorrectAnswer = option2.Text;
            }
            else if (correctAnsOpt3.Checked)
            {
                textData.CorrectAnswer = option3.Text;
            }
            else if (correctAnsOpt4.Checked)
            {
                textData.CorrectAnswer = option4.Text;
            }

            return textData;
        }

        //Depricated    
        private async Task<bool> BuildPayConfig(TextData textData)
        {
            string selectedValue = Properties.Settings.Default.Language;
            string lang = selectedValue;
            string selectedValue2 = Properties.Settings.Default.Voice;
            string voice = selectedValue2;

            await GenerateAudio(textData.Question, lang, voice, "0000014AE.MP3");
            await GenerateAudio(textData.CorrectAnswer, lang, voice, "00000C4AE.MP3");

            int option01Skip = textData.Key.Length + textData.Question.Length; //100
            int option02Skip = option01Skip + (textData.Option1.Length * 2); //100 + 118
            int option03Skip = option02Skip + (textData.Option2.Length * 2); //305

            var builder = new QuizConfigBuilder(DirectoryHelper.GetOutputDirectory());
            builder.AddWizedLength(textData.Key, textData.Question)
                .AddSelectionCounts(textData.NumberOfOptions)
                .AddSelectionLength_01(option01Skip, textData.Option1)
                .AddSelectionLength_02(option02Skip, textData.Option2)
                .AddSelectionLength_03(option03Skip, textData.Option3)
                .AddCorrectAnswerLength_03(textData.Key.Length + textData.Question.Length + (textData.Option1.Length * 2) + (textData.Option2.Length * 2) + (textData.Option3.Length * 2), textData.CorrectAnswer)
                .Build();




            return true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            StartupForm firstForm = new StartupForm();
            this.Hide();
            firstForm.ShowDialog();
            this.Close();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            //TextForm firstForm = new TextForm("correct_answer");
            //this.Hide();
            //firstForm.ShowDialog();
            //this.Close();

            this.Close();
        }

        private async Task<SoundPlayer> GenerateAudio(string text, string Language, string Voice, string fileName)
        {
            string outputFilePath = Path.Combine(DirectoryHelper.GetOutputDirectory(), fileName);

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
    }
}