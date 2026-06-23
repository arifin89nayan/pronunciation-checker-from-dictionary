using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1.UIDesign
{
    public partial class KanjiReview : Form
    {
        private readonly List<Inputtext.KanjiItem> _kanjiList;
        private DataGridView dgvKanji;
        private Button btnClose;

        public KanjiReview(List<Inputtext.KanjiItem> kanjiList)
        {
            InitializeComponent();

            _kanjiList = kanjiList ?? new List<Inputtext.KanjiItem>();

            BuildKanjiGrid();
            LoadKanjiList();
        }

        private void BuildKanjiGrid()
        {
            dgvKanji = new DataGridView();
            dgvKanji.Location = new Point(20, 80);
            dgvKanji.Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 150);
            dgvKanji.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvKanji.AllowUserToAddRows = false;
            dgvKanji.RowHeadersVisible = false;
            dgvKanji.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKanji.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKanji.ReadOnly = false;

            dgvKanji.Columns.Add("word", "Word");
            dgvKanji.Columns.Add("hiragana", "Hiragana");
            //dgvKanji.Columns.Add("romaji", "Romaji");        
            dgvKanji.Columns.Add("difficulty", "Difficulty");
            //dgvKanji.Columns.Add("review", "Review");
            dgvKanji.Columns.Add("reason", "Reason");

            this.Controls.Add(dgvKanji);

            btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Size = new Size(120, 40);
            btnClose.Location = new Point(this.ClientSize.Width - 150, this.ClientSize.Height - 55);
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Click += delegate { this.Close(); };

            this.Controls.Add(btnClose);
        }

        private void LoadKanjiList()
        {
            dgvKanji.Rows.Clear();

            foreach (var item in _kanjiList)
            {
                int rowIndex = dgvKanji.Rows.Add(
                    item.Word,
                    item.Hiragana,
                    item.Difficulty,
                    item.Reason
                );

                var row = dgvKanji.Rows[rowIndex];

                string difficulty = item.Difficulty == null ? "" : item.Difficulty.ToLower();

                if (difficulty == "high")
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                else if (difficulty == "medium")
                    row.DefaultCellStyle.BackColor = Color.LemonChiffon;
                else
                    row.DefaultCellStyle.BackColor = Color.Honeydew;
            }
        }

       
    }
}