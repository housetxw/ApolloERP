using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Dal.Model
{
    [Table("vehicle_type_timing_id_map")]
    public class VehicleTypeTimingIdMapDo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 五级车型tid
        /// </summary>
        [Column("tid")]
        public string Tid { get; set; }
        /// <summary>
        /// 扩展Id
        /// </summary>
        [Column("external_id")]
        public string ExternalId { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        [Column("source")]
        public string Source { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
