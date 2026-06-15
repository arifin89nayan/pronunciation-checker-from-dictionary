using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.UIDesign.Screens
{
    public partial class ExtractionResultScreen : UserControl, IScreen
    {
        private readonly AppState _state;
        private readonly Action<string> _nav;

        public ExtractionResultScreen() { InitializeComponent(); }

        public ExtractionResultScreen(AppState state, Action<string> nav) : this()
        {
            _state = state; _nav = nav;

            Theme.StyleGrid(dgvTerms);
            dgvTerms.Columns.Add("no", "No");
            dgvTerms.Columns.Add("word", "Word");
            dgvTerms.Columns.Add("hira", "Hiragana");
            dgvTerms.Columns.Add("status", "Status");
            dgvTerms.Columns.Add("action", "Action");
            dgvTerms.Columns[0].FillWeight = 20;

            cmbFilter.Items.AddRange(new object[] { "All", "Fixed", "Need Review", "General", "Conflict" });
            cmbFilter.SelectedIndex = 0;
            cmbFilter.SelectedIndexChanged += (sender, e) => Bind();
        }

        public void OnShown() => Bind();

        private void Bind()
        {
            dgvTerms.Rows.Clear();
            if (_state.Extraction == null) { lblStatus.Text = "No extraction yet — run Script Input first."; return; }

            string f = cmbFilter.SelectedItem?.ToString() ?? "All";
            int n = 1;
            foreach (var t in _state.Extraction.Terms.Where(t => Match(t, f)))
            {
                string status;
                switch (t.DictionaryStatus)
                {
                    case "matched":
                        status = "Fixed";
                        break;
                    case "conflict":
                        status = "Conflict";
                        break;
                    default:
                        status = t.ReviewRequired ? "Need Review" : "General";
                        break;
                }

                string action;
                switch (status)
                {
                    case "Fixed":
                        action = "View";
                        break;
                    case "Conflict":
                        action = "Fix";
                        break;
                    case "Need Review":
                        action = "Review";
                        break;
                    default:
                        action = "Add";
                        break;
                }

                int idx = dgvTerms.Rows.Add(n++, t.Word, t.Hiragana, status, action);
                Color backColor;
                switch (status)
                {
                    case "Conflict":
                        backColor = Color.MistyRose;
                        break;
                    case "Need Review":
                        backColor = Color.LemonChiffon;
                        break;
                    case "Fixed":
                        backColor = Color.Honeydew;
                        break;
                    default:
                        backColor = Color.White;
                        break;
                }
                dgvTerms.Rows[idx].DefaultCellStyle.BackColor = backColor;
            }
            lblStatus.Text = $"Showing {dgvTerms.Rows.Count} of {_state.Extraction.Terms.Count} terms.";
        }

        private static bool Match(Models.TtsTerm t, string f)
        {
            switch (f)
            {
                case "Fixed":
                    return t.DictionaryStatus == "matched";
                case "Conflict":
                    return t.DictionaryStatus == "conflict";
                case "Need Review":
                    return t.ReviewRequired;
                case "General":
                    return t.DictionaryStatus == "new" && !t.ReviewRequired;
                default:
                    return true;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (_state.Extraction == null) return;
            int added = 0;
            foreach (var t in _state.Extraction.Terms.Where(t => t.ReviewRequired))
            {
                int before = _state.Confirmation.Items.Count;
                _state.Confirmation.AddFromTerm(t);
                if (_state.Confirmation.Items.Count > before) added++;
            }
            lblStatus.Text = added == 0
                ? "Confirmation list is empty — you can proceed to TTS Script."
                : $"Sent {added} word(s) to the Confirmation List. Opening Human Review…";
            _nav(added > 0 ? "Human Review" : "TTS Script");
        }

        private void btnGoReview_Click(object sender, EventArgs e) => _nav("Human Review");
    }
}
