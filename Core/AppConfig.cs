using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class AppConfig
    {
        public string LlmApiKey { get; set; } = "";
        public string LlmModel { get; set; } = "claude-sonnet-4-20250514";

        public string AzureSpeechKey { get; set; } = "";
        public string AzureSpeechRegion { get; set; } = "japaneast";

        // Folder where generated .wav files and exported lists are written.
        public string OutputFolder { get; set; } =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TTSAgent");

        public static AppConfig Load()
        {
            var cfg = new AppConfig();
            string path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            try
            {
                if (File.Exists(path))
                {
                    var loaded = JsonSerializer.Deserialize<AppConfig>(File.ReadAllText(path));
                    if (loaded != null) cfg = loaded;
                }
            }
            catch { /* fall back to defaults / env vars */ }

            // Environment variables override the file if present.
            cfg.LlmApiKey = Env("ANTHROPIC_API_KEY", cfg.LlmApiKey);
            cfg.AzureSpeechKey = Env("AZURE_SPEECH_KEY", cfg.AzureSpeechKey);
            cfg.AzureSpeechRegion = Env("AZURE_SPEECH_REGION", cfg.AzureSpeechRegion);

            Directory.CreateDirectory(cfg.OutputFolder);
            return cfg;
        }

        private static string Env(string name, string fallback)
        {
            string v = Environment.GetEnvironmentVariable(name);
            return string.IsNullOrWhiteSpace(v) ? fallback : v;
        }
    }
}
