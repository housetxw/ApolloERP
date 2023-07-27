using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using Ae.B.Login.Api.Core.Model;

namespace Ae.B.Login.Api.Dal.Model
{
    [Table("account")]
    public class AccountDO
    {
        /// <summary>
        /// 账号id
        /// </summary>
        [Key]
        [Column("id")]
        public string Id { get; set; }

        /// <summary>
        /// 账号；目前仅支持手机号登录
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 登录密码；
        /// </summary>
        [Column("password")]
        public string Password { get; set; }

        /// <summary>
        /// 密码盐
        /// </summary>
        [Column("password_salt")]
        public string PasswordSalt { get; set; }

        /// <summary>
        /// 密码加密次数
        /// </summary>
        [Column("password_iteration")]
        public int PasswordIteration { get; set; }

        /// <summary>
        /// 状态是否正常；如账号正常，账号异常，密码锁定等等状态
        /// </summary>
        [Column("state")]
        public AccountState State { get; set; }

        /// <summary>
        /// 密码错误次数统计；每次登陆成功此字段更新为0
        /// </summary>
        [Column("error_count")]
        public int ErrorCount { get; set; }

        [Column("password_update_time")]
        public DateTime PasswordUpdateTime { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("create_by")]
        public string CreateBy { get; set; }

        [Column("create_time")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        [Column("update_by")]
        public string UpdateBy { get; set; }

        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
