using NAudio.Wave;
using System;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Enums;
using WindowsFormsApp1.Global;
using WindowsFormsApp1.Helper;
using WindowsFormsApp1.Helper.Quiz;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Models.Widgets;
using WindowsFormsApp1.Services;
using static System.Windows.Forms.LinkLabel;

namespace WindowsFormsApp1
{
    public partial class QuizForm : Form
    {
        private string _reason = string.Empty;
        private readonly string _filepath;
        private readonly Widget _widget;
        private readonly Line _line;
        private SoundPlayer soundPlayer;

        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private string audioFilePath;
        private string audionFileName;

        private bool IsAudioUploaded = false;

        public QuizForm(string reason)
        {
            InitializeComponent();
            _filepath = Properties.Settings.Default.PlayConfigFilePath;

            btnPlay.Enabled = false;
            btnPause.Enabled = false;
            btnNext.Enabled = false;

            this._reason = reason.Trim();

            if (reason == "question")
            {
                audioFilePath = Path.Combine(DirectoryHelper.GetOutputDirectory(), AudioInfo.QuizQuestionAudioFileName);
                LoadTextFromFile(_filepath);
            }
            else
            {
                audioFilePath = Path.Combine(DirectoryHelper.GetOutputDirectory(), AudioInfo.QuizCorrectAnswerAudioFileName);
                LoadCorrectAns(_filepath);
            }
        }

