using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Enums;
using WindowsFormsApp1.Global;
using WindowsFormsApp1.Helper.Quiz;
using WindowsFormsApp1.Helper;
using WindowsFormsApp1.Models.Widgets;
using WindowsFormsApp1.Services;
using WindowsFormsApp1.Services.Abstraction;
using WindowsFormsApp1.Models.AutoConverters;
using System.Threading.Tasks;
using System.Collections.Generic;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.EMMA;
using System.Text.Json;
using DocumentFormat.OpenXml.Vml;
using System.Text.RegularExpressions;


namespace WindowsFormsApp1
{
    public partial class AutoConverter : Form
    {
        private readonly FileService _fileService;
        private List<LanguageParsedModel> _languageList;
        bool hadErrors = false;
        public AutoConverter()
        {
            InitializeComponent();
            ErrorLogger.ErrorLogged += OnErrorLogged;

            txt_outputLocation.Text = Properties.Settings.Default.OutPutPath;
            txt_audioFile.Text = Properties.Settings.Default.AudioDataPath;
            txt_template.Text = Properties.Settings.Default.TemplatePath;
            txt_FilePath.Text = Properties.Settings.Default.CsvFilePath;
            LanTextBx.Text = Properties.Settings.Default.ExcelLanguage;
            txtIBCFile.Text = Properties.Settings.Default.OutPutIbcFile;
            txt_workspace.Text = Properties.Settings.Default.txt_workspace;
            _fileService = new FileService();
        }
        private void CopyDataIdFoldersToIbcFolder()
        {
            string sourceRoot = Properties.Settings.Default.OutPutPath;
            string targetRoot = Properties.Settings.Default.OutPutIbcFile;
            if (Directory.Exists(targetRoot))
            {
                //foreach (string file in Directory.GetFiles(sourceRoot))
                //    File.Delete(file);

                foreach (string dir in Directory.GetDirectories(targetRoot))
                    Directory.Delete(dir, true);
            }

            // Make sure source and target exist
            if (!Directory.Exists(sourceRoot))
                return;
            Directory.CreateDirectory(targetRoot);
            foreach (string mainFolder in Directory.GetDirectories(sourceRoot))
            {
                // Loop through all ID folders inside each main folder
                foreach (string idFolder in Directory.GetDirectories(mainFolder)) 
                {
                    string idFolderName = System.IO.Path.GetFileName(idFolder);

                    // If folder name is all digits (e.g., 10001)
                    if (!string.IsNullOrWhiteSpace(idFolderName) && idFolderName.All(char.IsLetterOrDigit))
                    {
                        string destDir = System.IO.Path.Combine(targetRoot, idFolderName);
                        CopyDirectory(idFolder, destDir, true);
                    }
                }
            }
        }
        private void CopyDirectory(string sourceDir, string destDir, bool overwrite = true)
        {
            var dir = new DirectoryInfo(sourceDir);
            if (!dir.Exists)
                return;
            Directory.CreateDirectory(destDir);
            foreach (var file in dir.GetFiles())
            {
                string targetFilePath = System.IO.Path.Combine(destDir, file.Name);
                file.CopyTo(targetFilePath, overwrite);
            }
            foreach (var subDir in dir.GetDirectories())
            {
                string newDestDir = System.IO.Path.Combine(destDir, subDir.Name);
                CopyDirectory(subDir.FullName, newDestDir, overwrite);
            }
        }

       
        private async void button2_Click(object sender, EventArgs e)
        {
            txt_userMessage.Clear(); 
            string templatepath = txt_template.Text.Trim() ?? "";
            Properties.Settings.Default.TemplatePath = templatepath;
            Properties.Settings.Default.Save();
            string csvFilePath = txt_FilePath.Text.Trim() ?? "";
            Properties.Settings.Default.CsvFilePath = csvFilePath;
            Properties.Settings.Default.Save();

            string audioFilePath = txt_audioFile.Text?.Trim() ?? "";
            Properties.Settings.Default.AudioDataPath = audioFilePath;
            Properties.Settings.Default.Save();

            Properties.Settings.Default.OutPutPath = txt_outputLocation.Text?.Trim() ?? "";
            Properties.Settings.Default.Save();
            Properties.Settings.Default.ExcelLanguage = LanTextBx.Text?.Trim() ?? "";
            Properties.Settings.Default.Save();
            Properties.Settings.Default.OutPutIbcFile = txtIBCFile.Text?.Trim() ?? ""; 
            Properties.Settings.Default.Save();
            

            LoadLanguageFile(LanTextBx.Text);
           

            if (!ValidateConvert())
            {
                txt_userMessage.Text = "Please select a valid file path and template file.";
                // MessageBox.Show("Please select a valid file path and template file.");
                return;
            }
            button2.Enabled = false;
            
            // ... your settings code ...

            bool hadErrors = false;

            IWidgetFileParserQuiz parser = AutoConfigGeneratorParserFactory.QuizGetParser(csvFilePath);
            var records = parser.ParseFile(csvFilePath).ToList();

            foreach (var item in records)
            {
                if (item is QuizParserModel quizItem)
                {
                    // parse your Generate column (0 = generate, 1 = skip)
                    if (int.TryParse(quizItem.Generate, out var flag) && flag == 1)
                    {
                        txt_userMessage.AppendText(
                            $"Skipped {quizItem.QuizFolderName} Data Generation\r\n");
                        continue;
                    }
                    txt_userMessage.AppendText($"Data generation Start for {quizItem.QuizFolderName}!\r\n");
                    Application.DoEvents();

                    bool success = await HandleQuizRecord(quizItem);

                    if (!success)
                    { 
                        hadErrors = true;
                    }
                    Application.DoEvents();
                }
                else if (item is GuideParsedModel guideItem)
                {
                    // parse your Generate column (0 = generate, 1 = skip)
                    if (int.TryParse(guideItem.Generate, out var flag) && flag == 1)
                    {
                        txt_userMessage.AppendText($"Skipped {guideItem.GuideName} Data Generation\r\n");
                        continue;
                    }
                    txt_userMessage.AppendText($"Data generation Start for {guideItem.GuideName}!\r\n");
                    Application.DoEvents();

                    bool success = await HandleGuideRecord(guideItem);

                    if (!success)
                    {
                        //txt_userMessage.AppendText($"This {guideItem.GuideName} Data generated successfully!\r\n");
                        hadErrors = true;
                    }
                    Application.DoEvents();
                }
            }

            if (!hadErrors)
                txt_userMessage.AppendText("All data generation completed successfully!\r\n");
            else
                txt_userMessage.AppendText("Data generation finished with some errors. Please check excel input data.\r\n");
           
            Application.DoEvents();
            button2.Enabled = true;
            CopyDataIdFoldersToIbcFolder();
        }

