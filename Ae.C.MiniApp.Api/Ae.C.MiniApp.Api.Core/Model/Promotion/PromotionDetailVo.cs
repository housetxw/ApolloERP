using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model.Promotion
{
    /// <summary>
    /// 促销详情
    /// </summary>
    public class PromotionDetailVo
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
        /// 促销价
        /// </summary>
        public decimal PromotionPrice { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal OriginalPrice { get; set; }

        /// <summary>
        /// 可售数量
        /// </summary>
        public int AvailQuantity { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public List<string> ImageUrl { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// 服务大类Id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 服务大类Code
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// 服务大类名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public List<TagInfo> Tags { get; set; }
    }
}
