using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;

namespace Ae.Shop.Service.Dal.Repositorys
{
    public interface IDepartmentRepository
    {
        Task<List<DepartmentDO>> GetDepartmentListByOrgIdType(DepartmentListReqByOrgIdTypeDTO req);



    }
}
