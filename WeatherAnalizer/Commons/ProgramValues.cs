using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAnalizer.Models.ViewModels;

namespace WeatherAnalizer.Commons
{
    public static class ProgramValues
    {
        public static List<vmPublicWeatherStation> Stations { get; set; } = new List<vmPublicWeatherStation>();

        public static vmWeatherAnalizeSetting AnalizeSetting { get; set; }
    }
}
