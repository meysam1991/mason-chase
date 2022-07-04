using System;
using System.Security.Cryptography;
using System.Text;

namespace Mc2.CrudTest.Shared.StringUtils
{
    public static class StringExtensions
    {
        public static byte[] ToByteArray(this string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }

        public static bool IsNullOrEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }
        public static string Normalized(this string input)
        {
            return input
                .Trim()
                .Replace(".", "")
                .Normalize();
        }
        public static string Hash(this string text)
        {
            using var sha1 = new SHA1Managed();
            return BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(text)));
        }
        public static string ConvertPersianNumbersToEnglishNumbers(this string text)
        {
            if (text == null) return null;
            if (text == "") return "";
            var persianDigits = new[] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹", "-" };
            var englishDigits = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "-" };
            for (var j = 0; j < persianDigits.Length; j++)
                text = text.Replace(persianDigits[j], englishDigits[j]);
            return text;
        }
    }
}
