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
        public string SourceId { get; set; }
        public string Topic { get; set; }
        public string Language { get; set; }
        public string LanguageCode { get; set; }
        public string Voice { get; set; }
        public string TemplateName { get; set; }
        public string ContentType { get; set; }
        public string SourceText { get; set; }
        public string GeneratedText { get; set; }
        public string AiStatus { get; set; }
        public string ValidationStatus { get; set; }
        public string ReviewStatus { get; set; }
        public string ProcessStatus { get; set; }
        public long DatabaseItemId { get; set; }
        public long? CurrentVersionId { get; set; }
        public WidgetParsedCommonModel OriginalRecord { get; set; }

        // Optional per-row AI overrides populated from Content.xlsx.
        // Leave them empty/zero to use AISettings.json defaults.
        public string AiAgeGroupOverride { get; set; }
        public string AiKnowledgeLevelOverride { get; set; }
        public string AiToneOverride { get; set; }
        public int AiMaximumWordsOverride { get; set; }
        public string AiDifficultyOverride { get; set; }
        public int AiChoiceCountOverride { get; set; }
    }
}
