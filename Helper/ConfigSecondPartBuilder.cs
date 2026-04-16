using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Enums;
using WindowsFormsApp1.Template;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1.Helper
{
    public class ConfigSecondPartBuilder
    {
        private Config config;
        string FilePath = Properties.Settings.Default.PlayConfigFilePath;

        public ConfigSecondPartBuilder(string fileSaveToPath)
        {
            config = new Config
            {
                ConfigText = PlayConfigTemplate.PlayConfigPart2,
                FileSaveToPath = fileSaveToPath
            };
        }

        public ConfigSecondPartBuilder AddSeenImages()
        {
            string animationImages = string.Join(";", WigetConfig.GetImages());

            config.ConfigText = config.ConfigText
                 .Replace("Seen_Images", animationImages);

            return this;
        }
        public ConfigSecondPartBuilder AddSeenImageNumber()
        {
            string imageCount = WigetConfig.GetImages().Count.ToString();

            config.ConfigText = config.ConfigText
                 .Replace("Seen_Image_Count", imageCount);

            return this;
        }
        public ConfigSecondPartBuilder AddAudioFileName()
        {
            string audioName = AudioInfo.AudioFileName;
            config.ConfigText = config.ConfigText
                 .Replace("Seen_Audio", audioName);

            return this;
        }
        public ConfigSecondPartBuilder AddAudioDuration()
        {

            // string duration = AudioInfo.GetAudioDuration();
            //string duration=(AudioInfo.GetAudioDurationInTimeSpan().TotalSeconds).ToString();
            string duration = (AudioInfo.GetAudioDuration()).ToString();
            TimeSpan timeSpan = (TimeSpan.Parse(duration));
            string duration1 = (timeSpan.TotalMilliseconds).ToString();

            config.ConfigText = config.ConfigText
                 .Replace("Audio_Duration", duration1);

            return this;
        }
        public ConfigSecondPartBuilder AddBaseImage()
        {

            string baseImageName = WigetConfig.GetBaseImageName();
            config.ConfigText = config.ConfigText
                 .Replace("Base_Image", baseImageName);

            return this;
        }
        public ConfigSecondPartBuilder AddBaseSliceImageCount(string baseImageName)
        {
            config.ConfigText = config.ConfigText
                 .Replace("Image_Block_Number", baseImageName);

            return this;
        }
        public ConfigSecondPartBuilder AddT1(string t1)
        {

            int x = int.Parse(t1);
            int value = x / 10;
            string T1value = (value).ToString();
            config.ConfigText = config.ConfigText
                 .Replace("T1", T1value);

            return this;
        }
        public ConfigSecondPartBuilder AddT2(string ta)
        {

            if (!int.TryParse(ta, out int parsedTa))
            {
                throw new Exception("Failed to parse Ta.Please provide numaric value.");
            }
            //string duration = ((AudioInfo.GetAudioDurationInTimeSpan().TotalSeconds) * 1000).ToString();
            string _filepath = FilePath;
            var data = ImageExtrator.GetAudioDurationProperties(_filepath);

            int ConfigFileAudioDuration = Convert.ToInt32(data);
            int TaValue = ConfigFileAudioDuration - parsedTa;

            var t2 = ((AudioInfo.GetAudioDurationInTimeSpan().TotalMilliseconds) - TaValue);
            //int x = Int32.Parse(t2);
            int x = Convert.ToInt32(t2);
            int value = x / 10;
            string T2value = (value).ToString();
            config.ConfigText = config.ConfigText
                 .Replace("T2", T2value);

            return this;
        }
        public ConfigSecondPartBuilder AddT3(string t3)
        {
            int x = Int32.Parse(t3);
            int value = x / 10;
            string T3value = (value).ToString();
            config.ConfigText = config.ConfigText
                 .Replace("T3", T3value);

            return this;
        }

        public ConfigSecondPartBuilder AddBaseImageHeight(string height)
        {
            config.ConfigText = config.ConfigText
                 .Replace("Final_Height", height);

            return this;
        }
        public Config SaveConfig()
        {


            try
            {
                File.AppendAllText(config.FileSaveToPath, config.ConfigText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating file: " + ex.Message);
            }
            return config;
        }
    }
}
