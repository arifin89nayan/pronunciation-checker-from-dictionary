using System.Threading.Tasks;
using WindowsFormsApp1.Models.AutoConverters;
using WindowsFormsApp1.Models.Widgets;

namespace WindowsFormsApp1.Services.Abstraction
{
    public interface IAutoWidgetProcessor
    {
        //Task ProcessWidges(Widget widget, WidgetParsedCommonModel widgetParsedModel);
        Task ProcessWidges(Widget widget, WidgetParsedCommonModel widgetParsedModel);
    }
    public interface IAutoWidgetProcessorQuiz
    {
        Task ProcessWidges(Widget widget, QuizParserModel widgetParsedModel);
        //Task ProcessWidges(Widget widget, WidgetParsedModel widgetParsedModel);
    }
}
