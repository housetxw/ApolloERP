using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;
using Ae.AccountAuthority.Service.Dal.Model;

namespace Ae.AccountAuthority.Service.Dal.Repositorys.IDAL
{
    public interface IRoleRepository
    {
        Task<List<OrgRangeRolesDTO>> GetRoleListByOrgIds(List<OrgEntityReqDTO> req);

        Task<bool> CreateRole(RoleDO req);

        Task<bool> UpdateRoleById(RoleDO req);

        Task<bool> DeleteRoleById(RoleDO req);

        Task<PagedEntity<RoleDO>> GetPagedRoleList(RoleListReqDTO req);

        Task<List<RoleDO>> GetAllRoleList();

        Task<List<RoleDO>> GetRoleListByName(List<string> roleName);

        Task<List<RoleDO>> GetRoleListByOrgIdAsync(RoleListReqDTO req);

        Task<List<RoleDO>> GetRoleListAnyCondition(RoleListInternalReqDTO req);


    }
}
