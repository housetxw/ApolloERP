using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Request
{
   public class PackageProductClientRequest
    {
        public List<string> PackageNos { get; set; } = new List<string>();

        public long TransferId { get; set; }

        public string UpdateBy { get; set; }

        public string Status { get; set; }
    }
}
