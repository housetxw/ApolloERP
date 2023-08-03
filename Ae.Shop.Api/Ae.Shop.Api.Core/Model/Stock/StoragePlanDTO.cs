using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class StoragePlanDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; } = string.Empty;
        public long WarehouseId { get; set; }
        public string WarehouseName { get; set; } = string.Empty;
        /// <summary>
        /// 计划编号
        /// </summary>
        public string PlanNo { get; set; } = string.Empty;
        /// <summary>
        /// 计划名称
        /// </summary>
        public string PlanName { get; set; } = string.Empty;
        /// <summary>
        /// 产品来源(总部产品(Company)，外采产品(Shop))
        /// </summary>
        public string SourceType { get; set; } = string.Empty;

        /// <summary>
        /// 盘点类型(指定产品 全盘)
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// 执行状态(新建 盘点中 盘点完成 差异处理中)
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 计划时间
        /// </summary>
        public DateTime PlanTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 实际完成时间
        /// </summary>
        public DateTime ActualTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0否 1是
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

        public List<StoragePlanProductDTO> StoragePlans { get; set; } = new List<StoragePlanProductDTO>();
    }
}