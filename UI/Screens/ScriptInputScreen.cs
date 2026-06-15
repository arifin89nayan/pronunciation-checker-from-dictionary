using System;
using System.Threading.Tasks;
using TTSAgent.Core;

namespace TTSAgent.UI.Screens
{
    public partial class ScriptInputScreen : ScreenBase
    {
        public ScriptInputScreen(AppState state) : base(state)
        {
            InitializeComponent();
            Ui.StylePrimaryButton(btnExtract);
            Ui.StyleSecondaryButton(btnClear);
            Ui.StyleSecondaryButton(btnLoadSample);

            voiceComboBox.SelectedItem = State.Voice;
            if (voiceComboBox.SelectedIndex < 0) voiceComboBox.SelectedIndex = 0;
            speedComboBox.SelectedItem = State.Speed;
            if (speedComboBox.SelectedIndex < 0) speedComboBox.SelectedIndex = 1;

            btnExtract.Click += async (_, _) => await ExtractAsync();
            btnClear.Click += (_, _) => scriptTextBox.Clear();
            btnLoadSample.Click += (_, _) => LoadSampleText();
        }

        public override void RefreshScreen()
        {
            projectNameTextBox.Text = State.ProjectName;
            voiceComboBox.SelectedItem = State.Voice;
            speedComboBox.SelectedItem = State.Speed;
            if (!string.IsNullOrEmpty(State.Script) && string.IsNullOrWhiteSpace(scriptTextBox.Text))
                scriptTextBox.Text = State.Script;
        }

        private async Task ExtractAsync()
        {
            try
            {
                SaveInputsToState();
                if (string.IsNullOrWhiteSpace(State.Script))
                {
                    ShowInfo("Please paste Japanese script first.");
                    return;
                }

                btnExtract.Enabled = false;
                progressLabel.Text = "Extracting terms with LLM and enforcing dictionary rules...";

                State.Extraction = await State.Extractor.ExtractAsync(State.Script);
                State.NotifyChanged();
                progressLabel.Text = $"Done. Extracted {State.Extraction.Terms.Count} terms.";
                NavigateTo("review");
            }
            catch (Exception ex)
            {
                progressLabel.Text = "Extraction failed.";
                ShowError(ex);
            }
            finally
            {
                btnExtract.Enabled = true;
            }
        }

        private void SaveInputsToState()
        {
            State.ProjectName = string.IsNullOrWhiteSpace(projectNameTextBox.Text)
                ? "Museum_Edo_Script_01"
                : projectNameTextBox.Text.Trim();
            State.Voice = Convert.ToString(voiceComboBox.SelectedItem) ?? "ja-JP-NanamiNeural";
            State.Speed = Convert.ToString(speedComboBox.SelectedItem) ?? "Normal";
            State.Script = scriptTextBox.Text;
        }

        private void LoadSampleText()
        {
            scriptTextBox.Text = "江戸時代になると、人々はそれぞれ自由にオシャレを楽しむようになり、それに合わせて多くの装身具が誕生しました。特に女性の髪型はとても華やかで、簪や笄などが使われました。大國神社には献額が残されています。";
        }
    }
}
