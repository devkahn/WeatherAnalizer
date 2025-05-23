using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherAnalizer.Helpers;
using WeatherAnalizer.Models.DataModels;
using WeatherAnalizer.Models.ViewModels;

namespace WeatherAnalizer.Views.Pages
{
    /// <summary>
    /// ucMain.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucMain : UserControl
    {
        private vmWeather _Weather = null;
        public vmWeather Weather
        {
            get => _Weather;
            set
            {
                _Weather = value;
                this.DataContext = value;
                this.ucControlPanel.Weather = value;
                this.ucDatapanel.Weather = value;
            }
        }

        public ucMain()
        {
            InitializeComponent();
        }

        private void btn_Load_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ee)
            {
                ErrorHelper.ShowErrorLog(ee, false);
            }
        }

        private void btn_Export_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog odf = new OpenFileDialog();
                bool? result = odf.ShowDialog();
                if (!result.HasValue || !result.Value) return;

                ExcelPackage package = new ExcelPackage();
                ExcelWorkbook workbook = package.Workbook;

                foreach (vmPublicWeatherStation station in this.Weather.Stations)
                {
                    ExcelWorksheet sheet = workbook.Worksheets.Add(station.Origin.StationName);

                    foreach (mPublicDailyWeatherData item in station.DailyWeatherData)
                    {
                        
                    }







                }

                FileInfo fInfo = new FileInfo(odf.FileName);
                package.SaveAs(fInfo.FullName);

                Process.Start(fInfo.Directory.FullName);

            }
            catch (Exception ee)
            {
                
                
            }
        }
    }
}
