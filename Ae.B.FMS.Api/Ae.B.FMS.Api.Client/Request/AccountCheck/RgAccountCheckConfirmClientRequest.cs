using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.FMS.Api.Client.Request
{
   public class RgAccountCheckConfirmClientRequest
    {
        public List<string> OrderNos { get; set; }

        public string RgCheckResult { get; set; }
        public int LocationId { get; set; }

        public string UpdateBy { get; set; }
    }
}
