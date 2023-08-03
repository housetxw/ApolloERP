using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Model.AccountCheck
{
    public class PurchaseSettlementBatchDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 结算批次no
        /// </summary>
        public string SettlementBatchNo { get; set; } = string.Empty;
        /// <summary>
        /// 结算方式(现金  、月结)
        /// </summary>
        public string SettlementType { get; set; } = string.Empty;
        /// <summary>
        /// 结算年份
        /// </summary>
        public int SettlementYear { get; set; }
        /// <summary>
        /// 结算月份
        /// </summary>
        public int SettlementMonth { get; set; }
        /// <summary>
        /// 状态(0:未付款 1:付款失败 2：已付款)
        /// </summary>
        public sbyte Status { get; set; }
        /// <summary>
        /// 结算人员
        /// </summary>
        public string SettlementStaff { get; set; } = string.Empty;
        /// <summary>
        /// 申请人
        /// </summary>
        public string ApplyUser { get; set; } = string.Empty;
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 本期对账金额
        /// </summary>
        public decimal BillAmount { get; set; }
        /// <summary>
        /// 银行的名称
        /// </summary>
        public string BankName { get; set; } = string.Empty;
        /// <summary>
        /// 银行卡卡号
        /// </summary>
        public string BankNo { get; set; } = string.Empty;
        /// <summary>
        /// 最新备注
        /// </summary>
        public string TopRemark { get; set; } = string.Empty;
        /// <summary>
        /// 门店编号
        /// </summary>
        public long LocationId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string LocationName { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
