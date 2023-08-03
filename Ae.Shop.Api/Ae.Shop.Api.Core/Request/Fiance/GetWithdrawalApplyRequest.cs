using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Fiance
{
    /// <summary>
    /// 开始提现申请的信息请求对象
    /// </summary>
    public class GetWithdrawalApplyRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        [Required]
        public int ShopId { get; set; }
    }
}
