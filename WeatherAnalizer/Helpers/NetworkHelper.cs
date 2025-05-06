using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAnalizer.Helpers
{
    public static class NetworkHelper
    {
        public static bool IsNetworkConnected()
        {
            bool output = false;

            // if (Debugger.IsAttached || Defines.URL.Contains("localhost")) return true;

            TcpClient tcpc = new TcpClient();
            try
            {
                tcpc.Connect("10.74.1.230", 80);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
