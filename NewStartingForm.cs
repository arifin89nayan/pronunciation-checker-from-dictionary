using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class NewStartingForm : Form
    {
        public NewStartingForm()
        {
            InitializeComponent();
        }

        private void ManualCon_Click(object sender, EventArgs e)
        {
            StartupForm startupForm = new StartupForm();
            startupForm.Show();
            //this.Close();
        }

        private void AutoCon_Click(object sender, EventArgs e)
        {
            
            AutoConverter GuideAutoConverterForm = new AutoConverter();
            GuideAutoConverterForm.Show();
            //this.Close();
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IBCForm iBCForm = new IBCForm();
            iBCForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DictionaryFiles DictionaryFiles = new DictionaryFiles();
            DictionaryFiles.Show();
        }

        private void AudioQualityChecker_Click(object sender, EventArgs e)
        {
            //AppConfig config = AppConfig.Load();
            //AppState state = new AppState(config);
            //MainForm uiMain = new MainForm();
            //uiMain.Show();
            AppConfig config = AppConfig.Load();
            //AppState state = new AppState(config);

          //  MainForm uiMain = new MainForm(state); // important

            //this.Hide();

            //uiMain.FormClosed += delegate
            //{
            //    this.Close();
            //};

            //uiMain.Show();
        }
    }
}
