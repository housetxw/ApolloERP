using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public class AllotProductRepository : AbstractRepository<AllotProductDO>, IAllotProductRepository
    {
        public AllotProductRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSqlReadOnly");
        }

        //public async Task<int> DeleteAllotProduct(DeleteAllotProductRequest request)
        //{
        //    var sql = @"UPDATE allot_product 
        //                SET is_deleted = 1,
        //                update_by = @update_by,
        //                update_time = SYSDATE( ) 
        //                WHERE
	       //                 id IN @ids";

        //    var para = new DynamicParameters();
        //    para.Add("ids", request.Ids);
        //    para.Add("update_by", request.UpdateBy);

        //    var result = -1;
        //    await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, para));
        //    return result;

        //}

        public async Task<int> UpdateAllotProductStock(AllotProductDO request)
        {
            var sql = @"UPDATE allot_product 
                        SET update_by = @update_by,
                        update_time = SYSDATE( ),
                        stock_id = @stock_id,
                        batch_id = @batch_id 
                        WHERE
	                        id = @id";

            var para = new DynamicParameters();
            para.Add("id", request.Id);
            para.Add("update_by", request.UpdateBy);
            para.Add("batch_id", request.BatchId);
            para.Add("stock_id", request.StockId);
            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, para));
            return result;
        }
    }
}
