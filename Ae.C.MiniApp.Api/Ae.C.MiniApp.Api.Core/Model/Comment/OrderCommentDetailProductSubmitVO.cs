using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model
{
    public class OrderCommentDetailProductSubmitVO : BaseOrderCommentDetailSubmitVO
    {
        /// <summary>
        /// 订单商品Id
        /// </summary>
        public long OrderProductId { get; set; }
    }
}
