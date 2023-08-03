using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.ShopPurchase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.ShopPurchase.Imp
{
    public class PurchaseOrderProductRepository : AbstractRepository<PurchaseOrderProductDO>, IPurchaseOrderProductRepository
    {
        public PurchaseOrderProductRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopPurchaseSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopPurchaseSqlReadOnly");
        }

        public async  Task<int> BatchUpdatePurchaseOrderProductStatus(List<long> purchaseIds,string updateBy)
        {
            var param = new DynamicParameters();
            param.Add("@update_by", updateBy);

            param.Add("@po_ids", purchaseIds);
            var result = -1;
            var sql = @"UPDATE purchase_order_product 
                        SET update_by = @update_by,
                        update_time = SYSDATE( ),
                        status = 10
                        WHERE
	                        po_id in @po_ids";

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        public async Task<int> DeletePurchaseOrderProduct(PurchaseOrderProductDO request)
        {
            var param = new DynamicParameters();
            param.Add("@update_by", request.UpdateBy);

            param.Add("@po_id", request.PoId);
            var result = -1;
            var sql = @"UPDATE purchase_order_product 
                        SET update_by = @update_by,
                        update_time = SYSDATE( ),
                        status = 3 
                        WHERE
	                        po_id = @po_id 
	                        AND status <>3";

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }
    }
}
