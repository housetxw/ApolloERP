using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.BasicData.Service.Common.Constant;
using Ae.BasicData.Service.Common.Exceptions;
using Ae.BasicData.Service.Common.Extension;
using Ae.BasicData.Service.Dal.Model;
using Ae.BasicData.Service.Dal.Repositorys.IDAL;

namespace Ae.BasicData.Service.Dal.Repositorys.DAL
{
    public class AppOperationLogRepository : AbstractRepository<AppOperationLogDO>, IAppOperationLogRepository
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<AppOperationLogRepository> logger;

        public AppOperationLogRepository(ApolloErpLogger<AppOperationLogRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("SystemLogSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("SystemLogSqlReadOnly");

            this.logger = logger;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<bool> CreateAppOperationLog(AppOperationLogDO req)
        {
            bool res;
            try
            {
                res = await InsertAsync(req) > 0;
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBInsertException), e);
                throw new CustomException(CommonConstant.InternalError);
            }
            return res;
        }

        // ---------------------------------- 私有方法 --------------------------------------
        
    }
}
