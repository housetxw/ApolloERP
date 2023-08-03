using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class GetReceivePackageRequest
    {
        public List<string> PackageNos { get; set; }

        public string CreateBy { get; set; }
    }
}
