using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("inventory_flow_item")]
    public class InventoryFlowItemDO
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 库存项目id
        /// </summary>
        [Column("inventory_id")]
        public long InventoryId { get; set; }
        /// <summary>
        /// 来源单号 总单号
        /// </summary>
        [Column("source_no")]
        public string SourceNo { get; set; } = string.Empty;

        /// <summary>
        /// 来源单号 采购子单号/入库子单号
        /// </summary>
        [Column("source_inventory_no")]
        public string SourceInventoryNo { get; set; } = string.Empty;
        /// <summary>
        /// 仓库id
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
        /// 批次号
        /// </summary>
        [Column("batch_no")]
        public string BatchNo { get; set; } = string.Empty;
        /// <summary>
        /// 商品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
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
        /// 业务分类：上传库存、2：修改现货库存 3： 修改锁定库存 4：下单
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
        /// 库存修改前总数量
        /// </summary>
        [Column("before_total_num")]
        public long BeforeTotalNum { get; set; }
        /// <summary>
        /// 修改后库存总数量
        /// </summary>
        [Column("after_total_num")]
        public long AfterTotalNum { get; set; }
        /// <summary>
        /// 库存总数改变数值
        /// </summary>
        [Column("change_total_num")]
        public long ChangeTotalNum { get; set; }
        /// <summary>
        /// 改变之前可用库存
        /// </summary>
        [Column("before_can_use_num")]
        public long BeforeCanUseNum { get; set; }
        /// <summary>
        /// 改变之后可用库存
        /// </summary>
        [Column("after_can_use_num")]
        public long AfterCanUseNum { get; set; }
        /// <summary>
        /// 该边的可用库存
        /// </summary>
        [Column("change_can_use_num")]
        public long ChangeCanUseNum { get; set; }
        /// <summary>
        /// 修改前占用库存
        /// </summary>
        [Column("before_occupy_num")]
        public long BeforeOccupyNum { get; set; }
        /// <summary>
        /// 修改后占用库存
        /// </summary>
        [Column("after_occupy_num")]
        public long AfterOccupyNum { get; set; }
        /// <summary>
        /// 修改占用库存
        /// </summary>
        [Column("change_occupy_num")]
        public long ChangeOccupyNum { get; set; }
        /// <summary>
        /// 修改前不可销售库存（损坏的库存）
        /// </summary>
        [Column("before_no_sale_num")]
        public long BeforeNoSaleNum { get; set; }
        /// <summary>
        /// 修改后不可销售库存（损坏的库存）
        /// </summary>
        [Column("after_no_sale_num")]
        public long AfterNoSaleNum { get; set; }
        /// <summary>
        /// 修改不可销售库存（损坏的库存）
        /// </summary>
        [Column("change_no_sale_num")]
        public long ChangeNoSaleNum { get; set; }
        /// <summary>
        /// 修改前锁定的库存（活动/促销）
        /// </summary>
        [Column("before_lock_num")]
        public long BeforeLockNum { get; set; }
        /// <summary>
        /// 修改后锁定的库存（活动/促销）
        /// </summary>
        [Column("after_lock_num")]
        public long AfterLockNum { get; set; }
        /// <summary>
        /// 修改后虚拟库存（仓库中没有实物库存，实物库存来源于供应商）
        /// </summary>
        [Column("after_invented_num")]
        public long AfterInventedNum { get; set; }
        /// <summary>
        /// 修改前虚拟库存（仓库中没有实物库存，实物库存来源于供应商）
        /// </summary>
        [Column("before_invented_num")]
        public long BeforeInventedNum { get; set; }
        /// <summary>
        /// 修改锁定库存数量
        /// </summary>
        [Column("change_lock_num")]
        public long ChangeLockNum { get; set; }
        /// <summary>
        /// 修改前调配库存
        /// </summary>
        [Column("before_allocation_num")]
        public long BeforeAllocationNum { get; set; }
        /// <summary>
        /// 修改后调配库存
        /// </summary>
        [Column("after_allocation_num")]
        public long AfterAllocationNum { get; set; }
        /// <summary>
        /// 修改虚拟库存（仓库中没有实物库存，实物库存来源于供应商）
        /// </summary>
        [Column("change_invented_num")]
        public long ChangeInventedNum { get; set; }
        /// <summary>
        /// 修改调配库存
        /// </summary>
        [Column("change_allocation_num")]
        public long ChangeAllocationNum { get; set; }
        /// <summary>
        /// 修改后调配中库存
        /// </summary>
        [Column("after_allocationing_num")]
        public long AfterAllocationingNum { get; set; }
        /// <summary>
        /// 修改前调配中库存
        /// </summary>
        [Column("before_allocationing_num")]
        public long BeforeAllocationingNum { get; set; }
        /// <summary>
        /// 修改调配中库存
        /// </summary>
        [Column("change_allocationing_num")]
        public long ChangeAllocationingNum { get; set; }
        /// <summary>
        /// 修改前安全库存设置
        /// </summary>
        [Column("before_safe_num")]
        public long BeforeSafeNum { get; set; }
        /// <summary>
        /// 修改后安全库存设置
        /// </summary>
        [Column("after_safe_num")]
        public long AfterSafeNum { get; set; }
        /// <summary>
        /// 修改安全库存
        /// </summary>
        [Column("change_safe_num")]
        public long ChangeSafeNum { get; set; }
        /// <summary>
        /// 修改前充足库存设置的数量
        /// </summary>
        [Column("before_enough_num")]
        public long BeforeEnoughNum { get; set; }
        /// <summary>
        /// 修改后充足库存设置的数量
        /// </summary>
        [Column("after_enough_num")]
        public long AfterEnoughNum { get; set; }
        /// <summary>
        /// 修改充足库存设置的数量
        /// </summary>
        [Column("change_enough_num")]
        public long ChangeEnoughNum { get; set; }
        /// <summary>
        /// 修改前紧张库存数量设置
        /// </summary>
        [Column("before_tight_num")]
        public long BeforeTightNum { get; set; }
        /// <summary>
        /// 修改后紧张库存数量设置
        /// </summary>
        [Column("after_tight_num")]
        public long AfterTightNum { get; set; }
        /// <summary>
        /// 修改紧张库存数量设置
        /// </summary>
        [Column("change_tight_num")]
        public long ChangeTightNum { get; set; }
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

        [NotMapped]
        public List<long> StockIds { get; set; }
    }

}
