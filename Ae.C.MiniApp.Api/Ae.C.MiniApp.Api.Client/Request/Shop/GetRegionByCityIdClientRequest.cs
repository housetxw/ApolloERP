using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request
{
    public class GetRegionByCityIdClientRequest
    {
        /// <summary>
        /// 市ID
        /// </summary>
        public long CityId { get; set; }
    }
}
