using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response.Stock
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
