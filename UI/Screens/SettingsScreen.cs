using System;
using System.IO;
using System.Windows.Forms;
using TTSAgent.Core;

namespace TTSAgent.UI.Screens
{
    public partial class SettingsScreen : ScreenBase
    {
        public SettingsScreen(AppState state) : base(state)
        {
            InitializeComponent();
            Ui.StyleSecondaryButton(btnBrowseOutput);
            Ui.StylePrimaryButton(btnSave);
            btnBrowseOutput.Click += (_, _) => BrowseOutputFolder();
            btnSave.Click += (_, _) => SaveSettings();
        }

        public override void RefreshScreen()
        {
            llmKeyTextBox.Text = State.Config.LlmApiKey;
            llmModelTextBox.Text = State.Config.LlmModel;
            azureKeyTextBox.Text = State.Config.AzureSpeechKey;
            azureRegionTextBox.Text = State.Config.AzureSpeechRegion;
            outputTextBox.Text = State.Config.OutputFolder;
        }

        private void BrowseOutputFolder()
        {
            using var dlg = new FolderBrowserDialog();
            if (Directory.Exists(outputTextBox.Text)) dlg.SelectedPath = outputTextBox.Text;
            if (dlg.ShowDialog() == DialogResult.OK) outputTextBox.Text = dlg.SelectedPath;
        }

        private void SaveSettings()
        {
            try
            {
                State.Config.LlmApiKey = llmKeyTextBox.Text.Trim();
                State.Config.LlmModel = string.IsNullOrWhiteSpace(llmModelTextBox.Text) ? "claude-sonnet-4-20250514" : llmModelTextBox.Text.Trim();
                State.Config.AzureSpeechKey = azureKeyTextBox.Text.Trim();
                State.Config.AzureSpeechRegion = string.IsNullOrWhiteSpace(azureRegionTextBox.Text) ? "japaneast" : azureRegionTextBox.Text.Trim();
                State.Config.OutputFolder = string.IsNullOrWhiteSpace(outputTextBox.Text) ? AppConfig.DefaultOutputFolder : outputTextBox.Text.Trim();
                State.Config.Save();
                State.RebuildExtractor();
                State.NotifyChanged();
                savedLabel.Text = "Saved to appsettings.json.";
            }
            catch (Exception ex) { ShowError(ex); }
        }
    }
}
