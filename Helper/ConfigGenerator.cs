using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1.Template;

namespace WindowsFormsApp1.Helper
{
    public class ConfigGenerator
    {
        private readonly string _tempDirectory;
        private readonly string _outputDirectory;

        public ConfigGenerator(string tempDirectory, string outputDirectory)
        {
            _tempDirectory = tempDirectory;
            _outputDirectory = outputDirectory;
        }

        public void GenerateConfig()
        { 

            var files = Directory.GetFiles(_outputDirectory)
                .Where(c => DirectoryHelper.IsImageFile(c))
                .Select(c => Path.GetFileName(c));

            

            var commaSeperatedImages = string.Join(",", files);
            var firstPartConfig = PlayConfigTemplate.BuildFirstPartFromGenericTemplate("", commaSeperatedImages);
         

            var playConfigFile = Path.Combine(_outputDirectory, "PLAY_CONFIG.TXT");

            try
            {
                using (StreamWriter writer = File.CreateText(playConfigFile))
                {

                    writer.WriteLine(firstPartConfig);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating file: " + ex.Message);
            }
        }    
    }
}
