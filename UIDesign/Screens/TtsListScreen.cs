using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.UIDesign.Screens
{
    public partial class TtsListScreen : UserControl, IScreen
    {
        private readonly AppState _state;
        private readonly Action<string> _nav;

        public TtsListScreen() { InitializeComponent(); }

        public TtsListScreen(AppState state, Action<string> nav) : this()
        {
            _state = state; _nav = nav;

            Theme.StyleGrid(dgvFinal);
            dgvFinal.Columns.Add("no", "No");
            dgvFinal.Columns.Add("word", "Word");
            dgvFinal.Columns.Add("hira", "Hiragana");
            dgvFinal.Columns.Add("src", "Source");
            dgvFinal.Columns.Add("use", "Use Type");
            dgvFinal.Columns[0].FillWeight = 20;
        }

        public void OnShown() => Build();

        private void btnGenGeneral_Click(object sender, EventArgs e) => Build();
        private void btnMerge_Click(object sender, EventArgs e) => Build();

        private void Build()
        {
            _state.FinalTtsList.Clear();
            if (_state.Extraction == null) { lblStats.Text = "No extraction yet."; return; }

            var seen = new HashSet<string>(StringComparer.Ordinal);

            foreach (var m in _state.Dictionary.FindMatchesInScript(_state.Script))
                if (seen.Add(m.Word))
                    _state.FinalTtsList.Add(new TtsListRow { Word = m.Word, Hiragana = m.Hiragana, Source = "Fixed" });

            foreach (var t in _state.Extraction.Terms.Where(t => t.DictionaryStatus != "conflict"))
                if (!seen.Contains(t.Word) && !string.IsNullOrWhiteSpace(t.Hiragana) && seen.Add(t.Word))
                {
                    string src = t.DictionaryStatus == "matched" ? "Fixed" : "General";
                    _state.FinalTtsList.Add(new TtsListRow { Word = t.Word, Hiragana = t.Hiragana, Source = src });
                }

            int fixedN = _state.FinalTtsList.Count(r => r.Source == "Fixed");
            int genN = _state.FinalTtsList.Count(r => r.Source == "General");
            int conflicts = _state.Extraction.Terms.Count(t => t.DictionaryStatus == "conflict" &&
                _state.Confirmation.Items.All(i => i.Word != t.Word || i.State != ReviewState.Approved));

            lblStats.Text = $"Fixed List Words: {fixedN}     General Words: {genN}\r\n" +
                            $"Total TTS Terms: {_state.FinalTtsList.Count}     Conflicts: {conflicts}";

            dgvFinal.Rows.Clear();
            int n = 1;
            foreach (var r in _state.FinalTtsList)
            {
                int idx = dgvFinal.Rows.Add(n++, r.Word, r.Hiragana, r.Source, r.UseType);
                dgvFinal.Rows[idx].DefaultCellStyle.BackColor = r.Source == "Fixed" ? Color.Honeydew : Color.White;
            }
        }

        private void btnGenTts_Click(object sender, EventArgs e)
        {
            Build();
            if (lblStats.Text.Contains("Conflicts: 0"))
            { lblStatus.Text = "Final TTS list ready. Opening Azure TTS…"; _nav("Azure TTS"); }
            else
            { lblStatus.Text = "Resolve remaining conflicts in Human Review before generating."; _nav("Human Review"); }
        }
    }
}
