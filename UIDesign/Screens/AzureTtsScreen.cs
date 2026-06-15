using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.UIDesign.Screens
{
    public partial class AzureTtsScreen : UserControl, IScreen
    {
        private readonly AppState _state;
        private readonly Action<string> _nav;

        public AzureTtsScreen() { InitializeComponent(); }

        public AzureTtsScreen(AppState state, Action<string> nav) : this()
        {
            _state = state; _nav = nav;

            cmbVoice.Items.AddRange(new object[]
            { "ja-JP-NanamiNeural", "ja-JP-AoiNeural", "ja-JP-MayuNeural", "ja-JP-KeitaNeural", "ja-JP-DaichiNeural" });
            cmbVoice.SelectedIndex = 0;
            cmbStyle.Items.AddRange(new object[] { "narration-relaxed", "narration", "chat", "cheerful", "calm", "none" });
            cmbStyle.SelectedIndex = 1;
        }

        public void OnShown()
        {
            if (cmbVoice.Items.Contains(_state.Voice)) cmbVoice.SelectedItem = _state.Voice;
            txtOutName.Text = _state.ProjectName + ".wav";
            if (_state.FinalTtsList.Count > 0 && string.IsNullOrEmpty(txtSsml.Text)) GenerateSsml();
        }

        private void btnGenSsml_Click(object sender, EventArgs e) => GenerateSsml();

        private void GenerateSsml()
        {
            if (string.IsNullOrWhiteSpace(_state.Script)) { lblStatus.Text = "No script — go to Script Input."; return; }
            _state.Voice = cmbVoice.SelectedItem?.ToString() ?? "ja-JP-NanamiNeural";
            _state.LastSsml = _state.Tts.BuildSsml(
                _state.Script, _state.FinalTtsList, _state.Voice,
                cmbStyle.SelectedItem?.ToString() ?? "narration",
                (int)numRate.Value, (int)numPitch.Value);
            txtSsml.Text = _state.LastSsml;
            lblStatus.Text = $"SSML generated · {_state.FinalTtsList.Count} reading tag(s) injected.";
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSsml.Text)) GenerateSsml();
            var (ok, msg) = _state.Tts.ValidateSsml(txtSsml.Text);
            lblStatus.Text = msg;
            MessageBox.Show(msg, "Validate SSML", MessageBoxButtons.OK,
                ok ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSsml.Text)) GenerateSsml();
            var (ok, msg) = _state.Tts.ValidateSsml(txtSsml.Text);
            if (!ok) { MessageBox.Show(msg, "Invalid SSML"); return; }
            try
            {
                Cursor = Cursors.WaitCursor; lblStatus.Text = "Synthesizing with Azure TTS…";
                string name = string.IsNullOrWhiteSpace(txtOutName.Text) ? _state.ProjectName + ".wav" : txtOutName.Text.Trim();
                _state.LastAudioPath = await _state.Tts.SynthesizeAsync(txtSsml.Text, name);
                lblStatus.Text = "Audio generated: " + _state.LastAudioPath;
                _nav("Voice Check");
            }
            catch (Exception ex) { lblStatus.Text = "Synthesis failed."; MessageBox.Show(ex.Message, "Azure TTS"); }
            finally { Cursor = Cursors.Default; }
        }
    }
}
