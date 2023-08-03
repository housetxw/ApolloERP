using System;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Rg.LoginAuthentication.Api.Dal.Repositorys.IDAL;
using ApolloErp.Log;
using Ae.C.Login.Api.Common.Constant;
using Newtonsoft.Json;
using Ae.C.Login.Api.Dal.Model;

namespace Rg.LoginAuthentication.Api.Dal.Repositorys.DAL
{
    public class LoginLogRepository : AbstractRepository<SysLoginLogDO>, ILoginLogRepository
    {
        private readonly ApolloErpLogger<LoginLogRepository> logger;

        public LoginLogRepository(ApolloErpLogger<LoginLogRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("UserSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("UserSqlReadOnly");

            this.logger = logger;
        }


        public async Task CreateLoginLogAsync(SysLoginLogDO req)
        {
            try
            {
                await InsertAsync(req);
            }
            catch (Exception e)
            {
                var msg = $"{CommonConstant.ExceptionOccured}" +
                          $"\n{CommonConstant.ParameterReqDetail}{JsonConvert.SerializeObject(req)}";
                logger.Error(msg, e);
            }
        }


    }
}
