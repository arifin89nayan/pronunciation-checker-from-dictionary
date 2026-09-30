using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Models.AutoConverters;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1
{
    public partial class ContentDataForm : Form
    {
        // ============================================================
        // MASTER DATA
        // Contains ALL Guide + Quiz records.
        // Never replace this collection when filtering.
        // ============================================================

        private readonly BindingList<ContentItem> _batchItems;


        // ============================================================
        // CURRENT GRID DATA
        // ============================================================

        private BindingList<ContentItem> _filteredItems;


        // ============================================================
        // ORIGINAL PARSED EXCEL RECORDS
        // ============================================================

        private readonly List<WidgetParsedCommonModel> _originalRecords;


        // ============================================================
        // LANGUAGE CONFIGURATION
        // ============================================================

        private readonly List<LanguageParsedModel> _languages;


        // ============================================================
        // WORKSPACE PATHS
        // ============================================================

        private readonly string _workspace;
        private readonly string _templateFolder;
        private readonly string _audioFolder;
        private readonly string _outputFolder;
        private readonly string _ibcFolder;


        // ============================================================
        // DATABASE + AI
        // ============================================================

        private readonly ContentRepository _repository;

        private readonly OpenAiGuideService _aiService;


        // ============================================================
        // CONSTRUCTOR
        // ============================================================

        public ContentDataForm(
            List<ContentItem> items,
            List<WidgetParsedCommonModel> originalRecords,
            List<LanguageParsedModel> languages,
            ContentRepository repository,
            string workspace,
            string templateFolder,
            string audioFolder,
            string outputFolder,
            string ibcFolder)
        {
            InitializeComponent();


            _batchItems =
                new BindingList<ContentItem>(
                    items ?? new List<ContentItem>());


            _originalRecords =
                originalRecords ??
                new List<WidgetParsedCommonModel>();


            _languages =
                languages ??
                new List<LanguageParsedModel>();


            _repository =
                repository;


            _aiService =
                new OpenAiGuideService();


            _workspace =
                workspace;


            _templateFolder =
                templateFolder;


            _audioFolder =
                audioFolder;


            _outputFolder =
                outputFolder;


            _ibcFolder =
                ibcFolder;


            ConfigureGrid();

            ConfigureFilter();


            // Default screen
            cmbContentFilter.SelectedItem =
                "All";


            ApplyContentFilter();
        }


        // ============================================================
        // CONTENT TYPE FILTER
        // ============================================================

        private void ConfigureFilter()
        {
            cmbContentFilter.Items.Clear();


            cmbContentFilter.Items.Add(
                "All");

            cmbContentFilter.Items.Add(
                "Guide");

            cmbContentFilter.Items.Add(
                "Quiz");


            cmbContentFilter.DropDownStyle =
                ComboBoxStyle.DropDownList;


            // Prevent duplicate event registration
            cmbContentFilter.SelectedIndexChanged -=
                cmbContentFilter_SelectedIndexChanged;


            cmbContentFilter.SelectedIndexChanged +=
                cmbContentFilter_SelectedIndexChanged;
        }


        private void cmbContentFilter_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            ApplyContentFilter();
        }


        // ============================================================
        // APPLY FILTER
        // ============================================================

        private void ApplyContentFilter()
        {
            string selectedType =
                cmbContentFilter.SelectedItem
                    ?.ToString()
                ?? "All";


            IEnumerable<ContentItem> query =
                _batchItems;


            // --------------------------------------------------------
            // GUIDE
            // --------------------------------------------------------

            if (selectedType.Equals(
                    "Guide",
                    StringComparison.OrdinalIgnoreCase))
            {
                query =
                    _batchItems.Where(x =>
                        string.Equals(
                            x.ContentType,
                            "Guide",
                            StringComparison.OrdinalIgnoreCase));
            }


            // --------------------------------------------------------
            // QUIZ
            // --------------------------------------------------------

            else if (selectedType.Equals(
                         "Quiz",
                         StringComparison.OrdinalIgnoreCase))
            {
                query =
                    _batchItems.Where(x =>
                        string.Equals(
                            x.ContentType,
                            "Quiz",
                            StringComparison.OrdinalIgnoreCase));
            }


            // --------------------------------------------------------
            // ALL
            // --------------------------------------------------------

            else
            {
                query =
                    _batchItems;
            }


            _filteredItems =
                new BindingList<ContentItem>(
                    query.ToList());


            dataGridViewContent.DataSource =
                null;


            dataGridViewContent.DataSource =
                _filteredItems;


            lblDetectedRecords.Text =
                $"Detected Records: {_filteredItems.Count}";


            UpdateAiGenerationButton();
        }


        // ============================================================
        // AI BUTTON
        // ============================================================

        private void UpdateAiGenerationButton()
        {
            string selectedType =
                cmbContentFilter.SelectedItem
                    ?.ToString()
                ?? "All";


            // --------------------------------------------------------
            // GUIDE
            // --------------------------------------------------------

            if (selectedType.Equals(
                    "Guide",
                    StringComparison.OrdinalIgnoreCase))
            {
                btnAiContent.Text =
                    "AI Generate Guides";


                btnAiContent.Enabled =
                    _filteredItems != null &&
                    _filteredItems.Count > 0;


                btnAiContent.BackColor =
                    Color.FromArgb(
                        25,
                        135,
                        125);


                btnAiContent.ForeColor =
                    Color.White;


                return;
            }


            // --------------------------------------------------------
            // QUIZ
            // --------------------------------------------------------

            if (selectedType.Equals(
                    "Quiz",
                    StringComparison.OrdinalIgnoreCase))
            {
                btnAiContent.Text =
                    "AI Generate Quizzes";


                btnAiContent.Enabled =
                    _filteredItems != null &&
                    _filteredItems.Count > 0;


                btnAiContent.BackColor =
                    Color.FromArgb(
                        47,
                        102,
                        225);


                btnAiContent.ForeColor =
                    Color.White;


                return;
            }


            // --------------------------------------------------------
            // ALL
            // --------------------------------------------------------

            btnAiContent.Text =
                "Select Guide or Quiz";


            btnAiContent.Enabled =
                false;


            btnAiContent.BackColor =
                Color.Gray;


            btnAiContent.ForeColor =
                Color.White;
        }


        // ============================================================
        // GRID
        // ============================================================

        private void ConfigureGrid()
        {
            dataGridViewContent.AutoGenerateColumns =
                false;


            dataGridViewContent.AllowUserToAddRows =
                false;


            dataGridViewContent.AllowUserToDeleteRows =
                false;


            dataGridViewContent.ReadOnly =
                true;


            dataGridViewContent.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;


            dataGridViewContent.MultiSelect =
                false;


            dataGridViewContent.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;


            dataGridViewContent.RowHeadersVisible =
                false;


            dataGridViewContent.BackgroundColor =
                Color.White;


            dataGridViewContent.BorderStyle =
                BorderStyle.None;


            dataGridViewContent.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;


            dataGridViewContent.GridColor =
                Color.FromArgb(
                    225,
                    230,
                    238);


            dataGridViewContent.RowTemplate.Height =
                38;


            // ========================================================
            // HEADER STYLE
            // ========================================================

            dataGridViewContent.EnableHeadersVisualStyles =
                false;


            dataGridViewContent
                .ColumnHeadersDefaultCellStyle
                .BackColor =
                Color.FromArgb(
                    36,
                    55,
                    85);


            dataGridViewContent
                .ColumnHeadersDefaultCellStyle
                .ForeColor =
                Color.White;


            dataGridViewContent
                .ColumnHeadersDefaultCellStyle
                .Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold);


            dataGridViewContent.ColumnHeadersHeight =
                42;


            // ========================================================
            // ROW STYLE
            // ========================================================

            dataGridViewContent
                .DefaultCellStyle
                .Font =
                new Font(
                    "Segoe UI",
                    9.5F);


            dataGridViewContent
                .DefaultCellStyle
                .SelectionBackColor =
                Color.FromArgb(
                    220,
                    235,
                    255);


            dataGridViewContent
                .DefaultCellStyle
                .SelectionForeColor =
                Color.Black;


            dataGridViewContent
                .AlternatingRowsDefaultCellStyle
                .BackColor =
                Color.FromArgb(
                    248,
                    250,
                    253);


            // ========================================================
            // COLUMNS
            // ========================================================

            dataGridViewContent.Columns.Clear();


            AddTextColumn(
                "#",
                "RowNumber",
                45);


            AddTextColumn(
                "Topic",
                "Topic",
                180);


            AddTextColumn(
                "Language",
                "Language",
                100);


            AddTextColumn(
                "Voice",
                "Voice",
                170);


            AddTextColumn(
                "Template",
                "TemplateName",
                100);


            AddTextColumn(
                "Content Type",
                "ContentType",
                90);


            AddTextColumn(
                "AI Status",
                "AiStatus",
                100);


            AddTextColumn(
                "Validation",
                "ValidationStatus",
                100);


            AddTextColumn(
                "Review",
                "ReviewStatus",
                100);


            AddTextColumn(
                "Process",
                "ProcessStatus",
                100);


            // ========================================================
            // EVENTS
            // ========================================================

            dataGridViewContent.CellFormatting -=
                DataGridViewContent_CellFormatting;


            dataGridViewContent.CellFormatting +=
                DataGridViewContent_CellFormatting;


            dataGridViewContent.CellDoubleClick -=
                dataGridViewContent_CellDoubleClick;


            dataGridViewContent.CellDoubleClick +=
                dataGridViewContent_CellDoubleClick;
        }


        private void AddTextColumn(
            string header,
            string property,
            int fillWeight)
        {
            DataGridViewTextBoxColumn column =
                new DataGridViewTextBoxColumn();


            column.HeaderText =
                header;


            column.DataPropertyName =
                property;


            column.FillWeight =
                fillWeight;


            column.ReadOnly =
                true;


            dataGridViewContent.Columns.Add(
                column);
        }


        // ============================================================
        // GRID STATUS COLORS
        // ============================================================

        private void DataGridViewContent_CellFormatting(
            object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 ||
                e.ColumnIndex < 0)
            {
                return;
            }


            string propertyName =
                dataGridViewContent
                    .Columns[e.ColumnIndex]
                    .DataPropertyName;


            string value =
                e.Value?.ToString()
                ?? "";


            // --------------------------------------------------------
            // AI STATUS
            // --------------------------------------------------------

            if (propertyName ==
                "AiStatus")
            {
                if (value == "Generated")
                {
                    e.CellStyle.ForeColor =
                        Color.RoyalBlue;
                }

                else if (value == "Edited")
                {
                    e.CellStyle.ForeColor =
                        Color.DarkViolet;
                }

                else if (value == "Generating")
                {
                    e.CellStyle.ForeColor =
                        Color.DarkOrange;
                }

                else if (value == "Failed")
                {
                    e.CellStyle.ForeColor =
                        Color.Red;
                }
            }


            // --------------------------------------------------------
            // VALIDATION
            // --------------------------------------------------------

            if (propertyName ==
                "ValidationStatus")
            {
                if (value == "Passed")
                {
                    e.CellStyle.ForeColor =
                        Color.Green;
                }

                else if (value == "Review" ||
                         value == "Not Checked")
                {
                    e.CellStyle.ForeColor =
                        Color.DarkOrange;
                }

                else if (value == "Failed")
                {
                    e.CellStyle.ForeColor =
                        Color.Red;
                }
            }


            // --------------------------------------------------------
            // REVIEW
            // --------------------------------------------------------

            if (propertyName ==
                "ReviewStatus")
            {
                if (value == "Approved")
                {
                    e.CellStyle.ForeColor =
                        Color.Green;


                    e.CellStyle.Font =
                        new Font(
                            dataGridViewContent.Font,
                            FontStyle.Bold);
                }

                else if (value == "Review" ||
                         value == "Waiting")
                {
                    e.CellStyle.ForeColor =
                        Color.DarkOrange;
                }
            }


            // --------------------------------------------------------
            // PROCESS
            // --------------------------------------------------------

            if (propertyName ==
                "ProcessStatus")
            {
                if (value == "Ready" ||
                    value == "Completed")
                {
                    e.CellStyle.ForeColor =
                        Color.Green;
                }

                else if (value == "Processing")
                {
                    e.CellStyle.ForeColor =
                        Color.Blue;
                }

                else if (value == "Attention")
                {
                    e.CellStyle.ForeColor =
                        Color.DarkOrange;
                }

                else if (value == "Error")
                {
                    e.CellStyle.ForeColor =
                        Color.Red;
                }
            }
        }


        // ============================================================
        // AI GENERATION BUTTON
        // ============================================================

        private void btnAiContent_Click(
            object sender,
            EventArgs e)
        {
            string selectedType =
                cmbContentFilter.SelectedItem
                    ?.ToString()
                ?? "All";


            // AI processing is deliberately disabled for All.
            if (!selectedType.Equals(
                    "Guide",
                    StringComparison.OrdinalIgnoreCase)
                &&
                !selectedType.Equals(
                    "Quiz",
                    StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "Please select Guide or Quiz first.",
                    "AI Content Generation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            if (_filteredItems == null ||
                _filteredItems.Count == 0)
            {
                MessageBox.Show(
                    $"No {selectedType} content was found.",
                    "AI Content Generation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }


            // ========================================================
            // IMPORTANT
            //
            // Pass ONLY currently filtered records.
            //
            // Guide selected:
            //   Guide records only.
            //
            // Quiz selected:
            //   Quiz records only.
            // ========================================================

            List<ContentItem> itemsToProcess =
                _filteredItems.ToList();


            // If user has selected Guide/Quiz #3,
            // AI review form opens at that item.
            int startIndex =
                GetSelectedStartIndex(
                    itemsToProcess);


            AiPreference preference =
                new AiPreference
                {
                    AgeGroup =
                        "General Visitor",

                    KnowledgeLevel =
                        "Beginner",

                    Tone =
                        "Clear and educational",

                    MaximumWords =
                        120
                };


            using (AiContentReviewForm form =
                   new AiContentReviewForm(
                       itemsToProcess,
                       startIndex,
                       _repository,
                       _aiService,
                       preference))
            {
                form.ShowDialog();
            }


            // Objects passed to AiContentReviewForm are the
            // same object instances stored in _batchItems.
            //
            // Therefore Generated / Approved / Edited status
            // is already updated when the form closes.

            ApplyContentFilter();


            dataGridViewContent.Refresh();
        }


        // ============================================================
        // CURRENT SELECTED ITEM INDEX
        // ============================================================

        private int GetSelectedStartIndex(
            List<ContentItem> items)
        {
            if (dataGridViewContent.CurrentRow ==
                null)
            {
                return 0;
            }


            ContentItem selected =
                dataGridViewContent
                    .CurrentRow
                    .DataBoundItem
                as ContentItem;


            if (selected == null)
            {
                return 0;
            }


            int index =
                items.IndexOf(
                    selected);


            return index >= 0
                ? index
                : 0;
        }


        // ============================================================
        // DOUBLE CLICK
        // ============================================================

        private void dataGridViewContent_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }


            btnAiContent.PerformClick();
        }


        // ============================================================
        // OPTIONAL OLD GENERATE BUTTON
        // ============================================================

        private void btnGenerateAll_Click(
            object sender,
            EventArgs e)
        {
            // If your Designer still points to this event,
            // redirect it to the new AI button.

            btnAiContent.PerformClick();
        }


        // ============================================================
        // CLOSE
        // ============================================================

        private void btnClose_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }


        // ============================================================
        // LOAD
        // ============================================================

        private void ContentDataForm_Load(
            object sender,
            EventArgs e)
        {
            // Everything is initialized by constructor.
        }

        private void ExitBnt_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}