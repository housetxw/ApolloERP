using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Product
{
    /// <summary>
    /// AddGrouponProductRequest
    /// </summary>
    public class AddGrouponProductRequest
    {
        /// <summary>
        /// 团购商品
        /// </summary>
        public List<GrouponProductRequest> GrouponProduct { get; set; }
    }

    /// <summary>
    /// GrouponProductRequest
    /// </summary>
    public class GrouponProductRequest
    {
        /// <summary>
        /// 产品pid
        /// </summary>
        [Required(ErrorMessage = "Pid不能为空")]
        public string Pid { get; set; }

        /// <summary>
        /// 最小价格
        /// </summary>
        public decimal MinPrice { get; set; }

        /// <summary>
        /// 最大价格
        /// </summary>
        public decimal MaxPrice { get; set; }
    }
}
