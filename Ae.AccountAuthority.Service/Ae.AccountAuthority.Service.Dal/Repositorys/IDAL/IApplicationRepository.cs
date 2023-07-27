using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Dal.Model;

namespace Ae.AccountAuthority.Service.Dal.Repositorys.IDAL
{
    public interface IApplicationRepository
    {
        Task<bool> CreateApplication(ApplicationDO req);

        Task<bool> UpdateApplicationById(ApplicationDO req);

        Task<bool> DeleteApplicationById(ApplicationDO req);

        Task<PagedEntity<ApplicationDO>> GetPagedApplicationList(AppListReqDTO req);
        
        Task<ApplicationDO> GetApplicationById(long id);

        Task<ApplicationDO> GetApplicationByName(string name);

        Task<ApplicationDO> GetApplicationByInitialism(string initialism);

        Task<ApplicationDO> GetApplicationByRoute(string route);

        Task<ApplicationDO> GetApplication(AppReqDTO req);

        Task<List<ApplicationDO>> GetApplicationListAnyCondition(AppReqDTO req);

        Task<List<ApplicationDO>> GetApplicationList();


    }
}
