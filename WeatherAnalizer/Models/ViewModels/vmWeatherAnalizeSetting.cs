using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using WeatherAnalizer.Models.DataModels;

namespace WeatherAnalizer.Models.ViewModels
{
    public partial class vmWeatherAnalizeSetting :vmBaseViewModel
    {
        private mWeatherAnalizeSetting _Origin = null;
        private mWeatherAnalizeSetting _Data = null;

        private bool _Display_YearRange_All = false;
        private bool _Display_YearRange_5 = false;
        private bool _Display_YearRange_3 = false;
        private bool _Display_YearRange_User = false;
        private int _Display_YearRange_From = 2015;
        private int _Display_YearRange_To = 2020;

        private bool _Display_MonthRange_All = true;
        private bool _Display_MonthRange_Summer = false;
        private bool _Display_MonthRange_Winter = false;
        private bool _Display_MonthRange_User = false;
        private int _Display_MonthRange_From = 1;
        private int _Display_MonthRange_To = 12;

        private bool _Display_HourRange_All = false;
        private bool _Display_HourRange_Working = true;
        private bool _Display_HourRange_User = false;
        private int _Display_HourRange_From = 1;
        private int _Display_HourRange_To = 12;

        private bool _Display_StandardSetting = true;

    }



    public partial class vmWeatherAnalizeSetting : vmBaseViewModel
    {
        public vmWeatherAnalizeSetting(mWeatherAnalizeSetting origin) 
        {
            this.Origin = origin;   
        }

        private mWeatherAnalizeSetting Origin
        {
            get => _Origin;
            set
            {
                _Origin = value;
                this.Data = _Origin;
            }
        }
        public mWeatherAnalizeSetting Data
        {
            get => _Data;
            private set
            {
                _Data = value;
                InitializeDisplay();
            }
        }


        public bool Display_YearRange_All
        {
            get => _Display_YearRange_All;
            set
            {
                _Display_YearRange_All = value;
                if (value == true) this.Data.YearRangeHashCode = -1;
            }
        }
        public bool Display_YearRange_5
        {
            get => _Display_YearRange_5;
            set
            {
                _Display_YearRange_5 = value;
                if (value == true) this.Data.YearRangeHashCode = 105;
            }
        }
        public bool Display_YearRange_3
        {
            get => _Display_YearRange_3;
            set
            {
                _Display_YearRange_3 = value;
                if (value == true) this.Data.YearRangeHashCode = 103;
            }
        }
        public bool Display_YearRange_User
        {
            get => _Display_YearRange_User;
            set
            {
                _Display_YearRange_User = value;
                if (value == true) this.Data.YearRangeHashCode = 999;
            }
        }
        public int Display_YearRange_From
        {
            get => _Display_YearRange_From;
            set
            {
                _Display_YearRange_From = value;
                this.Data.YearRangeFrom = value;
            }
        }
        public int Display_YearRange_To
        {
            get => _Display_YearRange_To;
            set
            {
                _Display_YearRange_To = value;
                this.Data.YearRangeTo = value;
            }
        }

        public bool Display_MonthRange_All
        {
            get => _Display_MonthRange_All;
            set
            {
                _Display_MonthRange_All = value;
                if (value == true) this.Data.MonthRangeHaseCode = -1;
            }
        }
        public bool Display_MonthRange_Summer
        {
            get => _Display_MonthRange_Summer;
            set
            {
                _Display_MonthRange_Summer = value;
                if (value == true) this.Data.MonthRangeHaseCode = 101;
            }
        }
        public bool Display_MonthRange_Winter
        {
            get => _Display_MonthRange_Winter;
            set
            {
                _Display_MonthRange_Winter = value;
                if (value == true) this.Data.MonthRangeHaseCode = 201;
            }
        }
        public bool Display_MonthRange_User
        {
            get => _Display_MonthRange_User;
            set
            {
                _Display_MonthRange_User = value;
                if (value == true) this.Data.MonthRangeHaseCode = 999;
            }
        }
        public int Display_MonthRange_From
        {
            get => _Display_MonthRange_From;
            set
            {
                _Display_MonthRange_From = value;
                this.Data.MonthRangeFrom = value;
            }
        }
        public int Display_MonthRange_To
        {
            get => _Display_MonthRange_To;
            set
            {
                _Display_MonthRange_To = value;
                this.Data.MonthRangeTo = value;
            }
        }

        public bool Display_HourRange_All
        {
            get => _Display_HourRange_All;
            set
            {
                _Display_HourRange_All = value;
                if (value == true) this.Data.HourRangeHaseCode = -1;
            }
        }
        public bool Display_HourRange_Working
        {
            get => _Display_HourRange_Working;
            set
            {
                _Display_HourRange_Working = value;
                if (value == true) this.Data.HourRangeHaseCode = 105;
            }
        }
        public bool Display_HourRange_User
        {
            get => _Display_HourRange_User;
            set
            {
                _Display_HourRange_User = value;
                if (value == true) this.Data.HourRangeHaseCode = 999;
            }
        }
        public int Display_HourRange_From
        {
            get => _Display_HourRange_From;
            set
            {
                _Display_HourRange_From = value;
                this.Data.HourRangeFrom = value;
            }
        }
        public int Display_HourRange_To
        {
            get => _Display_HourRange_To;
            set
            {
                _Display_HourRange_To = value;
                this.Data.HourRangeTo = value;
            }
        }


        public bool Display_StandardSetting
        {
            get => _Display_StandardSetting;
            set
            {
                _Display_StandardSetting = value;
            }
        }

    }

    public partial class vmWeatherAnalizeSetting
    {
        public override void InitializeDisplay()
        {
            if(this.Data != null)
            {
                this.Display_YearRange_All = this.Data.YearRangeHashCode == -1;
                this.Display_YearRange_3 = this.Data.YearRangeHashCode == 103;
                this.Display_YearRange_5 = this.Data.YearRangeHashCode == 105;
                this.Display_YearRange_User = this.Data.YearRangeHashCode == 999;
                this.Display_YearRange_From = this.Data.YearRangeFrom;
                this.Display_YearRange_To = this.Data.YearRangeTo;

                this.Display_MonthRange_All = this.Data.MonthRangeHaseCode == -1;
                this.Display_MonthRange_Summer = this.Data.MonthRangeHaseCode == 101;
                this.Display_MonthRange_Winter = this.Data.MonthRangeHaseCode == 201;
                this.Display_MonthRange_User = this.Data.MonthRangeHaseCode == 999;
                this.Display_MonthRange_From = this.Data.MonthRangeFrom;
                this.Display_MonthRange_To = this.Data.MonthRangeTo;

                this.Display_HourRange_All = this.Data.HourRangeHaseCode == -1;
                this.Display_HourRange_Working = this.Data.HourRangeHaseCode == 105;
                this.Display_HourRange_User = this.Data.HourRangeHaseCode == 999;
                this.Display_HourRange_From = this.Data.HourRangeFrom;
                this.Display_HourRange_To = this.Data.HourRangeTo;

            }
            
        }

        public override void SetInitialData()
        {

        }

        public override object UpdateOriginData()
        {
            return null;
        }
    }

}
