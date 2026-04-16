using NAudio.Gui;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1.Models.AutoConverters
{
    public class QuizParserModel : WidgetParsedCommonModel
    {
        

        public string Sl { get; set; }
        public string Generate { get; set; }
        public string QuizOrgUid { get; set; }
        public string Language { get; set; }
        public string QuizTemplate { get; set; }
        public string QuizFolderName { get; set; }
        public string QuizID { get; set; }
      
       
        //public string Voice { get; set; } 
        public string QuizQuestion { get; set; }
        public string QuizQuestionAudio { get; set; }

        public string QuizCorrectAnswer { get; set; } 
        public string QuizExplanationAudio { get; set; }
        public string SelectionNumber { get; set; }
        public string QuizCorrectAnswerSelection { get; set; }
        public string SelectionA { get; set; }
        public string SelectionB { get; set; }
        public string SelectionC { get; set; }
        public string SelectionD { get; set; }
        public string TitleFontSize { get; set; }
        public string TitleFontColor { get; set; }

    }
}
