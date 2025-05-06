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
    }
}
