using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.B.Login.Api.Client.Response.ShopManage.Employee;
using Ae.B.Login.Api.Core.Response;

namespace Ae.B.Login.Api.Core.Model
{
    public class Token
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string TokenContent { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int Expires { get; set; }
    }

    public enum TokenType
    {
        /// <summary>
        /// 访问Token
        /// </summary>
        [Display(Name = "AccessToken")]
        AccessToken = 1,

        /// <summary>
        /// 刷新Token
        /// </summary>
        [Display(Name = "RefreshToken")]
        RefreshToken = 2
    }

    public class TokenInfo
    {
        /// <summary>
        /// AccessToken
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// RefreshToken
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 刷新地址
        /// </summary>
        public string RefreshUri { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int Expires { get; set; }
    }

    /// <summary>
    /// 刷新Token
    /// </summary>
    public class RefreshTokenReq
    {
        /// <summary>
        /// RefreshToken
        /// </summary>
        [Required(ErrorMessage = "RefreshToken不能为空")]
        public string RefreshToken { get; set; }
    }

    public class EmployeeInfoVO
    {
        //public string AccountId { get; set; }

        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public int? OrganizationId { get; set; }

        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;
    }

    public class EmployeeInfoByAuthCodeReq
    {
        [Required(ErrorMessage = "请输入认证码")]
        public string AuthCode { get; set; }

        [Required(ErrorMessage = "请输入员工Id")]
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "请输入员工姓名")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "请输入所属单位Id")]
        public int OrganizationId { get; set; }

        [Required(ErrorMessage = "请输入所属单位类型")]
        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;
    }

    public class EmployeeInfoByTokenReq
    {
        [Required(ErrorMessage = "请输入正确的Token信息")]
        public string Token { get; set; }

        [Required(ErrorMessage = "请输入员工Id")]
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "请输入员工姓名")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "请输入所属单位Id")]
        public int OrganizationId { get; set; }

        [Required(ErrorMessage = "请输入所属单位类型")]
        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;
    }


}
