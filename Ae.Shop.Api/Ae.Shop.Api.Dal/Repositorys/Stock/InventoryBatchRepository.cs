using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Stock
{
    public class InventoryBatchRepository : AbstractRepository<InventoryBatchDO>, IInventoryBatchRepository
    {
        public InventoryBatchRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSqlReadOnly");
        }

        public async Task<List<InventoryBatchDO>> GetInventoryBatchByIds(List<long> ids)
        {
            var result = await GetListAsync($" where id in ({string.Join(",", ids)})");
            return result?.ToList() ?? new List<InventoryBatchDO>();
        }

        /// <summary>
        /// 查询门店外采的库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<List<InventoryBatchDO>> GetInventoryBatchs(GetStockInoutItemRequest request)
        {
            string pIds = string.Empty;
            foreach (var item in request.PIds)
            {
                pIds += "'" + item + "',";
            }
            var sql = $@"SELECT
                                id Id,
                                source_inventory_no SourceInventoryNo,
	                            product_id ProductId,
                                product_name ProductName,
	                            batch_no BatchNo,
                                can_use_num CanUseNum
                            FROM
                                inventory_batch
                            WHERE
                                is_deleted = 0
                                AND shop_Id = @shop_Id and own_type='Shop' and product_id in ({pIds.TrimEnd(',')})";

            var param = new DynamicParameters();
            param.Add("@shop_Id", request.ShopId);
            IEnumerable<InventoryBatchDO> result = null;
            await OpenConnectionAsync(async conn => result = await conn.QueryAsync<InventoryBatchDO>(sql, param));
            return result?.ToList() ?? new List<InventoryBatchDO>();
        }

        public async Task<int> UpdateInventoryBatchNum(InventoryBatchDO request)
        {
            var result = -1;
            var sql = @"UPDATE inventory_batch 
                        SET update_by = @update_by,
                        update_time = SYSDATE( ),
                        -- total_num = @total_num,
                        can_use_num = @can_use_num 
                        WHERE
	                        id = @id";


            var param = new DynamicParameters();
            param.Add("@id",request.Id);
            param.Add("@update_by", request.UpdateBy);
            param.Add("@total_num", request.TotalNum);
            param.Add("@can_use_num", request.CanUseNum);

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));

            return result;
        }
    }
}
