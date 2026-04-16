using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Helper;

namespace WindowsFormsApp1
{
    public partial class CaptionSetup : Form
    {
        //public static FontDialog _CaptionfontDlg = new FontDialog();
        //public static ColorDialog _CaptionColorDlg = new ColorDialog();
        public CaptionSetup()
        {
            InitializeComponent();
        }
        private void CaptionSetup_Load(object sender, EventArgs e)
        {
            // Load settings and apply to the label
            string fontName = (Properties.Settings.Default.CapFontName).ToString();
            FontStyle fontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), Properties.Settings.Default.CapFontStyle.ToString());
            float fontSize = Properties.Settings.Default.CapFontSize;
            string fontColor = (Properties.Settings.Default.CapFontColor).ToString();
            string CapBgfontColor =(Properties.Settings.Default.CapBgColor).ToString();

            Font savedFont = new Font(fontName, fontSize, fontStyle);
            Color savedColor = ColorTranslator.FromHtml(fontColor);
            Color CapBGColor = ColorTranslator.FromHtml(CapBgfontColor);

            LblSamtext.Font = savedFont;
            LblSamtext.ForeColor = savedColor;
            LblSamtext.BackColor = CapBGColor;
        }

        private void FontStyle_Click(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                fontDialog.ShowColor = true;
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    GlobalVariables.SelectedFont = fontDialog.Font;
                    GlobalVariables.FontColor = fontDialog.Color;
                  Font  _selectedFont = fontDialog.Font;
                  Color  _selectedColor = fontDialog.Color;
                    // Save properties to settings
                    Properties.Settings.Default.CapFontName = _selectedFont.Name;
                    Properties.Settings.Default.CapFontStyle = _selectedFont.Style.ToString();
                    Properties.Settings.Default.CapFontSize = _selectedFont.Size;
                    Properties.Settings.Default.CapFontColor = ColorTranslator.ToHtml(_selectedColor);
                    Properties.Settings.Default.Save();
                    // Apply font settings to a label or any control
                    LblSamtext.Font = _selectedFont;
                    LblSamtext.ForeColor = _selectedColor;
                }
            }
        }

        private void BcgroundColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    GlobalVariables.BackgroundColor = colorDialog.Color;
                    Color  _selectedColor = colorDialog.Color;
                    Properties.Settings.Default.CapBgColor = ColorTranslator.ToHtml(_selectedColor); ;
                    Properties.Settings.Default.Save();
                    // Apply font settings to a label or any control
                  
                    
                    LblSamtext.BackColor = _selectedColor;
                }
            }
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            StartupForm firstForm = new StartupForm();
            this.Hide();
            firstForm.ShowDialog();
            this.Close();
            
        }
    }
}
