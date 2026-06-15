using System.Drawing;
using System.Windows.Forms;

namespace TTSAgent.UI
{
    /// <summary>
    /// Shared visual constants. Actual control layout is in *.Designer.cs files
    /// so Visual Studio can open and edit the screens with the WinForms designer.
    /// </summary>
    internal static class Ui
    {
        public static readonly Color Sidebar = Color.FromArgb(30, 41, 59);
        public static readonly Color SidebarHover = Color.FromArgb(51, 65, 85);
        public static readonly Color Accent = Color.FromArgb(37, 99, 235);
        public static readonly Color Background = Color.FromArgb(248, 250, 252);
        public static readonly Color Card = Color.White;
        public static readonly Color Border = Color.FromArgb(226, 232, 240);
        public static readonly Color Text = Color.FromArgb(15, 23, 42);
        public static readonly Color Muted = Color.FromArgb(100, 116, 139);

        public static Font TitleFont => new("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
        public static Font HeaderFont => new("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
        public static Font BodyFont => new("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);

        public static void StylePrimaryButton(Button button)
        {
            button.BackColor = Accent;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = HeaderFont;
            button.Height = 38;
        }

        public static void StyleSecondaryButton(Button button)
        {
            button.BackColor = Color.White;
            button.ForeColor = Text;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Border;
            button.Font = BodyFont;
            button.Height = 34;
        }

        public static void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Text;
            grid.ColumnHeadersDefaultCellStyle.Font = HeaderFont;
            grid.DefaultCellStyle.Font = BodyFont;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            grid.DefaultCellStyle.SelectionForeColor = Text;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
        }
    }
}
