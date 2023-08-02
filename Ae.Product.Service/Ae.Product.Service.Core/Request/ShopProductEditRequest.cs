using Ae.Product.Service.Core.Model.ShopProduct;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request
{
    public class ShopProductEditRequest
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public BaseShopProductVo ShopProduct { get; set; }

        /// <summary>
        /// 图片信息
        /// </summary>
        public List<string> Images { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }

        /// <summary>
        /// 是否编辑
        /// </summary>
        public bool IsEdit { get; set; }
    }
}
