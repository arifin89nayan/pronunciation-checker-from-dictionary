namespace WindowsFormsApp1.Models
{
    public class GeneratedContentResult
    {
        public string ContentType { get; set; }

        // Human-readable text shown in review form.
        public string DisplayText { get; set; }

        // Canonical structured AI result.
        public string PayloadJson { get; set; }

        public string ModelName { get; set; }

        public string PromptVersion { get; set; }
    }
}