using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1
{
    public partial class AudioQualityChecker : Form
    {
        public AudioQualityChecker()
        {
            InitializeComponent();
        }

        private void Back_button_Click(object sender, EventArgs e)
        {
            NewStartingForm newStart = new NewStartingForm();
            newStart.Show();
            this.Hide();
        }

        // IMPORTANT:
        // WinForms event handler must be void / async void.
        // Do not use Task as direct button event return type.
        private async void AQCBTN_Click(object sender, EventArgs e)
        {
            try
            {
                AQCBTN.Enabled = false;
                Cursor = Cursors.WaitCursor;

                await AQCBTN_ClickCoreAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Audio quality check failed: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                Cursor = Cursors.Default;
                AQCBTN.Enabled = true;
            }
        }

        private async Task AQCBTN_ClickCoreAsync()
        {
            txt_ConvertMessage.Clear();

            string mp3Path = txt_AudioSelect.Text.Trim();
            string originalText = txt_OriginalText.Text.Trim();
            string dictionaryXmlPath = txt_XMLFile.Text.Trim();

            if (!File.Exists(mp3Path))
            {
                MessageBox.Show("Please select a valid MP3 file.");
                return;
            }

            if (string.IsNullOrWhiteSpace(originalText))
            {
                MessageBox.Show("Please enter original text.");
                return;
            }

            if (!File.Exists(dictionaryXmlPath))
            {
                MessageBox.Show("Please select a valid dictionary XML file.");
                return;
            }

            txt_ConvertMessage.AppendText("Starting audio quality check...\r\n");
            txt_ConvertMessage.AppendText("Whisper transcription is running. Please wait...\r\n");

            string modelPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Models",
                "ggml-large-v3-turbo.bin"
            );
            if (!File.Exists(modelPath))
            {
                MessageBox.Show(
                    "Whisper model file not found.\r\n\r\n" +
                    "Please put the model file here:\r\n" +
                    modelPath,
                    "Missing Whisper Model",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

                var whisperService = new WhisperNetSpeechToTextService(modelPath);

            var engine = new AudioQualityEngine(whisperService);

            AudioQualityFinalResult result = await Task.Run(async () =>
            {
                return await engine.CheckAsync(
                    mp3Path,
                    originalText,
                    dictionaryXmlPath,
                    "ja-JP"
                );
            });

            txt_ConvertMessage.AppendText("\r\nFinal Result: " + result.FinalGrade + "\r\n");
            txt_ConvertMessage.AppendText(result.Message + "\r\n");

            txt_ConvertMessage.AppendText("\r\nCER: " + result.CerPercent.ToString("0.00") + "%\r\n");

            txt_ConvertMessage.AppendText("\r\n--- Whisper Recognized Text ---\r\n");
            txt_ConvertMessage.AppendText(result.RecognizedText + "\r\n");
        }

        private void AudioSelect_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "MP3 Files (*.mp3)|*.mp3|All Files (*.*)|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                    txt_AudioSelect.Text = dlg.FileName;
            }
        }

        private void XmlFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                    txt_XMLFile.Text = dlg.FileName;
            }
        }
    }
}