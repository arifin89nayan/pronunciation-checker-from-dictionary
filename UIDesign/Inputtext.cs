using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp1.UIDesign
{
    public partial class Inputtext : Form
    {
        private readonly ScriptProcessingAgent _agent = new ScriptProcessingAgent();

        // Language selector (created in code so the Designer file stays untouched).
        private ComboBox cmbLanguage;
        private Label lblLanguage;

        public Inputtext()
        {
            InitializeComponent();
            BuildLanguageSelector();

            txt_FixedList.Text = Properties.Settings.Default.Fixed_List;
        }

        // ---------------------------------------------------------------
        // Language selection
        // ---------------------------------------------------------------

        private ILanguageProfile CurrentProfile
        {
            get
            {
                return cmbLanguage.SelectedItem as ILanguageProfile
                       ?? LanguageRegistry.Default;
            }
        }

        private void BuildLanguageSelector()
        {
            lblLanguage = new Label();
            lblLanguage.AutoSize = true;
            lblLanguage.Font = new Font("Segoe UI", 11F);
            lblLanguage.ForeColor = Color.FromArgb(170, 176, 188);
            lblLanguage.Text = "Language";
            lblLanguage.Location = new Point(pnlHeader.Width - 480, 18);
            lblLanguage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlHeader.Controls.Add(lblLanguage);

            cmbLanguage = new ComboBox();
            cmbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLanguage.FlatStyle = FlatStyle.Flat;
            cmbLanguage.Font = new Font("Segoe UI", 12F);
            cmbLanguage.Location = new Point(pnlHeader.Width - 480, 50);
            cmbLanguage.Size = new Size(430, 33);
            cmbLanguage.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            foreach (var profile in LanguageRegistry.All)
                cmbLanguage.Items.Add(profile);   // ToString() -> DisplayName

            // Restore last-used language; default = Japanese.
            string savedKey = "";
            try { savedKey = Properties.Settings.Default.Language; } catch { }

            ILanguageProfile restore = LanguageRegistry.GetByKey(savedKey);
            cmbLanguage.SelectedItem = restore;

            if (cmbLanguage.SelectedIndex < 0)
                cmbLanguage.SelectedIndex = 0;

            cmbLanguage.SelectedIndexChanged += cmbLanguage_SelectedIndexChanged;
            pnlHeader.Controls.Add(cmbLanguage);
        }

        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ILanguageProfile p = CurrentProfile;

            // One dictionary file per language. If the current path points at
            // another language's default file, suggest this language's file
            // in the same folder. Never overwrite a custom path silently.
            string current = txt_FixedList.Text.Trim();

            if (!string.IsNullOrWhiteSpace(current))
            {
                string fileName = Path.GetFileName(current);
                bool looksLikeDefaultOfOtherLanguage =
                    LanguageRegistry.All.Any(x =>
                        x.Key != p.Key &&
                        string.Equals(fileName, x.DefaultFixedListFileName,
                                      StringComparison.OrdinalIgnoreCase));

                if (looksLikeDefaultOfOtherLanguage)
                {
                    string dir = Path.GetDirectoryName(current);
                    txt_FixedList.Text = Path.Combine(dir ?? "", p.DefaultFixedListFileName);
                }
            }

            Log("Language switched to " + p.DisplayName +
                ". Fixed list expected: " + p.DefaultFixedListFileName);

            try
            {
                Properties.Settings.Default.Language = p.Key;
                Properties.Settings.Default.Save();
            }
            catch { /* Add a string setting named "Language" in project settings. */ }
        }

        // ---------------------------------------------------------------
        // Data models
        // ---------------------------------------------------------------

        public class FixedWord
        {
            public string Word { get; set; }
            public string Hiragana { get; set; }    // reading, in any language's notation
            public string Difficulty { get; set; }
        }

        public class KanjiItem
        {
            public string Word { get; set; }

            // Final reading used for TTS (hiragana / IPA / pinyin depending on language).
            public string Hiragana { get; set; }

            public string Difficulty { get; set; }
            public string Reason { get; set; }

            // Fixed / ChatGPT
            public string Source { get; set; }

            // matched / new / conflict
            public string DictionaryStatus { get; set; }

            public bool ReviewRequired { get; set; }

            // Only used when the model disagrees with the Fixed List.
            public string ModelHiragana { get; set; }

            public bool SaveToFixedList { get; set; }
        }

        public class ApiKanjiItem
        {
            [JsonProperty("word")]
            public string Word { get; set; }

            [JsonProperty("hiragana")]
            public string Hiragana { get; set; }

            [JsonProperty("difficulty")]
            public string Difficulty { get; set; }

            [JsonProperty("reason")]
            public string Reason { get; set; }
        }

        // ---------------------------------------------------------------
        // Merge: Fixed Dictionary always wins, conflicts are flagged
        // ---------------------------------------------------------------

        private List<KanjiItem> MergeWithConflicts(
            FixedDictionaryService matcher,
            List<FixedWord> fixedMatches,
            List<ApiKanjiItem> apiItems,
            ILanguageProfile profile)
        {
            StringComparer cmp = profile.UsesWordBoundaryMatching
                ? StringComparer.OrdinalIgnoreCase
                : StringComparer.Ordinal;

            var byWord = new Dictionary<string, KanjiItem>(cmp);
            var ordered = new List<KanjiItem>();

            // 1. Fixed words found locally.
            foreach (var fw in fixedMatches)
            {
                if (fw == null || string.IsNullOrWhiteSpace(fw.Word))
                    continue;

                string key = profile.NormalizeText(fw.Word);

                if (byWord.ContainsKey(key))
                    continue;

                var item = new KanjiItem
                {
                    Word = key,
                    Hiragana = profile.NormalizeReading(fw.Hiragana),
                    Difficulty = string.IsNullOrWhiteSpace(fw.Difficulty) ? "fixed" : fw.Difficulty,
                    Source = "Fixed",
                    DictionaryStatus = "matched",
                    ReviewRequired = false,
                    Reason = "Found in Fixed Dictionary. Fixed reading is final.",
                    SaveToFixedList = false,
                    ModelHiragana = ""
                };

                byWord[key] = item;
                ordered.Add(item);
            }

            // 2. Model output.
            foreach (var api in apiItems ?? new List<ApiKanjiItem>())
            {
                if (api == null || string.IsNullOrWhiteSpace(api.Word))
                    continue;

                string key = profile.NormalizeText(api.Word);
                string modelReading = profile.NormalizeReading(api.Hiragana);

                if (string.IsNullOrWhiteSpace(key))
                    continue;

                FixedWord fw;

                // If the model outputs something already in the fixed dictionary,
                // the fixed dictionary reading wins.
                if (matcher.TryGet(key, out fw))
                {
                    string fixedReading = profile.NormalizeReading(fw.Hiragana);

                    KanjiItem existing;

                    if (!byWord.TryGetValue(key, out existing))
                    {
                        existing = new KanjiItem
                        {
                            Word = key,
                            Hiragana = fixedReading,
                            Difficulty = string.IsNullOrWhiteSpace(fw.Difficulty) ? "fixed" : fw.Difficulty,
                            Source = "Fixed",
                            DictionaryStatus = "matched",
                            ReviewRequired = false,
                            Reason = "Found in Fixed Dictionary. Fixed reading is final.",
                            SaveToFixedList = false,
                            ModelHiragana = ""
                        };

                        byWord[key] = existing;
                        ordered.Add(existing);
                    }

                    if (!string.IsNullOrWhiteSpace(modelReading) &&
                        !string.Equals(fixedReading, modelReading, StringComparison.Ordinal))
                    {
                        existing.DictionaryStatus = "conflict";
                        existing.ReviewRequired = true;
                        existing.ModelHiragana = modelReading;
                        existing.Reason =
                            "CONFLICT: model suggested 「" + modelReading +
                            "」 but Fixed Dictionary says 「" + fixedReading +
                            "」. Fixed reading kept. Human review required.";
                    }

                    continue;
                }

                // 3. New unknown word.
                if (byWord.ContainsKey(key))
                    continue;

                var newItem = new KanjiItem
                {
                    Word = key,
                    Hiragana = modelReading,
                    Difficulty = string.IsNullOrWhiteSpace(api.Difficulty) ? "medium" : api.Difficulty,
                    Source = "ChatGPT",
                    DictionaryStatus = "new",
                    ReviewRequired = true,
                    Reason = string.IsNullOrWhiteSpace(api.Reason)
                        ? "New word suggested by the model. Human review required before entering dictionary."
                        : api.Reason,
                    SaveToFixedList = true,
                    ModelHiragana = modelReading
                };

                byWord[key] = newItem;
                ordered.Add(newItem);
            }

            return ordered;
        }

        // ---------------------------------------------------------------
        // Start button
        // ---------------------------------------------------------------

        private async void StartExractBtn_Click(object sender, EventArgs e)
        {
            string FixedListpath = txt_FixedList.Text.Trim() ?? "";
            Properties.Settings.Default.Fixed_List = FixedListpath;
            Properties.Settings.Default.Save();

            if (string.IsNullOrWhiteSpace(Txt_Input.Text))
            {
                MessageBox.Show("Please input text first.");
                return;
            }

            try
            {
                StartExractBtn.Enabled = false;
                Txt_Msg.Clear();

                ILanguageProfile profile = CurrentProfile;

                string fixedListPath = txt_FixedList.Text.Trim();
                string inputText = Txt_Input.Text.Trim();

                Log("Start processing... [" + profile.DisplayName + "]");
                Log("Loading fixed dictionary...");

                FixedDictionaryService matcher = _agent.GetMatcher(fixedListPath, profile);

                Log("Fixed dictionary ready: " + matcher.Count + " words.");

                string maskedText = matcher.MaskFixedWords(
                    inputText,
                    out List<FixedWord> fixedMatches
                );

                Log("Fixed words masked out of script: " + fixedMatches.Count);
                Log("Masked text for the model:");
                Log(maskedText);

                List<ApiKanjiItem> apiItems;

                if (!profile.ContainsUnresolvedTokens(maskedText))
                {
                    apiItems = new List<ApiKanjiItem>();
                    Log("No unresolved tokens remain. Model call skipped.");
                }
                else
                {
                    Log("Sending only unknown/difficult text to the model...");

                    apiItems = await _agent.ExtractKanjiAsync(maskedText, profile);

                    Log("Model returned " + apiItems.Count + " candidate term(s).");
                }

                List<KanjiItem> kanjiList =
                    MergeWithConflicts(matcher, fixedMatches, apiItems, profile);

                int fixedCount = kanjiList.Count(k => k.DictionaryStatus == "matched");
                int newCount = kanjiList.Count(k => k.DictionaryStatus == "new");
                int conflicts = kanjiList.Count(k => k.DictionaryStatus == "conflict");

                Log("Merged. Fixed " + fixedCount +
                    " | New " + newCount +
                    " | Conflicts " + conflicts);

                if (kanjiList.Count == 0)
                {
                    MessageBox.Show("No terms found.");
                    return;
                }

                Log("Opening review window...");

                KanjiReview popup = new KanjiReview(kanjiList, fixedListPath, profile);

                DialogResult reviewResult = popup.ShowDialog();

                if (reviewResult != DialogResult.OK)
                {
                    Log("Review cancelled/back.");
                    return;
                }

                List<KanjiItem> reviewedItems = popup.ReviewedItems;

                Log("Review completed. Saved/updated: " + popup.SavedCount + " word(s).");

                Log("Generating General List, Final TTS List, and Azure SSML...");

                TtsPipelineResult ttsResult =
                    TtsPipelineService.Build(inputText, reviewedItems, profile, profile.DefaultVoice);

                Log("General List: " + ttsResult.GeneralList.Count);
                Log("Final TTS List: " + ttsResult.FinalTtsList.Count);

                TtsResultPreview ttsForm =
                    new TtsResultPreview(inputText, ttsResult, profile);

                ttsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Processing Error");
                Log("ERROR: " + ex.Message);
            }
            finally
            {
                StartExractBtn.Enabled = true;
            }
        }

        private void Log(string message)
        {
            if (Txt_Msg == null)
                return;

            Txt_Msg.AppendText("[" + DateTime.Now.ToString("HH:mm:ss") + "] " +
                               message + Environment.NewLine);
        }

        // ---------------------------------------------------------------
        // Agent
        // ---------------------------------------------------------------

        public class ScriptProcessingAgent
        {
            // Change this model if needed.
            private const string ModelName = "gpt-5.4-mini";
            private const string Endpoint = "https://api.openai.com/v1/responses";

            private static readonly HttpClient Http = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(120)
            };

            // Cache is keyed by (path + language + file timestamp) so switching
            // language always rebuilds the matcher with the right strategy.
            private string _cachedPath;
            private string _cachedLangKey;
            private DateTime _cachedStamp;
            private FixedDictionaryService _cachedMatcher;

            public FixedDictionaryService GetMatcher(string path, ILanguageProfile profile)
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException("Fixed list file not found.", path);

                DateTime stamp = File.GetLastWriteTimeUtc(path);

                if (_cachedMatcher != null &&
                    _cachedPath == path &&
                    _cachedLangKey == profile.Key &&
                    _cachedStamp == stamp)
                {
                    return _cachedMatcher;
                }

                List<FixedWord> words = LoadFixedList(path, profile);

                _cachedMatcher = new FixedDictionaryService(words, profile);
                _cachedPath = path;
                _cachedLangKey = profile.Key;
                _cachedStamp = stamp;

                return _cachedMatcher;
            }

            public List<FixedWord> LoadFixedList(string path, ILanguageProfile profile)
            {
                var list = new List<FixedWord>();

                if (!File.Exists(path))
                    throw new FileNotFoundException("Fixed list file not found.", path);

                using (var workbook = new XLWorkbook(path))
                {
                    var ws = workbook.Worksheet(1);
                    var lastRow = ws.LastRowUsed();

                    if (lastRow == null)
                        return list;

                    for (int row = 2; row <= lastRow.RowNumber(); row++)
                    {
                        string word = ws.Cell(row, 1).GetString().Trim();
                        string reading = ws.Cell(row, 2).GetString().Trim();
                        string difficulty = ws.Cell(row, 3).GetString().Trim();

                        if (string.IsNullOrWhiteSpace(word))
                            continue;

                        list.Add(new FixedWord
                        {
                            Word = profile.NormalizeText(word),
                            Hiragana = profile.NormalizeReading(reading),
                            Difficulty = string.IsNullOrWhiteSpace(difficulty) ? "General" : difficulty
                        });
                    }
                }

                return list;
            }

            public async Task<List<ApiKanjiItem>> ExtractKanjiAsync(
                string maskedText, ILanguageProfile profile)
            {
                string apiKey =
                    Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.User)
                    ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.Machine)
                    ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY");

                if (string.IsNullOrWhiteSpace(apiKey))
                    throw new Exception("OPENAI_API_KEY is missing. Set it in Windows environment variables.");

                // THE ONLY LANGUAGE-SPECIFIC PART OF THE API CALL:
                string prompt = profile.BuildLlmPrompt(maskedText);

                var body = new
                {
                    model = ModelName,
                    input = prompt,
                    max_output_tokens = 2000,
                    temperature = 0
                };

                using (var req = new HttpRequestMessage(HttpMethod.Post, Endpoint))
                {
                    req.Headers.Authorization =
                        new AuthenticationHeaderValue("Bearer", apiKey);

                    req.Content = new StringContent(
                        JsonConvert.SerializeObject(body),
                        Encoding.UTF8,
                        "application/json"
                    );

                    HttpResponseMessage response = await Http.SendAsync(req);
                    string responseText = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(
                            "Model API error (" + (int)response.StatusCode + "): " + responseText
                        );
                    }

                    string rawText = ExtractOutputText(responseText);
                    return ParseKanjiArray(rawText, profile);
                }
            }

            private static string ExtractOutputText(string responseJson)
            {
                try
                {
                    JObject root = JObject.Parse(responseJson);

                    JToken convenience = root["output_text"];

                    if (convenience != null && convenience.Type == JTokenType.String)
                        return convenience.ToString();

                    var sb = new StringBuilder();

                    JArray output = root["output"] as JArray;

                    if (output != null)
                    {
                        foreach (JToken item in output)
                        {
                            JArray content = item["content"] as JArray;

                            if (content == null)
                                continue;

                            foreach (JToken c in content)
                            {
                                JToken t = c["text"];

                                if (t != null)
                                    sb.Append(t.ToString());
                            }
                        }
                    }

                    return sb.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not read model response shape: " + ex.Message);
                }
            }

            private static List<ApiKanjiItem> ParseKanjiArray(
                string text, ILanguageProfile profile)
            {
                if (string.IsNullOrWhiteSpace(text))
                    return new List<ApiKanjiItem>();

                string cleaned = text
                    .Replace("```json", "")
                    .Replace("```", "")
                    .Trim();

                int start = cleaned.IndexOf('[');
                int end = cleaned.LastIndexOf(']');

                if (start >= 0 && end > start)
                    cleaned = cleaned.Substring(start, end - start + 1);

                try
                {
                    List<ApiKanjiItem> items =
                        JsonConvert.DeserializeObject<List<ApiKanjiItem>>(cleaned);

                    if (items == null)
                        return new List<ApiKanjiItem>();

                    foreach (var item in items)
                    {
                        if (item == null)
                            continue;

                        item.Word = profile.NormalizeText(item.Word);
                        item.Hiragana = profile.NormalizeReading(item.Hiragana);
                    }

                    return items
                        .Where(x => x != null && !string.IsNullOrWhiteSpace(x.Word))
                        .ToList();
                }
                catch (JsonException ex)
                {
                    throw new Exception(
                        "The model returned invalid JSON.\n\nRaw model output:\n" +
                        text + "\n\nJSON error: " + ex.Message
                    );
                }
            }
        }

        // ---------------------------------------------------------------
        // Designer event handlers
        // ---------------------------------------------------------------

        private void Back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_FixedList_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select Fixed List Excel File";
                openFileDialog.Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls|All Files (*.*)|*.*";

                string current = txt_FixedList.Text;

                if (!string.IsNullOrWhiteSpace(current))
                {
                    string dir = Path.GetDirectoryName(current);

                    if (!string.IsNullOrWhiteSpace(dir) && Directory.Exists(dir))
                        openFileDialog.InitialDirectory = dir;
                }

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    txt_FixedList.Text = openFileDialog.FileName;
            }
        }
    }
}
