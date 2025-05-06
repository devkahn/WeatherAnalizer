using DLWIZ.ViewModels.Commons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WeatherAnalizer.Models.DataModels;

namespace WeatherAnalizer.Models.ViewModels
{
    public partial class vmSiteLevel
    {
        private mSiteLevel _Origin = null;


        private string _Display_SiteName = null;


    }
    public partial class vmSiteLevel : vmBaseModel
    {
        public vmSiteLevel(mSiteLevel _origin)
        {
            SetInitialData();
            this.Origin = _origin;
        }



        public mSiteLevel Origin
        {
            get => _Origin;
            set
            {
                _Origin = value;
                this.Display_SiteName = value.GetFullName();
                foreach (mPublicWeatherStation stationOrigin in value.Stations) this.Stations.Add(new vmPublicWeatherStation(stationOrigin));
                SetDisplayStations();

                OnPropertyChanged(nameof(Origin));
            }
        }



        public ObservableCollection<vmPublicWeatherStation> Stations { get; private set; } = null;



        public int Display_StationCount => this.Stations.Count();
        public string Display_SiteName
        {
            get => _Display_SiteName;
            set
            {
                _Display_SiteName = value;
                OnPropertyChanged(nameof(Display_SiteName));
            }
        }
        public ObservableCollection<ListBoxItem> Display_Stations { get; private set; } = null;

    }
    public partial class vmSiteLevel
    {
        private void SetInitialData()
        {
            this.Stations = new ObservableCollection<vmPublicWeatherStation>();
            this.Stations.CollectionChanged += Stations_CollectionChanged;

            this.Display_Stations = new ObservableCollection<ListBoxItem>();
            this.Display_Stations.CollectionChanged += Display_Stations_CollectionChanged;
        }



        private void Stations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    if (e.NewItems == null) return;
                    foreach (vmPublicWeatherStation item in e.NewItems) item.Parent = this;
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }
        }
        private void Display_Stations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }
        }
        private void SetDisplayStations()
        {
            Dictionary<mPublicCity, List<vmPublicWeatherStation>> stationbyCityDic = new Dictionary<mPublicCity, List<vmPublicWeatherStation>>();
            foreach (vmPublicWeatherStation item in this.Stations)
            {
                mPublicCity sameKey = stationbyCityDic.Keys.Where(x => x.Idx == item.Origin.MetropolisCity.Idx).FirstOrDefault();

                if (sameKey != null)
                {
                    stationbyCityDic[sameKey].Add(item);
                }
                else
                {
                    sameKey = item.Origin.MetropolisCity;
                    stationbyCityDic.Add(sameKey, new List<vmPublicWeatherStation>() { item });
                }
            }

            this.Display_Stations.Clear();
            string onlyMetro = string.Empty;
            foreach (mPublicCity item in stationbyCityDic.Keys)
            {
                if (stationbyCityDic[item].Any())
                {
                    ListBoxItem newItem = new ListBoxItem();
                    newItem.Uid = item.GetFullName() + item.Disctrict.GetFullName();

                    List<vmPublicWeatherStation> stations = stationbyCityDic[item];

                    string stationNames = string.Empty;
                    foreach (vmPublicWeatherStation station in stations)
                    {
                        stationNames += ", ";
                        stationNames += station.Origin.StationName;
                    }

                    stationNames = stationNames.Substring(2);
                    newItem.Content = "(" + stationNames + ")";
                    newItem.Tag = stations;

                    this.Display_Stations.Add(newItem);
                }
                else
                {
                    onlyMetro += ", ";
                    onlyMetro += item.GetFullName();
                }
            }
            if (!string.IsNullOrEmpty(onlyMetro))
            {
                onlyMetro = onlyMetro.Substring(2);
                this.Display_Stations.Insert(0, new ListBoxItem() { Uid = onlyMetro });
            }

        }
    }
}
