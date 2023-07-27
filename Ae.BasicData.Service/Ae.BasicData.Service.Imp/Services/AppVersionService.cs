using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ae.BasicData.Service.Core.Interfaces;
using Ae.BasicData.Service.Core.Model;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Core.Response;
using Ae.BasicData.Service.Dal.Repositorys.IDAL;

namespace Ae.BasicData.Service.Imp.Services
{
    public class AppVersionService : IAppVersionService
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly IMapper mapper;
        private readonly string className;
        private readonly IAppVersionRepository appVerRepo;

        public AppVersionService(IMapper mapper,
            IAppVersionRepository appVerRepo)
        {
            this.mapper = mapper;
            className = this.GetType().Name;

            this.appVerRepo = appVerRepo;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<AppVersionEntityResDTO> GetAppVersionInfo(AppVersionEntityReqDTO req)
        {
            var verDoList = await appVerRepo.GetAppVersionListByCode(req);
            if (verDoList == null || !verDoList.Any())
            {
                return null;
            }

            AppVersionEntityResDTO res = null;
            verDoList = verDoList.OrderByDescending(o => o.Code).ToList();
            foreach (var verDo in verDoList)
            {
                if (verDo.IsRelease)
                {
                    res = mapper.Map<AppVersionEntityResDTO>(verDo);
                    break;
                }

                var appGray = await appVerRepo.GetAppVersionGrayInfoByCodeAndId(req);
                if (appGray == null) return null;

                res = mapper.Map<AppVersionEntityResDTO>(verDo);
                break;
            }

            return res;
        }

        public async Task<List<AppVersionListResDTO>> GetAppVersionHistory()
        {
            var resDo = await appVerRepo.GetAppVersionHistory();
            var res = mapper.Map<List<AppVersionListResDTO>>(resDo);
            return res;
        }

        // ---------------------------------- 私有方法 --------------------------------------

    }
}
