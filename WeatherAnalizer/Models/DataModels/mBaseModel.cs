using DLWIZ.Defines.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAnalizer.Models.DataModels
{
    public class mBaseModel :mDataBase
    {
        [ColumnHeader("Name")]
        public string Name { get; set; } = string.Empty;


        public string SubName { get; set; } = string.Empty;


        public string GetFullName()
        {
            string text = Name;
            if (!string.IsNullOrEmpty(SubName) && !string.IsNullOrWhiteSpace(SubName))
            {
                text += " (";
                text += SubName;
                text += ")";
            }

            return text;
        }
    }
}
