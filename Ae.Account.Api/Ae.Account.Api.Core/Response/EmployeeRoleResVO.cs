using System;
using System.Collections.Generic;
using System.Text;
using Ae.Account.Api.Client.Response;

namespace Ae.Account.Api.Core.Response
{
    #region ！！！授权接口Model！！！

    public class AuthorizationWebComplexResVO
    {
        public List<TopMenuVO> TopMenu { get; set; }

        public List<AuthorizationWebResVO> SubMenu { get; set; }
    }

    public class AuthorizationWebResVO
    {
        /// <summary>
        /// Route
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// AuthorityName
        /// </summary>
        public string Name { get; set; }

        public List<MenuMeta> Meta { get; set; } = new List<MenuMeta>();

        public bool HasPermission { get; set; } = true;

        public string Type { get; set; }

        public long AuthorityId { get; set; }

        public long ParentId { get; set; }

        public long ApplicationId { get; set; }

        //public string ApplicationName { get; set; }
    }
    public class AuthorizationWebResDTO
    {
        public long AuthorityId { get; set; }

        public long ParentId { get; set; }

        /// <summary>
        /// Route
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// AuthorityName
        /// </summary>
        public string Name { get; set; }

        public List<MenuMeta> Meta { get; set; } = new List<MenuMeta>();

        public bool HasPermission { get; set; } = true;

        public long ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public AuthorityType Type { get; set; }
    }
    public class MenuMeta
    {
        /// <summary>
        /// AuthorityName
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// MenuIcon
        /// </summary>
        public string Icon { get; set; }
    }
    public class TopMenuVO
    {
        public long ApplicationId { get; set; }
        public string Title { get; set; }
        public bool HasPermission { get; set; } = true;
    }


    public class AuthorizationAPPResDTO
    {
        public string EmployeeId { get; set; }

        public long AuthorityId { get; set; }

        public string AuthorityName { get; set; }

        public string Route { get; set; }

        public string MenuIcon { get; set; }

        public string RouteParameter { get; set; }

        public long ApplicationId { get; set; }


        public AuthorityType AuthorityType { get; set; }
    }

    #endregion ！！！授权接口Model！！！

    public class EmployeeRoleResVO { }

    public class EmployeePageResVO
    {
        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string AccountId { get; set; }

        public string AccountName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Number { get; set; }

        /// <summary>
        /// 类型；0：公司；1：门店；2：仓库；
        /// </summary>
        public EmployeeType Type { get; set; }

        public string OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public int JobId { get; set; }

        public string JobName { get; set; }

        public string Identity { get; set; }

        public string Address { get; set; }

        public string Avatar { get; set; }

        public decimal Score { get; set; }

        /// <summary>
        /// 离职类型；0：自动离职；1：辞退 等等
        /// </summary>
        public DimissionType DimissionType { get; set; }

        public string DimissionCause { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; }
        public string CreateId { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; }
        public string UpdateId { get; set; }

        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }

    public class EmployeeResVO
    {
        public string Id { get; set; }

        public string AccountId { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 类型；0：公司；1：门店；2：仓库；
        /// </summary>
        public EmployeeType Type { get; set; }

        public string TypeName { get; set; }

        public int OrganizationId { get; set; }

        public int DepartmentId { get; set; }

        public int JobId { get; set; }

        public bool IsDeleted { get; set; }
    }


}
