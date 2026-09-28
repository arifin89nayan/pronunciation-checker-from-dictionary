using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models.AutoConverters;

namespace WindowsFormsApp1.Models
{
    public class ContentItem
    {
        
        public int RowNumber { get; set; }

        // GuideId or QuizID
        public string SourceId { get; set; }

        public string Topic { get; set; }

        // Example: JP_MALE / Eng_US
        public string Language { get; set; }

        // Example: ja-JP / en-US
        public string LanguageCode { get; set; }

        public string Voice { get; set; }

        public string TemplateName { get; set; }

        public string ContentType { get; set; }

        // Original Excel source text
        public string SourceText { get; set; }

        // Current AI/generated/edited text
        public string GeneratedText { get; set; }

        public string AiStatus { get; set; }

        public string ValidationStatus { get; set; }

        public string ReviewStatus { get; set; }

        public string ProcessStatus { get; set; }

        // SQLite IDs
        public long DatabaseItemId { get; set; }

        public long? CurrentVersionId { get; set; }

        // Keep original parsed Excel record
        public WidgetParsedCommonModel OriginalRecord { get; set; }
    }
}
