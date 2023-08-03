using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class GetTechnicianPageRequest
    {
        public long ShopId { get; set; }

        public string EmployeeId { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
