using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;

namespace Ae.AccountAuthority.Service.Core.Interfaces
{
    public interface IApplicationService
    {
        Task<bool> CreateApplication(ApplicationDTO req);

        Task<bool> UpdateApplicationById(ApplicationDTO req);

        Task<bool> DeleteApplicationById(ApplicationDTO req);

        Task<AppListResDTO> GetPagedApplicationList(AppListReqDTO req);

        Task<AppResDTO> GetApplicationById(long id);

        Task<AppResDTO> GetApplicationByName(string name);

        Task<AppResDTO> GetApplicationByInitialism(string initialism);

        Task<AppResDTO> GetApplicationByRoute(string route);

        Task<AppResDTO> GetApplication(AppReqDTO request);

        Task<List<AppResDTO>> GetApplicationListAnyCondition(AppReqDTO request);

        Task<List<AppResDTO>> GetApplicationList();


    }
}
