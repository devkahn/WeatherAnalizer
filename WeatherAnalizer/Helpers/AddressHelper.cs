using DLWIZ.Defines.Controllers;
using DLWIZ.Helpers.Data;
using DLWIZ.Utils.VisualUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherAnalizer.Commons;
using WeatherAnalizer.Models.DataModels;

namespace WeatherAnalizer.Helpers
{
    public static class AddressHelper
    {
        public static mAddress ReadGeoCoordinateFromAddressString(mRequestGeocoordinate value)
        {
            mAddress output = null;
            try
            {
                string contentString = JsonConvert.SerializeObject(value);
                string responsString = DBHelper.SendWebRequest(Defines.URL, PublicControllerDefines.Controller_PublicAddressController, PublicControllerDefines.Method_ReadGeoCoordinateFromAddress, contentString, HttpMethod.Post);
                mResponse response = JsonConvert.DeserializeObject<mResponse>(responsString);
                switch (response.ResponseStatus)
                {
                    case eServerResponseType.Success:
                        output = JsonConvert.DeserializeObject<mAddress>(response.Result.ToString());
                        break;
                    case eServerResponseType.Catch:
                        Exception ee = JsonConvert.DeserializeObject<Exception>(response.Result.ToString());
                        ErrorHelper.ShowErrorLog(ee, true);
                        break;
                    default:
                        //MessageUtil.ShowErrorMessage("좌표 변환하기", response.Description);
                        break;
                }
            }
            catch (Exception ee)
            {
                ErrorHelper.ShowErrorLog(ee, true);
            }
            return output;
        }


    }
}
