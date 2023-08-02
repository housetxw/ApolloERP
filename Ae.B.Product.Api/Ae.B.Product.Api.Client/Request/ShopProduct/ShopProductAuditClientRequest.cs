using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.ShopProduct
{
    public class ShopProductAuditClientRequest
    {
        /// <summary>
        /// 门店商品Id
        /// </summary>
        public int ShopProductId { get; set; }
        /// <summary>
        /// 门店商品编码
        /// </summary>
        public string ShopProductCode { get; set; }
        /// <summary>
        /// 审核人名称
        /// </summary>
        /// <returns></returns>
        public string AuditUser { get; set; }
        /// <summary>
        /// 审核状态 1 通过 2 审核中 3驳回
        /// </summary>
        public sbyte AuditStatus { get; set; }
        /// <summary>
        /// 审核意见
        /// </summary>
        public string AuditOpinion { get; set; }
        /// <summary>
        /// 操作人Id
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
    }
}
