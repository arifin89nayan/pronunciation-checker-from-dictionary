using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApp1.Helper;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Models.AutoConverters;
using WindowsFormsApp1.Services;
using WindowsFormsApp1.Services.Abstraction;

namespace WindowsFormsApp1
{
    public partial class AutomatedContent : Form
    {
        private WorkspaceInfo _workspace;
        private bool _workspaceReady = false;

        public AutomatedContent()
        {
            InitializeComponent();

            StartBtn.Enabled = false;

            // Load previous workspace if available
            txt_workspace.Text =
                Properties.Settings.Default.txt_workspace ?? "";

            if (!string.IsNullOrWhiteSpace(txt_workspace.Text) &&
                Directory.Exists(txt_workspace.Text))
            {
                DetectWorkspace();
            }
        }

        // ============================================================
        // WORKSPACE MODEL
        // ============================================================

        private class WorkspaceInfo
        {
            public string RootPath { get; set; }

            public string TemplateFolder { get; set; }
            public string AudioFolder { get; set; }

            public string LanguageFolder { get; set; }
            public string LanguageFile { get; set; }

            public string ContentFolder { get; set; }
            public string ContentFile { get; set; }

            public string OutputFolder { get; set; }
            public string IbcFolder { get; set; }

            public bool TemplateValid { get; set; }
            public bool AudioValid { get; set; }
            public bool LanguageValid { get; set; }
            public bool ContentValid { get; set; }
            public bool OutputValid { get; set; }
            public bool IbcValid { get; set; }

            public bool IsValid =>
                TemplateValid &&
                AudioValid &&
                LanguageValid &&
                ContentValid &&
                OutputValid &&
                IbcValid;
        }

        // ============================================================
        // SELECT WORKSPACE
        // ============================================================

        private void WorkSpaceFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description =
                    "Select Automated Content Workspace";

                dlg.ShowNewFolderButton = true;

