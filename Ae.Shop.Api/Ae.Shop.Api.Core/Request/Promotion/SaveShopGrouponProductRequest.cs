using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Promotion
{
    /// <summary>
    /// SaveShopGrouponProductRequest
    /// </summary>
    public class SaveShopGrouponProductRequest
    {
        /// <summary>
        /// 产品pid
        /// </summary>
        [Required(ErrorMessage = "产品pid不能为空")]
        public string Pid { get; set; }

        /// <summary>
        /// 售价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 状态 0：下架 1：上架
        /// </summary>
        public sbyte Status { get; set; }
    }
}
