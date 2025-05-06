using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAnalizer.Models.DataModels
{
    public class mPublicAdminDistrict : mBaseModel
    {
        public string CodeString { get; set; } = string.Empty;
        public int ParentIdx { get; set; } = -1;
        public int DataLevel { get; set; } = -1;

    }
}
