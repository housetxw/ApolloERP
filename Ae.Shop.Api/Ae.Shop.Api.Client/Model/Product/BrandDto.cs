using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Model.Product
{
    /// <summary>
    /// BrandDto
    /// </summary>
    public class BrandDto
    {
        public int Id { get; set; }

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
