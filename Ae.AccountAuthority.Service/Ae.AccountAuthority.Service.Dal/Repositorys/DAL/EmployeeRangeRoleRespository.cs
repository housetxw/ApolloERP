using Dapper;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.AccountAuthority.Service.Common.Constant;
using Ae.AccountAuthority.Service.Common.Extension;
using Ae.AccountAuthority.Service.Dal.Model;
using Ae.AccountAuthority.Service.Dal.Repositorys.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.AccountAuthority.Service.Dal.Repositorys.DAL
{
    public class EmployeeRangeRoleRespository : AbstractRepository<EmployeeOrganizationRange>, IEmployeeRangeRoleRespository
    {
        private readonly ApolloErpLogger<EmployeeRangeRoleRespository> logger;
        private readonly string className;

        public EmployeeRangeRoleRespository(ApolloErpLogger<EmployeeRangeRoleRespository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");

            this.logger = logger;
            className = this.GetType().Name;
        }

        public async Task<EmployeeOrganizationRange> GetRangeRoleListByIds(string employeeId,long organizationId,int type)
        {
            EmployeeOrganizationRange res =new EmployeeOrganizationRange();

            try
            {
                var param = new DynamicParameters();
                param.Add("@EmployeeId", employeeId);
                param.Add("@OrganizationId", organizationId);
                param.Add("@Type", type);

                var sql = @"select id Id,employee_id EmployeeId,organization_id OrganizationId,type Type,role_ids RoleIds from employee_organization_range where is_deleted=0 and employee_id=@EmployeeId and organization_id=@OrganizationId and type=@Type";

                //var enumerable = await GetListAsync(sql, param);
                //res = enumerable?.FirstOrDefault();
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var enumerable = await conn.QueryAsync<EmployeeOrganizationRange>(sql, param);
                    res = enumerable?.FirstOrDefault();
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(new{ employeeId, organizationId, type }).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
            }
            finally
            {
                logger.Info($"DB: {className}.GetRangeRoleListByIds 请求参数：{JsonConvert.SerializeObject(new { employeeId, organizationId, type })}");
                logger.Info($"DB: {className}.GetRangeRoleListByIds 返回值 Items:{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }
    }
}
