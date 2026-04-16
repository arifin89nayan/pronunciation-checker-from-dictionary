using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WindowsFormsApp1.Enums;

namespace WindowsFormsApp1.Models.Widgets
{
    public class Widget
    {
        public string FullText { get; set; }
        public int Order { get; set; }
        // public List<WidgetTypeEnum> Types { get => Lines.Where(c => c.Type != 0).Select(l => l.Type).ToList();} //What kind of change. Animation, Text, Caption, Selection
        public List<Line> Lines { get; set; } = new List<Line>();
        public List<Line> GetFromLines()
        {
            return Lines.Where(l => l.WidgetType != 0).ToList();
        }

        public bool IsContainSelection
        {
            get
            {
                if (Lines.Any(c => c.IsSelection))
                {
                    return true;
                }

                return false;
            }
        }

    }

    public class Line
    {
        public int WidgetId { get; set; }
        public int WidgetOrder { get; set; }
        public int GlobalOrder { get; set; }
        public string Text { get; set; }
        public WidgetTypeEnum WidgetType { get; set; }
        public LineTypeEnum Type { get; set; }
        public bool IsSkippedLine
        {
            get
            {
                foreach (var keyword in SkippableKeyword())
                {
                    if (Text.StartsWith(keyword + WidgetId))
                    {
                        return true;
                    }

                }
                return false;
            }
        }

        public bool IsOption
        {
            get
            {
                foreach (var keyword in SelectionStartKeyword())
                {
                    if (Text.StartsWith(keyword))
                    {
                        return true;
                    }

                }
                return false;
            }
        }
        public bool IsQuestion { get => Text.StartsWith("Template<T>"); }
        public bool IsSelection { get => Text.StartsWith("Template<S>"); }
        public int CorrectAnsIndicatorOrder { get; set; }

        public IList<string> GetFileNames()
        {
            string pattern = @"\b\w+\.(jpg|png|mp3|txt|WMA|csv)\b";

            // Use Regex.Matches to find all matches
            MatchCollection matches = Regex.Matches(Text, pattern, RegexOptions.IgnoreCase);

            // Store the matches in a list or print them
            IList<string> fileNames = new List<string>();
            foreach (Match match in matches)
            {
                fileNames.Add(match.Value);
            }

            return fileNames;
        }

        private IList<string> SkippableKeyword()
        {
            return new List<string>()
            {
                "Duration", "Audio"
            };
        }


        public IList<string> SelectionStartKeyword()
        {
            return new List<string>
            {
                "<N><T>",
                "<H><T>"
            };
        }
    }
}
