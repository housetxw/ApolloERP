using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.ShopProduct
{
    /// <summary>
    /// 商品列表搜索
    /// </summary>
    public class ShopProductListRequest : BasePageRequest
    {
        /// <summary>
        /// 关键字 商品编号或名称
        /// </summary>
        public string SearchContent { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 类目Id
        /// </summary>
        public int CategoryId { get; set; }
    }
}
