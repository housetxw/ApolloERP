using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;

namespace Ae.AccountAuthority.Service.Core.Interfaces
{
    public interface IAuthorityService
    {
        Task<bool> CreateAuthority(AuthorityDTO req);

        Task<bool> UpdateAuthorityById(AuthorityDTO req);

        Task<bool> DeleteAuthorityById(AuthorityDTO req);

        Task<AuthorityPageListResDTO> GetPagedAuthorityList(AuthorityListReqDTO req);

        Task<List<AuthorityDTO>> GetAllAuthorityList();

        Task<List<AuthorityDTO>> GetAuthorityListByApplicationIdAsync(AuthorityListReqDTO req);

        Task<List<AuthorityDTO>> GetAuthorityListByApplicationIdExceptIdAsync(AuthorityListReqDTO req);

        Task<List<AuthorityDTO>> GetAuthorityListByIsDeleted(AuthorityListReqDTO req);

        Task<List<AuthorityPageResDTO>> GetAuthorityListAnyCondition(AuthorityListInternalReqDTO req);

        Task<List<AuthorityPageResDTO>> GetAuthorityListWithApplicationInfo(AuthorityListInternalReqDTO req);

        Task<List<AuthorityDTO>> GetParentAuthorityListByAppIdAndAuthIdAsync(AuthorityListReqDTO req);

    }
}
