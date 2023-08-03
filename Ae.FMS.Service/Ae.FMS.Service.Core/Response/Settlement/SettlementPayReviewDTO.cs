using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Response
{
  public  class SettlementPayReviewDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; } = string.Empty;
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 结算批次id
        /// </summary>
        public long SettlementBatchId { get; set; }
        /// <summary>
        /// 结算批次no
        /// </summary>
        public string SettlementBatchNo { get; set; } = string.Empty;
        /// <summary>
        /// 状态(0:未付款 1：已付款 2：付款失败)
        /// </summary>
        public sbyte Status { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0未删除 1已删除
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
