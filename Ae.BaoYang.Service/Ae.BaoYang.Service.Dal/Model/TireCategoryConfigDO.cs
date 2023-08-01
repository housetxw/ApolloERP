using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Model
{
    [Table("tire_category_config")]
    public class TireCategoryConfigDO
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("category_type")]
        public string CategoryType { get; set; }

        /// <summary>
        /// 类目id
        /// </summary>
        [Column("category_id")]
        public string CategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("display_name")]
        public string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("brief_description")]
        public string BriefDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("image_url")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 适配展示商品类型：0套餐  1单品  2套餐&单品
        /// </summary>
        [Column("product_type")]
        public int ProductType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}
