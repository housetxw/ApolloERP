using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model.Promotion
{
    public class SearchPromotionCondition
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
        /// 活动开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 状态 0:全部 1待审核 2已拒绝  3未开始  4进行中 5已结束
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public List<string> ActivityIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime DateTimeNow { get; set; }
    }
}
