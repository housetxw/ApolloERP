using System;
using System.Collections.Generic;
using System.Text;
using Ae.AccountAuthority.Service.Core.Response;

namespace Ae.AccountAuthority.Service.Core.Model
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

}
