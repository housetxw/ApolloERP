using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Model
{
    public class OrderCommentDetailShopSubmitDTO : BaseOrderCommentDetailSubmitDTO
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
    }
}
