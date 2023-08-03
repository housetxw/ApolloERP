using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class InstallOrderProdcutDTO
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public int Num { get; set; }

        public long OrderListId { get; set; }

        public string OrderNo { get; set; }
    }
}
