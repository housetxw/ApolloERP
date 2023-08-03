using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Response.Fiance
{
    /// <summary>
    /// 提现记录列表返回对象
    /// </summary>
    public class GetWithdrawalRecordListResponse
    {
        /// <summary>
        /// 账单金额
        /// </summary>
        public decimal BillAmount { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public SByte Status { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string SettlementBatchNo { get; set; }

        /// <summary>
        /// 申请的时间
        /// </summary>
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string ApplyUser { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
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
