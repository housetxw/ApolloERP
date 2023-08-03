using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("shop_asset")]
    public class ShopAssetDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        [NotUpdate]
        public long Id { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        [Column("shop_id")]
        [NotUpdate]
        public long ShopId { get; set; }
        /// <summary>
        /// 资产编码
        /// </summary>
        [Column("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 一级类别编号
        /// </summary>
        [Column("first_type_code")]
        [NotUpdate]
        public string FirstTypeCode { get; set; } = string.Empty;
        /// <summary>
        /// 二级类别编号
        /// </summary>
        [Column("second_type_code")]
        [NotUpdate]
        public string SecondTypeCode { get; set; } = string.Empty;
        /// <summary>
        /// 资产名称
        /// </summary>
        [Column("name")]
        [NotUpdate]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 规格型号
        /// </summary>
        [Column("specification")]
        [NotUpdate]
        public string Specification { get; set; } = string.Empty;
        /// <summary>
        /// 数量
        /// </summary>
        [Column("number")]
        [NotUpdate]
        public int Number { get; set; }
        /// <summary>
        /// 开始使用日期
        /// </summary>
        [Column("start_use_date")]
        [NotUpdate]
        public DateTime StartUseDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 存放地点
        /// </summary>
        [Column("storage_location")]
        public string StorageLocation { get; set; } = string.Empty;
        /// <summary>
        /// 增加方式（0未设置 1购入 2转入）
        /// </summary>
        [Column("add_method")]
        [NotUpdate]
        public sbyte AddMethod { get; set; }
        /// <summary>
        /// 责任人员工ID
        /// </summary>
        [Column("owner_id")]
        public string OwnerId { get; set; } = string.Empty;
        /// <summary>
        /// 使用状态（0未设置 1使用中 2已报废 3已调拨）
        /// </summary>
        [Column("use_status")]
        public sbyte UseStatus { get; set; }
        /// <summary>
        /// 计划使用月份
        /// </summary>
        [Column("plan_use_months")]
        [NotUpdate]
        public int PlanUseMonths { get; set; }
        /// <summary>
        /// 原单价
        /// </summary>
        [Column("price")]
        [NotUpdate]
        public decimal Price { get; set; }
        /// <summary>
        /// 估计折旧
        /// </summary>
        [Column("estimate_depreciation")]
        [NotUpdate]
        public decimal EstimateDepreciation { get; set; }
        /// <summary>
        /// 图片地址逗号拼接（最多五张）
        /// </summary>
        [Column("images")]
        public string Images { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 是否删除 0否 1是
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        [NotUpdate]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        [NotUpdate]
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
