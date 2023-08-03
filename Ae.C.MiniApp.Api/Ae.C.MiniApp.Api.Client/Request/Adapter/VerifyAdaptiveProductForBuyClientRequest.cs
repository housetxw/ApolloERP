using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Adapter
{
    public class VerifyAdaptiveProductForBuyClientRequest
    {
        /// <summary>
        /// 商品Pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 最小类目Id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public BaoYang.VehicleClientRequest Vehicle { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }
    }
}
