using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using WeatherAnalizer.Models.DataModels;

namespace WeatherAnalizer.Models.ViewModels
{
    public partial class vmWeatherAnalize
    {
        private mWeatherAnalize _Origin = null;
        private mWeatherAnalize _Data = null;
    }



    public partial class vmWeatherAnalize : vmBaseViewModel
    {
        public vmWeatherAnalize() 
        {
            
        }

        private mWeatherAnalize Origin
        {
            get => _Origin;
            set
            {
                _Origin = value;
                this.Data = _Origin;
            }
        }
        public mWeatherAnalize Data
        {
            get => _Data;
            private set
            {
                _Data = value;
                InitializeDisplay();
            }
        }
    }

    public partial class vmWeatherAnalize
    {
        public override void InitializeDisplay()
        {
            if(this.Data == null)
            {

            }
            else
            {

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
