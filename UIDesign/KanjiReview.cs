using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1.UIDesign
{
    public partial class KanjiReview : Form
    {
        private readonly List<Inputtext.KanjiItem> _kanjiList;
        private readonly string _fixedListPath;

        private DataGridView dgvKanji;
        private Button btnSaveApproved;
        private Button btnSelectAllNew;
        private Button btnClose;

        public int SavedCount { get; private set; }

        public KanjiReview(List<Inputtext.KanjiItem> kanjiList, string fixedListPath)
        {
            InitializeComponent();

            _kanjiList = kanjiList ?? new List<Inputtext.KanjiItem>();
            _fixedListPath = fixedListPath;

            BuildKanjiGrid();
            LoadKanjiList();
        }

        private void BuildKanjiGrid()
        {
            dgvKanji = new DataGridView();
            dgvKanji.Location = new Point(20, 100);
            dgvKanji.Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 180);
            dgvKanji.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvKanji.AllowUserToAddRows = false;
            dgvKanji.RowHeadersVisible = false;
            dgvKanji.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKanji.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKanji.ReadOnly = false;

            var chk = new DataGridViewCheckBoxColumn();
            chk.Name = "save";
            chk.HeaderText = "Save";
            chk.FillWeight = 25;
            dgvKanji.Columns.Add(chk);

            dgvKanji.Columns.Add("word", "Word");
            dgvKanji.Columns.Add("hiragana", "Correct Hiragana");
            dgvKanji.Columns.Add("source", "Source");
            dgvKanji.Columns.Add("difficulty", "Difficulty");
            dgvKanji.Columns.Add("reason", "Reason");

            dgvKanji.Columns["word"].ReadOnly = true;
            dgvKanji.Columns["source"].ReadOnly = true;
            dgvKanji.Columns["difficulty"].ReadOnly = false;
            dgvKanji.Columns["reason"].ReadOnly = true;

            dgvKanji.Columns["save"].FillWeight = 25;
            dgvKanji.Columns["word"].FillWeight = 80;
            dgvKanji.Columns["hiragana"].FillWeight = 90;
            dgvKanji.Columns["source"].FillWeight = 50;
            dgvKanji.Columns["difficulty"].FillWeight = 60;
            dgvKanji.Columns["reason"].FillWeight = 180;

            this.Controls.Add(dgvKanji);

            btnSelectAllNew = new Button();
            btnSelectAllNew.Text = "Select All New";
            btnSelectAllNew.Size = new Size(160, 40);
            btnSelectAllNew.Location = new Point(20, this.ClientSize.Height - 60);
            btnSelectAllNew.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSelectAllNew.Click += btnSelectAllNew_Click;
            this.Controls.Add(btnSelectAllNew);

            btnSaveApproved = new Button();
            btnSaveApproved.Text = "Save Approved to Excel";
            btnSaveApproved.Size = new Size(230, 40);
            btnSaveApproved.Location = new Point(200, this.ClientSize.Height - 60);
            btnSaveApproved.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSaveApproved.BackColor = Color.Orange;
            btnSaveApproved.Click += btnSaveApproved_Click;
            this.Controls.Add(btnSaveApproved);

            btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Size = new Size(120, 40);
            btnClose.Location = new Point(this.ClientSize.Width - 150, this.ClientSize.Height - 60);
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Click += delegate { this.Close(); };
            this.Controls.Add(btnClose);
        }

        private void LoadKanjiList()
        {
            dgvKanji.Rows.Clear();

            foreach (var item in _kanjiList)
            {
                bool saveDefault = item.Source != "Fixed";

                int rowIndex = dgvKanji.Rows.Add(
                    saveDefault,
                    item.Word,
                    item.Hiragana,
                    item.Source,
                    item.Difficulty,
                    item.Reason
                );

                var row = dgvKanji.Rows[rowIndex];
                row.Tag = item;

                string source = item.Source == null ? "" : item.Source.ToLower();
                string difficulty = item.Difficulty == null ? "" : item.Difficulty.ToLower();

                if (source == "fixed")
                    row.DefaultCellStyle.BackColor = Color.Honeydew;
                else if (difficulty == "high")
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                else if (difficulty == "medium")
                    row.DefaultCellStyle.BackColor = Color.LemonChiffon;
                else
                    row.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void btnSelectAllNew_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvKanji.Rows)
            {
                string source = Convert.ToString(row.Cells["source"].Value);

                if (source != "Fixed")
                    row.Cells["save"].Value = true;
            }
        }

        private void btnSaveApproved_Click(object sender, EventArgs e)
        {
            try
            {
                dgvKanji.EndEdit();

                var approved = new List<ApprovedWord>();

                foreach (DataGridViewRow row in dgvKanji.Rows)
                {
                    bool shouldSave = false;

                    if (row.Cells["save"].Value is bool)
                        shouldSave = (bool)row.Cells["save"].Value;

                    if (!shouldSave)
                        continue;

                    string word = Convert.ToString(row.Cells["word"].Value).Trim();
                    string hiragana = NormalizeToHiragana(Convert.ToString(row.Cells["hiragana"].Value).Trim());
                    string difficulty = Convert.ToString(row.Cells["difficulty"].Value).Trim();

                    if (string.IsNullOrWhiteSpace(word))
                        continue;

                    if (string.IsNullOrWhiteSpace(hiragana))
                    {
                        MessageBox.Show(
                            $"Hiragana is empty for word: {word}",
                            "Validation Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    approved.Add(new ApprovedWord
                    {
                        Word = word,
                        Hiragana = hiragana,
                        Difficulty = string.IsNullOrWhiteSpace(difficulty) ? "general" : difficulty
                    });
                }

                if (approved.Count == 0)
                {
                    MessageBox.Show(
                        "No words selected. Please check Save column first.",
                        "No Selection",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                SaveApprovedWordsToExcel(_fixedListPath, approved);

                SavedCount = approved.Count;

                MessageBox.Show(
                    $"Saved/updated {approved.Count} word(s) to Fixed List Excel.\n\nNext time these words will be Fixed List words.",
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Excel Save Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SaveApprovedWordsToExcel(string path, List<ApprovedWord> approvedWords)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new Exception("Fixed List Excel path is empty.");

            string folder = Path.GetDirectoryName(path);

            if (!string.IsNullOrWhiteSpace(folder))
                Directory.CreateDirectory(folder);

            if (File.Exists(path))
            {
                string backupPath = path + ".backup_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                File.Copy(path, backupPath, true);
            }

            XLWorkbook workbook;
            IXLWorksheet ws;

            if (File.Exists(path))
            {
                workbook = new XLWorkbook(path);
                ws = workbook.Worksheet(1);
            }
            else
            {
                workbook = new XLWorkbook();
                ws = workbook.Worksheets.Add("FixedList");
            }

            EnsureHeaders(ws);

            var rowMap = BuildWordRowMap(ws);

            foreach (var item in approvedWords)
            {
                if (rowMap.ContainsKey(item.Word))
                {
                    int row = rowMap[item.Word];

                    ws.Cell(row, 1).Value = item.Word;
                    ws.Cell(row, 2).Value = item.Hiragana;
                    ws.Cell(row, 3).Value = item.Difficulty;
                    ws.Cell(row, 4).Value = "Approved";
                    ws.Cell(row, 5).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    int newRow = GetNextRow(ws);

                    ws.Cell(newRow, 1).Value = item.Word;
                    ws.Cell(newRow, 2).Value = item.Hiragana;
                    ws.Cell(newRow, 3).Value = item.Difficulty;
                    ws.Cell(newRow, 4).Value = "Approved";
                    ws.Cell(newRow, 5).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    rowMap[item.Word] = newRow;
                }
            }

            ws.Columns().AdjustToContents();
            workbook.SaveAs(path);
            workbook.Dispose();
        }

        private void EnsureHeaders(IXLWorksheet ws)
        {
            if (string.IsNullOrWhiteSpace(ws.Cell(1, 1).GetString()))
                ws.Cell(1, 1).Value = "word";

            if (string.IsNullOrWhiteSpace(ws.Cell(1, 2).GetString()))
                ws.Cell(1, 2).Value = "hiragana";

            if (string.IsNullOrWhiteSpace(ws.Cell(1, 3).GetString()))
                ws.Cell(1, 3).Value = "difficulty";

            if (string.IsNullOrWhiteSpace(ws.Cell(1, 4).GetString()))
                ws.Cell(1, 4).Value = "status";

            if (string.IsNullOrWhiteSpace(ws.Cell(1, 5).GetString()))
                ws.Cell(1, 5).Value = "updated_at";
        }

        private Dictionary<string, int> BuildWordRowMap(IXLWorksheet ws)
        {
            var map = new Dictionary<string, int>(StringComparer.Ordinal);

            var lastRow = ws.LastRowUsed();

            if (lastRow == null)
                return map;

            for (int row = 2; row <= lastRow.RowNumber(); row++)
            {
                string word = ws.Cell(row, 1).GetString().Trim();

                if (string.IsNullOrWhiteSpace(word))
                    continue;

                if (!map.ContainsKey(word))
                    map.Add(word, row);
            }

            return map;
        }

        private int GetNextRow(IXLWorksheet ws)
        {
            var lastRow = ws.LastRowUsed();

            if (lastRow == null)
                return 2;

            return lastRow.RowNumber() + 1;
        }

        private static string NormalizeToHiragana(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var sb = new StringBuilder();

            foreach (char c in text.Trim())
            {
                if (c >= '\u30A1' && c <= '\u30F6')
                    sb.Append((char)(c - 0x60));
                else
                    sb.Append(c);
            }

            return sb.ToString();
        }

        private class ApprovedWord
        {
            public string Word { get; set; }
            public string Hiragana { get; set; }
            public string Difficulty { get; set; }
        }
    }
}