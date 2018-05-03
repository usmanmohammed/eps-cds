using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EPS.Models
{
    public static class StringExtensions
    {
        public static string TruncateTo(this string input, int length)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.Length > length)
                {
                    return input.Substring(0, length) + "...";
                }
                return input;
            }
            return string.Empty;
        }

        public static string ToTitle(this string input)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToSentenceCase());
        }

        public static string ToSentenceCase(this string input)
        {
            return Regex.Replace(input, "[a-z][A-Z]", v => v.Value[0] + " " + char.ToLower(v.Value[1]));
        }
    }
}
