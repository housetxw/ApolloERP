using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.B.Login.Api.Common.Constant;
using Ae.B.Login.Api.Common.Extension;
using Ae.B.Login.Api.Dal.Model;
using Ae.B.Login.Api.Dal.Repositorys.IDAL;

namespace Ae.B.Login.Api.Dal.Repositorys.DAL
{
    public class LoginLogRepository : AbstractRepository<SysLoginLogDO>, ILoginLogRepository
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<LoginLogRepository> logger;

        public LoginLogRepository(ApolloErpLogger<LoginLogRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("LoginAuthenticationSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("LoginAuthenticationSqlReadOnly");

            this.logger = logger;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        public async Task CreateLoginLogAsync(SysLoginLogDO req)
        {
            try
            {
                await InsertAsync(req);
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBInsertException), e);
            }
        }


    }
}
