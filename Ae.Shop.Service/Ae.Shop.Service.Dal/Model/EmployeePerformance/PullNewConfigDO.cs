using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Service.Dal.Model
{
    [Table("pull_new_config")]
    public class PullNewConfigDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 拉新开关
        /// </summary>
        [Column("pull_new_flag")]
        public sbyte PullNewFlag { get; set; }
        /// <summary>
        /// 拉新开关修改时间
        /// </summary>
        [Column("pull_new_time")]
        public DateTime PullNewTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 拉新奖励点数
        /// </summary>
        [Column("pull_new_point")]
        public decimal PullNewPoint { get; set; }
        /// <summary>
        /// 首次消费开关
        /// </summary>
        [Column("first_consume_flag")]
        public sbyte FirstConsumeFlag { get; set; }
        /// <summary>
        /// 消费配置类型(按比例1,固定金额2)
        /// </summary>
        [Column("first_consume_type")]
        public sbyte FirstConsumeType { get; set; }
        /// <summary>
        /// 绩效点/固定金额
        /// </summary>
        [Column("first_consume_point")]
        public decimal FirstConsumePoint { get; set; }
        /// <summary>
        /// 首次消费配置修改时间
        /// </summary>
        [Column("first_consume_time")]
        public DateTime FirstConsumeTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 是否生效(总开关)
        /// </summary>
        [Column("is_active")]
        public sbyte IsActive { get; set; }
        /// <summary>
        /// 删除标识 0未删除 1已删除
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