using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model
{
    public class ShopArrivalCustomDO : ShopArrivalDO
    {
        public string Key { get; set; }

        public decimal Price { get; set; }

        public sbyte Num { get; set; }

    }
}
