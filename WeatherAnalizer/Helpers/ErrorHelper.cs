using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAnalizer.Commons;
using WeatherAnalizer.Views.Windows;

namespace WeatherAnalizer.Helpers
{
    public static class ErrorHelper
    {
        public static void ShowErrorLog(Exception _exception, bool _isNoticed)
        {
            string log = CreateLog(_exception);

            bool isSuccess = CreateLogFile(log);



            if (!_isNoticed || !isSuccess) return;

            wndErrorReport wndER = new wndErrorReport(Defines.ProgramName, log);
            wndER.ShowDialog();
        }

        private static bool CreateLogFile(string log)
        {
            bool isSaveSuccess = true;

            string dirPath = Defines.LOG_PATH + "\\" + DateTime.Now.ToString("yyyy-MM-dd");

            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
            string filePath = dirPath + "\\" + DateTime.Now.ToString("yyMMdd_HHmmss") + ".txt";
            File.WriteAllText(filePath, log);

            return isSaveSuccess;
        }

        private static string CreateLog(Exception _exception)
        {
            string msg = string.Empty;
            msg += "::: Error Time : " + DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + " :::";

            msg += "\n";
            msg += "++++++++++ [ Error Description START ] ++++++++++";
            msg += "\n";
            msg += "\n";


            msg += "Message : " + _exception.Message;
            msg += "\n";
            string locaString = string.Empty;
            try
            {
                locaString = _exception.TargetSite.DeclaringType.FullName;
            }
            catch (Exception)
            {
                locaString = "Location Exception";
            }
            msg += "\n";
            msg += "StackTrace : ";// + _exception.StackTrace;
            string[] stackTrances = _exception.StackTrace.Split(new string[] { "위치:" }, StringSplitOptions.None);
            foreach (string item in stackTrances.Reverse())
            {
                try
                {
                    if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item)) continue;

                    string[] fileSplitStrings = item.Contains("파일") ? item.Split(new string[] { "파일" }, StringSplitOptions.None) : item.Split();
                    string printString = fileSplitStrings[0];
                    printString += "------";
                    printString += fileSplitStrings[1].Contains('\\') ? fileSplitStrings[1].Split('\\').LastOrDefault() : "NONE";
                    msg += "\n\t";
                    msg += printString;
                }
                catch (Exception ee)
                {
                    msg += "\n\nFULL TRACE : ++++";
                    msg += "\n";
                    msg += _exception.StackTrace;
                    break;
                }
            }

            msg += "\n";
            msg += "\n";
            msg += "++++++++++ [ Error Description FINISH ] ++++++++++";
            msg += "\n";
            msg += "\n";


            return msg;
        }
    }
}
