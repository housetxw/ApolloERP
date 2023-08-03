using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    public class GetMyCommentListResponse : BaseCommentListResponse
    {
        /// <summary>
        /// 单品、服务或套餐
        /// </summary>
        public List<CommentProductOrPackageSimpleVO> ProductOrPackages { get; set; }
        /// <summary>
        /// 回复按钮集合
        /// </summary>
        public CommentReplyButtonVO ReplyButtons { get; set; }
    }
}
