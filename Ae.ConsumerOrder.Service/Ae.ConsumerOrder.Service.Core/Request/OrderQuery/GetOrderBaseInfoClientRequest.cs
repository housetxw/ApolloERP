using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request.OrderQuery
{
    public class GetOrderBaseInfoClientRequest : BaseGetRequest
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
        /// 安装码
        /// </summary>
        public List<string> InstallCodes { get; set; }
    }
}
