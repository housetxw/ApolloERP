using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public class StockManageRepository : AbstractRepository<InventoryDO>, IStockManageRepository
    {
        public StockManageRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSqlReadOnly");
        }

        public async Task<PagedEntity<InventoryDO>> GetInventoryPageData(ShopStockRequest request)
        {
            var conditions = new StringBuilder();
            var parames = new DynamicParameters();
            conditions.Append(" where is_deleted=0 ");

            //根据货主区分
            //if (request.Type != 1) { 

            //}
            if (request.LocationId > 0)
            {
                parames.Add("@location_id", request.LocationId);
                conditions.Append(" and location_id=@location_id");
            }

            if (request.isStock)
            {
                conditions.Append(" and can_use_num>0");
            }
            else {
                conditions.Append(" and can_use_num<=0");
            }

            if (!string.IsNullOrWhiteSpace(request.SourceType))
            {
                parames.Add("@source_type",request.SourceType);
                conditions.Append(" and source_type=@source_type");
            }

            if (!string.IsNullOrWhiteSpace(request.ProductId))
            {
                var productId = request.ProductId;
                var productNames = request.ProductId.Split(new char[] { ' ', '　', ',', '，', ';', '；', '\\', '-', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                if (productNames.Any())
                {
                    string productName = string.Join("%", productNames);
                    conditions.Append(" AND (product_name LIKE N'%" + productName.Replace("'", "''") + "%'  OR product_id=@productId)");
                    parames.Add("@productId", productId);
                }
            }
            var result = await GetListPagedAsync(request.PageIndex, request.PageSize, conditions.ToString(), "id desc", parames);
            return result;
        }


        public async Task<int> UpdateInventoryData(InventoryDO request)
        {
            var sql = @"UPDATE inventory 
                        SET total_num = @total_num,
                        total_cost = @total_cost,
                        can_use_num = @can_use_num ,update_by = @update_by,
                        update_time = SYSDATE( )
                        WHERE
	                        location_id = @location_id 
	                        AND product_id = @product_id 
	                   
	                        AND is_deleted =0";

            var param = new DynamicParameters();
            param.Add("@total_num", request.TotalNum);
            param.Add("@total_cost", request.TotalCost);
            param.Add("@can_use_num", request.CanUseNum);
            param.Add("@location_id", request.LocationId);
            param.Add("@product_id", request.ProductId);
            //param.Add("@pkid", request.Id);
            param.Add("@update_by", request.UpdateBy);

            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }
    }
}
