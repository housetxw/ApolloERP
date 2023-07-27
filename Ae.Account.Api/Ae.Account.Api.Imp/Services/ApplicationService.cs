using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Ae.Account.Api.Client.Interface;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Core.Interfaces;
using Ae.Account.Api.Core.Model;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;

namespace Ae.Account.Api.Imp.Services
{
    public class ApplicationService : IApplicationService
    {
        #region 变量和构造器

        private readonly IMapper mapper;

        private readonly IApplicationClient appClient;

        public ApplicationService(IMapper mapper, IApplicationClient appClient)
        {
            this.mapper = mapper;
            this.appClient = appClient;
        }

        #endregion 变量和构造器

        #region 接口实现

        public async Task<bool> CreateApplication(ApplicationVO req)
        {
            var reqDto = mapper.Map<ApplicationDTO>(req);
            var res = await appClient.CreateApplication(reqDto);
            return res;
        }

        public async Task<bool> UpdateApplicationById(ApplicationVO req)
        {
            var reqDto = mapper.Map<ApplicationDTO>(req);
            var res = await appClient.UpdateApplicationById(reqDto);
            return res;
        }

        public async Task<bool> DeleteApplicationById(ApplicationVO req)
        {
            var reqDto = mapper.Map<ApplicationDTO>(req);
            var res = await appClient.DeleteApplicationById(reqDto);
            return res;
        }

        public async Task<AppListResVO> GetPagedApplicationList(AppListReqVO req)
        {
            var reqDto = mapper.Map<AppListReqDTO>(req);
            var resDto = await appClient.GetPagedApplicationList(reqDto);
            var res = mapper.Map<AppListResVO>(resDto);
            return res;
        }

        public async Task<AppResVO> GetApplicationById(long id)
        {
            var resDo = await appClient.GetApplicationById(id);
            var res = mapper.Map<AppResVO>(resDo);
            return res;
        }

        public async Task<AppResVO> GetApplicationByName(string name)
        {
            var resDo = await appClient.GetApplicationByName(name);
            var res = mapper.Map<AppResVO>(resDo);
            return res;
        }

        public async Task<AppResVO> GetApplicationByInitialism(string initialism)
        {
            var resDo = await appClient.GetApplicationByInitialism(initialism);
            var res = mapper.Map<AppResVO>(resDo);
            return res;
        }

        public async Task<AppResVO> GetApplicationByRoute(string route)
        {
            var resDo = await appClient.GetApplicationByRoute(route);
            var res = mapper.Map<AppResVO>(resDo);
            return res;
        }

        public async Task<AppResVO> GetApplication(AppReqVO req)
        {
            var reqDto = mapper.Map<AppReqDTO>(req);
            var resDo = await appClient.GetApplication(reqDto);
            var res = mapper.Map<AppResVO>(resDo);
            return res;
        }

        public async Task<List<AppResVO>> GetApplicationListAnyCondition(AppReqVO req)
        {
            var reqDto = mapper.Map<AppReqDTO>(req);
            var resDo = await appClient.GetApplicationListAnyCondition(reqDto);
            var res = mapper.Map<List<AppResVO>>(resDo);
            return res;
        }

        public async Task<List<AppSelectResVO>> GetApplicationList()
        {
            var resDto = await appClient.GetApplicationList();
            var res = mapper.Map<List<AppSelectResVO>>(resDto);
            return res;
        }

        #endregion 接口实现

    }
}
