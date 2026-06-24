using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp1.UIDesign.Inputtext;
using Newtonsoft.Json;

namespace WindowsFormsApp1.UIDesign
{
    public partial class Inputtext : Form
    {
        
        private readonly ScriptProcessingAgent _agent = new ScriptProcessingAgent();

        public Inputtext()
        {
            InitializeComponent();
            

           // StartExractBtn.Click += StartExractBtn_Click;

            txt_FixedList.Text = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Resources",
                "fixed_list.xlsx"
            );
        }

        public class FixedWord
        {
            public string Word { get; set; }
            public string Hiragana { get; set; }
            public string Difficulty { get; set; }
        }

        public class KanjiItem
        {
            public string Word { get; set; }
            public string Hiragana { get; set; }
           
            public string Difficulty { get; set; }
           
            public string Reason { get; set; }
            public string Source { get; set; }          
            public bool SaveToFixedList { get; set; }   
        }
        public List<KanjiItem> FindFixedWordsInText(string inputText, List<FixedWord> fixedWords)
        {
            var result = new List<KanjiItem>();

            if (string.IsNullOrWhiteSpace(inputText) || fixedWords == null)
                return result;

            foreach (var fw in fixedWords.OrderByDescending(x => x.Word.Length))
            {
                if (string.IsNullOrWhiteSpace(fw.Word))
                    continue;

                if (inputText.Contains(fw.Word))
                {
                    result.Add(new KanjiItem
                    {
                        Word = fw.Word,
                        Hiragana = NormalizeToHiragana(fw.Hiragana),
                        Difficulty = string.IsNullOrWhiteSpace(fw.Difficulty) ? "fixed" : fw.Difficulty,
                        Reason = "Found in Fixed List Excel. Fixed List reading has highest priority.",
                        Source = "Fixed",
                        SaveToFixedList = false
                    });
                }
            }

            return result;
        }

        public List<KanjiItem> MergeFixedAndApiResults(
            List<KanjiItem> fixedMatches,
            List<KanjiItem> apiItems,
            List<FixedWord> fixedWords)
        {
            var finalList = new List<KanjiItem>();
            var seen = new HashSet<string>(StringComparer.Ordinal);

            var fixedDict = fixedWords
                .Where(x => !string.IsNullOrWhiteSpace(x.Word))
                .GroupBy(x => x.Word)
                .ToDictionary(g => g.Key, g => g.First(), StringComparer.Ordinal);

            // 1. Fixed List always goes first
            foreach (var item in fixedMatches)
            {
                if (seen.Add(item.Word))
                    finalList.Add(item);
            }

            // 2. ChatGPT result is used only for new/non-fixed words
            foreach (var item in apiItems ?? new List<KanjiItem>())
            {
                if (item == null || string.IsNullOrWhiteSpace(item.Word))
                    continue;

                if (fixedDict.TryGetValue(item.Word, out FixedWord fw))
                {
                    // If ChatGPT found a fixed word, keep Excel reading, not ChatGPT reading.
                    if (seen.Add(item.Word))
                    {
                        finalList.Add(new KanjiItem
                        {
                            Word = fw.Word,
                            Hiragana = NormalizeToHiragana(fw.Hiragana),
                            Difficulty = string.IsNullOrWhiteSpace(fw.Difficulty) ? "fixed" : fw.Difficulty,
                            Reason = "ChatGPT also found this, but Fixed List reading is used.",
                            Source = "Fixed",
                            SaveToFixedList = false
                        });
                    }

                    continue;
                }

                item.Hiragana = NormalizeToHiragana(item.Hiragana);
                item.Source = "ChatGPT";
                item.SaveToFixedList = true;

                if (string.IsNullOrWhiteSpace(item.Difficulty))
                    item.Difficulty = "medium";

                if (string.IsNullOrWhiteSpace(item.Reason))
                    item.Reason = "New word suggested by ChatGPT. Human review required.";

                if (seen.Add(item.Word))
                    finalList.Add(item);
            }

            return finalList;
        }

