using DLWIZ.Defines.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAnalizer.Models.DataModels
{
    public class mDataBase
    {
        [ColumnHeader("Idx")]
        public int Idx { get; set; } = -1;


        [ColumnHeader("Guid")]
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();


        [ColumnHeader("RegDate")]
        public DateTime RegDate { get; set; } = DateTime.Now;


        [ColumnHeader("EditDate")]
        public DateTime? EditDate { get; set; } = null;


        [ColumnHeader("DeleteDate")]
        public DateTime? DeleteDate { get; set; } = null;


        [ColumnHeader("IsUsed")]
        public bool IsUsed { get; set; } = true;

    }
}
