using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Dal.Model
{
    public class CommentLabelTotalDO
    {
        /// <summary>
        /// 标签ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string LabelName { get; set; } = string.Empty;
        /// <summary>
        /// 标签数量
        /// </summary>
        public int Num { get; set; }
    }
}
