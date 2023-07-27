using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ae.BasicData.Service.Client.Inteface;
using Ae.BasicData.Service.Common.Extension;
using Ae.BasicData.Service.Core.Interfaces;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Dal.Model;
using Ae.BasicData.Service.Dal.Repositorys.IDAL;

namespace Ae.BasicData.Service.Imp.Services
{
    public class AppOperationLogService : IAppOperationLogService
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly IMapper mapper;
        private readonly string className;
        private readonly IAppOperationLogRepository appOptLogRepo;

        public AppOperationLogService(IMapper mapper,
            IAppOperationLogRepository appOptLogRepo)
        {
            this.mapper = mapper;
            className = this.GetType().Name;

            this.appOptLogRepo = appOptLogRepo;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<bool> CreateAppOperationLog(CreateAppOperationLogReqDTO req)
        {
            var reqDo = mapper.Map<AppOperationLogDO>(req);

            reqDo.Content = req.Content.GetStringSpecificLength();
            reqDo.Platform = req.Platform ?? "";
            reqDo.CreateBy = req.UserId;

            return await appOptLogRepo.CreateAppOperationLog(reqDo);
        }

        // ---------------------------------- 私有方法 --------------------------------------
    }
}
