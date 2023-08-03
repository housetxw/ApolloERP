using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class InventoryFlowItemDTO
    {
        /// <summary>
        /// 序号
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 库存项目id
        /// </summary>
        public long InventoryId { get; set; }
        /// <summary>
        /// 来源单号
        /// </summary>
        public string SourceNo { get; set; } = string.Empty;
        /// <summary>
        /// 仓库id
        /// </summary>
        public long LocationId { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string LocationName { get; set; } = string.Empty;
        /// <summary>
        /// 商品名称Id
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; } = string.Empty;
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 品牌的Code
        /// </summary>
        public string BrandCode { get; set; } = string.Empty;
        /// <summary>
        /// 品牌的名称
        /// </summary>
        public string BrandName { get; set; } = string.Empty;
        /// <summary>
        /// 业务分类：上传库存、2：修改现货库存 3： 修改锁定库存 4：下单  5.释放库存
        /// </summary>
        public long BusinessCategory { get; set; }
        /// <summary>
        /// 业务分类编码
        /// </summary>
        public string BusinessCategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 业务分类名称
        /// </summary>
        public string BusinessCategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 一级分类Code
        /// </summary>
        public string FirstCategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 一级分类名称
        /// </summary>
        public string FirstCategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 二级分类Code
        /// </summary>
        public string SecondCategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 二级分类名称
        /// </summary>
        public string SecondCategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 三级分类Code
        /// </summary>
        public string ThirdCategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 三级分类名称
        /// </summary>
        public string ThirdCategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 四级分类Code
        /// </summary>
        public string FourCategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 四级分类名称
        /// </summary>
        public string FourCategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 五级商品分类code
        /// </summary>
        public string FiveCategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 五级商品分类名称
        /// </summary>
        public string FiveCategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 计量单位
        /// </summary>
        public string UomId { get; set; } = string.Empty;
        /// <summary>
        /// 计量单位文本
        /// </summary>
        public string UomText { get; set; } = string.Empty;
        /// <summary>
        /// 库存修改前总数量
        /// </summary>
        public long BeforeTotalNum { get; set; }
        /// <summary>
        /// 库存修改后总数量
        /// </summary>
        public long AfterTotalNum { get; set; }
        /// <summary>
        /// 库存总数改变数值
        /// </summary>
        public long ChangeTotalNum { get; set; }
        /// <summary>
        /// 改变之前可用库存
        /// </summary>
        public long BeforeCanUseNum { get; set; }
        /// <summary>
        /// 改变之后可用库存
        /// </summary>
        public long AfterCanUseNum { get; set; }
        /// <summary>
        /// 改变的可用库存
        /// </summary>
        public long ChangeCanUseNum { get; set; }
        /// <summary>
        /// 修改前占用库存
        /// </summary>
        public long BeforeOccupyNum { get; set; }
        /// <summary>
        /// 修改后占用库存
        /// </summary>
        public long AfterOccupyNum { get; set; }
        /// <summary>
        /// 修改占用库存
        /// </summary>
        public long ChangeOccupyNum { get; set; }
        /// <summary>
        /// 修改前不可销售库存（损坏的库存）
        /// </summary>
        public long BeforeNoSaleNum { get; set; }
        /// <summary>
        /// 修改后不可销售库存（损坏的库存）
        /// </summary>
        public long AfterNoSaleNum { get; set; }
        /// <summary>
        /// 修改不可销售库存（损坏的库存）
        /// </summary>
        public long ChangeNoSaleNum { get; set; }
        /// <summary>
        /// 修改前锁定的库存（活动/促销）
        /// </summary>
        public long BeforeLockNum { get; set; }
        /// <summary>
        /// 修改后锁定的库存（活动/促销）
        /// </summary>
        public long AfterLockNum { get; set; }
        /// <summary>
        /// 修改后虚拟库存（仓库中没有实物库存，实物库存来源于供应商）
        /// </summary>
        public long AfterInventedNum { get; set; }
        /// <summary>
        /// 修改前虚拟库存（仓库中没有实物库存，实物库存来源于供应商）
        /// </summary>
        public long BeforeInventedNum { get; set; }
        /// <summary>
        /// 修改锁定库存数量
        /// </summary>
        public long ChangeLockNum { get; set; }
        /// <summary>
        /// 修改前调配库存
        /// </summary>
        public long BeforeAllocationNum { get; set; }
        /// <summary>
        /// 修改后调配库存
        /// </summary>
        public long AfterAllocationNum { get; set; }
        /// <summary>
        /// 修改虚拟库存（仓库中没有实物库存，实物库存来源于供应商）
        /// </summary>
        public long ChangeInventedNum { get; set; }
        /// <summary>
        /// 修改调配库存
        /// </summary>
        public long ChangeAllocationNum { get; set; }
        /// <summary>
        /// 修改后调配中库存
        /// </summary>
        public long AfterAllocationingNum { get; set; }
        /// <summary>
        /// 修改前调配中库存
        /// </summary>
        public long BeforeAllocationingNum { get; set; }
        /// <summary>
        /// 修改调配中库存
        /// </summary>
        public long ChangeAllocationingNum { get; set; }
        /// <summary>
        /// 修改前安全库存设置
        /// </summary>
        public long BeforeSafeNum { get; set; }
        /// <summary>
        /// 修改后安全库存设置
        /// </summary>
        public long AfterSafeNum { get; set; }
        /// <summary>
        /// 修改安全库存
        /// </summary>
        public long ChangeSafeNum { get; set; }
        /// <summary>
        /// 修改前充足库存设置的数量
        /// </summary>
        public long BeforeEnoughNum { get; set; }
        /// <summary>
        /// 修改后充足库存设置的数量
        /// </summary>
        public long AfterEnoughNum { get; set; }
        /// <summary>
        /// 修改充足库存设置的数量
        /// </summary>
        public long ChangeEnoughNum { get; set; }
        /// <summary>
        /// 修改前紧张库存数量设置
        /// </summary>
        public long BeforeTightNum { get; set; }
        /// <summary>
        /// 修改后紧张库存数量设置
        /// </summary>
        public long AfterTightNum { get; set; }
        /// <summary>
        /// 修改紧张库存数量设置
        /// </summary>
        public long ChangeTightNum { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建用户ID
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

        /// <summary>
        /// 入/出
        /// </summary>
        public string Type { get; set; }
    }

}
