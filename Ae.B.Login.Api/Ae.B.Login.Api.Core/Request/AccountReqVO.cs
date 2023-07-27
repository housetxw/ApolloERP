using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.B.Login.Api.Client.Response.ShopManage.Employee;
using Ae.B.Login.Api.Common.Http;
using Ae.B.Login.Api.Core.Enums;

namespace Ae.B.Login.Api.Core.Request
{
    public class AccountReqVO { }

    public class LoginVO
    {
        [Required(ErrorMessage = "请输入登录账号")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }

        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;

        public LoginPlatform Platform { get; set; } = LoginPlatform.None;
    }

    public class LoginPhoneVO
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "请输入登录账号")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入验证码类型")]
        public SMSType SMSType { get; set; } = SMSType.Login;
    }

    public enum SMSType
    {
        None = -1,

        [Description("登录验证码")] [Display(Name = "Login")]
        Login = 0,

        [Description("修改密码验证码")] [Display(Name = "ChgPwd")]
        ChangePassword = 1,

        [Description("App快速注册")] [Display(Name = "AppQuickRegister")]
        AppQuickRegister = 2,

        [Description("公司快速注册")] [Display(Name = "QuickCompanyRegister")]
        QuickCompanyRegister = 3
    }

    public class LoginMessageVO
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "请输入登录账号")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入验证码")]
        public string VerificationCode { get; set; }

        [Required(ErrorMessage = "请输入验证码类型")]
        public SMSType SMSType { get; set; } = SMSType.Login;

        /// <summary>
        /// 所属单位类型： APP和S：默认是Shop或是1；Boss: 默认是All或是9999；
        /// </summary>
        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;

        public LoginPlatform Platform { get; set; } = LoginPlatform.None;
    }

    public class UpdatePasswordVO : LoginMessageVO
    {
        [Required(ErrorMessage = "请输入密码")]
        [MinLength(8, ErrorMessage = "密码长度至少是8位")]
        [MaxLength(18, ErrorMessage = "密码长度不能超过18位")]
        public string Password { get; set; }
    }

    public class AccountEntityReqByNameVO
    {
        [Required(ErrorMessage = "请输入登录账号")]
        public string Name { get; set; }
    }

    public class AccountStateOrErrorCountReqVO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int ErrorCount { get; set; }
    }

    public class UpdateAccountPwdVO : LoginMessageVO
    {
        public string Id { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public int PasswordIteration { get; set; }
    }

    public class OrganizationPageListReqVO : BaseOnlyPageRequest
    {
        [Required(ErrorMessage = "请输入认证码")]
        public string AuthCode { get; set; }

        /// <summary>
        /// 所属单位类型：APP和S：默认是Shop或是1；Boss: 默认是All或是9999；
        /// </summary>
        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;

        public string OrganizationName { get; set; }
    }

    public class OrganizationPageListByTokenReqVO : BaseOnlyPageRequest
    {
        [Required(ErrorMessage = "请输入Token")]
        public string Token { get; set; }

        [Required(ErrorMessage = "请输入员工Id")]
        public string EmployeeId { get; set; }

        /// <summary>
        /// 所属单位类型： APP和S：默认是Shop或是1；Boss: 默认是All或是9999；
        /// </summary>
        [Required(ErrorMessage = "请输入员工Id")]
        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;

        public string OrganizationName { get; set; }

        public LoginPlatform Platform { get; set; } = LoginPlatform.None;
    }

    public class AuthorityReqVO
    {
        [Required(ErrorMessage = "请输入认证码")]
        public string AuthCode { get; set; }

        [Required(ErrorMessage = "请输入员工Id")]
        public string EmployeeId { get; set; }
    }


}
