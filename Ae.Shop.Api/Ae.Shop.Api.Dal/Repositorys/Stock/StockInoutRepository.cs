using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Stock
{
    public class StockInoutRepository : AbstractRepository<StockInoutDO>, IStockInoutRepository
    {
        public StockInoutRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSqlReadOnly");
        }

        public async Task<int> DeleteStockInout(StockInoutDO request)
        {
            var sql = @"UPDATE stock_inout 
                        SET is_deleted = 0 
                        WHERE
	                        id = @id";

            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, new { id = request.Id }));
            return result;
        }

        public async Task<int> GenerateStockNo(StockInoutDO request)
        {
            var sql = @"UPDATE stock_inout 
                        SET source_inventory_no = @releation_id 
                        WHERE
	                        id = @id";

            var result = -1;
            var param = new DynamicParameters();
            param.Add("@id", request.Id);
            param.Add("@releation_id", request.SourceInventoryNo);

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        public async Task<PagedEntity<StockInOutTempDO>> GetStockInOutPageData(ShopBatchFlowRequest request)
        {
            PagedEntity<StockInOutTempDO> res = new PagedEntity<StockInOutTempDO>();
            var total = 0;
            var conditions = new StringBuilder();
            var param = new DynamicParameters();

            param.Add("@index", (request.PageIndex - 1) * request.PageSize);
            param.Add("@size", request.PageSize);

            var dateTime = new DateTime(2019, 10, 1);
            param.Add("@location_id", request.ShopId);
            conditions.Append(" where 1=1 and si.is_deleted=0 and si.location_id =@location_id ");

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
                conditions.Append(" and si.create_time>=@StartTime");
                param.Add("@StartTime", request.StartTime);
            }

            if (request.EndTime.HasValue &&
                request.EndTime.Value.Subtract(dateTime).TotalDays > 0)
            {
                conditions.Append(" and si.create_time<=@EndTime");
                param.Add("@EndTime", request.EndTime);

            }

            if (!string.IsNullOrWhiteSpace(request.StockId))
            {
                conditions.Append(" and si.id =@StockId ");
                param.Add("@StockId", request.StockId);
            }

            if (!string.IsNullOrWhiteSpace(request.SourceInventoryNo))
            {
                conditions.Append(" and si.source_inventory_no =@SourceInventoryNo ");
                param.Add("@SourceInventoryNo", request.SourceInventoryNo);
            }

            if (!string.IsNullOrWhiteSpace(request.SourceType))
            {
                conditions.Append(" and si.source_type =@source_type ");
                param.Add("@source_type", request.SourceType);
            }


            if (!string.IsNullOrWhiteSpace(request.OperationType) &&
                request.OperationType != "全部")
            {
                string operationType = request.OperationType;

                conditions.Append(" and operation_type =@operation_type ");
                param.Add("@operation_type", operationType);
            }

            var sqlCount = @"SELECT
	                count(DISTINCT si.id) 
                FROM
	                stock_inout si
	                INNER JOIN stock_inout_item sii ON si.id = sii.inout_id and sii.is_deleted=0 " + conditions.ToString();

            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
            });

            //var sql = @"SELECT
            //     si.id StockId,
            //     si.source_inventory_no SourceInventoryNo,
            //     si.location_name LocationName,
            //     si.operation_type OperationType,
            //     si.source_type SourceType,
            //     si.create_by CreateBy,
            //     si.create_time CreateTime 
            //    FROM
            //     stock_inout si
            //     INNER JOIN stock_inout_item sii ON si.id = sii.inout_id " + conditions.ToString()+ " order by si.id desc LIMIT @index, @size";

            var sql = $@"SELECT si.id AS StockId, si.source_inventory_no AS SourceInventoryNo, si.location_name AS LocationName,
                        si.operation_type AS OperationType, si.source_type AS SourceType,si.status Status,si.remark as Remark,
	                    si.create_by AS CreateBy, si.create_time AS CreateTime
                        FROM stock_inout si
                        WHERE si.id IN (
	                        SELECT t.id
	                        FROM (
		                        SELECT id
		                        FROM (
			                        SELECT DISTINCT si.id AS id
			                        FROM stock_inout si
				                        INNER JOIN stock_inout_item sii ON si.id = sii.inout_id and sii.is_deleted=0  
			                          {conditions.ToString()}  
			                        ORDER BY si.id
		                        ) T
		                        ORDER BY t.id DESC
		                        LIMIT @index, @size
	                        ) t
                        )";

            IEnumerable<StockInOutTempDO> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<StockInOutTempDO>(sql, param));
            res.TotalItems = total;
            res.Items = productDOs.ToList();
            return res;
        }

        public async Task<List<SupplierProductStockDTO>> GetSupplierSaleProducts(GetSupplierStockRequest request)
        {
            var conditions = new StringBuilder();
            var para = new DynamicParameters();
            para.Add("@supplier_id", request.VenderId);
            para.Add("@supplier_name", request.VenderShortName);
            if (!string.IsNullOrWhiteSpace(request.ProductId))
            {
                conditions.Append(" and sii.product_id =@product_id");
                para.Add("@product_id", request.ProductId);
            }

            var sql = @"SELECT
	                    sii.product_id ProductId,
	                    sii.product_name ProductName,
	                    sii.num SaleNum,
	                    si.location_id LocationId,
	                    si.location_name LocationName 
                    FROM
	                    stock_inout si
	                    INNER JOIN stock_inout_item sii ON si.id = sii.inout_id 
                    WHERE
	                    si.is_deleted = 0 
	                    AND sii.is_deleted = 0 
	                    AND si.operation_type = '出库' 
	                    AND si.STATUS = '已出库' 
	                    AND si.source_type = '订单安装' 
	                    AND sii.supplier_id = @supplier_id 
	                    AND sii.supplier_name = @supplier_name " + conditions.ToString();

            IEnumerable<SupplierProductStockDTO> productStocks = null;
            await OpenConnectionAsync(async conn => productStocks = await conn.QueryAsync<SupplierProductStockDTO>(sql, para));

            return productStocks?.ToList() ?? new List<SupplierProductStockDTO>();
        }

        public async Task<int> UpdateStockInoutStatus(StockInoutDO request)
        {
            var sql = @"UPDATE stock_inout 
                        SET update_by = @update_by,
                        update_time = SYSDATE( ),
                        status = @status 
                        WHERE
	                        id = @id";

            var param = new DynamicParameters();
            param.Add("@id", request.Id);
            param.Add("@status", request.Status);
            // param.Add("@operate_time", request.OperateTime);
            param.Add("@update_by", request.UpdateBy);

            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }
    }
}

