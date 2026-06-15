using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.UIDesign.Screens
{
    public partial class VoiceCheckScreen : UserControl, IScreen
    {
        private readonly AppState _state;
        private SoundPlayer _player;

        public VoiceCheckScreen() { InitializeComponent(); }

        public VoiceCheckScreen(AppState state) : this()
        {
            _state = state;
            Theme.StyleGrid(dgvChecks);
            dgvChecks.Columns.Add("type", "Check Type");
            dgvChecks.Columns.Add("result", "Result");
            dgvChecks.Columns.Add("score", "Score");
            dgvChecks.Columns.Add("details", "Details");
        }

        public void OnShown()
        {
            lblFile.Text = "Audio File: " + (string.IsNullOrEmpty(_state.LastAudioPath)
                ? "(none — generate on Azure TTS screen)" : _state.LastAudioPath);
            txtOriginal.Text = _state.Script;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_state.LastAudioPath) || !File.Exists(_state.LastAudioPath)) { Info("No audio file yet."); return; }
            _player?.Dispose();
            _player = new SoundPlayer(_state.LastAudioPath);
            _player.Play();
        }

        private void btnStop_Click(object sender, EventArgs e) => _player?.Stop();

        private async void btnRun_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_state.LastAudioPath) || !File.Exists(_state.LastAudioPath))
            { Info("Generate audio first (Azure TTS screen)."); return; }
            try
            {
                Cursor = Cursors.WaitCursor; lblStatus.Text = "Transcribing and comparing…";
                var dictWords = _state.FinalTtsList.Select(r => r.Word);
                (string recognized, List<QualityCheckRow> rows) = await _state.Quality.RunAsync(_state.LastAudioPath, _state.Script, dictWords);

                txtRecognized.Text = recognized;
                dgvChecks.Rows.Clear();
                foreach (var r in rows)
                {
                    int idx = dgvChecks.Rows.Add(r.CheckType, r.Result, r.Score, r.Details);
                    Color backColor;
                    if (r.Result == "Pass")
                        backColor = Color.Honeydew;
                    else if (r.Result == "Warning")
                        backColor = Color.LemonChiffon;
                    else
                        backColor = Color.MistyRose;
                    dgvChecks.Rows[idx].DefaultCellStyle.BackColor = backColor;
                }
                lblStatus.Text = rows.Any(r => r.Result == "Fail") ? "Some checks failed — review the script/readings."
                    : rows.Any(r => r.Result == "Warning") ? "Passed with warnings — review pronunciation."
                    : "All checks passed. Ready to publish.";
            }
            catch (Exception ex) { lblStatus.Text = "Quality check failed."; MessageBox.Show(ex.Message, "Voice Check"); }
            finally { Cursor = Cursors.Default; }
        }

        private void Info(string m) => MessageBox.Show(m, "Voice Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
