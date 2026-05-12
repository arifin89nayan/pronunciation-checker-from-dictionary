using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Services
{
    public class AzureSpeechToTextService
    {
        private readonly string _speechKey;
        private readonly string _region;

        public AzureSpeechToTextService(string speechKey, string region)
        {
            _speechKey = speechKey;
            _region = region;
        }

        public async Task<string> TranscribeLongAudioAsync(
            string wavPath,
            string language = "ja-JP")
        {
            if (!File.Exists(wavPath))
                throw new FileNotFoundException("WAV file not found.", wavPath);

            var speechConfig = SpeechConfig.FromSubscription(_speechKey, _region);
            speechConfig.SpeechRecognitionLanguage = language;

            var recognizedText = new StringBuilder();
            var stopRecognition = new TaskCompletionSource<int>();

            using (var audioConfig = AudioConfig.FromWavFileInput(wavPath))
            using (var recognizer = new SpeechRecognizer(speechConfig, audioConfig))
            {
                recognizer.Recognized += (s, e) =>
                {
                    if (e.Result.Reason == ResultReason.RecognizedSpeech)
                    {
                        if (!string.IsNullOrWhiteSpace(e.Result.Text))
                        {
                            recognizedText.Append(e.Result.Text);
                        }
                    }
                };

                recognizer.Canceled += (s, e) =>
                {
                    if (e.Reason == CancellationReason.Error)
                    {
                        stopRecognition.TrySetException(
                            new Exception(
                                $"Azure STT canceled. ErrorCode={e.ErrorCode}, ErrorDetails={e.ErrorDetails}"
                            )
                        );
                    }
                    else
                    {
                        stopRecognition.TrySetResult(0);
                    }
                };

                recognizer.SessionStopped += (s, e) =>
                {
                    stopRecognition.TrySetResult(0);
                };

                await recognizer.StartContinuousRecognitionAsync();

                try
                {
                    await stopRecognition.Task;
                }
                finally
                {
                    await recognizer.StopContinuousRecognitionAsync();
                }
            }

            return recognizedText.ToString();
        }
    }
}
