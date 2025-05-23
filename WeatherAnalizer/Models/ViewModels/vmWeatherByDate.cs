using DLWIZ.ViewModels.Commons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using WeatherAnalizer.Models.DataModels;

namespace WeatherAnalizer.Models.ViewModels
{
    public partial class vmWeatherByDate
    {
        private double _Sum_WindSpeedMax = 0;
        private double _Sum_TemperatureMax = 0;
        private double _Sum_TemperatureAve = 0;
        private double _Sum_TemperatureMin = 0;
        private double _Sum_RainTotal = 0;
        private double _Sum_99RainTotal = 0;
        private double _Sum_SnowTotal = 0;
        private double _Sum_DailySnowTotal = 0;

        private double _Ave_WindSpeedMax = 0;
        private double _Ave_TemperatureMax = 0;
        private double _Ave_TemperatureAve = 0;
        private double _Ave_TemperatureMin = 0;
        private double _Ave_RainTotal = 0;
        private double _Ave_99RainTotal = 0;
        private double _Ave_SnowTotal = 0;
        private double _Ave_DailySnowTotal = 0;

        private object _Display_Month = null;
        private object _Display_Day = null;

        private object _Display_Ave_WindSpeedMax = null;
        private object _Display_Ave_TemperatureMax = null;
        private object _Display_Ave_TemperatureAve = null;
        private object _Display_Ave_TemperatureMin = null;
        private object _Display_Ave_RainTotal = null;
        private object _Display_Ave_99RainTotal = null;
        private object _Display_Ave_SnowTotal = null;
        private object _Display_Ave_DailySnowTotal = null;
    }

    public partial class vmWeatherByDate : vmBaseModel
    {
        public vmWeatherByDate(mPublicDailyWeatherData item)
        {
            this.DailyChildrenOrigin = new ObservableCollection<mPublicDailyWeatherData>();
            this.DailyChildrenOrigin.CollectionChanged += DailyChildrenOrigin_CollectionChanged;

            this.Date = new DateTime(0004, item.DateMonth, item.DateDay);
            this.Dispaly_Month = this.Date.Month;
            this.Dispaly_Day = this.Date.Day;  
            AddChild(item);
        }





        public DateTime Date { get; set; }
        public ReadOnlyObservableCollection<mPublicDailyWeatherData> DailyChildren => new ReadOnlyObservableCollection<mPublicDailyWeatherData>(this.DailyChildrenOrigin);
        private ObservableCollection<mPublicDailyWeatherData> DailyChildrenOrigin { get; set; }
        public int DataCount { get; set; } = 0;




        private double Sum_WindSpeedMax
        {
            get => _Sum_WindSpeedMax;
            set
            {
                _Sum_WindSpeedMax = value;
                this.Ave_WindSpeedMax = value / this.DataCount;
            }
        }
        private double Sum_TemperatureMax
        {
            get => _Sum_TemperatureMax;
            set
            {
                _Sum_TemperatureMax = value;
                this.Ave_TemperatureMax = value / this.DataCount;
            }
        }
        private double Sum_TemperatureAve
        {
            get => _Sum_TemperatureAve;
            set
            {
                _Sum_TemperatureAve = value;
                this.Ave_TemperatureAve = value / this.DataCount;
            }
        }
        private double Sum_TemperatureMin
        {
            get => _Sum_TemperatureMin;
            set
            {
                _Sum_TemperatureMin = value;
                this.Ave_TemperatureMin = value / this.DataCount;
            }
        }
        private double Sum_RainTotal
        {
            get => _Sum_RainTotal;
            set
            {
                _Sum_RainTotal = value;
                this.Ave_RainTotal = value / this.DataCount;
            }
        }
        private double Sum_99RainTotal
        {
            get => _Sum_99RainTotal;
            set
            {
                _Sum_99RainTotal = value;
                this.Ave_99RainTotal = value / this.DataCount;
            }
        }
        private double Sum_SnowTotal
        {
            get => _Sum_SnowTotal;
            set
            {
                _Sum_SnowTotal = value;
                this.Ave_SnowTotal = value / this.DataCount;
            }
        }
        private double Sum_DailySnowTotal
        {
            get => _Sum_DailySnowTotal;
            set
            {
                _Sum_DailySnowTotal = value;
                this.Ave_DailySnowTotal = value / this.DataCount;
            }
        }


