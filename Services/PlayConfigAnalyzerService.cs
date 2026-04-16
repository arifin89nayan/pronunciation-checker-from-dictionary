using System.IO;
using System;
using System.Threading.Tasks;
using WindowsFormsApp1.Models.Widgets;
using WindowsFormsApp1.Exceptions;
using System.Linq;
using System.Collections.Generic;
using WindowsFormsApp1.Enums;

namespace WindowsFormsApp1.Services
{
    public class PlayConfigAnalyzerService
    {
        public async Task<WidgetContainer> ReadAsync(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length < 2)
            {
                throw new InvalidPlayConfigException("Invalid playconfig file.Contain invalid data");
            }

            string version = lines[0];

            if (!int.TryParse(lines[1], out int widgetCount))
            {
                throw new InvalidPlayConfigException($"Invalid playconfig file.Contain invalid version {lines[1]} data");
            }

            if (widgetCount < 1)
            {
                throw new InvalidPlayConfigException("Widget count cannot be 0 or less!");
            }

            int currentWidgetCount = 1;
            var widgets = new List<Widget>();
            int lineOrder = 0;
            var linesList = new List<Line>();
            int correctAnsIndicatorOrder = 0;

            for (int i = 2; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    //ErrorLogger.LogError($"Playconfig  file have a empty line: {lines[i]}");
                    throw new InvalidPlayConfigException($"Empty line detected at line {lines[i]}. Please remove Empty line.");
                   
                }

                var line = new Line()
                {
                    WidgetOrder = ++lineOrder,
                    Text = lines[i],
                    GlobalOrder = i + 1,
                    WidgetId = currentWidgetCount
                };

                if (lines[i] == $"<{currentWidgetCount}><1>")
                {
                    //Wigted start
                    line.Type = LineTypeEnum.Start;
                    linesList.Add(line);
                }
                else if (lines[i] == "<E>")
                {
                    //Widget end
                    line.Type = LineTypeEnum.End;
                    linesList.Add(line);

                    var widget = new Widget();
                    widget.Order = widgets.Count + 1;
                    widget.Lines.AddRange(linesList);
                    widget.FullText = string.Join
                        ("\n", linesList.OrderBy(c => c.WidgetOrder).Select(c => c.Text));

                    widgets.Add(widget);
                    linesList.Clear();
                    currentWidgetCount++;
                    //Reset widget count
                    lineOrder = 0;
                    correctAnsIndicatorOrder = 0;
                }
                else
                {
                    //Widget middle lines

                    var widgetType = GetWidgetType(lines[i]);
                    if (widgetType != null)
                    {
                        line.WidgetType = (WidgetTypeEnum)widgetType;
                    }
                    var type = GetLineType(line.Text, lines[i + 1], lines[i - 1]);

                    if (type != null)
                    {
                        line.Type = (LineTypeEnum)type;

                        if (line.Type == LineTypeEnum.Option)
                        {
                            if (lines[i - 1].StartsWith("<H><T>"))
                            {
                                line.CorrectAnsIndicatorOrder = ++correctAnsIndicatorOrder;
                            }
                        }
                    }
                    linesList.Add(line);
                }
            }

            return new WidgetContainer
            {
                Version = version,
                Widgets = widgets,
            };
        }

        public LineTypeEnum? GetLineType(string text, string next,string previous)
        {
            if (next.Contains("<M>"))
            {
                return LineTypeEnum.MediaDuration;
            }
            else if (text.Contains("<M>"))
            {
                return LineTypeEnum.Media;
            }
            else if (text.Contains("<P>"))
            {
                return LineTypeEnum.Animation;
            }
            else if (text.Contains("<C>"))
            {
                return LineTypeEnum.Caption;
            }
            else if (text.StartsWith("<T>") || text.StartsWith("Template<T>"))
            {
                return LineTypeEnum.Text;
            }
            else if (text.Contains("<S>"))
            {
                return LineTypeEnum.Selection;
            }
            else if (text.StartsWith("<N><T>") || text.StartsWith("<H><T>"))
            {
                return LineTypeEnum.Option;
            }
            else if (text == "<E>")
            {
                return LineTypeEnum.End;
            }
            else if (previous.StartsWith("<H><T>"))
            {
                return LineTypeEnum.Option;
            }
            return null;

        }

        public LineTypeEnum? GetLineType(string text, string aheadLine, int order)
        {
            if (order == 1) return LineTypeEnum.Version;

            else if (order == 2) return LineTypeEnum.WidgetNumber;

            else return GetLineType(text, aheadLine, previous: "");
        }
        public WidgetTypeEnum? GetWidgetType(string line)
        {
            if (line.Contains("Template<T>"))
            {
                return WidgetTypeEnum.Text;
            }
            else if (line.Contains("Template<C>"))
            {
                return WidgetTypeEnum.Caption;
            }
            else if (line.Contains("Template<P>"))
            {
                return WidgetTypeEnum.Animation;
            }
            else if (line.Contains("Template<S>"))
            {
                return WidgetTypeEnum.Selection;
            }
            else
                return null;

        }


    }
}
