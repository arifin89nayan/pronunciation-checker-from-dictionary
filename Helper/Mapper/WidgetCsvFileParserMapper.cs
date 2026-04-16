using CsvHelper.Configuration;
using WindowsFormsApp1.Models.AutoConverters;

namespace WindowsFormsApp1.Helper.Mapper
{
    public class WidgetCsvFileParserMapper : ClassMap<GuideParsedModel>
    {
        public WidgetCsvFileParserMapper()
        {
            Map(m => m.GuideId).Name("guide ID");
            Map(m => m.GuideName).Name("guide Name");
            Map(m => m.GuideTitle).Name("guide Title");
            Map(m => m.Guide).Name("guide");
            Map(m => m.TemplateName).Name("template Name");
            Map(m => m.AudioFile).Name("Audio File");
        }
    }
}
