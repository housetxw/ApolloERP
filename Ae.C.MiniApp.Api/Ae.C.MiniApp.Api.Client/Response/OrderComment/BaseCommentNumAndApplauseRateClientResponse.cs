using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response
{
    public class BaseCommentNumAndApplauseRateClientResponse
    {
        /// <summary>
        /// 评价数
        /// </summary>
        public int CommentNum { get; set; }
        /// <summary>
        /// 好评率（单位为%）
        /// </summary>
        public decimal ApplauseRate { get; set; }
    }
}
