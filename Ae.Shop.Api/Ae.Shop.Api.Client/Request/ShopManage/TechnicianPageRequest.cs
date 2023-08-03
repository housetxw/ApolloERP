using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Request.ShopManage
{
    public class TechnicianPageRequest
    {
        public int ShopId { get; set; }

        public string EmployeeId { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
