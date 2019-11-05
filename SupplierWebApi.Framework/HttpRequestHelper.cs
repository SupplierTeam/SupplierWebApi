using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SupplierWebApi.Framework
{
    public class HttpRequestHelper
    {
        /// <summary>
        /// 模拟get请求
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>String</returns>
        public static string HttpGet(Uri url)
        {
            string result = string.Empty;
            try
            {
                var httpclient = new HttpClient();
                var response = httpclient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    using var myResponseStream = response.Content.ReadAsStreamAsync().Result;
                    using var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    result = myStreamReader.ReadToEnd();
                }
                httpclient.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }

        /// <summary>
        /// 模拟post表单请求
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="postData">参数</param>
        /// <returns>String</returns>
        public static string HttpPost(Uri url, string postData)
        {
            string result = string.Empty;
            try
            {


                var httpCotent = new StringContent(postData);
                var httpclient = new HttpClient();
                var response = httpclient.PostAsync(url, httpCotent).Result;
                if (response.IsSuccessStatusCode)
                {
                    using var myResponseStream = response.Content.ReadAsStreamAsync().Result;
                    using var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    result = myStreamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }

        /// <summary>
        /// application/json数据格式模拟post请求
        /// </summary>
        /// <param name="uri">地址</param>
        /// <param name="data">数据</param>
        /// <returns>string</returns>
        public static string HttpPostJson(Uri uri, string data)
        {
            string result = string.Empty;
            try
            {
                var httpCotent = new StringContent(data, Encoding.UTF8, "application/json");
                var httpclient = new HttpClient();
                var response = httpclient.PostAsync(uri, httpCotent).Result;
                if (response.IsSuccessStatusCode)
                {
                    using var myResponseStream = response.Content.ReadAsStreamAsync().Result;
                    using var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    result = myStreamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("外部接口调用失败: 接口地址" + uri + ex.ToString());
            }

            return result;
        }

        /// <summary>
        /// application/json数据格式模拟post请求
        /// </summary>
        /// <param name="uri">地址</param>
        /// <param name="data">数据</param>
        /// <returns>string</returns>
        public static async Task<string> HttpPostJsonAsync(Uri uri, string data)
        {
            string result = string.Empty;
            try
            {
                var httpCotent = new StringContent(data, Encoding.UTF8, "application/json");
                var httpclient = new HttpClient();
                var response = httpclient.PostAsync(uri, httpCotent).Result;
                if (response.IsSuccessStatusCode)
                {
                    using var myResponseStream = response.Content.ReadAsStreamAsync().Result;
                    using var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    result = await Task.Run(() => myStreamReader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }

        /// <summary>
        /// application/json数据格式模拟put请求
        /// </summary>
        /// <param name="uri">地址</param>
        /// <param name="data">数据</param>
        /// <returns>string</returns>
        public static string HttpPutJson(Uri uri, string data)
        {
            string result = string.Empty;
            try
            {
                var httpCotent = new StringContent(data, Encoding.UTF8, "application/json");  
                var httpclient = new HttpClient();
                var response = httpclient.PutAsync(uri, httpCotent).Result;
                if (response.IsSuccessStatusCode)
                {
                    using var myResponseStream = response.Content.ReadAsStreamAsync().Result;
                    using var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    result = myStreamReader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                result = string.Empty;
            }

            return result;
        }

        /// <summary>
        /// application/json数据格式模拟Delete请求
        /// </summary>
        /// <param name="uri">地址</param>
        /// <param name="data">数据</param>
        /// <returns>string</returns>
        public static string HttpDeleteJson(Uri uri, string data)
        {
            string result = string.Empty;
            try
            {
                var httpclient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Content = new StringContent(data, Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Delete,
                    RequestUri = uri
                };
                var response= httpclient.SendAsync(request).Result;
               
                if (response.IsSuccessStatusCode)
                {
                    using var myResponseStream = response.Content.ReadAsStreamAsync().Result;
                    using var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    result = myStreamReader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                result = string.Empty;
            }

            return result;
        }

        /// <summary>
        /// 模拟put表单请求
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <param name="putData">参数</param>
        /// <returns>String</returns>
        public static string HttpPut(Uri url, string putData)
        {
            string result = string.Empty;
            try
            {
                var httpCotent = new StringContent(putData);
              
                var httpclient = new HttpClient();
                var response = httpclient.PutAsync(url, httpCotent).Result;
                if (response.IsSuccessStatusCode)
                {
                    using var myResponseStream = response.Content.ReadAsStreamAsync().Result;
                    using var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    result = myStreamReader.ReadToEnd();
                }
                httpclient.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }

        /// <summary>
        /// 模拟delete表单请求
        /// </summary>
        /// <param name="url">路径</param>
        /// <param name="deleteData">参数</param>
        /// <returns>String</returns>
        public static string HttpDelete(Uri url, string deleteData)
        {
            string result = string.Empty;
            try
            {
                var httpclient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Content = new StringContent(deleteData),
                    Method = HttpMethod.Delete,
                    RequestUri = url
                };
                var response = httpclient.SendAsync(request).Result;

                if (response.IsSuccessStatusCode)
                {
                    using var myResponseStream = response.Content.ReadAsStreamAsync().Result;
                    using var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    result = myStreamReader.ReadToEnd();
                }
                httpclient.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return result;
        }
    }
}
