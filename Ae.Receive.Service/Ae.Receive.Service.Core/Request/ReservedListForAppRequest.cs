using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    public class ReservedListForAppRequest : BaseGetRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 偏移值
        /// </summary>
        public int OffsetValue { get; set; }
        /// <summary>
        /// 搜索值
        /// </summary>
        public string SearchValue { get; set; }
    }
}
