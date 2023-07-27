using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Dal.Model;

namespace Ae.AccountAuthority.Service.Dal.Repositorys.IDAL
{
    public interface IRoleAuthorityRepository
    {
        bool SaveRoleAuthority(RoleAuthorityReqDO reqList);

        Task<List<RoleAuthorityDO>> GetRoleAuthorityListByRoleId(RoleAuthorityListReqByRoleIdDTO req);
    }
}
