using Ae.B.Product.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Response
{
    public class ProductSearchClientResponse
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public List<ProductAllInfoDto> Items { get; set; }

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
