using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.ShopProduct
{
    public class InitShopServiceRequest
    {
        [Required(ErrorMessage = "门店ID必填")]
        [Range(1, long.MaxValue, ErrorMessage = "门店ID必填")]
        public long ShopId { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
    }
}