        private async Task<bool> HandleQuizRecord(QuizParserModel item)
        {
            string templatePath = System.IO.Path.Combine(Properties.Settings.Default.TemplatePath, item.QuizTemplate, "PLAY_CONFIG.TXT");
            if (!File.Exists(templatePath))
            {
                txt_userMessage.AppendText($"PLAY_CONFIG.TXT not found for quiz: {item.QuizFolderName}\r\n");                
                return false;
            }
            try
            {
                SetGlobalPaths(item.QuizFolderName, item.QuizID, templatePath);
                var container = await new PlayConfigAnalyzerService().ReadAsync(templatePath);
                await ProcessQuizWidgets(container, item);
                txt_userMessage.AppendText($"This {item.QuizFolderName} Data generated successfully!\r\n");
                return true;
            }
            catch(Exception ex)
            {
                txt_userMessage.AppendText($"Error processing {item.QuizFolderName}: {ex.Message}\r\n");
                return false;
            }

        }
        private async Task<bool> HandleGuideRecord(WidgetParsedCommonModel item)

        {
            string templatePath = System.IO.Path.Combine(Properties.Settings.Default.TemplatePath, item.TemplateName, "PLAY_CONFIG.TXT");
            if (!File.Exists(templatePath))
            {
                txt_userMessage.AppendText($"PLAY_CONFIG.TXT not found for quiz: {item.TemplateName}");
                //MessageBox.Show($"PLAY_CONFIG.TXT not found for guide: {item.TemplateName}");
                return false;
            }
            try
            {
                SetGlobalPaths(item.GuideName, item.GuideId, templatePath);
                var container = await new PlayConfigAnalyzerService().ReadAsync(templatePath);
                await ProcessGuideWidgets(container, item);
                txt_userMessage.AppendText($"This {item.GuideName} Data generated successfully!\r\n");
                return true;

            }
            catch (Exception ex)
            {
                txt_userMessage.AppendText($"Error processing {item.GuideName}: {ex.Message}\r\n");
                return false;
            }

            
           
           
           
        }
        private void SetGlobalPaths(string guideName, string guideId, string playConfigPath)
        {
            guideName = RemoveInvalidFileNameChars(guideName);
            guideId = RemoveInvalidFileNameChars(guideId);

            string folderName = guideName + "-" + guideId;

            string masterFolder = System.IO.Path.Combine(Properties.Settings.Default.OutPutPath, folderName);
            Directory.CreateDirectory(masterFolder);
            // string combinedFolderName = $"{guideName}-{guideId}";
            string RemoveInvalidFileNameChars(string input)
            {
                var invalidChars = System.IO.Path.GetInvalidFileNameChars();
                var result = new System.Text.StringBuilder();
                foreach (var c in input)
                {
                    if (!invalidChars.Contains(c))
                        result.Append(c);
                }
                return result.ToString();
            }

            string outputFolder = System.IO.Path.Combine(masterFolder, guideId);
          
            // DELETE old data if folder exists!
            if (Directory.Exists(outputFolder))
            {
                Directory.Delete(outputFolder, true);
            }
            Directory.CreateDirectory(outputFolder);
            GlobalProperties.OutputPath = outputFolder;

            DirectoryHelper.OutputFolderName = outputFolder;
            DirectoryHelper.SelectedPlayconfigFilePath = playConfigPath;
            GlobalProperties.GuideName = guideName;
            GlobalProperties.OutputFolderName = guideId;
            GlobalProperties.PlayConfigPath = playConfigPath;
          
            GlobalProperties.AutoProcessAudioFolderPath = Properties.Settings.Default.AudioDataPath;
        }
        private async Task ProcessQuizWidgets(WidgetContainer container, QuizParserModel model)
        {
            var instance = GlobalConfigPropreties.Instance;
            instance.AddLine(container.Version);
            instance.AddLine(container.Widgets.Count.ToString());

            foreach (var widget in container.Widgets)
            {
                //var processor = new QuizWidgetProcessor();
                //await processor.ProcessWidges(widget, model);
                QuizProperties.Instance.CurrentOffset = 0;
                var processor = new QuizWidgetProcessor(_languageList);
                await processor.ProcessWidges(widget, model);
            }

            instance.SavePlayConfig(System.IO.Path.Combine(GlobalProperties.OutputPath, "PLAY_CONFIG.TXT"));

            var quizService = new QuizService();
            //quizService.BuildTextData(QuizProperties.Instance, container.Widgets.Skip(1).FirstOrDefault());
            
            
            var selectionExists = container.Widgets.Any(widget => widget.IsContainSelection == true);

            var isline = container.Widgets.Any(widget => widget.Lines.Any(line => line.WidgetType == WidgetTypeEnum.Text));


            if (selectionExists && isline)
            {
                quizService.BuildTextData(QuizProperties.Instance, container.Widgets.FirstOrDefault());

            }
            else
            {
                quizService.BuildTextData(QuizProperties.Instance, container.Widgets.Skip(1).FirstOrDefault());
            }
           
            new CsvGenerator().UpdateCSVFile(System.IO.Path.Combine(GlobalProperties.OutputPath, "CONTENTINFO.CSV"), QuizProperties.Instance);
            new ContentNameGeneratorService().GenerateContentNameFile(GlobalProperties.GuideName, System.IO.Path.Combine(GlobalProperties.OutputPath, "CONTENTNAME.TXT"));
          

           
            instance.Clear();
        }
        private async Task ProcessGuideWidgets(WidgetContainer container, WidgetParsedCommonModel model)
        {
            var instance = GlobalConfigPropreties.Instance;
            instance.AddLine(container.Version);
            instance.AddLine(container.Widgets.Count.ToString());

            foreach (var widget in container.Widgets)
            {
                var processor = GetGuideWidgetProcessor(widget);
                if (processor != null)
                    await processor.ProcessWidges(widget, model);
            }

            instance.SavePlayConfig(System.IO.Path.Combine(GlobalProperties.OutputPath, "PLAY_CONFIG.TXT"));
            new ContentNameGeneratorService().GenerateContentNameFile(GlobalProperties.GuideName, System.IO.Path.Combine(GlobalProperties.OutputPath, "CONTENTNAME.TXT"));
            
            instance.Clear();
        }
        private IAutoWidgetProcessor GetGuideWidgetProcessor(Widget widget)
        {
            //if (widget.Lines.Any(l => l.WidgetType == Enums.WidgetTypeEnum.Animation))
            //    return new AnimationWidgetProcessor();
            //if (widget.Lines.Any(l => l.WidgetType == Enums.WidgetTypeEnum.Caption))
            //    return new CaptionWidgetProcessor();

            //return null;
            if (widget.Lines.Any(l => l.WidgetType == WidgetTypeEnum.Animation))
                return new AnimationWidgetProcessor();

            if (widget.Lines.Any(l => l.WidgetType == WidgetTypeEnum.Caption))
                return new CaptionWidgetProcessor(_languageList);

            return null;
        }
        private bool ValidateConvert()
        {
            //return txt_audioFile.Text != string.Empty
            //    && txt_FilePath.Text != string.Empty
            //    && txt_template.Text != string.Empty
            //    && txt_outputLocation.Text != string.Empty;
            return txt_FilePath.Text != string.Empty
              && txt_template.Text != string.Empty;
              
        }

