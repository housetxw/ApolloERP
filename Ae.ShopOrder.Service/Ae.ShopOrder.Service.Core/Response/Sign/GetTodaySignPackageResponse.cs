using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Ae.ShopOrder.Service.Core.Response.Sign
{
    /// <summary>
    /// 得到进入签收包裹返回对象
    /// </summary>
    public class GetTodaySignPackageResponse
    {

        /// <summary>
        /// 包裹单号
        /// </summary>
        public string PackageNo { get; set; }

        /// <summary>
        /// 签收人
        /// </summary>
        public string SignUser { get; set; }

        /// <summary>
        /// 签收时间
        /// </summary>
        public DateTime SignTime { get; set; }

    }
}
