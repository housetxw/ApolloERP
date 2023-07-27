using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using Ae.B.Login.Api.Core.Model;

namespace Ae.B.Login.Api.Dal.Model
{
    [Table("sys_login_log")]
    public class SysLoginLogDO
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// EmployeeId或是AccountId，切记，一定不能存入登录账号(手机号)
        /// </summary>
        [Column("login_name")]
        public string LoginName { get; set; }

        [Column("ip_address")]
        public string IpAddress { get; set; } = "";

        [Column("login_location")]
        public string LoginLocation { get; set; } = "";

        [Column("browser")]
        public string Browser { get; set; } = "";

        [Column("os")]
        public string OS { get; set; } = "";

        /// <summary>
        /// 登录状态:（0: 成功; 1:失败）
        /// </summary>
        [Column("state")]
        public LoginState State { get; set; }

        /// <summary>
        /// 记录日志login_name类型，0：EmployeeId，1：AccountId
        /// </summary>
        [Column("login_name_type")]
        public LoginNameType LoginNameType { get; set; }

        /// <summary>
        /// 记录登录类型：0：账号密码登录，1：手机验证码登录
        /// </summary>
        [Column("login_type")]
        public LoginType LoginType { get; set; }

        [Column("comment")]
        public string Comment { get; set; }

        /// <summary>
        /// 访问时间
        /// </summary>
        [Column("login_time")]
        public DateTime LoginTime { get; set; } = DateTime.Now;
    }
}
