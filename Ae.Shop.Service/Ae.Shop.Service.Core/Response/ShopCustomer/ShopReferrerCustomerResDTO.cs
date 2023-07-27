using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response
{
    public class ShopReferrerCustomerResDTO : Statistics
    {
        public long ShopId { get; set; }

        public List<string> UserIds { get; set; } = new List<string>();
    }

    public class DrainageStatisticsResDTO : Statistics
    {
        /// <summary>
        /// 入参是： Employee，Key代表是 EmployeeId；
        ///         ShareType，Key代表是 ReferrerType；
        /// </summary>
        public string Key { get; set; }

        public List<string> UserIds { get; set; } = new List<string>();
    }
}
