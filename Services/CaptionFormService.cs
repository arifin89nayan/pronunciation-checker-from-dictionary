using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApp1.Enums;
using WindowsFormsApp1.Global;
using WindowsFormsApp1.Helper;
using WindowsFormsApp1.Models.Widgets;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;

namespace WindowsFormsApp1.Services
{
    internal class CaptionFormService
    {
        public int ConvertToSecond(string input)
        {
            int x = int.Parse(input);
            return x / 10;
        }
        public int ConvertToSecond(double input)
        {
            return (int)input / 10;
        }
        public int CalculateT2(string ta, Line line, string audioName)
        {
            var line5 = line.Text;

            if (line5.Contains("Duration"))
            {
                line5 = line5.Substring(8);
            }
            //foreach (var keyword in SkippableKeyword())
            //{
            //    if (line5.StartsWith(keyword))
            //    {
            //        line5 = line5.Substring(8);
            //    }

            //}
            var match = Regex.Match(line5, @"<(\d+)>");

            if (match.Success)
            {
                var duration = int.Parse(match.Groups[1].Value); // Extract the numeric value
                var parsedTa = int.Parse(ta);
                var Ta = duration - parsedTa;

                var generatedAudioDuration = AudioInfo.GetAudioDurationInTimeSpan(Path.Combine(GlobalProperties.OutputPath, audioName));

                var t2 = generatedAudioDuration.TotalMilliseconds - Ta;

                return ConvertToSecond(t2);

            }
            else
            {
                MessageBox.Show("Failed to extract duration.");
                return -1;
            }
            //var duration = int.Parse(line5.TrimStart('<').TrimEnd('>'));

        }
        //public static IList<string> SkippableKeyword()
        //{
        //    return new List<string>()
        //    {
        //        "Duration1", "Audio1","Duration2", "Audio2","Duration3", "Audio3","Duration4", "Audio4","Duration5", "Audio5"
        //    };
        //}

        public string GetCalculatedLine(Line line, int t1, int t2, int t3, int numChunks, string captionBaseImagePath, int baseImageHeigt, bool isChangeT2 = false)
        {
            var items = PatternHelper.GetItems(line.Text, @"<([^>]+)>");

            items[9] = $"<{t1}>";
            items[1] = $"<{captionBaseImagePath}>";
            if (isChangeT2)
            {
                items[10] = $"<{t2}>";
            }
            items[11] = $"<{t3}>";
            items[8] = $"<{baseImageHeigt}>";
            items[6] = $"<{numChunks}>";

            return string.Join("", items);
        }

        public string GetAudioLine(Line line, string audioFileName)
        {
            return string.Empty;
        }

        public string GetAudioDurationLine()
        {
            return string.Empty;
        }

        public void GenerateMediaLines(Line mediaDuration, Line media, string audioName)
        {
            int? Tempduration = ExtractDurationValue(mediaDuration.Text);
            string audioFileName = ExtractAudioFileName(media.Text);
            string playConfigPath = DirectoryHelper.SelectedPlayconfigFilePath;
            string folderPath = Path.GetDirectoryName(playConfigPath);
            string audioFullPath = Path.Combine(folderPath, audioFileName);
           if( !File.Exists(audioFullPath))
            {
                var msg = $"Template audio file missing: {audioFullPath} (file: {audioFileName})";
                ErrorLogger.LogError(msg);
                throw new FileNotFoundException(msg, audioFullPath);
            }
            var TempAudioDuration = GetAudioDurationMs(audioFullPath);
            //var timedifference= (Tempduration-TempAudioDuration);
            var timedifference = Math.Abs((int)(Tempduration - TempAudioDuration));


            if (mediaDuration.IsSkippedLine)
            {
                var generatedAudioDuration = AudioInfo.GetAudioDurationInTimeSpan(Path.Combine(GlobalProperties.OutputPath, audioName));
                var duration =( (generatedAudioDuration.TotalMilliseconds)+ timedifference);
                

                GlobalConfigPropreties.Instance.ReplaceLineText(mediaDuration, $"<{duration}>");

            }

            if (media.IsSkippedLine)
            {
                GlobalConfigPropreties.Instance.ReplaceLineText(media, $"<M><1><{audioName}>");
            }
        }
        public static int? ExtractDurationValue(string text)
        {
            var match = Regex.Match(text, @"<(\d+)>");
            if (match.Success)
                return int.Parse(match.Groups[1].Value);
            return null;
        }
        public static string ExtractAudioFileName(string text)
        {
            var match = Regex.Match(text, @"<([^>]+\.MP3)>", RegexOptions.IgnoreCase);
            return match.Success ? match.Groups[1].Value : null;
        }
        public static double GetAudioDurationMs(string audioPath)
        {
            using (var reader = new AudioFileReader(audioPath))
            {
                // Duration in seconds, convert to ms
                return reader.TotalTime.TotalMilliseconds;
            }
        }
    }
}
