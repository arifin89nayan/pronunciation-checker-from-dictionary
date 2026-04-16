using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApp1.Exceptions;
using WindowsFormsApp1.Models.Widgets;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.Global
{
    public sealed class GlobalConfigPropreties
    {
        private static readonly Lazy<GlobalConfigPropreties> _instance = new Lazy<GlobalConfigPropreties>(() => new GlobalConfigPropreties());
        public static GlobalConfigPropreties Instance => _instance.Value;

        public StringBuilder PlayConfig { get; } = new StringBuilder();
        private IList<Line> Lines = new List<Line>();

        public void AddLine(string text)
        {
            var analyzer = new PlayConfigAnalyzerService();
            var lineOrder = Lines.Count + 1;

            var line = new Line
            {
                GlobalOrder = lineOrder,
                Text = text,
                WidgetType = analyzer.GetWidgetType(text) ?? 0,
            };
            string previousLine = string.Empty;
            if (Lines.Count > 1)
            {
                previousLine = Lines[Lines.Count - 1].Text;
            }

            line.Type = analyzer.GetLineType(text, previousLine, lineOrder) ?? 0;
            Lines.Add(line);
        }

        public IList<Line> GetLines() { return Lines; }

        public void ReplaceLineText(Line line, string text)
        {
            if (string.IsNullOrWhiteSpace(text) || line == null)
            {
                throw new UnprocessableLineException();
            }

            var lineToUpdate = Lines.FirstOrDefault(c => c.GlobalOrder == line.GlobalOrder);

            if (lineToUpdate != null)
            {
                lineToUpdate.Text = text;
            }


        }


        internal void SavePlayConfig(string path)
        {
            foreach (var lineItem in Lines.OrderBy(c => c.WidgetOrder))
            {
                File.AppendAllText(path, lineItem.Text + "\r\n");
            }

            //File.WriteAllText(path, PlayConfig.ToString());
        }

        public void Clear()
        {
            Instance.PlayConfig.Clear();
            Instance.Lines.Clear();
        }
    }
}
