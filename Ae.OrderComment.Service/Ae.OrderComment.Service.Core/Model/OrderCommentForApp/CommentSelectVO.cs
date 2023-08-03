using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Model.OrderCommentForApp
{
    /// <summary>
    /// 评论选择的VO
    /// </summary>
    public class CommentSelectVO
    {
        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 默认选中项
        /// </summary>
        public bool Checked { get; set; }
    }
}
