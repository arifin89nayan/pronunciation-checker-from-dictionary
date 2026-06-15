using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTSAgent.Core;

namespace TTSAgent.UI.Screens
{
    public partial class VoiceCheckScreen : ScreenBase
    {
        public VoiceCheckScreen(AppState state) : base(state)
        {
            InitializeComponent();
            Ui.StyleGrid(resultGrid);
            Ui.StyleSecondaryButton(btnBrowse);
            Ui.StyleSecondaryButton(btnPlay);
            Ui.StylePrimaryButton(btnRunCheck);
            Ui.StyleSecondaryButton(btnOpenFolder);

            btnBrowse.Click += (_, _) => BrowseAudio();
            btnPlay.Click += (_, _) => PlayAudio();
            btnRunCheck.Click += async (_, _) => await RunCheckAsync();
            btnOpenFolder.Click += (_, _) => OpenFolder();
        }

        public override void RefreshScreen()
        {
            if (!string.IsNullOrWhiteSpace(State.LastAudioPath))
                audioPathTextBox.Text = State.LastAudioPath;
        }

        private void BrowseAudio()
        {
            using var dlg = new OpenFileDialog { Filter = "WAV files (*.wav)|*.wav|All files (*.*)|*.*" };
            if (dlg.ShowDialog() != DialogResult.OK) return;
            audioPathTextBox.Text = dlg.FileName;
            State.LastAudioPath = dlg.FileName;
            State.NotifyChanged();
        }

        private void PlayAudio()
        {
            try
            {
                string path = audioPathTextBox.Text;
                if (!File.Exists(path))
                {
                    ShowInfo("Audio file not found.");
                    return;
                }
                using var player = new SoundPlayer(path);
                player.Play();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private async Task RunCheckAsync()
        {
            try
            {
                string path = audioPathTextBox.Text;
                if (!File.Exists(path))
                {
                    ShowInfo("Audio file not found.");
                    return;
                }

                btnRunCheck.Enabled = false;
                statusLabel.Text = "Running Azure STT and comparison...";
                var result = await State.Quality.RunAsync(path, State.Script, State.FinalTtsList.ConvertAll(r => r.Word));
                recognizedTextBox.Text = result.recognized;
                resultGrid.DataSource = result.rows;
                statusLabel.Text = "Quality check completed.";
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Quality check failed.";
                ShowError(ex);
            }
            finally { btnRunCheck.Enabled = true; }
        }

        private void OpenFolder()
        {
            try
            {
                string folder = File.Exists(audioPathTextBox.Text)
                    ? Path.GetDirectoryName(audioPathTextBox.Text)
                    : State.Config.OutputFolder;
                if (!Directory.Exists(folder)) return;
                Process.Start(new ProcessStartInfo { FileName = folder, UseShellExecute = true });
            }
            catch (Exception ex) { ShowError(ex); }
        }
    }
}
