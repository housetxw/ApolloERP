using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.Config
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

        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "提交人不能为空")]
        public string SubmitBy { get; set; }
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
