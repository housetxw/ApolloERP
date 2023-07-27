using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace Ae.Shop.Service.Dal.Repositorys.EmployeePerformance
{
    public class BasicPerformanceConfigRepository : AbstractRepository<BasicPerformanceConfigDO>,
        IBasicPerformanceConfigRepository
    {
        public BasicPerformanceConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");

        }

        public  async   Task<List<BasicPerformanceConfigDO>> GetBasicPerformanceConfigs(GetBasicPerformanceConfigRequest request)
        {
            var configResult = await GetListAsync("where shop_id =@shop_id and is_deleted =0 ", new { shop_id = request.ShopId });
            return configResult?.ToList() ?? new List<BasicPerformanceConfigDO>();

        }

        public async Task<int> InsertEmployeePerformanceLog(EmployeePerformanceLogDO request)
        {
            try
            {
                return await InsertAsync<EmployeePerformanceLogDO>(request);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> UpdateBasicPerformanceConfig(UpdateBasicPerformanceFlagRequest request)
        {
            var sql = @"UPDATE basic_performance_config 
                        SET update_by = @updateBy,
                        update_time = SYSDATE( ),
                        config_flag = @config_flag,
                        config_flag_time = SYSDATE( ) 
                        WHERE
	                        shop_id = @shop_id 
	                        AND is_deleted =0";

            var param = new DynamicParameters();
            param.Add("@config_flag", request.ConfigFlag);
            param.Add("@shop_id", request.ShopId);
            param.Add("@updateBy", request.UpdateBy);
            int result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        /// <summary>
        /// 更新服务的绩效点
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async   Task<int> UpdateBasicPerformancePoint(BasicPerformanceConfigDO request)
        {
            var sql = @"UPDATE basic_performance_config 
                        SET config_type = @config_type,
                        config_point = @config_point,                        
                        update_by = @updateBy,
                        update_time = SYSDATE( ),config_flag =@config_flag,config_flag_time = SYSDATE( )
                        WHERE
	                        shop_id = @shop_id and service_type =@service_type
	                        AND is_deleted =0";

            var param = new DynamicParameters();
            param.Add("@config_type", request.ConfigType);
            param.Add("@shop_id", request.ShopId);
            param.Add("@config_point", request.ConfigPoint);            
            param.Add("@updateBy", request.UpdateBy);
            param.Add("@service_type", request.ServiceType);
            param.Add("@config_flag",request.ConfigFlag);
            
            int result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }
    }
}
