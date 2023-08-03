using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.BaoYang
{
    public class BaoYangAdaptiveProductRequest
    {
        /// <summary>
        /// 车型
        /// </summary>
        public VehicleClientRequest Vehicle { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public List<ProductClientRequest> Products { get; set; }
    }

    public class ProductClientRequest
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
