using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TTSAgent.Core;
using TTSAgent.Models;
using TTSAgent.UI.Screens;

namespace TTSAgent.UI
{
    public partial class MainForm : Form
    {
        private readonly AppState _state;
        private readonly Dictionary<string, ScreenBase> _screens = new();
        private readonly Dictionary<string, Button> _navButtons = new();
        private string _currentKey = "dashboard";

        public MainForm(AppState state)
        {
            _state = state;
            InitializeComponent();
            RegisterNavigation();
            CreateScreens();
            _state.StateChanged += UpdateStatus;
            ShowScreen("script");
        }

        private void RegisterNavigation()
        {
            _navButtons["dashboard"] = btnDashboard;
            _navButtons["script"] = btnScriptInput;
            _navButtons["review"] = btnKanjiReview;
            _navButtons["human"] = btnHumanReview;
            _navButtons["dictionary"] = btnDictionary;
            _navButtons["ttslist"] = btnTtsList;
            _navButtons["azure"] = btnAzureTts;
            _navButtons["voice"] = btnVoiceCheck;
            _navButtons["settings"] = btnSettings;

            btnDashboard.Click += (_, _) => ShowScreen("dashboard");
            btnScriptInput.Click += (_, _) => ShowScreen("script");
            btnKanjiReview.Click += (_, _) => ShowScreen("review");
            btnHumanReview.Click += (_, _) => ShowScreen("human");
            btnDictionary.Click += (_, _) => ShowScreen("dictionary");
            btnTtsList.Click += (_, _) => ShowScreen("ttslist");
            btnAzureTts.Click += (_, _) => ShowScreen("azure");
            btnVoiceCheck.Click += (_, _) => ShowScreen("voice");
            btnSettings.Click += (_, _) => ShowScreen("settings");
        }

        private void CreateScreens()
        {
            AddScreen("dashboard", new DashboardScreen(_state), "Dashboard");
            AddScreen("script", new ScriptInputScreen(_state), "Script Input");
            AddScreen("review", new ExtractionResultScreen(_state), "Kanji Review");
            AddScreen("human", new HumanReviewScreen(_state), "Human Review");
            AddScreen("dictionary", new DictionaryManagerScreen(_state), "Fixed Dictionary");
            AddScreen("ttslist", new TtsListScreen(_state), "TTS Script List");
            AddScreen("azure", new AzureTtsScreen(_state), "Azure TTS");
            AddScreen("voice", new VoiceCheckScreen(_state), "Voice Check");
            AddScreen("settings", new SettingsScreen(_state), "Settings");
        }

        private void AddScreen(string key, ScreenBase screen, string title)
        {
            screen.Tag = title;
            screen.NavigateRequested += ShowScreen;
            _screens[key] = screen;
        }

        private void ShowScreen(string key)
        {
            if (!_screens.TryGetValue(key, out var screen)) return;

            _currentKey = key;
            contentPanel.Controls.Clear();
            contentPanel.Controls.Add(screen);
            screen.RefreshScreen();
            screenTitleLabel.Text = Convert.ToString(screen.Tag);
            HighlightNav(key);
            UpdateStatus();
        }

        private void HighlightNav(string key)
        {
            foreach (var kv in _navButtons)
            {
                kv.Value.BackColor = kv.Key == key ? Ui.Accent : Ui.Sidebar;
            }
        }

        private void UpdateStatus()
        {
            int terms = _state.Extraction?.Terms?.Count ?? 0;
            int pending = _state.Confirmation.Items.FindAll(i => i.State == ReviewState.Pending || i.State == ReviewState.Editing).Count;
            statusLabel.Text = $"Project: {_state.ProjectName}   |   Dictionary: {_state.Dictionary.Count}   |   Terms: {terms}   |   Pending Review: {pending}";
        }
    }
}
