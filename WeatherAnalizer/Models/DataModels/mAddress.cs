using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAnalizer.Models.DataModels
{
    public class mAddress
    {
        [JsonProperty("admCd")]
        public string AdministrateCode { get; set; } = string.Empty;


        [JsonProperty("udrtYn")]
        public string IsBasement { get; set; } = string.Empty;


        [JsonProperty("bdKdcd")]
        public string IsApartment { get; set; } = string.Empty;


        [JsonProperty("mtYn")]
        public string IsMountain { get; set; } = string.Empty;


        [JsonProperty("zipNo")]
        public string ZipNumber { get; set; } = string.Empty;


        public mParcelAddress ParcelAddress { get; set; } = new mParcelAddress();


        public mRoadAddress RoadAddress { get; set; } = new mRoadAddress();


        [JsonProperty("siNm")]
        public string Level1AddressName { get; set; } = string.Empty;


        [JsonProperty("sggNm")]
        public string Level2AddressName { get; set; } = string.Empty;


        [JsonProperty("emdNm")]
        public string Level3AddressName { get; set; } = string.Empty;


        [JsonProperty("emdNo")]
        public string Level3AddressCode { get; set; } = string.Empty;


        [JsonProperty("liNm")]
        public string Level4AddressName { get; set; } = string.Empty;


        [JsonProperty("bdNm")]
        public string BuildingName { get; set; } = string.Empty;


        [JsonProperty("detBdNmList")]
        public List<string> BuildingDetailName { get; set; } = new List<string>();


        [JsonProperty("bdMgtSn")]
        public string BuildingSerial { get; set; } = string.Empty;


        public mGeoCoordinate GeoCooradinate { get; set; } = new mGeoCoordinate();


        //public void SetValue(JToken token)
        //{
        //    if (token != null)
        //    {
        //        AdministrateCode = token.Value<string>("admCd")!.ToString();
        //        IsBasement = token.Value<string>("udrtYn")!.ToString();
        //        IsApartment = token.Value<string>("bdKdcd")!.ToString();
        //        IsMountain = token.Value<string>("mtYn")!.ToString();
        //        ZipNumber = token.Value<string>("zipNo")!.ToString();
        //        Level1AddressName = token.Value<string>("siNm")!.ToString();
        //        Level2AddressName = token.Value<string>("sggNm")!.ToString();
        //        Level3AddressName = token.Value<string>("emdNm")!.ToString();
        //        Level3AddressCode = token.Value<string>("emdNo")!.ToString();
        //        Level4AddressName = token.Value<string>("liNm")!.ToString();
        //        BuildingName = token.Value<string>("bdNm")!.ToString();
        //        BuildingDetailName = token.Value<string>("detBdNmList")!.ToString().Split(',').ToList();
        //        BuildingSerial = token.Value<string>("bdMgtSn")!.ToString();
        //        RoadAddress.RoadAddress = token.Value<string>("roadAddrPart1")!.ToString();
        //        RoadAddress.RoadAddress_Ref = token.Value<string>("roadAddrPart2")!.ToString();
        //        RoadAddress.FUllRoadAddress = token.Value<string>("roadAddr")!.ToString();
        //        RoadAddress.FullRoadAddress_Eng = token.Value<string>("engAddr")!.ToString();
        //        RoadAddress.BuildingMainNumber = token.Value<string>("buldMnnm")!.ToString();
        //        RoadAddress.BuildingSubNumber = token.Value<string>("buldSlno")!.ToString();
        //        RoadAddress.RoadAddressNameCode = token.Value<string>("rnMgtSn")!.ToString();
        //        RoadAddress.RoadName = token.Value<string>("rn")!.ToString();
        //        ParcelAddress.FullParcelAddress = token.Value<string>("jibunAddr")!.ToString();
        //        ParcelAddress.MainNumber = token.Value<string>("lnbrMnnm")!.ToString();
        //        ParcelAddress.SubNumber = token.Value<string>("lnbrSlno")!.ToString();
        //    }
        //}
    }

    public class mParcelAddress
    {
        [JsonProperty("jibunAddr")]
        public string FullParcelAddress { get; set; } = string.Empty;


        [JsonProperty("lnbrMnnm")]
        public string MainNumber { get; set; } = string.Empty;


        [JsonProperty("lnbrSlno")]
        public string SubNumber { get; set; } = string.Empty;

    }

    public class mRoadAddress
    {
        [JsonProperty("roadAddrPart1")]
        public string RoadAddress { get; set; } = string.Empty;


        [JsonProperty("roadAddrPart2")]
        public string RoadAddress_Ref { get; set; } = string.Empty;


        [JsonProperty("roadAddr")]
        public string FUllRoadAddress { get; set; } = string.Empty;


        [JsonProperty("engAddr")]
        public string FullRoadAddress_Eng { get; set; } = string.Empty;


        [JsonProperty("buldMnnm")]
        public string BuildingMainNumber { get; set; } = string.Empty;


        [JsonProperty("buldSlno")]
        public string BuildingSubNumber { get; set; } = string.Empty;


        [JsonProperty("rnMgtSn")]
        public string RoadAddressNameCode { get; set; } = string.Empty;


        [JsonProperty("rn")]
        public string RoadName { get; set; } = string.Empty;

    }

    public class mRequestGeocoordinate
    {
        public string InputAddressString { get; set; }

        public mRequestGeocoordinate(string address)
        {
            InputAddressString = address;
        }
    }
}
