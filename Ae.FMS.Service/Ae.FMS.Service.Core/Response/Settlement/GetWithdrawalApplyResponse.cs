using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Response.Settlement
{
    /// <summary>
    /// 得到提现申请返回对象
    /// </summary>
    public class GetWithdrawalApplyResponse
    {
        /// <summary>
        /// 银行账户
        /// </summary>
        public string BankNo { get; set; }

        /// <summary>
        /// 显示可提现的金额
        /// </summary>
        public decimal WithdrawalAmount { get; set; }

        /// <summary>
        /// 图片显示的IconUrl
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// 银行的名称
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 银行的支行
        /// </summary>
        public string BankBranch { get; set; } 
        /// <summary>
        /// 银行账户名
        /// </summary>
        public string BankUser { get; set; } 

        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
    }
}
