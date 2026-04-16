using CsvHelper.Configuration; 
using WindowsFormsApp1.Models.AutoConverters;

namespace WindowsFormsApp1.Helper.Mapper
{
    public class Template2CsvFileParserMapper : ClassMap<QuizParserModel>
    {
        public Template2CsvFileParserMapper()
        {
            Map(m => m.Sl).Name("Sl");
            Map(m => m.QuizFolderName).Name("Quiz Folder Name");
            Map(m => m.QuizID).Name("Quiz ID");
            Map(m => m.QuizQuestion).Name("Quiz Question");
            Map(m => m.QuizCorrectAnswer).Name("Quiz Correct Answer");
            Map(m => m.QuizTemplate).Name("Quiz Template");
            Map(m => m.QuizQuestionAudio).Name("Quiz Question Audio");
            Map(m => m.QuizExplanationAudio).Name("Quiz Explanation Audio");
            Map(m => m.SelectionNumber).Name("Selection Number");
            Map(m => m.QuizCorrectAnswerSelection).Name("Quiz  Correct Answer Selection");
            Map(m => m.SelectionA).Name("Selection A");
            Map(m => m.SelectionB).Name("Selection B");
            Map(m => m.SelectionC).Name("Selection C");
            Map(m => m.SelectionD).Name("Selection D");
        }
    }
}
