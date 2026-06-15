using System;
using System.Windows.Forms;
using TTSAgent.Core;

namespace TTSAgent.UI.Screens
{
    public class ScreenBase : UserControl
    {
        protected readonly AppState State;
        public event Action<string> NavigateRequested;

        protected ScreenBase(AppState state)
        {
            State = state;
            Dock = DockStyle.Fill;
            BackColor = Ui.Background;
        }

        public virtual void RefreshScreen()
        {
        }

        protected void NavigateTo(string key) => NavigateRequested?.Invoke(key);

        protected void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "TTS Agent", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected void ShowInfo(string message)
        {
            MessageBox.Show(message, "TTS Agent", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
