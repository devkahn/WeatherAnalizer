using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WeatherAnalizer.Commons;

namespace WeatherAnalizer.Views.Windows
{
    /// <summary>
    /// wndErrorReport.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class wndErrorReport : Window
    {
        public wndErrorReport()
        {
            InitializeComponent();
        }
        public wndErrorReport(string _programame, string log)
        {
            InitializeComponent();
            this.Title = "[" + _programame + "]의 알 수 없는 오류 발생";
            this.textblock_PathProgramname.Text = _programame;
            this.textbox_logDesription.Text = log;
        }

        private void Grid_MouseMove(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Pressed) this.DragMove();
            }
            catch (Exception)
            {

            }
        }
        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_ShowFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Process.Start(Defines.LOG_PATH);
            }
            catch (Exception)
            {
            }
        }
    }
}
