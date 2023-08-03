using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class GetOrderInsuranceCompanyRequest:BaseGetRequest
    {
        public string OrderNo { get; set; }
    }
}
