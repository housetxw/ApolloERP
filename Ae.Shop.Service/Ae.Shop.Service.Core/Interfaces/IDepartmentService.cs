using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;

namespace Ae.Shop.Service.Core.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDTO>> GetDepartmentListByOrgIdType(DepartmentListReqByOrgIdTypeDTO req);

        Task<List<DepartmentSelDTO>> GetDepartmentSelByOrgIdType(DepartmentListReqByOrgIdTypeDTO req);

        Task<List<ElementDepartmentTree>> GetDepartmentTreeByOrgIdType(DepartmentListReqByOrgIdTypeDTO req);


    }
}
