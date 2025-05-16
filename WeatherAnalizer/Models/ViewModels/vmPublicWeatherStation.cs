
using DLWIZ.Helpers.Commons;
using DLWIZ.ViewModels.Commons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shell;
using WeatherAnalizer.Helpers;
using WeatherAnalizer.Models.DataModels;
using SiteHelper = WeatherAnalizer.Helpers.SiteHelper;

namespace WeatherAnalizer.Models.ViewModels
{
    public partial class vmPublicWeatherStation
    {
        private mPublicWeatherStation _Origin = null;
        private mNonWorkDayOfStation _NonWorkDayOrigin = null;

        private double _DistanceFromSite = 0;

        private int _RainyHoliday = 0;
        private int _HotSummerDay = 0;
        private int _StrongWinterDay = 0;
        private int _HeavyWindDay = 0;
        private int _SnowHoliday = 0;

        private object _Display_DistanceFromSite = "-";

        private object _Display_Name = null;
        private object _Display_RainyDay = "-";
        private object _Display_HotSummerDay = "-";
        private object _Display_StrongWinterDay = "-";
        private object _Display_WindDay = "-";
        private object _Display_SnowDay = "-";


    }
    public partial class vmPublicWeatherStation : vmBaseModel
    {
        public vmPublicWeatherStation(mPublicWeatherStation origin)
        {
            this.Origin = origin;
        }


        public mPublicWeatherStation Origin
        {
            get => _Origin;
            set
            {
                _Origin = value;
                this.Display_Name = value.StationName;
                mGeoCoordinate newGeo = new mGeoCoordinate();
                newGeo.Latitude = value.Latitude;
                newGeo.Longitude = value.Longtitude;
                this.GeoCoorinate = newGeo;
                OnPropertyChanged(nameof(Origin));
            }

        }
        public mNonWorkDayOfStation NonWorkDayOrigin
        {
            get => _NonWorkDayOrigin;
            set
            {
                _NonWorkDayOrigin = value;
                if (value == null) return;

                this.RainyHoliDay = value.HeavyRainyDay;
                this.HotSummerDay = value.HotSummerDay;
                this.StrongWinterDay = value.StrongWinterDay;
                this.HeavyWindDay = value.HardWindDay;
                this.SnowHoliDay = value.SnowDepth;
            }
        }
        public vmSiteLevel Parent { get; set; } = null;
        public mGeoCoordinate GeoCoorinate { get; set; } = null;
        public FileInfo WeatherDataDBFilePath { get; set; } = null;


        public List<mPublicHourlyWeatherData> WeatherData { get; private set; } = new List<mPublicHourlyWeatherData>();


        public double DistanceFromSite
        {
            get => _DistanceFromSite;
            set
            {
                _DistanceFromSite = value;
                this.Display_DistanceFromSite = Math.Round(value, 2).ToString("N2");
            }
        }
        public int RainyHoliDay
        {
            get => _RainyHoliday;
            set
            {
                _RainyHoliday = value;
                this.Display_RainyDay = value.ToString("N0");
            }
        }
        public int HotSummerDay
        {
            get => _HotSummerDay;
            set
            {
                _HotSummerDay = value;
                this.Display_HotSummerDay = value.ToString("N0");
            }
        }
        public int StrongWinterDay
        {
            get => _StrongWinterDay;
            set
            {
                _StrongWinterDay = value;
                this.Display_StrongWinterDay = value.ToString("N0");
            }
        }
        public int HeavyWindDay
        {
            get => _HeavyWindDay;
            set
            {
                _HeavyWindDay = value;
                this.Display_HeavyWindDay = value.ToString("N0");
            }
        }
        public int SnowHoliDay
        {
            get => _SnowHoliday;
            set
            {
                _SnowHoliday = value;
                this.Display_SnowDay = value.ToString("N0");
            }
        }



        public object Display_DistanceFromSite
        {
            get => _Display_DistanceFromSite;
            private set
            {
                _Display_DistanceFromSite = value;
                OnPropertyChanged(nameof(this.Display_DistanceFromSite));
            }
        }

