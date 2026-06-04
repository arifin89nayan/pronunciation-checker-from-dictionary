using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Whisper.net;

namespace WindowsFormsApp1.Services
{
    public class WhisperNetSpeechToTextService
    {
        private readonly string _modelPath;

        public WhisperNetSpeechToTextService(string modelPath)
        {
            _modelPath = modelPath;
        }

        public async Task<string> TranscribeAsync(
            string wavPath,
            string initialPrompt,
            string language = "ja")
        {
            if (!File.Exists(wavPath))
                throw new FileNotFoundException("WAV file not found.", wavPath);

            if (!File.Exists(_modelPath))
                throw new FileNotFoundException("Whisper model file not found.", _modelPath);

            StringBuilder resultText = new StringBuilder();

            using (var whisperFactory = WhisperFactory.FromPath(_modelPath))
            {
                using (var processor = whisperFactory.CreateBuilder()
                    .WithLanguage(language)
                    .WithPrompt(initialPrompt ?? "")
                    .WithCarryInitialPrompt(true)
                    .WithTemperature(0.0f)
                    .Build())
                {
                    using (var fileStream = File.OpenRead(wavPath))
                    {
                        // Workaround for lack of async streams in C# 7.3
                        var segmentEnumerator = processor.ProcessAsync(fileStream).GetAsyncEnumerator();
                        try
                        {
                            while (await segmentEnumerator.MoveNextAsync())
                            {
                                var segment = segmentEnumerator.Current;
                                if (!string.IsNullOrWhiteSpace(segment.Text))
                                {
                                    resultText.Append(segment.Text);
                                }
                            }
                        }
                        finally
                        {
                            if (segmentEnumerator != null)
                                await segmentEnumerator.DisposeAsync();
                        }
                    }
                }
            }

            return resultText.ToString();
        }
    }
}