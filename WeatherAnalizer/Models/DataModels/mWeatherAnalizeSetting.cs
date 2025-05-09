using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAnalizer.Models.DataModels
{
    public class mWeatherAnalizeSetting : mBaseModel
    {
        public int YearRangeHashCode { get; set; } = 105;
        public int YearRangeFrom { get; set; } = DateTime.Now.Year-5;
        public int YearRangeTo { get; set; } = DateTime.Now.Year-2;

        public int MonthRangeHaseCode { get; set; } = -1;
        public int MonthRangeFrom { get; set; } = 1;
        public int MonthRangeTo { get; set; } = 11;

        public int HourRangeHaseCode { get; set; } = 105;
        public int HourRangeFrom { get; set; } = 7;
        public int HourRangeTo { get; set; } = 18;


        public bool IsMaxTemperature { get; set; } = true;
        public double MaxTemperature { get; set; } = 35;
        public bool IsHotSummerRange { get; set; } = true;
        public int HotSummerFromMonth { get; set; } = 6;
        public int HotSummerToMonth { get; set; } = 8;

        public bool IsMinTemperature { get; set; } = true;
        public double MinTemperature { get; set; } = -5;
        public bool IsStrongWinter { get; set; } = true;
        public int StrongWinterFrom { get; set; } = 12;
        public int StrongWinterTo { get; set; } = 2;

        public double RainAmount { get; set; } = 10;
        public double SnowDepth { get; set; } = 10;
        public double MaxWindSpeed { get; set; } = 10;

        
        public string Address { get; set; } = string.Empty;
        public int StationCountHashCode { get; set; } = 510;
        public int StationCount { get; set; } = 10;
    }
}
