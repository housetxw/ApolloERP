using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using Ae.C.Login.Api.Dal.Model;
using KeyAttribute = ApolloErp.Data.DapperExtensions.Att.KeyAttribute;

namespace Ae.C.Login.Api.Dal.Model
{
    [Table("sys_login_log")]
    public class SysLoginLogDO
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// userid，切记，一定不能存入登录账号(手机号)
        /// </summary>
        [Column("login_name")]
        public string LoginName { get; set; } = string.Empty;

        [Column("ip_address")]
        public string IpAddress { get; set; }= string.Empty;

        [Column("login_location")]
        public string LoginLocation { get; set; }=string.Empty;

        [Column("browser")]
        public string Browser { get; set; }= string.Empty;

        [Column("os")]
        public string OS { get; set; }=string.Empty;

        /// <summary>
        /// 登录状态:（0: 成功; 1:失败）
        /// </summary>
        [Column("state")]
        public LoginState State { get; set; }

        /// <summary>
        /// 三方openid
        /// </summary>
        [Column("open_id")]
        public string OpenId { get; set; }= string.Empty;

        /// <summary>
        /// 记录登录类型：1：微信登录，2：QQ，3：微博，4：手机验证码登录
        /// </summary>
        [Column("login_type")]
        public LoginType LoginType { get; set; }

        [Column("comment")]
        public string Comment { get; set; }= string.Empty;

        /// <summary>
        /// 访问时间
        /// </summary>
        [Column("login_time")]
        public DateTime LoginTime { get; set; } = DateTime.Now;
    }

    public enum LoginState
    {
        None = -1,
        [Description("成功")]
        Success = 0,

        [Description("失败")]
        Failure = 1
    }

    public enum LoginType
    {
        None = -1,
        /// <summary>
        /// 微信
        /// </summary>
        [Display(Name = "微信")]
        WeChat = 1,
        /// <summary>
        /// QQ
        /// </summary>
        [Display(Name = "QQ")]
        QQ = 2,
        /// <summary>
        /// 微博
        /// </summary>
        [Display(Name = "微博")]
        Weibo = 3,

        [Display(Name = "手机验证码登录")]
        SMSCode = 4
    }
}
