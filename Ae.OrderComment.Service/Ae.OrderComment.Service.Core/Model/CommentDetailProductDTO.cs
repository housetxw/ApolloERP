using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Model
{
    public class CommentDetailProductDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 评论Id
        /// </summary>
        public long CommentId { get; set; }
        /// <summary>
        /// 订单商品Id
        /// </summary>
        public long OrderProductId { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 商品显示名称
        /// </summary>
        public string ProductDisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 商品图片
        /// </summary>
        public string ProductImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// 五级分值
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 是否匿名
        /// </summary>
        public sbyte IsAnonymous { get; set; }
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
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}