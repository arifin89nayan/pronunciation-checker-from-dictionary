using System.Collections.Generic;
using WindowsFormsApp1.Models.AutoConverters;

namespace WindowsFormsApp1.Services.Abstraction
{
    public interface IWidgetFileParser
    {
        IEnumerable<WidgetParsedCommonModel> ParseFile(string filePath);
    }
}
