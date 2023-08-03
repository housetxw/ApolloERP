using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Promotion
{
    public class GetPromotionActivitByProductCodeListRequest
    {
        public List<string> ProductCodeList { get; set; }

        public string PromotionChannel { get; set; }
    }
}
