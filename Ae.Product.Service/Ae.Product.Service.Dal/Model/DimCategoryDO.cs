using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model
{

    [Table("dim_category")]
    public class DimCategoryDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 类目Code
        /// </summary>
        [Column("category_code")]
        public string CategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 类目Code缩写
        /// </summary>
        [Column("category_code_short")]
        public string CategoryCodeShort { get; set; } = string.Empty;
        /// <summary>
        /// 显示名
        /// </summary>
        [Column("display_name")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        [Column("description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 父级id
        /// </summary>
        [Column("parent_id")]
        public int ParentId { get; set; }
        /// <summary>
        /// 类型 1 后台类目 2 前台类目
        /// </summary>
        [Column("category_Type")]
        public int CategoryType { get; set; }
        /// <summary>
        /// 类型基本 
        /// </summary>
        [Column("category_level")]
        public int CategoryLevel { get; set; }
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