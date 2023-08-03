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
    public class InventoryFlowItemRepository : AbstractRepository<InventoryFlowItemDO>, IInventoryFlowItemRepository
    {
        public InventoryFlowItemRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSqlReadOnly");
        }

        public async Task<int> ReleaseInventoryFlowItem(InventoryFlowItemDO request)
        {
            var param = new DynamicParameters();
            param.Add("@update_by", request.UpdateBy);
            param.Add("@itemIds", request.StockIds);

            var result = -1;
            var sql = @"UPDATE inventory_flow_item 
                        SET after_occupy_num = 0,
                        update_by = @update_by,
                        update_time = SYSDATE( ),
                        business_category = 5,is_deleted=1
                        WHERE
	                        id IN @itemIds 
	                        AND is_deleted =0";

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;

        }

        public async Task<List<InventoryFlowItemDO>> SelectTargetValues(List<long> shopIds, List<long> batchIds)
        {
            IEnumerable<InventoryFlowItemDO> result = null;
            var sql = @"SELECT
	                    After_Occupy_Num AfterOccupyNum,
	                    After_Lock_Num AfterLockNum,
	                    product_id ProductId,
	                    location_id LocationId,
	                    batch_no BatchNo
                    FROM
	                    inventory_flow_item 
                    WHERE
	                    is_deleted = 0 
	                    AND location_id IN @shopIds and batch_no in @batch_nos";

            var param = new DynamicParameters();
            param.Add("@shopIds", shopIds);
            param.Add("@batch_nos", batchIds);


            await OpenConnectionAsync(async conn => result = await conn.QueryAsync<InventoryFlowItemDO>(sql, param));
            return result?.ToList() ?? new List<InventoryFlowItemDO>();

        }

        /// <summary>
        /// 释放占用库存  回写库存数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UpdateInventoryFlowItemOccupy(InventoryFlowItemDO request)
        {
            var param = new DynamicParameters();
            param.Add("@update_by", request.UpdateBy);
            param.Add("@itemIds", request.StockIds);

            param.Add("@before_total_num", request.BeforeTotalNum);
            param.Add("@after_total_num", request.AfterTotalNum);
            param.Add("@change_total_num", request.ChangeTotalNum);
            param.Add("@before_can_use_num", request.BeforeCanUseNum);
            param.Add("@after_can_use_num", request.AfterCanUseNum);
            param.Add("@change_can_use_num", request.ChangeCanUseNum);

            var result = -1;
            var sql = @"UPDATE inventory_flow_item 
                        SET after_occupy_num = 0,
                        update_by = @update_by,
                        update_time = SYSDATE( ),
                        business_category = 5,
                        before_total_num = @before_total_num,
                        after_total_num = @after_total_num,
                        change_total_num = @change_total_num,
                        before_can_use_num = @before_can_use_num,
                        after_can_use_num = @after_can_use_num,
                        change_can_use_num = @change_can_use_num 
                        WHERE
	                        id IN @itemIds 
	                        AND is_deleted =0 ";

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }
    }

}
