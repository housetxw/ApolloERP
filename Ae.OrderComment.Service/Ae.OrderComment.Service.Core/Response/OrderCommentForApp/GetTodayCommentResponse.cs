using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Response.OrderCommentForApp
{
    /// <summary>
    /// 得到今日评价
    /// </summary>
    public class GetTodayCommentResponse
    {
        /// <summary>
        /// 好评数
        /// </summary>
        public  int GoodCommentNum { get; set; }

        /// <summary>
        /// 差评数
        /// </summary>
        public int NavigateCommentNum { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public decimal Score { get; set; }
    }
}
