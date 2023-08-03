using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
using ApolloErp.Data.DapperExtensions;
using System.Threading.Tasks;
using Ae.Shop.Api.Core.Request;
using Dapper;
using System.Linq;
using Ae.Shop.Api.Dal.Model;
using Ae.Shop.Api.Core.Model;

namespace Ae.Shop.Api.Dal.Repositorys
{

    public class StockInoutItemRepository : AbstractRepository<StockInoutItemDO>, IStockInoutItemRepository
    {
        public StockInoutItemRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSqlReadOnly");
        }

        public async Task<int> DeleteStockInoutItem(StockInoutItemDO request)
        {
            var sql = @"UPDATE stock_inout_item 
                        SET is_deleted = 0 
                        WHERE
	                        id = @id";

            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, new { id = request.Id }));
            return result;
        }

        public async Task<List<StockInoutItemDO>> GetStockInoutItemsByRelationIdAndType(GetStockInoutItemRequest request)
        {
            var sql = @"SELECT
	                        ib.can_use_num AvailableNum, 
	                        sii.batch_no BatchNo,
	                        sii.product_id ProductId,
	                        sii.product_name ProductName,
	                        sii.releation_id  ReleationId
                        FROM
	                        stock_inout si
	                        INNER JOIN stock_inout_item sii ON si.id = sii.inout_id left join inventory_batch ib on ib.id =sii.batch_no 
                        WHERE
	                        si.is_deleted = 0 
	                        AND sii.is_deleted = 0 and si.source_inventory_no=@source_inventory_no
                            and source_type =@source_type";

            IEnumerable<StockInoutItemDO> result = null;
            var param = new DynamicParameters();
            param.Add("@source_inventory_no", request.ObjectId);
            param.Add("@source_type", request.ObjectType);

            await OpenConnectionAsync(async conn => result = await conn.QueryAsync<StockInoutItemDO>(sql, param));
            return result?.ToList() ?? new List<StockInoutItemDO>();
        }

        public async Task<List<StockInOutTempDTO>> GetStockInoutItemsByPID(GetShopStockRequest request)
        {
            var sql = @"SELECT
	                        i.id as Id,i.inout_id as InoutId,
												  s.source_inventory_no as SourceInventoryNo, i.releation_id as ReleationId,
													s.location_id as LocationId, s.location_name as LocationName,
													s.operation_type as OperationType, s.source_type as SourceType,
													i.`status` as Status, i.create_by as CreateBy ,i.create_time as CreateTime,
	                        i.batch_no BatchNo,
	                        i.product_id ProductId,
	                        i.product_name ProductName,
													i.num as Num, i.actual_num as ActualNum,
													i.uom_text as UomText
                        FROM
	                        stock_inout s
	                        INNER JOIN stock_inout_item i ON s.id = i.inout_id 
                        WHERE
	                        s.is_deleted = 0 
	                        AND i.is_deleted = 0 
													and s.location_id=@location_id
                            and i.product_id =@product_id
							";

            IEnumerable<StockInOutTempDTO> result = null;
            var param = new DynamicParameters();
            param.Add("@location_id", request.ShopId);
            param.Add("@product_id", request.ProductId);

            await OpenConnectionAsync(async conn => result = await conn.QueryAsync<StockInOutTempDTO>(sql, param));
            return result?.ToList() ?? new List<StockInOutTempDTO>();
        }

        public async Task<List<StockInoutItemDO>> GetStockInoutItemsByStockId(StockInoutItemDO request)
        {
            var result = await GetListAsync(" where inout_id =@inout_id and is_deleted=0 ", new { inout_id = request.InoutId });

            return result?.ToList() ?? new List<StockInoutItemDO>();
        }

        /// <summary>
        /// 回写批次
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UpdateStockInoutBatch(StockInoutItemDO request)
        {
            var sql = string.Empty;
            var param = new DynamicParameters();
            param.Add("@batch_no", request.BatchNo);
            if (request.UpdateType == 1)
            {
                sql = @"UPDATE stock_inout_item 
                        SET batch_no = CONCAT( batch_no, @batch_no ) 
                        WHERE
	                        id = @id ";
                param.Add("@id", request.Id);
            }
            else if (request.UpdateType == 2)
            {
                sql = @"UPDATE stock_inout_item 
                        SET batch_no = CONCAT( batch_no, @batch_no ) 
                        WHERE
	                        inout_id = @inout_id 
	                        AND releation_id = @releation_id ";
                param.Add("@releation_id", request.ReleationId);
                param.Add("@inout_id", request.InoutId);
            }
            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        public async Task<int> UpdateStockInoutItem(StockInoutItemDO request)
        {
            var sql = @"UPDATE stock_inout_item 
                        SET actual_num = @actual_num,
                        bad_num = @bad_num,
                        update_by = @update_by,
                          status =@status,good_num=good_num+@good_num,
                        update_time = SYSDATE( )
                        WHERE
	                        inout_id = @inout_id 
	                        AND releation_id = @releation_id";

            var param = new DynamicParameters();
            param.Add("@inout_id", request.InoutId);
            param.Add("@releation_id", request.ReleationId);
            param.Add("@actual_num", request.ActualNum);
            param.Add("@bad_num", request.BadNum);
            param.Add("@update_by", request.UpdateBy);
            param.Add("@status",request.Status);
            param.Add("@good_num",request.GoodNum);
           // param.Add("@batch_no",request.BatchNo);
            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UpdateStockInoutRelationId(StockInoutItemDO request)
        {
            var sql = @"UPDATE stock_inout_item 
                        SET releation_id = @releation_id 
                        WHERE
	                        id = @id";

            var param = new DynamicParameters();
            param.Add("@id", request.Id);
            param.Add("@releation_id", request.ReleationId);
            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }


    }
}