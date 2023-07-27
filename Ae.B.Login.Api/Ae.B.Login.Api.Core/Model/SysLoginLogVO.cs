using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.B.Login.Api.Core.Model
{
    public class SysLoginLogVO
    {
        public long Id { get; set; }

        /// <summary>
        /// EmployeeId或是AccountId，切记，一定不能存入登录账号(手机号)
        /// </summary>
        public string LoginName { get; set; }

        public string IpAddress { get; set; } = "";

        public string LoginLocation { get; set; } = "";

        public string Browser { get; set; } = "";

        public string OS { get; set; } = "";

        /// <summary>
        /// 登录状态:（0: 成功; 1:失败）
        /// </summary>
        public LoginState State { get; set; }

        /// <summary>
        /// 记录日志login_name类型，0：EmployeeId，1：AccountId
        /// </summary>
        public LoginNameType LoginNameType { get; set; }

        /// <summary>
        /// 记录登录类型：0：账号密码登录，1：手机验证码登录
        /// </summary>
        public LoginType LoginType { get; set; }

        public string Comment { get; set; }

        /// <summary>
        /// 访问时间
        /// </summary>
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

    public enum LoginNameType
    {
        None = -1,

        [Description("员工账号")]
        EmployeeId = 0,

        [Description("登录账号")]
        AccountId = 1
    }

    public enum LoginType
    {
        None = -1,

        [Description("账号密码登录")]
        Password = 0,

        [Description("手机验证码登录")]
        SMSCode = 1
    }

}
