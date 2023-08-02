using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model.Config
{
    [Table("config_hot_product")]
    public class ConfigHotProductDo
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 产品pid
        /// </summary>
        [Column("pid")]
        public string Pid { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        [Column("rank")]
        public int Rank { get; set; }

        /// <summary>
        /// 终端类型
        /// </summary>
        [Column("terminal_type")]
        public string TerminalType { get; set; } = string.Empty;

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
