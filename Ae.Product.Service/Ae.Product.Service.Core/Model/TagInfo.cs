using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model
{
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
        /// 标签名称(EG:总部推荐)
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 标签颜色(EG:#F37C3E)
        /// </summary>
        public string TagColor { get; set; }
    }
}
