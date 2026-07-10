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
    /// <summary>
    /// Generic "Reading Review" screen. Works for any language:
    /// the reading column header, normalization, and Excel round-trip
    /// all come from the ILanguageProfile.
    /// </summary>
    public partial class KanjiReview : Form
    {
        private readonly List<Inputtext.KanjiItem> _kanjiList;
        private readonly string _fixedListPath;
        private readonly ILanguageProfile _profile;

        private DataGridView dgvKanji;
        private Button btnSelectReviewRequired;
        private Button btnSaveApproved;
        private Button btnClose;

        public int SavedCount { get; private set; }

        public List<Inputtext.KanjiItem> ReviewedItems
        {
            get
            {
                SyncGridToItems();
                return _kanjiList;
            }
        }

        /// <summary>Backward-compatible constructor: Japanese.</summary>
        public KanjiReview(List<Inputtext.KanjiItem> kanjiList, string fixedListPath)
            : this(kanjiList, fixedListPath, LanguageRegistry.Default)
        {
        }

        public KanjiReview(List<Inputtext.KanjiItem> kanjiList,
                           string fixedListPath,
                           ILanguageProfile profile)
        {
            InitializeComponent();

            _kanjiList = kanjiList ?? new List<Inputtext.KanjiItem>();
            _fixedListPath = fixedListPath;
            _profile = profile ?? LanguageRegistry.Default;

            this.Text = "Reading Review - " + _profile.DisplayName;

            BuildKanjiGrid();
            LoadKanjiList();
        }

        private void SyncGridToItems()
        {
            foreach (DataGridViewRow row in dgvKanji.Rows)
            {
                Inputtext.KanjiItem item = row.Tag as Inputtext.KanjiItem;

                if (item == null)
                    continue;

                item.Word = _profile.NormalizeText(
                    Convert.ToString(row.Cells["word"].Value)
                );

                item.Hiragana = _profile.NormalizeReading(
                    Convert.ToString(row.Cells["hiragana"].Value)
                );

                item.Source = Convert.ToString(row.Cells["source"].Value);
                item.DictionaryStatus = Convert.ToString(row.Cells["status"].Value);
                item.ModelHiragana = Convert.ToString(row.Cells["model"].Value);
                item.Difficulty = Convert.ToString(row.Cells["difficulty"].Value);
                item.Reason = Convert.ToString(row.Cells["reason"].Value);

                bool shouldSave = false;

                if (row.Cells["save"].Value is bool)
                    shouldSave = (bool)row.Cells["save"].Value;

                item.SaveToFixedList = shouldSave;
            }
        }

        private void dgvKanji_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvKanji.Columns[e.ColumnIndex].Name == "hiragana")
            {
                dgvKanji.Rows[e.RowIndex].Cells["save"].Value = true;
                dgvKanji.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LemonChiffon;
            }
        }

        private void BuildKanjiGrid()
        {
            dgvKanji = new DataGridView();
            dgvKanji.Location = new Point(20, 130);
            dgvKanji.Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 240);
            dgvKanji.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvKanji.AllowUserToAddRows = false;
            dgvKanji.RowHeadersVisible = false;
            dgvKanji.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKanji.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKanji.ReadOnly = false;
            dgvKanji.CellEndEdit += dgvKanji_CellEndEdit;

            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.Name = "save";
            chk.HeaderText = "Save";
            chk.FillWeight = 25;
            dgvKanji.Columns.Add(chk);

            dgvKanji.Columns.Add("word", "Word");
            dgvKanji.Columns.Add("hiragana", _profile.ReadingColumnHeader);
            dgvKanji.Columns.Add("source", "Source");
            dgvKanji.Columns.Add("status", "Status");
            dgvKanji.Columns.Add("model", "Model Reading");
            dgvKanji.Columns.Add("difficulty", "Difficulty");
            dgvKanji.Columns.Add("reason", "Reason");

            dgvKanji.Columns["word"].ReadOnly = true;
            dgvKanji.Columns["source"].ReadOnly = true;
            dgvKanji.Columns["status"].ReadOnly = true;
            dgvKanji.Columns["model"].ReadOnly = true;
            dgvKanji.Columns["reason"].ReadOnly = true;

            dgvKanji.Columns["save"].FillWeight = 25;
            dgvKanji.Columns["word"].FillWeight = 80;
            dgvKanji.Columns["hiragana"].FillWeight = 80;
            dgvKanji.Columns["source"].FillWeight = 50;
            dgvKanji.Columns["status"].FillWeight = 60;
            dgvKanji.Columns["model"].FillWeight = 80;
            dgvKanji.Columns["difficulty"].FillWeight = 60;
            dgvKanji.Columns["reason"].FillWeight = 200;

            this.Controls.Add(dgvKanji);

            btnSelectReviewRequired = new Button();
            btnSelectReviewRequired.Text = "Select Review Items";
            btnSelectReviewRequired.Size = new Size(190, 40);
            btnSelectReviewRequired.Location = new Point(20, this.ClientSize.Height - 60);
            btnSelectReviewRequired.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSelectReviewRequired.Click += btnSelectReviewRequired_Click;
            this.Controls.Add(btnSelectReviewRequired);

            btnSaveApproved = new Button();
            btnSaveApproved.Text = "Save Approved to Excel";
            btnSaveApproved.Size = new Size(230, 40);
            btnSaveApproved.Location = new Point(230, this.ClientSize.Height - 60);
            btnSaveApproved.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSaveApproved.BackColor = Color.Orange;
            btnSaveApproved.Click += btnSaveApproved_Click;
            this.Controls.Add(btnSaveApproved);

            btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Size = new Size(120, 40);
            btnClose.Location = new Point(this.ClientSize.Width - 150, this.ClientSize.Height - 60);
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Click += delegate
            {
                SyncGridToItems();
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            this.Controls.Add(btnClose);
        }

        private void LoadKanjiList()
        {
            dgvKanji.Rows.Clear();

            foreach (var item in _kanjiList)
            {
                bool saveDefault = item.SaveToFixedList || item.DictionaryStatus == "new";

                int rowIndex = dgvKanji.Rows.Add(
                    saveDefault,
                    item.Word,
                    item.Hiragana,
                    item.Source,
                    item.DictionaryStatus,
                    item.ModelHiragana,
                    item.Difficulty,
                    item.Reason
                );

                DataGridViewRow row = dgvKanji.Rows[rowIndex];
                row.Tag = item;

                ApplyRowColor(row, item);
            }
        }

        private void ApplyRowColor(DataGridViewRow row, Inputtext.KanjiItem item)
        {
            string status = item.DictionaryStatus ?? "";
            string difficulty = item.Difficulty == null ? "" : item.Difficulty.ToLower();

            if (status == "matched")
            {
                row.DefaultCellStyle.BackColor = Color.Honeydew;
            }
            else if (status == "conflict")
            {
                row.DefaultCellStyle.BackColor = Color.MistyRose;
            }
            else if (difficulty == "high")
            {
                row.DefaultCellStyle.BackColor = Color.MistyRose;
            }
            else if (difficulty == "medium")
            {
                row.DefaultCellStyle.BackColor = Color.LemonChiffon;
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void btnSelectReviewRequired_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvKanji.Rows)
            {
                Inputtext.KanjiItem item = row.Tag as Inputtext.KanjiItem;

                if (item == null)
                    continue;

                if (item.ReviewRequired || item.SaveToFixedList || item.DictionaryStatus == "new")
                    row.Cells["save"].Value = true;
            }
        }

        private void btnSaveApproved_Click(object sender, EventArgs e)
        {
            try
            {
                dgvKanji.EndEdit();

                List<ApprovedWord> approved = new List<ApprovedWord>();

                foreach (DataGridViewRow row in dgvKanji.Rows)
                {
                    bool shouldSave = false;

                    if (row.Cells["save"].Value is bool)
                        shouldSave = (bool)row.Cells["save"].Value;

                    if (!shouldSave)
                        continue;

                    string word = Convert.ToString(row.Cells["word"].Value).Trim();

                    string reading = _profile.NormalizeReading(
                        Convert.ToString(row.Cells["hiragana"].Value).Trim()
                    );

                    string difficulty = Convert.ToString(row.Cells["difficulty"].Value).Trim();

                    if (string.IsNullOrWhiteSpace(word))
                        continue;

                    if (string.IsNullOrWhiteSpace(reading))
                    {
                        MessageBox.Show(
                            "Reading is empty for word: " + word,
                            "Validation Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    approved.Add(new ApprovedWord
                    {
                        Word = _profile.NormalizeText(word),
                        Hiragana = reading,
                        Difficulty = string.IsNullOrWhiteSpace(difficulty) ? "general" : difficulty
                    });
                }

                if (approved.Count == 0)
                {
                    MessageBox.Show(
                        "No rows selected. Please check the Save box for the row you want to update.",
                        "No Selection",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                SaveApprovedWordsToExcel(_fixedListPath, approved);

                SavedCount = approved.Count;

                MessageBox.Show(
                    "Saved/updated " + approved.Count +
                    " word(s) to Fixed List Excel.\n\nNext time these words will use the updated Fixed List reading.",
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Update both the grid AND the underlying KanjiItem so a second
                // "Select Review Items" click won't re-flag rows already saved.
                foreach (DataGridViewRow row in dgvKanji.Rows)
                {
                    if (row.Cells["save"].Value is bool b && b)
                    {
                        if (row.Tag is Inputtext.KanjiItem item)
                        {
                            item.DictionaryStatus = "matched";
                            item.Source = "Fixed";
                            item.ReviewRequired = false;
                            item.SaveToFixedList = false;
                        }

                        row.Cells["status"].Value = "matched";
                        row.Cells["source"].Value = "Fixed";
                        row.Cells["save"].Value = false;
                        row.DefaultCellStyle.BackColor = Color.Honeydew;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Excel Save Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            SyncGridToItems();
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

            XLWorkbook workbook = File.Exists(path)
                ? new XLWorkbook(path)
                : new XLWorkbook();

            try
            {
                IXLWorksheet ws = File.Exists(path)
                    ? workbook.Worksheet(1)
                    : workbook.Worksheets.Add("FixedList");

                EnsureHeaders(ws);

                Dictionary<string, int> rowMap = BuildWordRowMap(ws);

                foreach (ApprovedWord item in approvedWords)
                {
                    string key = _profile.NormalizeText(item.Word);

                    if (rowMap.ContainsKey(key))
                    {
                        int row = rowMap[key];

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

                        rowMap[key] = newRow;
                    }
                }

                ws.Columns().AdjustToContents();
                workbook.SaveAs(path);
            }
            finally
            {
                workbook.Dispose();
            }
        }

        private void EnsureHeaders(IXLWorksheet ws)
        {
            if (string.IsNullOrWhiteSpace(ws.Cell(1, 1).GetString()))
                ws.Cell(1, 1).Value = "word";

            if (string.IsNullOrWhiteSpace(ws.Cell(1, 2).GetString()))
                ws.Cell(1, 2).Value = "reading";

            if (string.IsNullOrWhiteSpace(ws.Cell(1, 3).GetString()))
                ws.Cell(1, 3).Value = "difficulty";

            if (string.IsNullOrWhiteSpace(ws.Cell(1, 4).GetString()))
                ws.Cell(1, 4).Value = "status";

            if (string.IsNullOrWhiteSpace(ws.Cell(1, 5).GetString()))
                ws.Cell(1, 5).Value = "updated_at";
        }

        private Dictionary<string, int> BuildWordRowMap(IXLWorksheet ws)
        {
            StringComparer cmp = _profile.UsesWordBoundaryMatching
                ? StringComparer.OrdinalIgnoreCase
                : StringComparer.Ordinal;

            Dictionary<string, int> map = new Dictionary<string, int>(cmp);

            IXLRow lastRow = ws.LastRowUsed();

            if (lastRow == null)
                return map;

            for (int row = 2; row <= lastRow.RowNumber(); row++)
            {
                string word = _profile.NormalizeText(
                    ws.Cell(row, 1).GetString()
                );

                if (string.IsNullOrWhiteSpace(word))
                    continue;

                if (!map.ContainsKey(word))
                    map.Add(word, row);
            }

            return map;
        }

        private int GetNextRow(IXLWorksheet ws)
        {
            IXLRow lastRow = ws.LastRowUsed();

            if (lastRow == null)
                return 2;

            return lastRow.RowNumber() + 1;
        }

        private class ApprovedWord
        {
            public string Word { get; set; }
            public string Hiragana { get; set; }
            public string Difficulty { get; set; }
        }

        private void Back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            try
            {
                dgvKanji.EndEdit();

                SyncGridToItems();

                var missing = _kanjiList
                    .Where(x => !string.IsNullOrWhiteSpace(x.Word))
                    .Where(x => string.IsNullOrWhiteSpace(x.Hiragana))
                    .Select(x => x.Word)
                    .ToList();

                if (missing.Count > 0)
                {
                    MessageBox.Show(
                        "Some words have an empty reading. Please fix them first:\n\n" +
                        string.Join("\n", missing),
                        "Missing Reading",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Next Error");
            }
        }
    }
}
