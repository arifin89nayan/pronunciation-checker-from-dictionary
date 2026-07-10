using System;

namespace WindowsFormsApp1.UIDesign
{
    /// <summary>
    /// Central list of supported languages. To add German later:
    /// 1. Create GermanProfile : ILanguageProfile (copy EnglishProfile —
    ///    German is also word-boundary + IPA).
    /// 2. Add it to the All array below.
    /// That's it. The UI dropdown, pipeline, masking, SSML and review
    /// screen pick it up automatically.
    /// </summary>
    public static class LanguageRegistry
    {
        public static readonly ILanguageProfile Japanese = new JapaneseProfile();
        public static readonly ILanguageProfile English = new EnglishProfile();
        public static readonly ILanguageProfile ChineseTaiwan = new ChineseTaiwanProfile();

        public static readonly ILanguageProfile[] All =
        {
            Japanese,
            English,
            ChineseTaiwan
        };

        /// <summary>Default profile keeps existing behavior (Japanese).</summary>
        public static ILanguageProfile Default
        {
            get { return Japanese; }
        }

        public static ILanguageProfile GetByKey(string key)
        {
            foreach (var p in All)
            {
                if (string.Equals(p.Key, key, StringComparison.OrdinalIgnoreCase))
                    return p;
            }

            return Default;
        }
    }
}
