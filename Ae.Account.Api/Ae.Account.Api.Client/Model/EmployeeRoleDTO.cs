using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Account.Api.Client.Model
{
    public class EmployeeRoleDTO { }

    public class EmployeeRoleEntityDTO
    {
        public long Id { get; set; }

        public string EmployeeId { get; set; }

        public long RoleId { get; set; }

        public bool IsDeleted { get; set; } = false;

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; } = string.Empty;

        public DateTime UpdateTime { get; set; } = DateTime.Now;
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
