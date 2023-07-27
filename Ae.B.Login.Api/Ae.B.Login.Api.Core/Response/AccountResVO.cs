using System;
using System.Collections.Generic;
using System.Text;
using Ae.B.Login.Api.Client.Response.ShopManage.Employee;
using Ae.B.Login.Api.Core.Model;

namespace Ae.B.Login.Api.Core.Response
{
    public class AccountResVO { }

    #region 登录认证出参相关Model

    public class LoginInfoResVO
    {
        public TokenInfo Token { get; set; }

        public int OrgTotal { get; set; }

        //用于当OrgTotal多个时，请求OrgItems列表，和，生成AccessToken和RefreshToken简单的安全验证
        public string AuthCode { get; set; }

        public List<OrganizationVO> OrgItems { get; set; } = new List<OrganizationVO>();
    }

    public class OrganizationListVO
    {
        //用于当OrgTotal多个时，请求OrgItems列表，和，生成AccessToken和RefreshToken简单的安全验证
        public string AuthCode { get; set; }

        public List<OrganizationVO> OrgItems { get; set; }
    }
    public class OrganizationVO
    {
        public string AccountId { get; set; }

        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public int OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        public string OrganizationSimpleName { get; set; }

        public List<ShopTypeVO> ShopTypeList { get; set; } = new List<ShopTypeVO>();

        public string OrganizationAddress { get; set; }

        public string OrganizationImage { get; set; }

        public EmployeeType EmployeeType { get; set; } = EmployeeType.None;

        public bool IsOrganizationRange { get; set; }
    }

    public class ShopTypeVO
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }

    #endregion 登录认证出参相关Model

    public class AccountKeyInfoEntityResVO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public AccountState State { get; set; }

        public int ErrorCount { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class AccountKeyInfoWithPwdEntityResVO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public int PasswordIteration { get; set; }

        public AccountState State { get; set; }

        public int ErrorCount { get; set; }
    }

    public class AccountKeyInfoApiResVO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public int PasswordIteration { get; set; }

        public AccountState State { get; set; }

        public int ErrorCount { get; set; }
    }


}
