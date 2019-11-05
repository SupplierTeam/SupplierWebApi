using System;
using System.Security.Cryptography;
using System.Text;

namespace SupplierWebApi.Framework
{
    public class MD5Helper
    {

        /// <summary>
		/// Md5加密
		/// </summary>
		/// <param name="secret">加密内容</param>
		/// <returns>string</returns>
		public static string MD5Hash(string secret)
        {
            using MD5 md5 = MD5.Create();
            var result = md5.ComputeHash(Encoding.GetEncoding("UTF-8").GetBytes(secret));
            var strResult = BitConverter.ToString(result);
            return strResult.Replace("-", string.Empty).ToLower();
        }
    }
}
