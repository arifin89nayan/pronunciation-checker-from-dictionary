using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsFormsApp1.StartupForm;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace WindowsFormsApp1.Helper
{
    public static class FormNavigetor
    {
        
        public static void Navigate(string filepath, int displayWindow)
        {
            string text = ImageExtrator.GetConfigString(filepath);
         
            //List<string> fileLines = new List<string>(File.ReadAllLines(filepath));
            List<(string Widget, int LineNumber)> WidgetList = ExtractTemplateLettersWithLineNumbers(text);
            //Console.WriteLine(sequenceLetter);
            if(displayWindow >= WidgetList.Count)
            {
                return;
            }
           
            if (WidgetList[displayWindow].Widget == "P")
            {
                string Title = Properties.Settings.Default.TextTitle;
                string ContentTitle = Title;
                AnimationForm nextForm = new AnimationForm(filepath, ContentTitle);
                nextForm.FormClosed += (s, args) => { nextForm.Hide(); Navigate(filepath, ++displayWindow); };
                nextForm.ShowDialog();
            }
            else if (WidgetList[displayWindow].Widget == "C")
            {
                CaptionForm nextForm = new CaptionForm();
                nextForm.FormClosed += (s, args) => { nextForm.Hide(); Navigate(filepath, ++displayWindow); };
                nextForm.ShowDialog();
            }
            else if (WidgetList[displayWindow].Widget == "T")
            {
                QuizForm nextForm = new QuizForm("question");
                nextForm.FormClosed += (s, args) => { nextForm.Hide(); Navigate(filepath, ++displayWindow); };
                nextForm.ShowDialog();
            }
            else if (WidgetList[displayWindow].Widget == "S")
            {
                SelectionForm nextForm = new SelectionForm();
                nextForm.FormClosed += (s, args) => { nextForm.Hide(); Navigate(filepath, ++displayWindow); };
                nextForm.ShowDialog();
            }
            else if (text.Contains("Invalid"))
            {
                MessageBox.Show(text);
            }
        }
        public static List<(string Widget, int LineNumber)> ExtractTemplateLettersWithLineNumbers(string input)
        {
            const string pattern = @"Template<([A-Z])>";
            var result = new List<(string Letter, int LineNumber)>();
            var lines = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                var matches = Regex.Matches(lines[i], pattern, RegexOptions.IgnoreCase);
                foreach (Match match in matches)
                {
                    if (match.Groups.Count > 1)
                    {
                        result.Add((match.Groups[1].Value, i + 1));
                    }
                }
            }
            return result;
        }

        //public static List<string> ExtractTemplateLetters(string input)
        //{
        //    // This pattern matches 'Template<L>' where L is any single character enclosed in angle brackets
        //    const string pattern = @"Template<([A-Z])>";

        //    var matches = Regex.Matches(input, pattern, RegexOptions.IgnoreCase);
        //    List<string> letters = new List<string>();

        //    foreach (Match match in matches)
        //    {
        //        if (match.Groups.Count > 1) // Ensure there's a captured group
        //        {
        //            letters.Add(match.Groups[1].Value); // The first group is the letter
        //        }
        //    }

        //    return letters;
        //}

    }
    public enum AppWindows
    {
        First,
        Second,
        Third,
        Fourth
    }
}
