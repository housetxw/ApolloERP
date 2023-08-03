using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Promotion
{
    /// <summary>
    /// 编辑促销
    /// </summary>
    public class EditPromotionRequest
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        [Required(ErrorMessage = "活动Id不能为空")]
        public string ActivityId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        [Required(ErrorMessage = "活动名称不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 促销语
        /// </summary>
        [Required(ErrorMessage = "促销描述不能为空")]
        public string Subtitle { get; set; }

        /// <summary>
        /// 活动描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string ThumbUrl { get; set; }

        /// <summary>
        /// 活动类型（0门店商品促销）
        /// </summary>
        public int ActivityType { get; set; }

        /// <summary>
        /// 促销类型（0自定义价格 1打折）
        /// </summary>
        public int PromotionType { get; set; }

        /// <summary>
        /// 标签展示
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        [Required(ErrorMessage = "活动开始时间不能为空")]
        public string StartTime { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        [Required(ErrorMessage = "活动结束时间不能为空")]
        public string EndTime { get; set; }

        /// <summary>
        /// 适用渠道
        /// </summary>
        public List<string> ApplyChannel { get; set; }

        /// <summary>
        /// 促销商品
        /// </summary>
        [Required(ErrorMessage = "促销商品不能为空")]
        [MinLength(1, ErrorMessage = "促销商品不能为空")]
        public List<ActivityProductRequest> ActivityProduct { get; set; }
    }
}
