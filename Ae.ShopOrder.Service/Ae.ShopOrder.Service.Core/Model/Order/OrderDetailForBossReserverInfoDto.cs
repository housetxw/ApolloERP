using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Order
{
    public class OrderDetailForBossReserverInfoDto
    {
        public long ReserveId { get; set; }

        /// <summary>
        /// 服务时间(预约时间)
        /// </summary>
        public string ReserverTime { get; set; }
    }
}