        private void SDataButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                
                openFileDialog.Title = "Select a File";
                openFileDialog.Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls|CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";

                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    txt_FilePath.Text = selectedFilePath;
                }
            }
        }

       

        private void btn_template_click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a Folder";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txt_template.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void btn_audioFile_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a Folder";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txt_audioFile.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void btn_output_click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a Folder";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txt_outputLocation.Text = folderDialog.SelectedPath;
                }
            }
        }

       

        private void Back_button_Click(object sender, EventArgs e)
        {

            //NewStartingForm NewStart = new NewStartingForm();

            //NewStart.Show();
            this.Close();
            


        }

        private void LangBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.Title = "Select a File";
                openFileDialog.Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls|CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";

                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    LanTextBx.Text = selectedFilePath;

                }
            }

        }
        private void LoadLanguageFile(string filePath)
        {
            var parser = new LanguageExcelParser();
            _languageList = parser.ParseFile(filePath).Skip(1).ToList();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnErrorLogged(string newError)
        {
            // This ensures thread safety if error comes from a background thread
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => AppendError(newError)));
            }
            else
            {
                AppendError(newError);
            }
        }

        private void AppendError(string error)
        {
            
            txt_userMessage.Text = ErrorLogger.GetAllErrors();
            txt_userMessage.SelectionStart = txt_userMessage.Text.Length; // Scroll to bottom
            txt_userMessage.ScrollToCaret();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ErrorLogger.ErrorLogged -= OnErrorLogged; // Unsubscribe!
            base.OnFormClosed(e);
        }
        private void btn_IbcFiles_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a Folder";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtIBCFile.Text = folderDialog.SelectedPath;
                }
            }

        }

        private void txt_FilePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ParentFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select your GuideProject workspace (root) folder";
                dlg.ShowNewFolderButton = false;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txt_workspace.Text = dlg.SelectedPath;
                    AutoFillFromWorkspace(dlg.SelectedPath);
                }
            }
        }
        private void AutoFillFromWorkspace(string root)
        {
            // ── Expected sub-paths (edit these to match YOUR folder names) ──
            var folderMap = new (string subPath, System.Windows.Forms.TextBox target, bool isFile)[]
            {
        (@"Template",                txt_template,       isFile: false),
        (@"Audio Files",             txt_audioFile,      isFile: false),
        (@"Output",                  txt_outputLocation, isFile: false),
        (@"IBC Files",               txtIBCFile,         isFile: false),
        
            };

            var missing = new System.Text.StringBuilder();
            var filled = new System.Text.StringBuilder();

            //foreach (var (subPath, textBox, isFile) in map)
            //{
            //    string fullPath = System.IO.Path.Combine(root, subPath);
            //    bool exists = isFile ? File.Exists(fullPath) : Directory.Exists(fullPath);

            //    if (exists)
            //    {
            //        textBox.Text = fullPath;
            //        filled.AppendLine($"  ✓  {subPath}");
            //    }
            //    else
            //    {
            //        missing.AppendLine($"  ✗  {subPath}  ← not found, set manually");
            //    }
            //}
            foreach (var (subPath, textBox,_) in folderMap)
            {
                string fullPath = System.IO.Path.Combine(root, subPath);
                if (Directory.Exists(fullPath))
                {
                    textBox.Text = fullPath;
                    filled.AppendLine($"  ✓  {subPath}");
                }
                else
                {
                    missing.AppendLine($"  ✗  {subPath}  ← not found, set manually");
                }
            }

            // ── Language Excel: search Content\ for first .xlsx/.xls ──
            string languageFolder = System.IO.Path.Combine(root, "Language");

            string langFile = FindFirstFile(languageFolder, "*.xlsx", "*.xls");
            if (langFile != null)
            {
                LanTextBx.Text = langFile;
                filled.AppendLine($"  ✓  Language Excel  →  {System.IO.Path.GetFileName(langFile)}");
            }
            else
            {
                missing.AppendLine($"  ✗  Language Excel (.xlsx/.xls) not found in Content\\");
            }

            // ── CSV/Excel: search Content\ for first .csv, fallback to .xlsx ──
            string contentFolder = System.IO.Path.Combine(root, "Content");
            string contentFile = FindFirstFile(contentFolder, "*.xlsx", "*.xls");
            if (contentFile != null)
            {
                txt_FilePath.Text = contentFile;
                filled.AppendLine($"  ✓  Excel  →  {System.IO.Path.GetFileName(contentFile)}");
            }
            else
            {
                missing.AppendLine($"  ✗  Excel file not found in Content\\");
            }

            // Save workspace root
            Properties.Settings.Default.txt_workspace = root;
            Properties.Settings.Default.Save();

            // Report to the message box
            txt_userMessage.Clear();
            if (filled.Length > 0)
                txt_userMessage.AppendText("Auto-filled from workspace:\r\n" + filled.ToString());
            if (missing.Length > 0)
                txt_userMessage.AppendText("\r\nNot found (set manually):\r\n" + missing.ToString());
        }
        private string FindFirstFile(string folder, params string[] searchPatterns)
        {
            if (!Directory.Exists(folder))
                return null;

            foreach (string pattern in searchPatterns)
            {
                string match = Directory.EnumerateFiles(folder, pattern, SearchOption.TopDirectoryOnly)
                                        .FirstOrDefault();
                if (match != null)
                    return match;
            }

            return null;
        }
    }
}
