using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1
{
    public class AppState
    {
        public AppConfig Config { get; }
        public FixedDictionaryService Dictionary { get; }
        public KanjiExtractionService Extractor { get; private set; }
        public AzureTtsService Tts { get; }
        public QualityCheckService Quality { get; }
        public ConfirmationListStore Confirmation { get; } = new ConfirmationListStore();

        // --- project settings (Screen 2) ---
        public string ProjectName { get; set; } = "Museum_Edo_Script_01";
        public string Voice { get; set; } = "ja-JP-NanamiNeural";
        public string Speed { get; set; } = "Normal";
        public string Script { get; set; } = "";

        // --- pipeline artifacts ---
        public TtsExtractionResult Extraction { get; set; }
        public List<ReviewItem> ReviewQueue { get; } = new List<ReviewItem>();
        public List<TtsListRow> FinalTtsList { get; } = new List<TtsListRow>();
        public string LastSsml { get; set; } = "";
        public string LastAudioPath { get; set; } = "";

        // Raised whenever a screen wants the shell to refresh nav badges, etc.
        public event Action StateChanged;
        public void NotifyChanged() => StateChanged?.Invoke();

        public AppState(AppConfig config)
        {
            //Config = config;
            //Dictionary = new FixedDictionaryService();
            //Extractor = new KanjiExtractionService(config, Dictionary);
            //Tts = new AzureTtsService(config);
            //Quality = new QualityCheckService(config);
            try
            {
                Config = config;
                Dictionary = new FixedDictionaryService();
                Extractor = new KanjiExtractionService(config, Dictionary);
                Tts = new AzureTtsService(config);
                Quality = new QualityCheckService(config);

            }   
            
            catch (Exception ex)
                {
                MessageBox.Show(ex.ToString(), "Failed to open agent");
            }
        }

        public void RebuildExtractor() =>
            Extractor = new KanjiExtractionService(Config, Dictionary);
    }
}
