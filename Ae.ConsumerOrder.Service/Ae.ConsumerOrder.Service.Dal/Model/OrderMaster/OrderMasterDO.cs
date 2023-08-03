using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.ConsumerOrder.Service.Dal.Model
{
    [Table("order_master")]
    public class OrderMasterDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        [Column("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// 负责人标识
        /// </summary>
        [Column("person_id")]
        public string PersonId { get; set; } = string.Empty;
        /// <summary>
        /// 负责人姓名
        /// </summary>
        [Column("person_name")]
        public string PersonName { get; set; } = string.Empty;
        /// <summary>
        /// 负责人类型（Owner...）
        /// </summary>
        [Column("type")]
        public sbyte Type { get; set; }
        /// <summary>
        /// 开始跟进阶段（BeforeCreate...)
        /// </summary>
        [Column("follow_stage")]
        public string FollowStage { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
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
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
