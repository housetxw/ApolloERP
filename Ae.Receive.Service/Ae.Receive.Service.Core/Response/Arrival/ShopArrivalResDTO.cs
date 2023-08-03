using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Ae.Receive.Service.Core.Enums;
using Ae.Receive.Service.Core.Model.Arrival;

namespace Ae.Receive.Service.Core.Response.Arrival
{
    public class TodayArrivalShopStatisticsResDTO
    {
        public ShopArrivalStatus Status { get; set; }

        public int Amount { get; set; }
    }

    public class MonthArrivalShopStatisticsResDTO
    {
        public DateTime Date { get; set; }

        public int Amount { get; set; }
    }

    public class NewCustomerArrivalShopResDTO : Statistics
    {
        public string Key { get; set; }
    }

    public class NewCustomerArrivalShopSaleroomResDTO
    {
        public string Key { get; set; }

        public decimal Amount { get; set; }
    }


}
