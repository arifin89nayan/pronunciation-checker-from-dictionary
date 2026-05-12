using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class AudioQualityEngine
    {
        private readonly AzurePronunciationAssessmentService _azureService;
        //private readonly OpenAiTranscriptionService _openAiService;
        private readonly DictionaryVerifierService _dictionaryVerifier;
        private readonly AzureSpeechToTextService _azureSttService;

        public AudioQualityEngine(
            AzurePronunciationAssessmentService azureService,
            AzureSpeechToTextService auzreAiService,
            DictionaryVerifierService dictionaryVerifier)
        {
            _azureService = azureService;
            _azureSttService = auzreAiService;
            _dictionaryVerifier = dictionaryVerifier;
        }

        public async Task<AudioQualityFinalResult> CheckAsync(
            string mp3Path,
            string originalText,
            string dictionaryXmlPath,
            string language)
        {
            string wavPath = null;

            try
            {
                wavPath = Mp3ToWavConverter.ConvertTo16kMonoWav(mp3Path);

                var azureTask = _azureService.AssessAsync(wavPath, originalText, language);
                //var openAiTask = _azureSttService.TranscribeAsync(wavPath, language.StartsWith("ja") ? "ja" : "en");
                //var sttTask = _azureSttService.TranscribeAsync(wavPath, language);
                var sttTask = _azureSttService.TranscribeLongAudioAsync(wavPath, language);

                await Task.WhenAll(azureTask, sttTask);

                var azureResult = await azureTask;
                string recognizedText = await sttTask;

                double cer = TextSimilarityService.CalculateCerPercent(
                    originalText,
                    recognizedText
                );

                var dictionaryResults = _dictionaryVerifier.Verify(
                    originalText,
                    recognizedText,
                    dictionaryXmlPath,
                    azureResult.AzureWords
                );

                azureResult.RecognizedText = recognizedText;
                azureResult.CerPercent = cer;
                azureResult.DictionaryWords = dictionaryResults;

                int totalDict = dictionaryResults.Count;
                int passDict = dictionaryResults.Count(x => x.Status == "PASS");

                azureResult.DictionaryPassRate =
                    totalDict == 0 ? 100 : (double)passDict / totalDict * 100.0;

                DecideFinalGrade(azureResult);

                return azureResult;
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(wavPath) && File.Exists(wavPath))
                    File.Delete(wavPath);
            }
        }

        private void DecideFinalGrade(AudioQualityFinalResult result)
        {
            bool fail =
                result.AzureAccuracyScore < 70 ||
                result.CerPercent > 20 ||
                result.DictionaryWords.Any(x => x.Status == "FAIL");

            bool warning =
                result.AzureAccuracyScore < 85 ||
                result.CerPercent > 10 ||
                result.DictionaryWords.Any(x => x.Status == "WARNING");

            if (fail)
            {
                result.FinalGrade = "FAIL";
                result.Message = "Audio failed quality validation.";
            }
            else if (warning)
            {
                result.FinalGrade = "WARNING";
                result.Message = "Audio is usable, but review is recommended.";
            }
            else
            {
                result.FinalGrade = "PASS";
                result.Message = "Audio passed all validation checks.";
            }
        }
    }
}
