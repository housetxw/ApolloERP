using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Ae.AccountAuthority.Service.Core.Interfaces;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;
using Ae.AccountAuthority.Service.Dal.Model;
using Ae.AccountAuthority.Service.Dal.Repositorys.IDAL;

namespace Ae.AccountAuthority.Service.Imp.Services
{
    public class ApplicationService : IApplicationService
    {
        #region 变量和构造器

        private readonly IMapper mapper;

        private readonly IApplicationRepository appRepo;

        public ApplicationService(IMapper mapper, IApplicationRepository appRepo)
        {
            this.mapper = mapper;
            this.appRepo = appRepo;
        }

        #endregion 变量和构造器

        #region 接口实现

        public async Task<bool> CreateApplication(ApplicationDTO req)
        {
            var reqDo = mapper.Map<ApplicationDO>(req);
            var res = await appRepo.CreateApplication(reqDo);
            return res;
        }

        public async Task<bool> UpdateApplicationById(ApplicationDTO req)
        {
            var reqDo = mapper.Map<ApplicationDO>(req);
            var res = await appRepo.UpdateApplicationById(reqDo);
            return res;
        }

        public async Task<bool> DeleteApplicationById(ApplicationDTO req)
        {
            var reqDo = mapper.Map<ApplicationDO>(req);
            var res = await appRepo.DeleteApplicationById(reqDo);
            return res;
        }

        public async Task<AppListResDTO> GetPagedApplicationList(AppListReqDTO req)
        {
            var res = new AppListResDTO();

            var resDo = await appRepo.GetPagedApplicationList(req);

            if (null == resDo)
            {
                return res;
            }

            res.AppList = mapper.Map<List<AppResDTO>>(resDo.Items);
            res.TotalItems = resDo.TotalItems;
            return res;
        }

        public async Task<AppResDTO> GetApplicationById(long id)
        {
            var resDo = await appRepo.GetApplicationById(id);
            var res = mapper.Map<AppResDTO>(resDo);
            return res;
        }

        public async Task<AppResDTO> GetApplicationByName(string name)
        {
            var resDo = await appRepo.GetApplicationByName(name);
            var res = mapper.Map<AppResDTO>(resDo);
            return res;
        }

        public async Task<AppResDTO> GetApplicationByInitialism(string initialism)
        {
            var resDo = await appRepo.GetApplicationByInitialism(initialism);
            var res = mapper.Map<AppResDTO>(resDo);
            return res;
        }

        public async Task<AppResDTO> GetApplicationByRoute(string route)
        {
            var resDo = await appRepo.GetApplicationByRoute(route);
            var res = mapper.Map<AppResDTO>(resDo);
            return res;
        }

        public async Task<AppResDTO> GetApplication(AppReqDTO req)
        {
            var resDo = await appRepo.GetApplication(req);
            var res = mapper.Map<AppResDTO>(resDo);
            return res;
        }

        public async Task<List<AppResDTO>> GetApplicationListAnyCondition(AppReqDTO req)
        {
            var resDo = await appRepo.GetApplicationListAnyCondition(req);
            var res = mapper.Map<List<AppResDTO>>(resDo);
            return res;
        }

        public async Task<List<AppResDTO>> GetApplicationList()
        {
            var resDo = await appRepo.GetApplicationList();
            var res = mapper.Map<List<AppResDTO>>(resDo);
            return res;
        }

        #endregion 接口实现

        #region 私有方法

        #endregion 私有方法
    }
}
