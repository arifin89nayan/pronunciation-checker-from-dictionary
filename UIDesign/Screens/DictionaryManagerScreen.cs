using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.UIDesign.Screens
{
    public partial class DictionaryManagerScreen : UserControl, IScreen
    {
        private readonly AppState _state;

        public DictionaryManagerScreen() { InitializeComponent(); }

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

            cmbCategory.Items.AddRange(new object[] { "All", "Place", "History", "Culture", "Technical", "General" });
            cmbCategory.SelectedIndex = 0;
            cmbCategory.SelectedIndexChanged += (sender, e) => Bind();
            txtSearch.TextChanged += (sender, e) => Bind();
        }

        public void OnShown() => Bind();

        private void Bind()
        {
            dgvDict.Rows.Clear();
            string q = txtSearch.Text.Trim();
            string cat = cmbCategory.SelectedItem?.ToString() ?? "All";
            int n = 1;
            foreach (var ent in _state.Dictionary.All
                .Where(en => (cat == "All" || en.Category == cat) &&
                             (q.Length == 0 || en.Word.Contains(q) || (en.Hiragana ?? "").Contains(q))))
            {
                int idx = dgvDict.Rows.Add(n++, ent.Word, ent.Hiragana, ent.Category, ent.Updated.ToString("yyyy-MM-dd"));
                dgvDict.Rows[idx].Tag = ent;
            }
            lblStatus.Text = $"{dgvDict.Rows.Count} term(s) shown · {_state.Dictionary.Count} total" +
                             (_state.Dictionary.BackingCsvPath != null ? $" · file: {Path.GetFileName(_state.Dictionary.BackingCsvPath)}" : "");
        }

        private WindowsFormsApp1.Models.DictionaryEntry Selected() =>
            dgvDict.SelectedRows.Count > 0 && dgvDict.SelectedRows[0].Tag is WindowsFormsApp1.Models.DictionaryEntry entry ? entry : default;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var entry = new WindowsFormsApp1.Models.DictionaryEntry();
            if (EditDialog(entry, "Add New Term")) { _state.Dictionary.AddOrUpdate(entry); Save(); Bind(); }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var sel = Selected(); 
       
            if (EditDialog(sel, "Edit Term")) { _state.Dictionary.AddOrUpdate(sel); Save(); Bind(); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var sel = Selected(); 
        
            if (MessageBox.Show($"Delete '{sel.ToString()}'?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            { _state.Dictionary.Backup(_state.Config.OutputFolder); _state.Dictionary.Remove(sel.ToString()); Save(); Bind(); }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = "CSV files (*.csv)|*.csv" };
            try
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                _state.Dictionary.LoadCsv(dlg.FileName);
                _state.RebuildExtractor();
                Bind();
                lblStatus.Text = $"Imported {_state.Dictionary.Count} terms.";
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
            SaveFileDialog dlg = new SaveFileDialog { Filter = "PLS XML (*.xml)|*.xml", FileName = "lexicon.xml" };
            try
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                _state.Dictionary.ExportPlsXml(dlg.FileName);
                lblStatus.Text = "Exported PLS lexicon: " + Path.GetFileName(dlg.FileName);
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
            if (_state.Dictionary.BackingCsvPath == null)
            {
                SaveFileDialog dlg = new SaveFileDialog { Filter = "CSV files (*.csv)|*.csv", FileName = "fixed_list.csv" };
                try
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                        _state.Dictionary.SaveCsv(dlg.FileName);
                }
                finally
                {
                    dlg.Dispose();
                }
            }
            else
            {
                _state.Dictionary.SaveCsv();
            }
            _state.RebuildExtractor();
        }

        private static bool EditDialog(Models.DictionaryEntry entry, string title)
        {
            using (var f = new Form { Text = title, Width = 360, Height = 250, StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false })
            {
                var word = new TextBox { Left = 120, Top = 20, Width = 200, Text = entry.Word, Font = new Font("Yu Gothic UI", 10f) };
                var hira = new TextBox { Left = 120, Top = 55, Width = 200, Text = entry.Hiragana, Font = new Font("Yu Gothic UI", 10f) };
                var cat = new ComboBox { Left = 120, Top = 90, Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
                cat.Items.AddRange(new object[] { "Place", "History", "Culture", "Technical", "General" });
                cat.SelectedItem = string.IsNullOrEmpty(entry.Category) ? "General" : entry.Category;
                if (cat.SelectedIndex < 0) cat.SelectedIndex = 4;
                var ok = new Button { Text = "OK", Left = 120, Top = 140, Width = 90, DialogResult = DialogResult.OK };
                var cancel = new Button { Text = "Cancel", Left = 230, Top = 140, Width = 90, DialogResult = DialogResult.Cancel };
                f.Controls.AddRange(new Control[]
                {
                    new Label{ Text="Word:", Left=20, Top=23, Width=90 }, word,
                    new Label{ Text="Hiragana:", Left=20, Top=58, Width=90 }, hira,
                    new Label{ Text="Category:", Left=20, Top=93, Width=90 }, cat, ok, cancel
                });
                f.AcceptButton = ok; f.CancelButton = cancel;
                if (f.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(word.Text)) return false;
                entry.Word = word.Text.Trim();
                entry.Hiragana = hira.Text.Trim();
                entry.Category = cat.SelectedItem.ToString();
                return true;
            }
        }

        private void Info(string m) => MessageBox.Show(m, "Dictionary", MessageBoxButtons.OK, MessageBoxIcon.Information);
        private void btnBackup_Click(object sender, EventArgs e)
        {
            string p = _state.Dictionary.Backup(_state.Config.OutputFolder);
            lblStatus.Text = p == null ? "Nothing to back up (import or save a CSV first)." : "Backup created: " + Path.GetFileName(p);
        }
    }
}
