using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Promotion
{
    /// <summary>
    /// 促销列表Request
    /// </summary>
    public class SearchPromotionActivityRequest : BasePageRequest
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// 商品Pid
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 活动开始日期
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// 活动结束日期
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 状态 0:全部 1待审核 2已拒绝  3未开始  4进行中 5已结束
        /// </summary>
        public int Status { get; set; }
    }
}
