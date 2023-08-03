using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Fiance
{
    public class GetAccountCheckRequest
    {
        public int LocationId { get; set; }

        public string OrderNo { get; set; }

        public string StartTime { get; set; }

        public string Telephone { get; set; }

        public string EndTime { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string ShopCheckResult { get; set; }
        public string RgCheckResult { get; set; }

        public string LocationIds { get; set; }

    }
}
