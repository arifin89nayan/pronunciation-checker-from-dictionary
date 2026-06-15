using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.UIDesign.Screens
{
    public partial class HumanReviewScreen : UserControl, IScreen
    {
        private readonly AppState _state;
        private readonly Action<string> _nav;
        private ReviewItem _current;

        public HumanReviewScreen() { InitializeComponent(); }

        public HumanReviewScreen(AppState state, Action<string> nav) : this()
        {
            _state = state; _nav = nav;

            cmbCategory.Items.AddRange(new object[]
            { "place_name", "shrine_name", "museum_name", "cultural_term", "historical_term", "technical_term", "general_word" });
            cmbSaveType.Items.AddRange(new object[] { "Fixed List", "General Only", "Skip" });
            cmbCategory.SelectedIndex = 0; cmbSaveType.SelectedIndex = 0;

            Theme.StyleGrid(dgvQueue);
            dgvQueue.Columns.Add("no", "No");
            dgvQueue.Columns.Add("word", "Word");
            dgvQueue.Columns.Add("api", "API Reading");
            dgvQueue.Columns.Add("correct", "Correct");
            dgvQueue.Columns.Add("state", "Status");
            dgvQueue.Columns[0].FillWeight = 20;
            dgvQueue.SelectionChanged += dgvQueue_SelectionChanged;
        }

        public void OnShown() { BindQueue(); SelectFirstPending(); }

        private void BindQueue()
        {
            dgvQueue.Rows.Clear();
            int n = 1;
            foreach (var i in _state.Confirmation.Items)
            {
                int idx = dgvQueue.Rows.Add(n++, i.Word, i.ApiReading, i.CorrectReading, i.State.ToString());
                dgvQueue.Rows[idx].Tag = i;
                Color backColor;
                if (i.State == ReviewState.Approved)
                    backColor = Color.Honeydew;
                else if (i.State == ReviewState.Rejected)
                    backColor = Color.WhiteSmoke;
                else if (i.State == ReviewState.Editing)
                    backColor = Color.LemonChiffon;
                else
                    backColor = Color.White;
                dgvQueue.Rows[idx].DefaultCellStyle.BackColor = backColor;
            }
            int pending = _state.Confirmation.Items.Count(i => i.State is ReviewState.Pending || i.State is ReviewState.Editing);
            lblStatus.Text = pending == 0
                ? "Confirmation list is empty — proceed to TTS Script."
                : $"{pending} word(s) awaiting review.";
        }

        private void SelectFirstPending()
        {
            foreach (DataGridViewRow row in dgvQueue.Rows)
                if (row.Tag is ReviewItem it && (it.State is ReviewState.Pending || it.State is ReviewState.Editing))
                { row.Selected = true; LoadItem(it); return; }
            _current = null;
        }

        private void dgvQueue_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvQueue.SelectedRows.Count > 0 && dgvQueue.SelectedRows[0].Tag is ReviewItem it) LoadItem(it);
        }

        private void LoadItem(ReviewItem it)
        {
            _current = it;
            txtSentence.Text = it.SourceSentence;
            txtWord.Text = it.Word;
            txtApi.Text = it.ApiReading;
            txtCorrect.Text = it.CorrectReading;
            if (cmbCategory.Items.Contains(it.Category)) cmbCategory.SelectedItem = it.Category;
            cmbSaveType.SelectedItem = it.SaveType;
        }

        private void btnApprove_Click(object sender, EventArgs e) => Approve(false);
        private void btnEditApprove_Click(object sender, EventArgs e) => Approve(true);

        private void Approve(bool useEdited)
        {
            if (_current == null) { Info("Select a word from the queue first."); return; }
            _current.CorrectReading = useEdited ? txtCorrect.Text.Trim() : _current.CorrectReading;
            _current.Category = cmbCategory.SelectedItem?.ToString() ?? "general_word";
            _current.SaveType = cmbSaveType.SelectedItem?.ToString() ?? "Fixed List";
            _current.State = ReviewState.Approved;

            if (_current.SaveType == "Fixed List")
            {
                _state.Dictionary.AddOrUpdate(new DictionaryEntry
                {
                    Word = _current.Word,
                    Hiragana = _current.CorrectReading,
                    Category = MapCategory(_current.Category),
                    Status = "Approved"
                });
                try { _state.Dictionary.SaveCsv(); } catch { /* no backing file yet */ }
            }

            BindQueue();
            if (_state.Confirmation.IsEmpty) { lblStatus.Text = "All reviewed. Opening TTS Script…"; _nav("TTS Script"); }
            else SelectFirstPending();
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (_current == null) return;
            _current.State = ReviewState.Rejected;
            BindQueue();
            if (!_state.Confirmation.IsEmpty) SelectFirstPending();
        }

        private async void btnPreview_Click(object sender, EventArgs e)
        {
            if (_current == null) return;
            try
            {
                string ssml = _state.Tts.BuildSsml(_current.Word,
                    new[] { new TtsListRow { Word = _current.Word, Hiragana = txtCorrect.Text.Trim() } },
                    _state.Voice);
                string path = await _state.Tts.SynthesizeAsync(ssml, "preview.wav");
                using (var player = new SoundPlayer(path))
                {
                    player.Play();
                }
            }
            catch (Exception ex) { Info("Preview unavailable: " + ex.Message); }
        }

        private static string MapCategory(string c)
        {
            if (c == "place_name" || c == "shrine_name" || c == "museum_name")
                return "Place";
            if (c == "historical_term")
                return "History";
            if (c == "cultural_term")
                return "Culture";
            if (c == "technical_term")
                return "Technical";
            return "General";
        }

        private void Info(string m) => MessageBox.Show(m, "Human Review", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
