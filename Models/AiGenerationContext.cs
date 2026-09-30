namespace WindowsFormsApp1.Models
{
    public class AiGenerationContext
    {
        public string ContentType { get; set; }
        public string Topic { get; set; }
        public string OutputLanguage { get; set; }
        public string SourceText { get; set; }

        public string AgeGroup { get; set; }
        public string KnowledgeLevel { get; set; }
        public string Tone { get; set; }
        public int MaximumWords { get; set; }

        public string Difficulty { get; set; }
        public int ChoiceCount { get; set; }

        public string PromptVersion { get; set; }
        public string Model { get; set; }
        public int MaxOutputTokens { get; set; }
    }
}
