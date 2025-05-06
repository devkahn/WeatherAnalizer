using DLWIZ.Defines.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAnalizer.Models.DataModels
{
    public class mPublicWeatherStation
    {
        [ColumnHeader("Idx")]
        public int Idx { get; set; } = -1;
        [ColumnHeader("StationCode")]
        public int StationCode { get; set; } = -1;
        [ColumnHeader("StationName")]
        public string StationName { get; set; } = string.Empty;
        [ColumnHeader("StationAddress")]
        public string StationAddress { get; set; } = string.Empty;
        [ColumnHeader("Latitude")]
        public double Latitude { get; set; } = 0;
        [ColumnHeader("longitude")]
        public double Longtitude { get; set; } = 0;

        [ColumnHeader("Altitude")]
        public double Altitude { get; set; } = 0;

        public mPublicCity MetropolisCity { get; set; } = null;
    }
}
