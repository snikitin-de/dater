using System.Text.RegularExpressions;

namespace dater
{
    class Validator
    {
        public static bool IsValidPositiveInt(string str)
        {
            int i;
            return int.TryParse(str, out i) && i > 0;
        }

        public static bool IsValidSeparator(string str)
        {
            string validSeparator = @"[,;t]";
            return Regex.IsMatch(str, validSeparator) && str.Length == 1;
        }
    }
}
