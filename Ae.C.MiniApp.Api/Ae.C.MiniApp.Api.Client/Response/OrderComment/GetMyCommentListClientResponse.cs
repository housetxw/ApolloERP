using Ae.C.MiniApp.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response
{
    public class GetMyCommentListClientResponse : BaseCommentListClientResponse
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
