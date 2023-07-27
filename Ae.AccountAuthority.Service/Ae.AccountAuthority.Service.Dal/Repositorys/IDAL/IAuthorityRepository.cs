using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Dal.Model;

namespace Ae.AccountAuthority.Service.Dal.Repositorys.IDAL
{
    public interface IAuthorityRepository
    {
        Task<bool> CreateAuthority(AuthorityDO req, List<long> parenIdList = null);

        Task<bool> UpdateAuthorityById(AuthorityDO req);

        Task<bool> DeleteAuthorityById(AuthorityDO req, List<long> parenIdList = null);

        Task<PagedEntity<AuthorityPageDO>> GetPagedAuthorityList(AuthorityListReqDTO req);

        Task<List<AuthorityPageDO>> GetAllAuthorityList();

        Task<List<AuthorityPageDO>> GetAuthorityListByIsDeleted(AuthorityListReqDTO req);

        Task<List<AuthorityPageDO>> GetAuthorityListByApplicationIdAsync(AuthorityListReqDTO req);

        Task<List<AuthorityPageDO>> GetAuthorityListByApplicationIdExceptIdAsync(AuthorityListReqDTO req);

        Task<List<AuthorityPageDO>> GetAuthorityListAnyCondition(AuthorityListInternalReqDTO req);

        Task<List<AuthorityPageDO>> GetAuthorityListWithApplicationInfo(AuthorityListInternalReqDTO req);

    }
}
