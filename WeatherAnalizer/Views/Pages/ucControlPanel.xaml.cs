using DLWIZ.Helpers.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherAnalizer.Commons;
using WeatherAnalizer.Helpers;
using WeatherAnalizer.Models.DataModels;
using WeatherAnalizer.Models.ViewModels;
using static System.Collections.Specialized.BitVector32;
using Path = System.IO.Path;

namespace WeatherAnalizer.Views.Pages
{
    /// <summary>
    /// ucControlPanel.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucControlPanel : UserControl
    {
        private List<ComboBoxItem> StartYears { get; set; } = new List<ComboBoxItem>();
        private List<ComboBoxItem> FinishYears { get; set; } = new List<ComboBoxItem>();
        private List<ComboBoxItem> StartMonths { get; set; } = new List<ComboBoxItem>();
        private List<ComboBoxItem> FinishMonths { get; set; } = new List<ComboBoxItem>();
        private List<ComboBoxItem> StartHours { get; set; } = new List<ComboBoxItem>();
        private List<ComboBoxItem> FinishHours { get; set; } = new List<ComboBoxItem>();
        private List<ComboBoxItem> StartSummer { get; set; } = new List<ComboBoxItem>();
        private List<ComboBoxItem> FinishSummer { get; set; } = new List<ComboBoxItem>();
        private List<ComboBoxItem> StartWinter { get; set; } = new List<ComboBoxItem>();
        private List<ComboBoxItem> FinishWinter { get; set; } = new List<ComboBoxItem>();

        private bool IsAllStandardCheck { get; set; } = false;
        private bool IsSingleStandardCheck { get; set; } = false;
        private List<CheckBox> StandardCheckBoxes { get; set; } = new List<CheckBox>();

        private vmWeather _Weather = null;
        public vmWeather Weather
        {
            get => _Weather;
            set
            {
                _Weather = value;
                this.DataContext = value.Setting;
            }
        }


        public ucControlPanel()
        {
            InitializeComponent();
            InitializeData();
        }


        private bool HasStandard()
        {
            if (!this.StandardCheckBoxes.Any(x => x.IsChecked == true))
            {
                MessageHelper.ShowErrorMessage("날씨 기준 오류", "설정한 날씨 기준이 없습니다.\n날씨 기준을 확인하세요.");
                return false;
            }
            return true;
        }
        private void InitializeData()
        {
            InitializeYearCombo();
            InitializeMonthCombo();
            InitializeHourCombo();
            InitializeSummerCombo();
            InitializeWinterCombo();
            InitializeFieldData();
        }

 

        private void InitializeYearCombo()
        {
            int thisYear = DateTime.Now.Year;
            for (int i = 2015; i < thisYear; i++)
            {
                ComboBoxItem newItem = null;
                for (int j = 0; j < 2; j++)
                {
                    newItem = new ComboBoxItem();
                    newItem.Uid = i.ToString();
                    newItem.Content = i.ToString();

                    if (j == 0)
                    {
                        this.StartYears.Add(newItem);
                    }
                    else if (j == 1)
                    {
                        this.FinishYears.Add(newItem);
                    }
                }
            }

            this.combo_YearStart.ItemsSource = this.StartYears;
            this.combo_YearStart.SelectedValue = ProgramValues.WeatherData.Setting.Data.YearRangeFrom;
            this.combo_YearFinish.ItemsSource = this.FinishYears;
            this.combo_YearFinish.SelectedItem = ProgramValues.WeatherData.Setting.Data.YearRangeTo;
        }
        private void InitializeMonthCombo()
        {
            for (int i = 1; i <= 12; i++)
            {
                ComboBoxItem newItem = null;
                for (int j = 0; j < 2; j++)
                {
                    newItem = new ComboBoxItem();
                    newItem.Uid = i.ToString();
                    newItem.Content = i.ToString();

                    if (j == 0) this.StartMonths.Add(newItem);
                    else if (j == 1) this.FinishMonths.Add(newItem);
                }
            }

            this.combo_MonthStart.ItemsSource = this.StartMonths;
            this.combo_MonthStart.SelectedValue = ProgramValues.WeatherData.Setting.Data.MonthRangeFrom;
            this.combo_MonthFinish.ItemsSource = this.FinishMonths;
            this.combo_MonthFinish.SelectedValue = ProgramValues.WeatherData.Setting.Data.MonthRangeTo;
        }
        private void InitializeHourCombo()          
        {
            for (int i = 0; i <= 24; i++)
            {
                ComboBoxItem newItem = null;
                for (int j = 0; j < 2; j++)
                {
                    newItem = new ComboBoxItem();
                    newItem.Uid = i.ToString();
                    newItem.Content = i.ToString();

                    if (j == 0) this.StartHours.Add(newItem);
                    else if (j == 1) this.FinishHours.Add(newItem);
                }
            }

            this.combo_HourStart.ItemsSource = this.StartHours;
            this.combo_HourStart.SelectedValue = ProgramValues.WeatherData.Setting.Data.HourRangeFrom;
            this.combo_HourFinish.ItemsSource = this.FinishHours;
            this.combo_HourFinish.SelectedValue = ProgramValues.WeatherData.Setting.Data.HourRangeTo;
            
        }
        private void InitializeSummerCombo()
        {
            for (int i = 1; i <= 12; i++)
            {
                ComboBoxItem newItem = null;
                for (int j = 0; j < 2; j++)
                {
                    newItem = new ComboBoxItem();
                    newItem.Uid = i.ToString();
                    newItem.Content = i.ToString();

                    if (j == 0) this.StartSummer.Add(newItem);
                    else if (j == 1) this.FinishSummer.Add(newItem);
                }
            }

            this.combo_SummerStart.ItemsSource = this.StartSummer;
            this.combo_SummerStart.SelectedIndex = 6;
            this.combo_SummerFinish.ItemsSource = this.FinishSummer;
            this.combo_SummerFinish.SelectedIndex = 7;
        }
        private void InitializeWinterCombo()
        {
            for (int i = 1; i <= 12; i++)
            {
                ComboBoxItem newItem = null;
                for (int j = 0; j < 2; j++)
                {
                    newItem = new ComboBoxItem();
                    newItem.Uid = i.ToString();
                    newItem.Content = i.ToString();

                    if (j == 0) this.StartWinter.Add(newItem);
                    else if (j == 1) this.FinishWinter.Add(newItem);
                }
            }

            this.combo_WinterStart.ItemsSource = this.StartWinter;
            this.combo_WinterStart.SelectedIndex = 11;
            this.combo_WinterFinish.ItemsSource = this.FinishWinter;
            this.combo_WinterFinish.SelectedIndex = 1;
        }
        private void InitializeFieldData()
        {
            
        }
        private bool IsStandardValid()
        {
            bool output = true;

            if (!IsDataPeriodYearValid()) return false;
            if (!IsDataPeriodMonthValid()) return false;
            if (!IsDataPeriodHourValid()) return false;

            if (!HasStandard()) return false;
            if (this.checkbox_SummerPeriod.IsChecked == true && !IsStandardSummerValid()) return false;
            if (this.checkbox_WinterPeriod.IsChecked == true && !IsStandardWinterValid()) return false;
            if (this.checkbox_HighTemper.IsChecked == true && !IsStandardMaxTemper()) return false;
            if (this.checkbox_LowTemper.IsChecked == true && !IsStandardMinTemper()) return false;
            if (this.checkbox_Rainy.IsChecked == true && !IsStandardRainy()) return false;
            if (this.checkbox_Snow.IsChecked == true && !IsStandardSnowDepth()) return false;
            if (this.checkbox_MaxWind.IsChecked == true && !IsStandardWindSpeed()) return false;

            if (!IsAddressValid()) return false;
            if(!IsStationCountValid()) return false;

            return output;
        }

        private bool IsStationCountValid()
        {
            if (!int.TryParse(this.textbox_StationCount.Text, out int cnt))
            {
                MessageHelper.ShowErrorMessage("관측소 수 오류", "관측소 수 설정을 확인하세요.");
                return false;
            }
            return true;
        }

        private bool IsAddressValid()
        {
            this.Weather.Address = GetAddressInfo(this.Weather.Setting.Display_Address);
            if (this.Weather.Address == null)
            {
                MessageHelper.ShowErrorMessage("현장 주소 오류", "현장 주소 설정을 확인하세요.");
                return false;
            }
            return true;
        }

        private bool IsStandardWindSpeed()
        {
            if (!double.TryParse(this.txtbox_WIndSpeed.Text, out double value))
            {
                MessageHelper.ShowErrorMessage("날씨 기준 오류", "최대 풍속 설정을 확인하세요.");
                return false;
            }
            return true;
        }
        private bool IsStandardSnowDepth()
        {
            if (!double.TryParse(this.txtbox_SnowDepth.Text, out double value))
            {
                MessageHelper.ShowErrorMessage("날씨 기준 오류", "적설량 설정을 확인하세요.");
                return false;
            }
            return true;
        }
        private bool IsStandardRainy()
        {
            if (!double.TryParse(this.txtbox_Rainy.Text, out double value))
            {
                MessageHelper.ShowErrorMessage("날씨 기준 오류", "강우량 설정을 확인하세요.");
                return false;
            }
            return true;
        }
        private bool IsStandardMinTemper()
        {
            if (!double.TryParse(this.txtbox_MinTemper.Text, out double value))
            {
                MessageHelper.ShowErrorMessage("날씨 기준 오류", "최저 기온 설정을 확인하세요.");
                return false;
            }
            return true;
        }
        private bool IsStandardMaxTemper()
        {
            if (!double.TryParse(this.txtbox_MaxTemper.Text, out double value))
            {
                MessageHelper.ShowErrorMessage("날씨 기준 오류", "최고 기온 설정을 확인하세요.");
                return false;
            }
            return true;
        }
        private bool IsStandardWinterValid()
        {
            ComboBoxItem start = this.combo_WinterStart.SelectedItem as ComboBoxItem;
            ComboBoxItem finish = this.combo_WinterFinish.SelectedItem as ComboBoxItem;
            if (start == null || finish == null)
            {
                MessageHelper.ShowErrorMessage("날씨 기준 오류", "동절기 설정을 확인하세요.");
                return false;
            }
            return true;
        }
        private bool IsStandardSummerValid()
        {
            ComboBoxItem start = this.combo_SummerStart.SelectedItem as ComboBoxItem;
            ComboBoxItem finish = this.combo_SummerFinish.SelectedItem as ComboBoxItem;
            if (start == null || finish == null)
            {
                MessageHelper.ShowErrorMessage("날씨 기준 오류", "혹서기 설정을 확인하세요.");
                return false;
            }
            return true;
        }
        private bool IsDataPeriodHourValid()
        {
            ComboBoxItem start = this.combo_HourStart.SelectedItem as ComboBoxItem;
            ComboBoxItem finish = this.combo_HourFinish.SelectedItem as ComboBoxItem;
            if (start == null || finish == null)
            {
                MessageHelper.ShowErrorMessage("Data 기준 오류", "시간 설정을 확인하세요.");
                return false;
            }
            return true;
        }
        private bool IsDataPeriodMonthValid()
        {
            ComboBoxItem start = this.combo_MonthStart.SelectedItem as ComboBoxItem;
            ComboBoxItem finish = this.combo_MonthFinish.SelectedItem as ComboBoxItem;
            if (start == null || finish == null)
            {
                MessageHelper.ShowErrorMessage("Data 기준 오류", "월 설정을 확인하세요.");
                return false;
            }
            return true;
        }
        private bool IsDataPeriodYearValid()
        {
            ComboBoxItem start = this.combo_YearStart.SelectedItem as ComboBoxItem;
            ComboBoxItem finish = this.combo_YearFinish.SelectedItem as ComboBoxItem;
            if (start == null || finish == null)
            {
                MessageHelper.ShowErrorMessage("Data 기준 오류", "연도 설정을 확인하세요.");
                return false;
            }

            int startYear = int.Parse(start.Uid);
            int finishYear = int.Parse(finish.Uid);
            if (finishYear < startYear)
            {
                MessageHelper.ShowErrorMessage("Data 기준 오류", "연도 설정을 확인하세요.");
                return false;
            }

            return true;
        }
        private mAddress GetAddressInfo(string addressString)
        {
            mRequestGeocoordinate req = new mRequestGeocoordinate(addressString);
            mAddress address = AddressHelper.ReadGeoCoordinateFromAddressString(req);
            return address;
        }
        private List<vmPublicWeatherStation> SetStationListByDistance(mAddress address, List<vmPublicWeatherStation> stationList)
        {
            foreach (vmPublicWeatherStation station in stationList) station.DistanceFromSite = SiteHelper.CalculateDistance(address.GeoCooradinate, station.GeoCoorinate);
            return stationList.OrderBy(x =>  x.DistanceFromSite).ToList();
        }


        private void radio_Year_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton rBtn = sender as RadioButton;
                if (rBtn == null) return;

                bool isDigit = int.TryParse(rBtn.Uid, out int code);
                if (!isDigit) code = -1;

                this.combo_YearStart.IsEnabled = false;
                this.combo_YearFinish.IsEnabled = false;
                switch (code)
                {
                    case -1:
                        this.combo_YearStart.SelectedIndex = 0;
                        this.combo_YearFinish.SelectedIndex = this.FinishYears.Count - 1;
                        break;
                    case 105:
                        this.combo_YearStart.SelectedIndex = this.StartYears.Count -5;
                        this.combo_YearFinish.SelectedIndex = this.FinishYears.Count - 1;
                        
                        break;
                    case 103:
                        this.combo_YearStart.SelectedIndex = this.StartYears.Count - 3;
                        this.combo_YearFinish.SelectedIndex = this.FinishYears.Count - 1;
                        break; 
                    default:
                        this.combo_YearStart.IsEnabled = true;
                        this.combo_YearFinish.IsEnabled = true;
                        break;
                }
            }
            catch (Exception ee)
            {
                ErrorHelper.ShowErrorLog(ee, true);

            }
        }
        private void radio_Month_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton rBtn = sender as RadioButton;
                if (rBtn == null) return;

                bool isDigit = int.TryParse(rBtn.Uid, out int code);
                if (!isDigit) code = -1;

                this.combo_MonthStart.IsEnabled = false;
                this.combo_MonthFinish.IsEnabled = false;
                switch (code)
                {
                    case -1:
                        this.combo_MonthStart.SelectedIndex = 0;
                        this.combo_MonthFinish.SelectedIndex = 11;
                        break;
                    case 101:
                        this.combo_MonthStart.SelectedIndex = 6;
                        this.combo_MonthFinish.SelectedIndex = 8;

                        break;
                    case 201:
                        this.combo_MonthStart.SelectedIndex = 11;
                        this.combo_MonthFinish.SelectedIndex = 1;
                        break;
                    default:
                        this.combo_MonthStart.IsEnabled = true;
                        this.combo_MonthFinish.IsEnabled = true;
                        break;
                }
            }
            catch (Exception ee)
            {
                ErrorHelper.ShowErrorLog(ee, true);

            }
        }
        private void radio_Hour_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton rBtn = sender as RadioButton;
                if (rBtn == null) return;

                bool isDigit = int.TryParse(rBtn.Uid, out int code);
                if (!isDigit) code = -1;

                this.combo_HourStart.IsEnabled = false;
                this.combo_HourFinish.IsEnabled = false;
                switch (code)
                {
                    case -1:
                        this.combo_HourStart.SelectedIndex = 0;
                        this.combo_HourFinish.SelectedIndex = 24;
                        break;
                    case 105:
                        this.combo_HourStart.SelectedIndex = 7;
                        this.combo_HourFinish.SelectedIndex = 17;
                        break;
                    default:
                        this.combo_HourStart.IsEnabled = true;
                        this.combo_HourFinish.IsEnabled = true;
                        break;
                }
            }
            catch (Exception ee)
            {
                ErrorHelper.ShowErrorLog(ee, true);

            }
        }
        private void radio_stationCnt_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton rBtn = sender as RadioButton;
                if (rBtn == null) return;

                bool isDigit = int.TryParse(rBtn.Uid, out int code);
                if (!isDigit) code = -1;

                this.textbox_StationCount.IsEnabled = false;
                switch (code)
                {
                    case -1:
                        this.textbox_StationCount.Text = ProgramValues.Stations.Count.ToString();
                        break;
                    case 510:
                        this.textbox_StationCount.Text = "10";
                        break;
                    case 505:
                        this.textbox_StationCount.Text = "5";
                        break;
                    default:
                        this.textbox_StationCount.IsEnabled = true;
                        break;
                }
            }
            catch (Exception ee)
            {
                ErrorHelper.ShowErrorLog(ee, true);

            }
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                TextBox tb = e.OriginalSource as TextBox;
                if (tb == null) return;

                string value = tb.Text;

                if (e.Text == "." && (value.Contains(".") || TextHelper.IsNoText(value))) e.Handled = true;
                if (e.Text == "-" && (value.Contains("-") || tb.CaretIndex != 0)) e.Handled = true;
                

                if (!e.Text.IsTextNumbericElement()) e.Handled = true;
               
            }
            catch (Exception ee)
            {
                ErrorHelper.ShowErrorLog(ee, true);

            }

            
                
            //if (!TextHelper.IsTextNuberic(e.Text)) e.Handled = true;
        }
        private void TextBox_PreviewTextInput2(object sender, TextCompositionEventArgs e)
        {
            try
            {
                TextBox tb = e.OriginalSource as TextBox;
                if (tb == null) return;

                string value = tb.Text;
                if (!e.Text.IsTextNumbericElement()) e.Handled = true;
            }
            catch (Exception ee)
            {
                ErrorHelper.ShowErrorLog(ee, true);

            }



            //if (!TextHelper.IsTextNuberic(e.Text)) e.Handled = true;
        }
        private void checkbox_AllStandard_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.IsSingleStandardCheck) return;

                CheckBox cb = sender as CheckBox;
                if (cb == null) return;

                this.IsAllStandardCheck = true;

                bool isChecked = cb.IsChecked == true;
                foreach (CheckBox item in this.StandardCheckBoxes)
                {
                    item.IsChecked = isChecked;
                }

                this.IsAllStandardCheck = false;
            }
            catch (Exception ee)
            {
                
            }
        }
        private void CheckBox_Initialized(object sender, EventArgs e)
        {
            try
            {
                this.StandardCheckBoxes.Add(sender as CheckBox);
            }
            catch (Exception ee)
            {

                
            }
        }
        private void checkbox_Single_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.IsAllStandardCheck) return;

                CheckBox cBox = sender as CheckBox;
                if (cBox == null) return;

                this.IsSingleStandardCheck = true;

                bool isAllCheck = true;
                foreach (CheckBox item in this.StandardCheckBoxes)
                {
                    if(item.IsChecked != true)
                    {
                        isAllCheck = false;
                        break;
                    }
                }
                this.checkbox_AllStandard.IsChecked = isAllCheck;

                this.IsSingleStandardCheck = false;
            }
            catch (Exception ee)
            {

                
            }
        }
        private void btn_UnfoldPanel_Click(object sender, RoutedEventArgs e)
        {
            this.toggle_PanelFold.IsChecked = false;
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                if (tb == null)
                {
                    ProgramValues.WeatherData.Address = null;
                    return;
                }

                string text = tb.Text;

                mAddress address = GetAddressInfo(text);
                ProgramValues.WeatherData.Address = address;

            }
            catch (Exception ee)
            {
                ErrorHelper.ShowErrorLog(ee, true);
            }
        }
        private void textbox_StationCount_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                if (tb == null) return;

                string text = tb.Text;

                bool isDigit = int.TryParse(text, out int digit);
                if (!isDigit) return;

                if (digit < 1) tb.Text = "1";

                int cnt = ProgramValues.Stations.Count;
                if (cnt < digit) tb.Text = cnt.ToString();
            }
            catch (Exception ee)
            {
                ErrorHelper.ShowErrorLog(ee, true);
            }
        }
        private void txtbox_Setting_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                if(tb == null) return;

                string value = tb.Text;

                bool isDigit = int.TryParse(value, out int digit);
                if (!isDigit) tb.Text = "0";
                
            }
            catch (Exception ee)
            {
                ErrorHelper.ShowErrorLog(ee, true);
            }
        }
        private void btn_Analyzie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsStandardValid()) return;
                if(!SaveSettingLog())
                {
                    string eMsg = "데이터 설정 저장시 오류가 발생하였습니다.\n다시 시도하세요.";
                    MessageHelper.ShowErrorMessage("데이터 설정값 오류", eMsg);
                    return;
                }

                string location = this.Weather.Setting.Display_Address;
                this.Weather.Address = GetAddressInfo(location);
                List<vmPublicWeatherStation> stationList = ProgramValues.Stations;
                if (this.Weather.Address != null) stationList = SetStationListByDistance(this.Weather.Address, stationList);

                int cnt = this.Weather.Setting.Display_StationCount;

                this.Weather.ClearStations();
                foreach (vmPublicWeatherStation station in stationList)
                {
                    if (cnt < 1) break;
                    this.Weather.AddStations(station);
                    
                    cnt--;
                }
                Thread.Sleep(5000);
                foreach (vmPublicWeatherStation station in this.Weather.Stations)
                {
                    station.SetWeatherData(this.Weather.Setting);
                    station.Calculation(this.Weather.Setting);
                }
            }
            catch (Exception ee)
            {
                ErrorHelper.ShowErrorLog(ee, true);
            }
        }

        private bool SaveSettingLog()
        {
            vmWeatherAnalizeSetting setting = this.Weather.Setting;

            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            string targetpath = Path.Combine(Defines.BASE_DATA_PROGRAM, fileName+ ".was");

            return JsonHelper.WriteData(targetpath, setting.Data);
        }
    }
}

;