namespace WindowsFormsApp1.Models.AutoConverters
{
    public class GuideParsedModel : WidgetParsedCommonModel
    { 

        public string GuideTitle { get; set; }
        public string Generate { get; set; }
        public string Guide { get; set; }
        public string language { get; set; }
        //public string Voice { get; set; }
        //public string TemplateName { get; set; }
        public string AudioFile { get; set; }
        
        public string QuizexpAudioFile { get; set; }
        public string GuidePronounce { get; set; }
        public string GuidePronounceExtra { get; set; }
        public string TitleFontSize { get; set; }
        public string TitleFontColor { get; set; }
    }
}
