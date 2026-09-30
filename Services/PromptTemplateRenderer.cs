using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1.Services
{
    public class PromptTemplateRenderer
    {
        private static readonly Regex TokenRegex =
            new Regex(@"\{\{([A-Z0-9_]+)\}\}", RegexOptions.Compiled);

        public string Render(
            string template,
            IDictionary<string, string> values)
        {
            if (template == null)
                throw new ArgumentNullException(nameof(template));

            string result = TokenRegex.Replace(
                template,
                match =>
                {
                    string key = match.Groups[1].Value;

                    if (!values.TryGetValue(key, out string value))
                    {
                        throw new InvalidOperationException(
                            "Prompt template contains an unresolved token: {{" + key + "}}");
                    }

                    return value ?? string.Empty;
                });

            Match unresolved = TokenRegex.Match(result);
            if (unresolved.Success)
            {
                throw new InvalidOperationException(
                    "Prompt template still contains unresolved token: " + unresolved.Value);
            }

            return result;
        }
    }
}
