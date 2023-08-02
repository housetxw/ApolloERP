using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.FMS.Api.Core.Request
{
   public class RgAccountCheckConfirmRequest
    {
        public List<string> OrderNos { get; set; }

        public string RgCheckResult { get; set; }
        public int LocationId { get; set; }


        public string UpdateBy { get; set; }
    }
}
