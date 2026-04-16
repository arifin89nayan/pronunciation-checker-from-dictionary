using System.Diagnostics;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace WindowsFormsApp1.Helper
{
    public class DirectoryHelper
    {
        //public static string OutputFolderName = "test_content";
        public static string OutputFolderName = "";
        public static string SelectedPlayconfigFilePath { get; set; } = string.Empty;

        public static string GetPlayconfigDirectory()
        {
            if (string.IsNullOrWhiteSpace(SelectedPlayconfigFilePath))
            {
                return string.Empty;
            }
            return Path.GetDirectoryName(SelectedPlayconfigFilePath);

        }
        public static void QuizCopyFiles(string sourcePath, string destinationPath )
        {

            if (!Directory.Exists(sourcePath))
            {
                throw new DirectoryNotFoundException($"Source directory '{sourcePath}' not found.");
            }

            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }
            else
            {
                //ClearDirectory(destinationPath);
            }

            var files = Directory.GetFiles(sourcePath);
            foreach (var filePath in files)
            {
                    string fileName = Path.GetFileName(filePath);
                    string destinationFile = Path.Combine(destinationPath, fileName);
                    File.Copy(filePath, destinationFile, true);
                
            }
        }

        public static void CopyFiles(string sourcePath, string destinationPath, Func<string, bool> action)
        {

            if (!Directory.Exists(sourcePath))
            {
                throw new DirectoryNotFoundException($"Source directory '{sourcePath}' not found.");
            }

            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }
            

            var files = Directory.GetFiles(sourcePath);
            foreach (var filePath in files)
            {
                string fileName = Path.GetFileName(filePath);

                if (action(fileName))
                {
                    
                    string destinationFile = Path.Combine(destinationPath, fileName);
                    File.Copy(filePath, destinationFile, true);
                }
            }
        }

        public static bool IsImageFile(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            return extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".bmp";
        }
        public static void ClearDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var files = Directory.GetFiles(directoryPath);
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

        //public static string GetCurrentDirectory()
        //{
        //    var d = Directory.GetCurrentDirectory();

        //    return Path.Combine(d, @"..\..");
        //}
        public static string GetCurrentDirectory()
        {
            // Get the current directory
            var currentDirectory = Directory.GetCurrentDirectory();

            // Combine the current directory with "../.."
            var newPath = Path.Combine(currentDirectory, @"..\..");

            // Get the full path, which resolves the relative components
            var fullPath = Path.GetFullPath(newPath);

            return fullPath;
        }
        public static void openDirectory(string directory)
        {
            try
            {
                Process.Start("explorer.exe", directory);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to open the directory: " + ex.Message);
            }
        }

        public static string GetTempDirectory()
        {
            return GetTempDataDirectory("temp");

        }

        public static string GetOutputDirectory(string folderName)
        {
            return GetTempDataDirectory(folderName);
        }
        /// <summary>
        /// This method will return the directory of output folder.
        /// Which has been selected in the form 1.
        /// </summary>
        /// <returns></returns>
        public static string GetOutputDirectory()
        {
            return GetTempDataDirectory(OutputFolderName);
        }
        public static string GetResDirectory()
        {
            return GetResDataDirectory(OutputFolderName);
        }

        private static string GetTempDataDirectory(string folderName)
        {
            var currentDirectory = GetCurrentDirectory();
            var newPath = Path.Combine(currentDirectory, @"..\", "OutPut", folderName);

            var fullPath = Path.GetFullPath(newPath);

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            return fullPath;
        }
        private static string GetResDataDirectory(string folderName)
        {
            var currentDirectory = GetCurrentDirectory();
            var newPath = Path.Combine(currentDirectory, @"..\", "Res");

            var fullPath = Path.GetFullPath(newPath);

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            return fullPath;
        }
    }
}
