using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetShopServiceInfoRequest
    {
        /// <summary>
        /// 门店服务ID
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "门店服务ID必须大于0")]
        public long Id { get; set; }
    }
}
