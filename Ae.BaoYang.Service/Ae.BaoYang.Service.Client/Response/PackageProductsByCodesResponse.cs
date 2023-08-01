using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Client.Response
{
    public class PackageProductsByCodesResponse
    {
        /// <summary>
        ///  套餐信息
        /// </summary>
        public PackageInfoVo PackageInfo { get; set; }

        /// <summary>
        /// 套餐明细
        /// </summary>

        public List<ProductPackageDetailVo> Details { get; set; }

        /// <summary>
        /// 如果只配了类型，是否有该类型适配的商品
        /// </summary>
        public bool HasAdaptionData { get; set; } = true;
    }

    public class PackageInfoVo
    {
        /// <summary>
        ///  套餐Id
        /// </summary>
        public string PackageId { get; set; }

        /// <summary>
        ///  产品编码
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 基准价格
        /// </summary>
        public decimal StandardPrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        ///// <summary>
        ///// 可用库存
        ///// </summary>
        //public int AvailableStockQuantity { get; set; }

        /// <summary>
        /// 是否标缺
        /// </summary>
        public bool StockOut { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        // /// <summary>
        // /// 购买过
        // /// </summary>
        // public bool Purchased { get; set; }
        //
        // /// <summary>
        // /// 排序权重
        // /// </summary>
        // public int OrderLevel
        // {
        //     get
        //     {
        //         int stock = 1000;
        //         int purchased = 10;
        //         //int adaptionCount = 1;
        //         if (!StockOut)
        //         {
        //             stock = 2000;
        //         }
        //
        //         if (Purchased)
        //         {
        //             purchased = 20;
        //         }
        //
        //         return stock + purchased;
        //     }
        // }
    }

    public class ProductPackageDetailVo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public string ProjectId { get; set; } = string.Empty;

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; } = string.Empty;

        /// <summary>
        /// 项目类型 1 产品PId 2 配件 （机油滤芯，燃油滤芯，汽油滤芯） 
        /// </summary>
        public int ProjectType { get; set; }

        /// <summary>
        /// 项目品牌
        /// </summary>
        public List<string> ProjectBrands { get; set; }

        /// <summary>
        /// 基准价格
        /// </summary>
        public decimal StandardPrice { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 产品数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 能否替换产品
        /// </summary>
        public sbyte ReplaceProduct { get; set; }

        /// <summary>
        /// 替换产品类目 逗号分隔,
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// 替换外采产品类目
        /// </summary>
        public string ShopCategoryId { get; set; }

        ///// <summary>
        ///// 可用库存
        ///// </summary>
        //public int AvailableStockQuantity { get; set; }
    }
}
