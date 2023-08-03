using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response.Arrival
{
    public class GetArrivalMaintenanceItemResponse
    {
        public int Id { get; set; }

        public DateTime ArrivalTime { get; set; }

        public string ShopName { get; set; }

        public string OrderType { get; set; }

        public string Pid { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string OrderNo { get; set; }

        public int Num { get; set; }

        public string TechName { get; set; }



    }
}
