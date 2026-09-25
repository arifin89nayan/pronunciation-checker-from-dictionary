using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Models.AutoConverters;

namespace WindowsFormsApp1
{
    public partial class ContentDataForm : Form
    {
        private readonly BindingList<ContentItem>
            _batchItems;

        private readonly List<WidgetParsedCommonModel>
            _originalRecords;

        private readonly List<LanguageParsedModel>
            _languages;

        private readonly string _workspace;
        private readonly string _templateFolder;
        private readonly string _audioFolder;
        private readonly string _outputFolder;
        private readonly string _ibcFolder;

        private DataGridView dataGridViewContent;

        public ContentDataForm(
            List<ContentItem> items,
            List<WidgetParsedCommonModel> originalRecords,
            List<LanguageParsedModel> languages,
            string workspace,
            string templateFolder,
            string audioFolder,
            string outputFolder,
            string ibcFolder)
        {
            InitializeComponent();

            dataGridViewContent = new DataGridView
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(dataGridViewContent);

            _batchItems =
                new BindingList<ContentItem>(
                    items);

            _originalRecords =
                originalRecords;

            _languages =
                languages;

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

            LoadBatchData();
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

            dataGridViewContent.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dataGridViewContent.Columns.Clear();


            AddTextColumn(
                "#",
                "RowNumber",
                40);

            AddTextColumn(
                "Topic",
                "Topic",
                170);

            AddTextColumn(
                "Language",
                "Language",
                100);

            AddTextColumn(
                "Voice",
                "Voice",
                160);

            AddTextColumn(
                "Template Name",
                "TemplateName",
                110);

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
                "Start Process",
                "ProcessStatus",
                100);


            dataGridViewContent.CellFormatting +=
                DataGridViewContent_CellFormatting;
        }


        private void AddTextColumn(
            string header,
            string property,
            int width)
        {
            DataGridViewTextBoxColumn column =
                new DataGridViewTextBoxColumn();

            column.HeaderText = header;
            column.DataPropertyName = property;
            column.Width = width;

            dataGridViewContent.Columns.Add(
                column);
        }


        private void LoadBatchData()
        {
            dataGridViewContent.DataSource =
                _batchItems;

            //lblDetectedRecords.Text =$"Detected Records: {_batchItems.Count}";
        }


        // ============================================================
        // STATUS COLORS
        // ============================================================

        private void DataGridViewContent_CellFormatting(
            object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            string propertyName =
                dataGridViewContent
                    .Columns[e.ColumnIndex]
                    .DataPropertyName;


            if (propertyName == "ReviewStatus" ||
                propertyName == "ProcessStatus" ||
                propertyName == "AiStatus" ||
                propertyName == "ValidationStatus")
            {
                string value =
                    e.Value?.ToString() ?? "";


                if (value == "Approved" ||
                    value == "Ready" ||
                    value == "Generated" ||
                    value == "Passed")
                {
                    e.CellStyle.ForeColor =
                        Color.Green;
                }

                else if (value == "Review" ||
                         value == "Waiting" ||
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
        }


        // ============================================================
        // GENERATE ALL
        //
        // NEXT STEP:
        // OpenAI API will be connected here.
        // ============================================================

        private void btnGenerateAll_Click(
            object sender,
            EventArgs e)
        {
            MessageBox.Show(
                $"Next step: generate {_batchItems.Count} " +
                "records using the AI API.",
                "AI Batch Generation",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }


        private void btnClose_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }

        // Add this method to handle the Load event and resolve CS1061.
        private void ContentDataForm_Load(object sender, EventArgs e)
        {
            // You can add initialization code here if needed.
        }
    }
}