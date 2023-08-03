using Ae.Shop.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Request
{
   public class BatchCreateStockTranctionClientRequest
    {
        public List<StockTranctionClientDTO> StockTranctions { get; set; } = new List<StockTranctionClientDTO>();

    }
}
