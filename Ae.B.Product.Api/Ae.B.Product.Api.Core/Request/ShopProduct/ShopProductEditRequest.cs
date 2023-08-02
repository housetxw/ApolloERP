using Ae.B.Product.Api.Core.Model.ShopProduct;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.ShopProduct
{
   public class ShopProductEditRequest
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public ShopProductVo ShopProduct { get; set; }
       

        /// <summary>
        /// 是否编辑
        /// </summary>
        public bool IsEdit { get; set; }
    }
}
