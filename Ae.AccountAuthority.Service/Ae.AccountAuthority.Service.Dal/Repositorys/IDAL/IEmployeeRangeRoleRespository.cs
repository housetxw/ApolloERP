using Ae.AccountAuthority.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.AccountAuthority.Service.Dal.Repositorys.IDAL
{
    public interface IEmployeeRangeRoleRespository
    {
        Task<EmployeeOrganizationRange> GetRangeRoleListByIds(string employeeId, long organizationId, int type);
    }
}
