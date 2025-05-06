using System;
using System.Windows.Input;
using System.Windows.Threading;

namespace WeatherAnalizer.Models.ViewModels.Views
{
    public partial class SplashWindowViewModel 
    {
        private DispatcherTimer _timer;
        private int _dotCount = 0;

        private string _Display_MainStatusMessage = string.Empty;
        private string _Display_SubStatusMessage = string.Empty;
    }
    public partial class SplashWindowViewModel : vmBaseViewModel
    {
        public SplashWindowViewModel() 
        {

        }


        public string Display_MainStatusMessage
        {
            get => _Display_MainStatusMessage;
            private set
            {
                _Display_MainStatusMessage = value;
                UpdateSubMessage(string.Empty);
                OnPropertyChanged(nameof(Display_MainStatusMessage));
            }
        }
        public string Display_SubStatusMessage
        {
            get => _Display_SubStatusMessage;
            private set
            {
                _Display_SubStatusMessage = value;
                OnPropertyChanged(nameof(Display_SubStatusMessage));
            }
        }
    }
    public partial class SplashWindowViewModel 
    {
        public override void InitializeDisplay()
        {
            
        }

        public override void SetInitialData()
        {
            
        }

        public override object UpdateOriginData()
        {
            return null;
        }
        public void UpdateMainMessage(string message) => this.Display_MainStatusMessage = message;
        public void UpdateSubMessage(string message)
        {
            if(_timer != null) _timer.Stop();
            if (string.IsNullOrEmpty(message))
            {
                this.Display_SubStatusMessage = message;
                return;
            }

            _dotCount = 0;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Tick += (s, e) =>
            {
                _dotCount = (_dotCount + 1) % 4;
                this.Display_SubStatusMessage = message + new string('.', _dotCount);
            };
            _timer.Start();
        }
    }
}
