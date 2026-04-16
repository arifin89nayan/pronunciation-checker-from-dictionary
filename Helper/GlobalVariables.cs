using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Helper
{
    public static class GlobalVariables
    {
        public static string SelectedLanguage { get; set; }/* = "en-US";*/
        public static string Gender { get; set; } /*= "Male";*/
        public static string SelectedVoice { get; set; } /*= "ja-JP-KeitaNeural";*/
        public static Font SelectedFont { get; set; } /*= new Font(FontFamily.GenericMonospace,20);*/
        public static Color FontColor { get; set; } /*= Color.Black;*/
        public static Color BackgroundColor { get; set; } /*= Color.White;*/
    }

}
