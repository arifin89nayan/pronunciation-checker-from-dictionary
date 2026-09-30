namespace WindowsFormsApp1.Models
{
    public class AiSettings
    {
        public string Model { get; set; } = "gpt-5.6-luna";
        public GuideAiSettings Guide { get; set; } = new GuideAiSettings();
        public QuizAiSettings Quiz { get; set; } = new QuizAiSettings();
    }

    public class GuideAiSettings
    {
        public string AgeGroup { get; set; } = "General Visitor";
        public string KnowledgeLevel { get; set; } = "Beginner";
        public string Tone { get; set; } = "Clear and educational";
        public int MaximumWords { get; set; } = 120;
        public string PromptVersion { get; set; } = "GUIDE_V1";
        public int MaxOutputTokens { get; set; } = 800;
    }

    public class QuizAiSettings
    {
        public string AgeGroup { get; set; } = "General Visitor";
        public string KnowledgeLevel { get; set; } = "Beginner";
        public string Difficulty { get; set; } = "Easy";
        public int ChoiceCount { get; set; } = 4;
        public string PromptVersion { get; set; } = "QUIZ_V1";
        public int MaxOutputTokens { get; set; } = 1000;
    }
}
