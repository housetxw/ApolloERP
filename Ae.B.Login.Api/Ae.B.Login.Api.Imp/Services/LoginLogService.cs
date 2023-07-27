using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ae.B.Login.Api.Core.Interfaces;
using Ae.B.Login.Api.Core.Model;
using Ae.B.Login.Api.Dal.Model;
using Ae.B.Login.Api.Dal.Repositorys.IDAL;

namespace Ae.B.Login.Api.Imp.Services
{
    public class LoginLogService : ILoginLogService
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly IMapper mapper;

        private readonly ILoginLogRepository loginLogRepo;

        public LoginLogService(IMapper mapper, ILoginLogRepository loginLogRepo)
        {
            this.mapper = mapper;
            this.loginLogRepo = loginLogRepo;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        public async Task CreateLoginLogAsync(SysLoginLogVO req)
        {
            var reqDto = mapper.Map<SysLoginLogDO>(req);

            await loginLogRepo.CreateLoginLogAsync(reqDto);
        }


    }
}
