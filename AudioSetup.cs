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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class SetupForCaption : Form
    {
        public static FontDialog _CaptionfontDlg = new FontDialog();
        public static ColorDialog _CaptionColorDlg = new ColorDialog();
        private Dictionary<string, Dictionary<string, List<string>>> data;
        public static string selectedLanguage;
        public static string selectedGender;
        public static string selectedVoice;
        public SetupForCaption()
        {
            InitializeComponent();


          
            // Initialize the data
            data = new Dictionary<string, Dictionary<string, List<string>>>()
            {
                { "English", new Dictionary<string, List<string>>()
                    {
                        { "Male", new List<string> { "en-US-BrianNeural", "en-US-AndrewNeural", "en-GB-RyanNeural", "en-GB-OliverNeural" } },
                        { "Female", new List<string> { "en-US-AvaNeural", "en-US-EmmaNeural", "en-GB-SoniaNeural", "en-GB-LibbyNeural" } }
                    }
                },
                { "Japanese", new Dictionary<string, List<string>>()
                    {
                        { "Male", new List<string> { "ja-JP-KeitaNeural", "ja-JP-DaichiNeural", "ja-JP-NaokiNeural", "ja-JP-MasaruMultilingualNeural" } },
                        { "Female", new List<string> { "ja-JP-NanamiNeural", "ja-JP-AoiNeural", "ja-JP-MayuNeural", "ja-JP-ShioriNeural" } }
                    }
                }
            };
            Language.Items.AddRange(new string[] { "English", "Japanese" });
            Language.SelectedIndexChanged += Language_SelectedIndexChanged;
           Genderbox.SelectedIndexChanged += Genderbox_SelectedIndexChanged;

            LoadComboBoxSettings();


        }
        private void LoadComboBoxSettings()
        {
            string selectedValue = Properties.Settings.Default.Language;
            
                Language.SelectedItem = selectedValue;
          
            string selectedValue1 = Properties.Settings.Default.Gender;
           
                Genderbox.SelectedItem = selectedValue1;
            
            string selectedValue2 = Properties.Settings.Default.Voice;
            
                Voice.SelectedItem = selectedValue2;
            

            


        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Get the selected values
           //selectedLanguage = Language.SelectedItem?.ToString();
           //selectedGender = Genderbox.SelectedItem?.ToString();
           //selectedVoice = Voice.SelectedItem?.ToString();
            GlobalVariables.SelectedLanguage = Language.SelectedItem.ToString();
            GlobalVariables.Gender = Genderbox.SelectedItem.ToString();
            GlobalVariables.SelectedVoice = Voice.SelectedItem.ToString();
            if (!string.IsNullOrEmpty(GlobalVariables.SelectedLanguage) && !string.IsNullOrEmpty(GlobalVariables.Gender) && !string.IsNullOrEmpty(GlobalVariables.SelectedVoice))
            {
                StartupForm firstForm = new StartupForm();
                this.Hide();
                firstForm.ShowDialog();
                this.Close();

                
            }
            else
            {
                MessageBox.Show("Please make all selections.");
            }


           
        }

        private void Language_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLanguage = Language.SelectedItem.ToString();
            Genderbox.Items.Clear();
            Genderbox.Text = null;
            Voice.Items.Clear();
            Voice.Text = null;


            //if (Language.SelectedItem != null)
            //{
            //    Properties.Settings.Default.Language = Language.SelectedItem.ToString();
            //    Properties.Settings.Default.Save();
            //}

            if (data.ContainsKey(selectedLanguage))
            {
                Genderbox.Items.AddRange(new string[] { "Male", "Female" });
                 
            }
        }

        private void Genderbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Gender = Genderbox.SelectedItem.ToString();
            Properties.Settings.Default.Save();
            PopulateVoice();
            
                
            
        }

        private void Voice_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                Properties.Settings.Default.Voice = Voice.SelectedItem.ToString();
                Properties.Settings.Default.Save();
          
        }


        private void PopulateVoice()
        {
            string selectedLanguage = Language.SelectedItem.ToString();
            string selectedGender = Genderbox.SelectedItem.ToString();
            Voice.Items.Clear();
            Voice.Text = null;

            if (data.ContainsKey(selectedLanguage) && data[selectedLanguage].ContainsKey(selectedGender))
            {
                Voice.Items.AddRange(data[selectedLanguage][selectedGender].ToArray());
            }
        }

        private void Language_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = Language.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }

        //private void FontStyle_Click(object sender, EventArgs e)
        //{

        //    using (FontDialog fontDialog = new FontDialog())
        //    {
        //        fontDialog.ShowColor = true;
        //        if (fontDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            GlobalVariables.SelectedFont = fontDialog.Font;
        //            GlobalVariables.FontColor = fontDialog.Color;
        //        }
        //    }
        //}

        //private void BcgroundColor_Click(object sender, EventArgs e)
        //{

        //    using (ColorDialog colorDialog = new ColorDialog())
        //    {
        //        if (colorDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            GlobalVariables.BackgroundColor = colorDialog.Color;
        //        }
        //    }
        //}
    }
}
