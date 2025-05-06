using DLWIZ.Common.Models.ProgramModels.Attributes;
using DLWIZ.Defines.Attributes;
using DLWIZ.Defines.Controllers;
using DLWIZ.Helpers.Data;
using DLWIZ.Utils.DataUtils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Windows.Shapes;
using WeatherAnalizer.Commons;
using WeatherAnalizer.Models.DataModels;
using WeatherAnalizer.Models.ViewModels;
using Path = System.IO.Path;

namespace WeatherAnalizer.Helpers
{
    public static class SiteHelper
    {
        public static double CalculateDistance(mGeoCoordinate origin, mGeoCoordinate target)
        {
            double oLat = origin.Latitude;
            double oLon = origin.Longitude;

            double tLat = target.Latitude;
            double tLon = target.Longitude;

            double dLon = tLon - oLon;
            double dist = Math.Sin(oLat.DegreeToRadian()) * Math.Sin(tLat.DegreeToRadian())
                            + Math.Cos(oLat.DegreeToRadian()) * Math.Cos(tLat.DegreeToRadian()) * Math.Cos(dLon.DegreeToRadian());
            dist = Math.Acos(dist);
            dist = dist.RadianToDegree();
            dist = dist * 60 * 1.1515;
            dist = dist * 1.609344;

            return dist;
        }
        public static List<vmPublicWeatherStation> GetStationViewModels(List<vmSiteLevel> siteLevels)
        {
            List<vmPublicWeatherStation> output = new List<vmPublicWeatherStation>();

            foreach (vmSiteLevel sitelevel in siteLevels)
            {
                output.AddRange(sitelevel.Stations);
            }

            return output;
        }
        public static List<vmSiteLevel> ReadSiteLevelViewModels()
        {
            List<vmSiteLevel> output = new List<vmSiteLevel>();

            string responseString = DBHelper.SendWebRequest(Defines.URL, TIMEControllerDefines.Controller_SiteLevelHoliday, TIMEControllerDefines.Method_ReadSiteLevels, string.Empty, HttpMethod.Get);
            if (!string.IsNullOrEmpty(responseString))
            {
                List<mSiteLevel> origin = new List<mSiteLevel>();

                mResponse response = JsonConvert.DeserializeObject<mResponse>(responseString);
                switch (response.ResponseStatus)
                {
                    case eServerResponseType.Success:
                        List<mSiteLevel> levels = JsonConvert.DeserializeObject<List<mSiteLevel>>(response.Result.ToString());
                        if (levels != null) origin = levels;
                        break;
                    case eServerResponseType.Fail:
                        break;
                    case eServerResponseType.Catch:
                        break;
                    default:
                        break;
                }

                foreach (mSiteLevel item in origin)
                {
                    output.Add(new vmSiteLevel(item));
                }
            }

            return output;
        }
        public static List<vmPublicWeatherStation> ReadStationFromOrigin(FileInfo dbPath)
        {
            List<vmPublicWeatherStation> output = null;

            string conString = string.Format("Data Source={0};Version=3", dbPath.FullName);
            using (SQLiteConnection conn = new SQLiteConnection(conString))
            {
                conn.Open();

                List<vmSiteLevel> siteLevels = ReadSiteLevels(conn).Select(x => new vmSiteLevel(x)).ToList();
                List<mPublicWeatherStation> stations = ReadStations(conn);

                string selectQuery = "SELECT * FROM t_REL_SiteLevel_Station";
                SQLiteCommand cmd = new SQLiteCommand(selectQuery, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int siteLevelIdx = int.Parse(reader["SiteLevelIdx"].ToString());
                    int stationCode = int.Parse(reader["StationCode"].ToString()) ;

                    mPublicWeatherStation sameStation = stations.Where(x=> x.StationCode == stationCode).FirstOrDefault();
                    if(sameStation != null)
                    {
                        vmSiteLevel sameSite = siteLevels.Where(x=> x.Origin.Idx == siteLevelIdx).FirstOrDefault();
                        if (sameSite == null) continue;
                        if (output == null) output = new List<vmPublicWeatherStation>();


                        vmPublicWeatherStation newStation = new vmPublicWeatherStation(sameStation);
                        newStation.Parent = sameSite;
                        output.Add(newStation);
                    }
                }

            }

            return output;
        }
        internal static List<mPublicWeatherStation> ReadStations(SQLiteConnection conn)
        {
            List<mPublicWeatherStation> output = new List<mPublicWeatherStation>();

            string selectQuery = "SELECT * FROM t_Weather_Station";

            SQLiteCommand cmd = new SQLiteCommand(selectQuery, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                mPublicWeatherStation newStation = new mPublicWeatherStation();
                SetProperties(reader, newStation);
                output.Add(newStation);
            }

            return output;
        }
        internal static List<mSiteLevel> ReadSiteLevels(SQLiteConnection conn)
        {
            List<mSiteLevel> output= new List<mSiteLevel>();

            string selectQuery = "SELECT * FROM t_SiteLevel";

            SQLiteCommand cmd = new SQLiteCommand(selectQuery, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                mSiteLevel newSiteLevel = new mSiteLevel();
                SetProperties(reader, newSiteLevel);
                output.Add(newSiteLevel);
            }

            return output;
        }

