using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class AzureTtsService
    {
        private static readonly HttpClient Http = new HttpClient { Timeout = TimeSpan.FromMinutes(2) };
        private readonly AppConfig _config;

        public AzureTtsService(AppConfig config) => _config = config;

        /// <summary>
        /// Inject readings into the raw script. Longest words first so a longer
        /// term (上米内) is replaced before a contained shorter one (米内).
        /// Uses &lt;sub alias="kana"&gt; which reliably forces the reading for ja-JP.
        /// </summary>
        public string BuildSsml(string script, IEnumerable<TtsListRow> rows,
                                string voice, string style = "narration",
                                int ratePercent = 0, int pitchPercent = 0)
        {
            string body = System.Security.SecurityElement.Escape(script ?? "");

            foreach (var r in rows.Where(r => !string.IsNullOrEmpty(r.Word))
                                   .OrderByDescending(r => r.Word.Length))
            {
                string word = System.Security.SecurityElement.Escape(r.Word);
                string kana = System.Security.SecurityElement.Escape(r.Hiragana);
                string tag = $"<sub alias=\"{kana}\">{word}</sub>";
                body = body.Replace(word, tag);
            }

            string rate = (ratePercent >= 0 ? "+" : "") + ratePercent + "%";
            string pitch = (pitchPercent >= 0 ? "+" : "") + pitchPercent + "%";

            var sb = new StringBuilder();
            sb.AppendLine("<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" " +
                          "xmlns:mstts=\"https://www.w3.org/2001/mstts\" xml:lang=\"ja-JP\">");
            sb.AppendLine($"  <voice name=\"{voice}\">");
            bool useStyle = !string.IsNullOrWhiteSpace(style) && style != "none";
            if (useStyle) sb.AppendLine($"    <mstts:express-as style=\"{style}\">");
            sb.AppendLine($"      <prosody rate=\"{rate}\" pitch=\"{pitch}\">");
            sb.AppendLine("        " + body);
            sb.AppendLine("      </prosody>");
            if (useStyle) sb.AppendLine("    </mstts:express-as>");
            sb.AppendLine("  </voice>");
            sb.AppendLine("</speak>");
            return sb.ToString();
        }

        /// <summary>Basic well-formedness check (Validate SSML button).</summary>
        public (bool ok, string message) ValidateSsml(string ssml)
        {
            try
            {
                System.Xml.Linq.XDocument.Parse(ssml);
                return (true, "SSML is well-formed XML.");
            }
            catch (Exception ex)
            {
                return (false, "Invalid SSML: " + ex.Message);
            }
        }

        /// <summary>Synthesize to a .wav file. Returns the output path.</summary>
        public async Task<string> SynthesizeAsync(string ssml, string outputFileName)
        {
            if (string.IsNullOrWhiteSpace(_config.AzureSpeechKey))
                throw new InvalidOperationException("Azure Speech key is not configured.");

            string endpoint = $"https://{_config.AzureSpeechRegion}.tts.speech.microsoft.com/cognitiveservices/v1";

            using (var req = new HttpRequestMessage(HttpMethod.Post, endpoint))
            {
                req.Headers.Add("Ocp-Apim-Subscription-Key", _config.AzureSpeechKey);
                req.Headers.Add("X-Microsoft-OutputFormat", "riff-24khz-16bit-mono-pcm");
                req.Headers.Add("User-Agent", "TTSAgent");
                req.Content = new StringContent(ssml, Encoding.UTF8, "application/ssml+xml");

                using (var resp = await Http.SendAsync(req).ConfigureAwait(false))
                {
                    if (!resp.IsSuccessStatusCode)
                    {
                        string err = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new Exception($"Azure TTS failed ({(int)resp.StatusCode}): {err}");
                    }

                    Directory.CreateDirectory(_config.OutputFolder);
                    string path = Path.Combine(_config.OutputFolder, outputFileName);
                    using (var fs = File.Create(path))
                    {
                        await resp.Content.CopyToAsync(fs).ConfigureAwait(false);
                    }
                    return path;
                }
            }
        }
    }
}
