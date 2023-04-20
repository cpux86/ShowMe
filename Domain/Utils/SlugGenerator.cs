using NickBuhro.Translit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Utils
{
    public class SlugGenerator
    {
        public static string ToUrlSlug(string value)
        {
            value = Transliteration.CyrillicToLatin(value, Language.Russian);
            //First to lower case
            value = value.ToLowerInvariant();

            // замена недопустимых символов
            value = value.Replace("#", "sharp");
            value = value.Replace("+", "plus");

            //Replace spaces
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

            //Remove invalid chars
            value = Regex.Replace(value, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

            //Trim dashes from end
            value = value.Trim('-', '_');

            //Replace double occurences of - or _
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return value;
        }
    }
}
