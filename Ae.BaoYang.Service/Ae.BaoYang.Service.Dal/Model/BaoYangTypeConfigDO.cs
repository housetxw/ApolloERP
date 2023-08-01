using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Model
{
    [Table("bao_yang_type_config")]
    public class BaoYangTypeConfigDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 保养类型BaoYangType
        /// </summary>
        [Column("bao_yang_type")]
        public string BaoYangType { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        [Column("display_name")]
        public string DisplayName { get; set; }
        /// <summary>
        /// 产品类目
        /// </summary>
        [Column("category_name")]
        public string CategoryName { get; set; }

        /// <summary>
        /// 最小商品类目 ,分割
        /// </summary>
        [Column("child_categories")]
        public string ChildCategories { get; set; }

        /// <summary>
        /// 组别
        /// </summary>
        [Column("type_group")]
        public string TypeGroup { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Column("image_url")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
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
        /// 更新人
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}
