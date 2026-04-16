using System.IO;
using System.Windows.Forms;
using System;
using WindowsFormsApp1.Template;

namespace WindowsFormsApp1.Helper
{
    public class QuizConfigBuilder
    {
        private Config config;
        private int nextLength = 0;
        public QuizConfigBuilder(string fileSaveToPath)
        {
            config = new Config
            {
                ConfigText = QuizPlayConfigTemplate.PlayConfig,
                FileSaveToPath = fileSaveToPath
            };
        }

        public QuizConfigBuilder AddWizedLength(string key, string question)
        {
            config.ConfigText = config.ConfigText
                 .Replace("wized_skip", key.Length.ToString());

            config.ConfigText = config.ConfigText
                .Replace("wized_length", question.Length.ToString());
             
            return this;
        }
        public QuizConfigBuilder AddSelectionCounts(int numberOfOptions)
        {
            config.ConfigText = config.ConfigText
                 .Replace("selection_count", numberOfOptions.ToString());

            return this;
        }
        public QuizConfigBuilder AddSelectionLength_01(int skip, string option)
        {
            config.ConfigText = config.ConfigText
                 .Replace("q1_n_skip", skip.ToString())
                 .Replace("q1_length", option.Length.ToString())
                 .Replace("q1_h_skip", (skip + option.Length).ToString());

            return this;
        }
        public QuizConfigBuilder AddSelectionLength_02(int skip, string option)
        {
            config.ConfigText = config.ConfigText
                 .Replace("q2_n_skip", skip.ToString())
                 .Replace("q2_length", option.Length.ToString())
                 .Replace("q2_h_skip", (skip + option.Length).ToString());

            return this;
        }
        public QuizConfigBuilder AddSelectionLength_03(int skip, string option)
        {
            config.ConfigText = config.ConfigText
                 .Replace("q3_n_skip", skip.ToString())
                 .Replace("q3_length", option.Length.ToString())
                 .Replace("q3_h_skip", (skip + option.Length).ToString());

            return this;
        }

        public QuizConfigBuilder AddCorrectAnswerLength_03(int skip, string correctAnswer)
        {
            config.ConfigText = config.ConfigText
                 .Replace("correct_answer_skip", skip.ToString())
                 .Replace("correct_answer_length", correctAnswer.Length.ToString());

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
