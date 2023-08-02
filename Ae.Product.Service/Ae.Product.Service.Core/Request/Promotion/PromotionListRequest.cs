using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.Promotion
{
    /// <summary>
    /// 促销列表Request
    /// </summary>
    public class PromotionListRequest : BasePageRequest
    {
        /// <summary>
        /// 状态： 0进行中 1已结束
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "ShopId大于0")]
        public int ShopId { get; set; }
    }
}
