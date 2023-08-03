using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.FMS.Service.Core.Request.Settlement
{
    /// <summary>
    /// 门店账户信息请求对象
    /// </summary>
    public class GetAccountInfoRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        [Required]
        public int ShopId { get; set; }

    }
}
