using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Stock
{
    public class WarehouseStockForAdaptationRequest
    {
        public string ProvinceId { get; set; }

        public string CityId { get; set; }

        public List<string> ProductCodes { get; set; }
    }
}
