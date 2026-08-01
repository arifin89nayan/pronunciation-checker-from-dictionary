using ClosedXML.Excel;
using ClosedXML.Excel.CalcEngine.Functions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.CognitiveServices.Speech;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Xml.Linq;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Models.AutoConverters;
using WindowsFormsApp1.Services;



namespace WindowsFormsApp1.Helper
{
    public class TextConverterService
    {
        //private const string key = "faad58ee6ac7477d966aa52401f0efab"; //key2

        private const string key = "47e0d89105c1425582baf32853502a55"; //key 1
        private const string region = "japaneast";
        //private const string Endpoint= "https://japaneast.api.cognitive.microsoft.com/";
        private string _outputFilePath;

        public TextConverterService(string outputFilePath)
        {
            _outputFilePath = outputFilePath;
        }
        public static string GenerateLexiconFromExcel(string excelPath,string outputXmlPath,string language)
        {
            if (string.IsNullOrWhiteSpace(excelPath))
                throw new ArgumentException("Excel path is empty.", nameof(excelPath));

            if (!File.Exists(excelPath))
                throw new FileNotFoundException(
                    "Dictionary Excel file was not found.",
                    excelPath);

            if (string.IsNullOrWhiteSpace(outputXmlPath))
                throw new ArgumentException(
                    "Output XML path is empty.",
                    nameof(outputXmlPath));

            language = string.IsNullOrWhiteSpace(language)
                ? "ja-JP"
                : language.Trim();

            bool isEnglish = language.StartsWith(
                "en",
                StringComparison.OrdinalIgnoreCase);

            bool isJapanese = language.StartsWith(
                "ja",
                StringComparison.OrdinalIgnoreCase);

            // Japanese uses SAPI pronunciation.
            // English uses IPA or aliases.
            string alphabet = isEnglish ? "ipa" : "sapi";

            XNamespace ns =
                "http://www.w3.org/2005/01/pronunciation-lexicon";

            XNamespace xsi =
                "http://www.w3.org/2001/XMLSchema-instance";

            var lexicon = new XElement(
                ns + "lexicon",

                new XAttribute("version", "1.0"),
                new XAttribute("alphabet", alphabet),
                new XAttribute(XNamespace.Xml + "lang", language),
                new XAttribute(
                    XNamespace.Xmlns + "xsi",
                    xsi.NamespaceName),

                new XAttribute(
                    xsi + "schemaLocation",
                    "http://www.w3.org/2005/01/pronunciation-lexicon " +
                    "http://www.w3.org/TR/2007/CR-pronunciation-lexicon-20071212/pls.xsd")
            );

            var seenGraphemes =
                new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            using (var workbook = new XLWorkbook(excelPath))
            {
                var worksheet = workbook.Worksheet(1);
                var range = worksheet.RangeUsed();

                if (range == null)
                    throw new InvalidOperationException(
                        "The dictionary Excel file is empty.");

                foreach (var row in range.RowsUsed())
                {
                    // Column A: word
                    string grapheme =
                        NormalizeKey(row.Cell(1).GetString());

                    // Column B: pronunciation or alias
                    string pronunciation =
                        NormalizePhoneme(row.Cell(2).GetString());

                    if (string.IsNullOrWhiteSpace(grapheme))
                    {
                        ErrorLogger.LogError(
                            $"Row {row.RowNumber()} skipped: word is empty.");

                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(pronunciation))
                    {
                        ErrorLogger.LogError(
                            $"Row {row.RowNumber()} skipped: pronunciation is empty.");

                        continue;
                    }

                    if (!seenGraphemes.Add(grapheme))
                    {
                        ErrorLogger.LogError(
                            $"Duplicate grapheme skipped: \"{grapheme}\"");

                        continue;
                    }

                    XElement lexeme;

                    if (isEnglish)
                    {
                        // English Excel:
                        // Column A = original word
                        // Column B = spoken alias
                        lexeme = new XElement(
                            ns + "lexeme",

                            new XElement(
                                ns + "grapheme",
                                grapheme),

                            new XElement(
                                ns + "alias",
                                pronunciation)
                        );
                    }
                    else
                    {
                        // Japanese and other phoneme-based dictionaries
                        lexeme = new XElement(
                            ns + "lexeme",

                            new XElement(
                                ns + "grapheme",
                                grapheme),

                            new XElement(
                                ns + "phoneme",
                                pronunciation)
                        );
                    }

                    lexicon.Add(lexeme);
                }
            }

            string outputDirectory =
                Path.GetDirectoryName(outputXmlPath);

            if (!string.IsNullOrWhiteSpace(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            var document = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                lexicon);

            document.Save(outputXmlPath);

            return outputXmlPath;
        }
        private static string NormalizeKey(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            value = value.Trim();

            // Japanese full-width space → normal space
            value = value.Replace("\u3000", " ");

            value = System.Text.RegularExpressions.Regex.Replace(
                value,
                @"\s+",
                " ");

            return value;
        }

        private static string NormalizePhoneme(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? string.Empty
                : value.Trim();
        }

        public async Task<bool> GenerateTextToSpeechAsync(string inputText, TextValue lanAndVoice)
        {
            try
            {
                string lang = lanAndVoice.language;
                string voice = lanAndVoice.voice;
                string filename = lanAndVoice.filename;
                string languagepath = Properties.Settings.Default.ExcelLanguage;
                string folderPath = Path.GetDirectoryName(languagepath);
                string safeLangPath = folderPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                string dictPath = !string.IsNullOrEmpty(filename) ? Path.Combine(safeLangPath, filename) : null;
                string textWithPhonemes;


                string outputXmlPath = Path.ChangeExtension(dictPath, ".xml");

                if (string.IsNullOrWhiteSpace(filename))
                {
                    // For other languages, use default method
                    await SynthesizeAudioAsyncV2(key, region, inputText, lang, voice, _outputFilePath);
                    return true;
                }



                //string outputXmlPath = Path.Combine(safeLangPath, ".xml");
                if (!File.Exists(outputXmlPath))
                {

                    //MessageBox.Show($"Dictionary file not found: {dictPath}");
                    ErrorLogger.LogError($"Dictionary file not found: {outputXmlPath}");
                    //await SynthesizeAudioAsyncV2(key, region, inputText, lang, voice, _outputFilePath);
                    //return true;

                }
                bool regenerate = false;

                if (!File.Exists(outputXmlPath))
                {
                    // No XML yet, must generate
                    regenerate = true;
                }
                else
                {
                    DateTime excelDate = File.GetLastWriteTimeUtc(dictPath);
                    DateTime xmlDate = File.GetLastWriteTimeUtc(outputXmlPath);

                    if (excelDate > xmlDate)
                    {
                        // Excel has changed since XML was generated
                        regenerate = true;
                    }
                }

                if (regenerate)
                {
                    // Call your XML generation code
                    var newxmlfile = GenerateLexiconFromExcel(dictPath, outputXmlPath, lanAndVoice.language);

                }

                var helper = new PhonemeLexiconHelper(outputXmlPath, lanAndVoice.language);
                // 2. Inject phoneme tags into your input text

                // Initialize textWithPhonemes to avoid CS0165
                textWithPhonemes = null;

                if (lang.StartsWith("jp-JP", StringComparison.OrdinalIgnoreCase))
                {
                    textWithPhonemes = helper.InjectPhonemesJapanese(inputText);
                }
                else if (lang.StartsWith("en", StringComparison.OrdinalIgnoreCase))
                {
                    textWithPhonemes = helper.InjectPhonemes(inputText);
                }

                // Ensure textWithPhonemes is assigned before use
                if (!string.IsNullOrEmpty(filename))
                {
                    // Use SSML with lexicon for Japanese
                    //string ssml = BuildSSMLWithLexicon(inputText, lanAndVoice.language, lanAndVoice.voice, lexiconUrl);
                    string ssml = PhonemeLexiconHelper.BuildSsml(textWithPhonemes, lanAndVoice.language, lanAndVoice.voice);
                    await SynthesizeAudioWithLexiconAsync(key, region, ssml, _outputFilePath);
                }
                else
                {
                    // For other languages, use default method
                    await SynthesizeAudioAsyncV2(key, region, inputText, lang, voice, _outputFilePath);
                }

                return true;
            }
            catch (Exception ex)
            {
                //Console.Error.WriteLine($"Audio generation error: {ex.Message}");
                ErrorLogger.LogError($"Audio generation error: {ex.Message}");
                return false;
            }
        }



        public static TextValue MapQuizModelToTextValue(QuizParserModel quizModel, List<LanguageParsedModel> languageVoiceMap)
        {
            var matchLanguage = languageVoiceMap
               .FirstOrDefault(x =>
                   string.Equals(x.LanguageName?.Trim(), quizModel.language?.Trim(), StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(x.Language?.Trim(), quizModel.language?.Trim(), StringComparison.OrdinalIgnoreCase)
               );
            if (matchLanguage != null)
            {
                return new TextValue
                {
                    language = matchLanguage.Language,
                    voice = matchLanguage.Voiceoption,
                    filename = matchLanguage.LanguageDictionary,
                    fontname = matchLanguage.FontName,
                    fontsize = matchLanguage.FontSize

                };
            }

            // Default fallback
            return new TextValue
            {
                language = "en-US",
                voice = "en-US-BrianNeural",
                fontname = "Arial",
                fontsize = "20"
            };
        }

        public static TextValue MapGuideModelToTextValue(GuideParsedModel guideModel, List<LanguageParsedModel> languageList)
        {

            var matchLanguage = languageList
                 .FirstOrDefault(x =>
                     string.Equals(x.LanguageName?.Trim(), guideModel.language?.Trim(), StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(x.Language?.Trim(), guideModel.language?.Trim(), StringComparison.OrdinalIgnoreCase)
                 );
            if (matchLanguage != null)
            {
                return new TextValue
                {
                    language = matchLanguage.Language,
                    voice = matchLanguage.Voiceoption,
                    filename = matchLanguage.LanguageDictionary,
                    fontname = matchLanguage.FontName,
                    fontsize = matchLanguage.FontSize
                };
            }

            return new TextValue
            {
                language = "en-US",
                voice = "en-US-BrianNeural",
                fontname = "Arial",
                fontsize = "20"
            };
        }
        private string BuildSSMLWithLexicon(string text, string language, string voice, string lexiconUrl)
        {
            return $@"
            <speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='{language}'>
                <lexicon uri='{lexiconUrl}' />
                <voice name='{voice}'>{text}</voice>
            </speak>";
        }
        public async Task<bool> NewGenerateTextToSpeechAsync(string inputText, TextValue lanAndVoice)
        {
            try
            {
                // Set your public lexicon URL here!
                string lexiconUrl = "https://ipuresearch.blob.core.windows.net/iputest/JanDic.xml"; // <-- UPDATE THIS!

                if (lanAndVoice.language.StartsWith("ja", StringComparison.OrdinalIgnoreCase))
                {
                    // Use SSML with lexicon for Japanese
                    string ssml = BuildSSMLWithLexicon(inputText, lanAndVoice.language, lanAndVoice.voice, lexiconUrl);
                    await SynthesizeAudioWithLexiconAsync(key, region, ssml, _outputFilePath);
                }
                else
                {
                    // For other languages, use default method
                    await SynthesizeAudioAsyncV2(key, region, inputText, lanAndVoice.language, lanAndVoice.voice, _outputFilePath);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Audio generation error: {ex.Message}");
                return false;
            }
        }

        private async Task SynthesizeAudioWithLexiconAsync(string key, string region, string ssml, string outputFilePath)
        {
            var speechConfig = SpeechConfig.FromSubscription(key, region);
            speechConfig.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Riff16Khz16BitMonoPcm);

            string id = Guid.NewGuid().ToString("N");
            string tempWavPath = Path.Combine(Path.GetTempPath(), $"temp_{id}.wav");
            string tempMp3Path = Path.Combine(Path.GetTempPath(), $"temp_{id}.mp3");

            try
            {
                using (var speechSynthesizer = new SpeechSynthesizer(speechConfig, null))
                {
                    var result = await speechSynthesizer.SpeakSsmlAsync(ssml);
                    if (result.Reason == ResultReason.Canceled)
                    {
                        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                        //Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");
                        ErrorLogger.LogError($"CANCELED: Reason={cancellation.Reason}");

                        if (cancellation.Reason == CancellationReason.Error)
                        {
                            //Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                            ErrorLogger.LogError($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                            // Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                            ErrorLogger.LogError($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                            //Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
                            ErrorLogger.LogError($"CANCELED: Did you set the speech resource key and region values?");
                        }
                    }
                    //if (result.Reason != ResultReason.SynthesizingAudioCompleted)
                    //    throw new Exception($"Speech synthesis failed: {result.Reason}");

                    var stream = AudioDataStream.FromResult(result);
                    await stream.SaveToWaveFileAsync(tempWavPath);
                }

                if (!File.Exists(tempWavPath) || new FileInfo(tempWavPath).Length == 0)
                {
                    string msg = "WAV file was not created or is empty.";
                    ErrorLogger.LogError($"Problem is :{msg}");
                    throw new Exception(msg);

                }


                using (var reader = new WaveFileReader(tempWavPath))
                {
                    using (var mp3Writer = new FileStream(tempMp3Path, FileMode.Create, FileAccess.Write))
                    {
                        MediaFoundationEncoder.EncodeToMp3(reader, mp3Writer);
                    }
                }
                File.Move(tempMp3Path, outputFilePath);
            }
            catch (Exception ex)
            {
                //Console.Error.WriteLine($"Audio generation error: {ex.Message}");
                ErrorLogger.LogError($"Audio generation error: {ex.Message}");
                throw;
            }
            finally
            {
                if (File.Exists(tempWavPath)) File.Delete(tempWavPath);
                if (File.Exists(tempMp3Path)) File.Delete(tempMp3Path);
            }
        }




        private async Task SynthesizeAudioAsyncV2(string key, string region, string text, string language, string voice1, string outputFilePath)
        {
            var speechConfig = SpeechConfig.FromSubscription(key, region);
            speechConfig.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Riff16Khz16BitMonoPcm);

            speechConfig.SpeechSynthesisLanguage = language;
            speechConfig.SpeechSynthesisVoiceName = voice1;

            string id = Guid.NewGuid().ToString("N");
            string tempWavPath = Path.Combine(Path.GetTempPath(), $"temp_{id}.wav");
            string tempMp3Path = Path.Combine(Path.GetTempPath(), $"temp_{id}.mp3");

            try
            {
                using (var speechSynthesizer = new SpeechSynthesizer(speechConfig, null))
                {
                    var result = await speechSynthesizer.SpeakTextAsync(text);
                    if (result.Reason != ResultReason.SynthesizingAudioCompleted)
                        throw new Exception($"Speech synthesis failed: {result.Reason}");

                    var stream = AudioDataStream.FromResult(result);
                    await stream.SaveToWaveFileAsync(tempWavPath);
                }

                // Ensure the file exists and is not empty
                if (!File.Exists(tempWavPath) || new FileInfo(tempWavPath).Length == 0)
                    throw new Exception("WAV file was not created or is empty.");

                using (var reader = new WaveFileReader(tempWavPath))
                {
                    using (var mp3Writer = new FileStream(tempMp3Path, FileMode.Create, FileAccess.Write))
                    {
                        MediaFoundationEncoder.EncodeToMp3(reader, mp3Writer);
                    }
                }

                File.Move(tempMp3Path, outputFilePath);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError($"Audio generation error: {ex.Message}");
                throw;
            }
            finally
            {
                if (File.Exists(tempWavPath)) File.Delete(tempWavPath);
                if (File.Exists(tempMp3Path)) File.Delete(tempMp3Path);
            }
        }


    }
}
