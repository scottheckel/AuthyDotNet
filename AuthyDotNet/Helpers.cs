using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AuthyDotNet
{
    internal class Helpers
    {
        public static string SanitizeNumber(string value)
        {
            return Regex.Replace(value, @"\D", string.Empty);
        }

        public static bool TokenIsValid(string token)
        {
            token = SanitizeNumber(token);

            if (token.Length < 6 || token.Length > 10)
            {
                return false;
            }

            return true;
        }
    }
}
