using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.FMS.Api.Client.Request
{
    public class AccountCheckExceptionClientRequest
    { /// <summary>
      /// 主键
      /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 关联单号(订单号,采购单号)
        /// </summary>
        public string RelationNo { get; set; } = string.Empty;
        /// <summary>
        /// 关联类型(订单 采购单)
        /// </summary>
        public string RelationType { get; set; } = string.Empty;
        /// <summary>
        /// 上报类型
        /// </summary>
        public string ReportType { get; set; } = string.Empty;
        /// <summary>
        /// 上报人
        /// </summary>
        public string ReportBy { get; set; } = string.Empty;
        /// <summary>
        /// 门店编号
        /// </summary>
        public long LocationId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string LocationName { get; set; } = string.Empty;
        /// <summary>
        /// 上报原因
        /// </summary>
        public string ReportReason { get; set; } = string.Empty;
        /// <summary>
        /// 上报时间
        /// </summary>
        public DateTime ReportTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 状态(新建 已审核...)
        /// </summary>
        public string Status { get; set; } = string.Empty;
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