        public QuizForm(Widget widget, Line line)
        {
            _widget = widget;
            _line = line;
            _filepath = GlobalProperties.PlayConfigPath;
            InitializeComponent();

            #region Design settings
            
            btnNext.Enabled = false;
            btnPlay.Visible = false;
            btnPause.Visible = false; 

            #endregion

            if (line.IsQuestion && _widget.IsContainSelection)
            {
                audionFileName = AudioInfo.GetRandomFileName("Q-");
                audioFilePath = Path.Combine(GlobalProperties.OutputPath, audionFileName);
                LoadTextFromFile(_filepath);
            }
            else
            {
                audionFileName = AudioInfo.GetRandomFileName("QD-");
                audioFilePath = Path.Combine(GlobalProperties.OutputPath, audionFileName);
                LoadCorrectAns(_filepath);
            }

        }
        private void LoadCorrectAns(string filePath)
        {
            QuizQuesTxt.Text = TextExtrator.GetCorrectAns(filePath);
        }
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
                            string extractedText = cleanContent.Substring(startIndex + 1, length);
                            QuizQuesTxt.Text = extractedText;
                        }
                        else
                        {
                            QuizQuesTxt.Text = "No matching patterns found.";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading file: " + ex.Message);
            }
        }

        private async void btn_Generate(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Language;
            string voice = Properties.Settings.Default.Voice;
            btnPlay.Enabled = true;
            btnPause.Enabled = true;
            btnNext.Enabled = true;

            if (_reason == "question")
            {
                QuizProperties.Instance.Question = QuizQuesTxt.Text;
                if (IsAudioUploaded)
                {
                    ProcessUploadedAudio(uploadedAudioPath: AudioLocation.Text,
                      outputFilePath: audioFilePath);
                }
                else
                {
                    soundPlayer = await GenerateAudio(QuizQuesTxt.Text, lang, voice, AudioInfo.QuizQuestionAudioFileName);
                }
                //GenerateQuestionPlayConfig();
            }
            else
            {
                if (IsAudioUploaded)
                {
                    ProcessUploadedAudio(AudioLocation.Text, audioFilePath);
                }
                else
                    soundPlayer = await GenerateAudio(QuizQuesTxt.Text, lang, voice, audionFileName);

                GeneratePlayConfig();
            }


            InitializeAudio(audioFilePath);

            //TextData textData = GetTextData();
            //BuildTextData(textData);
            //await BuildPayConfig(textData);
            //UpdateCSVFile(Path.Combine(DirectoryHelper.GetOutputDirectory(), "CONTENTINFO.CSV"), textData);
        }

        private async void btn_Generate_V2(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Language;
            string voice = Properties.Settings.Default.Voice;
             

            if (_line.IsQuestion && _widget.IsContainSelection)
            {
                QuizProperties.Instance.Question = QuizQuesTxt.Text;
                if (IsAudioUploaded)
                {
                    ProcessUploadedAudio(uploadedAudioPath: AudioLocation.Text,
                      outputFilePath: audioFilePath);
                }
                else
                {

                    soundPlayer = await GenerateAudio(QuizQuesTxt.Text, lang, voice, audionFileName);
                }
                GenerateQuestionLine();
            }
            else
            {
                QuizProperties.Instance.CorrectAnsDetails = QuizQuesTxt.Text;
                if (IsAudioUploaded)
                {
                    ProcessUploadedAudio(AudioLocation.Text, audioFilePath);
                }
                else
                    soundPlayer = await GenerateAudio(QuizQuesTxt.Text, lang, voice, audionFileName);

                GenerateQuestionDetailLine();
            }


            InitializeAudio(audioFilePath);

            //TextData textData = GetTextData();
            //BuildTextData(textData);
            //await BuildPayConfig(textData);
            //UpdateCSVFile(Path.Combine(DirectoryHelper.GetOutputDirectory(), "CONTENTINFO.CSV"), textData);

            MessageBox.Show("Files generated successfully.");
           
            #region Design settings 

            btnGenerate.Enabled = false;
            btnNext.Enabled = true;
            btnPlay.Visible = true;
            btnPause.Visible = true;
            #endregion
        }

        private void ProcessUploadedAudio(string uploadedAudioPath, string outputFilePath)
        {
            File.Copy(uploadedAudioPath, outputFilePath, overwrite: true);
            soundPlayer = new SoundPlayer(outputFilePath);
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

        private void button3_Click(object sender, EventArgs e)
        {
            StartupForm firstForm = new StartupForm();
            this.Hide();
            firstForm.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media Files|*.mp3;*.mp4;*.wav|MP3 Files|*.mp3|MP4 Files|*.mp4|WAV Files|*.wav";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Title = "Select an Audio File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                IsAudioUploaded = true;
                string selectedFilePath = openFileDialog.FileName;
                AudioLocation.Text = selectedFilePath;

                //ProcessUploadedAudio(AudioLocation.Text, audioFilePath);

                soundPlayer = new SoundPlayer(AudioLocation.Text);
                btnPlay.Enabled = true;
                btnPause.Enabled = true;
            }
        }

        private void InitializeAudio(string filePath)
        {
            try
            {
                if (outputDevice == null)
                {
                    outputDevice = new WaveOutEvent();
                }
                if (audioFile != null)
                {
                    audioFile.Dispose();
                }
                audioFile = new AudioFileReader(filePath);
                outputDevice.Init(audioFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing audio: {ex.Message}");
            }
        }

        private void OnBtnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                string audioFilePath = GetAudioFilePath();
                if (outputDevice != null)
                {
                    OnPlaybackStopped(sender, e);
                }
                if (outputDevice == null)
                {
                    outputDevice = new WaveOutEvent();
                }
                if (audioFile == null)
                {
                    InitializeAudio(audioFilePath);
                }
                outputDevice.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to paly audio!");
            }
        }

        private string GetAudioFilePath()
        {
            if (IsAudioUploaded)
            {
                return AudioLocation.Text;
            }
            return audioFilePath;
        }

        private void OnPlaybackStopped(object sender, EventArgs e)
        {
            outputDevice?.Stop();
            outputDevice.Dispose();
            outputDevice = null;
            audioFile.Dispose();
            audioFile = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (_reason == "question")
            //{
            //    var nextForm = new SelectionForm();
            //    this.Hide();
            //    nextForm.ShowDialog();
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("Config generated");
            //}

            this.Hide();
        }

        private void GeneratePlayConfig()
        {
            string audioFileName = AudioInfo.QuizQuestionAudioFileName;
            int questionOffset = 50;
            int questionLength = QuizProperties.Instance.Question.Length;

            //Option
            int optioncount = QuizProperties.Instance.OptionsCount;
            int option01Skip = 50 + QuizProperties.Instance.Question.Length; //50+150
            int option02Skip = option01Skip + (QuizProperties.Instance.Options.FirstOrDefault(c => c.Order == 1).Text.Length * 2); //200 + 118 + 118
            int option03Skip = option02Skip + (QuizProperties.Instance.Options.FirstOrDefault(c => c.Order == 2).Text.Length * 2); //318 + 118

            //Correct answer
            int correctAnsSkip = option03Skip + (QuizProperties.Instance.Options.FirstOrDefault(c => c.Order == 3).Text.Length * 2);

            var builder = new QuizConfigBuilderV2(DirectoryHelper.GetOutputDirectory());
            builder.AddQuestionFileName(audioFileName)
                .AddQuestionOffset(questionOffset)
                .AddQuestionLength(questionLength)
                .AddSelectionCounts(optioncount)
                .AddSelectionLength_01(option01Skip, QuizProperties.Instance.Options.FirstOrDefault(c => c.Order == 1).Text)
                .AddSelectionLength_02(option02Skip, QuizProperties.Instance.Options.FirstOrDefault(c => c.Order == 2).Text)
                .AddSelectionLength_03(option03Skip, QuizProperties.Instance.Options.FirstOrDefault(c => c.Order == 3).Text)
                .AddCorrectAnsAudioTime("12")
                .AddCorrectAnsAudioFileName(AudioInfo.QuizCorrectAnswerAudioFileName)
                .AddCorrectAnsOffset(correctAnsSkip, QuizProperties.Instance.CorrectAns)
                .Build();
        }

        private void GenerateQuestionLine()
        {
            int questionOffset = 50;
            int questionLength = QuizProperties.Instance.Question.Length * 2;

            string text = $"<T><{questionOffset}:{questionLength}><0><33><240><97><16777088><0><0><0><0><0><4><3000><1><0>";

            QuizProperties.Instance.CurrentOffset = questionOffset + questionLength;

            GlobalConfigPropreties.Instance.AddLine(text);

            var mediaLine = _widget.Lines[4];

            var textFormService = new TextFormService();
            textFormService.GenerateMediaLines(mediaLine, audionFileName);


        }

        private void GenerateQuestionDetailLine()
        {   
           
            int questionLength = QuizProperties.Instance.CorrectAnsDetails.Length * 2;
             
            string text = $"<T><{QuizProperties.Instance.CurrentOffset }:{questionLength}><0><0><240><241><8454143><0><0><0><0><0><4><300><1><0>";

            GlobalConfigPropreties.Instance.AddLine(text);

            var mediaDuration = _widget.Lines[3];
            var media = _widget.Lines[4];

            var textFormService = new TextFormService();
            textFormService.GenerateMediaLines(mediaDuration,media, audionFileName);

        }
    }
}