using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.TextFormatting;
using WeatherAnalizer.Models.DataModels;

namespace WeatherAnalizer.Models.ViewModels
{
    public partial class vmWeatherAnalizeSetting
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

        private bool _Display_IsMaxTemperature = true;
        private double _Display_MaxTemperature = 35;
        private bool _Display_IsHotSummerRange = true;
        private int _Display_HotSummerFromMonth = 6;
        private int _Display_HotSummerFromTo = 8;

        private bool _DisplayIsMinTemperature = true;
        private double _Display_MinTemperature = -5;
        private bool _Display_IsStringWinterRange = true;
        private int _Display_StrongWinterFromMonth = 12;
        private int _Display_StrongWinterFromTo = 2;

        private double _Display_RainAmount = 10;
        private double _Display_SnowDepth = 10;
        private double _Display_MaxWindSpeed = 10;

        private string _Display_Address = string.Empty;
        private bool _Display_StationCount_All = false;
        private bool _Display_StationCount_10 =true;
        private bool _Display_StationCount_5 = false;
        private bool _Display_StationCount_User = false;
        private int _Display_StationCount = 10;

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

        public bool IsAddressValid { get; set; }



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

        public bool Display_IsMaxTemperature
        {
            get => _Display_IsMaxTemperature;
            set
            {
                _Display_IsMaxTemperature = value;  
                this.Data.IsMaxTemperature = value;
            }
        }
        public double Display_MaxTemperature
        {
            get => _Display_MaxTemperature;
            set
            {
                _Display_MaxTemperature = value;
                this.Data.MaxTemperature = value;
            }
        }
        public bool Display_IsHotSummerRange
        {
            get => _Display_IsHotSummerRange;
            set
            {
                _Display_IsHotSummerRange = value;
                this.Data.IsHotSummerRange = value;
            }
        }
        public int Display_HotSummerFromMonth
        {
            get => _Display_HotSummerFromMonth;
            set
            {
                _Display_HotSummerFromMonth = value;
                this.Data.HotSummerFromMonth = value;
            }
        }
        public int Display_HotSummerFromTo
        {
            get => _Display_HotSummerFromTo;
            set
            {
                _Display_HotSummerFromTo = value;
                this.Data.HotSummerToMonth = value;
            }
        }
        
        public bool Display_IsMinTemperature
        {
            get => _DisplayIsMinTemperature;
            set
            {
                _DisplayIsMinTemperature = value;
                this.Data.IsMinTemperature = value;
            }
        }
        public double Display_MinTemperature
        {
            get => _Display_MinTemperature;
            set
            {
                _Display_MinTemperature = value;
                this.Data.MinTemperature = value;
            }
        }
        public bool Display_IsStringWinterRange
        {
            get => _Display_IsStringWinterRange;
            set
            {
                _Display_IsStringWinterRange = value;
                this.Data.IsStrongWinter = value;
            }
        }
        public int Display_StrongWinterFromMonth
        {
            get => _Display_StrongWinterFromMonth;
            set
            {
                _Display_StrongWinterFromMonth = value;
                this.Data.StrongWinterFrom = value;
            }
        }
        public int Display_StrongWinterFromTo
        {
            get => _Display_StrongWinterFromTo;
            set
            {
                _Display_StrongWinterFromTo = value;
                this.Data.StrongWinterTo = value;   
            }
        }
        
        public double Display_RainAmount
        {
            get => _Display_RainAmount;
            set
            {
                _Display_RainAmount = value;
                this.Data.RainAmount = value;
            }
        }
        public double Display_SnowDepth
        {
            get => _Display_SnowDepth;
            set
            {
                _Display_SnowDepth = value;
                this.Data.SnowDepth = value;
            }
        }
        public double Display_MaxWindSpeed
        {
            get => _Display_MaxWindSpeed;
            set
            {
                _Display_MaxWindSpeed = value;
                this.Data.MaxWindSpeed = value;
            }
        }



        public string Display_Address
        {
            get => _Display_Address;
            set
            {
                _Display_Address = value;
                this.Data.Address = value;
            }
        }
        public bool Display_StationCount_All
        {
            get => _Display_StationCount_All;
            set
            {
                _Display_StationCount_All = value;
                this.Data.StationCountHashCode = -1;
            }
        }
        public bool Display_StationCount_10
        {
            get => _Display_StationCount_10;
            set
            {
                _Display_StationCount_10 = value;
                this.Data.StationCountHashCode = 510;
            }
        }
        public bool Display_StationCount_5
        {
            get => _Display_StationCount_5;
            set
            {
                _Display_StationCount_5 = value;
                this.Data.StationCountHashCode = 505;
            }
        }
        public bool Display_StationCount_User
        {
            get => _Display_StationCount_User;
            set
            {
                _Display_StationCount_User = value;
                this.Data.StationCountHashCode = 999;
            }
        }

        public int Display_StationCount
        {
            get => _Display_StationCount;
            set
            {
                _Display_StationCount = value;
                this.Data.StationCount = value;
            }
        }
    }

    public partial class vmWeatherAnalizeSetting
    {
        public void CheckAddressValid(mAddress address)
        {
            this.IsAddressValid = address != null;
            OnPropertyChanged(nameof(this.IsAddressValid));
        }
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

                this.Display_IsMaxTemperature = this.Data.IsMaxTemperature;
                this.Display_MaxTemperature = this.Data.MaxTemperature;
                this.Display_IsHotSummerRange = this.Data.IsHotSummerRange;
                this.Display_HotSummerFromMonth = this.Data.HotSummerFromMonth;
                this.Display_HotSummerFromTo = this.Data.HotSummerToMonth;

                this.Display_IsMinTemperature = this.Data.IsMinTemperature;
                this.Display_MinTemperature = this.Data.MinTemperature;
                this.Display_IsStringWinterRange = this.Data.IsStrongWinter;
                this.Display_StrongWinterFromMonth = this.Data.StrongWinterFrom;
                this.Display_StrongWinterFromTo = this.Data.StrongWinterTo;

                this.Display_RainAmount = this.Data.RainAmount;
                this.Display_SnowDepth = this.Data.SnowDepth;
                this.Display_MaxWindSpeed = this.Data.MaxWindSpeed;

                this.Display_Address = this.Data.Address;

                this.Display_StationCount_All = this.Data.StationCountHashCode == -1;
                this.Display_StationCount_10 = this.Data.StationCountHashCode == 510;
                this.Display_StationCount_5 = this.Data.StationCountHashCode == 505;
                this.Display_StationCount_User = this.Data.StationCountHashCode == 999;
                this.Display_StationCount = 10;
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
