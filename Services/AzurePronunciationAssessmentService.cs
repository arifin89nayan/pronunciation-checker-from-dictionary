using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.PronunciationAssessment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class AzurePronunciationAssessmentService
    {
        private readonly string _speechKey;
        private readonly string _region;

        public AzurePronunciationAssessmentService(string speechKey, string region)
        {
            _speechKey = speechKey;
            _region = region;
        }

        public async Task<AudioQualityFinalResult> AssessAsync(
            string wavPath,
            string referenceText,
            string language)
        {
            var resultModel = new AudioQualityFinalResult();

            var speechConfig = SpeechConfig.FromSubscription(_speechKey, _region);
            speechConfig.SpeechRecognitionLanguage = language;

            using (var audioConfig = AudioConfig.FromWavFileInput(wavPath))
            using (var recognizer = new SpeechRecognizer(speechConfig, audioConfig))
            {
                var pronunciationConfig = new PronunciationAssessmentConfig(
                    referenceText
                );

                pronunciationConfig.ApplyTo(recognizer);

                var result = await recognizer.RecognizeOnceAsync();

                if (result.Reason != ResultReason.RecognizedSpeech)
                {
                    resultModel.Message = "Azure pronunciation assessment failed: " + result.Reason;
                    return resultModel;
                }

                var paResult = PronunciationAssessmentResult.FromResult(result);

                resultModel.AzureAccuracyScore = paResult.AccuracyScore;
                resultModel.AzureFluencyScore = paResult.FluencyScore;
                resultModel.AzureCompletenessScore = paResult.CompletenessScore;

                string json = result.Properties.GetProperty(
                    PropertyId.SpeechServiceResponse_JsonResult
                );

                resultModel.AzureWords = ParseAzureWords(json);

                return resultModel;
            }
        }

        private List<AzureWordScore> ParseAzureWords(string json)
        {
            var words = new List<AzureWordScore>();

            if (string.IsNullOrWhiteSpace(json))
                return words;

            using (var doc = JsonDocument.Parse(json))
            {
                if (!doc.RootElement.TryGetProperty("NBest", out var nbest))
                    return words;

                if (nbest.GetArrayLength() == 0)
                    return words;

                var first = nbest[0];

                if (!first.TryGetProperty("Words", out var wordArray))
                    return words;

                foreach (var item in wordArray.EnumerateArray())
                {
                    string word = item.TryGetProperty("Word", out var w) ? w.GetString() : "";
                    string errorType = item.TryGetProperty("ErrorType", out var e) ? e.GetString() : "";

                    double accuracy = 0;
                    if (item.TryGetProperty("PronunciationAssessment", out var pa))
                    {
                        if (pa.TryGetProperty("AccuracyScore", out var acc))
                            accuracy = acc.GetDouble();
                    }

                    words.Add(new AzureWordScore
                    {
                        Word = word,
                        AccuracyScore = accuracy,
                        ErrorType = errorType
                    });
                }
            }

            return words;
        }
    }
}
