using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GenerateAppletCodeRequest
    {
        public string Type { get; set; } = string.Empty;

        public long ShopId { get; set; }
    }
}