        public double Ave_WindSpeedMax
        {
            get => _Ave_WindSpeedMax;
            set
            {
                _Ave_WindSpeedMax = value;
                this.Display_Ave_WindSpeedMax = _Ave_WindSpeedMax.ToString("N2");
            }
        }
        public double Ave_TemperatureMax
        {
            get => _Ave_TemperatureMax;
            set
            {
                _Ave_TemperatureMax = value;
                this.Display_Ave_TemperatureMax = _Ave_TemperatureMax.ToString("N2");
            }
        }
        public double Ave_TemperatureAve
        {
            get => _Ave_TemperatureAve;
            set
            {
                _Ave_TemperatureAve = value;
                this.Display_Ave_TemperatureAve = _Ave_TemperatureAve.ToString("N2");
            }
        }
        public double Ave_TemperatureMin
        {
            get => _Ave_TemperatureMin;
            set
            {
                _Ave_TemperatureMin = value;
                this.Display_Ave_TemperatureMin = _Ave_TemperatureMin.ToString("N2");
            }
        }
        public double Ave_RainTotal
        {
            get => _Ave_RainTotal;
            set
            {
                _Ave_RainTotal = value;
                this.Display_Ave_RainTotal = _Ave_RainTotal.ToString("N2");
            }
        }
        public double Ave_99RainTotal
        {
            get => _Ave_99RainTotal;
            set
            {
                _Ave_99RainTotal = value;
                this.Display_Ave_99RainTotal = _Ave_99RainTotal.ToString("N2");
            }
        }
        public double Ave_SnowTotal
        {
            get => _Ave_SnowTotal;
            set
            {
                _Ave_SnowTotal = value;
                this.Display_Ave_SnowTotal = _Ave_SnowTotal.ToString("N2");
            }
        }
        public double Ave_DailySnowTotal
        {
            get => _Ave_DailySnowTotal;
            set
            {
                _Ave_DailySnowTotal = value;
                this.Display_Ave_DailySnowTotal = _Ave_DailySnowTotal.ToString("N2");
            }
        }



        public object Dispaly_Month
        {
            get => _Display_Month;
            set
            {
                _Display_Month = value;
                OnPropertyChanged(nameof(this.Dispaly_Month));

            }
        }
        public object Dispaly_Day
        {
            get => _Display_Day;
            set
            {
                _Display_Day = value;
                OnPropertyChanged(nameof(this.Dispaly_Day));

            }
        }

        public object Display_Ave_WindSpeedMax
        {
            get => _Display_Ave_WindSpeedMax;
            set
            {
                _Display_Ave_WindSpeedMax = value;
                OnPropertyChanged(nameof(Display_Ave_WindSpeedMax));
            }
        }
        public object Display_Ave_TemperatureMax
        {
            get => _Display_Ave_TemperatureMax;
            set
            {
                _Display_Ave_TemperatureMax = value;
                OnPropertyChanged(nameof(Display_Ave_TemperatureMax));
            }
        }
        public object Display_Ave_TemperatureAve
        {
            get => _Display_Ave_TemperatureAve;
            set
            {
                _Display_Ave_TemperatureAve = value;
                OnPropertyChanged(nameof(Display_Ave_TemperatureAve));
            }
        }
        public object Display_Ave_TemperatureMin
        {
            get => _Display_Ave_TemperatureMin;
            set
            {
                _Display_Ave_TemperatureMin = value;
                OnPropertyChanged(nameof(Display_Ave_TemperatureMin));
            }
        }
        public object Display_Ave_RainTotal
        {
            get => _Display_Ave_RainTotal;
            set
            {
                _Display_Ave_RainTotal = value;
                OnPropertyChanged(nameof(Display_Ave_RainTotal));
            }
        }
        public object Display_Ave_99RainTotal
        {
            get => _Display_Ave_99RainTotal;
            set
            {
                _Display_Ave_99RainTotal = value;
                OnPropertyChanged(nameof(Display_Ave_99RainTotal));
            }
        }
        public object Display_Ave_SnowTotal
        {
            get => _Display_Ave_SnowTotal;
            set
            {
                _Display_Ave_SnowTotal = value;
                OnPropertyChanged(nameof(Display_Ave_SnowTotal));
            }
        }
        public object Display_Ave_DailySnowTotal
        {
            get => _Display_Ave_DailySnowTotal;
            set
            {
                _Display_Ave_DailySnowTotal = value;
                OnPropertyChanged(nameof(Display_Ave_DailySnowTotal));
            }
        }
    }

    public partial class vmWeatherByDate
    {
        private void DailyChildrenOrigin_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    if (e.NewItems == null) return;

                    this.DataCount = this.DailyChildren.Count();
                    foreach (mPublicDailyWeatherData item in e.NewItems)
                    {
                        this.Sum_WindSpeedMax += item.WindSpeed_Max;
                        this.Sum_TemperatureMax += item.Temperature_Max;
                        this.Sum_TemperatureAve += item.Temperature_Ave;
                        this.Sum_TemperatureMin += item.Temperature_Min;
                        this.Sum_RainTotal += item.Rain_Total;
                        this.Sum_99RainTotal += item.Rain_99Total;
                        this.Sum_SnowTotal += item.Snow_Total_Depth;
                        this.Sum_DailySnowTotal += item.Snow_Daily_Total_Depth;
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }
        }



        public void AddChild(mPublicDailyWeatherData item)
        {
            this.DailyChildrenOrigin.Add(item);
        }

    }
}
