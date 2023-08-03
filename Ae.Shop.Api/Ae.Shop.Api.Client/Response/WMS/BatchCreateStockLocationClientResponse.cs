using Ae.Shop.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Response
{
    public class BatchCreateStockLocationClientResponse
    {
        public List<StockLocationClientDTO> StockLocations { get; set; } = new List<StockLocationClientDTO>();
    }

}
