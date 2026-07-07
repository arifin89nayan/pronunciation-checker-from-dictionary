using System;
using System.Collections.Generic;
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
        private static readonly object _logLock = new object();
        private string _logFilePath;

        public Inputtext()
        {
            InitializeComponent();
            txt_FixedList.Text = Properties.Settings.Default.Fixed_List;

            //txt_FixedList.Text = Path.Combine(
            //    AppDomain.CurrentDomain.BaseDirectory,
            //    "Resources",
            //    "fixed_list.xlsx"
            //);
        }

        // ---------------------------------------------------------------
        // Data models
        // ---------------------------------------------------------------

        public class FixedWord
        {
            public string Word { get; set; }
            public string Hiragana { get; set; }
            public string Difficulty { get; set; }
        }

        public class KanjiItem
        {
            public string Word { get; set; }

            // Final reading used for TTS.
            public string Hiragana { get; set; }

            public string Difficulty { get; set; }
            public string Reason { get; set; }

            // Fixed / ChatGPT
            public string Source { get; set; }

            // matched / new / conflict
            public string DictionaryStatus { get; set; }

            public bool ReviewRequired { get; set; }

            // Only used when ChatGPT disagrees with Fixed List.
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
            List<ApiKanjiItem> apiItems)
        {
            var byWord = new Dictionary<string, KanjiItem>(StringComparer.Ordinal);
            var ordered = new List<KanjiItem>();

            // 1. Fixed words found locally.
            foreach (var fw in fixedMatches)
            {
                if (fw == null || string.IsNullOrWhiteSpace(fw.Word))
                    continue;

                string key = JapaneseTextNormalizer.NormalizeText(fw.Word);

                if (byWord.ContainsKey(key))
                    continue;

                var item = new KanjiItem
                {
                    Word = key,
                    Hiragana = JapaneseTextNormalizer.ToHiragana(fw.Hiragana),
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

            // 2. ChatGPT output.
            foreach (var api in apiItems ?? new List<ApiKanjiItem>())
            {
                if (api == null || string.IsNullOrWhiteSpace(api.Word))
                    continue;

                string key = JapaneseTextNormalizer.NormalizeText(api.Word);
                string modelReading = JapaneseTextNormalizer.ToHiragana(api.Hiragana);

                if (string.IsNullOrWhiteSpace(key))
                    continue;

                FixedWord fw;

                // If ChatGPT outputs something already in fixed dictionary,
                // fixed dictionary reading wins.
                if (matcher.TryGet(key, out fw))
                {
                    string fixedReading = JapaneseTextNormalizer.ToHiragana(fw.Hiragana);

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
                        !JapaneseTextNormalizer.ReadingsEqual(fixedReading, modelReading))
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
                        ? "New word suggested by ChatGPT. Human review required before entering dictionary."
                        : api.Reason,
                    SaveToFixedList = true,
                    ModelHiragana = modelReading
                };

                byWord[key] = newItem;
                ordered.Add(newItem);
            }

            return ordered;
        }
        private string GetLogFilePath()
        {
            if (!string.IsNullOrEmpty(_logFilePath))
                return _logFilePath;

            // LogData folder inside the application folder (bin\Debug\LogData while developing).
            string logFolder = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "LogData");

            Directory.CreateDirectory(logFolder);

            // One file per day: ProcessingLog_20260702.txt
            string fileName = "ProcessingLog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

            _logFilePath = Path.Combine(logFolder, fileName);
            return _logFilePath;
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
                MessageBox.Show("Please input Japanese text first.");
                return;
            }

            try
            {
                StartExractBtn.Enabled = false;
                Txt_Msg.Clear();
                Log("==================== NEW EXTRACTION RUN ====================");

                string fixedListPath = txt_FixedList.Text.Trim();
                string inputText = Txt_Input.Text.Trim();

                Log("Start processing...");
                Log("Loading fixed dictionary...");

                FixedDictionaryService matcher = _agent.GetMatcher(fixedListPath);

                Log("Fixed dictionary ready: " + matcher.Count + " words.");

                string maskedText = matcher.MaskFixedWords(
                    inputText,
                    out List<FixedWord> fixedMatches
                );

                Log("Fixed words masked out of script: " + fixedMatches.Count);
                Log("Masked text for ChatGPT:");
                Log(maskedText);

                List<ApiKanjiItem> apiItems;

                if (!JapaneseTextNormalizer.ContainsKanjiExceptFixedMarker(maskedText))
                {
                    apiItems = new List<ApiKanjiItem>();
                    Log("No unknown kanji remains. ChatGPT skipped.");
                }
                else
                {
                    Log("Sending only unknown/difficult kanji text to ChatGPT...");

                    apiItems = await _agent.ExtractKanjiAsync(maskedText);

                    Log("Model returned " + apiItems.Count + " candidate term(s).");
                }

                List<KanjiItem> kanjiList =
                    MergeWithConflicts(matcher, fixedMatches, apiItems);

                int fixedCount = kanjiList.Count(k => k.DictionaryStatus == "matched");
                int newCount = kanjiList.Count(k => k.DictionaryStatus == "new");
                int conflicts = kanjiList.Count(k => k.DictionaryStatus == "conflict");

                Log("Merged. Fixed " + fixedCount +
                    " | New " + newCount +
                    " | Conflicts " + conflicts);

                if (kanjiList.Count == 0)
                {
                    MessageBox.Show("No kanji terms found.");
                    return;
                }

                Log("Opening kanji review window...");

                //KanjiReview popup = new KanjiReview(kanjiList, fixedListPath);
                //popup.ShowDialog();

                //Log("Kanji review completed. Saved/updated: " + popup.SavedCount + " word(s).");
                KanjiReview popup = new KanjiReview(kanjiList, fixedListPath);

                DialogResult reviewResult = popup.ShowDialog();

                if (reviewResult != DialogResult.OK)
                {
                    Log("Kanji review cancelled/back.");
                    return;
                }

                List<KanjiItem> reviewedItems = popup.ReviewedItems;

                Log("Kanji review completed. Saved/updated: " + popup.SavedCount + " word(s).");

                // Step 8-13
                Log("Generating General List, Final TTS List, and Azure SSML...");

                string voiceName = "ja-JP-NanamiNeural";

                TtsPipelineResult ttsResult =
                    TtsPipelineService.Build(inputText, reviewedItems, voiceName);

                Log("General List: " + ttsResult.GeneralList.Count);
                Log("Final TTS List: " + ttsResult.FinalTtsList.Count);

                // Open TTS Result Preview form
                TtsResultPreview ttsForm =
                    new TtsResultPreview(inputText, ttsResult);

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
            string line = "[" + DateTime.Now.ToString("HH:mm:ss") + "] " + message;

            // 1. Show in the Processing Log console (unchanged behavior).
            Txt_Msg.AppendText(line + Environment.NewLine);

            // 2. Append to the log file. Logging must never break the pipeline,
            //    so file errors are swallowed silently.
            try
            {
                lock (_logLock)
                {
                    File.AppendAllText(GetLogFilePath(), line + Environment.NewLine, Encoding.UTF8);
                }
            }
            catch
            {
                // Ignore file logging errors (locked file, permissions, etc.).
            }
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

            private string _cachedPath;
            private DateTime _cachedStamp;
            private FixedDictionaryService _cachedMatcher;

            public FixedDictionaryService GetMatcher(string path)
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException("Fixed list file not found.", path);

                DateTime stamp = File.GetLastWriteTimeUtc(path);

                if (_cachedMatcher != null &&
                    _cachedPath == path &&
                    _cachedStamp == stamp)
                {
                    return _cachedMatcher;
                }

                List<FixedWord> words = LoadFixedList(path);

                _cachedMatcher = new FixedDictionaryService(words);
                _cachedPath = path;
                _cachedStamp = stamp;

                return _cachedMatcher;
            }

            public List<FixedWord> LoadFixedList(string path)
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
                        string hira = ws.Cell(row, 2).GetString().Trim();
                        string difficulty = ws.Cell(row, 3).GetString().Trim();

                        if (string.IsNullOrWhiteSpace(word))
                            continue;

                        list.Add(new FixedWord
                        {
                            Word = JapaneseTextNormalizer.NormalizeText(word),
                            Hiragana = JapaneseTextNormalizer.ToHiragana(hira),
                            Difficulty = string.IsNullOrWhiteSpace(difficulty) ? "General" : difficulty
                        });
                    }
                }

                return list;
            }

            public async Task<List<ApiKanjiItem>> ExtractKanjiAsync(string maskedText)
            {
                string apiKey =
                    Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.User)
                    ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.Machine)
                    ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY");

                if (string.IsNullOrWhiteSpace(apiKey))
                    throw new Exception("OPENAI_API_KEY is missing. Set it in Windows environment variables.");

                string prompt =
                                @"You are a Japanese TTS pronunciation extraction agent.

                                The marker 【FIXED】 means this word is already handled by the fixed dictionary.
                                Ignore every 【FIXED】 marker completely.
                                Never output 【FIXED】.
                                Never guess the hidden fixed words.
                                Extract EVERY kanji word and important term — do not skip any, even if it
                                seems common. It is better to over-extract than to miss one.

                                From the remaining visible text, extract ONLY kanji words and important Japanese terms.

                                Target terms:
                                - place names
                                - shrine names
                                - temple names
                                - museum names
                                - person names
                                - organization names
                                - historical terms
                                - cultural terms
                                - technical terms
                                - rare kanji words
                                - difficult pronunciation words

                                Prefer full words and phrases over single kanji characters.

                                Return ONLY a JSON array.
                                No prose.
                                No markdown fences.

                                Format:
                                [
                                  {
                                    ""word"": ""笄"",
                                    ""hiragana"": ""こうがい"",
                                    ""difficulty"": ""high"",
                                    ""reason"": ""Rare kanji; reading is not predictable.""
                                  }
                                ]

                                difficulty:
                                low = common stable word
                                medium = compound/technical/cultural/historical word
                                high = place/shrine/person name, rare kanji, local term, uncertain reading

                                Input Text:
                                " + maskedText;

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
                    return ParseKanjiArray(rawText);
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

            private static List<ApiKanjiItem> ParseKanjiArray(string text)
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

                        item.Word = JapaneseTextNormalizer.NormalizeText(item.Word);
                        item.Hiragana = JapaneseTextNormalizer.ToHiragana(item.Hiragana);
                    }

                    return items
                        .Where(x => x != null && !string.IsNullOrWhiteSpace(x.Word))
                        .ToList();
                }
                catch (JsonException ex)
                {
                    throw new Exception(
                        "ChatGPT returned invalid JSON.\n\nRaw model output:\n" +
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