namespace WindowsFormsApp1.Models
{
    public class AiPreference
    {
        public string AgeGroup { get; set; } = "General Visitor";

        public string KnowledgeLevel { get; set; } = "Beginner";

        public string Tone { get; set; } = "Clear and educational";

        public int MaximumWords { get; set; } = 120;
        public string Difficulty { get; set; }
        public int ChoiceCount { get; set; }
    }
}


