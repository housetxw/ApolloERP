using System.Collections.Generic;
using System.Threading.Tasks;
using Ae.Account.Api.Core.Model;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;

namespace Ae.Account.Api.Core.Interfaces
{
    public interface IApplicationService
    {
        Task<bool> CreateApplication(ApplicationVO req);

        Task<bool> UpdateApplicationById(ApplicationVO req);

        Task<bool> DeleteApplicationById(ApplicationVO req);

        Task<AppListResVO> GetPagedApplicationList(AppListReqVO req);

        Task<AppResVO> GetApplicationById(long id);

        Task<AppResVO> GetApplicationByName(string name);

        Task<AppResVO> GetApplicationByInitialism(string initialism);

        Task<AppResVO> GetApplicationByRoute(string route);
        
        Task<AppResVO> GetApplication(AppReqVO req);

        Task<List<AppResVO>> GetApplicationListAnyCondition(AppReqVO req);

        Task<List<AppSelectResVO>> GetApplicationList();


    }
}