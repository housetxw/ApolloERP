using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetShopServiceListRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "门店ID必须大于0")]
        public long ShopId { get; set; }
        /// <summary>
        /// 服务分类ID
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "服务分类ID必须大于0")]
        public long CategoryId { get; set; }
        /// <summary>
        /// 类型 0门店未开通过的服务;1 门店开通过的服务；
        /// </summary>
        [Range(0, 1, ErrorMessage = "服务类型不正确")]
        public int Type { get; set; }
    }
}
