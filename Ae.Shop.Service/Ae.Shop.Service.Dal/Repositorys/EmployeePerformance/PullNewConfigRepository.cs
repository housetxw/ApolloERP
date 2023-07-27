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
    public class PullNewConfigRepository : AbstractRepository<PullNewConfigDO>, IPullNewConfigRepository
    {
        public PullNewConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");

        }

        public async Task<PullNewConfigDO> GetPullNewConfig(GetPullNewConfigRequest request)
        {
            var configResult = await GetListAsync("where shop_id =@shop_id and is_deleted =0 ", new { shop_id = request.ShopId });
            return configResult?.FirstOrDefault();
        }

        public async Task<int> UpdatePullNewConfigFlag(UpdatePullNewFlagRequest request)
        {
            var sql = @"";
            var param = new DynamicParameters();
            param.Add("@updateBy", request.UpdateBy);
            param.Add("@shop_id", request.ShopId);

            if (request.Type == 1)
            {
                sql = @"UPDATE pull_new_config 
                        SET update_by = @updateBy,
                        update_time = SYSDATE( ),
                        pull_new_time = SYSDATE( ),
                        pull_new_flag = @pull_new_flag 
                        WHERE
	                        shop_id = @shop_id 
	                        AND is_deleted = 0;";

                param.Add("@pull_new_flag", request.PullNewFlag);
            }
            else if (request.Type == 2)
            {
                sql = @"UPDATE pull_new_config 
	                    SET update_by = @updateBy,
	                    update_time = SYSDATE( ),
	                    first_consume_flag = @first_consume_flag,
	                    first_consume_time = SYSDATE( ) 
                    WHERE
	                    shop_id = @shop_id 
	                    AND is_deleted =0;";

                param.Add("@first_consume_flag", request.FirstConsumeFlag);
            }
            else if (request.Type == 3) {
                sql = @"UPDATE pull_new_config 
	                    SET update_by = @updateBy,
	                    update_time = SYSDATE( ),
	                    first_consume_flag = @first_consume_flag,
	                    first_consume_time = SYSDATE( ) ,
                        pull_new_time = SYSDATE( ),
                        pull_new_flag = @pull_new_flag 
                    WHERE
	                    shop_id = @shop_id 
	                    AND is_deleted =0;";

                param.Add("@first_consume_flag", request.FirstConsumeFlag);
                param.Add("@pull_new_flag", request.PullNewFlag);
            }
            int result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        public async Task<int> UpdatePullNewConfig(PullNewConfigDO request)
        {
            var sql = @"UPDATE pull_new_config 
                        SET pull_new_time = SYSDATE( ),
                        pull_new_flag = @pull_new_flag,
                        pull_new_point = @pull_new_point,
                        first_consume_flag = @first_consume_flag,
                        first_consume_time = SYSDATE( ),
                        first_consume_type = @first_consume_type,
                        first_consume_point = @first_consume_point,
                        update_by = @updateBy,
                        update_time = SYSDATE( )
                        WHERE
	                        shop_id = @shop_id 
	                        AND is_deleted = 0";
            var param = new DynamicParameters();            
            param.Add("@first_consume_flag", request.FirstConsumeFlag);
            param.Add("@first_consume_type", request.FirstConsumeType);
            param.Add("@first_consume_point", request.FirstConsumePoint);

            param.Add("@pull_new_flag", request.PullNewFlag);
            param.Add("@pull_new_point", request.PullNewPoint);            
            param.Add("@updateBy", request.UpdateBy);
            param.Add("@shop_id", request.ShopId);
            int result = -1;
            await OpenConnectionAsync(async conn => result =await conn.ExecuteAsync(sql, param));
            return result;
        }
    }
}

