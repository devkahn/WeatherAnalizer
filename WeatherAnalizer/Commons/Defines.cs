using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAnalizer.Commons
{
    public static class Defines
    {
        internal static string PromgraGuid => "8AAED00E-2337-45AD-8FCA-502A6F8258A1";
        internal static string ProgramName => "Weather Analyzer";

        internal static string LOG_PATH { get; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DLCON\\" + "Weather Analyzer" + "\\";
    }
}
