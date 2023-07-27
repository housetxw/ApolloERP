using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class AdvertisementDTO
    {
        /// <summary>
        /// 广告主键ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; } = string.Empty;
        /// <summary>
        /// 广告类型 
        /// </summary>
        public sbyte Type { get; set; }
        /// <summary>
        /// 有效开始时间
        /// </summary>
        public DateTime StartDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 有效结束时间
        /// </summary>
        public DateTime EndDate { get; set; } = new DateTime(1900, 1, 1);
    }
}
