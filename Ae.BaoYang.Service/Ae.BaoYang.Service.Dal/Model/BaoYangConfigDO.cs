using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Model
{
    [Table("bao_yang_config")]
    public class BaoYangConfigDO
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("config_name")]
        public string ConfigName { get; set; }

        [Column("config")]
        public string Config { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("create_by")]
        public string CreateBy { get; set; }

        [Column("create_time")]
        public DateTime CreateTime { get; set; }

        [Column("update_by")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}
