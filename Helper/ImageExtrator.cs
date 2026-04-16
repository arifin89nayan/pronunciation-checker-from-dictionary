using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using WindowsFormsApp1.Models.Animation;

namespace WindowsFormsApp1.Helper
{
    public class ImageExtrator
    {
        public static (List<string> imageNames, int totalImagesProcessed, int timeInterval) GetImageNames(string filepath)
        {
            string text = GetConfigString(filepath);

            var imageNames = new List<string>();
            string pattern = @"Template<P><([^>]+)>(?:<[^>]+>){4}<([^>]+)><([^>]+)>";

            var match = Regex.Match(text, pattern);
            if (match.Success)
            {
                var imagesSegment = match.Groups[1].Value;
                var images = imagesSegment.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var image in images)
                {
                    var trimmedImage = image.Trim();
                    trimmedImage = trimmedImage.Split(' ')[0];
                    if (trimmedImage.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                    {
                        imageNames.Add(trimmedImage);
                    }
                }
            }

            return (imageNames, int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
        }

        public static (string scrollTimeStart, string scrollTimeEnd, string scrollTimeInterval) GetScrollProperties(string filepath)
        {
            string scrollTimeStart = string.Empty;
            string scrollTimeEnd = string.Empty;
            string scrollTimeInterval = string.Empty;

            string text = GetConfigString(filepath);

            string pattern = @"Template<C>(?:<[^>]*>){8}<(\d+)><(\d+)><(\d+)>";

            var match = Regex.Match(text, pattern);
            if (match.Success)
            {
                scrollTimeStart = match.Groups[1].Value;
                scrollTimeEnd = match.Groups[2].Value;
                scrollTimeInterval = match.Groups[3].Value;

            }

            return (scrollTimeStart, scrollTimeEnd, scrollTimeInterval);
        }
        public static (string scrollTimeStart, string scrollTimeEnd, string scrollTimeInterval) GetScrollProperties_V2(string text)
        {
            string scrollTimeStart = string.Empty;
            string scrollTimeEnd = string.Empty;
            string scrollTimeInterval = string.Empty;

            string pattern = @"Template<C>(?:<[^>]*>){8}<(\d+)><(\d+)><(\d+)>";

            var match = Regex.Match(text, pattern);
            if (match.Success)
            {
                scrollTimeStart = match.Groups[1].Value;
                scrollTimeEnd = match.Groups[2].Value;
                scrollTimeInterval = match.Groups[3].Value;

            }

            return (scrollTimeStart, scrollTimeEnd, scrollTimeInterval);
        }

        public static string GetAudioDurationProperties(string filepath)
        {
            string AudioDuration = string.Empty;
            string text = GetConfigString(filepath);
            string pattern = @"<(\d+)>\s*<M>";
            var matches = Regex.Match(text, pattern);

            if (matches.Success)
            {
                matches = matches.NextMatch();
                if (matches.Success)
                {
                    AudioDuration = matches.Groups[1].Value;
                }
            }
            return AudioDuration;
        }
        public static string Playconfig_Analyzer(string filepath)
        {
            string result = "Invalid template";
            string text = GetConfigString(filepath);
            if (text.Contains("Template<P>"))
            {
                return "Template<P>";
            }
            else if (text.Contains("Template<C>"))
            {
                return "Template<C>";
            }
            else if (text.Contains("Template<T>"))
            {
                return "Template<T>";
            }
            else if (text.Contains("Template<S>"))
            {
                return "Template<S>";
            }

            return result;
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

        public static AnimationLineExtract ExtractAnimationLine(string text)
        {

            var imageNames = new List<string>();
            string pattern = @"Template<P><([^>]+)>(?:<[^>]+>){4}<([^>]+)><([^>]+)>";
            string pattern2 = @"<P><([^>]+)>(?:<[^>]+>){4}<([^>]+)><([^>]+)>";

            var match = Regex.Match(text, pattern);
            if (!match.Success)
            {
                match = Regex.Match(text, pattern2);
            }
            if (match.Success)
            {
                var imagesSegment = match.Groups[1].Value;
                var images = imagesSegment.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var image in images)
                {
                    var trimmedImage = image.Trim();
                    trimmedImage = trimmedImage.Split(' ')[0];
                    if (trimmedImage.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                    {
                        imageNames.Add(trimmedImage);
                    }
                    else
                    {
                        throw new InvalidOperationException("Animation image must be .jpg.");
                    }
                }
            }

            int.TryParse(match.Groups[3].Value, out int timeInterval);

            return new AnimationLineExtract
            {
                Images = imageNames,
                TimeInterval = timeInterval
            };

        }

    }
}
