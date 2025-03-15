using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAnalizer.Helpers
{
    public static class TextHelper
    {
        public static bool IsNoText(string input)
        {
            return string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input);
        }
        public static bool IsTextNuberic(this string text)
        {
            foreach (char c in text)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        public static bool IsTextNumbericElement(this string text)
        {
            foreach (char c in text)
            {
                if (Char.IsDigit(c))
                {
                    continue;
                }
                else
                {
                    if (c.Equals('.')) continue;
                    if (c.Equals('-')) continue;
                    return false;
                }
            }
            return true;
        }
    }
}
