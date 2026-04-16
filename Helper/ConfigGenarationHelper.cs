using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Helper
{
    public static class ConfigGenarationHelper
    {
        public static string ReplaceValueBeforeTemplate(string input, string template, int position, string newValue)
        {
            // Find the position of 'template'
            int templatePos = input.IndexOf(template);

            if (templatePos == -1)
            {
                return input; // Return original if template not found
            }

            // Initialize variables to store positions of '<' and '>'
            int lastLessThan = -1;
            int lastGreaterThan = -1;

            // Loop backwards to find the specified occurrence of '<...>' before 'template'
            while (position > 0 && templatePos > 0)
            {
                lastLessThan = input.LastIndexOf('<', templatePos - 1);
                if (lastLessThan == -1)
                {
                    return input; // Return original if no '<' found
                }

                // Ensure that we find a '>' after our '<' and before the template
                lastGreaterThan = input.IndexOf('>', lastLessThan);
                if (lastGreaterThan == -1 || lastGreaterThan > templatePos)
                {
                    return input; // Return original if no '>' found or '>' after template
                }

                // Decrease position count and adjust the templatePos to look for previous '<>'
                position--;
                templatePos = lastLessThan;
            }

            // Rebuild the string with the new value, assuming position was reached
            if (position == 0)
            {
                return input.Substring(0, lastLessThan + 1) + newValue + input.Substring(lastGreaterThan);
            }

            return input; // Return original if position count not satisfied
        }
        public static string ReplaceValueAfterTemplate(string input, string template, int position, string newValue)
        {
            // Find the position just after 'template'
            int startPos = input.IndexOf(template) + template.Length;
            int lessThan = -1, greaterThan = -1;

            // Loop through to find the correct position of '<' and '>'
            for (int i = 0; i < position; i++)
            {
                lessThan = input.IndexOf('<', startPos);
                greaterThan = input.IndexOf('>', lessThan);
                startPos = greaterThan + 1; // Move start position past the current '>'
            }

            // Check that the brackets are found and not overlapping
            if (lessThan != -1 && greaterThan != -1 && lessThan < greaterThan)
            {
                // Rebuild the string with the new value
                input = input.Substring(0, lessThan + 1) + newValue + input.Substring(greaterThan);
            }

            return input;
        }
    }
}
