using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Dal.Model.settlement
{
    /// <summary>
    /// 门店结算账户DO
    /// </summary>
    public class AccountSettlementDO
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
