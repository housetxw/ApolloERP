using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Model
{
    [Table("bao_yang_property_description")]
    public class BaoYangPropertyDescriptionDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("en_name")]
        public string EnName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("title")]
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("content")]
        public string Content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}
