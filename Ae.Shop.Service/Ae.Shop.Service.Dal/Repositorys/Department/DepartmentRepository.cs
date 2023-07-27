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
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;

namespace Ae.Shop.Service.Dal.Repositorys
{
    public class DepartmentRepository : AbstractRepository<DepartmentDO>, IDepartmentRepository
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private IConfiguration configuration;

        private readonly ApolloErpLogger<DepartmentRepository> logger;
        private readonly string className;

        public DepartmentRepository(ApolloErpLogger<DepartmentRepository> logger, IConfiguration configuration)
        {
            this.configuration = configuration;
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");

            this.logger = logger;
            className = this.GetType().Name;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<List<DepartmentDO>> GetDepartmentListByOrgIdType(DepartmentListReqByOrgIdTypeDTO req)
        {
            IEnumerable<DepartmentDO> res = new List<DepartmentDO>();
            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var param = new DynamicParameters();
                    param.Add("@OrganizationId", req.OrganizationId);
                    param.Add("@Type", req.Type);

                    var sql = @"SELECT id, parent_id parentId, name, code, organization_id organizationId, type
                                FROM department WHERE is_deleted = 0
                                AND type = @Type AND organization_id = @OrganizationId 
                                ORDER BY create_time DESC ";
                    res = await conn.QueryAsync<DepartmentDO>(sql, param);
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }
            return res.ToList();
        }


        // ---------------------------------- 私有方法 --------------------------------------


    }
}
