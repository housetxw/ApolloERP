using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace Ae.Shop.Api.Dal.Repositorys
{
    public class OccupyItemRepository : AbstractRepository<OccupyItemDO>, IOccupyItemRepository
    {
        public OccupyItemRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopStockSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopStockSqlReadOnly");
        }

        /// <summary>
        /// 释放占用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> ReleaseOccupyItemStatus(OccupyItemDO request)
        {
            var param = new DynamicParameters();
            param.Add("@update_by", request.UpdateBy);
            param.Add("@source_inventory_no", request.SourceInventoryNo);

            var result = -1;
            var sql = @"UPDATE occupy_item 
                        SET is_valid = 3,
                        update_by = @update_by,
                        update_time = SYSDATE( ) 
                        WHERE
	                        source_inventory_no = @source_inventory_no and is_deleted=0 and is_valid=1; ";

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        /// <summary>
        /// 更新订单占库有效性状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<int> UpdateOccupyItemValid(OccupyItemDO request)
        {
            var param = new DynamicParameters();
            param.Add("@update_by", request.UpdateBy);
            param.Add("@source_inventory_no", request.SourceInventoryNo);
            param.Add("@is_valid",request.IsValid);

            var result = -1;
            var sql = @"UPDATE occupy_item 
                        SET is_valid = @is_valid,
                        update_by = @update_by,
                        update_time = SYSDATE( ) 
                        WHERE
	                        source_inventory_no = @source_inventory_no and is_deleted=0 and is_valid<>3; ";

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }
    }
}

