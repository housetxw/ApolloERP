using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.FMS.Api.Core.Request.AccountCheck
{
    public class SavePurchaseExceptionAccountRequest
    {
        public long Id { get; set; }

        public string ReportBy { get; set; }

        public string ReportReason { get; set; }


    }
}
