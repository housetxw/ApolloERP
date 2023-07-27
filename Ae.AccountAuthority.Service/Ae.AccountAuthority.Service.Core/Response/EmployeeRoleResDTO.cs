using System;
using System.Collections.Generic;
using System.Text;
using Ae.AccountAuthority.Service.Core.Model;

namespace Ae.AccountAuthority.Service.Core.Response
{
    #region ！！！授权接口Model！！！

    public class AuthorizationResDTO
    {
        public string EmployeeId { get; set; }

        public long AuthorityId { get; set; }

        public long ParentId { get; set; }

        public string AuthorityName { get; set; }

        public string Route { get; set; }

        public string MenuIcon { get; set; }

        public string RouteParameter { get; set; }

        public long ApplicationId { get; set; }

        public string ApplicationName { get; set; }


        public AuthorityType AuthorityType { get; set; }
    }

    public class AuthorizationAPPResDTO
    {
        public List<AuthorizationAuthorityAPPResDTO> TopMenuList = new List<AuthorizationAuthorityAPPResDTO>();

        public List<AuthorizationAuthorityAPPResDTO> CenterMenuList = new List<AuthorizationAuthorityAPPResDTO>();

        public List<AuthorizationAuthorityAPPResDTO> ShopManagerList = new List<AuthorizationAuthorityAPPResDTO>();

        //public List<AuthorizationAuthorityAPPResDTO> SidebarMenuList = new List<AuthorizationAuthorityAPPResDTO>();

        //public List<AuthorizationAuthorityAPPResDTO> NavigationMenuList = new List<AuthorizationAuthorityAPPResDTO>();
    }

    public class AuthorizationAuthorityAPPResDTO
    {
        /// <summary>
        /// AuthorityName
        /// </summary>
        public string RouteKey { get; set; }

        /// <summary>
        /// AuthorityName
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// Route
        /// </summary>
        public string RouteValue { get; set; }

        /// <summary>
        /// MenuIcon
        /// </summary>
        public string RouteImg { get; set; }

        public Dictionary<string, string> RouteParam { get; set; }

        public string Type { get; set; }
    }


    #endregion ！！！授权接口Model！！！

    public class EmployeeRoleResDTO { }

    public class EmployeeRolesDTO
    {
        public string EmployeeId { get; set; }

        public List<EmployeeRoleListDTO> Roles { get; set; }
    }

    public class EmployeeRoleListDTO
    {
        public string EmployeeId { get; set; }

        public long RoleId { get; set; }

        public string RoleName { get; set; }

        public RoleType RoleType { get; set; }

        public long OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }


}
