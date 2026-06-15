using TTSAgent.Core;
using TTSAgent.Models;

namespace TTSAgent.UI.Screens
{
    public partial class DashboardScreen : ScreenBase
    {
        public DashboardScreen(AppState state) : base(state)
        {
            InitializeComponent();
            Ui.StylePrimaryButton(btnStart);
            Ui.StyleSecondaryButton(btnDictionary);
            btnStart.Click += (_, _) => NavigateTo("script");
            btnDictionary.Click += (_, _) => NavigateTo("dictionary");
            workflowTextBox.Text =
                "1. Import or create the Fixed Dictionary CSV.\r\n" +
                "2. Paste Japanese script and extract kanji-containing terms with the LLM.\r\n" +
                "3. Check fixed/new/conflict terms in Kanji Review.\r\n" +
                "4. Confirm uncertain readings in Human Review.\r\n" +
                "5. Generate the final TTS pronunciation list.\r\n" +
                "6. Build SSML and synthesize Azure TTS WAV audio.\r\n" +
                "7. Run STT-based voice quality check.\r\n\r\n" +
                "Important rule: the fixed dictionary always wins. LLM suggestions never overwrite confirmed readings automatically.";
        }

        public override void RefreshScreen()
        {
            dictionaryCountLabel.Text = $"Dictionary\r\n{State.Dictionary.Count}";
            termsCountLabel.Text = $"Extracted Terms\r\n{State.Extraction?.Terms?.Count ?? 0}";
            int pending = State.Confirmation.Items.FindAll(i => i.State == ReviewState.Pending || i.State == ReviewState.Editing).Count;
            reviewCountLabel.Text = $"Pending Review\r\n{pending}";
            audioStatusLabel.Text = string.IsNullOrWhiteSpace(State.LastAudioPath) ? "Audio\r\nNot generated" : "Audio\r\nGenerated";
        }
    }
}
