using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request
{
    public class ShopProductQueryRequest
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        ///  门店ID
        /// </summary>
        [Required(ErrorMessage = "门店ID不可为空")]
        [Range(1, long.MaxValue, ErrorMessage = "门店ID必须大于0")]
        public long ShopId { get; set; }

        /// <summary>
        ///  分类ID
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        ///  是否上架  0 否 1 是  2（审核中）
        /// </summary>
        public int? IsOnSale { get; set; }
        /// <summary>
        /// 审核状态 1 通过 2 审核中 3驳回
        /// </summary>
        public int? AuditStatus { get; set; }
    }
}
