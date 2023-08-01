using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Client.Response
{
    public class WarehouseStockForAdaptationResponse
    {
        public string ProvinceId { get; set; }

        public string CityId { get; set; }

        public int LocationId { get; set; }

        public string ProductCode { get; set; }

        public int AvailableNum { get; set; }
    }
}
