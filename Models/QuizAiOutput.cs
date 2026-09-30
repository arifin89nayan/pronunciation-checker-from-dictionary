using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WindowsFormsApp1.Models
{
    public class QuizAiOutput
    {
        [JsonPropertyName("question")]
        public string Question { get; set; }

        [JsonPropertyName("choices")]
        public List<string> Choices { get; set; }

        [JsonPropertyName("correct_index")]
        public int CorrectIndex { get; set; }

        [JsonPropertyName("explanation")]
        public string Explanation { get; set; }
    }
}
