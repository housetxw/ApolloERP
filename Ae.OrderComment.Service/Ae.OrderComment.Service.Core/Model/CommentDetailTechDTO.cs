using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Model
{
    public class CommentDetailTechDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 评论Id
        /// </summary>
        public long CommentId { get; set; }
        /// <summary>
        /// 技师员工Id
        /// </summary>
        public string EmployeeId { get; set; } = string.Empty;
        /// <summary>
        /// 技师名称
        /// </summary>
        public string TechName { get; set; } = string.Empty;
        /// <summary>
        /// 技师级别
        /// </summary>
        public int TechLevel { get; set; } 
        /// <summary>
        /// 技师头像
        /// </summary>
        public string TechHeadUrl { get; set; } = string.Empty;
        /// <summary>
        /// 分值
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 是否匿名（对技师评价默认是）
        /// </summary>
        public sbyte IsAnonymous { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}