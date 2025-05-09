using System;
using System.Collections.Generic;
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
using WeatherAnalizer.Commons;
using WeatherAnalizer.Models.ViewModels;

namespace WeatherAnalizer.Views.Pages
{
    /// <summary>
    /// ucDataPanel.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucDataPanel : UserControl
    {
        private vmWeather _Weather = null;
        public vmWeather Weather
        {
            get => _Weather;
            set
            {
                _Weather = value;
                this.DataContext = value;
               // this.ucDataWeatherByStation.Weather = value;
            }
        }


        public ucDataPanel()
        {
            InitializeComponent();
        }
    }
}
