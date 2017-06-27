using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace XCode.Infrastructure
{
    /// <summary>
    /// 调用WebAPI
    /// </summary>
    public static class XcHttpClient
    {
        public static Task<T> Get<T>(ApiHost host) where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(host.Domain);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(host.DataMimeType));
                var response = client.GetAsync(host.ApiName + (string.IsNullOrEmpty(host.InputQueryString) ? "" : "?" + host.InputQueryString)).Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<string> json = response.Content.ReadAsStringAsync();
                    var strJson = json.Result;
                    if (!string.IsNullOrWhiteSpace(strJson))
                    {
                        if (strJson.IndexOf("k__BackingField") > 0)
                        {
                            return response.Content.ReadAsAsync<T>();
                        }
                        else
                        {
                            return Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(strJson));
                        }
                    }
                    //return response.Content.ReadAsAsync<T>();
                }
                return null;
            }
        }

        public static Task<string> Get(ApiHost host) 
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(host.Domain);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(host.DataMimeType));
                var response = client.GetAsync(host.ApiName + (string.IsNullOrEmpty(host.InputQueryString) ? "" : "?" + host.InputQueryString)).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync();
                }
                return null;
            }
        }

        public static Task<TOut> Post<TIn, TOut>(ApiHost host, TIn postData)
            where TIn : class
            where TOut : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(host.Domain);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(host.DataMimeType));
                var response = client.PostAsJsonAsync<TIn>(host.ApiName, postData).Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<string> json = response.Content.ReadAsStringAsync();
                    var strJson = json.Result;
                    if (!string.IsNullOrWhiteSpace(strJson))
                    {
                        if (strJson.IndexOf("k__BackingField") > 0)
                        {
                            return response.Content.ReadAsAsync<TOut>();
                        }
                        else
                        {
                            return Task.Factory.StartNew(() => JsonConvert.DeserializeObject<TOut>(strJson));
                        }
                        //var task = Task.Factory.StartNew(() => JsonConvert.DeserializeObject<TOut>(strJson));
                        //if (task.Result == null)
                        //{
                        //    return response.Content.ReadAsAsync<TOut>();
                        //}
                        //else
                        //{
                        //    return task;
                        //}
                    }
                    //return response.Content.ReadAsAsync<TOut>();
                }
                return null;
            }
        }
    }

    /// <summary>
    /// WebApi配置
    /// </summary>
    public class ApiHost
    {
        public ApiHost()
        {
            DataMimeType = "application/json";
        }
        public ApiHost(string domain, string apiName, string inputQueryString, string dataMimeType)
        {
            Domain = domain;
            ApiName = apiName;
            InputQueryString = inputQueryString;
            DataMimeType = dataMimeType ?? "application/json";
        }
        /// <summary>
        /// 主域名(比如http://api.rongzi.com or http://127.0.0.1)
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Api名称(api/v1/studentinfotest)
        /// </summary>
        public string ApiName { get; set; }

        /// <summary>
        /// 传入参数名称
        /// </summary>
        public string InputQueryString { get; set; }

        /// <summary>
        /// 返回数据类型
        /// </summary>
        public string DataMimeType { get; set; }

        /// <summary>
        /// 重写ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}",
                this.ApiName,
                this.Domain,
                this.DataMimeType,
                this.InputQueryString);
        }
    }
}
