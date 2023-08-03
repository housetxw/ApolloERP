using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.HomeCare
{
    public class HomeCareStockRequest
    {
        public long ShopId { get; set; }

        public string TechId { get; set; }
    }

    public class HomeCareRequest : BasePageRequest
    {
        public long ShopId { get; set; }

        public string TechId { get; set; }
    }

    public class HomeCareDetailRequest
    {
        public long Id { get; set; }
    }
}
