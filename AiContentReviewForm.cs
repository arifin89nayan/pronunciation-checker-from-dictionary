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
        // ============================================================
        // CONTENT CURRENTLY BEING REVIEWED
        //
        // IMPORTANT:
        // ContentDataForm passes either:
        //
        // List<ContentItem> Guide only
        //
        // OR
        //
        // List<ContentItem> Quiz only
        //
        // ============================================================

        private readonly List<ContentItem> _items;


        // ============================================================
        // DATABASE
        // ============================================================

        private readonly ContentRepository _repository;


        // ============================================================
        // AI
        //
        // Currently using your existing service for both types.
        //
        // Later OpenAiGuideService can internally switch its
        // prompt/schema based on item.ContentType.
        // ============================================================

        private readonly OpenAiGuideService _aiService;


        // ============================================================
        // VALIDATION
        // ============================================================

        private readonly GuideValidationService _guideValidator;


        // ============================================================
        // PREFERENCE
        // ============================================================

        private readonly AiPreference _preference;


        // ============================================================
        // CURRENT POSITION
        // ============================================================

        private int _currentIndex;


        // ============================================================
        // EDIT STATE
        // ============================================================

        private bool _editing = false;


        // ============================================================
        // CURRENT ITEM
        // ============================================================

        private ContentItem CurrentItem
        {
            get
            {
                if (_items == null ||
                    _items.Count == 0)
                {
                    return null;
                }


                return _items[_currentIndex];
            }
        }


        // ============================================================
        // CURRENT CONTENT TYPE
        // ============================================================

        private bool IsGuide
        {
            get
            {
                return CurrentItem != null &&
                       string.Equals(
                           CurrentItem.ContentType,
                           "Guide",
                           StringComparison.OrdinalIgnoreCase);
            }
        }


        private bool IsQuiz
        {
            get
            {
                return CurrentItem != null &&
                       string.Equals(
                           CurrentItem.ContentType,
                           "Quiz",
                           StringComparison.OrdinalIgnoreCase);
            }
        }


        private string CurrentContentType
        {
            get
            {
                if (IsQuiz)
                    return "Quiz";


                return "Guide";
            }
        }


        // ============================================================
        // CONSTRUCTOR
        // ============================================================

        public AiContentReviewForm(
            List<ContentItem> items,
            int startIndex,
            ContentRepository repository,
            OpenAiGuideService aiService,
            AiPreference preference)
        {
            InitializeComponent();


            _items =
                items ??
                new List<ContentItem>();


            _repository =
                repository;


            _aiService =
                aiService;


            _preference =
                preference;


            _guideValidator =
                new GuideValidationService();


            // --------------------------------------------------------
            // START INDEX VALIDATION
            // --------------------------------------------------------

            if (startIndex < 0)
            {
                startIndex = 0;
            }


            if (startIndex >= _items.Count)
            {
                startIndex = 0;
            }


            _currentIndex =
                startIndex;


            this.Shown -=
                AiContentReviewForm_Shown;


            this.Shown +=
                AiContentReviewForm_Shown;
        }


        // ============================================================
        // FORM SHOWN
        // ============================================================

        private async void AiContentReviewForm_Shown(
            object sender,
            EventArgs e)
        {
            await LoadCurrentItemAsync();
        }


        // ============================================================
        // LOAD CURRENT ITEM
        // ============================================================

        private async Task LoadCurrentItemAsync()
        {
            if (_items == null ||
                _items.Count == 0)
            {
                MessageBox.Show(
                    "No content was found.",
                    "AI Content Review",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);


                Close();

                return;
            }


            ContentItem item =
                CurrentItem;


            if (item == null)
            {
                Close();

                return;
            }


            // ========================================================
            // TOP COUNTER
            // ========================================================

            lblContentCounter.Text =
                $"Content: {item.ContentType} " +
                $"{_currentIndex + 1} / " +
                $"{_items.Count}";


            // ========================================================
            // TOPIC
            // ========================================================

            lblTopic.Text =
                item.Topic ?? "";


            // ========================================================
            // DYNAMIC LABELS
            // ========================================================

            if (IsGuide)
            {
                lblSourceTitle.Text =
                    "Source Text";


                lblGeneratedTitle.Text =
                    "Generated Guide";
            }

            else if (IsQuiz)
            {
                lblSourceTitle.Text =
                    "Source Quiz";


                lblGeneratedTitle.Text =
                    "Generated Quiz";
            }

            else
            {
                lblSourceTitle.Text =
                    "Source Content";


                lblGeneratedTitle.Text =
                    "Generated Content";
            }


            // ========================================================
            // ORIGINAL EXCEL CONTENT
            // ========================================================

            txtSourceText.Text =
                item.SourceText ?? "";


            // ========================================================
            // LOAD LATEST DATABASE VERSION
            // ========================================================

            if (string.IsNullOrWhiteSpace(
                    item.GeneratedText)
                &&
                item.DatabaseItemId > 0
                &&
                _repository != null)
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


            // ========================================================
            // CURRENT GENERATED CONTENT
            // ========================================================

            txtGeneratedText.Text =
                item.GeneratedText ?? "";


            txtGeneratedText.ReadOnly =
                true;


            txtGeneratedText.BackColor =
                Color.White;


            // ========================================================
            // RESET EDIT MODE
            // ========================================================

            _editing =
                false;


            btnEdit.Text =
                "Edit";


            // ========================================================
            // REFRESH UI
            // ========================================================

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
            if (CurrentItem == null)
            {
                return;
            }


            try
            {
                SetBusy(true);


                ContentItem item =
                    CurrentItem;


                lblStatus.Text =
                    $"Generating {CurrentContentType} content with AI...";


                item.AiStatus =
                    "Generating";


                item.ProcessStatus =
                    "Processing";


                // ====================================================
                // CALL AI
                //
                // IMPORTANT:
                //
                // Same service currently receives Guide or Quiz.
                //
                // OpenAiGuideService should later inspect:
                //
                // item.ContentType
                //
                // to use:
                //
                // GUIDE_V1 prompt
                // or
                // QUIZ_V1 prompt
                // ====================================================

                AiGuideResult result =
                    await _aiService.GenerateAsync(
                        item,
                        _preference);


                if (result == null ||
                    string.IsNullOrWhiteSpace(
                        result.GeneratedText))
                {
                    throw new Exception(
                        $"AI returned empty {CurrentContentType} content.");
                }


                // ====================================================
                // SAVE NEW DATABASE VERSION
                // ====================================================

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


                // ====================================================
                // VALIDATION
                // ====================================================

                if (IsGuide)
                {
                    ValidateGuide(
                        item,
                        result.GeneratedText);
                }

                else if (IsQuiz)
                {
                    ValidateQuiz(
                        item,
                        result.GeneratedText);
                }

                else
                {
                    item.ValidationStatus =
                        "Review";


                    item.ProcessStatus =
                        "Attention";


                    lblStatus.Text =
                        "Unknown content type. Manual review is required.";
                }


                // Every newly generated version requires review.
                item.ReviewStatus =
                    "Review";


                // ====================================================
                // SHOW RESULT
                // ====================================================

                txtGeneratedText.Text =
                    result.GeneratedText;


                txtGeneratedText.ReadOnly =
                    true;


                txtGeneratedText.BackColor =
                    Color.White;


                _editing =
                    false;


                btnEdit.Text =
                    "Edit";


                UpdateButtons();
            }
            catch (Exception ex)
            {
                if (CurrentItem != null)
                {
                    CurrentItem.AiStatus =
                        "Failed";


                    CurrentItem.ValidationStatus =
                        "Failed";


                    CurrentItem.ProcessStatus =
                        "Error";
                }


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

                UpdateButtons();
            }
        }


        // ============================================================
        // GUIDE VALIDATION
        // ============================================================

        private void ValidateGuide(
            ContentItem item,
            string generatedText)
        {
            GuideValidationResult validation =
                _guideValidator.Validate(
                    item,
                    generatedText);


            item.ValidationStatus =
                validation.Status;


            item.ProcessStatus =
                validation.Passed
                    ? "Waiting"
                    : "Attention";


            lblStatus.Text =
                validation.Message;
        }


        // ============================================================
        // QUIZ VALIDATION
        //
        // This is intentionally basic for now.
        //
        // Later validate structured Quiz fields:
        // Question
        // Answer
        // Options
        // Correct selection
        // Explanation
        // ============================================================

        private void ValidateQuiz(
            ContentItem item,
            string generatedText)
        {
            if (string.IsNullOrWhiteSpace(
                    generatedText))
            {
                item.ValidationStatus =
                    "Failed";


                item.ProcessStatus =
                    "Attention";


                lblStatus.Text =
                    "Generated Quiz content is empty.";


                return;
            }


            if (generatedText.Trim().Length < 20)
            {
                item.ValidationStatus =
                    "Review";


                item.ProcessStatus =
                    "Attention";


                lblStatus.Text =
                    "Generated Quiz content may be too short.";


                return;
            }


            item.ValidationStatus =
                "Passed";


            item.ProcessStatus =
                "Waiting";


            lblStatus.Text =
                "Quiz generation completed. Review before approval.";
        }


        // ============================================================
        // EDIT BUTTON
        // ============================================================

        private void btnEdit_Click(
            object sender,
            EventArgs e)
        {
            if (CurrentItem == null)
            {
                return;
            }


            // --------------------------------------------------------
            // ENTER EDIT MODE
            // --------------------------------------------------------

            if (!_editing)
            {
                if (string.IsNullOrWhiteSpace(
                        txtGeneratedText.Text))
                {
                    MessageBox.Show(
                        $"Generate {CurrentContentType} content first.",
                        "Edit",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);


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


            // --------------------------------------------------------
            // SAVE EDIT
            // --------------------------------------------------------

            SaveEditedText();
        }


        // ============================================================
        // SAVE EDITED TEXT
        // ============================================================

        private bool SaveEditedText()
        {
            if (CurrentItem == null)
            {
                return false;
            }


            string text =
                txtGeneratedText.Text.Trim();


            if (string.IsNullOrWhiteSpace(
                    text))
            {
                MessageBox.Show(
                    "Generated text cannot be empty.",
                    "Save Edit",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);


                return false;
            }


            ContentItem item =
                CurrentItem;


            // ========================================================
            // SAVE NEW HUMAN EDIT VERSION
            // ========================================================

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


            // ========================================================
            // EXIT EDIT MODE
            // ========================================================

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


            return true;
        }


        // ============================================================
        // APPROVE
        // ============================================================

        private void btnApprove_Click(
            object sender,
            EventArgs e)
        {
            if (CurrentItem == null)
            {
                return;
            }


            ContentItem item =
                CurrentItem;


            if (string.IsNullOrWhiteSpace(
                    txtGeneratedText.Text))
            {
                MessageBox.Show(
                    "No generated content is available.",
                    "Approve",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);


                return;
            }


            // ========================================================
            // SAVE UNSAVED EDIT FIRST
            // ========================================================

            if (_editing)
            {
                bool saved =
                    SaveEditedText();


                if (!saved)
                {
                    return;
                }
            }


            if (!item.CurrentVersionId.HasValue)
            {
                MessageBox.Show(
                    "No generated database version exists.",
                    "Approve",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);


                return;
            }


            // ========================================================
            // APPROVE DATABASE VERSION
            // ========================================================

            _repository.ApproveVersion(
                item.DatabaseItemId,
                item.CurrentVersionId.Value);


            // ========================================================
            // UPDATE UI STATUS
            // ========================================================

            item.AiStatus =
                item.AiStatus == "Edited"
                    ? "Edited"
                    : "Generated";


            item.ValidationStatus =
                "Passed";


            item.ReviewStatus =
                "Approved";


            item.ProcessStatus =
                "Ready";


            lblStatus.Text =
                $"✓ {CurrentContentType} approved and saved to database.";


            txtGeneratedText.ReadOnly =
                true;


            txtGeneratedText.BackColor =
                Color.White;


            _editing =
                false;


            btnEdit.Text =
                "Edit";


            UpdateButtons();
        }


        // ============================================================
        // NEXT
        // ============================================================

        private async void btnNext_Click(
            object sender,
            EventArgs e)
        {
            if (CurrentItem == null)
            {
                return;
            }


            // ========================================================
            // UNSAVED EDIT WARNING
            // ========================================================

            if (_editing)
            {
                DialogResult result =
                    MessageBox.Show(
                        "You have unsaved changes.\r\n\r\n" +
                        "Continue without saving?",
                        "Unsaved Edit",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);


                if (result !=
                    DialogResult.Yes)
                {
                    return;
                }
            }


            // ========================================================
            // LAST CONTENT
            // ========================================================

            if (_currentIndex >=
                _items.Count - 1)
            {
                MessageBox.Show(
                    $"All {CurrentContentType} contents have been reviewed.",
                    "Review Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);


                return;
            }


            // ========================================================
            // NEXT ITEM
            // ========================================================

            _currentIndex++;


            await LoadCurrentItemAsync();
        }


        // ============================================================
        // UPDATE BUTTONS
        // ============================================================

        private void UpdateButtons()
        {
            if (CurrentItem == null)
            {
                btnRegenerate.Enabled =
                    false;


                btnEdit.Enabled =
                    false;


                btnApprove.Enabled =
                    false;


                btnNext.Enabled =
                    false;


                return;
            }


            bool generated =
                !string.IsNullOrWhiteSpace(
                    txtGeneratedText.Text);


            // --------------------------------------------------------
            // GENERATE / REGENERATE
            // --------------------------------------------------------

            btnRegenerate.Text =
                generated
                    ? "Regenerate"
                    : "Generate";


            // --------------------------------------------------------
            // EDIT
            // --------------------------------------------------------

            btnEdit.Enabled =
                generated;


            // --------------------------------------------------------
            // APPROVE
            // --------------------------------------------------------

            btnApprove.Enabled =
                generated &&
                CurrentItem.ReviewStatus !=
                "Approved";


            if (CurrentItem.ReviewStatus ==
                "Approved")
            {
                btnApprove.Text =
                    "Approved";
            }
            else
            {
                btnApprove.Text =
                    "Approve";
            }


            // --------------------------------------------------------
            // NEXT
            // --------------------------------------------------------

            btnNext.Enabled =
                _items.Count > 1;
        }


        // ============================================================
        // UPDATE STATUS
        // ============================================================

        private void UpdateStatus()
        {
            ContentItem item =
                CurrentItem;


            if (item == null)
            {
                lblStatus.Text =
                    "";

                return;
            }


            if (item.ReviewStatus ==
                "Approved")
            {
                lblStatus.Text =
                    $"✓ {item.ContentType} approved.";
            }


            else if (!string.IsNullOrWhiteSpace(
                         item.GeneratedText))
            {
                lblStatus.Text =
                    $"{item.ContentType} content is ready for review.";
            }


            else
            {
                lblStatus.Text =
                    $"No AI {item.ContentType} content generated yet.";
            }
        }


        // ============================================================
        // BUSY STATE
        // ============================================================

        private void SetBusy(
            bool busy)
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
                !busy &&
                _items.Count > 1;
        }

        private async void btnPrevious_Click(object sender, EventArgs e)
        {
            if (CurrentItem == null)
            {
                return;
            }

            // If user is editing, warn before moving back
            if (_editing)
            {
                DialogResult result =
                    MessageBox.Show(
                        "You have unsaved changes.\r\n\r\n" +
                        "Go back without saving?",
                        "Unsaved Edit",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                {
                    return;
                }

                // Exit edit mode
                _editing = false;

                txtGeneratedText.ReadOnly = true;
                txtGeneratedText.BackColor = Color.White;
                btnEdit.Text = "Edit";
            }

            // Already at first item
            if (_currentIndex <= 0)
            {
                MessageBox.Show(
                    $"You are already at the first {CurrentContentType}.",
                    "First Content",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            // Move to previous item
            _currentIndex--;

            await LoadCurrentItemAsync();
        }

        private void ExitBnt_Click(object sender, EventArgs e)
        {


            this.Close();

        }
    }
}