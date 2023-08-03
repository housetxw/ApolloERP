using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
 
namespace Ae.OrderComment.Service.Dal.Model
{
    [Table("comment_label_config")]
    public class CommentLabelConfigDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; } 
        /// <summary>
        /// 评论明细类型（0未设置 1技师 2门店 3商品）
        /// </summary>
        [Column("comment_detail_type")]
        public sbyte CommentDetailType { get; set; } 
        /// <summary>
        /// 五级分值
        /// </summary>
        [Column("score")]
        public int Score { get; set; } 
        /// <summary>
        /// 标签名称
        /// </summary>
        [Column("label_name")]
        public string LabelName { get; set; } = string.Empty;
        /// <summary>
        /// 是否有效
        /// </summary>
        [Column("is_valid")]
        public sbyte IsValid { get; set; } 
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