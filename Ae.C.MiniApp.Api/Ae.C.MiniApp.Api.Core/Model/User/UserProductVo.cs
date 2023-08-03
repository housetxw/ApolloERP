using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserProductVo
    {
        /// <summary>
        /// 商品pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 商品显示
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal Price { get; set; }   

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// 好评率
        /// </summary>
        public decimal FeedbackRate { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public List<TagInfo> Tags { get; set; } = new List<TagInfo>();
    }

    /// <summary>
    /// 标签
    /// </summary>
    public class TagInfo
    {
        /// <summary>
        /// 标签CODE(EG:RGRecommend)
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 标签名称(EG:推荐)
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 标签颜色(EG:#F37C3E)
        /// </summary>
        public string TagColor { get; set; }
    }
}
