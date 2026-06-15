using PPT;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1.UIDesign.Screens
{
    public partial class DictionaryManagerScreen : UserControl, IScreen
    {
        private readonly AppState _state;

        public DictionaryManagerScreen()
        {
            InitializeComponent();
        }

        public DictionaryManagerScreen(AppState state) : this()
        {
            _state = state;

            Theme.StyleGrid(dgvDict);

            dgvDict.Columns.Add("no", "No");
            dgvDict.Columns.Add("word", "Word");
            dgvDict.Columns.Add("hira", "Hiragana");
            dgvDict.Columns.Add("cat", "Category");
            dgvDict.Columns.Add("upd", "Updated");

            dgvDict.Columns[0].FillWeight = 20;

            cmbCategory.Items.AddRange(new object[]
            {
                "All", "Place", "History", "Culture", "Technical", "General"
            });

            cmbCategory.SelectedIndex = 0;

            cmbCategory.SelectedIndexChanged += delegate { Bind(); };
            txtSearch.TextChanged += delegate { Bind(); };

            LoadDefaultFixedListFromResources();
        }

        public void OnShown()
        {
            Bind();
        }

        private void LoadDefaultFixedListFromResources()
        {
            try
            {
                if (_state == null || _state.Dictionary == null)
                    return;

                if (!string.IsNullOrWhiteSpace(_state.Dictionary.BackingCsvPath))
                    return;

                string defaultExcelPath = Path.Combine(
                    System.Windows.Forms.Application.StartupPath,
                    "Resources",
                    "fixed_list.xlsx"
                );

                if (!File.Exists(defaultExcelPath))
                    return;

                _state.Dictionary.LoadExcel(defaultExcelPath);
                _state.RebuildExtractor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Default fixed list load failed:\n" + ex.Message,
                    "Dictionary",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void Bind()
        {
            dgvDict.Rows.Clear();

            if (_state == null || _state.Dictionary == null)
                return;

            string q = txtSearch.Text.Trim();
            string cat = cmbCategory.SelectedItem != null
                ? cmbCategory.SelectedItem.ToString()
                : "All";

            int n = 1;

            foreach (var ent in _state.Dictionary.All
                .Where(en =>
                    (cat == "All" || en.Category == cat) &&
                    (
                        q.Length == 0 ||
                        en.Word.Contains(q) ||
                        ((en.Hiragana ?? "").Contains(q))
                    )))
            {
                int idx = dgvDict.Rows.Add(
                    n++,
                    ent.Word,
                    ent.Hiragana,
                    ent.Category,
                    ent.Updated.ToString("yyyy-MM-dd")
                );

                dgvDict.Rows[idx].Tag = ent;
            }

            string fileInfo = "";

            if (!string.IsNullOrWhiteSpace(_state.Dictionary.BackingCsvPath))
            {
                fileInfo =
                    "\nFile: " + _state.Dictionary.BackingCsvPath;
            }

            lblStatus.Text =
                $"{dgvDict.Rows.Count} term(s) shown · {_state.Dictionary.Count} total" +
                fileInfo;
        }

        private WindowsFormsApp1.Models.DictionaryEntry Selected()
        {
            if (dgvDict.SelectedRows.Count > 0 &&
                dgvDict.SelectedRows[0].Tag is WindowsFormsApp1.Models.DictionaryEntry entry)
            {
                return entry;
            }

            return null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var entry = new WindowsFormsApp1.Models.DictionaryEntry();

            if (EditDialog(entry, "Add New Term"))
            {
                _state.Dictionary.AddOrUpdate(entry);
                Save();
                Bind();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var sel = Selected();

            if (sel == null)
            {
                Info("Please select one dictionary term first.");
                return;
            }

            if (EditDialog(sel, "Edit Term"))
            {
                _state.Dictionary.AddOrUpdate(sel);
                Save();
                Bind();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var sel = Selected();

            if (sel == null)
            {
                Info("Please select one dictionary term first.");
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Delete '{sel.Word}'?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                _state.Dictionary.Backup(_state.Config.OutputFolder);
                _state.Dictionary.Remove(sel.Word);
                Save();
                Bind();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Title = "Import Fixed List",
                Filter = "Excel files (*.xlsx)|*.xlsx|CSV files (*.csv)|*.csv",
                Multiselect = false
            };

            try
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                string ext = Path.GetExtension(dlg.FileName).ToLowerInvariant();

                if (ext == ".xlsx")
                {
                    _state.Dictionary.LoadExcel(dlg.FileName);
                }
                else if (ext == ".csv")
                {
                    _state.Dictionary.LoadCsv(dlg.FileName);
                }
                else
                {
                    Info("Unsupported file type. Please select .xlsx or .csv file.");
                    return;
                }

                _state.RebuildExtractor();
                Bind();

                lblStatus.Text =
                    $"Imported {_state.Dictionary.Count} terms.\nFile: {_state.Dictionary.BackingCsvPath}";
            }
            catch (Exception ex)
            {
                Info("Import failed: " + ex.Message);
            }
            finally
            {
                dlg.Dispose();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "PLS XML (*.xml)|*.xml",
                FileName = "lexicon.xml"
            };

            try
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                _state.Dictionary.ExportPlsXml(dlg.FileName);

                lblStatus.Text =
                    "Exported PLS lexicon: " + Path.GetFileName(dlg.FileName);
            }
            catch (Exception ex)
            {
                Info("Export failed: " + ex.Message);
            }
            finally
            {
                dlg.Dispose();
            }
        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(_state.Dictionary.BackingCsvPath))
            {
                SaveFileDialog dlg = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx|CSV files (*.csv)|*.csv",
                    FileName = "fixed_list.xlsx"
                };

                try
                {
                    if (dlg.ShowDialog() != DialogResult.OK)
                        return;

                    SaveByExtension(dlg.FileName);
                }
                finally
                {
                    dlg.Dispose();
                }
            }
            else
            {
                SaveByExtension(_state.Dictionary.BackingCsvPath);
            }

            _state.RebuildExtractor();
        }

        private void SaveByExtension(string path)
        {
            string ext = Path.GetExtension(path).ToLowerInvariant();

            if (ext == ".xlsx")
            {
                _state.Dictionary.SaveExcel(path);
            }
            else if (ext == ".csv")
            {
                _state.Dictionary.SaveCsv(path);
            }
            else
            {
                _state.Dictionary.SaveExcel(path + ".xlsx");
            }
        }

        private static bool EditDialog(Models.DictionaryEntry entry, string title)
        {
            if (entry == null)
                return false;

            using (var f = new Form
            {
                Text = title,
                Width = 380,
                Height = 250,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            })
            {
                var word = new TextBox
                {
                    Left = 120,
                    Top = 20,
                    Width = 220,
                    Text = entry.Word,
                    Font = new Font("Yu Gothic UI", 10f)
                };

                var hira = new TextBox
                {
                    Left = 120,
                    Top = 55,
                    Width = 220,
                    Text = entry.Hiragana,
                    Font = new Font("Yu Gothic UI", 10f)
                };

                var cat = new ComboBox
                {
                    Left = 120,
                    Top = 90,
                    Width = 220,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };

                cat.Items.AddRange(new object[]
                {
                    "Place", "History", "Culture", "Technical", "General"
                });

                cat.SelectedItem = string.IsNullOrEmpty(entry.Category)
                    ? "General"
                    : entry.Category;

                if (cat.SelectedIndex < 0)
                    cat.SelectedIndex = 4;

                var ok = new Button
                {
                    Text = "OK",
                    Left = 120,
                    Top = 140,
                    Width = 90,
                    DialogResult = DialogResult.OK
                };

                var cancel = new Button
                {
                    Text = "Cancel",
                    Left = 250,
                    Top = 140,
                    Width = 90,
                    DialogResult = DialogResult.Cancel
                };

                f.Controls.AddRange(new Control[]
                {
                    new Label { Text = "Word:", Left = 20, Top = 23, Width = 90 },
                    word,

                    new Label { Text = "Hiragana:", Left = 20, Top = 58, Width = 90 },
                    hira,

                    new Label { Text = "Category:", Left = 20, Top = 93, Width = 90 },
                    cat,

                    ok,
                    cancel
                });

                f.AcceptButton = ok;
                f.CancelButton = cancel;

                if (f.ShowDialog() != DialogResult.OK)
                    return false;

                if (string.IsNullOrWhiteSpace(word.Text))
                    return false;

                entry.Word = word.Text.Trim();
                entry.Hiragana = hira.Text.Trim();
                entry.Category = cat.SelectedItem.ToString();

                return true;
            }
        }

        private void Info(string m)
        {
            MessageBox.Show(
                m,
                "Dictionary",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            string p = _state.Dictionary.Backup(_state.Config.OutputFolder);

            lblStatus.Text = p == null
                ? "Nothing to back up. Import or save a fixed list first."
                : "Backup created: " + p;
        }
    }
}