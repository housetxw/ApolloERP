using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.Promotion
{
    public class PromotionActivityListDto
    {
        /// <summary>
        /// 促销活动Id
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// 主标题
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 状态显示
        /// </summary>
        public string StatusDisplay { get; set; }

        /// <summary>
        /// 状态 1待审核 2已拒绝  3未开始  4进行中 5已结束
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }
    }
}
