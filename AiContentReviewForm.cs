using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1
{
    public partial class AiContentReviewForm : Form
    {
        private readonly List<ContentItem> _items;

        private readonly ContentRepository _repository;

        private readonly OpenAiGuideService _aiService;

        private readonly GuideValidationService _validator;

        private readonly AiPreference _preference;

        private int _currentIndex;

        private bool _editing = false;


        private ContentItem CurrentItem
        {
            get
            {
                return _items[_currentIndex];
            }
        }


        public AiContentReviewForm(
            List<ContentItem> items,
            int startIndex,
            ContentRepository repository,
            OpenAiGuideService aiService,
            AiPreference preference)
        {
            InitializeComponent();

            _items =
                items ?? new List<ContentItem>();

            _repository =
                repository;

            _aiService =
                aiService;

            _preference =
                preference;

            _validator =
                new GuideValidationService();


            if (startIndex < 0)
                startIndex = 0;

            if (startIndex >= _items.Count)
                startIndex = 0;

            _currentIndex =
                startIndex;


            this.Shown +=
                AiContentReviewForm_Shown;
        }


        private async void AiContentReviewForm_Shown(
            object sender,
            EventArgs e)
        {
            await LoadCurrentItemAsync();
        }


        // ============================================================
        // LOAD GUIDE
        // ============================================================

        private async Task LoadCurrentItemAsync()
        {
            if (_items.Count == 0)
            {
                MessageBox.Show(
                    "No Guide content found.");

                Close();

                return;
            }


            ContentItem item =
                CurrentItem;


            lblContentCounter.Text =
                $"Content: Guide " +
                $"{_currentIndex + 1} / " +
                $"{_items.Count}";


            lblTopic.Text =
                item.Topic;


            txtSourceText.Text =
                item.SourceText ?? "";


            // Load latest DB version if necessary
            if (string.IsNullOrWhiteSpace(
                    item.GeneratedText) &&
                item.DatabaseItemId > 0)
            {
                GeneratedContentVersion latest =
                    _repository.GetLatestVersion(
                        item.DatabaseItemId);


                if (latest != null)
                {
                    item.GeneratedText =
                        latest.GeneratedText;

                    item.CurrentVersionId =
                        latest.Id;

                    item.AiStatus =
                        "Generated";


                    if (latest.IsApproved)
                    {
                        item.ValidationStatus =
                            "Passed";

                        item.ReviewStatus =
                            "Approved";

                        item.ProcessStatus =
                            "Ready";
                    }
                }
            }


            txtGeneratedText.Text =
                item.GeneratedText ?? "";


            txtGeneratedText.ReadOnly =
                true;

            txtGeneratedText.BackColor =
                Color.White;


            _editing =
                false;


            btnEdit.Text =
                "Edit";


            UpdateButtons();
            UpdateStatus();


            await Task.CompletedTask;
        }


        // ============================================================
        // GENERATE / REGENERATE
        // ============================================================

        private async void btnRegenerate_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                SetBusy(true);


               ContentItem item =
                    CurrentItem;


                lblStatus.Text =
                    "Generating content with AI...";


                item.AiStatus =
                    "Generating";

                item.ProcessStatus =
                    "Processing";


                AiGuideResult result =
                    await _aiService.GenerateAsync(
                        item,
                        _preference);


                if (result == null ||
                    string.IsNullOrWhiteSpace(
                        result.GeneratedText))
                {
                    throw new Exception(
                        "AI returned empty content.");
                }


                // Save as new version
                long versionId =
                    _repository.CreateVersion(
                        item.DatabaseItemId,
                        result.GeneratedText,
                        result.ModelName,
                        result.PromptVersion,
                        false);


                item.CurrentVersionId =
                    versionId;

                item.GeneratedText =
                    result.GeneratedText;

                item.AiStatus =
                    "Generated";


                // Validate
                GuideValidationResult validation =
                    _validator.Validate(
                        item,
                        result.GeneratedText);


                item.ValidationStatus =
                    validation.Status;

                item.ReviewStatus =
                    "Review";

                item.ProcessStatus =
                    validation.Passed
                    ? "Waiting"
                    : "Attention";


                txtGeneratedText.Text =
                    result.GeneratedText;


                lblStatus.Text =
                    validation.Message;


                UpdateButtons();
            }
            catch (Exception ex)
            {
                CurrentItem.AiStatus =
                    "Failed";

                CurrentItem.ValidationStatus =
                    "Failed";

                CurrentItem.ProcessStatus =
                    "Error";


                lblStatus.Text =
                    "Generation failed.";


                MessageBox.Show(
                    ex.Message,
                    "AI Generation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                SetBusy(false);
            }
        }


        // ============================================================
        // EDIT
        // ============================================================

        private void btnEdit_Click(
            object sender,
            EventArgs e)
        {
            if (!_editing)
            {
                if (string.IsNullOrWhiteSpace(
                        txtGeneratedText.Text))
                {
                    MessageBox.Show(
                        "Generate content first.");

                    return;
                }


                _editing =
                    true;


                txtGeneratedText.ReadOnly =
                    false;


                txtGeneratedText.BackColor =
                    Color.FromArgb(
                        255,
                        253,
                        235);


                txtGeneratedText.Focus();


                btnEdit.Text =
                    "Save Edit";


                lblStatus.Text =
                    "Edit mode: modify the generated text, then click Save Edit.";

                return;
            }


            SaveEditedText();
        }


        private void SaveEditedText()
        {
            string text =
                txtGeneratedText.Text.Trim();


            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show(
                    "Generated text cannot be empty.");

                return;
            }


            ContentItem item =
                CurrentItem;


            long versionId =
                _repository.CreateVersion(
                    item.DatabaseItemId,
                    text,
                    "HUMAN_EDIT",
                    "MANUAL_EDIT",
                    true);


            item.CurrentVersionId =
                versionId;

            item.GeneratedText =
                text;

            item.AiStatus =
                "Edited";

            item.ValidationStatus =
                "Not Checked";

            item.ReviewStatus =
                "Review";

            item.ProcessStatus =
                "Waiting";


            _editing =
                false;


            txtGeneratedText.ReadOnly =
                true;

            txtGeneratedText.BackColor =
                Color.White;


            btnEdit.Text =
                "Edit";


            lblStatus.Text =
                "✓ Edited version saved to database.";


            UpdateButtons();
        }


        // ============================================================
        // APPROVE
        // ============================================================

        private void btnApprove_Click(
            object sender,
            EventArgs e)
        {
            ContentItem item =
                CurrentItem;


            if (string.IsNullOrWhiteSpace(
                    txtGeneratedText.Text))
            {
                MessageBox.Show(
                    "No generated text is available.");

                return;
            }


            // If edited without pressing Save Edit,
            // save automatically before approval.
            if (_editing)
            {
                SaveEditedText();
            }


            if (!item.CurrentVersionId.HasValue)
            {
                MessageBox.Show(
                    "No generated database version exists.");

                return;
            }


            _repository.ApproveVersion(
                item.DatabaseItemId,
                item.CurrentVersionId.Value);


            item.AiStatus =
                "Generated";

            item.ValidationStatus =
                "Passed";

            item.ReviewStatus =
                "Approved";

            item.ProcessStatus =
                "Ready";


            lblStatus.Text =
                "✓ Approved and saved to database.";


            btnApprove.Enabled =
                false;
        }


        // ============================================================
        // NEXT
        // ============================================================

        private async void btnNext_Click(
            object sender,
            EventArgs e)
        {
            if (_editing)
            {
                DialogResult result =
                    MessageBox.Show(
                        "You have unsaved changes. Continue without saving?",
                        "Unsaved Edit",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);


                if (result !=
                    DialogResult.Yes)
                {
                    return;
                }
            }


            if (_currentIndex >=
                _items.Count - 1)
            {
                MessageBox.Show(
                    "All Guide contents have been reviewed.",
                    "Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            _currentIndex++;


            await LoadCurrentItemAsync();
        }


        // ============================================================
        // STATUS
        // ============================================================

        private void UpdateButtons()
        {
            bool generated =
                !string.IsNullOrWhiteSpace(
                    txtGeneratedText.Text);


            btnRegenerate.Text =
                generated
                ? "Regenerate"
                : "Generate";


            btnEdit.Enabled =
                generated;


            btnApprove.Enabled =
                generated &&
                CurrentItem.ReviewStatus !=
                "Approved";


            btnNext.Enabled =
                _items.Count > 1;
        }


        private void UpdateStatus()
        {
            ContentItem item =
                CurrentItem;


            if (item.ReviewStatus ==
                "Approved")
            {
                lblStatus.Text =
                    "✓ Approved";
            }

            else if (!string.IsNullOrWhiteSpace(
                         item.GeneratedText))
            {
                lblStatus.Text =
                    "Generated content is ready for review.";
            }

            else
            {
                lblStatus.Text =
                    "No AI content generated yet.";
            }
        }


        // ============================================================
        // BUSY STATE
        // ============================================================

        private void SetBusy(bool busy)
        {
            Cursor =
                busy
                ? Cursors.WaitCursor
                : Cursors.Default;


            btnRegenerate.Enabled =
                !busy;


            btnEdit.Enabled =
                !busy &&
                !string.IsNullOrWhiteSpace(
                    txtGeneratedText.Text);


            btnApprove.Enabled =
                !busy &&
                !string.IsNullOrWhiteSpace(
                    txtGeneratedText.Text);


            btnNext.Enabled =
                !busy;
        }

        
    }
}