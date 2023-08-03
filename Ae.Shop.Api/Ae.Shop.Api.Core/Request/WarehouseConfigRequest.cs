using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class WarehouseConfigRequest
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }

        public string WarehouseName { get; set; }

    }
}