        public object Display_Name
        {
            get => _Display_Name;
            private set
            {
                _Display_Name = value;
                OnPropertyChanged(nameof(this.Display_Name));
            }
        }
        public object Display_RainyDay
        {
            get => _Display_RainyDay;
            private set
            {
                _Display_RainyDay = value;
                OnPropertyChanged(nameof(Display_RainyDay));
            }
        }
        public object Display_HotSummerDay
        {
            get => _Display_HotSummerDay;
            private set
            {
                _Display_HotSummerDay = value;
                OnPropertyChanged(nameof(Display_HotSummerDay));
            }
        }
        public object Display_StrongWinterDay
        {
            get => _Display_StrongWinterDay;
            private set
            {
                _Display_StrongWinterDay = value;
                OnPropertyChanged(nameof(Display_StrongWinterDay));
            }
        }
        public object Display_HeavyWindDay
        {
            get => _Display_WindDay;
            private set
            {
                _Display_WindDay = value;
                OnPropertyChanged(nameof(Display_HeavyWindDay));
            }
        }
        public object Display_SnowDay
        {
            get => _Display_SnowDay;
            private set
            {
                _Display_SnowDay = value;
                OnPropertyChanged(nameof(this.Display_SnowDay));
            }
        }
    }
    public partial class vmPublicWeatherStation
    {
        public bool HasWeatherData() => this.WeatherData.Any();
        public void SetWeatherData(vmWeatherAnalizeSetting setting)
        {
            List<mPublicHourlyWeatherData> data = SiteHelper.ReadWeatherDataByStation(this, setting);
            this.WeatherData = data;
        }
        internal void Calculation(vmWeatherAnalizeSetting setting)
        {
            List<mPublicDailyWeatherData> weathers = new List<mPublicDailyWeatherData>();

            DateTime startTime = this.WeatherData.First().ObserveTime;
            DateTime endTime = this.WeatherData.Last().ObserveTime;

            int index = 0;
            for (DateTime date = startTime; date <= endTime; date = date.AddDays(1))
            {
                List<mPublicHourlyWeatherData> tempList = new List<mPublicHourlyWeatherData>();
                mPublicHourlyWeatherData dayItem = this.WeatherData[index];
                bool sameDate = dayItem.ObserveTime.Year == date.Year && dayItem.ObserveTime.Month == date.Month && dayItem.ObserveTime.Day == date.Day;
                while (sameDate)
                {
                    tempList.Add(dayItem);
                    index++;

                    if (this.WeatherData.Count == index) break;

                    dayItem = this.WeatherData[index];
                    sameDate = dayItem.ObserveTime.Year == date.Year && dayItem.ObserveTime.Month == date.Month && dayItem.ObserveTime.Day == date.Day;
                }


                mPublicDailyWeatherData newItem = new mPublicDailyWeatherData();
                newItem.DateYear = date.Year;
                newItem.DateMonth = date.Month;
                newItem.DateDay = date.Day;
                //newItem.StationCode = tempList.First().StationCode;
                #region 최대 풍속

                mPublicHourlyWeatherData maxWindItem = tempList.OrderBy(x => x.WindSpeed).Last();
                newItem.WindSpeed_Max = maxWindItem.WindSpeed.HasValue ? maxWindItem.WindSpeed.Value : -99;
                newItem.WindSpeed_Max_Time = int.Parse(maxWindItem.ObserveTime.ToString("HHmm"));
                newItem.WindDirection_Max = maxWindItem.WindDirection.HasValue ? maxWindItem.WindDirection.Value : -99;

                #endregion
                #region 기온

                newItem.Temperature_Ave = tempList.Sum(x => x.TemperatureAtomospheric).Value / tempList.Count();

                mPublicHourlyWeatherData maxTempITem = tempList.OrderBy(x => x.TemperatureAtomospheric).Last();
                newItem.Temperature_Max = maxTempITem.TemperatureAtomospheric.HasValue ? maxTempITem.TemperatureAtomospheric.Value : -99;
                newItem.Temperature_Max_Time = int.Parse(maxTempITem.ObserveTime.ToString("HHmm"));

                mPublicHourlyWeatherData minTempItem = tempList.OrderBy(x => x.TemperatureAtomospheric).First();
                newItem.Temperature_Min = minTempItem.TemperatureAtomospheric.HasValue ? minTempItem.TemperatureAtomospheric.Value : -99;
                newItem.Temperature_Min_Time = int.Parse(minTempItem.ObserveTime.ToString("HHmm"));

                #endregion
                #region 강수량

                double rainfall = 0;

                for (int i = 0; i < tempList.Count() - 1; i++)
                {
                    double valuePrev = 0;
                    bool hasValuePrev = tempList[i].RainfallOfDayByStatics.HasValue;
                    if (hasValuePrev) valuePrev = tempList[i].RainfallOfDayByStatics.Value;

                    double valueNext = 0;
                    bool hasValueNext = tempList[i + 1].RainfallOfDayByStatics.HasValue;
                    if (hasValueNext) valueNext = tempList[i + 1].RainfallOfDayByStatics.Value;

                    if (hasValueNext && hasValuePrev) rainfall += (valueNext - valuePrev);
                }

                if (rainfall != 0)
                {

                }

                newItem.Rain_Total = rainfall;
                newItem.Rain_99Total = rainfall;

                #endregion
                #region 누적 적설량

                double snowDepth = 0;

                for (int i = 0; i < tempList.Count() - 1; i++)
                {
                    double valuePrev = 0;
                    bool hasValuePrev = tempList[i].SnowDepthOfDay.HasValue;
                    if (hasValuePrev) valuePrev = tempList[i].SnowDepthOfDay.Value;

                    double valueNext = 0;
                    bool hasValueNext = tempList[i + 1].SnowDepthOfDay.HasValue;
                    if (hasValueNext) valueNext = tempList[i + 1].SnowDepthOfDay.Value;

                    if (hasValueNext && hasValuePrev) snowDepth += (valueNext - valuePrev);
                }

                if (snowDepth != 0)
                {

                }

                newItem.Snow_Total_Depth = snowDepth;
                newItem.Snow_Daily_Total_Depth = snowDepth;

                #endregion

                weathers.Add(newItem);
            }

            List<vmWeatherByDate> standards = new List<vmWeatherByDate>();
            foreach (mPublicDailyWeatherData item in weathers)
            {
                vmWeatherByDate sameStandard = standards.Where(x => x.Date.Month == item.DateMonth && x.Date.Day == item.DateDay).FirstOrDefault();
                if (sameStandard == null)
                {
                    sameStandard = new vmWeatherByDate(item);
                    standards.Add(sameStandard);
                }
                else
                {
                    sameStandard.AddChild(item);
                }
            }

            if(true)
            {

            }
        }
    }
}
