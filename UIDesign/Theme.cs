using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public static class Theme
    {
        public static readonly Color Navy = Color.FromArgb(30, 39, 97);
        public static readonly Color Accent = Color.FromArgb(199, 62, 58);
        public static readonly Color Panel = Color.FromArgb(238, 243, 252);
        public static readonly Color Ink = Color.FromArgb(35, 38, 43);

        public static void StyleGrid(DataGridView g)
        {
            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersDefaultCellStyle.BackColor = Navy;
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersHeight = 30;
        }
    }

    public interface IScreen
    {
        void OnShown();
    }
}
