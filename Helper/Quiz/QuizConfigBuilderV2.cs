using System.IO;
using System.Windows.Forms;
using System;
using WindowsFormsApp1.Template;

namespace WindowsFormsApp1.Helper.Quiz
{
    public class QuizConfigBuilderV2
    {
        private Config config;
        public QuizConfigBuilderV2(string fileSaveToPath)
        {
            config = new Config
            {
                ConfigText = QuizPlayConfigTemplateV2.PlayConfig,
                FileSaveToPath = fileSaveToPath
            };
        }
        #region Question
        public QuizConfigBuilderV2 AddQuestionFileName(string name)
        {
            config.ConfigText = config.ConfigText
                 .Replace("Question_AudioFileName", name);
            return this;
        }
        public QuizConfigBuilderV2 AddQuestionOffset(int offset)
        {
            config.ConfigText = config.ConfigText
                 .Replace("Question_Offset", offset.ToString());
            return this;
        }
        public QuizConfigBuilderV2 AddQuestionLength(int length)
        {
            
            config.ConfigText = config.ConfigText
                 .Replace("Question_Length", length.ToString());
            return this;
        }
        #endregion

        #region CorrectAns
        public QuizConfigBuilderV2 AddCorrectAnsAudioTime(string fileName)
        {
            config.ConfigText = config.ConfigText
                 .Replace("Correct_Ans_Audio_Time", fileName);
            return this;
        }
        public QuizConfigBuilderV2 AddCorrectAnsAudioFileName(string fileName)
        {
            config.ConfigText = config.ConfigText
                 .Replace("Correct_AnsAudioFileName", fileName);
            return this;
        }
        public QuizConfigBuilderV2 AddCorrectAnsOffset(int offset,string correctAns)
        {
            config.ConfigText = config.ConfigText
                 .Replace("Correct_Ans_Offset", offset.ToString())
                 .Replace("Correct_Ans_Length", correctAns.Length.ToString());

            return this;
        }
    
        #endregion

        #region Options
        public QuizConfigBuilderV2 AddSelectionCounts(int numberOfOptions)
        {
            config.ConfigText = config.ConfigText
                 .Replace("selection_count", numberOfOptions.ToString());

            return this;
        }
        public QuizConfigBuilderV2 AddSelectionLength_01(int skip, string option)
        {
            config.ConfigText = config.ConfigText
                 .Replace("q1_n_skip", skip.ToString())
                 .Replace("q1_length", option.Length.ToString())
                 .Replace("q1_h_skip", (skip + option.Length).ToString());

            return this;
        }
        public QuizConfigBuilderV2 AddSelectionLength_02(int skip, string option)
        {
            config.ConfigText = config.ConfigText
                 .Replace("q2_n_skip", skip.ToString())
                 .Replace("q2_length", option.Length.ToString())
                 .Replace("q2_h_skip", (skip + option.Length).ToString());

            return this;
        }
        public QuizConfigBuilderV2 AddSelectionLength_03(int skip, string option)
        {
            config.ConfigText = config.ConfigText
                 .Replace("q3_n_skip", skip.ToString())
                 .Replace("q3_length", option.Length.ToString())
                 .Replace("q3_h_skip", (skip + option.Length).ToString());

            return this;
        }
        #endregion
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