        public static string NormalizeToHiragana(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var sb = new StringBuilder();

            foreach (char c in text.Trim())
            {
                if (c >= '\u30A1' && c <= '\u30F6')
                    sb.Append((char)(c - 0x60));
                else
                    sb.Append(c);
            }

            return sb.ToString();
        }
        private async void StartExractBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Txt_Input.Text))
            {
                MessageBox.Show("Please input Japanese text first.");
                return;
            }

            try
            {
                StartExractBtn.Enabled = false;
                Txt_Msg.Clear();

                Log("Start processing...");
                Log("Loading fixed list...");

                string fixedListPath = txt_FixedList.Text.Trim();

                List<FixedWord> fixedWords = _agent.LoadFixedList(fixedListPath);
                Log($"Fixed list loaded: {fixedWords.Count} words.");

                Log("Sending text to ChatGPT for kanji extraction...");

                //List<KanjiItem> kanjiList =
                //    await _agent.ExtractKanjiAsync(Txt_Input.Text.Trim(), fixedWords);

                //Log($"Extraction finished. Found {kanjiList.Count} kanji terms.");

                //if (kanjiList.Count == 0)
                //{
                //    MessageBox.Show("No kanji terms found.");
                //    return;
                //}

                //Log("Opening kanji review window...");

                //KanjiReview popup = new KanjiReview(kanjiList);
                //popup.ShowDialog();

                //Log("Kanji review completed.");
                string inputText = Txt_Input.Text.Trim();

                List<KanjiItem> fixedMatches = FindFixedWordsInText(inputText, fixedWords);
                Log($"Fixed words found in input text: {fixedMatches.Count}");

                Log("Sending text to ChatGPT for kanji extraction...");

                List<KanjiItem> apiItems =
                    await _agent.ExtractKanjiAsync(inputText, fixedWords);

                List<KanjiItem> kanjiList =
                    MergeFixedAndApiResults(fixedMatches, apiItems, fixedWords);

                Log($"Extraction finished. Total review terms: {kanjiList.Count}");

                if (kanjiList.Count == 0)
                {
                    MessageBox.Show("No kanji terms found.");
                    return;
                }

                Log("Opening kanji review window...");

                KanjiReview popup = new KanjiReview(kanjiList, fixedListPath);
                popup.ShowDialog();

                Log($"Kanji review completed. Saved/updated: {popup.SavedCount} word(s).");
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
            Txt_Msg.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
        }


        public class ScriptProcessingAgent
        {
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
                        string Difficulty = ws.Cell(row, 3).GetString().Trim();

                        if (string.IsNullOrWhiteSpace(word))
                            continue;

                        list.Add(new FixedWord
                        {
                            Word = word,
                            Hiragana = hira,
                            Difficulty = string.IsNullOrWhiteSpace(Difficulty) ? "General" : Difficulty
                        });
                    }
                }

                return list;
            }

            public async Task<List<KanjiItem>> ExtractKanjiAsync(string inputText, List<FixedWord> fixedWords)
            {
              
                string apiKey =
                Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.User)
                ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY", EnvironmentVariableTarget.Machine)
                ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY");

                if (string.IsNullOrWhiteSpace(apiKey))
                    throw new Exception("OPENAI_API_KEY is missing. Please set it in Windows environment variables.");

                string fixedDictionaryText = BuildFixedDictionaryText(fixedWords);

                string prompt = @"
                                    You are a Japanese TTS pronunciation extraction agent.

                                    Extract kanji words and important Japanese terms from the input text.

                                    Return JSON only.

                                    JSON format:
                                    [
                                      {
                                        ""word"": ""盛岡"",
                                        ""hiragana"": ""もりおか"",
                                        ""difficulty"": ""high"",
                                        ""reason"": ""Place name; reading is not predictable.""
                                      }
                                    ]

                                    Difficulty rules:
                                    low = common word with stable pronunciation.
                                    medium = compound kanji, technical word, cultural word, historical word.
                                    high = place name, shrine name, person name, rare kanji, local term, or difficult pronunciation.

                                    Fixed dictionary has highest priority.
                                    If a word exists in fixed dictionary, use that hiragana reading.

                                    Fixed Dictionary:
                                    " + fixedDictionaryText + @"

                                    Input Text:
                                    " + inputText;

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);

                    var body = new
                    {
                        model = "gpt-5.4-mini",
                        input = prompt
                    };

                    string jsonBody = JsonConvert.SerializeObject(body);

                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("https://api.openai.com/v1/responses", content);

                    string responseText = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                        throw new Exception(responseText);

                    dynamic result = JsonConvert.DeserializeObject(responseText);

                    string outputText = result.output[0].content[0].text;

                    return JsonConvert.DeserializeObject<List<KanjiItem>>(outputText);
                }
            }

            private string BuildFixedDictionaryText(List<FixedWord> fixedWords)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var item in fixedWords)
                {
                    sb.AppendLine(item.Word + " = " + item.Hiragana);
                }

                return sb.ToString();
            }


        }

        private void Back_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_FixedList_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select Language Excel File";
                openFileDialog.Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls|All Files (*.*)|*.*";
                openFileDialog.InitialDirectory = Path.GetDirectoryName(txt_FixedList.Text);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txt_FixedList.Text = openFileDialog.FileName;
                }
            }
        }
    }
}
