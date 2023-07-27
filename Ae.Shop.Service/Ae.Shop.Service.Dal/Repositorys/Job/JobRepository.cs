using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Common.Exceptions;
using Ae.Shop.Service.Common.Extension;
using Ae.Shop.Service.Core.Request.ShopServer;
using Ae.Shop.Service.Dal.Model;

namespace Ae.Shop.Service.Dal.Repositorys
{
    public class JobRepository : AbstractRepository<JobDO>, IJobRepository
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private IConfiguration configuration;

        private readonly ApolloErpLogger<JobRepository> logger;
        private readonly string className;

        public JobRepository(ApolloErpLogger<JobRepository> logger, IConfiguration configuration)
        {
            this.configuration = configuration;
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");

            this.logger = logger;
            className = this.GetType().Name;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<List<JobDO>> GetJobListByType(JobListReqByTypeDTO req)
        {
            List<JobDO> res = null;
            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var param = new DynamicParameters();
                    param.Add("@Type", req.Type);

                    var sql = @"SELECT id, name, code, organization_id organizationId, type 
                                FROM job
                                WHERE is_deleted = 0 AND type = @Type";
                    res = (await conn.QueryAsync<JobDO>(sql, param)).ToList();
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }
            return res;
        }

        public async Task<List<JobDO>> GetJobListByOrganizationId(JobListReqByOrgIdDTO req)
        {
            List<JobDO> res = null;
            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var param = new DynamicParameters();
                    param.Add("@OrganizationId", req.OrganizationId);

                    var sql = @"SELECT id, name, code, organization_id organizationId, type 
                                FROM job
                                WHERE is_deleted = 0 AND organization_id = @OrganizationId";
                    res = (await conn.QueryAsync<JobDO>(sql, param)).ToList();
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }
            return res;
        }

        public async Task<List<WorkKindDO>> GetWorkKindList()
        {
            List<WorkKindDO> res = null;
            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var sql = @"SELECT id, name FROM work_kind WHERE is_deleted = 0";
                    res = (await conn.QueryAsync<WorkKindDO>(sql)).ToList();
                });
            }
            catch (Exception e)
            {
                logger.Error(string.Empty.GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }
            return res;
        }


        // ---------------------------------- 私有方法 --------------------------------------


    }
}
