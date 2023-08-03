using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    public class GetShopEmployeeByJobIdPageRequest
    {
        public long ShopId { get; set; }

        public string EmployeeId { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public List<int> JobId { get; set; }
    }
}
