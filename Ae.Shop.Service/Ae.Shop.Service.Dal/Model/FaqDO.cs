using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
 
namespace Ae.Shop.Service.Dal.Model
{
    [Table("faq")]
    public class FaqDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; } 
        /// <summary>
        /// 渠道
        /// </summary>
        [Column("channel_id")]
        public long ChannelId { get; set; }
        /// <summary>
        /// 一级分类ID
        /// </summary>
        [Column("category_one_id")]
        public long CategoryOneId { get; set; }
        /// <summary>
        /// 二级分类ID
        /// </summary>
        [Column("category_two_id")]
        public long CategoryTwoId { get; set; }
        /// <summary>
        /// 三级分类ID
        /// </summary>
        [Column("category_three_id")]
        public long CategoryThreeId { get; set; } 
        /// <summary>
        /// 问题
        /// </summary>
        [Column("question")]
        public string Question { get; set; } = string.Empty;
        /// <summary>
        /// 回答
        /// </summary>
        [Column("answer")]
        public string Answer { get; set; } = string.Empty;
        /// <summary>
        /// 是否删除 0否 1是
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