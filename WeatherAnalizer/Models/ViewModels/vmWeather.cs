using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WeatherAnalizer.Models.DataModels;

namespace WeatherAnalizer.Models.ViewModels
{
    public partial class vmWeather
    {
        private mAddress _Address = null;
    }

    public partial class vmWeather : vmBaseViewModel
    {
        public vmWeather() 
        {
            SetInitialData();
        }

        
        
        public mAddress Address
        {
            get => _Address;
            set
            {
                _Address = value;
                OnPropertyChanged(nameof(Address));
                this.Setting.CheckAddressValid(_Address);
            }
        }
        public vmWeatherAnalizeSetting Setting { get; set; } 
        
        private ObservableCollection<vmPublicWeatherStation> OriginStations { get;  set; }
        public ReadOnlyObservableCollection<vmPublicWeatherStation> Stations => new ReadOnlyObservableCollection<vmPublicWeatherStation>(new ObservableCollection<vmPublicWeatherStation>(this.OriginStations.OrderBy(x=> x.DistanceFromSite)));
    }

    public partial class vmWeather
    {
        public void AddStations(vmPublicWeatherStation station) => this.OriginStations.Add(station);
        public void ClearStations() =>  this.OriginStations.Clear();
        private void OriginStations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => OnPropertyChanged(nameof(Stations));
        public override void SetInitialData()
        {
            this.OriginStations = new ObservableCollection<vmPublicWeatherStation>();
            this.OriginStations.CollectionChanged += OriginStations_CollectionChanged;
        }
        public override object UpdateOriginData() => null;
        public override void InitializeDisplay()
        {

        }

    }
}
