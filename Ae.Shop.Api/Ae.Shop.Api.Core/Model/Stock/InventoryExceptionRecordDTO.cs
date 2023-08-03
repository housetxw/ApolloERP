using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class InventoryExceptionRecordDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 铺货单号
        /// </summary>
        public string TransferId { get; set; } = string.Empty;
        /// <summary>
        /// 铺货类型(铺货,调拨)
        /// </summary>
        public string TransferType { get; set; } = string.Empty;
        /// <summary>
        /// 铺货产品单号
        /// </summary>
        public long TransferProductId { get; set; }
        /// <summary>
        /// 包裹单号
        /// </summary>
        public string PackageNo { get; set; } = string.Empty;
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 货损数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 货损类型
        /// </summary>
        public string ExceptionType { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 状态(新建,已处理)
        /// </summary>
        public string Status { get; set; } = string.Empty;
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