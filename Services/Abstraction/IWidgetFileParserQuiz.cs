using System.Collections.Generic;
using WindowsFormsApp1.Models.AutoConverters;

namespace WindowsFormsApp1.Services.Abstraction
{
    public interface IWidgetFileParserQuiz
    {
        // IEnumerable<QuizParserModel> ParseFile(string filePath);
        IEnumerable<WidgetParsedCommonModel> ParseFile(string filePath);
    }
    

}
