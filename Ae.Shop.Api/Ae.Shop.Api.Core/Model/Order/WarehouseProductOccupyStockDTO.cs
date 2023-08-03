using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
  public  class WarehouseProductOccupyStockDTO
    {
        public List<OrderProductStockDTO> ProductStocks { get; set; } = new List<OrderProductStockDTO>();

        public bool IsOccupy { get; set; }
    }
}
