using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class UpdateShopDepositRequest
    {
        public long ShopId { get; set; }

        public decimal Amount { get; set; }

        public string UpdateBy { get; set; }
    }
}
