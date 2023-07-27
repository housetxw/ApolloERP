using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class ShopImgDTO
    {
        /// <summary>
        /// 图片ID
        /// </summary>
        public long ImgId { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgUrl { get; set; } = string.Empty;
        /// <summary>
        /// 图片类型 1门头图片 2门店照片 3资质证明 4正面照
        /// </summary>
        public sbyte Type { get; set; }

        public string Url { get; set; } = string.Empty;
    }
}
