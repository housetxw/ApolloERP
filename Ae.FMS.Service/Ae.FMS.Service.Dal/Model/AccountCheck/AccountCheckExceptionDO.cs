using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.FMS.Service.Dal.Model
{
    [Table("account_check_exception")]
    public class AccountCheckExceptionDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 关联单号(订单号,采购单号)
        /// </summary>
        [Column("relation_no")]
        public string RelationNo { get; set; } = string.Empty;
        /// <summary>
        /// 关联类型(订单 采购单)
        /// </summary>
        [Column("relation_type")]
        public string RelationType { get; set; } = string.Empty;
        /// <summary>
        /// 上报类型
        /// </summary>
        [Column("report_type")]
        public string ReportType { get; set; } = string.Empty;
        /// <summary>
        /// 上报人
        /// </summary>
        [Column("report_by")]
        public string ReportBy { get; set; } = string.Empty;
        /// <summary>
        /// 门店编号
        /// </summary>
        [Column("location_id")]
        public long LocationId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        [Column("location_name")]
        public string LocationName { get; set; } = string.Empty;
        /// <summary>
        /// 上报原因
        /// </summary>
        [Column("report_reason")]
        public string ReportReason { get; set; } = string.Empty;
        /// <summary>
        /// 上报时间
        /// </summary>
        [Column("report_time")]
        public DateTime ReportTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 状态(新建 已审核...)
        /// </summary>
        [Column("status")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 异常处理意见
        /// </summary>
        [Column("suggestion")]
        public string Suggestion { get; set; } = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 安装时间
        /// </summary>
        [Column("install_time")]
        public DateTime InstallTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 付款方式
        /// </summary>
        [Column("pay_method")]
        public string PayMethod { get; set; } = string.Empty;
        /// <summary>
        /// 到账状态
        /// </summary>
        [Column("money_arrive_status")]
        public string MoneyArriveStatus { get; set; } = string.Empty;
        /// <summary>
        /// 订单类型
        /// </summary>
        [Column("order_type")]
        public string OrderType { get; set; } = string.Empty;

        /// <summary>
        /// 结算价
        /// </summary>
        [Column("settlement_fee")]
        public decimal SettlementFee { get; set; }
    }
}