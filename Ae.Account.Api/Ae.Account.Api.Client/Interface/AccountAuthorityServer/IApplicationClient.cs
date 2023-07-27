using System.Collections.Generic;
using System.Threading.Tasks;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;

namespace Ae.Account.Api.Client.Interface
{
    public interface IApplicationClient
    {
        Task<bool> CreateApplication(ApplicationDTO req);

        Task<bool> UpdateApplicationById(ApplicationDTO req);

        Task<bool> DeleteApplicationById(ApplicationDTO req);

        Task<AppListResDTO> GetPagedApplicationList(AppListReqDTO req);

        Task<AppResDTO> GetApplicationById(long id);

        Task<AppResDTO> GetApplicationByName(string name);

        Task<AppResDTO> GetApplicationByInitialism(string initialism);

        Task<AppResDTO> GetApplicationByRoute(string route);

        Task<AppResDTO> GetApplication(AppReqDTO req);

        Task<List<AppResDTO>> GetApplicationListAnyCondition(AppReqDTO req);

        Task<List<AppResDTO>> GetApplicationList();

    }
}
