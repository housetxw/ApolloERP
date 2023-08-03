using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Model
{
    public class OrderCommentDetailProductSubmitDTO : BaseOrderCommentDetailSubmitDTO
    {
        /// <summary>
        /// 订单商品Id
        /// </summary>
        public long OrderProductId { get; set; }
    }
}
