using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.UIDesign
{
    public class AzureTtsSimpleService
    {
        private static readonly HttpClient Http = new HttpClient
        {
            Timeout = TimeSpan.FromMinutes(2)
        };

        public async Task<string> GenerateAudioAsync(
            string ssml,
            string outputFolder,
            string outputFileName)
        {
            string key =
                Environment.GetEnvironmentVariable("AZURE_SPEECH_KEY", EnvironmentVariableTarget.User)
                ?? Environment.GetEnvironmentVariable("AZURE_SPEECH_KEY", EnvironmentVariableTarget.Machine)
                ?? Environment.GetEnvironmentVariable("AZURE_SPEECH_KEY");

           
               string region = "japaneast";

            if (string.IsNullOrWhiteSpace(key))
                throw new Exception("AZURE_SPEECH_KEY is missing.");

            if (string.IsNullOrWhiteSpace(region))
                throw new Exception("AZURE_SPEECH_REGION is missing. Example: japaneast");

            string endpoint =
                "https://" + region + ".tts.speech.microsoft.com/cognitiveservices/v1";

            Directory.CreateDirectory(outputFolder);

            string outputPath = Path.Combine(outputFolder, outputFileName);

            using (var req = new HttpRequestMessage(HttpMethod.Post, endpoint))
            {
                req.Headers.Add("Ocp-Apim-Subscription-Key", key);
                req.Headers.Add("X-Microsoft-OutputFormat", "riff-24khz-16bit-mono-pcm");
                req.Headers.Add("User-Agent", "TTSAgent");

                req.Content = new StringContent(
                    ssml,
                    Encoding.UTF8,
                    "application/ssml+xml"
                );

                using (HttpResponseMessage resp = await Http.SendAsync(req))
                {
                    string responseText = await resp.Content.ReadAsStringAsync();

                    if (!resp.IsSuccessStatusCode)
                    {
                        throw new Exception(
                            "Azure TTS failed (" + (int)resp.StatusCode + "): " + responseText
                        );
                    }

                    using (var fs = File.Create(outputPath))
                    {
                        await resp.Content.CopyToAsync(fs);
                    }
                }
            }

            return outputPath;
        }
    }
}