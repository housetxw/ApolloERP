using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Response.Settlement
{
    /// <summary>
    /// 门店账户信息的返回对象
    /// </summary>
    public class GetAccountInfoResponse
    {
        /// <summary>
        /// 的账余额
        /// </summary>
        public decimal AccountBalanceAmount { get; set; }

        /// <summary>
        /// 体现中金额
        /// </summary>
        public decimal WithdrawalingAmount { get; set; }

        /// <summary>
        /// 已提现金额
        /// </summary>
        public decimal HaveWithdrawalAmount { get; set; }


    }
}
