using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.ShopProduct
{
    public class ShopProductQueryClientRequest
    {
        /// <summary>
        ///  门店ID
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        ///  分类ID
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        ///  是否上架  0 否 1 是
        /// </summary>
        public int? IsOnSale { get; set; }
    }
}
