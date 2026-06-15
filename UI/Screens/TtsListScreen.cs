using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TTSAgent.Core;
using TTSAgent.Models;

namespace TTSAgent.UI.Screens
{
    public partial class TtsListScreen : ScreenBase
    {
        public TtsListScreen(AppState state) : base(state)
        {
            InitializeComponent();
            Ui.StyleGrid(ttsGrid);
            Ui.StylePrimaryButton(btnGenerate);
            Ui.StyleSecondaryButton(btnExport);
            Ui.StyleSecondaryButton(btnAzure);
            Ui.StyleSecondaryButton(btnHumanReview);

            btnGenerate.Click += (_, _) => GenerateList();
            btnExport.Click += (_, _) => ExportCsv();
            btnAzure.Click += (_, _) => NavigateTo("azure");
            btnHumanReview.Click += (_, _) => NavigateTo("human");
        }

        public override void RefreshScreen()
        {
            ttsGrid.DataSource = State.FinalTtsList.Select(r => new
            {
                r.Word,
                r.Hiragana,
                r.Source,
                r.UseType
            }).ToList();

            int pending = State.Confirmation.Items.Count(i => i.State == ReviewState.Pending || i.State == ReviewState.Editing);
            infoLabel.Text = $"Final TTS rows: {State.FinalTtsList.Count} | Pending review: {pending}";
        }

        private void GenerateList()
        {
            int pending = State.Confirmation.Items.Count(i => i.State == ReviewState.Pending || i.State == ReviewState.Editing);
            if (pending > 0)
            {
                ShowInfo($"There are {pending} pending review item(s). Please approve/reject them before generating the final list.");
                NavigateTo("human");
                return;
            }

            State.FinalTtsList.Clear();
            var seen = new HashSet<string>(StringComparer.Ordinal);

            foreach (var e in State.Dictionary.FindMatchesInScript(State.Script))
                AddRow(e.Word, e.Hiragana, "Fixed Dictionary", seen);

            foreach (var item in State.Confirmation.Items.Where(i => i.State == ReviewState.Approved && i.SaveType != "Skip"))
                AddRow(item.Word, item.CorrectReading, item.SaveType == "Fixed List" ? "Fixed Dictionary" : "Human Reviewed", seen);

            if (State.Extraction?.Terms != null)
            {
                foreach (var t in State.Extraction.Terms.Where(t => !t.ReviewRequired && t.DictionaryStatus != "conflict"))
                    AddRow(t.Word, t.Hiragana, t.DictionaryStatus == "matched" ? "Fixed Dictionary" : "LLM General", seen);
            }

            State.NotifyChanged();
            RefreshScreen();
            ShowInfo($"Generated {State.FinalTtsList.Count} TTS list row(s).");
        }

        private void AddRow(string word, string reading, string source, HashSet<string> seen)
        {
            if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(reading) || seen.Contains(word)) return;
            State.FinalTtsList.Add(new TtsListRow
            {
                Word = word,
                Hiragana = reading,
                Source = source,
                UseType = "SSML"
            });
            seen.Add(word);
        }

        private void ExportCsv()
        {
            try
            {
                if (State.FinalTtsList.Count == 0) GenerateList();
                Directory.CreateDirectory(State.Config.OutputFolder);
                string path = Path.Combine(State.Config.OutputFolder, $"tts_list_{DateTime.Now:yyyyMMdd_HHmmss}.csv");
                var sb = new StringBuilder();
                sb.AppendLine("word,hiragana,source,use_type");
                foreach (var r in State.FinalTtsList)
                    sb.AppendLine($"\"{r.Word.Replace("\"", "\"\"")}\",\"{r.Hiragana.Replace("\"", "\"\"")}\",\"{r.Source}\",\"{r.UseType}\"");
                File.WriteAllText(path, sb.ToString(), new UTF8Encoding(true));
                ShowInfo("Exported: " + path);
            }
            catch (Exception ex) { ShowError(ex); }
        }
    }
}
