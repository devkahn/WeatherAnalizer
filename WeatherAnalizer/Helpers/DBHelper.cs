using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace WeatherAnalizer.Helpers
{
    public static class DBHelper
    {
        private static string RequestNormalData(string request, string method, string token = "")
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                HttpClient httpClient = new HttpClient(handler);
                httpClient.BaseAddress = new Uri("https://dcis.dlconstruction.co.kr/dbs" + '/');
                httpClient.Timeout = Timeout.InfiniteTimeSpan;
                if (method.CompareTo("login") != 0)
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add("authorization", token);
                }

                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpResponseMessage result = httpClient.PostAsync("revit.webservice", content).GetAwaiter().GetResult();
                return result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            catch (Exception exception)
            {
                ErrorHelper.ShowErrorLog(exception, _isNoticed: true);
                return "";
            }
        }

        public static string RequestNormalData(string id, string method, List<Tuple<string, string>> headers, string token = "")
        {
            string empty = string.Empty;
            string requestString = GetRequestString(id, method, headers);
            if (string.IsNullOrEmpty(requestString))
            {
                return empty;
            }

            string text = RequestNormalData(requestString, method, token);
            if (string.IsNullOrEmpty(text))
            {
                return empty;
            }

            if (IsErrorResponse(text))
            {
                return empty;
            }

            return text;
        }

        private static string GetRequestString(string id, string method, List<Tuple<string, string>> headers)
        {
            try
            {
                string text = "{\"id\":" + "\"" + id + "\"";
                text = text + "," + "\"method\":" + "\"" + method + "\"";
                if (headers != null && headers.Count > 0)
                {
                    text = text + "," + "\"params\":{";
                    for (int i = 0; i < headers.Count; i++)
                    {
                        Tuple<string, string> tuple = headers[i];
                        text = text + "\"" + tuple.Item1 + "\":" + "\"" + tuple.Item2 + "\"";
                        if (i < headers.Count - 1)
                        {
                            text += ",";
                        }
                    }

                    text += "}";
                }

                return text + "}";
            }
            catch (Exception exception)
            {
                ErrorHelper.ShowErrorLog(exception, _isNoticed: false);
                return string.Empty;
            }
        }

        private static bool IsErrorResponse(string request)
        {
            //IL_0004: Unknown result type (might be due to invalid IL or missing references)
            //IL_0009: Unknown result type (might be due to invalid IL or missing references)
            //IL_0024: Unknown result type (might be due to invalid IL or missing references)
            //IL_0029: Unknown result type (might be due to invalid IL or missing references)
            //IL_003a: Unknown result type (might be due to invalid IL or missing references)
            //IL_003f: Unknown result type (might be due to invalid IL or missing references)
            try
            {
                //JsonElement val = JsonSerializer.Deserialize<JsonElement>(request, (JsonSerializerOptions)null);
                //JsonElement val2 = default(JsonElement);
                //if (((JsonElement)(ref val)).TryGetProperty("error", ref val2))
                //{
                //    JsonElement property = ((JsonElement)(ref val2)).GetProperty("code");
                //    int @int = ((JsonElement)(ref property)).GetInt32();
                //    property = ((JsonElement)(ref val2)).GetProperty("message");
                //    string @string = ((JsonElement)(ref property)).GetString();
                //    return true;
                //}

                return false;
            }
            catch (Exception exception)
            {
                ErrorHelper.ShowErrorLog(exception, _isNoticed: false);
                return false;
            }
        }

        public static string SendWebRequest(string url, string controllerName, string methodName, string contentString, HttpMethod httpMethod)
        {
            HttpWebRequest httpWebRequest = null;
            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(url + controllerName + methodName));
                httpWebRequest.Method = httpMethod.Method;
                httpWebRequest.Accept = "application/json";
                httpWebRequest.ContentType = "application/json; charset=UTF-8";
                httpWebRequest.Timeout = 500000;
                if (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Put)
                {
                    StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                    streamWriter.Write(contentString);
                    streamWriter.Close();
                }

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
                string result = streamReader.ReadToEnd();
                if (httpMethod == HttpMethod.Get || httpMethod == HttpMethod.Post)
                {
                    return result;
                }

                return string.Empty;
            }
            catch (WebException)
            {
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
