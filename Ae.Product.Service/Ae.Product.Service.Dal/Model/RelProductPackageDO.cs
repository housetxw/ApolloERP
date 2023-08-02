using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Dal.Model
{
    [Table("rel_product_package")]
    public class RelProductPackageDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [ApolloErp.Data.DapperExtensions.Att.Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 套餐pid
        /// </summary>
        [Column("package_pid")]
        public string PackagePid { get; set; } = string.Empty;
        /// <summary>
        /// 项目Id
        /// </summary>
        [Column("project_id")]
        public string ProjectId { get; set; } = string.Empty;
        /// <summary>
        /// 项目Id
        /// </summary>
        [Column("project_Name")]
        public string ProjectName { get; set; } = string.Empty;
        /// <summary>
        /// 项目类型 1 产品PId 2 配件 （机油滤芯，燃油滤芯，汽油滤芯） 
        /// </summary>
        [Column("project_type")]
        public sbyte ProjectType { get; set; }
        /// <summary>
        /// 项目品牌
        /// </summary>
        [Column("project_brand")]
        public string ProjectBrand { get; set; } = string.Empty;
        /// <summary>
        /// 基准价格
        /// </summary>
        [Column("standard_price")]
        public decimal StandardPrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        [Column("sales_price")]
        public decimal SalesPrice { get; set; }
        /// <summary>
        /// 产品数量
        /// </summary>
        [Column("quantity")]
        public int Quantity { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column("sort")]
        public int Sort { get; set; }

        /// <summary>
        /// 能否替换产品
        /// </summary>
        [Column("replace_product")]
        public sbyte ReplaceProduct { get; set; }

        /// <summary>
        /// 替换产品类目：最小类目
        /// </summary>
        [Column("category_id")]
        public string CategoryId { get; set; } = string.Empty;

        /// <summary>
        /// 替换外采产品类目：最小类目
        /// </summary>
        [Column("shop_category_id")]
        public string ShopCategoryId { get; set; } = string.Empty;

        /// <summary>
        /// 标记删除
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }

}