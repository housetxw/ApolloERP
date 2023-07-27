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
    public interface IAuthorityService
    {
        Task<bool> CreateAuthority(AuthorityVO req);

        Task<bool> UpdateAuthorityById(AuthorityVO req);

        Task<bool> DeleteAuthorityById(AuthorityVO req);

        Task<AuthorityPageListResVO> GetPagedAuthorityList(AuthorityListReqVO req);

        Task<List<AuthoritySelectResVO>> GetAllAuthoritySelectList();

        Task<List<AuthoritySelectResVO>> GetAuthorityListByApplicationIdAsync(AuthorityListReqVO req);

        Task<List<AuthoritySelectResVO>> GetAuthorityListByApplicationIdExceptIdAsync(AuthorityListReqVO req);

        Task<List<AuthorityPageResDTO>> GetAuthorityListAnyCondition(AuthorityListInternalReqDTO req);

        Task<List<AuthorityTreeVO>> GetAuthorityTreeWithApplicationInfo();

        Task<List<AuthorityDTO>> GetParentAuthorityListByAppIdAndAuthIdAsync(AuthorityListReqDTO req);


    }
}