using Ae.C.MiniApp.Api.Client.Model.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response.Product
{
    public class ProductSearchClientResponse
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public List<ProductSearchInfoDto> Items { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// 聚合后的商品品牌
        /// </summary>
        public List<string> Brands { get; set; }
    }
}
