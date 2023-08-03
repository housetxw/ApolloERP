using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Dal.Model
{
  public  class GetOrderCommentForReplyDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 回复id
        /// </summary>
        public long ReplyId { get; set; }
        /// <summary>
        /// 若为回复，回复方类型（0未设置 1门店商家 2官方客服 3:用户）
        /// </summary>
        public sbyte ReplyPartType { get; set; }
        /// <summary>
        /// 回复类型（0未设置  1回复点评 2客户追评 3回复追评）
        /// </summary>
        public sbyte ReplyType { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; } = string.Empty;
        /// <summary>
        /// 渠道（0未设置 1平台C端 2平台门店）
        /// </summary>
        public sbyte Channel { get; set; }
        /// <summary>
        /// 订单类型（0未设置 1轮胎 2保养 3美容）
        /// </summary>
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 审核状态（0待审核 1审核通过 2审核不通过）
        /// </summary>
        public sbyte CheckStatus { get; set; }
        /// <summary>
        /// 审核意见
        /// </summary>
        public string CheckComment { get; set; } = string.Empty;
        /// <summary>
        /// 手写评价内容
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);

    }
}




