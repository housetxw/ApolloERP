using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;

namespace Ae.Account.Api.Client.Interface
{
    public interface IRoleClient
    {
        Task<bool> CreateRole(RoleDTO req);

        Task<bool> SaveRoleAuthority(RoleAuthorityReqDTO req);

        Task<bool> UpdateRoleById(RoleDTO req);

        Task<bool> DeleteRoleById(RoleDTO req);

        Task<RolePageListResDTO> GetPagedRoleList(RoleListReqDTO req);

        Task<List<RoleDTO>> GetAllRoleList();

        Task<List<RoleDTO>> GetRoleListByOrgIdAsync(RoleListReqDTO req);

        Task<List<RolePageResDTO>> GetRoleListAnyCondition(RoleListInternalReqDTO req);

        Task<List<RoleAuthorityDTO>> GetRoleAuthorityListByRoleId(RoleAuthorityListReqByRoleIdDTO req);

    }
}
