using System;
using System.Collections.Generic;
using System.Text;
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Response;

namespace Ae.AccountAuthority.Service.Dal.Model
{
    #region ！！！授权接口Model！！！

    public class AuthorizationDO
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

    #endregion ！！！授权接口Model！！！

    public class EmployeeRoleCustomDO { }

    public class EmployeeRoleEntityDO
    {
        public long Id { get; set; }

        public string EmployeeId { get; set; }

        public string RoleName { get; set; }

        public long RoleId { get; set; }

        public RoleType RoleType { get; set; }

        public long OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }

    public class EmployeeRoleListDO
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

    public class EmployeeRolePageDO { }

    public class EmployeeRoleSaveReqDO
    {
        public string EmployeeId { get; set; }

        public string Operator { get; set; }

        public List<EmployeeRoleDO> EmployeeRoleList { get; set; }
    }

}
