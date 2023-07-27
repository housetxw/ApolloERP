using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Account.Api.Core.Model
{
    public class EmployeeRoleVO { }

    public class EmployeeRoleLisVO
    {
        public string EmployeeId { get; set; }

        public long RoleId { get; set; }

        public string RoleName { get; set; }

        public RoleType RoleType { get; set; }

        public string OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; }
        public string CreateId { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; }
        public string UpdateId { get; set; }

        public DateTime UpdateTime { get; set; } = DateTime.Now;

        public int Features { get; set; }
    }

}
