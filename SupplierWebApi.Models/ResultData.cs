using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierWebApi.Models
{
    public class ResultData
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        ///  返回信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }
    }
}
