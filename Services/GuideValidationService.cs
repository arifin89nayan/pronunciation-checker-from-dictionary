using System;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class GuideValidationResult
    {
        public bool Passed { get; set; }

        public string Status { get; set; }

        public string Message { get; set; }
    }


    public class GuideValidationService
    {
        public GuideValidationResult Validate(
            ContentItem item,
            string generatedText)
        {
            if (string.IsNullOrWhiteSpace(
                    generatedText))
            {
                return new GuideValidationResult
                {
                    Passed = false,
                    Status = "Failed",
                    Message =
                        "Generated text is empty."
                };
            }


            if (generatedText.Trim().Length < 30)
            {
                return new GuideValidationResult
                {
                    Passed = false,
                    Status = "Review",
                    Message =
                        "Generated text may be too short."
                };
            }


            if (string.IsNullOrWhiteSpace(
                    item.SourceText))
            {
                return new GuideValidationResult
                {
                    Passed = false,
                    Status = "Failed",
                    Message =
                        "Original source text is missing."
                };
            }


            return new GuideValidationResult
            {
                Passed = true,
                Status = "Passed",
                Message =
                    "Basic validation passed."
            };
        }
    }
}