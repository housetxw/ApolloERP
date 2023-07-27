using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Dal.Model
{
    public class EmployeeCustomDO : EmployeeDO
    {
        public string WorkKindName { get; set; }

        public string JobName { get; set; }
        

    }

    public class EmployeeRangeRoleSaveReqDO
    {
        public string EmployeeId { get; set; }

        public string Operator { get; set; } = string.Empty;

        /// <summary>
        /// 操作者Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        public List<EmployeeOrganizationRangeDO> EmpOrgRoles { get; set; } = new List<EmployeeOrganizationRangeDO>();
    }

}
