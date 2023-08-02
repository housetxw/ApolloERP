using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.FMS.Api.Core.Response
{
    public class GetRegionChinaListByRegionIdResponse
    {
        public string RegionId { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string CountryCode { get; set; }
        public string Initial { get; set; }
        public string Spell { get; set; }
        public int Level { get; set; }
    }
}
