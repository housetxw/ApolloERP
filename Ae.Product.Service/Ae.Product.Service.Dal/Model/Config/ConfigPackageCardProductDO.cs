using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model.Config
{
    [Table("config_package_card_product")]
    public class ConfigPackageCardProductDO
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 套餐卡pid
        /// </summary>
        [Column("pid")]
        public string Pid { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("rank")]
        public int Rank { get; set; }

        /// <summary>
        /// 类型：保养  美容 洗车等
        /// </summary>
        [Column("type")]
        public sbyte Type { get; set; }

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
