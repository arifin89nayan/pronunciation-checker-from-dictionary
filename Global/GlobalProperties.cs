using System;
using System.IO;

namespace WindowsFormsApp1.Global
{
    public sealed class GlobalProperties
    {
        private static readonly Lazy<GlobalProperties> _instance = new Lazy<GlobalProperties>(() => new GlobalProperties());
        public static GlobalProperties Instance => _instance.Value;

        public static string PlayConfigPath { get; set; }
        public static string GuideName { get; set; }
        public static string OutputFolderName { get; set; }
        public static string OutputPath { get; set; }
        public static string AutoProcessAudioFolderPath { get; set; }
        public static string extractedKey { get; set; }
        public static string PlayConfigFolderPath
        {
            get
            {
                var newPath = Path.Combine(PlayConfigPath, @"..\");

                var fullPath = Path.GetFullPath(newPath);
                return fullPath;
            }
        }

        #region Caption Form
        public static string CaptionBaseImagePath { get; set; }
        public static string CaptionBaseImage
        {
            get
            {
                return Path.GetFileName(CaptionBaseImagePath);
            }
        }
        public static string CaptionAudioPath { get; set; }

        #endregion
    }
}
