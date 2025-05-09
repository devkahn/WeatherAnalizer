using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WeatherAnalizer.Commons;
using WeatherAnalizer.Helpers;
using WeatherAnalizer.Models.ViewModels;
using WeatherAnalizer.Views.Windows;
using NetworkHelper = WeatherAnalizer.Helpers.NetworkHelper;

namespace WeatherAnalizer
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        private static Mutex mutex = new Mutex(true, Defines.PromgraGuid);

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                bool isConnected = NetworkHelper.IsNetworkConnected();
                if (!isConnected)
                {
                    string caption = "네트워크 오류";
                    string eMsg = "데이터 서버에 연결할 수 없습니다.\n다시 시도하세요.";

                    MessageHelper.ShowErrorMessage(caption,eMsg);
                    return;

                }

                SetInitialIze();





                if(!mutex.WaitOne(TimeSpan.Zero, true))
                {
                    // 이미 실행 중이면 기존 창 활성화
                    ActivateExistingWindow();
                    return;
                }

                ProgramValues.WeatherData = new vmWeather();
                wndSplash splash = new wndSplash();
                splash.Show();

                GC.KeepAlive(mutex); // Mutex가 GC에 의해 해제되지 않도록 유지
            }
            catch (Exception ee)
            {
                ErrorHelper.ShowErrorLog(ee,true);
            }
        }

        private void SetInitialIze()
        {
            string dlconDir = Defines.DLCON_DIRECTORY;
            if (!Directory.Exists(dlconDir)) Directory.CreateDirectory(dlconDir);
            string baseDir = Defines.BASE_DIRECTORY;
            if(!Directory.Exists(baseDir)) Directory.CreateDirectory(baseDir);
            string baseProgram = Defines.BASE_DATA_PROGRAM;
            if (!Directory.Exists(baseProgram)) Directory.CreateDirectory(baseProgram);
            string logDir = Defines.LOG_PATH;
            if(!Directory.Exists(logDir)) Directory.CreateDirectory(logDir);
            string dbDir = Defines.DATABASE_DIRECTORY;
            if(!Directory.Exists(dbDir)) Directory.CreateDirectory(dbDir);
            
        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private static void ActivateExistingWindow()
        {
            Process currentProcess = Process.GetCurrentProcess();
            foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
            {
                if (process.Id != currentProcess.Id) // 현재 실행 중인 프로세스 제외
                {
                    IntPtr hWnd = process.MainWindowHandle;
                    if (hWnd != IntPtr.Zero)
                    {
                        ShowWindow(hWnd, 9); // SW_RESTORE: 최소화된 창을 복원
                        SetForegroundWindow(hWnd); // 창을 최상위로 설정
                    }
                    break;
                }
            }
        }
    }
}
