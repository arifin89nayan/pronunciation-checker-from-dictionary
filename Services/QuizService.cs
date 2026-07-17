 
using System;
using WindowsFormsApp1.Models;
using System.IO; 
using WindowsFormsApp1.Global;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using WindowsFormsApp1.Helper;
using WindowsFormsApp1.Enums;
using WindowsFormsApp1.Models.Widgets;
using DocumentFormat.OpenXml.Vml;

namespace WindowsFormsApp1.Services
{
    public class QuizService
    {
      

        public bool BuildTextData(Quiz quiz, Widget widget)
        {
          if(widget.Lines.Any(l => l.Text.Contains("Template<T>")) && widget.IsContainSelection)
            {
                try
                {
                    var textDatePath = System.IO.Path.Combine(GlobalProperties.OutputPath, "TEXTDATA.TXT");
                    if (textDatePath.IndexOfAny(System.IO.Path.GetInvalidPathChars()) >= 0)
                    {
                        Console.WriteLine("⚠️ Invalid characters in path!");
                    }
                    var content = GenerateTextDateContent(quiz);
                    // Write text using UTF-8 encoding
                    System.IO.File.WriteAllText(textDatePath, content, Encoding.UTF8);
                    FileStream fs1 = new FileStream(textDatePath, FileMode.Create);
                    char[] tempchararr1 = content.ToCharArray();
                    byte[] tempbytearr1 = System.Text.UTF8Encoding.Unicode.GetBytes(tempchararr1);
                    //byte[] tempbytearr1 = System.Text.UTF8Encoding.UTF8.GetBytes(tempchararr1);
                    fs1.Write(tempbytearr1, 0, tempbytearr1.Length);
                    fs1.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;

                }
                
            }
            else
            {
                try
                {
                    var textDatePath = System.IO.Path.Combine(GlobalProperties.OutputPath, "TEXTDATA.TXT");
                    if (textDatePath.IndexOfAny(System.IO.Path.GetInvalidPathChars()) >= 0)
                    {
                        Console.WriteLine("⚠️ Invalid characters in path!");
                    }
                   
                    var content = CaptionGenerateTextDateContent(quiz);
                    // Write text using UTF-8 encoding
                    System.IO.File.WriteAllText(textDatePath, content, Encoding.UTF8);
                    FileStream fs1 = new FileStream(textDatePath, FileMode.Create);
                    char[] tempchararr1 = content.ToCharArray();
                    byte[] tempbytearr1 = System.Text.UTF8Encoding.Unicode.GetBytes(tempchararr1);
                    //byte[] tempbytearr1 = System.Text.UTF8Encoding.UTF8.GetBytes(tempchararr1);
                    fs1.Write(tempbytearr1, 0, tempbytearr1.Length);
                    fs1.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    return false;

                }
            }


        }
        private string GenerateTextDateContent(Quiz quiz)
        {
            var FilesPath = System.IO.Path.Combine(GlobalProperties.PlayConfigFolderPath, "TEXTDATA.TXT");
            var extractedKeyValue = TextExtrator.ExtractKeyFromTextData(FilesPath);
            GlobalProperties.extractedKey = extractedKeyValue;
            var Key = GlobalProperties.extractedKey;
            
            string content = Key + quiz.Question;
             
            string correctAns = string.Empty;

            foreach (var option in quiz.Options.OrderBy(c => c.Order))
            {
                content += option.Text+ option.Text; 
            }

            content += $"{quiz.CorrectAnsDetails}"; ;
            return content + correctAns;

        }
        private string CaptionGenerateTextDateContent(Quiz quiz)
        {
            var FilesPath = System.IO.Path.Combine(GlobalProperties.PlayConfigFolderPath, "TEXTDATA.TXT");
            var extractedKeyValue = TextExtrator.ExtractKeyFromTextData(FilesPath);
            GlobalProperties.extractedKey = extractedKeyValue;
            var Key = GlobalProperties.extractedKey;

            string content = Key;

            string correctAns = string.Empty;

            foreach (var option in quiz.Options.OrderBy(c => c.Order))
            {
                string trimmedOption = option.Text?.Trim() ?? "";
                content += trimmedOption+ trimmedOption;
            }

            //content += $"{quiz.CorrectAnsDetails}";
            return content;

        }

    }
}
