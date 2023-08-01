using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Model
{

    /// <summary>
    /// VehicleFuelTypeConfigDO
    /// </summary>
    [Table("vehicle_fuel_type_config")]
    public class VehicleFuelTypeConfigDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 机油类别
        /// </summary>
        [Column("fuel_type")]
        public string FuelType { get; set; }

        /// <summary>
        /// 不支持的packageType
        /// </summary>
        [Column("not_supported_types")]
        public string NotSupportedTypes { get; set; }

        /// <summary>
        /// 标记删除
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
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}
