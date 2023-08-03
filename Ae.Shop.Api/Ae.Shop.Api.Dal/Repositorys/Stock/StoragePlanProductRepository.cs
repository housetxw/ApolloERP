using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Ae.Shop.Api.Core.Request;
using System.Linq;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public class StoragePlanProductRepository : AbstractRepository<StoragePlanProductDO>, IStoragePlanProductRepository
    {
        public StoragePlanProductRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSqlReadOnly");
        }

        /// <summary>
        /// 盘点中的数据可以撤销
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> CancelStoragePlanProduct(StoragePlanProductDO request)
        {
            var sql = @"UPDATE storage_plan_product 
                        SET update_by = @update_by,
                        update_time = SYSDATE( ),
                        status = @status,
                        storage_num = 0 
                        WHERE
	                        id = @id";

            var param = new DynamicParameters();
            param.Add("@id", request.Id);
            param.Add("@status", request.Status);
            param.Add("@update_by", request.UpdateBy);

            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }


        public async Task<int> DealStorageProduct(StoragePlanProductDO request)
        {
            var sql = @"UPDATE storage_plan_product 
                        SET update_by = @update_by,
                        update_time = SYSDATE( ),
                        status = @status,
                        deal_by = @deal_by,
                        deal_type = @deal_type,
                        deal_remark = @deal_remark,
                        deal_time = SYSDATE( ) 
                        WHERE
	                        id = @id";

            var param = new DynamicParameters();
            param.Add("@id", request.Id);
            param.Add("@status", request.Status);
            param.Add("@update_by", request.UpdateBy);

            param.Add("@deal_remark", request.DealRemark);
            param.Add("@deal_type", request.DealType);
            param.Add("@deal_by", request.DealBy);

            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        public async Task<StockDiffDO> GetStockDiffDetail(StoragePlanProductDO request)
        {
            StockDiffDO result = null;
            var sql = @"SELECT
	                    sp.plan_no PlanNo,spp.id Id,
	                    sp.warehouse_id WarehouseId,
	                    sp.warehouse_name WarehouseName,
	                    sp.plan_name PlanName,
	                    sp.type PlanType,
	                    spp.plan_id PlanId,spp.product_type ProductType,
	                    spp.product_id ProductId,
	                    spp.product_name ProductName,
	                    spp.system_num SystemNum,
	                    spp.storage_num StorageNum,
	                    spp.diff_num DiffNum,spp.unit Unit,
	                    spp.status Status,
	                    spp.deal_by DealBy,
	                    spp.deal_time DealTime,
	                    spp.deal_type DealType,
	                    spp.deal_remark DealRemark, spp.operate_by OperateBy,
	                    spp.operate_time OperateTime 
                    FROM
	                    storage_plan sp
	                    INNER JOIN storage_plan_product spp ON sp.id = spp.plan_id 
                    WHERE
	                    spp.id = @id 
	                    AND spp.is_deleted =0";

            await OpenConnectionAsync(async conn => result = await conn.QueryFirstAsync<StockDiffDO>(sql, new { id = request.Id }));

            return result;
        }

        public async Task<PagedEntity<StockDiffDO>> GetStockDiffs(StockDiffRequest request)
        {
            PagedEntity<StockDiffDO> res = new PagedEntity<StockDiffDO>();
            var total = 0;
            var conditions = new StringBuilder();
            var param = new DynamicParameters();

            param.Add("@index", (request.PageIndex - 1) * request.PageSize);
            param.Add("@size", request.PageSize);

            var dateTime = new DateTime(2019, 10, 1);
            param.Add("@location_id", request.ShopId);

            if (request.PlanId > 0)
            {
                conditions.Append(" and sp.id =@id ");
                param.Add("@id", request.PlanId);
            }
            if (request.WarehouseId > 0)
            {
                conditions.Append(" and sp.warehouse_id =@warehouse_id ");
                param.Add("@warehouse_id", request.WarehouseId);
            }


            if (!string.IsNullOrWhiteSpace(request.ProductId))
            {
                var productId = request.ProductId;
                var productNames = request.ProductId.Split(new char[] { ' ', '　', ',', '，', ';', '；', '\\', '-', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                if (productNames.Any())
                {
                    string productName = string.Join("%", productNames);
                    conditions.Append(" AND (product_name LIKE N'%" + productName.Replace("'", "''") + "%'  OR product_id=@productId)");
                    param.Add("@productId", productId);
                }
            }

            if (request.StartTime.HasValue &&
               request.StartTime.Value.Subtract(dateTime).TotalDays > 0)
            {
                conditions.Append(" and spp.operate_time>=@StartTime");
                param.Add("@StartTime", request.StartTime);
            }

            if (request.EndTime.HasValue &&
                request.EndTime.Value.Subtract(dateTime).TotalDays > 0)
            {
                conditions.Append(" and spp.operate_time<=@EndTime");
                param.Add("@EndTime", request.EndTime);

            }

            if (!string.IsNullOrWhiteSpace(request.PlanNo))
            {
                conditions.Append(" and sp.plan_no =@plan_no ");
                param.Add("@plan_no", request.PlanNo);
            }

            if (!string.IsNullOrWhiteSpace(request.Status))
            {
                conditions.Append(" and spp.status =@status");
                param.Add("@status", request.Status);
            }

            if (!string.IsNullOrWhiteSpace(request.SourceType)) {
                conditions.Append(" and sp.source_type =@source_type");
                param.Add("@source_type", request.SourceType);
            }

            if (request.Type > 1)
            {
                if (request.Type == 2)
                {
                    conditions.Append(" and spp.diff_num<0 ");
                }
                else if (request.Type == 3)
                {
                    conditions.Append(" and spp.diff_num>0 ");
                }
            }

            var sqlCount = @"SELECT
	                        count( spp.id ) 
                        FROM
	                        storage_plan sp
	                        INNER JOIN storage_plan_product spp ON sp.id = spp.plan_id 
                        WHERE
	                        spp.is_deleted = 0 
	                        AND sp.shop_id = @location_id and spp.diff_num!=0 and (spp.status<>'盘点中' or spp.status<>'新建')" + conditions.ToString();

            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
            });

            var sql = @"SELECT spp.id Id,
	                    sp.plan_no PlanNo,sp.source_type SourceType,
	                    spp.plan_id PlanId,
	                    sp.warehouse_id WarehouseId,
	                    sp.warehouse_name WarehouseName,
	                    spp.product_id ProductId,
	                    spp.product_name ProductName,
	                    spp.system_num SystemNum,
	                    spp.storage_num StorageNum,
	                    spp.diff_num DiffNum, 
	                    spp.status Status,
	                    spp.deal_by DealBy,
	                    spp.deal_time DealTime,
	                    spp.operate_by OperateBy,
	                    spp.operate_time OperateTime 
                    FROM
	                    storage_plan sp
	                    INNER JOIN storage_plan_product spp ON sp.id = spp.plan_id 
                    WHERE
	                    spp.is_deleted = 0 and sp.shop_id=@location_id and spp.diff_num!=0 and (spp.status<>'盘点中' or spp.status<>'新建')
                " + conditions.ToString()+ " order by spp.operate_time desc LIMIT @index, @size";

            IEnumerable<StockDiffDO> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<StockDiffDO>(sql, param));
            res.TotalItems = total;
            res.Items = productDOs.ToList();
            return res;
        }

        /// <summary>
        /// 更新为盘点中  填入差异数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UpdateProductStorageNum(StoragePlanProductDO request)
        {
            var sql = @"UPDATE storage_plan_product 
                        SET update_by = @update_by,
                        update_time = SYSDATE( ),
                        status = @status,
                        storage_num = @storage_num,
                        system_num =@system_num,
                        diff_num = @diff_num,
                        operate_by = @update_by,
                        operate_time = SYSDATE( ) 
                        WHERE
	                        id = @id";

            var param = new DynamicParameters();
            param.Add("@id", request.Id);
            param.Add("@status", request.Status);
            param.Add("@update_by", request.UpdateBy);
            param.Add("@system_num", request.SystemNum);
            param.Add("@storage_num", request.StorageNum);
            param.Add("@diff_num", request.DiffNum);

            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        /// <summary>
        /// 盘库完成 更新盘库产品状态为"盘点完成"
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UpdateStoragePlanStatus(StoragePlanProductDO request)
        {
            var sql = @"UPDATE storage_plan_product 
                        SET update_by = @update_by,
                        update_time = SYSDATE( ),
                        status = @status
                        WHERE
	                        id = @id";

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
