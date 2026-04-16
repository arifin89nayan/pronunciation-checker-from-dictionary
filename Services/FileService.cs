using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Enums;
using WindowsFormsApp1.Global;
using WindowsFormsApp1.Helper;
using WindowsFormsApp1.Models.Widgets;

namespace WindowsFormsApp1.Services
{
    public class FileService
    {
        public void SaveLineFiles(Line line)
        {


            if (line == null || line.IsSkippedLine)
            {
                return;
            }
            
            IEnumerable<string> files; // Declare files here
            files = line.GetFileNames().Union(new[] { "CONTENTINFO.CSV", "TEXTDATA.TXT" });
            if (!files.Any())
            {
                return;
            }

            DirectoryHelper.CopyFiles(GlobalProperties.PlayConfigFolderPath, GlobalProperties.OutputPath,
                (fileName) =>
                { 
                    if (files.Any(c => c.Equals(fileName,StringComparison.OrdinalIgnoreCase)))
                    {
                        return true;
                    }
                    return false;
                });

        }
        public void QuizSaveLineFiles(Line line)
        {
            if (line == null || line.IsSkippedLine)
                return;
            var excludedFiles = new[] { "CONTENTINFO.CSV", "TEXTDATA.TXT" };

            var files = line.GetFileNames()
                            .Where(f => !excludedFiles.Contains(f, StringComparer.OrdinalIgnoreCase));

            if (!files.Any())
                return;

            DirectoryHelper.CopyFiles(GlobalProperties.PlayConfigFolderPath, GlobalProperties.OutputPath,
                (fileName) => files.Any(c => c.Equals(fileName, StringComparison.OrdinalIgnoreCase)));
        }

        private void TextDataSave()
        {
            
            string TextData = GlobalProperties.GuideName;
            if (!string.IsNullOrEmpty(TextData))
            {
                string outputPath = GlobalProperties.OutputPath;
                string fullFilePath = Path.Combine(outputPath, "CONTENTNAME.TXT");

                // Write text using UTF-8 encoding UTF8Encoding.Unicode.
                byte[] tempbytearr1 = UTF8Encoding.Unicode.GetBytes(TextData);
                File.WriteAllBytes(fullFilePath, tempbytearr1);
                //File.WriteAllText(fullFilePath, TextData, Encoding.UTF8);

            } 
        }
    }
}
