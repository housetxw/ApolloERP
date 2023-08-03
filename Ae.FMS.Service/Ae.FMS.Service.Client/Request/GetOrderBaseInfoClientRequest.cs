using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Client.Request
{
   public class GetOrderBaseInfoClientRequest
    {
        /// <summary>
     /// 门店ShopId
     /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 订单号集合
        /// </summary>
        public List<string> OrderNos { get; set; }

        /// <summary>
        /// 订单Ids
        /// </summary>
        public List<int> OrderIds { get; set; }
    }
}
