using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;
using Ae.Account.Api.Core.Model;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;

namespace Ae.Account.Api.Core.Interfaces
{
    public interface IRoleService
    {
        Task<bool> CreateRole(RoleVO req);

        Task<bool> SaveRoleAuthority(RoleAuthorityReqVO req);

        Task<bool> UpdateRoleById(RoleVO req);

        Task<bool> DeleteRoleById(RoleVO req);

        Task<RolePageListResVO> GetPagedRoleList(RoleListReqVO req);

        Task<List<RoleSelectResVO>> GetAllRoleSelectList();

        Task<List<RoleSelectResVO>> GetRoleListByOrgIdAsync(RoleListReqVO req);

        Task<List<RolePageResVO>> GetRoleListAnyCondition(RoleListInternalReqDTO req);

        Task<List<RoleAuthorityVO>> GetRoleAuthorityListByRoleId(RoleAuthorityListReqByRoleIdVO req);

    }
}
