using NAudio.Wave;
using System;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp1.Helper;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1.Enums
{

    //public class AudioInfo
    //{
    //    public const string AudioFileName = "output_*.mp3";
    //    public const string AudioGenerateMessage = "Successfully audio generated";
    //    public const string AudioAnimationFileName = "000001F14.WMA";

    //    #region Quiz output audio
    //    public const string QuizQuestionAudioFileName = "0000014AE.MP3";
    //    public const string QuizCorrectAnswerAudioFileName = "00000C4AE.MP3";
    //    #endregion

    //    public static string GetAudioAnimationFilePath() =>
    //         Path.Combine(DirectoryHelper.GetCurrentDirectory(), "Resources", "Audio");

    //    public static string GetAudioFilePath() =>
    //        Path.Combine(DirectoryHelper.GetOutputDirectory(), "output_*.mp3");

    //    public static string GetAudioDuration()
    //    {
    //        using (var reader = new AudioFileReader(GetAudioFilePath()))
    //        {
    //            TimeSpan duration = reader.TotalTime;
    //            string formattedDuration = string.Format("{0:D2}:{1:D2}:{2:D2}",
    //                          duration.Hours,
    //                          duration.Minutes,
    //                          duration.Seconds);
    //            return formattedDuration;
    //        }
    //    }
    //    public static TimeSpan GetAudioDurationInTimeSpan()
    //    {
    //        var audioPath = GetAudioFilePath();
    //        using (var reader = new AudioFileReader(audioPath))
    //        {
    //            TimeSpan duration = reader.TotalTime;
    //            double roundedTotalSeconds = Math.Round(duration.TotalSeconds);
    //            TimeSpan roundedDuration = TimeSpan.FromSeconds(roundedTotalSeconds);
    //            return roundedDuration;
    //            //return reader.TotalTime;


    //        }
    //    }


    //}

    public class AudioInfo
    {
        public static string GetRandomFileName(string prefix = "",string extention = ".MP3")
        {
            return prefix + Guid.NewGuid().ToString().Substring(0, 7) + extention;
        }
        public static string AudioFileName => GetLatestAudioFileName();

        public const string AudioGenerateMessage = "Successfully audio generated";
        public const string AudioAnimationFileName = "000001F14.WMA";
        #region Quiz output audio
        public const string QuizQuestionAudioFileName = "0000014AE.MP3";
        public const string QuizCorrectAnswerAudioFileName = "00000C4AE.MP3";
        #endregion
        public static string GetAudioAnimationFilePath() =>
             Path.Combine(DirectoryHelper.GetCurrentDirectory(), "Resources", "Audio");
       
        
        public static string GetLatestAudioFileName()
        {
            var outputDirectory = DirectoryHelper.GetOutputDirectory();
            var audioFiles = Directory.GetFiles(outputDirectory, "output_*.mp3");

            if (audioFiles.Length == 0)
                return null; // No audio files found

            // Assuming you want the latest file based on file creation time
            var latestFile = new FileInfo(audioFiles[0]);
            foreach (var file in audioFiles)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.CreationTime > latestFile.CreationTime)
                {
                    latestFile = fileInfo;
                }
            }
            return latestFile.Name;
        }

        public static string GetAudioFilePath()
        {
            string latestFileName = GetLatestAudioFileName();
            if (latestFileName == null)
                return null; // Handle case if no file found

            return Path.Combine(DirectoryHelper.GetOutputDirectory(), latestFileName);
        }

        public static string GetAudioDuration()
        {
            string audioPath = GetAudioFilePath();
            if (audioPath == null)
                return "No audio file found";

            using (var reader = new AudioFileReader(audioPath))
            {
                TimeSpan duration = reader.TotalTime;
                return string.Format("{0:D2}:{1:D2}:{2:D2}",
                    duration.Hours,
                    duration.Minutes,
                    duration.Seconds);
            }
        }
        public static string GetAudioDuration(string audioPath)
        { 
            if (audioPath == null) return "No audio file found";

            using (var reader = new AudioFileReader(audioPath))
            {
                TimeSpan duration = reader.TotalTime;
                return string.Format("{0:D2}:{1:D2}:{2:D2}",
                    duration.Hours,
                    duration.Minutes,
                    duration.Seconds);
            }
        }
        public static TimeSpan GetAudioDurationInTimeSpan()
        {
            string audioPath = GetAudioFilePath();
            if (audioPath == null)
                return TimeSpan.Zero; // Handle case if no file found

            using (var reader = new AudioFileReader(audioPath))
            {
                return TimeSpan.FromSeconds(reader.TotalTime.TotalSeconds);
            }
        }
        public static TimeSpan GetAudioDurationInTimeSpan(string audioPath)
        { 
            if (audioPath == null) return TimeSpan.Zero;
            if (!File.Exists(audioPath))
            {
                Console.WriteLine($"File not found: {audioPath}");
                return TimeSpan.Zero;
            }
            try
            {
                
                using (var reader = new AudioFileReader(audioPath))
                {
                    return TimeSpan.FromSeconds(reader.TotalTime.TotalSeconds);
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"Error reading audio file: {ex.Message}");
                return TimeSpan.Zero;
            }

            //using (var reader = new AudioFileReader(audioPath))
            //{
            //    return TimeSpan.FromSeconds(Math.Round(reader.TotalTime.TotalSeconds));
            //}
        }
    }

}
