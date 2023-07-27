using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetShopCarPageListRequest : BaseOnlyPageRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 车牌
        /// </summary>
        public string CarNumber { get; set; } = string.Empty;
        /// <summary>
        /// 状态 -1全部 0正常  1禁用
        /// </summary>
        public sbyte Status { get; set; }
    }
}
