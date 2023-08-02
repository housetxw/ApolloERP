using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.ShopProduct
{
    public class ShopProductAuditRequest
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
        /// 审核状态 1 通过 2 审核中 3驳回
        /// </summary>
        public sbyte AuditStatus { get; set; }
        /// <summary>
        /// 审核意见
        /// </summary>
        public string AuditOpinion { get; set; }
     
    }
}
