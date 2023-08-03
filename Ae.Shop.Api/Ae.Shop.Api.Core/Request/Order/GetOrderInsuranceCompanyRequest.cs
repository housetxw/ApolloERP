using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Order
{
    public class GetOrderInsuranceCompanyRequest : BaseGetRequest
    {
        public string OrderNo { get; set; }
    }
}
