using System;
using System.Collections.Generic;
using System.Text;


namespace Ae.ShopOrder.Service.Core.Model
{
    public class Purchase
    {
        public int PKID { get; set; }
        public int ShopID { get; set; }
        public string Supplier { get; set; }
    }
}
