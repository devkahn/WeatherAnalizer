using DLWIZ.Helpers.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WeatherAnalizer.Commons;
using WeatherAnalizer.Helpers;
using WeatherAnalizer.Models.DataModels;
using WeatherAnalizer.Models.ViewModels;
using WeatherAnalizer.Models.ViewModels.Views;

namespace WeatherAnalizer.Views.Windows
{
    /// <summary>
    /// wndSplash.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class wndSplash : Window
    {
        private SplashWindowViewModel _Viewmodel = null;
        private SplashWindowViewModel ViewModel
        {
            get => _Viewmodel;
            set
            {
                if (value != null)
                {
                    _Viewmodel = value;
                    this.DataContext = _Viewmodel;
                }
            }
        }


        public wndSplash()
        {
            InitializeComponent();
            WindowHelper.CenterWindowOnScreen(this);
            this.ViewModel = new SplashWindowViewModel();
            this.ContentRendered += WndSplash_ContentRendered;
        }

        private void UpdateStatus(string message, int delaySecont = 0, bool isMainMessage = true)
        {
            if(isMainMessage) this.Dispatcher.Invoke(() => this.ViewModel.UpdateMainMessage(message));
            else this.Dispatcher.Invoke(() => this.ViewModel.UpdateSubMessage(message));

            Thread.Sleep(delaySecont *1000);
        }

        private async void WndSplash_ContentRendered(object sender, EventArgs e)
        {
            bool isSuccess = false;
            await Task.Factory.StartNew(() =>
            {
                UpdateStatus("프로그램을 초기화 합니다." ,1);
                isSuccess = SetInitializeDatabase();

                UpdateStatus("날씨 데이터를 초기화 합니다.", 1);
                if (isSuccess) isSuccess = ReadWeatherDataByStations(ProgramValues.Stations);
            });

            UpdateStatus("프로그램을 시작합니다.", 3);

            if (isSuccess)
            {
                wndMainWIndow mw = new wndMainWIndow();
                
                mw.Show();
                this.Close();
            }


        }

        private bool SetInitializeDatabase()
        {
            UpdateStatus("기본 데이터를 불러옵니다.", 0, false);
            mWeatherAnalizeSetting seeting = null;


            DirectoryInfo dInfo = new DirectoryInfo(Defines.BASE_DATA_PROGRAM);
            FileInfo fInfo = dInfo.GetFiles().ToList().Where(x => x.Extension == ".was").OrderBy(x => x.FullName).LastOrDefault();
            if (fInfo != null && fInfo.Exists)
            {
                string settingString = File.ReadAllText(fInfo.FullName);
                seeting = JsonHelper.ReadData<mWeatherAnalizeSetting>(settingString);
            }
            if (seeting == null) seeting = new mWeatherAnalizeSetting();
            ProgramValues.WeatherData.Setting = new vmWeatherAnalizeSetting(seeting);


            UpdateStatus("관측소 정보를 불러옵니다.", 0, false);

            string originDbFilePath = Path.Combine(Defines.BASE_DATA_DIRECTORY, Defines.STATION_FILE_NAME);
            List<vmPublicWeatherStation> stationList = SiteHelper.ReadStationFromOrigin(new FileInfo(originDbFilePath));
            if(stationList == null)
            {
                string eMsg = "관측소를 불러오는데 실패하였습니다.\n다시 시도하세요.";
                MessageHelper.ShowErrorMessage("관측소 불러오기", eMsg);
                return false;
            }
            ProgramValues.Stations = stationList;

            return true;
        }


        internal bool ReadWeatherDataByStations(List<vmPublicWeatherStation> stationList)
        {
            DirectoryInfo dInfo = new DirectoryInfo(Defines.DATABASE_DIRECTORY);
            List<FileInfo> files = dInfo.GetFiles().Count() > 0 ? dInfo.GetFiles().ToList() : new List<FileInfo>();
            if (files.Count > 0) files = files.Where(x => x.Extension == ".weather").ToList();

            int cnt = 1;
            int total = stationList.Count();

            foreach (vmPublicWeatherStation station in stationList)
            {
                string stationName = station.Origin.StationName;

                string msg = string.Format("'{2}' 관측소의 날씨 데이터를 불러옵니다...({0}/{1})", cnt, total, stationName);
                this.UpdateStatus(msg, 0);

                FileInfo fInfo = files.Where(x => x.FullName.Contains("_")).Where(x => Path.GetFileNameWithoutExtension(x.Name).Split('_')[1] == station.Origin.StationCode.ToString()).FirstOrDefault();
                if (fInfo == null)
                {
                    msg = string.Format("Database를 생성합니다.");
                    this.UpdateStatus(msg, 1, false);

                    string templateFilePath = Path.Combine(Defines.BASE_DATA_DIRECTORY, "wa_weatherData_Template.db");

                    string fileName = string.Format("{0}_{1}.weather", Guid.NewGuid().ToString(), station.Origin.StationCode.ToString());
                    string targetPath = Path.Combine(Defines.DATABASE_DIRECTORY, fileName);
                    fInfo = new FileInfo(targetPath);
                    File.Copy(templateFilePath, fInfo.FullName);
                }
                station.WeatherDataDBFilePath = fInfo;

                DateTime? lastDate = SiteHelper.ReadLastDateWeatherDataByStation(station);
                if (lastDate.HasValue)
                {
                    msg = string.Format("{0} 이후 데이터를 서버에서 다운로드 받고 있습니다.", lastDate.Value.ToString("yyyy-MM-dd"));
                    this.UpdateStatus(msg, 0, false);
                    SiteHelper.DownloadWeatherDataByStation(station, lastDate.Value);
                }
                else
                {
                    for (int year = 2015; year < DateTime.Now.Year; year++)
                    {
                        msg = string.Format("{0}년도 데이터를 불러옵니다.", year);
                        this.UpdateStatus(msg, 0, false);

                        msg = string.Format("{0}년도 데이터를 서버에서 다운로드 받고 있습니다.", year);
                        this.UpdateStatus(msg, 0, false);
                        SiteHelper.DownloadWeatherDataByStation(station, year);
                    }
                }

                cnt++;
                
                Debug.WriteLine(stationName);
            }

            return true;
        }
        
    }
}
