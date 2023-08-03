using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Dal.Model
{
    public class ShopOrderDO
    {
        public long Id { get; set; }
        public long ShopId { get; set; }
        public string OrderNo { get; set; }
        public sbyte OrderType { get; set; }
        public sbyte ProduceType { get; set; }
        public int OrderStatus { get; set; }
        public decimal ActualAmount { get; set; }
        public int InstallStatus { get; set; }
        public DateTime InstallTime { get; set; }
        public string UserId { get; set; }

    }
}
