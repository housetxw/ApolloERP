using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Client.Model
{
    public class ShopProductDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 类目Id
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        ///  产品编码
        /// </summary>
        public string ProductCode { get; set; } = string.Empty;
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 产品显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 市场价格
        /// </summary>
        public decimal StandardPrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 采购价
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// 采购成本价格
        /// </summary>
        public decimal PurchaseCost { get; set; }

        /// <summary>
        /// 1 置顶  2 按照上架时间顺序
        /// </summary>
        public int SortType { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public int IsTop { get; set; }
        /// <summary>
        /// 上下架
        /// </summary>
        public int OnSale { get; set; }
        /// <summary>
        /// 上架时间
        /// </summary>
        public DateTime OnSaleTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 缩率图
        /// </summary>
        public string Icon { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0否 1是
        /// </summary>
        public sbyte IsDeleted { get; set; }

        #region 类目信息

        /// <summary>
        /// 一级类目Id
        /// </summary>
        public int MainCategoryId { get; set; }

        /// <summary>
        /// 顶级类目Code
        /// </summary>
        public string MainCategoryCode { get; set; }
        /// <summary>
        /// 顶级类目ShortCode
        /// </summary>
        public string MainCategoryShortCode { get; set; }
        /// <summary>
        /// 顶级类目名称
        /// </summary>
        public string MainCategoryName { get; set; }


        /// <summary>
        /// 二级类目Id
        /// </summary>
        public int SecondCategoryId { get; set; }
        /// <summary>
        /// 关联类目Code
        /// </summary>
        public string SecondCategoryCode { get; set; }

        /// <summary>
        /// 关联类目ShortCode
        /// </summary>
        public string SecondCategoryShortCode { get; set; }
        /// <summary>
        /// 关联类目名称
        /// </summary>
        public string SecondCategoryName { get; set; }


        /// <summary>
        /// 三级类目Id
        /// </summary>
        public int ChildCategoryId { get; set; }

        /// <summary>
        /// 关联类目Code
        /// </summary>
        public string ChildCategoryCode { get; set; }

        /// <summary>
        /// 关联类目ShortCode
        /// </summary>
        public string ChildCategoryShortCode { get; set; }

        /// <summary>
        /// 关联类目名称
        /// </summary>
        public string ChildCategoryName { get; set; }

        #endregion
    }
}
