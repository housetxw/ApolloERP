using Ae.Shop.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
   public class ProductStockRequest
    {
        public List<ProductStockDTO> Stocks { get; set; } = new List<ProductStockDTO>();
    }
}
