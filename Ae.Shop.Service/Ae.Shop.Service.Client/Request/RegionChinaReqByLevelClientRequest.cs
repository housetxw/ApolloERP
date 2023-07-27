using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Request
{
    public class RegionChinaReqByLevelClientRequest
    {
        public RegionChinaType Level { get; set; }
    }
    public enum RegionChinaType
    {
        Province = 1,
        City = 2,
        District = 3,
    }
}
