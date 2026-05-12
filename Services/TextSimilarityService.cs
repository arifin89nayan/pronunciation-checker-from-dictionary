using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Services
{
    public static class TextSimilarityService
    {
        public static double CalculateCerPercent(string expected, string actual)
        {
            string a = NormalizeJapanese(expected);
            string b = NormalizeJapanese(actual);

            if (string.IsNullOrWhiteSpace(a) && string.IsNullOrWhiteSpace(b))
                return 0;

            if (string.IsNullOrWhiteSpace(a))
                return 100;

            int distance = LevenshteinDistance(a, b);
            return (double)distance / a.Length * 100.0;
        }

        public static string NormalizeJapanese(string text)
        {
            if (text == null) return "";

            text = text.Trim();
            text = text.Replace("　", "");
            text = Regex.Replace(text, @"\s+", "");
            text = Regex.Replace(text, @"[。、．，,\.!?！？「」『』（）\(\)\[\]]", "");

            return text;
        }

        private static int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++) d[i, 0] = i;
            for (int j = 0; j <= m; j++) d[0, j] = j;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = s[i - 1] == t[j - 1] ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost
                    );
                }
            }

            return d[n, m];
        }
    }
}
