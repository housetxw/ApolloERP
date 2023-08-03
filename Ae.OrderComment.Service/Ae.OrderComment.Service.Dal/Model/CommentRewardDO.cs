using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
 
namespace Ae.OrderComment.Service.Dal.Model
{
    [Table("comment_reward")]
    public class CommentRewardDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; } 
        /// <summary>
        /// 点评Id
        /// </summary>
        [Column("comment_id")]
        public long CommentId { get; set; } 
        /// <summary>
        /// 用户Id
        /// </summary>
        [Column("user_id")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 奖励类型（0未设置 1返现 2积分 3鸡蛋 4鹅蛋）
        /// </summary>
        [Column("reward_type")]
        public sbyte RewardType { get; set; } 
        /// <summary>
        /// 奖励值
        /// </summary>
        [Column("reward_value")]
        public decimal RewardValue { get; set; } 
        /// <summary>
        /// 状态（0发起奖励 1已奖励 2退款收回）
        /// </summary>
        [Column("status")]
        public sbyte Status { get; set; } 
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