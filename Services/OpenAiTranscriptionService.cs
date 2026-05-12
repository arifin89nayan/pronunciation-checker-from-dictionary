using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Services
{
    public class OpenAiTranscriptionService
    {
        private readonly string _apiKey;

        public OpenAiTranscriptionService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<string> TranscribeAsync(string wavPath, string language = "ja")
        {
            using (var client = new HttpClient())
            using (var form = new MultipartFormDataContent())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _apiKey);

                form.Add(new StringContent("gpt-4o-mini-transcribe"), "model");
                form.Add(new StringContent(language), "language");

                var fileBytes = File.ReadAllBytes(wavPath);
                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType =
                    new MediaTypeHeaderValue("audio/wav");

                form.Add(fileContent, "file", Path.GetFileName(wavPath));

                var response = await client.PostAsync(
                    "https://api.openai.com/v1/audio/transcriptions",
                    form
                );

                string json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new Exception("OpenAI transcription failed: " + json);

                using (var doc = JsonDocument.Parse(json))
                {
                    return doc.RootElement.GetProperty("text").GetString();
                }
            }
        }
    }
}
