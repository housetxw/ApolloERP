using Ae.Shop.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Request
{
    public class GetOrderCommentBaseClientRequest : BasePageRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;


        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;


        /// <summary>
        /// 门店名称
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 审核状态（0待审核 1审核通过 2审核不通过）
        /// </summary>
        public sbyte CheckStatus { get; set; } = -1;

        /// <summary>
        /// 渠道（0未设置 1C端 2门店）
        /// </summary>
        public sbyte Channel { get; set; }

        /// <summary>
        /// 订单类型（0未设置 1轮胎 2保养 3美容）
        /// </summary>
        public sbyte OrderType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateStartTime { get; set; }

        public DateTime CreateEndTime { get; set; }

        public List<string> CreateTime { get; set; }
    }
}
