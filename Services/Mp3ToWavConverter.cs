using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Services
{
    public static class Mp3ToWavConverter
    {
        public static string ConvertTo16kMonoWav(string mp3Path)
        {
            if (!File.Exists(mp3Path))
                throw new FileNotFoundException("MP3 file not found.", mp3Path);

            string tempWavPath = Path.Combine(
                Path.GetTempPath(),
                "audio_qc_" + Guid.NewGuid().ToString("N") + ".wav"
            );

            using (var reader = new AudioFileReader(mp3Path))
            {
                var outputFormat = new WaveFormat(16000, 16, 1);

                using (var resampler = new MediaFoundationResampler(reader, outputFormat))
                {
                    resampler.ResamplerQuality = 60;
                    WaveFileWriter.CreateWaveFile(tempWavPath, resampler);
                }
            }

            return tempWavPath;
        }
    }
}
