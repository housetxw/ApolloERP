using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.ShopProduct
{
    public class ShopProductQueryRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Required(ErrorMessage = "门店ID不能为空")]
        public long ShopId { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        [Required(ErrorMessage = "分类ID不能为空")]
        public int CategoryId { get; set; }
    }
}
