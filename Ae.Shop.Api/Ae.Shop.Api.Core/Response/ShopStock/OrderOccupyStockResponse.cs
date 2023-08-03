using Ae.Shop.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Response
{
    public class OrderOccupyStockResponse
    {
        public List<OrderProductStockDetailDTO> OccupyStockList { get; set; } = new List<OrderProductStockDetailDTO>();

        public bool IsOccupy { get; set; } = false;

    }
}
