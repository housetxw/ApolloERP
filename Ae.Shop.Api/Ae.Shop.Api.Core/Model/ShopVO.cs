using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class ShopVO
    {
        /// <summary>
        /// 简单名称
        /// </summary>
        [Required(ErrorMessage = "简单名称不能为空")]
        public string SimpleName { get; set; }
        /// <summary>
        /// 门店全称
        /// </summary>
        [Required(ErrorMessage = "门店全称不能为空")]
        public string FullName { get; set; }
    }
}
