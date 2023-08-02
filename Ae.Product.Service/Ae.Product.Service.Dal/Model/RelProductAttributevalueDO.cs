using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model
{

    [Table("rel_product_attributevalue")]
    public class RelProductAttributevalueDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 商品id
        /// </summary>
        [Column("product_id")]
        public string ProductId { get; set; }
        /// <summary>
        /// 属性名id
        /// </summary>
        [Column("attribute_name_id")]
        public int AttributeNameId { get; set; }
        /// <summary>
        /// 商品属性值
        /// </summary>
        [Column("product_attribute_value")]
        public string ProductAttributeValue { get; set; }
        /// <summary>
        /// 标记删除
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } 
        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } 
    }
}