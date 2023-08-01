using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Client.Request
{
    public class WarehouseStockForAdaptationRequest
    {
        public string ProvinceId { get; set; }

        public string CityId { get; set; }

        public List<string> ProductCodes { get; set; }
    }
}
