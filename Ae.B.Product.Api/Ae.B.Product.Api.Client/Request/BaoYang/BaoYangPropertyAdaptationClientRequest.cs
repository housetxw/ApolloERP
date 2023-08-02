using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.BaoYang
{
    public class BaoYangPropertyAdaptationClientRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public List<string> TidList { get; set; }

        /// <summary>
        /// 保养类型
        /// </summary>
        public string BaoYangType { get; set; }
    }
}
