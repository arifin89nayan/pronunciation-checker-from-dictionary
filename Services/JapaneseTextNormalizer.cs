using System.Text;

namespace WindowsFormsApp1.UIDesign
{
    public static class JapaneseTextNormalizer
    {
        public static string NormalizeText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            return text.Normalize(NormalizationForm.FormKC).Trim();
        }

        public static string ToHiragana(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            text = NormalizeText(text);

            var sb = new StringBuilder(text.Length);

            foreach (char c in text)
            {
                // Convert full-width Katakana to Hiragana.
                if (c >= '\u30A1' && c <= '\u30F6')
                    sb.Append((char)(c - 0x60));
                else
                    sb.Append(c); // keep ー and other marks
            }

            return sb.ToString().Trim();
        }

        public static bool ReadingsEqual(string a, string b)
        {
            return ToHiragana(a) == ToHiragana(b);
        }

        public static bool ContainsKanjiExceptFixedMarker(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            string cleaned = text.Replace(FixedDictionaryService.Placeholder, "");

            foreach (char c in cleaned)
            {
                if ((c >= '\u4E00' && c <= '\u9FFF') ||
                    (c >= '\u3400' && c <= '\u4DBF'))
                {
                    return true;
                }
            }

            return false;
        }
    }
}