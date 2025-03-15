using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WeatherAnalizer.Helpers
{
    public static class MessageHelper
    {
        public static void ShowErrorMessage(string caption, string message)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static MessageBoxResult ShowQuestion(string caption, string messgae)
        {
            return MessageBox.Show(messgae, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        public static void ShowMessage(string caption, string messgae)
        {
            MessageBox.Show(messgae, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
