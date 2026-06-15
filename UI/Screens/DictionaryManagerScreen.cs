using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TTSAgent.Core;
using TTSAgent.Models;
using TTSAgent.Services;

namespace TTSAgent.UI.Screens
{
    public partial class DictionaryManagerScreen : ScreenBase
    {
        public DictionaryManagerScreen(AppState state) : base(state)
        {
            InitializeComponent();
            Ui.StyleGrid(dictionaryGrid);
            Ui.StyleSecondaryButton(btnImport);
            Ui.StyleSecondaryButton(btnSaveAs);
            Ui.StyleSecondaryButton(btnBackup);
            Ui.StyleSecondaryButton(btnExportPls);
            Ui.StylePrimaryButton(btnAddUpdate);
            Ui.StyleSecondaryButton(btnRemove);

            btnImport.Click += (_, _) => ImportCsv();
            btnSaveAs.Click += (_, _) => SaveCsvAs();
            btnBackup.Click += (_, _) => BackupCsv();
            btnExportPls.Click += (_, _) => ExportPls();
            btnAddUpdate.Click += (_, _) => AddOrUpdateEntry();
            btnRemove.Click += (_, _) => RemoveEntry();
            dictionaryGrid.SelectionChanged += (_, _) => LoadSelectedEntry();
        }

        public override void RefreshScreen()
        {
            dictionaryGrid.DataSource = State.Dictionary.All.Select(e => new
            {
                e.Word,
                e.Hiragana,
                e.Category,
                e.Status,
                Updated = e.Updated.ToString("yyyy-MM-dd")
            }).ToList();
            pathLabel.Text = string.IsNullOrWhiteSpace(State.Dictionary.BackingCsvPath)
                ? "No CSV loaded. Use Import CSV or Save CSV As."
                : State.Dictionary.BackingCsvPath;
        }

        private void ImportCsv()
        {
            using var dlg = new OpenFileDialog { Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*" };
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try
            {
                State.Dictionary.LoadCsv(dlg.FileName);
                State.RebuildExtractor();
                State.NotifyChanged();
                RefreshScreen();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void SaveCsvAs()
        {
            using var dlg = new SaveFileDialog { Filter = "CSV files (*.csv)|*.csv", FileName = "fixed_list.csv" };
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try
            {
                State.Dictionary.SaveCsv(dlg.FileName);
                State.NotifyChanged();
                RefreshScreen();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void BackupCsv()
        {
            try
            {
                string path = State.Dictionary.Backup(State.Config.OutputFolder);
                ShowInfo(path == null ? "No loaded dictionary CSV to backup." : "Backup created: " + path);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void ExportPls()
        {
            using var dlg = new SaveFileDialog { Filter = "PLS XML (*.xml)|*.xml", FileName = "tts_lexicon.pls.xml" };
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try
            {
                State.Dictionary.ExportPlsXml(dlg.FileName);
                ShowInfo("Exported: " + dlg.FileName);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void AddOrUpdateEntry()
        {
            string word = wordTextBox.Text.Trim();
            string reading = FixedDictionaryService.NormalizeToHiragana(readingTextBox.Text);
            if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(reading))
            {
                ShowInfo("Word and reading are required.");
                return;
            }

            State.Dictionary.AddOrUpdate(new DictionaryEntry
            {
                Word = word,
                Hiragana = reading,
                Category = string.IsNullOrWhiteSpace(categoryTextBox.Text) ? "General" : categoryTextBox.Text.Trim(),
                Status = "Approved",
                Updated = DateTime.Today
            });
            if (!string.IsNullOrWhiteSpace(State.Dictionary.BackingCsvPath))
                State.Dictionary.SaveCsv();
            State.RebuildExtractor();
            State.NotifyChanged();
            RefreshScreen();
        }

        private void RemoveEntry()
        {
            string word = wordTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(word)) return;
            State.Dictionary.Remove(word);
            if (!string.IsNullOrWhiteSpace(State.Dictionary.BackingCsvPath))
                State.Dictionary.SaveCsv();
            State.RebuildExtractor();
            State.NotifyChanged();
            RefreshScreen();
        }

        private void LoadSelectedEntry()
        {
            if (dictionaryGrid.SelectedRows.Count == 0) return;
            wordTextBox.Text = Convert.ToString(dictionaryGrid.SelectedRows[0].Cells["Word"].Value);
            readingTextBox.Text = Convert.ToString(dictionaryGrid.SelectedRows[0].Cells["Hiragana"].Value);
            categoryTextBox.Text = Convert.ToString(dictionaryGrid.SelectedRows[0].Cells["Category"].Value);
        }
    }
}
