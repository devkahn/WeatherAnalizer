using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAnalizer.Commons
{
    public static class Defines
    {
        internal static string PromgraGuid => "8AAED00E-2337-45AD-8FCA-502A6F8258A1";
        internal static string ProgramName => "Weather Analyzer";

        internal static string DLCON_DIRECTORY => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DLCON");
        internal static string BASE_DIRECTORY => Path.Combine(DLCON_DIRECTORY, ProgramName);
        internal static string BASE_DATA_DIRECTORY => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources", "Databases");
        internal static string BASE_DATA_PROGRAM => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources", "Settings");
        internal static string LOG_PATH { get; } = Path.Combine(BASE_DIRECTORY, "Error Logs");
        internal static string DATABASE_DIRECTORY => Path.Combine(BASE_DIRECTORY, "52006284-03DF-40BD-BFBA-E2AA17A9513B");

        public static List<string> LIST_COLUMN_NAMES = new List<string>() { "TM", "STN", "WS", "TA", "RN", "SD_HR3" };


        internal static string STATION_FILE_NAME => "wa_station_base.db";
        

        //public static string URL { get; set; } = Debugger.IsAttached ? "https://localhost:44319/" : "http://10.74.1.230/";
        public static string URL { get; set; } = Debugger.IsAttached ? "http://10.74.1.230/" : "http://10.74.1.230/";


    }
}
