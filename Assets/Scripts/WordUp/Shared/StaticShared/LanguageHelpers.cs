using System;
using System.Text.RegularExpressions;

namespace WordUp.Shared.StaticShared
{
    public static class LanguageHelpers
    {
        public static readonly string RuLanguageCode = "ru";
        public static readonly string EnLanguageCode = "en";

        public static Language GetLanguageByString(string text)
        {
            text = Regex.Replace(text, "[^0-9a-zA-ZА-Яа-я]+", "");
            
            bool isEnglish = Regex.IsMatch(text, "^[a-zA-Z0-9]*$");

            if (isEnglish)
            {
                return Language.English;
            }
            
            bool isRussian = Regex.IsMatch(text, "^[А-Яа-я0-9]+$");

            if (isRussian)
            {
                return Language.Russian;
            }

            return Language.Other;
        }

        public static string GetLanguageCodeByLanguage(Language language)
        {
            return language switch
            {
                Language.Russian => RuLanguageCode,
                Language.English => EnLanguageCode,
                _ => throw new Exception("Unexpected language")
            };
        }
    }
    
    public enum Language
    {
        English,
        Russian,
        Other
    }
}