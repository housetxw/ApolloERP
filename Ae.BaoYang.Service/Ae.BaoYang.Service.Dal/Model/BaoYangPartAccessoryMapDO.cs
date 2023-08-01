using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Table("bao_yang_part_accessory_map")]
    public class BaoYangPartAccessoryMapDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 保养类型
        /// </summary>
        [Column("bao_yang_type")]
        public string BaoYangType { get; set; }

        /// <summary>
        /// 辅料名称
        /// </summary>
        [Column("accessory_names")]
        public string AccessoryNames { get; set; }

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
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}
