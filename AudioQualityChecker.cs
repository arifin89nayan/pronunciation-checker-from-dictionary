using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        private async void AQCBTN_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                AQCBTN.Enabled = false;
                await AQCBTN_ClickCoreAsync(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Audio quality check failed: " + ex.Message);
            }
            finally
            {
                AQCBTN.Enabled = true;
            }
        }

        private async Task AQCBTN_ClickCoreAsync(object sender, EventArgs e)
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

            string azureKey = "7e3b899567c24c67adf484f14ea0b0e5";
            string azureRegion = "japaneast";
           //string openAiKey = "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";


            //var openAiService = new OpenAiTranscriptionService(openAiKey);
            var dictionaryService = new DictionaryVerifierService();
            var azureService = new AzurePronunciationAssessmentService(
                azureKey,
                azureRegion
            );

            var azureSttService = new AzureSpeechToTextService(
                azureKey,
                azureRegion
            );



            var engine = new AudioQualityEngine(
                azureService,
                azureSttService,
                dictionaryService
            );

            var result = await engine.CheckAsync(
                mp3Path,
                originalText,
                dictionaryXmlPath,
                "ja-JP"
            );



            txt_ConvertMessage.AppendText("Final Result: " + result.FinalGrade + "\r\n");
            txt_ConvertMessage.AppendText(result.Message + "\r\n");

            await Task.CompletedTask;
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
