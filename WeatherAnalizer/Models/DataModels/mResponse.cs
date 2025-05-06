using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAnalizer.Commons;

namespace WeatherAnalizer.Models.DataModels
{
    public class mResponse
    {
        public eServerResponseType ResponseStatus { get; set; } = eServerResponseType.None;


        public object Result { get; set; } = null;


        public string Description { get; set; } = string.Empty;


        public static mResponse Success(object obj)
        {
            mResponse mResponse2 = new mResponse();
            mResponse2.ResponseStatus = eServerResponseType.Success;
            mResponse2.Result = obj;
            return mResponse2;
        }

        public static mResponse Fail(object obj, string message = "")
        {
            mResponse mResponse2 = new mResponse();
            mResponse2.ResponseStatus = eServerResponseType.Fail;
            mResponse2.Result = obj;
            mResponse2.Description = message;
            return mResponse2;
        }

        public static mResponse Catch(Exception ee)
        {
            mResponse mResponse2 = new mResponse();
            mResponse2.ResponseStatus = eServerResponseType.Catch;
            mResponse2.Result = ee;
            return mResponse2;
        }
    }
}
