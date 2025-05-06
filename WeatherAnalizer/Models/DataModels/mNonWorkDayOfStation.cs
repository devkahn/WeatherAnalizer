using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAnalizer.Models.DataModels
{
    public class mNonWorkDayOfStation
    {
        public int Idx { get; set; } = -1;
        public Guid Guid { get; set; } = new Guid();
        public DateTime RegDate { get; set; } = DateTime.Now;
        public DateTime? EditDate { get; set; } = null;
        public DateTime? DeleteDate { get; set; } = null;
        public bool IsUsed { get; set; } = true;
        public int SiteHoliDayIdx { get; set; } = -1;
        public int StationCode { get; set; } = -1;
        public int HotSummerDay { get; set; } = 0;
        public int HeavyRainyDay { get; set; } = 0;
        public int StrongWinterDay { get; set; } = 0;
        public int HardWindDay { get; set; } = 0;
        public int SnowDepth { get; set; } = 0;


    }
}
