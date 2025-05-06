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
    public static class WeatherHelper
    {
        public static List<mPublicHourlyWeatherData> ReadHourlyWeatherData(mPublicWeatherStation station)
        {
            List<mPublicHourlyWeatherData> output = null;

            try
            {
                string method = "ReadHourlyWeatherDatasByStation";

                string contentString = JsonConvert.SerializeObject(station);
                string resString = DBHelper.SendWebRequest(Defines.URL, PublicControllerDefines.Controller_PublicWeatherDataController, method, contentString, HttpMethod.Post);
                mResponse response = JsonConvert.DeserializeObject<mResponse>(resString);
                switch (response.ResponseStatus)
                {
                    case eServerResponseType.Success:
                        List<mPublicHourlyWeatherData> result = JsonConvert.DeserializeObject<List<mPublicHourlyWeatherData>>(response.Result.ToString());
                        if (result != null) output = result;
                        break;
                    case eServerResponseType.Fail:
                        MessageUtil.ShowErrorMessage("", response.Description);
                        break;
                    case eServerResponseType.Catch:
                        Exception ee = JsonConvert.DeserializeObject<Exception>(response.Result.ToString());
                        // ErrorHelper.ShowErrorLog(ee, false);
                        break;
                    default: break;
                }
            }
            catch (Exception ee)
            {
                ErrorHelper.ShowErrorLog(ee, false);
            }

            return output;
        }
        public static List<mPublicHourlyWeatherData> ReadHourlyWeatherData(mPublicWeatherStation station, int year)
        {
            List<mPublicHourlyWeatherData> output = null;

            try
            {
                string method = string.Format("ReadAllHourlyWeatherDataByStation?year={0}", year.ToString());

                string contentString = JsonConvert.SerializeObject(station);
                string resString = DBHelper.SendWebRequest(Defines.URL, PublicControllerDefines.Controller_PublicWeatherDataController, method, contentString, HttpMethod.Post);
                mResponse response = JsonConvert.DeserializeObject<mResponse>(resString);
                switch (response.ResponseStatus)
                {
                    case eServerResponseType.Success:
                        List<mPublicHourlyWeatherData> result = JsonConvert.DeserializeObject<List<mPublicHourlyWeatherData>>(response.Result.ToString());
                        if (result != null) output = result;
                        break;
                    case eServerResponseType.Fail:
                        MessageUtil.ShowErrorMessage("", response.Description);
                        break;
                    case eServerResponseType.Catch:
                        Exception ee = JsonConvert.DeserializeObject<Exception>(response.Result.ToString());
                        // ErrorHelper.ShowErrorLog(ee, false);
                        break;
                    default: break;
                }
            }
            catch (Exception ee)
            {
                ErrorHelper.ShowErrorLog(ee, false);
            }

            return output;
        }
        public static List<mPublicHourlyWeatherData> ReadHourlyWeatherData(mPublicWeatherStation station, DateTime lastDate)
        {
            List<mPublicHourlyWeatherData> output = null;

            try
            {
                string method = string.Format("ReadAllHourlyWeatherDataFromLastDateByStation?lastDate={0}", lastDate.ToString());

                string contentString = JsonConvert.SerializeObject(station);
                string resString = DBHelper.SendWebRequest(Defines.URL, PublicControllerDefines.Controller_PublicWeatherDataController, method, contentString, HttpMethod.Post);
                mResponse response = JsonConvert.DeserializeObject<mResponse>(resString);
                switch (response.ResponseStatus)
                {
                    case eServerResponseType.Success:
                        List<mPublicHourlyWeatherData> result = JsonConvert.DeserializeObject<List<mPublicHourlyWeatherData>>(response.Result.ToString());
                        if (result != null) output = result;
                        break;
                    case eServerResponseType.Fail:
                        MessageUtil.ShowErrorMessage("", response.Description);
                        break;
                    case eServerResponseType.Catch:
                        Exception ee = JsonConvert.DeserializeObject<Exception>(response.Result.ToString());
                        // ErrorHelper.ShowErrorLog(ee, false);
                        break;
                    default: break;
                }
            }
            catch (Exception ee)
            {
                ErrorHelper.ShowErrorLog(ee, false);
            }

            return output;
        }
        public static int ReadHourlyWeatherDataCount(mPublicWeatherStation station, int year)
        {
            int total = 0;
            try
            {
                string method = string.Format("ReadCountOfAllHourlyWeatherDataByStation?year={0}", year.ToString());

                string contentString = JsonConvert.SerializeObject(station);
                string resString = DBHelper.SendWebRequest(Defines.URL, PublicControllerDefines.Controller_PublicWeatherDataController, method, contentString, HttpMethod.Post);
                mResponse response = JsonConvert.DeserializeObject<mResponse>(resString);
                switch (response.ResponseStatus)
                {
                    case eServerResponseType.Success:
                        total = Convert.ToInt32(response.Result.ToString());// JsonConvert.DeserializeObject<List<mPublicHourlyWeatherData>>(response.Result.ToString());
                        break;
                    case eServerResponseType.Fail:
                        MessageUtil.ShowErrorMessage("", response.Description);
                        break;
                    case eServerResponseType.Catch:
                        Exception ee = JsonConvert.DeserializeObject<Exception>(response.Result.ToString());
                        // ErrorHelper.ShowErrorLog(ee, false);
                        break;
                    default: break;
                }
            }
            catch (Exception ee)
            {
                ErrorHelper.ShowErrorLog(ee, false);
            }

            return total;
        }
    }
}
