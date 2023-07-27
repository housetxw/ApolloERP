using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetFAQListRequest
    {

        /// <summary>
        /// 渠道
        /// </summary>
        public long ChannelId { get; set; }
        /// <summary>
        /// 一级分类ID
        /// </summary>
        public long OneCategoryId { get; set; }
        /// <summary>
        /// 二级分类ID
        /// </summary>
        public long TwoCategoryId { get; set; }
        /// <summary>
        /// 三级分类ID
        /// </summary>
        public long ThreeCategoryId { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "页码必须大于0")]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "页大小必须大于0")]
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// 问题
        /// </summary>
        public string Question { get; set; }
    }
}
