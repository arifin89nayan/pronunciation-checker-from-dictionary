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

        public string Topic { get; set; }

        public string Language { get; set; }

        public string Voice { get; set; }

        public string TemplateName { get; set; }

        public string ContentType { get; set; }

        // Original Excel content that AI will use later
        public string SourceText { get; set; }

        // Future AI result
        public string GeneratedText { get; set; }

        public string AiStatus { get; set; }

        public string ValidationStatus { get; set; }

        public string ReviewStatus { get; set; }

        public string ProcessStatus { get; set; }

        // Keep original parsed model.
        // Later we can write generated AI data back into it.
        public WidgetParsedCommonModel OriginalRecord { get; set; }
    }
}
