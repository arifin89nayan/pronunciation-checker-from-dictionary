using System.IO;
using System.Windows.Forms;
using System;
using WindowsFormsApp1.Template;

namespace WindowsFormsApp1.Helper.Quiz
{
    internal class CorrectAnsConfigBuilder
    {
        private Config config;
        public CorrectAnsConfigBuilder(string fileSaveToPath)
        {
            config = new Config
            {
                ConfigText = QuizPlayConfigTemplate.PlayConfig_Question,
                FileSaveToPath = fileSaveToPath
            };
        }
        public CorrectAnsConfigBuilder AddCorrectAnsAudioTime(string fileName)
        {
            config.ConfigText = config.ConfigText
                 .Replace("Correct_Ans_Audio_Time", fileName);
            return this;
        }
        public CorrectAnsConfigBuilder AddCorrectAnsAudioFileName(string fileName)
        {
            config.ConfigText = config.ConfigText
                 .Replace("Correct_AnsAudioFileName", fileName);
            return this;
        }
        public CorrectAnsConfigBuilder AddCorrectAnsOffset(string offset)
        {
            if (string.IsNullOrEmpty(offset))
            {
                throw new ArgumentNullException(nameof(offset) + " cannot be null.");
            }
            config.ConfigText = config.ConfigText
                 .Replace("Correct_Ans_Offset", offset);
            return this;
        }
        public CorrectAnsConfigBuilder AddCorrectAnsLength(string length)
        {
            if (string.IsNullOrEmpty(length))
            {
                throw new ArgumentNullException(nameof(length) + " cannot be null.");
            }
            config.ConfigText = config.ConfigText
                 .Replace("Correct_Ans_Length", length);
            return this;
        }
        public Config Build()
        {
            var playConfigFile = Path.Combine(config.FileSaveToPath, "PLAY_CONFIG.TXT");

            try
            {
                using (StreamWriter writer = File.CreateText(playConfigFile))
                {
                    writer.WriteLine(config.ConfigText);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating file: " + ex.Message);
            }
            return config;
        }
    }
}
