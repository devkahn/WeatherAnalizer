using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WeatherAnalizer.Helpers
{
    internal static class WindowHelper
    {
        public static void CenterWindowOnScreen(Window wnd)
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = wnd.Width;
            double windowHeight = wnd.Height;
            wnd.Left = (screenWidth / 2) - (windowWidth / 2);
            wnd.Top = (screenHeight / 2) - (windowHeight / 2);
        }
    }
}
