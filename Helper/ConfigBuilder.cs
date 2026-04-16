using System.IO;
using System.Windows.Forms;
using System;
using WindowsFormsApp1.Template;

namespace WindowsFormsApp1.Helper
{
    public class ConfigBuilder
    {
        private Config config;

        public ConfigBuilder(string fileSaveToPath)
        {
            config = new Config
            {
                ConfigText = PlayConfigTemplate.PlayConfigPart1,
                FileSaveToPath = fileSaveToPath
            };
        }

        public ConfigBuilder AddAnimationImages(string animation_Images)
        {
            config.ConfigText = config.ConfigText
                 .Replace("Animation_Images", animation_Images);

            return this;
        }
        public ConfigBuilder AddAnimationTime(string animation_time)
        {
            config.ConfigText = config.ConfigText
                 .Replace("Animation_Time", animation_time);

            return this;
        }
        public ConfigBuilder AddAnimationImageNumber(string animation_image_num)
        {
            string template = "<P>";
            int possition = 6;

            config.ConfigText = ConfigGenarationHelper.ReplaceValueAfterTemplate(config.ConfigText, template, 6, animation_image_num);

            return this;
        }

        public Config Build()
        {
            var playConfigFile = Path.Combine(config.FileSaveToPath, "PLAY_CONFIG.TXT");

            try
            {
                using (StreamWriter writer = File.AppendText(playConfigFile))
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
