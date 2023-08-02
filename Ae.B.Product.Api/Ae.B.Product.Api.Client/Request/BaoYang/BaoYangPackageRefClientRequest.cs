using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.BaoYang
{
    public class BaoYangPackageRefClientRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public List<string> TidList { get; set; }

        /// <summary>
        /// 套餐Id
        /// </summary>
        public string PackageId { get; set; }
    }
}
