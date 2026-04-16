using System.Collections.Generic;

namespace WindowsFormsApp1.Models.Widgets
{
    public class WidgetContainer
    {
        public string Version { get; set; }
        public IList<Widget> Widgets { get; set; } = new List<Widget>();
    }
}
