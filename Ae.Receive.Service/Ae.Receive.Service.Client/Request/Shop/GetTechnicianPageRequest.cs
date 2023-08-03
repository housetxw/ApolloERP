using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request.Shop
{
    public class GetTechnicianPageRequest
    {
      public long ShopId { get; set; }

        public string EmployeeId { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

    }
}
