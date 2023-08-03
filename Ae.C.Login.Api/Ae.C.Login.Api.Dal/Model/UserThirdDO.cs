using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.C.Login.Api.Dal.Model
{
    [Table("user_third")]
    public class UserThirdDO
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("user_id")]
        public string UserId { get; set; }
        [Column("open_id")]
        public string OpenId { get; set; } = string.Empty;
        [Column("login_type")]
        public int LoginType { get; set; }
        /// <summary>
        /// 是否删除
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
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = DateTime.Now;

    }
}
