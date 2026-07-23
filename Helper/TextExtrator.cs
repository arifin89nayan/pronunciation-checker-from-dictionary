using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Models.Widgets;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;

namespace WindowsFormsApp1.Helper
{
    public class TextExtrator
    {
        public static (int rightAns, int wrongAns)? ExtractRightOrWorngAns(string file)
        {
            string text = GetConfigString(file); // Assumes GetConfigString is defined elsewhere

            string pattern = @"Template<S>\((\d+),(\d+)\)";
            Match match = Regex.Match(text, pattern);
            if (match.Success)
            {
                int rightAns = int.Parse(match.Groups[1].Value);
                int wrongAns = int.Parse(match.Groups[2].Value);
                return (rightAns, wrongAns);
            }

            return null; // No match found
        }
        public static (int rightAns, int wrongAns) ExtractRightOrWorngAnsFromLine(string lineText)
        {
            string text = lineText;

            string pattern = @"Template<S>\((\d+),(\d+)\)";
            Match match = Regex.Match(text, pattern);
            if (match.Success)
            {
                int rightAns = int.Parse(match.Groups[1].Value);
                int wrongAns = int.Parse(match.Groups[2].Value);
                return (rightAns, wrongAns);
            }

            return (0, 0);
        }
        public static string ExtractKeyFromTextData(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("TEXTDATA.TXT not found.");

            byte[] fileBytes = File.ReadAllBytes(filePath);
            string content = Encoding.Unicode.GetString(fileBytes).Replace("\0", "");

            // Key (based on example "EDOTKBFFB261C866D00000085")
            // If fixed pattern is known, like starting with "EDOTK", extract it
            Match match = Regex.Match(content, @"EDOTK[A-Z0-9]{20}");
            if (match.Success)
            {
                return match.Value;
            }

            throw new InvalidOperationException("Key not found in TEXTDATA.TXT");
        }
        public static string ExtractIDFromTextData(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("TEXTDATA.TXT not found.", filePath);

            // Use StreamReader with BOM detection so we read text with the correct encoding.
            string content;
            using (var sr = new StreamReader(filePath, detectEncodingFromByteOrderMarks: true))
            {
                content = sr.ReadToEnd();
            }

            // Normalize and remove NUL padding
            content = content.Replace("\0", "").Trim();

            // Match key (case-insensitive). Adjust pattern length if needed.
            var match = Regex.Match(content, @"EDOTK[A-Z0-9]{20}", RegexOptions.IgnoreCase);
            if (!match.Success)
                throw new InvalidOperationException("Key not found in TEXTDATA.TXT");

            var key = match.Value.ToUpperInvariant();
            var id = key.Substring(key.Length - 8); // last 8 characters
            return id;
        }
        //public static string ExtractIDFromTextData(string filePath)
        //{
        //    if (!File.Exists(filePath))
        //        throw new FileNotFoundException("TEXTDATA.TXT not found.");

        //    byte[] fileBytes = File.ReadAllBytes(filePath);
        //    string content = Encoding.Unicode.GetString(fileBytes).Replace("\0", "");

        //    // Key (based on example "EDOTKBFFB261C866D00000085")
        //    // If fixed pattern is known, like starting with "EDOTK", extract it
        //    Match match = Regex.Match(content, @"EDOTK[A-Z0-9]{20}");
        //    if (match.Success)
        //    {
        //        string key = match.Value;
        //        string id = key.Substring(key.Length - 8); // Get last 8 characters
        //        return id;
        //    }

        //    throw new InvalidOperationException("Key not found in TEXTDATA.TXT");
        //}

        public static List<(string Quiz, int Offset, int Length)> GetOffset_Lengths(string filepath)
        {

            List<(string Quiz, int Offset, int Length)> results = new List<(string Quiz, int Offset, int Length)>();


            string text = GetConfigString(filepath);

            string[] patterns = {
                                    @"Template<T><(\d+):(\d+)>",
                                    @"<N><T><(\d+):(\d+)>",
                                    @"<N><T><(\d+):(\d+)>",
                                    @"<N><T><(\d+):(\d+)>"
                                };

            foreach (var pattern in patterns)
            {
                var match = Regex.Match(text, pattern);
                if (match.Success)
                {
                    string quizName = Regex.Match(pattern, @"Template\d").Value;
                    string Offsetval = match.Groups[1].Value;
                    string Lengthval = match.Groups[2].Value;
                    int Offset = (Int32.Parse(Offsetval)) / 2;
                    int Length = (Int32.Parse(Lengthval)) / 2;
                    results.Add((quizName, Offset, Length));
                }
            }

            return results;
        }

