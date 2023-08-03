using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model.Arrival
{
    [Table("quque_number_generator")]
    public class QuqueNumberGeneratorDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 发号类型（0：未设置 1：预约 2：到店）
        /// </summary>
        [Column("generator_type")]
        public sbyte GeneratorType { get; set; }
        /// <summary>
        /// 发号器发号的日期
        /// </summary>
        [Column("generator_date")]
        public DateTime GeneratorDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 当前发号的号码
        /// </summary>
        [Column("current_number")]
        public int CurrentNumber { get; set; }
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
