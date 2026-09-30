namespace WindowsFormsApp1.Models
{
    public class PromptPackage
    {
        public string ContentType { get; set; }
        public string Model { get; set; }
        public string PromptVersion { get; set; }
        public string SystemInstruction { get; set; }
        public string UserPrompt { get; set; }
        public string PreferencesJson { get; set; }
        public int MaxOutputTokens { get; set; }
    }
}
