using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    public partial class DictionaryFiles : Form
    {
        public DictionaryFiles()
        {
            InitializeComponent();
            txt_dicfiles.Text = Properties.Settings.Default.txt_dicfiles;
            txt_OutFilePath.Text = Properties.Settings.Default.txt_OutFilePath;
        }

        private void btn_DicFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.Title = "Select a File";
                openFileDialog.Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls|CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";

                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    txt_dicfiles.Text = selectedFilePath;
                    
                    // Auto set output folder
                    string folderPath = Path.GetDirectoryName(selectedFilePath);

                    if (!string.IsNullOrWhiteSpace(folderPath))
                    {
                        txt_OutFilePath.Text = folderPath;
                        
                    }
                    // Create original backup
                    //CreateOriginalCopy(selectedFilePath, folderPath);
                }
            }
        }
        private void CreateOriginalCopy(string sourceFilePath, string folderPath)
        {
            try
            {
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(sourceFilePath);
                string extension = Path.GetExtension(sourceFilePath);

                // 👉 Add "_original"
                string backupFileName = fileNameWithoutExt + "_original" + extension;

                string backupFilePath = Path.Combine(folderPath, backupFileName);

                File.Copy(sourceFilePath, backupFilePath, true);



            }
            catch (Exception ex)
            {
                AppendLog("Excel Original creation failed: " + ex.Message);
            }
        }

        private void SDataOutPut_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a Folder";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txt_OutFilePath.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void Back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void Btn_Convert_ClickAsync(object sender, EventArgs e)
        {
            Btn_Convert.Enabled = false;
            txt_DicuserMessage.Clear();
            string DicFillepath = txt_dicfiles.Text.Trim() ?? "";
            Properties.Settings.Default.txt_dicfiles = DicFillepath;
            Properties.Settings.Default.Save();
            string OutPutFillepath = txt_OutFilePath.Text.Trim() ?? "";
            Properties.Settings.Default.txt_OutFilePath = OutPutFillepath;
            Properties.Settings.Default.Save();

            try
            {
                string inputFilePath = txt_dicfiles.Text?.Trim();
                string outputFolderPath = txt_OutFilePath.Text?.Trim();

                AppendLog("===== DICTIONARY PROCESS START =====");

                // Validation
                if (string.IsNullOrWhiteSpace(inputFilePath) || !File.Exists(inputFilePath))
                {
                    AppendLog("❌ Error: Invalid dictionary file path.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(outputFolderPath))
                {
                    AppendLog("❌ Error: Output folder not selected.");
                    return;
                }

                if (!Directory.Exists(outputFolderPath))
                {
                    Directory.CreateDirectory(outputFolderPath);
                    AppendLog("📁 Output folder created.");
                }

                AppendLog($"📄 Input File  : {inputFilePath}");
                AppendLog($"📂 Output Path : {outputFolderPath}");
                AppendLog("");

                var service = new DictionaryProcessServiceUpdated(AppendLog);

                AppendLog("🔄 Processing dictionary file...");

                DictionaryProcessResult result = await Task.Run(() =>
                    service.ProcessDictionaryFile(inputFilePath, outputFolderPath));

                AppendLog("");
                AppendLog("===== PROCESS SUMMARY =====");
                AppendLog($"Total Rows            : {result.TotalRows}");
                AppendLog($"Valid Rows            : {result.ValidRows}");
                AppendLog($"Invalid Rows          : {result.InvalidRows}");
                AppendLog($"Removed Duplicates    : {result.RemovedDuplicateRows}");
                AppendLog($"Removed Containment   : {result.RemovedContainmentRows}");
                AppendLog($"Final Clean Rows      : {result.FinalRows}");
                AppendLog($"Log File              : {result.LogFilePath}");
                AppendLog($"Clean Excel File      : {result.CleanedExcelPath}");
                AppendLog($"XML File              : {result.XmlFilePath}");
                AppendLog("Process completed successfully.");
            }
            catch (Exception ex)
            {
                AppendLog("❌ SYSTEM ERROR: " + ex.Message);
            }
            finally
            {
                Btn_Convert.Enabled = true;
            }
            

        }

        private void AppendLog(string message)
        {
            if (txt_DicuserMessage.InvokeRequired)
            {
                txt_DicuserMessage.Invoke(new Action(() => AppendLog(message)));
                return;
            }

            txt_DicuserMessage.AppendText(message + Environment.NewLine);
            txt_DicuserMessage.SelectionStart = txt_DicuserMessage.Text.Length;
            txt_DicuserMessage.ScrollToCaret();
            Application.DoEvents();
        }

    }
}
