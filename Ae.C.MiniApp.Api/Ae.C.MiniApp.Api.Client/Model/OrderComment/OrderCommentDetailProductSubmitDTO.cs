using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model
{
    public class OrderCommentDetailProductSubmitDTO : BaseOrderCommentDetailSubmitDTO
    {
        /// <summary>
        /// 订单商品Id
        /// </summary>
        public long OrderProductId { get; set; }
    }
}
