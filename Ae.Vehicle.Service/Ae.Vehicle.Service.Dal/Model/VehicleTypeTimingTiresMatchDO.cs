using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Dal.Model
{
    [Table("vehicle_type_timing_tires_match")]
    public class VehicleTypeTimingTiresMatchDO
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
        /// 轮胎pid
        /// </summary>
        [Column("pid")]
        public string Pid { get; set; }

        /// <summary>
        /// 原配轮胎产品(无产品)
        /// </summary>
        [Column("no_product_name")]
        public string NoProductName { get; set; }

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
