using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Services
{
    public class DictionaryVerifierService
    {
        public List<DictionaryWordCheck> Verify(
            string originalText,
            string recognizedText,
            string dictionaryXmlPath,
            List<AzureWordScore> azureWords)
        {
            var results = new List<DictionaryWordCheck>();

            if (!File.Exists(dictionaryXmlPath))
                throw new FileNotFoundException("Dictionary XML not found.", dictionaryXmlPath);

            string originalNorm = NormalizeJapanese(originalText);
            string recognizedNorm = NormalizeJapanese(recognizedText);

            bool isLongText = originalNorm.Length > 120;

            var dictionaryWords = LoadDictionary(dictionaryXmlPath)
                .OrderByDescending(x => x.Word.Length)
                .ToList();

            foreach (var item in dictionaryWords)
            {
                string wordNorm = NormalizeJapanese(item.Word);

                if (string.IsNullOrWhiteSpace(wordNorm))
                    continue;

                bool foundOriginal = originalNorm.Contains(wordNorm);

                if (!foundOriginal)
                    continue;

                bool foundRecognized = recognizedNorm.Contains(wordNorm);

                double? azureAccuracy = FindAzureAccuracy(item.Word, azureWords);

                string status;
                string message;

                if (isLongText)
                {
                    // For long paragraph, Azure Pronunciation Assessment score is often unreliable.
                    // Use STT/dictionary as main signal.
                    if (foundRecognized)
                    {
                        status = "PASS";
                        message = "Dictionary word found in recognized text.";
                    }
                    else
                    {
                        status = "WARNING";
                        message = "Dictionary word exists in original text, but exact recognized match was not found. Please review.";
                    }
                }
                else
                {
                    if (foundRecognized && (!azureAccuracy.HasValue || azureAccuracy.Value >= 80))
                    {
                        status = "PASS";
                        message = "Dictionary word found and pronunciation score is good.";
                    }
                    else if (foundRecognized && azureAccuracy.HasValue && azureAccuracy.Value >= 60)
                    {
                        status = "WARNING";
                        message = "Dictionary word found, but pronunciation score is low.";
                    }
                    else if (foundRecognized && !azureAccuracy.HasValue)
                    {
                        status = "WARNING";
                        message = "Dictionary word found, but Azure pronunciation score was not available.";
                    }
                    else
                    {
                        status = "FAIL";
                        message = "Dictionary word missing from recognized text.";
                    }
                }

                results.Add(new DictionaryWordCheck
                {
                    Word = item.Word,
                    ExpectedPhoneme = item.Phoneme,
                    FoundInOriginalText = foundOriginal,
                    FoundInRecognizedText = foundRecognized,
                    AzureAccuracyScore = azureAccuracy.HasValue ? azureAccuracy.Value : -1,
                    Status = status,
                    Message = message
                });
            }

            return results;
        }

        private double? FindAzureAccuracy(string dictionaryWord, List<AzureWordScore> azureWords)
        {
            if (azureWords == null || azureWords.Count == 0)
                return null;

            string target = NormalizeJapanese(dictionaryWord);

            // 1. Exact normalized match
            var exactMatches = azureWords
                .Where(w => NormalizeJapanese(w.Word) == target)
                .Where(w => w.AccuracyScore > 0)
                .ToList();

            if (exactMatches.Count > 0)
                return exactMatches.Average(w => w.AccuracyScore);

            // 2. Partial match
            // Example:
            // dictionary: 旧石器
            // Azure word: 旧石器時代
            var partialMatches = azureWords
                .Where(w =>
                {
                    string aw = NormalizeJapanese(w.Word);

                    if (string.IsNullOrWhiteSpace(aw))
                        return false;

                    if (target.Length < 2 || aw.Length < 2)
                        return false;

                    return aw.Contains(target) || target.Contains(aw);
                })
                .Where(w => w.AccuracyScore > 0)
                .ToList();

            if (partialMatches.Count > 0)
                return partialMatches.Average(w => w.AccuracyScore);

            return null;
        }

        private string NormalizeJapanese(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            text = text.Trim();

            text = text.Replace("　", "");
            text = text.Replace(" ", "");
            text = text.Replace(",", "");

            // Number normalization for your case
            text = text.Replace("20,000", "2万");
            text = text.Replace("20000", "2万");
            text = text.Replace("13,000", "1万3000");
            text = text.Replace("13000", "1万3000");

            text = Regex.Replace(text, @"[。、．，,\.!?！？「」『』（）\(\)\[\]\r\n\t]", "");

            return text;
        }

        private List<(string Word, string Phoneme)> LoadDictionary(string xmlPath)
        {
            var list = new List<(string Word, string Phoneme)>();

            XDocument doc = XDocument.Load(xmlPath);
            XNamespace ns = "http://www.w3.org/2005/01/pronunciation-lexicon";

            foreach (var lexeme in doc.Descendants(ns + "lexeme"))
            {
                string word = lexeme.Element(ns + "grapheme")?.Value?.Trim();
                string phoneme = lexeme.Element(ns + "phoneme")?.Value?.Trim();

                if (!string.IsNullOrWhiteSpace(word))
                    list.Add((word, phoneme ?? ""));
            }

            return list;
        }
    }
}
