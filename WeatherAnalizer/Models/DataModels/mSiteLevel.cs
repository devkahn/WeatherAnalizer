using DLWIZ.Defines.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAnalizer.Models.ViewModels;

namespace WeatherAnalizer.Models.DataModels
{
    public class mSiteLevel : mBaseModel
    {
        [ColumnHeader("LevelOrder")]
        public int LevelOrder { get; set; } = -1;

        public List<mPublicWeatherStation> Stations { get; set; } = new List<mPublicWeatherStation>();
    }
}
