using System;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp1.Template;

namespace WindowsFormsApp1.Helper.Quiz
{
    internal class QuestionConfgBuilder
    {
        private Config config;
        public QuestionConfgBuilder(string fileSaveToPath)
        { 
            config = new Config
            {
                ConfigText = QuizPlayConfigTemplate.PlayConfig_Question,
                FileSaveToPath = fileSaveToPath
            };
        }
        public QuestionConfgBuilder AddQuestionFileName(string name)
        {
            config.ConfigText = config.ConfigText
                 .Replace("Question_AudioFileName", name); 
            return this;
        }
        public QuestionConfgBuilder AddQuestionOffset(string offset)
        {
            config.ConfigText = config.ConfigText
                 .Replace("Question_Offset", offset);
            return this;
        }
        public QuestionConfgBuilder AddQuestionLength(string length)
        {
            if (string.IsNullOrEmpty(length))
            {
                throw new ArgumentNullException(nameof(length) + " cannot be null.");
            }
            config.ConfigText = config.ConfigText
                 .Replace("Question_Length", length);
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
