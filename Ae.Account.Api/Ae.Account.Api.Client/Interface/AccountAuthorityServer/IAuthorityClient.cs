using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;

namespace Ae.Account.Api.Client.Interface
{
    public interface IAuthorityClient
    {
        Task<bool> CreateAuthority(AuthorityDTO req);

        Task<bool> UpdateAuthorityById(AuthorityDTO req);

        Task<bool> DeleteAuthorityById(AuthorityDTO req);

        Task<AuthorityPageListResDTO> GetPagedAuthorityList(AuthorityListReqDTO req);

        Task<List<AuthorityDTO>> GetAllAuthorityList();

        Task<List<AuthorityDTO>> GetAuthorityListByApplicationIdAsync(AuthorityListReqDTO req);

        Task<List<AuthorityDTO>> GetAuthorityListByApplicationIdExceptIdAsync(AuthorityListReqDTO req);

        Task<List<AuthorityPageResDTO>> GetAuthorityListAnyCondition(AuthorityListInternalReqDTO req);

        Task<List<AuthorityPageResDTO>> GetAuthorityListWithApplicationInfo(AuthorityListInternalReqDTO req);

        Task<List<AuthorityDTO>> GetParentAuthorityListByAppIdAndAuthIdAsync(AuthorityListReqDTO req);

    }
}
