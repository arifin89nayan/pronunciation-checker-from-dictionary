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
    public partial class ScriptInputScreen : UserControl, IScreen
    {
        private readonly AppState _state;
        private readonly Action<string> _nav;

        public ScriptInputScreen() { InitializeComponent(); }

        public ScriptInputScreen(AppState state, Action<string> nav) : this()
        {
            _state = state; _nav = nav;

            cmbVoice.Items.AddRange(new object[]
            { "ja-JP-NanamiNeural", "ja-JP-AoiNeural", "ja-JP-MayuNeural", "ja-JP-ShioriNeural",
              "ja-JP-KeitaNeural", "ja-JP-DaichiNeural", "ja-JP-NaokiNeural" });
            cmbSpeed.Items.AddRange(new object[] { "Slow", "Normal", "Fast" });
            cmbVoice.SelectedIndex = 0; cmbSpeed.SelectedIndex = 1;
        }

        public void OnShown()
        {
            txtProject.Text = _state.ProjectName;
            txtScript.Text = _state.Script;
            if (_state.Extraction != null) ShowSummary();
        }

        private async void btnExtract_Click(object sender, EventArgs e)
        {
            //SaveSettings();
            //if (string.IsNullOrWhiteSpace(txtScript.Text)) { Info("Please paste a script first."); return; }
            //try
            //{
            //    btnExtract.Enabled = btnCheck.Enabled = false;
            //    Cursor = Cursors.WaitCursor;
            //    lblStatus.Text = "Extracting… calling the LLM.";
            //    _state.Extraction = await _state.Extractor.ExtractAsync(txtScript.Text.Trim());
            //    ShowSummary();
            //    lblStatus.Text = "Extraction complete. Opening Kanji Review…";
            //    _nav("Kanji Review");
            //}
            //catch (Exception ex) { Info("Extraction failed: " + ex.Message); lblStatus.Text = "Extraction failed."; }
            //finally { btnExtract.Enabled = btnCheck.Enabled = true; Cursor = Cursors.Default; }
            SaveSettings();

            if (string.IsNullOrWhiteSpace(txtScript.Text))
            {
                MessageBox.Show("Please input Japanese script first.",
                    "Script Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                MessageBox.Show("Processing started. Kanji extraction will begin now.",
                    "Start Processing",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                btnExtract.Enabled = false;
                btnCheck.Enabled = false;
                Cursor = Cursors.WaitCursor;
                lblStatus.Text = "Processing... extracting kanji from ChatGPT.";

                _state.Extraction = await _state.Extractor.ExtractAsync(txtScript.Text.Trim());

                ShowSummary();

                if (_state.Extraction == null || _state.Extraction.Terms.Count == 0)
                {
                    MessageBox.Show("No kanji terms were extracted.",
                        "Extraction Result",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    lblStatus.Text = "No kanji found.";
                    return;
                }

                MessageBox.Show(
                    $"Kanji extraction completed.\n\nFound {_state.Extraction.Terms.Count} term(s).\nOpening Kanji Review list.",
                    "Extraction Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                _nav("Kanji Review");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Extraction failed:\n\n" + ex.Message,
                    "Processing Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                lblStatus.Text = "Extraction failed.";
            }
            finally
            {
                btnExtract.Enabled = true;
                btnCheck.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            SaveSettings();
            if (_state.Dictionary.Count == 0) { Info("Import a dictionary first (Dictionary screen)."); return; }
            var matches = _state.Dictionary.FindMatchesInScript(txtScript.Text);
            lblSummary.Text = $"Offline check — dictionary words found in script: {matches.Count}";
            lblStatus.Text = matches.Count == 0 ? "No fixed terms found." :
                "Found: " + string.Join("、", matches.Take(8).Select(m => m.Word)) + (matches.Count > 8 ? "…" : "");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtScript.Clear(); lblSummary.Text = ""; lblStatus.Text = "Cleared.";
        }

        private void ShowSummary()
        {
            var s = _state.Extraction.Summary;
            lblSummary.Text = $"Kanji Words: {s.TotalTerms}  |  Fixed Matched: {s.FixedDictionaryMatches}  |  " +
                              $"Need Review: {s.ReviewRequiredCount}  |  Conflicts: {s.ConflictCount}";
        }

        private void SaveSettings()
        {
            _state.ProjectName = string.IsNullOrWhiteSpace(txtProject.Text) ? "Project" : txtProject.Text.Trim();
            _state.Voice = cmbVoice.SelectedItem?.ToString() ?? "ja-JP-NanamiNeural";
            _state.Speed = cmbSpeed.SelectedItem?.ToString() ?? "Normal";
            _state.Script = txtScript.Text;
        }

        private void Info(string m) => MessageBox.Show(m, "Script Input", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
