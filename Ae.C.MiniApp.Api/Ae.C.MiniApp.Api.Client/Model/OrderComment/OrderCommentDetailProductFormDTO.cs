using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model
{
    public class OrderCommentDetailProductFormDTO
    {
        /// <summary>
        /// 订单商品Id
        /// </summary>
        public long OrderProductId { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string ProductImageUrl { get; set; }
        /// <summary>
        /// 商品显示名称
        /// </summary>
        public string ProductDisplayName { get; set; }
    }
}
