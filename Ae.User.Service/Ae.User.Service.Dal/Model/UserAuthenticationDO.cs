using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Dal.Model
{
    [Table("user_authentication")]
    public class UserAuthenticationDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [Column("user_id")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 认证类型：默认0 二代身份证
        /// </summary>
        [Column("auth_type")]
        public int AuthType { get; set; }

        /// <summary>
        /// 认证姓名
        /// </summary>
        [Column("auth_name")]
        public string AuthName { get; set; } = string.Empty;

        /// <summary>
        /// 认证身份证号
        /// </summary>
        [Column("auth_id_card")]
        public string AuthIdCard { get; set; } = string.Empty;

        /// <summary>
        /// 删除标记
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
