using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.BaoYang
{
    public class BaoYangPackageByTidClientRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 保养类型
        /// </summary>
        public string BaoYangType { get; set; }
    }
}
