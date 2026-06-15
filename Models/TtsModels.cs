using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    // ---- LLM extraction result (Screen 2/3) ----
    public class TtsExtractionResult
    {
        [JsonPropertyName("summary")] public TtsExtractionSummary Summary { get; set; } = new TtsExtractionSummary();
        [JsonPropertyName("terms")] public List<TtsTerm> Terms { get; set; } = new List<TtsTerm>();
    }

    public class TtsExtractionSummary
    {
        [JsonPropertyName("total_terms")] public int TotalTerms { get; set; }
        [JsonPropertyName("fixed_dictionary_matches")] public int FixedDictionaryMatches { get; set; }
        [JsonPropertyName("new_terms")] public int NewTerms { get; set; }
        [JsonPropertyName("review_required_count")] public int ReviewRequiredCount { get; set; }
        [JsonPropertyName("conflict_count")] public int ConflictCount { get; set; }
    }

    public class TtsTerm
    {
        [JsonPropertyName("word")] public string Word { get; set; }
        [JsonPropertyName("hiragana")] public string Hiragana { get; set; }
        [JsonPropertyName("source_sentence")] public string SourceSentence { get; set; }
        [JsonPropertyName("category")] public string Category { get; set; }
        [JsonPropertyName("dictionary_status")] public string DictionaryStatus { get; set; } // matched|new|conflict
        [JsonPropertyName("reading_source")] public string ReadingSource { get; set; }
        [JsonPropertyName("review_required")] public bool ReviewRequired { get; set; }
        [JsonPropertyName("confidence")] public string Confidence { get; set; }       // high|medium|low
        [JsonPropertyName("reason")] public string Reason { get; set; }

        [JsonIgnore] public string ModelSuggestedReading { get; set; }
    }

    // ---- Human review queue item (Screen 4) ----
    public enum ReviewState { Pending, Editing, Approved, Rejected }

    public class ReviewItem
    {
        public string Word { get; set; }
        public string ApiReading { get; set; }
        public string CorrectReading { get; set; }
        public string SourceSentence { get; set; }
        public string Category { get; set; } = "general_word";
        public string SaveType { get; set; } = "Fixed List"; // Fixed List | General Only | Skip
        public ReviewState State { get; set; } = ReviewState.Pending;
    }

    // ---- Permanent dictionary entry (Screen 5) ----
    public class DictionaryEntry
    {
        public string Word { get; set; }
        public string Hiragana { get; set; }
        public string Category { get; set; } = "General";
        public string Status { get; set; } = "Approved";
        public DateTime Updated { get; set; } = DateTime.Today;
    }

    // ---- Final merged TTS list rows (Screen 6) ----
    public class TtsListRow
    {
        public string Word { get; set; }
        public string Hiragana { get; set; }
        public string Source { get; set; }   // Fixed | General
        public string UseType { get; set; } = "SSML";
    }

    // ---- Quality check result (Screen 8) ----
    public class QualityCheckRow
    {
        public string CheckType { get; set; }
        public string Result { get; set; }   // Pass | Warning | Fail
        public string Score { get; set; }
        public string Details { get; set; }
    }
}
