using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Request
{
    public class AccountCheckCollectRequest
    {
        public int ShopId { get; set; }

        public string ShopName { get; set; }

        public string OrderNo { get; set; }

        public string ProvinceId { get; set; }

        public string CityId { get; set; }

        public string DistrictId { get; set; }


        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
