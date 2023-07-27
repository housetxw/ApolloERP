using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class DeleteShopImgRequest
    {
        /// <summary>
        /// 门店id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "门店ID必须大于0")]
        public long ShopId { get; set; }
        /// <summary>
        /// 图片id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "图片ID必须大于0")]
        public long ImgId { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
    }
}
