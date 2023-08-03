using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Request
{
   public class ShopAccountCheckConfirmRequest
    {
        public string OrderNo { get; set; }

        public string ShopCheckResult { get; set; }

        public string LocationName { get; set; }

        public int LocationId { get; set; }

        public string UpdateBy { get; set; }
    }
}
