using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys
{

    public class StoragePlanRepository : AbstractRepository<StoragePlanDO>, IStoragePlanRepository
    {
        public StoragePlanRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSqlReadOnly");
        }

        /// <summary>
        /// 更新盘库计划状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<int> UpdateStoragePlanStatus(StoragePlanDO request)
        {
            var sql = string.Empty;

            if (request.UpdateType == 1)
            {
                sql = @"UPDATE storage_plan 
                        SET update_by = @update_by,
                        update_time = SYSDATE( ),
                        status = @status
                        WHERE
	                        id = @id";
            }
            else if (request.UpdateType == 2) {
                sql = @"UPDATE storage_plan 
                        SET update_by = @update_by,
                        update_time = SYSDATE( ),actual_time=SYSDATE( ),
                        status = @status
                        WHERE
	                        id = @id";
            }
             
            var param = new DynamicParameters();
            param.Add("@id", request.Id);
            param.Add("@status", request.Status);
            param.Add("@update_by", request.UpdateBy);

            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }
    }
}
