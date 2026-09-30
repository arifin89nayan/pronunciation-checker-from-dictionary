namespace WindowsFormsApp1.Models
{
    // Generated in memory. Persist this only when the user approves it.
    public class AiGeneratedDraft
    {
        public string ContentType { get; set; }
        public string DisplayText { get; set; }
        public string PayloadJson { get; set; }

        public string ModelName { get; set; }
        public string PromptVersion { get; set; }

        // Audit snapshot. These are the exact values used for this generation.
        public string SystemInstruction { get; set; }
        public string FinalPrompt { get; set; }
        public string PreferencesJson { get; set; }
        public string RawResponseJson { get; set; }

        public bool IsEdited { get; set; }
    }
}
