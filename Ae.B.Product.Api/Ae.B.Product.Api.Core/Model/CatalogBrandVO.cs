using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model
{
    /// <summary>
    /// 商品品牌ViewModel
    /// </summary>
    public class CatalogBrandVo
    {
        /// <summary>
        /// 品牌
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 品牌图片Url
        /// </summary>
        public string BrandImg { get; set; }
    }
}