                if (!string.IsNullOrWhiteSpace(txt_workspace.Text) &&
                    Directory.Exists(txt_workspace.Text))
                {
                    dlg.SelectedPath = txt_workspace.Text;
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txt_workspace.Text = dlg.SelectedPath;

                    Properties.Settings.Default.txt_workspace =
                        dlg.SelectedPath;

                    Properties.Settings.Default.Save();

                    DetectWorkspace();
                }
            }
        }

        // ============================================================
        // AUTO DETECTION BUTTON
        // ============================================================

        private void AutodetectionBtn_Click(object sender, EventArgs e)
        {
            DetectWorkspace();
        }

        // ============================================================
        // DETECT WORKSPACE
        // ============================================================

        private void DetectWorkspace()
        {
            txt_userMessage.Clear();

            string root = txt_workspace.Text?.Trim();

            if (string.IsNullOrWhiteSpace(root))
            {
                AppendError("Please select a workspace folder.");

                StartBtn.Enabled = false;
                _workspaceReady = false;

                return;
            }

            if (!Directory.Exists(root))
            {
                AppendError("Workspace folder does not exist.");

                StartBtn.Enabled = false;
                _workspaceReady = false;

                return;
            }

            try
            {
                AppendTitle("WORKSPACE AUTO DETECTION");

                WorkspaceInfo info = new WorkspaceInfo
                {
                    RootPath = root,

                    TemplateFolder =
                        Path.Combine(root, "Template"),

                    AudioFolder =
                        Path.Combine(root, "Audio"),

                    LanguageFolder =
                        Path.Combine(root, "Language"),

                    ContentFolder =
                        Path.Combine(root, "Contents"),

                    OutputFolder =
                        Path.Combine(root, "Output"),

                    IbcFolder =
                        Path.Combine(root, "IBC Files")
                };

                // Folders that can safely be created automatically
                EnsureFolder(info.AudioFolder);
                EnsureFolder(info.OutputFolder);
                EnsureFolder(info.IbcFolder);

                // Find Excel / CSV files
                info.LanguageFile =
                    FindLanguageFile(info.LanguageFolder);

                info.ContentFile =
                    FindContentFile(info.ContentFolder);

                // Template must contain at least one PLAY_CONFIG.TXT
                info.TemplateValid =
                    Directory.Exists(info.TemplateFolder) &&
                    Directory
                        .EnumerateFiles(
                            info.TemplateFolder,
                            "PLAY_CONFIG.TXT",
                            SearchOption.AllDirectories)
                        .Any();

                info.AudioValid =
                    Directory.Exists(info.AudioFolder);

                info.LanguageValid =
                    !string.IsNullOrWhiteSpace(info.LanguageFile) &&
                    File.Exists(info.LanguageFile);

                info.ContentValid =
                    !string.IsNullOrWhiteSpace(info.ContentFile) &&
                    File.Exists(info.ContentFile);

                info.OutputValid =
                    Directory.Exists(info.OutputFolder);

                info.IbcValid =
                    Directory.Exists(info.IbcFolder);

                _workspace = info;
                _workspaceReady = info.IsValid;

                ShowWorkspaceStatus(info);

                SaveWorkspaceSettings(info);

                StartBtn.Enabled = _workspaceReady;

                if (_workspaceReady)
                {
                    AppendSuccess("");
                    AppendSuccess("Workspace ready.");
                    AppendSuccess(
                        "Press Start Generation to load Excel content.");
                }
                else
                {
                    AppendError("");
                    AppendError(
                        "Workspace is not ready. Please check missing files.");
                }
            }
            catch (Exception ex)
            {
                _workspaceReady = false;
                StartBtn.Enabled = false;

                AppendError(
                    "Auto detection error: " + ex.Message);
            }
        }

        // ============================================================
        // START GENERATION
        //
        // IMPORTANT:
        // This DOES NOT create media yet.
        //
        // It:
        // 1. Parses language Excel
        // 2. Parses content Excel
        // 3. Creates batch rows
        // 4. Opens BatchContentForm
        // ============================================================

        private void StartBtn_Click(object sender, EventArgs e)
        {
            if (!_workspaceReady || _workspace == null)
            {
                MessageBox.Show(
                    "Please run Auto Detection first.",
                    "Workspace",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                StartBtn.Enabled = false;

                AppendTitle("READING EXCEL DATA");

                // ====================================================
                // LANGUAGE FILE
                // ====================================================

                AppendMessage(
                    "Loading language configuration...");

                LanguageExcelParser languageParser =
                    new LanguageExcelParser();

                List<LanguageParsedModel> languages =
                    languageParser
                        .ParseFile(_workspace.LanguageFile)
                        .Skip(1)
                        .ToList();

                AppendSuccess(
                    $"Language records loaded: {languages.Count}");

                // ====================================================
                // CONTENT FILE
                // ====================================================

                AppendMessage(
                    "Loading content records...");

                IWidgetFileParserQuiz contentParser =
                    AutoConfigGeneratorParserFactory
                        .QuizGetParser(
                            _workspace.ContentFile);

                List<WidgetParsedCommonModel> records =
                    contentParser
                        .ParseFile(_workspace.ContentFile)
                        .ToList();

                AppendSuccess(
                    $"Content records loaded: {records.Count}");

                if (records.Count == 0)
                {
                    MessageBox.Show(
                        "No content records were found.",
                        "Content",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // ====================================================
                // CONVERT PARSED RECORDS INTO UI BATCH ROWS
                // ====================================================

                List<ContentItem> batchItems =
                    BuildBatchItems(
                        records,
                        languages);

                AppendSuccess(
                    $"Batch rows created: {batchItems.Count}");

                AppendMessage("");

                AppendSuccess(
                    "Opening Batch Content Generation screen...");

                // ====================================================
                // OPEN SCREEN 2
                // ====================================================

                using (ContentDataForm form =
                       new ContentDataForm(
                           batchItems,
                           records,
                           languages,
                           _workspace.RootPath,
                           _workspace.TemplateFolder,
                           _workspace.AudioFolder,
                           _workspace.OutputFolder,
                           _workspace.IbcFolder))
                {
                    this.Hide();

                    form.ShowDialog();

                    this.Show();
                }
            }
            catch (Exception ex)
            {
                AppendError(
                    "Unable to parse content: " +
                    ex.Message);

                MessageBox.Show(
                    ex.Message,
                    "Excel Parse Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                StartBtn.Enabled =
                    _workspaceReady;
            }
        }

        // ============================================================
        // BUILD ROWS FOR SCREEN 2
        // ============================================================

        private List<ContentItem> BuildBatchItems(
            List<WidgetParsedCommonModel> records,
            List<LanguageParsedModel> languages)
        {
            List<ContentItem> result =
                new List<ContentItem>();

            int rowNumber = 1;

            foreach (WidgetParsedCommonModel record in records)
            {
                // ====================================================
                // QUIZ
                // ====================================================

                if (record is QuizParserModel quiz)
                {
                    TextValue voiceInfo =
                        TextConverterService
                            .MapQuizModelToTextValue(
                                quiz,
                                languages);

                    result.Add(
                        new ContentItem
                        {
                            RowNumber = rowNumber++,

                            Topic =
                                quiz.QuizFolderName,

                            Language =
                                !string.IsNullOrWhiteSpace(
                                    quiz.Language)
                                ? quiz.Language
                                : quiz.language,

                            Voice =
                                voiceInfo?.voice ?? "",

                            TemplateName =
                                quiz.QuizTemplate,

                            ContentType =
                                "Quiz",

                            SourceText =
                                quiz.QuizQuestion,

                            AiStatus =
                                "Not Generated",

                            ValidationStatus =
                                "Not Checked",

                            ReviewStatus =
                                "Waiting",

                            ProcessStatus =
                                "Ready",

                            OriginalRecord =
                                quiz
                        });
                }

                // ====================================================
                // GUIDE
                // ====================================================

                else if (record is GuideParsedModel guide)
                {
                    TextValue voiceInfo =
                        TextConverterService
                            .MapGuideModelToTextValue(
                                guide,
                                languages);

                    result.Add(
                        new ContentItem
                        {
                            RowNumber = rowNumber++,

                            Topic =
                                guide.GuideName,

                            Language =
                                guide.language,

                            Voice =
                                voiceInfo?.voice ?? "",

                            TemplateName =
                                guide.TemplateName,

                            ContentType =
                                "Guide",

                            SourceText =
                                guide.Guide,

                            AiStatus =
                                "Not Generated",

                            ValidationStatus =
                                "Not Checked",

                            ReviewStatus =
                                "Waiting",

                            ProcessStatus =
                                "Ready",

                            OriginalRecord =
                                guide
                        });
                }
            }

            return result;
        }

        // ============================================================
        // FILE DETECTION
        // ============================================================

        private string FindLanguageFile(string folder)
        {
            if (!Directory.Exists(folder))
                return null;

            string[] extensions =
            {
                "*.xlsx",
                "*.xls",
                "*.csv"
            };

            foreach (string extension in extensions)
            {
                List<string> files =
                    Directory
                        .EnumerateFiles(
                            folder,
                            extension,
                            SearchOption.TopDirectoryOnly)
                        .ToList();

                // Prefer files containing Language in name
                string languageFile =
                    files.FirstOrDefault(
                        x =>
                        Path.GetFileName(x)
                            .IndexOf(
                                "language",
                                StringComparison.OrdinalIgnoreCase)
                        >= 0);

                if (languageFile != null)
                    return languageFile;

                if (files.Count > 0)
                    return files[0];
            }

            return null;
        }

        private string FindContentFile(string folder)
        {
            if (!Directory.Exists(folder))
                return null;

            string[] extensions =
            {
                "*.xlsx",
                "*.xls",
                "*.csv"
            };

            foreach (string extension in extensions)
            {
                string file =
                    Directory
                        .EnumerateFiles(
                            folder,
                            extension,
                            SearchOption.TopDirectoryOnly)
                        .FirstOrDefault(
                            x =>
                            Path.GetFileName(x)
                                .IndexOf(
                                    "language",
                                    StringComparison.OrdinalIgnoreCase)
                            < 0);

                if (file != null)
                    return file;
            }

            return null;
        }

        // ============================================================
        // WORKSPACE STATUS
        // ============================================================

        private void ShowWorkspaceStatus(
            WorkspaceInfo info)
        {
            ShowStatus(
                "Content file",
                info.ContentValid,
                info.ContentFile);

            ShowStatus(
                "Language file",
                info.LanguageValid,
                info.LanguageFile);

            ShowStatus(
                "Template folder",
                info.TemplateValid,
                info.TemplateFolder);

            ShowStatus(
                "Audio folder",
                info.AudioValid,
                info.AudioFolder);

            ShowStatus(
                "Output folder",
                info.OutputValid,
                info.OutputFolder);

            ShowStatus(
                "IBC folder",
                info.IbcValid,
                info.IbcFolder);
        }

        private void ShowStatus(
            string name,
            bool valid,
            string path)
        {
            if (valid)
            {
                AppendSuccess(name);

                if (!string.IsNullOrWhiteSpace(path))
                {
                    AppendMessage(
                        "    " + path);
                }
            }
            else
            {
                AppendError(name + " NOT FOUND");
            }

            AppendMessage("");
        }

        // ============================================================
        // SAVE EXISTING PROJECT SETTINGS
        // ============================================================

        private void SaveWorkspaceSettings(
            WorkspaceInfo info)
        {
            Properties.Settings.Default.txt_workspace =
                info.RootPath ?? "";

            Properties.Settings.Default.TemplatePath =
                info.TemplateFolder ?? "";

            Properties.Settings.Default.AudioDataPath =
                info.AudioFolder ?? "";

            Properties.Settings.Default.ExcelLanguage =
                info.LanguageFile ?? "";

            Properties.Settings.Default.CsvFilePath =
                info.ContentFile ?? "";

            Properties.Settings.Default.OutPutPath =
                info.OutputFolder ?? "";

            Properties.Settings.Default.OutPutIbcFile =
                info.IbcFolder ?? "";

            Properties.Settings.Default.Save();

            SaveContentPathInfo(info);
        }

        private void SaveContentPathInfo(
            WorkspaceInfo info)
        {
            string file =
                Path.Combine(
                    info.RootPath,
                    "ContentPathInfo.txt");

            string[] lines =
            {
                "1." + info.TemplateFolder,
                "2." + info.AudioFolder,
                "3." + info.LanguageFile,
                "4." + info.ContentFile,
                "5." + info.OutputFolder,
                "6." + info.IbcFolder
            };

            File.WriteAllLines(
                file,
                lines,
                Encoding.UTF8);
        }

        private void EnsureFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        // ============================================================
        // MESSAGE HELPERS
        // ============================================================

        private void AppendTitle(string text)
        {
            txt_userMessage.SelectionColor =
                Color.DarkBlue;

            txt_userMessage.SelectionFont =
                new Font(
                    txt_userMessage.Font,
                    FontStyle.Bold);

            txt_userMessage.AppendText(
                Environment.NewLine +
                "===== " +
                text +
                " =====" +
                Environment.NewLine);

            txt_userMessage.SelectionColor =
                Color.Black;

            txt_userMessage.SelectionFont =
                txt_userMessage.Font;
        }

        private void AppendMessage(string text)
        {
            txt_userMessage.SelectionColor =
                Color.Black;

            txt_userMessage.AppendText(
                text +
                Environment.NewLine);

            ScrollMessage();
        }

        private void AppendSuccess(string text)
        {
            txt_userMessage.SelectionColor =
                Color.Green;

            txt_userMessage.AppendText(
                "✓ " +
                text +
                Environment.NewLine);

            txt_userMessage.SelectionColor =
                Color.Black;

            ScrollMessage();
        }

        private void AppendError(string text)
        {
            txt_userMessage.SelectionColor =
                Color.Red;

            txt_userMessage.AppendText(
                "✕ " +
                text +
                Environment.NewLine);

            txt_userMessage.SelectionColor =
                Color.Black;

            ScrollMessage();
        }

        private void ScrollMessage()
        {
            txt_userMessage.SelectionStart =
                txt_userMessage.Text.Length;

            txt_userMessage.ScrollToCaret();
        }

       
    }
}