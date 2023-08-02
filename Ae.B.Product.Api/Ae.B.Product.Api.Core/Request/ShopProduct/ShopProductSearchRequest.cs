using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.ShopProduct
{
    /// <summary>
    /// 门店商品搜索
    /// </summary>
    public class ShopProductSearchRequest :BasePageRequest
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        ///  分类ID
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        ///  门店ID
        /// </summary>
        public long? ShopId { get; set; }

        /// <summary>
        /// 是否禁用 0 否 1 是
        /// </summary>
        public int? IsDeleted { get; set; }

        /// <summary>
        ///  是否上架  0 否 1 是
        /// </summary>
        public int? IsOnSale { get; set; }

        /// <summary>
        /// 审核状态 1 通过 2 审核中 3驳回
        /// </summary>
        public int? AuditStatus { get; set; }
    }
}
