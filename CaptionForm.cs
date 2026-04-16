using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Enums;
using WindowsFormsApp1.Global;
using WindowsFormsApp1.Helper;
using WindowsFormsApp1.Models.Widgets;
using WindowsFormsApp1.Services;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace WindowsFormsApp1
{
    public partial class CaptionForm : Form
    {
        public string ContentText;
        private bool IsAudioUploaded = false;
        private FontDialog _fontDlg = new FontDialog();
        private ColorDialog _ColorDlg = new ColorDialog();
        //private string _outputDirectory = @"D:\Iwate University japan\test\Rimon Work\OutPut";
        private string _outputDirectory = DirectoryHelper.GetOutputDirectory();
        private SoundPlayer soundPlayer;
        Bitmap bitmap = null;
        public string AudioName = $"3000{DateTime.Now:ss}.MP3";

        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;


        private readonly Widget _widget;
        private readonly Line _line;

        private readonly CaptionFormService _captionFormService;
        private readonly FileService _fileService;

        public CaptionForm()
        {
            IsAudioUploaded = false;
            InitializeComponent();

            var playConfigFilePath = GlobalProperties.PlayConfigPath;
            var scrollProperties = ImageExtrator.GetScrollProperties(playConfigFilePath);
            int scrollTimeStartValue, scrollTimeEndValue, scrollTimeIntervalValue;
            if (int.TryParse(scrollProperties.scrollTimeStart, out scrollTimeStartValue))
            {
                this.txtT1.Text = (scrollTimeStartValue * 10).ToString();
            }
            if (int.TryParse(scrollProperties.scrollTimeEnd, out scrollTimeEndValue))
            {
                this.txtTa.Text = (scrollTimeEndValue * 10).ToString();
            }
            if (int.TryParse(scrollProperties.scrollTimeInterval, out scrollTimeIntervalValue))
            {
                this.txtT3.Text = (scrollTimeIntervalValue * 10).ToString();
            }
        }

        public CaptionForm(Widget widget, Line line)
        {
            _widget = widget;
            _line = line;
            IsAudioUploaded = false;

            InitializeComponent();

            _captionFormService = new CaptionFormService();

            var scrollProperties = ImageExtrator.GetScrollProperties_V2(line.Text);
            int scrollTimeStartValue, scrollTimeEndValue, scrollTimeIntervalValue;

            if (int.TryParse(scrollProperties.scrollTimeStart, out scrollTimeStartValue))
            {
                this.txtT1.Text = (scrollTimeStartValue * 10).ToString();
            }
            if (int.TryParse(scrollProperties.scrollTimeEnd, out scrollTimeEndValue))
            {
                this.txtTa.Text = (scrollTimeEndValue * 10).ToString();
            }
            if (int.TryParse(scrollProperties.scrollTimeInterval, out scrollTimeIntervalValue))
            {
                this.txtT3.Text = (scrollTimeIntervalValue * 10).ToString();
            }

            _fileService = new FileService();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            StartupForm firstForm = new StartupForm();
            this.Hide();
            firstForm.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SetupForCaption audioSetup = new SetupForCaption();
            this.Hide();
            audioSetup.ShowDialog();
            this.Close();

        }
        private bool ConvertImage()
        {
            ContentText = TxtToImage.Text;
            if (string.IsNullOrWhiteSpace(ContentText))
            {
                return false;
            }

            if (bitmap != null)
            {
                bitmap.Dispose();
            }


            var textGenerateRequest = new TxtToImage
            {
                InputText = TxtToImage.Text,

            };
            var imageService = new imageService();
            var generatedImagePath = imageService.GenerateImage(textGenerateRequest);

            GlobalProperties.CaptionBaseImagePath = generatedImagePath;

            bitmap = new Bitmap(generatedImagePath);
            pictureBoxTextToImagePreview.Image = bitmap;

            return true;
        }

        private async Task ConvertAudio()
        {
            ContentText = TxtToImage.Text;
            string lang = Properties.Settings.Default.Language;
            string voice = Properties.Settings.Default.Voice;

            soundPlayer = await GenerateAudio(ContentText, lang, voice, AudioName);

            GlobalProperties.CaptionAudioPath = Path.Combine(GlobalProperties.OutputPath, AudioName);
            InitializeAudio(GlobalProperties.CaptionAudioPath);
            AppendAudioMessages(AudioInfo.GetAudioDuration(GlobalProperties.CaptionAudioPath), AudioInfo.AudioGenerateMessage);
        }

        private void AppendAudioMessages(string audioDuration, string audioGenerateMessage)
        {
            // Append messages to the RichTextBox
            MsgShow.AppendText($"Total Audio Duration {audioDuration}\n");
            MsgShow.AppendText(audioGenerateMessage + "\n");
            AudioDuration.Text = $"Audio Duration {audioDuration}\n";
        }
        private void ProcessUploadedAudio(string uploadedAudioPath)
        {
            string outputFilePath = Path.Combine(GlobalProperties.OutputPath, AudioName);

            GlobalProperties.CaptionAudioPath = outputFilePath;

            File.Copy(uploadedAudioPath, outputFilePath, overwrite: true);
            soundPlayer = new SoundPlayer(outputFilePath);


            InitializeAudio(outputFilePath);

            AppendAudioMessages(AudioInfo.GetAudioDuration(outputFilePath), AudioInfo.AudioGenerateMessage);
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

        private void AudioSetupOnGenerate(string audioFilePath)
        {
            InitializeAudio(audioFilePath);
        }
        private void OnBtnPlay_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GlobalProperties.CaptionAudioPath))
            {
                MessageBox.Show("Audio didn't generated.Please try again.");
                return;
            }


            if (outputDevice == null)
            {
                outputDevice = new WaveOutEvent();
            }
            if (audioFile == null)
            {
                InitializeAudio(GlobalProperties.CaptionAudioPath);
            }
            outputDevice.Play();

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
                //Geting Azur Audio Creation Erro
                audioFile = new AudioFileReader(filePath);
                outputDevice.Init(audioFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing audio: {ex.Message}");
            }
        }
        private void OnPlaybackStopped(object sender, EventArgs args)
        {

            if (outputDevice == null || audioFile == null)
            {
                return;
            }

            outputDevice?.Stop();
            outputDevice.Dispose();
            outputDevice = null;
            audioFile.Dispose();
            audioFile = null;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //Form1 firstForm = new Form1();
            //this.Hide();
            //firstForm.ShowDialog();
            this.Close();
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            ConvertImage();


            DirectoryHelper.CopyFiles(WigetConfig.GetImagePath(),
                DirectoryHelper.GetOutputDirectory(),
                (imagename) => { return WigetConfig.GetImages().Contains(imagename); });

            DirectoryHelper.CopyFiles(AudioInfo.GetAudioAnimationFilePath(),
                 DirectoryHelper.GetOutputDirectory(),
                (fileName) =>
                {
                    return isAnimationOrTitleAudio(fileName);
                });


            bool isAnimationOrTitleAudio(string fileName) =>
                fileName == AudioInfo.AudioAnimationFileName || fileName == AudioInfo.AudioFileName;


            var image = pictureBoxTextToImagePreview.Image;
            string outputPath = Path.Combine(DirectoryHelper.GetOutputDirectory(), WigetConfig.GetBaseImageName());


            if (IsAudioUploaded)
            {
                ProcessUploadedAudio(AudioLocation.Text);
            }
            else
                await ConvertAudio();

            string t1 = txtT1.Text;
            string ta = txtTa.Text;
            string t3 = txtT3.Text;

            var playConfigPath = Path.Combine(DirectoryHelper.GetOutputDirectory(), "PLAY_CONFIG.TXT");

            int numChunks = (int)Math.Ceiling((double)image.Height / 362);



            var builder = new ConfigSecondPartBuilder(playConfigPath);
            builder.AddSeenImages()
                .AddSeenImageNumber()
                .AddBaseImage()
                .AddBaseImageHeight(image.Height.ToString())
                .AddAudioFileName()
                .AddAudioDuration()
                .AddBaseSliceImageCount((numChunks).ToString())
                .AddT1(txtT1.Text)
                .AddT2(txtTa.Text)
                .AddT3(txtT3.Text)
                .SaveConfig();
        }
        private async void btnGenerate_Click_V2(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtToImage.Text))
            {
                MessageBox.Show("Please enter text.");
                return;
            }

            if (!ConvertImage())
            {
                MessageBox.Show("Failed to convert text to image.Please try again.");
                return;
            }

            var image = pictureBoxTextToImagePreview.Image;

            var mediaDuration = _widget.Lines[3];
            var media = _widget.Lines[4];
            
            if (media.Text.Contains("Audio") || mediaDuration.Text.Contains("Duration"))
            {
                if (IsAudioUploaded)
                    ProcessUploadedAudio(AudioLocation.Text);
                else
                    await ConvertAudio();
            }

            int numChunks = (int)Math.Ceiling((double)image.Height / 362);
            _captionFormService.GenerateMediaLines(mediaDuration, media, AudioName);

            var t1 = _captionFormService.ConvertToSecond(txtT1.Text);
            var t3 = _captionFormService.ConvertToSecond(txtT3.Text);
            var t2 = _captionFormService.CalculateT2(txtTa.Text, _widget.Lines[3], AudioName);
            var baseImageHeigt = (int)Math.Ceiling((double)image.Height);

            string baseFileName = Path.GetFileNameWithoutExtension(GlobalProperties.CaptionBaseImage);

            var text = _captionFormService.GetCalculatedLine(
                _line, 
                t1, t2,t3, 
                numChunks, 
                baseFileName,
                baseImageHeigt,
                isChangeT2: mediaDuration.Text.Contains("Duration"));
            GlobalConfigPropreties.Instance.AddLine(text);


           
            if (_line.Type == LineTypeEnum.Caption && _widget.IsContainSelection)
            {
                QuizProperties.Instance.Question = TxtToImage.Text;
              
            }
            //TODO : 2. Need to fix the correct answer details if the line is a truly caption line.
            else     
            {
                QuizProperties.Instance.CorrectAnsDetails = TxtToImage.Text;
               
            }


            MessageBox.Show("Files generated successfully.");
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BcgroundColor_Click(object sender, EventArgs e)
        {
            ColorDialog colDlg = new ColorDialog();  // Default background color
            colDlg.AllowFullOpen = false;
            colDlg.AnyColor = true;
            colDlg.SolidColorOnly = false;
            if (colDlg.ShowDialog() == DialogResult.OK)
            {
                _ColorDlg = colDlg;

            }
        }

        private void ClearMessege_Click(object sender, EventArgs e)
        {
            // Clear the RichTextBox
            MsgShow.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media Files|*.mp3;*.mp4|MP3 Files|*.mp3|MP4 Files|*.mp4";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Title = "Select an Audio File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                IsAudioUploaded = true;
                string selectedFilePath = openFileDialog.FileName;
                AudioLocation.Text = selectedFilePath;
            }
        }

 
    }
}
