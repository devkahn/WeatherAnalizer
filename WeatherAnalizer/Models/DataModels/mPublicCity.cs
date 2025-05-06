using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAnalizer.Models.DataModels
{
    public class mPublicCity : mBaseModel
    {
        public string Nick { get; set; } = string.Empty;
        public int ParentCityIdx { get; set; } = -1;
        public mPublicAdminDistrict Disctrict { get; set; } = null;

    }
}
