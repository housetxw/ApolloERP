using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class WarehouseConfigDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        public long CompanyId { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WarehouseName { get; set; } = string.Empty;
        /// <summary>
        /// 仓库类型(轮胎 保养 车品)
        /// </summary>
        public string WarehouseType { get; set; } = string.Empty;
        /// <summary>
        /// 状态(正常 待上线 检查)
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string WarehouseCode { get; set; } = string.Empty;
        /// <summary>
        /// 是否激活
        /// </summary>
        public sbyte IsActive { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }
}
