using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.ShopProduct
{
    /// <summary>
    /// 查询上架的门店项目
    /// </summary>
    public class OnSaleShopProductRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "门店Id必须大于0")]
        public int ShopId { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "分类Id必须大于0")]
        public int CategoryId { get; set; }

        /// <summary>
        /// 渠道：MiniApp,ShopApp
        /// </summary>
        public string Channel { get; set; }
    }
}
