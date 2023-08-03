using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;


namespace Ae.Shop.Api.Dal.Model
{
    [Table("inventory")]
    public class InventoryDO
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 仓库Id
        /// </summary>
        [Column("location_id")]
        public long LocationId { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        [Column("location_name")]
        public string LocationName { get; set; } = string.Empty;
        /// <summary>
        /// 商品名称Id
        /// </summary>
        [Column("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 商品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 来源类型
        /// </summary>
        [Column("source_type")]
        public string SourceType { get; set; } = string.Empty;
        /// <summary>
        /// 品牌的Code
        /// </summary>
        [Column("brand_code")]
        public string BrandCode { get; set; } = string.Empty;
        /// <summary>
        /// 品牌的名称
        /// </summary>
        [Column("brand_name")]
        public string BrandName { get; set; } = string.Empty;
        /// <summary>
        /// 业务分类
        /// </summary>
        [Column("business_category")]
        public long BusinessCategory { get; set; }
        /// <summary>
        /// 业务分类编码
        /// </summary>
        [Column("business_category_code")]
        public string BusinessCategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 业务分类名称
        /// </summary>
        [Column("business_category_name")]
        public string BusinessCategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 一级分类Code
        /// </summary>
        [Column("first_category_code")]
        public string FirstCategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 一级分类名称
        /// </summary>
        [Column("first_category_name")]
        public string FirstCategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 二级分类Code
        /// </summary>
        [Column("second_category_code")]
        public string SecondCategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 二级分类名称
        /// </summary>
        [Column("second_category_name")]
        public string SecondCategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 三级分类Code
        /// </summary>
        [Column("third_category_code")]
        public string ThirdCategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 三级分类名称
        /// </summary>
        [Column("third_category_name")]
        public string ThirdCategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 四级分类Code
        /// </summary>
        [Column("four_category_code")]
        public string FourCategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 四级分类名称
        /// </summary>
        [Column("four_category_name")]
        public string FourCategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 五级商品分类code
        /// </summary>
        [Column("five_category_code")]
        public string FiveCategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 五级商品分类名称
        /// </summary>
        [Column("five_category_name")]
        public string FiveCategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 库存总数量
        /// </summary>
        [Column("total_num")]
        public long TotalNum { get; set; }

        /// <summary>
        /// 库存总金额(每个批次的金额累加)
        /// </summary>
        [Column("total_cost")]
        public decimal TotalCost { get; set; }

        /// <summary>
        /// 可用库存
        /// </summary>
        [Column("can_use_num")]
        public long CanUseNum { get; set; }
        /// <summary>
        /// 占用库存
        /// </summary>
        [Column("occupy_num")]
        public long OccupyNum { get; set; }
        /// <summary>
        /// 不可销售库存（损坏的库存）
        /// </summary>
        [Column("no_sale_num")]
        public long NoSaleNum { get; set; }
        /// <summary>
        /// 锁定的库存（活动/促销）
        /// </summary>
        [Column("lock_num")]
        public long LockNum { get; set; }
        /// <summary>
        /// 虚拟库存（仓库中没有实物库存，实物库存来源于供应商）
        /// </summary>
        [Column("invented_num")]
        public long InventedNum { get; set; }
        /// <summary>
        /// 调配库存
        /// </summary>
        [Column("allocation_num")]
        public long AllocationNum { get; set; }
        /// <summary>
        /// 调配中库存
        /// </summary>
        [Column("allocationing_num")]
        public long AllocationingNum { get; set; }
        /// <summary>
        /// 安全库存设置
        /// </summary>
        [Column("safe_num")]
        public long SafeNum { get; set; }
        /// <summary>
        /// 充足库存设置的数量
        /// </summary>
        [Column("enough_num")]
        public long EnoughNum { get; set; }
        /// <summary>
        /// 紧张库存数量设置
        /// </summary>
        [Column("tight_num")]
        public long TightNum { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        [Column("uom_id")]
        public string UomId { get; set; } = string.Empty;
        /// <summary>
        /// 计量单位文本
        /// </summary>
        [Column("uom_text")]
        public string UomText { get; set; } = string.Empty;
        /// <summary>
        /// 库存状态（状态：好、损毁、有缺陷)
        /// </summary>
        [Column("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 描述信息
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建用户ID
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
