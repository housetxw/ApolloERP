using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model.Config
{
    [Table("config_front_category_product")]
    public class ConfigFrontCategoryProductDo
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 分类id
        /// </summary>
        [Column("category_id")]
        public long CategoryId { get; set; }

        /// <summary>
        /// 对应产品/类目/品牌等id
        /// </summary>
        [Column("product_id")]
        public string ProductId { get; set; } = string.Empty;

        /// <summary>
        /// 关联类型：product/category/brand
        /// </summary>
        [Column("product_type")]
        public string ProductType { get; set; } = string.Empty;

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
