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
    public partial class DashboardScreen : UserControl, IScreen
    {
        private readonly AppState _state;
        private readonly Action<string> _nav;

        public DashboardScreen() { InitializeComponent(); }

        public DashboardScreen(AppState state, Action<string> nav) : this()
        {
            _state = state; _nav = nav;

            int y = 120;
            foreach (var (label, target) in new[]
            {
                ("1. Paste script & extract  →  Script Input", "Script Input"),
                ("2. Review extraction results  →  Kanji Review", "Kanji Review"),
                ("3. Human-confirm readings  →  Human Review", "Human Review"),
                ("4. Manage permanent list  →  Dictionary", "Dictionary"),
                ("5. Merge final list  →  TTS Script", "TTS Script"),
                ("6. Generate SSML & audio  →  Azure TTS", "Azure TTS"),
                ("7. Verify the voice  →  Voice Check", "Voice Check"),
            })
            {
                var b = new Button
                {
                    Text = label,
                    Left = 20,
                    Top = y,
                    Width = 360,
                    Height = 34,
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                    BackColor = Theme.Panel,
                    ForeColor = Theme.Navy,
                    Cursor = Cursors.Hand
                };
                b.FlatAppearance.BorderColor = Color.FromArgb(201, 212, 232);
                string t = target;
                b.Click += delegate { _nav(t); };
                Controls.Add(b);
                y += 42;
            }
        }

        public void OnShown()
        {
            int ext = _state?.Extraction?.Terms.Count ?? 0;
            int pending = _state?.Confirmation.Items.Count ?? 0;
            lblStatus.Text =
                $"Project: {_state?.ProjectName}    Dictionary terms: {_state?.Dictionary.Count}\n" +
                $"Extracted: {ext}    In confirmation list: {pending}    Final TTS rows: {_state?.FinalTtsList.Count}";
        }

       
    }
}
