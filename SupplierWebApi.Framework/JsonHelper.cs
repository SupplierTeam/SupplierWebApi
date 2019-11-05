using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SupplierWebApi.Framework
{
    public class JsonHelper
    {
        /// <summary>
        /// 读取json文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadJson(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        /// <summary>
        /// 利用newtonsoft.json序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string NewtonsoftSerialiize<T>(T t)
        {
            return JsonConvert.SerializeObject(t);
        }

        /// <summary>
        ///  利用newtonsoft.json反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T NewtonsoftDeserialize<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
    }

}
