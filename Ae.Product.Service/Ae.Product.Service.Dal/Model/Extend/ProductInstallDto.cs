using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model.Extend
{
    public class ProductInstallDTO
    {
        public long ProductId { get; set; }

        public string Pid { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public decimal Price { get; set; }

        public sbyte OnSale { get; set; }
    }
}
