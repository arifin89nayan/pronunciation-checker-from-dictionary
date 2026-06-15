using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TTSAgent.Core;
using TTSAgent.Models;
using TTSAgent.Services;

namespace TTSAgent.UI.Screens
{
    public partial class HumanReviewScreen : ScreenBase
    {
        public HumanReviewScreen(AppState state) : base(state)
        {
            InitializeComponent();
            Ui.StyleGrid(reviewGrid);
            Ui.StylePrimaryButton(btnApprove);
            Ui.StyleSecondaryButton(btnReject);
            Ui.StyleSecondaryButton(btnExport);
            Ui.StyleSecondaryButton(btnDictionary);
            Ui.StyleSecondaryButton(btnTtsList);

            reviewGrid.SelectionChanged += (_, _) => LoadSelectedItem();
            btnApprove.Click += (_, _) => ApproveSelected();
            btnReject.Click += (_, _) => ChangeSelectedState(ReviewState.Rejected);
            btnExport.Click += (_, _) => ExportQueue();
            btnDictionary.Click += (_, _) => NavigateTo("dictionary");
            btnTtsList.Click += (_, _) => NavigateTo("ttslist");
        }

        public override void RefreshScreen()
        {
            reviewGrid.DataSource = State.Confirmation.Items.Select(i => new
            {
                i.Word,
                Api = i.ApiReading,
                Correct = i.CorrectReading,
                i.Category,
                i.SaveType,
                State = i.State.ToString()
            }).ToList();

            if (reviewGrid.Rows.Count > 0 && reviewGrid.SelectedRows.Count == 0)
                reviewGrid.Rows[0].Selected = true;
            LoadSelectedItem();
        }

        private ReviewItem SelectedItem()
        {
            if (reviewGrid.SelectedRows.Count == 0) return null;
            string word = Convert.ToString(reviewGrid.SelectedRows[0].Cells["Word"].Value);
            return State.Confirmation.Items.FirstOrDefault(i => i.Word == word);
        }

        private void LoadSelectedItem()
        {
            var item = SelectedItem();
            if (item == null)
            {
                wordTextBox.Text = apiReadingTextBox.Text = correctReadingTextBox.Text = sentenceTextBox.Text = "";
                categoryComboBox.Text = "general_word";
                saveTypeComboBox.SelectedItem = "Fixed List";
                return;
            }

            wordTextBox.Text = item.Word;
            apiReadingTextBox.Text = item.ApiReading;
            correctReadingTextBox.Text = item.CorrectReading;
            categoryComboBox.Text = item.Category;
            saveTypeComboBox.SelectedItem = string.IsNullOrWhiteSpace(item.SaveType) ? "Fixed List" : item.SaveType;
            sentenceTextBox.Text = item.SourceSentence;
        }

        private void ApproveSelected()
        {
            var item = SelectedItem();
            if (item == null) return;

            string correct = FixedDictionaryService.NormalizeToHiragana(correctReadingTextBox.Text);
            if (string.IsNullOrWhiteSpace(correct))
            {
                ShowInfo("Correct reading is required.");
                return;
            }

            item.CorrectReading = correct;
            item.Category = categoryComboBox.Text;
            item.SaveType = Convert.ToString(saveTypeComboBox.SelectedItem) ?? "Fixed List";
            item.State = ReviewState.Approved;

            if (item.SaveType == "Fixed List")
            {
                State.Dictionary.AddOrUpdate(new DictionaryEntry
                {
                    Word = item.Word,
                    Hiragana = item.CorrectReading,
                    Category = item.Category,
                    Status = "Approved",
                    Updated = DateTime.Today
                });

                if (!string.IsNullOrWhiteSpace(State.Dictionary.BackingCsvPath))
                    State.Dictionary.SaveCsv();
            }

            State.NotifyChanged();
            RefreshScreen();
        }

        private void ChangeSelectedState(ReviewState state)
        {
            var item = SelectedItem();
            if (item == null) return;
            item.State = state;
            State.NotifyChanged();
            RefreshScreen();
        }

        private void ExportQueue()
        {
            try
            {
                Directory.CreateDirectory(State.Config.OutputFolder);
                string path = Path.Combine(State.Config.OutputFolder, $"confirmation_queue_{DateTime.Now:yyyyMMdd_HHmmss}.csv");
                State.Confirmation.ExportCsv(path);
                ShowInfo("Exported: " + path);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }
    }
}
