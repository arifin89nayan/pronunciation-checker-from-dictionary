using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTSAgent.Core;

namespace TTSAgent.UI.Screens
{
    public partial class AzureTtsScreen : ScreenBase
    {
        public AzureTtsScreen(AppState state) : base(state)
        {
            InitializeComponent();
            Ui.StylePrimaryButton(btnGenerateSsml);
            Ui.StyleSecondaryButton(btnValidate);
            Ui.StyleSecondaryButton(btnSynthesize);
            Ui.StyleSecondaryButton(btnSaveSsml);
            Ui.StyleSecondaryButton(btnVoiceCheck);
            styleComboBox.SelectedIndex = 0;

            btnGenerateSsml.Click += (_, _) => GenerateSsml();
            btnValidate.Click += (_, _) => ValidateSsml();
            btnSynthesize.Click += async (_, _) => await SynthesizeAsync();
            btnSaveSsml.Click += (_, _) => SaveSsml();
            btnVoiceCheck.Click += (_, _) => NavigateTo("voice");
        }

        public override void RefreshScreen()
        {
            if (!string.IsNullOrWhiteSpace(State.LastSsml) && string.IsNullOrWhiteSpace(ssmlTextBox.Text))
                ssmlTextBox.Text = State.LastSsml;
            outputLabel.Text = string.IsNullOrWhiteSpace(State.LastAudioPath) ? "No audio output." : State.LastAudioPath;
        }

        private void GenerateSsml()
        {
            try
            {
                if (State.FinalTtsList.Count == 0)
                {
                    ShowInfo("TTS list is empty. Please generate the final TTS list first.");
                    NavigateTo("ttslist");
                    return;
                }

                int rate = (int)rateNumeric.Value;
                if (State.Speed == "Slow") rate -= 15;
                if (State.Speed == "Fast") rate += 15;

                string style = Convert.ToString(styleComboBox.SelectedItem) ?? "narration";
                State.LastSsml = State.Tts.BuildSsml(State.Script, State.FinalTtsList, State.Voice, style, rate, (int)pitchNumeric.Value);
                ssmlTextBox.Text = State.LastSsml;
                State.NotifyChanged();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void ValidateSsml()
        {
            var result = State.Tts.ValidateSsml(ssmlTextBox.Text);
            ShowInfo(result.message);
        }

        private async Task SynthesizeAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ssmlTextBox.Text)) GenerateSsml();
                if (string.IsNullOrWhiteSpace(ssmlTextBox.Text)) return;

                btnSynthesize.Enabled = false;
                State.LastSsml = ssmlTextBox.Text;
                string file = $"{SafeFileName(State.ProjectName)}_{DateTime.Now:yyyyMMdd_HHmmss}.wav";
                State.LastAudioPath = await State.Tts.SynthesizeAsync(State.LastSsml, file);
                outputLabel.Text = State.LastAudioPath;
                State.NotifyChanged();
                ShowInfo("Audio generated: " + State.LastAudioPath);
            }
            catch (Exception ex) { ShowError(ex); }
            finally { btnSynthesize.Enabled = true; }
        }

        private void SaveSsml()
        {
            try
            {
                Directory.CreateDirectory(State.Config.OutputFolder);
                string path = Path.Combine(State.Config.OutputFolder, $"{SafeFileName(State.ProjectName)}_{DateTime.Now:yyyyMMdd_HHmmss}.ssml.xml");
                File.WriteAllText(path, ssmlTextBox.Text);
                ShowInfo("Saved: " + path);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private static string SafeFileName(string text)
        {
            foreach (char c in Path.GetInvalidFileNameChars()) text = text.Replace(c, '_');
            return string.IsNullOrWhiteSpace(text) ? "tts_audio" : text;
        }
    }
}
