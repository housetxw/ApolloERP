using Ae.OrderComment.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Response
{
    public class GetMyCommentListResponse : BaseCommentListResponse
    {
        /// <summary>
        /// 单品、服务或套餐
        /// </summary>
        public List<CommentProductOrPackageSimpleDTO> ProductOrPackages { get; set; }
        /// <summary>
        /// 回复按钮集合
        /// </summary>
        public CommentReplyButtonDTO ReplyButtons { get; set; }
    }
}
