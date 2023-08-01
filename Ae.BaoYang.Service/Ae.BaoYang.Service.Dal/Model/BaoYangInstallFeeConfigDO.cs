using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Model
{
    [Table("bao_yang_install_fee_config")]
    public class BaoYangInstallFeeConfigDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 服务pid
        /// </summary>
        [Column("service_id")]
        public string ServiceId { get; set; } = string.Empty;

        /// <summary>
        /// 车辆最小指导价
        /// </summary>
        [Column("car_min_price")]
        public decimal CarMinPrice { get; set; }

        /// <summary>
        /// 车辆最大指导价
        /// </summary>
        [Column("car_max_price")]
        public decimal CarMaxPrice { get; set; }

        /// <summary>
        /// 加价金额
        /// </summary>
        [Column("additional_price")]
        public decimal AdditionalPrice { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 删除标记
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

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
