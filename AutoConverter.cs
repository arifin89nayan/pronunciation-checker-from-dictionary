using CsvHelper;
using CsvHelper.Configuration;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Enums;
using WindowsFormsApp1.Global;
using WindowsFormsApp1.Helper;
using WindowsFormsApp1.Helper.Quiz;
using WindowsFormsApp1.Models.AutoConverters;
using WindowsFormsApp1.Models.Widgets;
using WindowsFormsApp1.Services;
using WindowsFormsApp1.Services.Abstraction;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


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

            if (!ValidateConvert())
            {
                txt_userMessage.Text = "Please select required files and folders correctly.";
                return;
            }
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
            
            if (widget.Lines.Any(l => l.WidgetType == WidgetTypeEnum.Animation))
                return new AnimationWidgetProcessor();

            if (widget.Lines.Any(l => l.WidgetType == WidgetTypeEnum.Caption))
                return new CaptionWidgetProcessor(_languageList);

            return null;
        }
        private bool ValidateConvert()
        {

            return Directory.Exists(txt_template.Text)
        && Directory.Exists(txt_audioFile.Text)
        && Directory.Exists(txt_outputLocation.Text)
        && Directory.Exists(txtIBCFile.Text)
        && File.Exists(LanTextBx.Text)
        && File.Exists(txt_FilePath.Text);

        }
        private void SDataButton_Click(object sender, EventArgs e)           
         => BrowseFile(txt_FilePath, "Select Language Excel File",
                "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls|All Files (*.*)|*.*");

        private void btn_template_click(object sender, EventArgs e)
            => BrowseFolder(txt_template, "Select Template Folder");
        
        private void btn_audioFile_Click(object sender, EventArgs e)
            => BrowseFolder(txt_audioFile, "Select Audio Folder");

       
        private void btn_output_click(object sender, EventArgs e)
            => BrowseFolder(txt_outputLocation, "Select Output Folder");



        private void Back_button_Click(object sender, EventArgs e)
        {
            NewStartingForm newStart = new NewStartingForm();            
            newStart.Show();
            this.Hide();
        }

        
        private void LangBtn_Click(object sender, EventArgs e)
            => BrowseFile(LanTextBx, "Select Language Excel File",
                "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls|All Files (*.*)|*.*");
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
            => BrowseFolder(txtIBCFile, "Select IBC Folder");

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
                dlg.ShowNewFolderButton = true;
                string startFolder = GetBestStartFolder(txt_workspace.Text);
                dlg.SelectedPath = startFolder;
                if (!string.IsNullOrWhiteSpace(txt_workspace.Text) && Directory.Exists(txt_workspace.Text))
                {
                    dlg.SelectedPath = txt_workspace.Text;
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txt_workspace.Text = dlg.SelectedPath;
                    LoadPathsFromContentPathInfoOrDefault(dlg.SelectedPath);
                    //AutoFillFromWorkspace(dlg.SelectedPath);
                    //CreateWorkspaceFoldersAndAutoFill(dlg.SelectedPath);
                }
            }
        }
        private void LoadPathsFromContentPathInfoOrDefault(string parentFolder)
        {
            if (string.IsNullOrWhiteSpace(parentFolder))
            {
                txt_userMessage.Text = "Parent folder path is empty.";
                return;
            }

            if (!Directory.Exists(parentFolder))
            {
                Directory.CreateDirectory(parentFolder);
            }

            string infoFile = System.IO.Path.Combine(parentFolder, "ContentPathInfo.txt");

            List<string> pathsFromInfo = new List<string>();

            bool contentInfoExists = File.Exists(infoFile);

            if (contentInfoExists)
            {
                pathsFromInfo = File.ReadAllLines(infoFile)
                    .Select(line => Regex.Replace(line.Trim(), @"^\d+\s*[\.\)]\s*", ""))
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .ToList();
            }

            // If ContentPathInfo.txt exists, read by line index.
            // If not exists, use default child folders.
            string templatePath = GetSafePathByIndex(pathsFromInfo, parentFolder, 0, "Template");
            string audioPath = GetSafePathByIndex(pathsFromInfo, parentFolder, 1, "Audio Files");
            string languagePath = GetSafePathByIndex(pathsFromInfo, parentFolder, 2, "Language");
            string contentPath = GetSafePathByIndex(pathsFromInfo, parentFolder, 3, "Contents");
            string outputPath = GetSafePathByIndex(pathsFromInfo, parentFolder, 4, "Output");
            string ibsPath = GetSafePathByIndex(pathsFromInfo, parentFolder, 5, "IBS Files");

            // Important:
            // Language and Content can be file path:
            // D:\Phd\Doc3\Language\Language5.xlsx
            // D:\Phd\Doc3\Contents\Content_Test.xlsx
            string languageFolder = GetFolderFromFileOrFolderPath(languagePath);
            string contentsFolder = GetFolderFromFileOrFolderPath(contentPath);

            StringBuilder folderLog = new StringBuilder();

            CreateFolderOnlyIfMissing(templatePath, "Template", folderLog);
            CreateFolderOnlyIfMissing(audioPath, "Audio Files", folderLog);
            CreateFolderOnlyIfMissing(languageFolder, "Language", folderLog);
            CreateFolderOnlyIfMissing(contentsFolder, "Contents", folderLog);
            CreateFolderOnlyIfMissing(outputPath, "Output", folderLog);
            CreateFolderOnlyIfMissing(ibsPath, "IBS Files", folderLog);

            // Fill folder textboxes
            txt_template.Text = templatePath;
            txt_audioFile.Text = audioPath;
            txt_outputLocation.Text = outputPath;
            txtIBCFile.Text = ibsPath;

            // Fill language file textbox
            string finalLanguageFile = "";

            if (IsSupportedFilePath(languagePath) && File.Exists(languagePath))
            {
                finalLanguageFile = languagePath;
            }
            else
            {
                finalLanguageFile = FindFirstFile(languageFolder, new[] { "*.xlsx", "*.xls", "*.csv" });
            }

            LanTextBx.Text = !string.IsNullOrWhiteSpace(finalLanguageFile)
                ? finalLanguageFile
                : languageFolder;

            // Fill content file textbox
            string finalContentFile = "";

            if (IsSupportedFilePath(contentPath) && File.Exists(contentPath))
            {
                finalContentFile = contentPath;
            }
            else
            {
                finalContentFile = FindFirstFile(contentsFolder, new[] { "*.csv", "*.xlsx", "*.xls" }, excludeLanguageFiles: true);
            }

            txt_FilePath.Text = !string.IsNullOrWhiteSpace(finalContentFile)
                ? finalContentFile
                : contentsFolder;

            // Save settings
            SaveCurrentPaths();

            // Create or update ContentPathInfo.txt
            SaveContentPathInfoFile(
                infoFile,
                txt_template.Text,
                txt_audioFile.Text,
                LanTextBx.Text,
                txt_FilePath.Text,
                txt_outputLocation.Text,
                txtIBCFile.Text
            );

            // Show message
            txt_userMessage.Clear();

            if (contentInfoExists)
                txt_userMessage.AppendText("ContentPathInfo.txt loaded successfully.\r\n\r\n");
            else
                txt_userMessage.AppendText("ContentPathInfo.txt not found. Default child folder information loaded.\r\n\r\n");

            txt_userMessage.AppendText("Workspace prepared successfully:\r\n");
            txt_userMessage.AppendText($"✔ Parent Folder   : {parentFolder}\r\n");
            txt_userMessage.AppendText($"✔ Template Folder : {txt_template.Text}\r\n");
            txt_userMessage.AppendText($"✔ Audio Folder    : {txt_audioFile.Text}\r\n");
            txt_userMessage.AppendText($"✔ Language Path   : {LanTextBx.Text}\r\n");
            txt_userMessage.AppendText($"✔ Content Path    : {txt_FilePath.Text}\r\n");
            txt_userMessage.AppendText($"✔ Output Folder   : {txt_outputLocation.Text}\r\n");
            txt_userMessage.AppendText($"✔ IBS Folder      : {txtIBCFile.Text}\r\n\r\n");

            txt_userMessage.AppendText(folderLog.ToString());

            if (!File.Exists(LanTextBx.Text))
                txt_userMessage.AppendText("\r\n⚠ Language file not found. Please select language Excel manually.\r\n");

            if (!File.Exists(txt_FilePath.Text))
                txt_userMessage.AppendText("\r\n⚠ Content CSV/Excel file not found. Please select content file manually.\r\n");
        }
        private string GetSafePathByIndex(List<string> paths, string parentFolder, int index, string defaultFolderName)
        {
            string defaultPath = System.IO.Path.Combine(parentFolder, defaultFolderName);

            if (paths == null || paths.Count <= index)
            {
                return defaultPath;
            }

            string pathFromFile = paths[index];

            if (string.IsNullOrWhiteSpace(pathFromFile))
            {
                return defaultPath;
            }

            try
            {
                string fullParent = System.IO.Path.GetFullPath(parentFolder)
                    .TrimEnd(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar);

                string fullPath = System.IO.Path.GetFullPath(pathFromFile);

                // Prevent Doc1 parent + Doc3 child path mixing
                if (!fullPath.StartsWith(fullParent, StringComparison.OrdinalIgnoreCase))
                {
                    return defaultPath;
                }

                return pathFromFile;
            }
            catch
            {
                return defaultPath;
            }
        }

        private string GetFolderFromFileOrFolderPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return path;
            }

            if (IsSupportedFilePath(path))
            {
                return System.IO.Path.GetDirectoryName(path);
            }

            return path;
        }

        private bool IsSupportedFilePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

            string ext = System.IO.Path.GetExtension(path).ToLower();

            return ext == ".xlsx" ||
                   ext == ".xls" ||
                   ext == ".csv";
        }

        private bool CreateFolderOnlyIfMissing(string folderPath, string folderLabel, StringBuilder log)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                log.AppendLine($"⚠ {folderLabel}: path is empty.");
                return false;
            }

            if (Directory.Exists(folderPath))
            {
                log.AppendLine($"✓ {folderLabel}: already exists.");
                return false;
            }

            Directory.CreateDirectory(folderPath);
            log.AppendLine($"＋ {folderLabel}: created.");
            return true;
        }

        private void SaveContentPathInfoFile(
            string infoFile,
            string templatePath,
            string audioPath,
            string languagePath,
            string contentPath,
            string outputPath,
            string ibsPath)
        {
            string[] lines =
            {
        "1." + templatePath,
        "2." + audioPath,
        "3." + languagePath,
        "4." + contentPath,
        "5." + outputPath,
        "6." + ibsPath
    };

            File.WriteAllLines(infoFile, lines, Encoding.UTF8);
        }
        private string GetPathByFolderName(List<string> paths, params string[] folderNames)
        {
            var targetKeys = folderNames
                .Select(NormalizeFolderKey)
                .ToList();

            foreach (string path in paths)
            {
                string cleanPath = path.TrimEnd(
                    System.IO.Path.DirectorySeparatorChar,
                    System.IO.Path.AltDirectorySeparatorChar
                );

                string lastFolderName = System.IO.Path.GetFileName(cleanPath);
                string key = NormalizeFolderKey(lastFolderName);

                if (targetKeys.Contains(key))
                    return cleanPath;
            }

            return null;
        }

        private string NormalizeFolderKey(string value)
        {
            return value
                .Replace(" ", "")
                .Replace("_", "")
                .Replace("-", "")
                .ToLower();
        }
        private void AutoFillFromWorkspace(string root)
        {
            var missing = new System.Text.StringBuilder();
            var filled = new System.Text.StringBuilder();
            // ── Expected sub-paths (edit these to match YOUR folder names) ──
            var folderMap = new (string subPath, System.Windows.Forms.TextBox target, bool isFile)[]
            {
        (@"Template",                txt_template,       isFile: false),
        (@"Audio Files",             txt_audioFile,      isFile: false),
        (@"Output",                  txt_outputLocation, isFile: false),
        (@"IBC Files",               txtIBCFile,         isFile: false),
        
            };

          

            
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
            string contentFolder = System.IO.Path.Combine(root, "Contents");
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

        // This method tries to find the best starting folder for browsing based on the current textbox value and parent folder
        private string GetBestStartFolder(string currentPath)
        {
            // 1) use current textbox path if valid
            if (!string.IsNullOrWhiteSpace(currentPath))
            {
                if (Directory.Exists(currentPath))
                    return currentPath;

                if (File.Exists(currentPath))
                    return System.IO.Path.GetDirectoryName(currentPath);
            }

            // 2) otherwise use parent folder if valid
            if (!string.IsNullOrWhiteSpace(txt_workspace.Text) && Directory.Exists(txt_workspace.Text))
            {
                return txt_workspace.Text;
            }

            // 3) fallback
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }
        private void BrowseFolder(System.Windows.Forms.TextBox targetTextBox, string description)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = description;
                folderDialog.ShowNewFolderButton = true;

                string startFolder = GetBestStartFolder(targetTextBox.Text);
                folderDialog.SelectedPath = startFolder;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    targetTextBox.Text = folderDialog.SelectedPath;
                }
            }
        }
        private void BrowseFile(System.Windows.Forms.TextBox targetTextBox, string title, string filter)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = title;
                openFileDialog.Filter = filter;
                openFileDialog.Multiselect = false;

                string currentPath = targetTextBox.Text;

                if (!string.IsNullOrWhiteSpace(currentPath))
                {
                    if (File.Exists(currentPath))
                    {
                        openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(currentPath);
                        openFileDialog.FileName = System.IO.Path.GetFileName(currentPath);
                    }
                    else if (Directory.Exists(currentPath))
                    {
                        openFileDialog.InitialDirectory = currentPath;
                    }
                }
                else if (!string.IsNullOrWhiteSpace(txt_workspace.Text) && Directory.Exists(txt_workspace.Text))
                {
                    openFileDialog.InitialDirectory = txt_workspace.Text;
                }

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    targetTextBox.Text = openFileDialog.FileName;
                }
            }
        }
        private void CreateWorkspaceFoldersAndAutoFill(string parentFolder)
        {
            if (string.IsNullOrWhiteSpace(parentFolder))
            {
                txt_userMessage.Text = "Parent folder path is empty.";
                return;
            }

            Directory.CreateDirectory(parentFolder);

            string templateFolder = System.IO.Path.Combine(parentFolder, "Template");
            string audioFolder = System.IO.Path.Combine(parentFolder, "Audio Files");
            string languageFolder = System.IO.Path.Combine(parentFolder, "Language");
            string contentsFolder = System.IO.Path.Combine(parentFolder, "Contents");
            string outputFolder = System.IO.Path.Combine(parentFolder, "OutPut");
            string ibsFolder = System.IO.Path.Combine(parentFolder, "IBS Files");

            // Create folders if missing
            Directory.CreateDirectory(templateFolder);
            Directory.CreateDirectory(audioFolder);
            Directory.CreateDirectory(languageFolder);
            Directory.CreateDirectory(contentsFolder);
            Directory.CreateDirectory(outputFolder);
            Directory.CreateDirectory(ibsFolder);

            // Fill folder textboxes
            txt_template.Text = templateFolder;
            txt_audioFile.Text = audioFolder;
            txt_outputLocation.Text = outputFolder;
            txtIBCFile.Text = ibsFolder;

            // Try to find files
            string languageFile = FindFirstFile(languageFolder, new[] { "*.xlsx", "*.xls", "*.csv" });
            string contentFile = FindFirstFile(contentsFolder, new[] { "*.csv", "*.xlsx", "*.xls" }, excludeLanguageFiles: true);

            LanTextBx.Text = languageFile ?? languageFolder;
            txt_FilePath.Text = contentFile ?? contentsFolder;

            SaveCurrentPaths();

            ShowWorkspaceStatus(parentFolder, templateFolder, audioFolder, languageFolder, contentsFolder, outputFolder, ibsFolder, languageFile, contentFile);
        }
        private string FindFirstFile(string folderPath, string[] patterns, bool excludeLanguageFiles = false)
        {
            if (!Directory.Exists(folderPath))
            {
                return null;
            }

            foreach (var pattern in patterns)
            {
                var files = Directory.GetFiles(folderPath, pattern, SearchOption.TopDirectoryOnly);

                foreach (var file in files)
                {
                    string fileName = System.IO.Path.GetFileName(file);

                    if (excludeLanguageFiles &&
                        fileName.IndexOf("language", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        continue;
                    }

                    return file;
                }
            }

            return null;
        }

        private void SaveCurrentPaths()
        {
            Properties.Settings.Default.TemplatePath = txt_template.Text?.Trim() ?? "";
            Properties.Settings.Default.AudioDataPath = txt_audioFile.Text?.Trim() ?? "";
            Properties.Settings.Default.CsvFilePath = txt_FilePath.Text?.Trim() ?? "";
            Properties.Settings.Default.ExcelLanguage = LanTextBx.Text?.Trim() ?? "";
            Properties.Settings.Default.OutPutPath = txt_outputLocation.Text?.Trim() ?? "";
            Properties.Settings.Default.OutPutIbcFile = txtIBCFile.Text?.Trim() ?? "";
            Properties.Settings.Default.Save();
        }

        private void ShowWorkspaceStatus(
            string parentFolder,
            string templateFolder,
            string audioFolder,
            string languageFolder,
            string contentsFolder,
            string outputFolder,
            string ibsFolder,
            string languageFile,
            string contentFile)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Workspace prepared successfully:");
            sb.AppendLine($"✔ Parent Folder   : {parentFolder}");
            sb.AppendLine($"✔ Template Folder : {templateFolder}");
            sb.AppendLine($"✔ Audio Folder    : {audioFolder}");
            sb.AppendLine($"✔ Language Folder : {languageFolder}");
            sb.AppendLine($"✔ Contents Folder : {contentsFolder}");
            sb.AppendLine($"✔ Output Folder   : {outputFolder}");
            sb.AppendLine($"✔ IBS Folder      : {ibsFolder}");
            sb.AppendLine();

            if (!string.IsNullOrWhiteSpace(languageFile))
                sb.AppendLine($"✔ Language File   : {languageFile}");
            else
                sb.AppendLine("⚠ Language folder created, but no language file found. Select manually.");

            if (!string.IsNullOrWhiteSpace(contentFile))
                sb.AppendLine($"✔ Content File    : {contentFile}");
            else
                sb.AppendLine("⚠ Contents folder created, but no CSV/Excel file found. Select manually.");

            txt_userMessage.Text = sb.ToString();
        }
    }
}
