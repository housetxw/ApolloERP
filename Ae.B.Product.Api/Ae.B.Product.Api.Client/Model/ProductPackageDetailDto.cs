using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model
{
    /// <summary>
    /// 套餐明细
    /// </summary>
    public class ProductPackageDetailDto
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
        public sbyte ProjectType { get; set; }
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

        /// <summary>
        /// 删除标识 0否 1是
        /// </summary>
        public sbyte IsDeleted { get; set; }
    }
}
