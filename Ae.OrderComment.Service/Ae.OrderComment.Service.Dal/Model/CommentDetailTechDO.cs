using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.OrderComment.Service.Dal.Model
{
    [Table("comment_detail_tech")]
    public class CommentDetailTechDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 评论Id
        /// </summary>
        [Column("comment_id")]
        public long CommentId { get; set; }
        /// <summary>
        /// 技师员工Id
        /// </summary>
        [Column("employee_id")]
        public string EmployeeId { get; set; } = string.Empty;
        /// <summary>
        /// 技师名称
        /// </summary>
        [Column("tech_name")]
        public string TechName { get; set; } = string.Empty;
        /// <summary>
        /// 技师级别
        /// </summary>
        [Column("tech_level")]
        public int TechLevel { get; set; }
        /// <summary>
        /// 技师头像
        /// </summary>
        [Column("tech_head_url")]
        public string TechHeadUrl { get; set; } = string.Empty;
        /// <summary>
        /// 分值
        /// </summary>
        [Column("score")]
        public int Score { get; set; }
        /// <summary>
        /// 是否匿名（对技师评价默认是）
        /// </summary>
        [Column("is_anonymous")]
        public sbyte IsAnonymous { get; set; }
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