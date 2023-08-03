using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class GetUserCouponPageByUserIdRequest : BasePageRequest
    {
        public string UserId { get; set; }

        //  public CouponListEntranceType EntranceType { get; set; }
    }
}
