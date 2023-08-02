using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.FMS.Api.Core.Response
{
    public class GetSettlementListResponse
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
        /// 状态(0:未付款 1:付款失败 2：已付款)
        /// </summary>
        public sbyte Status { get; set; }

        public string StatusStr { get; set; }

        /// <summary>
        /// 队对账人
        /// </summary>
        public string CheckUser { get; set; } = string.Empty;
        /// <summary>
        /// 申请人
        /// </summary>
        public string ApplyUser { get; set; } = string.Empty;
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; } = new DateTime(1900, 1, 1);

        public string ApplyTimeStr { get; set; }
        /// <summary>
        /// 本期对账金额
        /// </summary>
        public decimal BillAmount { get; set; }
        /// <summary>
        /// 银行的名称
        /// </summary>
        public string BankName { get; set; } = string.Empty;
        /// <summary>
        /// 银行的支行
        /// </summary>
        public string BankBranch { get; set; } = string.Empty;
        /// <summary>
        /// 银行账户名
        /// </summary>
        public string BankUser { get; set; } = string.Empty;
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
        /// 省
        /// </summary>
        public string ProvinceId { get; set; } = string.Empty;
        /// <summary>
        /// 市
        /// </summary>
        public string CityId { get; set; } = string.Empty;
        /// <summary>
        /// 区
        /// </summary>
        public string DistrictId { get; set; } = string.Empty;
        /// <summary>
        /// 省名称
        /// </summary>
        public string Province { get; set; } = string.Empty;
        /// <summary>
        /// 市名称
        /// </summary>
        public string City { get; set; } = string.Empty;
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// 区县名称
        /// </summary>
        public string District { get; set; } = string.Empty;
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

        public string CreateTimeStr { get; set; }

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
