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
             string ApiKey= Properties.Settings.Default.OPENAI_API_KEY;

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

                List<KanjiItem> kanjiList =
                    await _agent.ExtractKanjiAsync(Txt_Input.Text.Trim(), fixedWords);

                Log($"Extraction finished. Found {kanjiList.Count} kanji terms.");

                if (kanjiList.Count == 0)
                {
                    MessageBox.Show("No kanji terms found.");
                    return;
                }

                Log("Opening kanji review window...");

                KanjiReview popup = new KanjiReview(kanjiList);
                popup.ShowDialog();

                Log("Kanji review completed.");
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
                //string apiKey = Properties.Settings.Default.OPENAI_API_KEY;
                string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

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
