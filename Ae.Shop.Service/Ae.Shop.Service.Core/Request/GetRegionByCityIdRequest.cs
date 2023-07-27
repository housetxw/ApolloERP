using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetRegionByCityIdRequest
    {
        /// <summary>
        /// 市ID
        /// </summary>
        [Range(1, long.MaxValue, ErrorMessage = "市ID必须大于0")]
        public long CityId { get; set; }

        /// <summary>
        /// 是否展示门店数量
        /// </summary>
        public bool IsShowShopCount { get; set; } = true;
    }
}
