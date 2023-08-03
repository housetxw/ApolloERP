using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Adapter
{
    public class VerifyAdaptiveProductsClientRequest
    {
        /// <summary>
        /// 车型
        /// </summary>
        public VehicleClientRequest Vehicle { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public List<ProductClinetRequest> Products { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ProductClinetRequest
    {
        /// <summary>
        /// 商品pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 最小类目Id
        /// </summary>
        public string CategoryId { get; set; }
    }
}
