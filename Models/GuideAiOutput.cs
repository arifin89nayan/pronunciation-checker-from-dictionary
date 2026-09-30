using System.Text.Json.Serialization;

namespace WindowsFormsApp1.Models
{
    public class GuideAiOutput
    {
        [JsonPropertyName("guide_text")]
        public string GuideText { get; set; }
    }
}
