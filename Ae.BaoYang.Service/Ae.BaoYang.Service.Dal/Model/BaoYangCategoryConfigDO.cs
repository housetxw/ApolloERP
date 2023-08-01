using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Model
{
    [Table("bao_yang_category_config")]
    public class BaoYangCategoryConfigDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 分类英文代码
        /// </summary>
        [Column("category_alias")]
        public string CategoryAlias { get; set; }

        /// <summary>
        /// 类目名称
        /// </summary>
        [Column("category_name")]
        public string CategoryName { get; set; }

        /// <summary>
        /// 类目名称（简称）
        /// </summary>
        [Column("category_simple_name")]
        public string CategorySimpleName { get; set; }

        /// <summary>
        /// 保养包类型packageType
        /// </summary>
        [Column("package_type")]
        public string PackageType { get; set; }

        /// <summary>
        /// 排序显示
        /// </summary>
        [Column("display_index")]
        public int DisplayIndex { get; set; }

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
        /// 修改人
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
