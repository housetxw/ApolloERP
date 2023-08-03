using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys
{

    public class OccupyQueueRepository : AbstractRepository<OccupyQueueDO>, IOccupyQueueRepository
    {
        public OccupyQueueRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopStockSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopStockSqlReadOnly");
        }

        public async Task<int> UpdateOrderProcessStatus(OccupyQueueDO orderQueueDO)
        {
            var updateFileds = new List<string>() { "IsProcessing", "UpdateBy", "UpdateTime" };

            return await UpdateAsync(orderQueueDO, updateFileds);
        }

        public async Task<int> UpdateOrderQueue(OccupyQueueDO orderQueue)
        {
            var sql = @"UPDATE occupy_queue 
                        SET is_processing = @IsProcessing,
                        status = @queue_status, update_by = @update_by,
                        update_time = SYSDATE( ) 
                        WHERE
	                        source_no = @source_no";
            var para = new DynamicParameters();
            para.Add("@source_no", orderQueue.SourceNo);
            para.Add("@IsProcessing", orderQueue.IsProcessing);
            para.Add("@queue_status", orderQueue.Status);
            para.Add("@update_by", orderQueue.UpdateBy);

            var result = -1;

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, para));
            return result;
        }

        /// <summary>
        /// 主键更新
        /// </summary>
        /// <param name="orderQueueDO"></param>
        /// <returns></returns>
        public async Task<int> UpdateOrderQueueStatus(OccupyQueueDO orderQueueDO)
        {
            var updateFileds = new List<string>() { "Status", "UpdateBy", "UpdateTime" };
            return await UpdateAsync(orderQueueDO, updateFileds);
        }

        public async Task<List<OccupyQueueDO>> GetOrderQueues(OccupyQueueDO orderQueueDO)
        {
            string condition = " where 1 =1 and status<>'Cancel'";
            var para = new DynamicParameters();
            if (!string.IsNullOrWhiteSpace(orderQueueDO.SourceNo))
            {
                condition += " and source_no =@QueueNo";
                para.Add("@QueueNo", orderQueueDO.SourceNo);
            }

            if (!string.IsNullOrWhiteSpace(orderQueueDO.Status))
            {
                condition += " and status =@QueueStatus";
                para.Add("@QueueStatus", orderQueueDO.Status);
            }

            var list = await GetListAsync(condition, para, true);
            return list?.ToList() ?? new List<OccupyQueueDO>();
        }

        public async Task<int> UpdateOrderQueueRemark(OccupyQueueDO orderQueue)
        {
            var sql = @"UPDATE occupy_queue 
                        SET remark=@remark,update_by = @update_by,
                        update_time = SYSDATE( ) 
                        WHERE
	                        source_no = @source_no";

            var para = new DynamicParameters();
            para.Add("@source_no", orderQueue.SourceNo);
            para.Add("@remark", orderQueue.Remark);
            para.Add("@update_by", orderQueue.UpdateBy);

            var result = -1;

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, para));
            return result;
        }

        public async Task<int> CancelOrderQueue(OccupyQueueDO orderQueue)
        {
            var sql = @"UPDATE occupy_queue 
                        SET remark = CONCAT( remark, @remark ),
                        update_by = @update_by,
                        update_time = SYSDATE( ),
                        status = @status 
                        WHERE
	                        source_no = @source_no";

            var para = new DynamicParameters();
            para.Add("@source_no", orderQueue.SourceNo);
            para.Add("@remark", orderQueue.Remark);
            para.Add("@update_by", orderQueue.UpdateBy);
            para.Add("@status", orderQueue.Status);

            var result = -1;

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, para));
            return result;
        }
    }

}
