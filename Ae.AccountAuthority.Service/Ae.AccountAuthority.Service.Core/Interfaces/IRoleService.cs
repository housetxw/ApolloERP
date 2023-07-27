using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;

namespace Ae.AccountAuthority.Service.Core.Interfaces
{
    public interface IRoleService
    {
        Task<List<OrgRangeRolesDTO>> GetRoleListByOrgIds(List<OrgEntityReqDTO> req);

        Task<bool> CreateRole(RoleDTO req);

        bool SaveRoleAuthority(RoleAuthorityReqDTO req);

        Task<bool> UpdateRoleById(RoleDTO req);

        Task<bool> DeleteRoleById(RoleDTO req);

        Task<RolePageListResDTO> GetPagedRoleList(RoleListReqDTO req);

        Task<List<RoleDTO>> GetAllRoleList();

        Task<List<RoleDTO>> GetRoleListByName(List<string> roleName);

        Task<List<RoleDTO>> GetRoleListByOrgIdAsync(RoleListReqDTO req);

        Task<List<RolePageResDTO>> GetRoleListAnyCondition(RoleListInternalReqDTO req);

        Task<List<RoleAuthorityDTO>> GetRoleAuthorityListByRoleId(RoleAuthorityListReqByRoleIdDTO req);

    }
}
