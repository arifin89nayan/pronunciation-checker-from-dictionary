using System;
using System.IO;
using System.Text.Json;

namespace WindowsFormsApp1
{
    public class AppConfig
    {
         public string OpenAiApiKey { get; private set; }  
        // You can change this model from appsettings.json.
        public string LlmModel { get; set; } = "gpt-5.4-mini";

        public string AzureSpeechKey { get; set; } 
        public string AzureSpeechRegion { get; set; } = "japaneast";

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

                    if (loaded != null)
                        cfg = loaded;
                }
            }
            catch
            {
                // Use defaults / environment variables.
            }

            //cfg.OpenAiApiKey = Env("OPENAI_API_KEY", cfg.OpenAiApiKey);
            cfg.OpenAiApiKey = Env("OPENAI_API_KEY", cfg.OpenAiApiKey);

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