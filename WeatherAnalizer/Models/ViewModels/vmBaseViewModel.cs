using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WeatherAnalizer.Models.Interfaces;

namespace WeatherAnalizer.Models.ViewModels
{
    public abstract class vmBaseViewModel : INotifyPropertyChanged, INotifyPropertyChanging, IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;


        public vmBaseViewModel()
        {
            SetInitialData();
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        protected void OnPropertyChanging([CallerMemberName] string propertyName ="") => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        public abstract void SetInitialData();
        public abstract object UpdateOriginData();
        public abstract void InitializeDisplay();
    }
}