        public static List<mPublicHourlyWeatherData> ReadWeatherDataByStation(vmPublicWeatherStation station)
        {
            FileInfo fInfo = station.WeatherDataDBFilePath;
            if (fInfo == null) return null;


            List<mPublicHourlyWeatherData> output = new List<mPublicHourlyWeatherData>();

            string conString = string.Format("Data Source={0};Version=3", fInfo.FullName);
            string tableName = "t_Weather_Hourly";
            using (SQLiteConnection conn = new SQLiteConnection(conString))
            {
                conn.Open();
                string recentTmQuery = $"SELECT * FROM {tableName};";
                
                using (SQLiteCommand cmd = new SQLiteCommand(recentTmQuery, conn))
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        mPublicHourlyWeatherData newData = new mPublicHourlyWeatherData();

                        var props = newData.GetType().GetProperties();
                        foreach (PropertyInfo prop in props)
                        {
                            var headerAttr = prop.GetCustomAttribute<ColumnHeaderAttribute>();
                            if (headerAttr == null) continue; // 컬럼이 아닌 경우 무시

                            InteagerAttribute idxAttribute = prop.GetCustomAttribute(typeof(InteagerAttribute)) as InteagerAttribute;
                            if (idxAttribute == null) continue;

                            string columnName = headerAttr.ColumnValue;
                            int index = idxAttribute.InteagerValue;
                            object obj = reader[columnName];
                            string value = obj == null? string.Empty : obj.ToString();
                            switch (index)
                            {
                                case 0: prop.SetValue(newData, ConvertIntegerFromString(value)); break;  // 0.Idx
                                case 1: prop.SetValue(newData, ConvertDatetimeFromString(value)); break;  // 1.관측시간
                                case 2: prop.SetValue(newData, ConvertIntegerFromString(value)); break;  // 2.국내 지점 번호
                                case 3: prop.SetValue(newData, ConvertIntegerFromString(value)); break;  // 3.풍향
                                case 4: prop.SetValue(newData, ConvertDoubleFromString(value, -9)); break;  // 4.풍속
                                case 5: prop.SetValue(newData, ConvertIntegerFromString(value, -9)); break;  // 5.돌풍향
                                case 6: prop.SetValue(newData, ConvertDoubleFromString(value, -9)); break;  // 6.돌풍속
                                case 7: prop.SetValue(newData, ConvertIntegerFromString(value, -9)); break;  // 7.돌풍속이 관측된 시각
                                case 8: prop.SetValue(newData, ConvertDoubleFromString(value)); break;  // 8.현지기압
                                case 9: prop.SetValue(newData, ConvertDoubleFromString(value, -9)); break;  // 9.해면기압
                                case 10: prop.SetValue(newData, ConvertIntegerFromString(value, -9)); break;  // 10.기압변화경향
                                case 11: prop.SetValue(newData, ConvertDoubleFromString(value, -9)); break;  // 11.기압변화량
                                case 12: prop.SetValue(newData, ConvertDoubleFromString(value, -99)); break;  // 12.기온
                                case 13: prop.SetValue(newData, ConvertDoubleFromString(value, -99)); break;  // 13.이슬점온도
                                case 14: prop.SetValue(newData, ConvertDoubleFromString(value)); break;  // 14.상대습도
                                case 15: prop.SetValue(newData, ConvertDoubleFromString(value)); break;  // 15.수증기압
                                case 16: prop.SetValue(newData, ConvertDoubleFromString(value, -9)); break;  // 16.강수량
                                case 17: prop.SetValue(newData, ConvertDoubleFromString(value, -9)); break;  // 17.일강수량(통계)
                                case 18: prop.SetValue(newData, ConvertDoubleFromString(value, -9)); break;  // 18.일강수량(전문)
                                case 19: prop.SetValue(newData, ConvertDoubleFromString(value, -9)); break;  // 19.강수도
                                case 20: prop.SetValue(newData, ConvertDoubleFromString(value, -9)); break;  // 20.3시간 신적설
                                case 21: prop.SetValue(newData, ConvertDoubleFromString(value, -9)); break;  // 21.일 신적설
                                case 22: prop.SetValue(newData, ConvertDoubleFromString(value, -9)); break;  // 22.적설
                                case 23: prop.SetValue(newData, ConvertIntegerFromString(value, -9)); break;  // 23.GTS 현재 일기
                                case 24: prop.SetValue(newData, ConvertIntegerFromString(value, -9)); break;  // 24.GTS 과거일기
                                case 25: prop.SetValue(newData, ConvertIntegerFromString(value)); break;  // 25.국내식 일기코드
                                case 26: prop.SetValue(newData, ConvertIntegerFromString(value, -9)); break;  // 26.전운량
                                case 27: prop.SetValue(newData, ConvertIntegerFromString(value, -9)); break;  // 27.중하층운량
                                case 28: prop.SetValue(newData, ConvertIntegerFromString(value, -9)); break;  // 28.최저운고
                                case 29: prop.SetValue(newData, ConvertIntegerFromString(value, -9)); break;  // 29.운형
                                case 30: prop.SetValue(newData, ConvertIntegerFromString(value, -9)); break;  // 30.GTS 상층운형
                                case 31: prop.SetValue(newData, ConvertIntegerFromString(value, -9)); break;  // 31.GTS 중층운형
                                case 32: prop.SetValue(newData, ConvertIntegerFromString(value, -9)); break;  // 32.GTS 하층운형
                                case 33: prop.SetValue(newData, ConvertIntegerFromString(value)); break;  // 33.시정
                                case 34: prop.SetValue(newData, ConvertDoubleFromString(value, -9)); break;  // 34.일조
                                case 35: prop.SetValue(newData, ConvertDoubleFromString(value, -9)); break;  // 35.일사
                                case 36: prop.SetValue(newData, ConvertIntegerFromString(value, -9)); break;  // 36.지면상태 코드
                                case 37: prop.SetValue(newData, ConvertDoubleFromString(value, -9)); break;  // 37.지면온도
                                case 38: prop.SetValue(newData, ConvertDoubleFromString(value, -99)); break;  // 38.5cm 지중온도
                                case 39: prop.SetValue(newData, ConvertDoubleFromString(value, -99)); break;  // 39.10cm 지중온도
                                case 40: prop.SetValue(newData, ConvertDoubleFromString(value, -99)); break;  // 40.20cm 지중온도
                                case 41: prop.SetValue(newData, ConvertDoubleFromString(value, -99)); break;  // 41.30cm 지중온도
                                case 42: prop.SetValue(newData, ConvertIntegerFromString(value, -9)); break;  // 42.해면상태 코드
                                case 43: prop.SetValue(newData, ConvertDoubleFromString(value, -9)); break;  // 43.파고
                                case 44: prop.SetValue(newData, ConvertIntegerFromString(value, -9)); break;  // 44.Beaufart 최대 풍력
                                case 45: prop.SetValue(newData, ConvertIntegerFromString(value)); break;  // 45.강수자료 유무
                                case 46: prop.SetValue(newData, ConvertIntegerFromString(value, -9)); break;  // 46.유인관츨/무인관츨 및 일기 포함 여부
                                default: prop.SetValue(newData, value); break;
                            }
                        }

                        output.Add(newData);    
                    }
                }
            }

