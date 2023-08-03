using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class InventoryDTO
    {
        /// <summary>
        /// 序号
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 仓库Id
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
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 来源类型
        /// </summary>
        public string SourceType { get; set; } = string.Empty;
        /// <summary>
        /// 品牌的Code
        /// </summary>
        public string BrandCode { get; set; } = string.Empty;
        /// <summary>
        /// 品牌的名称
        /// </summary>
        public string BrandName { get; set; } = string.Empty;
        /// <summary>
        /// 业务分类
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
        /// 库存总数量
        /// </summary>
        public long TotalNum { get; set; }
        public decimal TotalCost { get; set; }
        /// <summary>
        /// 可用库存
        /// </summary>
        public long CanUseNum { get; set; }
        /// <summary>
        /// 占用库存
        /// </summary>
        public long OccupyNum { get; set; }
        /// <summary>
        /// 不可销售库存（损坏的库存）
        /// </summary>
        public long NoSaleNum { get; set; }
        /// <summary>
        /// 锁定的库存（活动/促销）
        /// </summary>
        public long LockNum { get; set; }
        /// <summary>
        /// 虚拟库存（仓库中没有实物库存，实物库存来源于供应商）
        /// </summary>
        public long InventedNum { get; set; }
        /// <summary>
        /// 调配库存
        /// </summary>
        public long AllocationNum { get; set; }
        /// <summary>
        /// 调配中库存
        /// </summary>
        public long AllocationingNum { get; set; }
        /// <summary>
        /// 安全库存设置
        /// </summary>
        public long SafeNum { get; set; }
        /// <summary>
        /// 充足库存设置的数量
        /// </summary>
        public long EnoughNum { get; set; }
        /// <summary>
        /// 紧张库存数量设置
        /// </summary>
        public long TightNum { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string UomId { get; set; } = string.Empty;
        /// <summary>
        /// 计量单位文本
        /// </summary>
        public string UomText { get; set; } = string.Empty;
        /// <summary>
        /// 库存状态（状态：好、损毁、有缺陷)
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Remark { get; set; } = string.Empty;
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
    }

}
