using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAnalizer.Models.Interfaces
{
    public interface IViewModel
    {
        void SetInitialData();
        object UpdateOriginData();
        void InitializeDisplay();
    }
}
