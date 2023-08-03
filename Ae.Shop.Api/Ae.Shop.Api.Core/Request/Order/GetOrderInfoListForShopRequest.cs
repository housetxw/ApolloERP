using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Order
{
    public class GetOrderInfoListForShopRequest : BasePageRequest
    {
        public long ShopId
        {
            get; set;
        }

        public List<long> ShopIds { get; set; }
        public string OrderNo { get; set; }

        public string OrderStatus { get; set; }

        public string OrderChannel { get; set; }

        public string OrderType { get; set; }
        /// <summary>
        /// 订单分类
        /// </summary>
        public int ProductType { get; set; } = -1;


        public string ExceptionOrder { get; set; }

        public string InstallStatus { get; set; }

        public string PayStatus { get; set; }

        public string CustomerName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone { get; set; }


        public string CreateStartTime { get; set; }

        public string CreateEndTime { get; set; }

        public string InstallStartTime { get; set; }

        public string InstallEndTime { get; set; }

    }
}
