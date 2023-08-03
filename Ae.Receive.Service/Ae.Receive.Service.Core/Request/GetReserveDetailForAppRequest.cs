using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    public class GetReserveDetailForAppRequest : BaseGetRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 预约ID
        /// </summary>
        public long ReserveId { get; set; }
    }
}
