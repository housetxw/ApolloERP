using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Shop.Service.Client.Response
{
    class AccountAuthorityReqDTO { }

    public class RoleDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 0：公司；1：门店；2：仓库；
        /// </summary>
        public RoleType Type { get; set; }

        public long OrganizationId { get; set; }

        public int Features { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
    public enum RoleType
    {
        None = -1,

        [Description("公司")]
        Company = 0,

        [Description("门店")]
        Shop = 1,

        [Description("仓库")]
        Warehouse = 2,
        [Description("拓展")]
        Extend = 3,
        [Description("供应商")]
        Supplier = 4
    }

    public class OrgRangeRolesDTO
    {
        public long OrganizationId { get; set; }

        public string OrganizationName { get; set; } = string.Empty;

        public RoleType Type { get; set; }

        public List<RoleDTO> Roles { get; set; } = new List<RoleDTO>();
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

    public class EmployeeRolesDTO
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public string EmployeeId { get; set; }
        /// <summary>
        /// 员工角色
        /// </summary>
        public List<EmployeeRoleListDTO> Roles { get; set; }
    }
}
