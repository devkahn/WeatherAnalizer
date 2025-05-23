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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherAnalizer.Models.ViewModels;

namespace WeatherAnalizer.Views.Pages
{
    /// <summary>
    /// ucDataWeatherByDaily.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucDataWeatherByDaily : UserControl
    {
        private vmWeather _Weather = null;
        public vmWeather Weather
        {
            get => _Weather;
            set
            {
                _Weather = value;
                this.DataContext = value;
            }
        }
        public ucDataWeatherByDaily()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(e.AddedItems == null || e.AddedItems.Count == 0)
                {
                    this.txtblock_AveRain.Text = string.Empty;
                    this.txtblock_AveSnow.Text = string.Empty;
                    this.txtblock_AveTemperature.Text = string.Empty;
                    this.txtblock_AveWindSpeed.Text = string.Empty;
                    return;
                }

                List<vmWeatherByDate> selectedItems = new List<vmWeatherByDate>();
                foreach (vmWeatherByDate item in this.dg_weatherData.SelectedItems) selectedItems.Add(item);


                int cnt = selectedItems.Count();
                this.txtblock_Count.Text = cnt.ToString();

                double sum_Rain = selectedItems.Sum(x => x.Ave_RainTotal);
                this.txtblock_AveRain.Text = (sum_Rain / cnt).ToString("N2");

                double sum_Snow = selectedItems.Sum(x => x.Ave_DailySnowTotal);
                this.txtblock_AveSnow.Text = (sum_Snow / cnt).ToString("N2");

                double sum_Temperature = selectedItems.Sum(x => x.Ave_TemperatureAve);
                this.txtblock_AveTemperature.Text = (sum_Temperature / cnt).ToString("N2");

                double sum_WindSpeed = selectedItems.Sum(x => x.Ave_WindSpeedMax);
                this.txtblock_AveWindSpeed.Text = (sum_WindSpeed / cnt).ToString("N2");

            }
            catch (Exception ee)
            {
                
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.listbox_StationList.SelectedItem == null) this.listbox_StationList.SelectedIndex = 0;
            }
            catch (Exception ee)
            {
                
            }
        }
    }
}
