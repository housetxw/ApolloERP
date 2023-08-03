using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;

namespace Ae.FMS.Service.Dal.Model.settlement
{
    [Table("settlement_batch")]
    public class SettlementBatchDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 结算批次no
        /// </summary>
        [Column("settlement_batch_no")]
        public string SettlementBatchNo { get; set; } = string.Empty;
        /// <summary>
        /// 状态(0:未付款 1:付款失败 2：已付款)
        /// </summary>
        [Column("status")]
        public sbyte Status { get; set; }
        /// <summary>
        /// 对账人
        /// </summary>
        [Column("check_user")]
        public string CheckUser { get; set; } = string.Empty;
        /// <summary>
        /// 申请人
        /// </summary>
        [Column("apply_user")]
        public string ApplyUser { get; set; } = string.Empty;
        /// <summary>
        /// 申请时间
        /// </summary>
        [Column("apply_time")]
        public DateTime ApplyTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 本期对账金额
        /// </summary>
        [Column("bill_amount")]
        public decimal BillAmount { get; set; }
        /// <summary>
        /// 银行的名称
        /// </summary>
        [Column("bank_name")]
        public string BankName { get; set; } = string.Empty;
        /// <summary>
        /// 银行的支行
        /// </summary>
        [Column("bank_branch")]
        public string BankBranch { get; set; } = string.Empty;
        /// <summary>
        /// 银行账户名
        /// </summary>
        [Column("bank_user")]
        public string BankUser { get; set; } = string.Empty;
        /// <summary>
        /// 银行卡卡号
        /// </summary>
        [Column("bank_no")]
        public string BankNo { get; set; } = string.Empty;
        /// <summary>
        /// 最新备注
        /// </summary>
        [Column("top_remark")]
        public string TopRemark { get; set; } = string.Empty;
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
        /// 省
        /// </summary>
        [Column("province_id")]
        public string ProvinceId { get; set; } = string.Empty;
        /// <summary>
        /// 市
        /// </summary>
        [Column("city_id")]
        public string CityId { get; set; } = string.Empty;
        /// <summary>
        /// 区
        /// </summary>
        [Column("district_id")]
        public string DistrictId { get; set; } = string.Empty;
        /// <summary>
        /// 省名称
        /// </summary>
        [Column("province")]
        public string Province { get; set; } = string.Empty;
        /// <summary>
        /// 市名称
        /// </summary>
        [Column("city")]
        public string City { get; set; } = string.Empty;
        /// <summary>
        /// 详细地址
        /// </summary>
        [Column("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// 区县名称
        /// </summary>
        [Column("district")]
        public string District { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
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
    }
}
