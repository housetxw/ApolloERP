using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    /// <summary>
    /// 获取门店的评论列表请求
    /// </summary>
    public class GetCommentListRequest : BasePageRequest
    {
        /// <summary>
        /// 订单类型（All=全部 Tire=轮胎 BaoYang=保养 Beauty=美容）
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// 评分级别（All=全部 ShopGood(5)分好评 ShopNegative(3分以下评价)
        /// </summary>
        public string ScoreLevel { get; set; }
        /// <summary>
        /// 回复状态（All=全部 ShopReply=门店已回 ShopNotReply=门店未回）
        /// </summary>
        public string ReplyStatus { get; set; }
    }
}
