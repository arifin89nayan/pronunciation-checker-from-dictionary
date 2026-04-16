using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WindowsFormsApp1.Models.Widgets;

namespace WindowsFormsApp1.Helper
{
    internal static class PatternHelper
    {
        public static IList<string> GetItems(string input, string pattern)
        {  
            MatchCollection matches = Regex.Matches(input, pattern); 
            IList<string> result = new List<string>();
             
            for (int i = 0; i < matches.Count; i++)
            {
                var v = matches[i];

                result.Add(matches[i].Value);
            }

            return result;
        }
       

    }
}
