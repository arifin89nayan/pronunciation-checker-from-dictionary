using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.UIDesign.Screens;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private readonly AppState _state;
        private readonly Dictionary<string, UserControl> _screens = new Dictionary<string, UserControl>();
        // Replace target-typed new() with explicit type for C# 7.3 compatibility
        private readonly List<Button> _navButtons = new List<Button>();

        // Parameterless ctor so the Visual Studio designer can open the form.
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(AppState state) : this()
        {
            _state = state;
            BuildScreens();
            BuildNav();
            Show("Script Input");
        }

        private void BuildScreens()
        {
            _screens["Dashboard"] = new DashboardScreen(_state, Show);
            _screens["Script Input"] = new ScriptInputScreen(_state, Show);
            _screens["Kanji Review"] = new ExtractionResultScreen(_state, Show);
            _screens["Human Review"] = new HumanReviewScreen(_state, Show);
            _screens["Dictionary"] = new DictionaryManagerScreen(_state);
            _screens["TTS Script"] = new TtsListScreen(_state, Show);
            _screens["Azure TTS"] = new AzureTtsScreen(_state, Show);
            _screens["Voice Check"] = new VoiceCheckScreen(_state);

            foreach (var sc in _screens.Values)
            {
                sc.Dock = DockStyle.Fill;
                sc.Visible = false;
                panelHost.Controls.Add(sc);
            }
        }

        private void BuildNav()
        {
            string[] order =
            {
                "Dashboard", "Script Input", "Kanji Review", "Human Review",
                "Dictionary", "TTS Script", "Azure TTS", "Voice Check"
            };
            foreach (var name in order)
            {
                var b = new Button
                {
                    Text = "  " + name,
                    Width = 180,
                    Height = 40,
                    Margin = new Padding(5, 2, 5, 2),
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI", 10f),
                    ForeColor = Theme.Ink,
                    BackColor = Theme.Panel,
                    Cursor = Cursors.Hand
                };
                b.FlatAppearance.BorderSize = 0;
                string target = name;
                // Replace this line in BuildNav():
                // b.Click += (_, _) => Show(target);
                // With the following, which is compatible with C# 7.3:
                b.Click += delegate (object sender, EventArgs e) { Show(target); };
                _navButtons.Add(b);
                flowNav.Controls.Add(b);
            }
        }

        private void Show(string name)
        {
            if (!_screens.TryGetValue(name, out var screen)) return;
            foreach (var sc in _screens.Values) sc.Visible = false;
            screen.Visible = true;
            screen.BringToFront();
            (screen as IScreen)?.OnShown();

            foreach (var b in _navButtons)
            {
                bool active = b.Text.Trim() == name;
                b.BackColor = active ? Theme.Navy : Theme.Panel;
                b.ForeColor = active ? Color.White : Theme.Ink;
                b.Font = new Font("Segoe UI", 10f, active ? FontStyle.Bold : FontStyle.Regular);
            }
        }
    }

}