        public static List<(string Quiz, int Offset, int Length)> GetSelectionData(string text)
        {

            List<(string Quiz, int Offset, int Length)> results = new List<(string Quiz, int Offset, int Length)>();


            string[] patterns = {
                                    @"Template<T><(\d+):(\d+)>",
                                    @"<N><T><(\d+):(\d+)>",
                                    @"<N><T><(\d+):(\d+)>",
                                    @"<N><T><(\d+):(\d+)>"
                                };

            foreach (var pattern in patterns)
            {
                var match = Regex.Match(text, pattern);
                if (match.Success)
                {
                    string quizName = Regex.Match(pattern, @"Template\d").Value;
                    string Offsetval = match.Groups[1].Value;
                    string Lengthval = match.Groups[2].Value;
                    int Offset = (int.Parse(Offsetval)) / 2;
                    int Length = (int.Parse(Lengthval)) / 2;
                    results.Add((quizName, Offset, Length));
                }
            }

            return results;
        }

        public static List<ParseSelectionMetadata> GetSelectionMetadata(Widget widget, string directoryFilePath)
        {
            string textDataPath = Path.Combine(Path.GetDirectoryName(directoryFilePath), "TEXTDATA.TXT");

            byte[] fileBytes = File.ReadAllBytes(textDataPath);
            //string fileContent = Encoding.Unicode.GetString(fileBytes);
            string fileContent = Encoding.Unicode.GetString(fileBytes).TrimStart('\uFEFF');


            string textDataContent = fileContent.Replace("\0", "");

            var results = new List<ParseSelectionMetadata>(); 

            foreach (var line in widget.Lines)
            {
                if (line.Text.StartsWith("<N><T>"))
                {
                    var items = PatternHelper.GetItems(line.Text, @"<([^>]+)>");

                    var offsetLength = items[2].TrimStart('<').TrimEnd('>').Split(':');

                    var result = new ParseSelectionMetadata
                    {
                        Order = results.Count + 1,
                        Offset = int.Parse(offsetLength[0]) / 2,
                        Length = int.Parse(offsetLength[1]) / 2
                    };

                    int startIndex = result.Offset;
                    int length = result.Length;

                    if (startIndex + length <= textDataContent.Length)
                    {
                        string option = textDataContent.Substring(startIndex, length).Trim();
                        result.Text = option;
                    }
                    else
                    {
                        result.Text = string.Empty;
                    }

                    //string option = textDataContent.Substring(startIndex, length);
                    //result.Text = option;
                    results.Add(result);
                }
            }

            return results;
        }

        public static string GetConfigString(string filePath)
        {
            StringBuilder text = new StringBuilder();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        text.AppendLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred:");
                Console.WriteLine(ex.Message);
            }

            return text.ToString();
        }

        public static string GetCorrectAnsError(string filePath)
        {
            string inputText = GetConfigString(filePath);

            string marker = "<00000C4AE.MP3>";
            int markerIndex = inputText.IndexOf(marker);

            if (markerIndex != -1)
            {
                string substringAfterMarker = inputText.Substring(markerIndex + marker.Length);
                string pattern = @"Template<T><(\d+:\d+)>";
                Match match = Regex.Match(substringAfterMarker, pattern);

                if (match.Success)
                {
                    string extractedValue = match.Groups[1].Value;

                    if (extractedValue == null)
                    {
                        throw new Exception("Invalid quiz template");
                    }

                    string textDataFilePath = Path.Combine(DirectoryHelper.GetPlayconfigDirectory(), "TEXTDATA.TXT");
                    var textDataContent = File.ReadAllText(textDataFilePath);

                    var offsetAndLength = extractedValue.Split(':').Select(c => Convert.ToInt32(c)).ToList();

                    string correctAns = textDataContent.Substring(offsetAndLength[0] / 2, offsetAndLength[1] / 2);

                    return correctAns;
                }
                else
                {
                    Console.WriteLine("No match found after the marker.");
                }
            }
            else
            {
                Console.WriteLine("Marker <00000C4AE.MP3> not found.");
            }

            return string.Empty;
        }
        public static string GetCorrectAns(string filePath)
        {
            string inputText = GetConfigString(filePath);

            var matches = Regex.Matches(inputText, @"<W>\s*Template<T><(\d+):(\d+)>.*?<W>", RegexOptions.Singleline);

            if (matches.Count < 2)
            {
                MessageBox.Show("Second Template<T> block between <W> tags not found.");
                return string.Empty;
            }

            var match = matches[1]; // 0-based index → second match

            int offset = int.Parse(match.Groups[1].Value);
            int length = int.Parse(match.Groups[2].Value);

            // Load and decode TEXTDATA.TXT
            string textDataFilePath = Path.Combine(DirectoryHelper.GetPlayconfigDirectory(), "TEXTDATA.TXT");
            byte[] fileBytes = File.ReadAllBytes(textDataFilePath);
            //string textContent = Encoding.Unicode.GetString(fileBytes).Replace("\0", "");
            string textContent = Encoding.Unicode.GetString(fileBytes).TrimStart('\uFEFF').Replace("\0", "");


            int charOffset = offset / 2;
            int charLength = length / 2;

            if (charOffset + charLength > textContent.Length)
            {
                MessageBox.Show("Offset and length exceed text content.");
                return string.Empty;
            }
            string extracted = textContent.Substring(charOffset, charLength).TrimEnd('\r', '\n', '\t', ' ');

            return extracted;


            //return textContent.Substring(charOffset, charLength);
        }

    }
}
