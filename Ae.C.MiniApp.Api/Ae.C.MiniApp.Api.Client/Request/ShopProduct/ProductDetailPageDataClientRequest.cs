using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.ShopProduct
{
    public class ProductDetailPageDataClientRequest
    {
        /// <summary>
        /// 商品Pid
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// shopId
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 渠道：MiniApp,ShopApp
        /// </summary>
        public string Channel { get; set; }
    }
}
