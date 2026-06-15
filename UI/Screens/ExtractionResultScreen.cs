using System.Linq;
using TTSAgent.Core;

namespace TTSAgent.UI.Screens
{
    public partial class ExtractionResultScreen : ScreenBase
    {
        public ExtractionResultScreen(AppState state) : base(state)
        {
            InitializeComponent();
            Ui.StyleGrid(termsGrid);
            Ui.StylePrimaryButton(btnSendReview);
            Ui.StyleSecondaryButton(btnHumanReview);
            Ui.StyleSecondaryButton(btnTtsList);
            Ui.StyleSecondaryButton(btnBack);

            btnSendReview.Click += (_, _) => SendReviewWords();
            btnHumanReview.Click += (_, _) => NavigateTo("human");
            btnTtsList.Click += (_, _) => NavigateTo("ttslist");
            btnBack.Click += (_, _) => NavigateTo("script");
        }

        public override void RefreshScreen()
        {
            var r = State.Extraction;
            if (r == null)
            {
                summaryLabel.Text = "No extraction result. Please run extraction from Script Input.";
                termsGrid.DataSource = null;
                return;
            }

            summaryLabel.Text =
                $"Total: {r.Summary.TotalTerms} | Fixed: {r.Summary.FixedDictionaryMatches} | New: {r.Summary.NewTerms} | Review: {r.Summary.ReviewRequiredCount} | Conflict: {r.Summary.ConflictCount}";
            termsGrid.DataSource = r.Terms.Select(t => new
            {
                t.Word,
                t.Hiragana,
                Status = t.DictionaryStatus,
                Source = t.ReadingSource,
                Review = t.ReviewRequired,
                t.Confidence,
                t.Category,
                t.Reason,
                Sentence = t.SourceSentence
            }).ToList();
        }

        private void SendReviewWords()
        {
            if (State.Extraction == null)
            {
                ShowInfo("No extraction result found.");
                return;
            }

            int before = State.Confirmation.Items.Count;
            foreach (var term in State.Extraction.Terms.Where(t =>
                         t.ReviewRequired || t.DictionaryStatus == "new" || t.DictionaryStatus == "conflict"))
            {
                State.Confirmation.AddFromTerm(term);
            }

            int added = State.Confirmation.Items.Count - before;
            State.NotifyChanged();
            ShowInfo(added == 0
                ? "No new review item was added. You can continue to TTS List."
                : $"Added {added} item(s) to the Human Review queue.");

            NavigateTo(State.Confirmation.IsEmpty ? "ttslist" : "human");
        }
    }
}