            return output;
        }
        internal static DateTime? ReadLastDateWeatherDataByStation(vmPublicWeatherStation station)
        {
            FileInfo fInfo = station.WeatherDataDBFilePath;
            if (fInfo == null) return null;

            string conString = string.Format("Data Source={0};Version=3", fInfo.FullName);
            string tableName = "t_Weather_Hourly";
            using (SQLiteConnection conn = new SQLiteConnection(conString))
            {
                conn.Open();
                string recentTmQuery = $"SELECT TM FROM {tableName} ORDER BY TM DESC LIMIT 1;";

                bool isSync = true;
                using (SQLiteCommand cmd = new SQLiteCommand(recentTmQuery, conn))
                {
                    var last = cmd.ExecuteScalar();
                    if (last != DBNull.Value && last != null)
                    {
                        return Convert.ToDateTime(last);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        internal static bool DownloadWeatherDataByStation(vmPublicWeatherStation station, int year)
        {
            FileInfo fInfo = station.WeatherDataDBFilePath;
            if (fInfo == null) return false;

            string conString = string.Format("Data Source={0};Version=3", fInfo.FullName);
            string tableName = "t_Weather_Hourly";
            using (SQLiteConnection conn = new SQLiteConnection(conString))
            {
                conn.Open();
                List<mPublicHourlyWeatherData> weatherData = WeatherHelper.ReadHourlyWeatherData(station.Origin, year);
                if (weatherData == null || weatherData.Count == 0) return false;
                using (var transaction = conn.BeginTransaction())
                {
                    foreach (mPublicHourlyWeatherData item in weatherData)
                    {
                        using (var command = item.GetInsertCommand(tableName, conn))
                        {
                            command.Transaction = transaction;
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
            }

            return true;
        }
        internal static bool DownloadWeatherDataByStation(vmPublicWeatherStation station, DateTime lastDate)
        {
            FileInfo fInfo = station.WeatherDataDBFilePath;
            if (fInfo == null) return false;

            string conString = string.Format("Data Source={0};Version=3", fInfo.FullName);
            string tableName = "t_Weather_Hourly";
            using (SQLiteConnection conn = new SQLiteConnection(conString))
            {
                conn.Open();
                List<mPublicHourlyWeatherData> weatherData = WeatherHelper.ReadHourlyWeatherData(station.Origin, lastDate);
                if (weatherData == null || weatherData.Count == 0) return false;
                using (var transaction = conn.BeginTransaction())
                {
                    foreach (mPublicHourlyWeatherData item in weatherData)
                    {
                        using (var command = item.GetInsertCommand(tableName, conn))
                        {
                            command.Transaction = transaction;
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
            }

            return true;
        }

        private static void SetProperties(SQLiteDataReader reader, object obj)
        {
            PropertyInfo[] infos = obj.GetType().GetProperties();
            foreach (PropertyInfo p in infos)
            {
                ColumnHeaderAttribute colHeader = p.GetCustomAttribute(typeof(ColumnHeaderAttribute)) as ColumnHeaderAttribute;
                if (colHeader == null) continue;

                var rawValue = reader[colHeader.ColumnValue];
                string value = rawValue == null ? string.Empty : rawValue.ToString();

                string fullName = p.PropertyType.FullName;
                if (fullName == typeof(string).FullName) p.SetValue(obj, value);
                if (fullName == typeof(int).FullName) p.SetValue(obj, int.Parse(value));
                if (fullName == typeof(DateTime).FullName) p.SetValue(obj, DateTime.Parse(value));
                if (fullName == typeof(DateTime?).FullName) if (!string.IsNullOrEmpty(value)) p.SetValue(obj, DateTime.Parse(value));
                if (fullName == typeof(bool).FullName) p.SetValue(obj, bool.Parse(value));
            }
        }
        private static SQLiteCommand GetInsertCommand(this mPublicHourlyWeatherData data, string tableName, SQLiteConnection conn)
        {

            var props = data.GetType().GetProperties();

            var columnNames = new List<string>();
            var parameterNames = new List<string>();
            var command = conn.CreateCommand();

            foreach (var prop in props)
            {
                var headerAttr = prop.GetCustomAttribute<ColumnHeaderAttribute>();
                if (headerAttr == null) continue; // 컬럼이 아닌 경우 무시

                string columnName = headerAttr.ColumnValue;
                string paramName = "@" + columnName;


                columnNames.Add(columnName);
                parameterNames.Add(paramName);

                var value = prop.GetValue(data) ?? DBNull.Value;
                command.Parameters.AddWithValue(paramName, value);
            }

            var insertSql = $"INSERT INTO {tableName} ({string.Join(", ", columnNames)}) VALUES ({string.Join(", ", parameterNames)});";
            command.CommandText = insertSql;

            return command;
        }

        public static DateTime ConvertDatetimeFromString(string value)
        {
           bool isDatetime = DateTime.TryParse(value, out DateTime date);
           return isDatetime ? date : DateTime.MinValue;
        }
        public static int? ConvertIntegerFromString(string value, int? nullValue = null)
        {
            int? output = null;

            bool isInteger = int.TryParse(value, out int number);
            if (isInteger)
            {
                if (nullValue.HasValue)
                {
                    if (number != nullValue.Value) output = number;
                }
                else
                {
                    output = number;
                }
            }

            return output;
        }
        public static double? ConvertDoubleFromString(string value, double? nullValue = null)
        {
            double? output = null;

            bool isDouble = double.TryParse(value, out double number);
            if (isDouble)
            {
                if (nullValue.HasValue)
                {
                    if (number != nullValue.Value) output = number;
                }
                else
                {
                    output = number;
                }
            }

            return output;
        }

    }
}
