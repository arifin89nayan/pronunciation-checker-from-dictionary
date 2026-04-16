using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class IBCForm : Form
    {
        public IBCForm()
        {
            InitializeComponent();
            SlFileLocation.Text = Properties.Settings.Default.IbcSelectFile;
            IbcfileLocation.Text = Properties.Settings.Default.IbcOutputFile;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Ibc_txt_userMessage.Clear();

            string parentFolder = SlFileLocation.Text;
            string outputFolder = IbcfileLocation.Text;
            Properties.Settings.Default.IbcSelectFile = parentFolder?.Trim() ?? "";
            Properties.Settings.Default.Save();
            Properties.Settings.Default.IbcOutputFile = outputFolder?.Trim() ?? "";
            Properties.Settings.Default.Save();
            // Delete all previous files and folders in the output folder
            if (Directory.Exists(outputFolder))
            {
                foreach (string file in Directory.GetFiles(outputFolder))
                    File.Delete(file);

                //foreach (string dir in Directory.GetDirectories(outputFolder))
                //    Directory.Delete(dir, true);
            }
           
            foreach (string subfolder in Directory.GetDirectories(parentFolder))
            {
                string subfolderName = new DirectoryInfo(subfolder).Name;
                // string hexString = "&HFF";
                UInt32 value = Convert.ToUInt32(subfolderName, 16);
                //string hexsubfolder = BitConverter.ToString(System.Text.Encoding.UTF8.GetBytes(subfolderName)).Replace("-", "");
               // UInt32 u32ID = UInt32.Parse(subfolderName);
                string title = subfolderName;
                //string hex = BitConverter.ToString(System.Text.Encoding.UTF8.GetBytes(title)).Replace("-", "");

                compress(title, value, subfolder, outputFolder);
                Ibc_txt_userMessage.AppendText($"{DateTime.Now:mm:ss} - {title} IBC generation completed successfully!\r\n");
            }

        }
        public void compress(string szTitle, UInt32 u32ID, string szCompressFileFolder, string saveFolder)
        {

            //get all files information in szCompressFileFolder
            string[] filestomp4 = Directory.GetFiles(szCompressFileFolder);

            string szFileIBC = saveFolder + "\\" + szTitle + ".ibc";
            FileStream fsbin = new FileStream(szFileIBC, FileMode.Create);
            byte[] buf = new byte[1024];

            for (int i = 0; i < filestomp4.Length; i++)
            {
                byte[] tempfiledata = File.ReadAllBytes(filestomp4[i]);
                long time = File.GetCreationTimeUtc(filestomp4[i]).Ticks;
                string shortfilename = filestomp4[i].Substring(filestomp4[i].LastIndexOf('\\') + 1);
                shortfilename = u32ID.ToString("x") + "/" + shortfilename;
                shortfilename = shortfilename.ToUpper();
                buf[0] = (byte)(11 + shortfilename.Length);
                //buf[1] = (byte)(tempfiledata.Length / (256 * 256 * 256));
                //buf[2] = (byte)((tempfiledata.Length % (256 * 256 * 256)) / (256 * 256));
                //buf[3] = (byte)((tempfiledata.Length % (256 * 256)) / (256));
                //buf[4] = (byte)(tempfiledata.Length % 256);
                buf[4] = (byte)(tempfiledata.Length / (256 * 256 * 256));
                buf[3] = (byte)((tempfiledata.Length % (256 * 256 * 256)) / (256 * 256));
                buf[2] = (byte)((tempfiledata.Length % (256 * 256)) / (256));
                buf[1] = (byte)(tempfiledata.Length % 256);
                //time
                buf[5] = (byte)(time / (256 * 256 * 256));
                buf[6] = (byte)((time % (256 * 256 * 256)) / (256 * 256));
                buf[7] = (byte)((time % (256 * 256)) / (256));
                buf[8] = (byte)(time % 256);

                buf[9] = (byte)(shortfilename.Length);
                int j;
                for (j = 0; j < buf[9]; j++)
                {
                    buf[10 + j] = (byte)(shortfilename[j]);
                }
                buf[10 + j] = 0;
                fsbin.Write(buf, 0, 10 + j + 1);
                fsbin.Write(tempfiledata, 0, tempfiledata.Length);
            }

            fsbin.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a Folder";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    SlFileLocation.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a Folder";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    IbcfileLocation.Text = folderDialog.SelectedPath;
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
