using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace Ae.Shop.Service.Dal.Repositorys
{
    public class ShopPerformanceConfigRepository : AbstractRepository<ShopPerformanceConfigDO>, IShopPerformanceConfigRepository
    {
        public ShopPerformanceConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");

        }

        public async Task<List<ShopPerformanceConfigDO>> GetShopPerformanceConfig(GetBasicPerformanceConfigRequest request)
        {
            var configResult = await GetListAsync("where shop_id =@shop_id and is_deleted =0 ", new { shop_id = request.ShopId });
            return configResult?.ToList() ?? new List<ShopPerformanceConfigDO>();
        }

        public async Task<int> DeleteShopPerformanceConfig(ShopPerformanceConfigDO request)
        {
            var param = new DynamicParameters();
            param.Add("@updateBy", request.UpdateBy);
            param.Add("@id", request.Id);

            var sql = @"UPDATE shop_performance_config 
	                SET update_by = @updateBy,
	                update_time = SYSDATE( ),
                    is_deleted =1 
                WHERE
	                id = @id 
	                AND is_deleted =0;";

            int result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        public async Task<int> UpdateShopPerformanceConfig(ShopPerformanceConfigDO request)
        {
            var sql = @"UPDATE shop_performance_config 
                        SET type_value = @type_value,
                        config_type = @config_type,
                        config_point = @config_point,
                        update_by = @updateBy,
                        update_time = SYSDATE( )
                        WHERE
	                        id = @id 
	                        AND is_deleted = 0";
            var param = new DynamicParameters();            
            param.Add("@type_value", request.TypeValue);
            param.Add("@config_type", request.ConfigType);
            param.Add("@config_point", request.ConfigPoint);
          
            param.Add("@updateBy", request.UpdateBy);
            param.Add("@id", request.Id);
            int result = -1;
            await OpenConnectionAsync(async conn => result =await conn.ExecuteAsync(sql, param));
            return result;
        }
    }
}

