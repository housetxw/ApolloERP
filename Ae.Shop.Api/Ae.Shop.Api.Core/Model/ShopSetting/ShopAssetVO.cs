using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class ShopAssetVO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 资产编码
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 一级类别编号
        /// </summary>
        public string FirstTypeCode { get; set; } = string.Empty;
        /// <summary>
        /// 一级类别名称
        /// </summary>
        public string FirstTypeName { get; set; } = string.Empty;
        /// <summary>
        /// 二级类别编号
        /// </summary>
        public string SecondTypeCode { get; set; } = string.Empty;
        /// <summary>
        /// 二级列表名称
        /// </summary>
        public string SecondTypeName { get; set; } = string.Empty;
        /// <summary>
        /// 资产名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specification { get; set; } = string.Empty;
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 开始使用日期
        /// </summary>
        public DateTime StartUseDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 存放地点
        /// </summary>
        public string StorageLocation { get; set; } = string.Empty;
        /// <summary>
        /// 增加方式（0未设置 1购入 2转入）
        /// </summary>
        public sbyte AddMethod { get; set; }
        /// <summary>
        /// 责任人员工ID
        /// </summary>
        public string OwnerId { get; set; } = string.Empty;
        /// <summary>
        /// 使用状态（0未设置 1使用中 2已报废 3已调拨）
        /// </summary>
        public sbyte UseStatus { get; set; }
        /// <summary>
        /// 计划使用月份
        /// </summary>
        public int PlanUseMonths { get; set; }
        /// <summary>
        /// 原单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 估计折旧
        /// </summary>
        public decimal EstimateDepreciation { get; set; }
        /// <summary>
        /// 图片地址逗号拼接（最多五张）
        /// </summary>
        public string Images { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 是否删除 0否 1是
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
