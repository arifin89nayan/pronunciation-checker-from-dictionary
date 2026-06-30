using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApp1.UIDesign
{
    public partial class TtsResultPreview : Form
    {
        private readonly string _originalScript;
        private readonly TtsPipelineResult _result;

        private string _lastAudioPath;
        private SoundPlayer _player;

        // Standard ja-JP neural voices (reliably support <sub> alias control).
        private static readonly string[] JapaneseVoices =
        {
            "ja-JP-NanamiNeural",
            "ja-JP-KeitaNeural",
            "ja-JP-AoiNeural",
            "ja-JP-DaichiNeural",
            "ja-JP-MayuNeural",
            "ja-JP-NaokiNeural",
            "ja-JP-ShioriNeural"
        };

        // Active-tab colors
        private static readonly Color ActiveTab = Color.FromArgb(255, 128, 0);
        private static readonly Color InactiveTab = Color.FromArgb(70, 76, 92);

        public TtsResultPreview(string originalScript, TtsPipelineResult result)
        {
            InitializeComponent();

            _originalScript = originalScript;
            _result = result;

            BuildTtsColumns();
            PopulateVoices();
            UpdateSummary();
            ShowTtsList();
        }

        // -----------------------------------------------------------------
        // Voice selection
        // -----------------------------------------------------------------

        private void PopulateVoices()
        {
            cmbVoice.Items.Clear();
            foreach (var v in JapaneseVoices)
                cmbVoice.Items.Add(v);

            // Default to whatever voice the SSML was built with; else Nanami.
            string current = ExtractVoiceFromSsml(_result?.Ssml);
            int idx = string.IsNullOrEmpty(current) ? -1 : cmbVoice.Items.IndexOf(current);

            if (idx < 0 && !string.IsNullOrEmpty(current))
            {
                // SSML uses a voice not in our standard list; add it so it stays selectable.
                cmbVoice.Items.Insert(0, current);
                idx = 0;
            }

            cmbVoice.SelectedIndex = idx >= 0 ? idx : 0;
        }

        private static string ExtractVoiceFromSsml(string ssml)
        {
            if (string.IsNullOrWhiteSpace(ssml))
                return null;

            Match m = Regex.Match(ssml, "<voice\\s+name=\"([^\"]*)\"", RegexOptions.IgnoreCase);
            return m.Success ? m.Groups[1].Value : null;
        }

        private string SelectedVoice
        {
            get { return cmbVoice.SelectedItem as string ?? JapaneseVoices[0]; }
        }

        // Returns the SSML with the <voice name="..."> swapped to the selected voice.
        private string SsmlWithSelectedVoice()
        {
            if (_result == null || string.IsNullOrWhiteSpace(_result.Ssml))
                return _result?.Ssml;

            return Regex.Replace(
                _result.Ssml,
                "(<voice\\s+name=\")([^\"]*)(\")",
                "${1}" + SelectedVoice + "$3",
                RegexOptions.IgnoreCase);
        }

        private void cmbVoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If the SSML view is open, refresh it so the preview matches what will be sent.
            if (txtSsml.Visible)
                txtSsml.Text = SsmlWithSelectedVoice() ?? "No SSML generated.";

            lblStatus.ForeColor = Color.FromArgb(80, 86, 96);
            lblStatus.Text = "Voice set to " + SelectedVoice + ".";
        }

        // -----------------------------------------------------------------
        // View switching
        // -----------------------------------------------------------------

        private void btnTtsList_Click(object sender, EventArgs e)
        {
            ShowTtsList();
        }

        private void btnSsmlPreview_Click(object sender, EventArgs e)
        {
            ShowSsmlPreview();
        }

        private void ShowTtsList()
        {
            SetActiveTab(isList: true);

            dgvTtsList.Visible = true;
            txtSsml.Visible = false;
            chkWordWrap.Visible = false;
            btnCopySsml.Visible = false;

            LoadTtsGrid();
        }

        private void ShowSsmlPreview()
        {
            SetActiveTab(isList: false);

            dgvTtsList.Visible = false;
            txtSsml.Visible = true;
            chkWordWrap.Visible = true;
            btnCopySsml.Visible = true;

            txtSsml.WordWrap = chkWordWrap.Checked;
            txtSsml.Text = (_result == null || string.IsNullOrWhiteSpace(_result.Ssml))
                ? "No SSML generated."
                : SsmlWithSelectedVoice();
        }

        private void SetActiveTab(bool isList)
        {
            btnTtsList.BackColor = isList ? ActiveTab : InactiveTab;
            btnTtsList.ForeColor = isList ? Color.Black : Color.White;

            btnSsmlPreview.BackColor = isList ? InactiveTab : ActiveTab;
            btnSsmlPreview.ForeColor = isList ? Color.White : Color.Black;
        }

        // -----------------------------------------------------------------
        // TTS list grid
        // -----------------------------------------------------------------

        private void BuildTtsColumns()
        {
            dgvTtsList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTtsList.EnableHeadersVisualStyles = false;
            dgvTtsList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(38, 42, 54);
            dgvTtsList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTtsList.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            dgvTtsList.DefaultCellStyle.Font = new Font("Segoe UI", 12F);
            dgvTtsList.RowTemplate.Height = 36;

            dgvTtsList.Columns.Add("no", "No");
            dgvTtsList.Columns.Add("word", "Word");
            dgvTtsList.Columns.Add("hiragana", "Hiragana");
            dgvTtsList.Columns.Add("source", "Source");

            dgvTtsList.Columns["no"].FillWeight = 12;
            dgvTtsList.Columns["word"].FillWeight = 30;
            dgvTtsList.Columns["hiragana"].FillWeight = 38;
            dgvTtsList.Columns["source"].FillWeight = 20;
        }

        private void LoadTtsGrid()
        {
            dgvTtsList.Rows.Clear();

            if (_result == null || _result.FinalTtsList == null)
                return;

            int no = 1;
            foreach (var item in _result.FinalTtsList)
            {
                int idx = dgvTtsList.Rows.Add(no, item.Word, item.Hiragana, item.Source);

                if (string.Equals(item.Source, "Fixed", StringComparison.OrdinalIgnoreCase))
                    dgvTtsList.Rows[idx].DefaultCellStyle.BackColor = Color.Honeydew;
                else
                    dgvTtsList.Rows[idx].DefaultCellStyle.BackColor = Color.LemonChiffon;

                no++;
            }
        }

        private void UpdateSummary()
        {
            if (_result == null)
                return;

            lblFixedCount.Text = "Fixed: " + (_result.FixedList?.Count ?? 0);
            lblGeneralCount.Text = "General: " + (_result.GeneralList?.Count ?? 0);
            lblFinalCount.Text = "Final TTS: " + (_result.FinalTtsList?.Count ?? 0);
        }

        // -----------------------------------------------------------------
        // SSML helpers
        // -----------------------------------------------------------------

        private void chkWordWrap_CheckedChanged(object sender, EventArgs e)
        {
            txtSsml.WordWrap = chkWordWrap.Checked;
        }

        private void btnCopySsml_Click(object sender, EventArgs e)
        {
            if (_result != null && !string.IsNullOrWhiteSpace(_result.Ssml))
            {
                Clipboard.SetText(SsmlWithSelectedVoice());
                lblStatus.Text = "SSML copied to clipboard.";
            }
        }

        // -----------------------------------------------------------------
        // Audio generation
        // -----------------------------------------------------------------

        private async void btnGenerateAudio_Click(object sender, EventArgs e)
        {
            if (_result == null || string.IsNullOrWhiteSpace(_result.Ssml))
            {
                MessageBox.Show(
                    "SSML is empty. Please generate SSML first.",
                    "Missing SSML",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                StopPlayback();

                btnGenerateAudio.Enabled = false;
                btnPlay.Enabled = false;
                btnStop.Enabled = false;
                btnOpenFolder.Enabled = false;

                progress.Visible = true;
                lblStatus.ForeColor = Color.FromArgb(80, 86, 96);
                lblStatus.Text = "Generating audio with " + SelectedVoice + "...";

                string outputFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "TTSAgent",
                    "Audio");

                string voiceTag = SelectedVoice.Replace("ja-JP-", "").Replace("Neural", "");
                string outputFileName =
                    "tts_" + voiceTag + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".wav";

                var service = new AzureTtsSimpleService();

                _lastAudioPath = await service.GenerateAudioAsync(
                    SsmlWithSelectedVoice(), outputFolder, outputFileName);

                progress.Visible = false;
                lblStatus.ForeColor = Color.FromArgb(34, 139, 34);
                lblStatus.Text = "Done: " + _lastAudioPath;

                btnPlay.Enabled = true;
                btnOpenFolder.Enabled = true;
            }
            catch (Exception ex)
            {
                progress.Visible = false;
                lblStatus.ForeColor = Color.FromArgb(220, 53, 69);
                lblStatus.Text = "Error: " + ex.Message;

                MessageBox.Show(
                    ex.Message,
                    "Azure TTS Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnGenerateAudio.Enabled = true;
            }
        }

        // -----------------------------------------------------------------
        // Playback (in-app, using SoundPlayer for WAV)
        // -----------------------------------------------------------------

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_lastAudioPath) || !File.Exists(_lastAudioPath))
            {
                lblStatus.Text = "No audio file to play.";
                return;
            }

            try
            {
                _player?.Dispose();
                _player = new SoundPlayer(_lastAudioPath);
                _player.Play();

                btnStop.Enabled = true;
                lblStatus.ForeColor = Color.FromArgb(40, 167, 69);
                lblStatus.Text = "Playing...";
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = Color.FromArgb(220, 53, 69);
                lblStatus.Text = "Playback error: " + ex.Message;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopPlayback();
            lblStatus.ForeColor = Color.FromArgb(80, 86, 96);
            lblStatus.Text = "Stopped.";
        }

        private void StopPlayback()
        {
            try
            {
                _player?.Stop();
            }
            catch { /* ignore */ }

            btnStop.Enabled = false;
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_lastAudioPath) && File.Exists(_lastAudioPath))
                Process.Start("explorer.exe", "/select,\"" + _lastAudioPath + "\"");
        }

        // -----------------------------------------------------------------
        // Close
        // -----------------------------------------------------------------

        private void btnClose_Click(object sender, EventArgs e)
        {
            StopPlayback();
            _player?.Dispose();
            this.Close();
        }
    }
}