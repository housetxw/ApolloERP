using System;
using System.Collections.Generic;
using ApolloErp.Data.DapperExtensions.Att;
using System.Text;

namespace Ae.Product.Service.Dal.Model
{
    [Table("dim_shop_bisicservice")]
    public class DimShopBisicserviceDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 类目Id
        /// </summary>
        [Column("category_id")]
        public int CategoryId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 产品显示名称
        /// </summary>
        [Column("display_name")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 产品描述
        /// </summary>
        [Column("description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 市场价格
        /// </summary>
        [Column("standard_price")]
        public decimal StandardPrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        [Column("sales_price")]
        public decimal SalesPrice { get; set; }
        /// <summary>
        /// 缩率图
        /// </summary>
        [Column("icon")]
        public string Icon { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0否 1是
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
