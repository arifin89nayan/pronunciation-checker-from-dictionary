using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class AudioQualityFinalResult
    {
        public string FinalGrade { get; set; } // PASS, WARNING, FAIL
        public string Message { get; set; }

        public double AzureAccuracyScore { get; set; }
        public double AzureFluencyScore { get; set; }
        public double AzureCompletenessScore { get; set; }

        public string RecognizedText { get; set; }
        public double CerPercent { get; set; }

        public double DictionaryPassRate { get; set; }

        public List<AzureWordScore> AzureWords { get; set; } = new List<AzureWordScore>();
        public List<DictionaryWordCheck> DictionaryWords { get; set; } = new List<DictionaryWordCheck>();
    }

    public class AzureWordScore
    {
        public string Word { get; set; }
        public double AccuracyScore { get; set; }
        public string ErrorType { get; set; }
    }

    public class DictionaryWordCheck
    {
        public string Word { get; set; }
        public string ExpectedPhoneme { get; set; }
        public bool FoundInOriginalText { get; set; }
        public bool FoundInRecognizedText { get; set; }
        public double AzureAccuracyScore { get; set; }
        public string Status { get; set; } // PASS, WARNING, FAIL
        public string Message { get; set; }
    }
}
