using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.FMS.Service.Core.Request.Settlement
{
    /// <summary>
    /// 提现申请请求参数
    /// </summary>
    public class SubmitWithdrawalApplyRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        [Required]
        public  int ShopId { get; set; }

        /// <summary>
        /// 跟新者
        /// </summary>
        public string ApplyUser { get; set; }

        /// <summary>
        /// 提现的金额
        /// </summary>
        [Required]
        public decimal ApplyAmount { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        [Required]
        public string BankNo { get; set; }

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

    }
}
