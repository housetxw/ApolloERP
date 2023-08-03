using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.FMS.Service.Core.Model.AccountCheck;
using Ae.FMS.Service.Core.Request.AccountCheck;
using Ae.FMS.Service.Dal.Model.AccountCheck;

namespace Ae.FMS.Service.Dal.Repositorys.Impl
{
    public class ReconciliationFeeRepository : AbstractRepository<ReconciliationFeeDO>, IReconciliationFeeRepository
    {
        public ReconciliationFeeRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("FMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("FMSSqlReadOnly");
        }

        public async Task<List<ReconciliationFeeDO>> GetReconciliationFees(GetReconciliationFeesRequest request)
        {
            var data = await GetListAsync("Where order_no in @OrderNos and is_deleted=0", new {OrderNos = request.OrderNos});

            return data?.ToList();
        }

        public async Task<List<GetNoReconciliationAmountDO>> GetNoReconciliationAmount(GetNoReconciliationAmountRequest request)
        {
            if (request.ShopIds == null || !request.ShopIds.Any())
                return null;
            else
            {
                var sql = @"select  SUM(actual_amount) AS ServiceFee,SUM(shop_install_amount) AS TotalCost, SUM(shop_settlement_amount) AS SettlementFee from reconciliation_fee where is_deleted=0 and shop_id In @ShopIds
                    GROUP BY shop_id";

                var paramters = new DynamicParameters();
                paramters.Add("@ShopIds", request.ShopIds);

                IEnumerable<GetNoReconciliationAmountDO> orderDos = null;
                await OpenConnectionAsync(async conn =>
                    orderDos = await conn.QueryAsync<GetNoReconciliationAmountDO>(sql, paramters));
                return orderDos?.ToList();
            }
        }
        public async Task<int> DeleteReconciliationFee(CalculationReconciliationFeeRequest request)
        {
            int id = 0;

            string sql = "Update reconciliation_fee set is_deleted=1,update_by=@UpdateBy,update_time=NOW(3) where order_no = @OrderNo and is_deleted=0 ";
            var parameters = new DynamicParameters();
            parameters.Add("@UpdateBy", request.CreateBy);
            parameters.Add("@OrderNo", request.OrderNo);
            await OpenSlaveConnectionAsync(async conn => { id = await conn.ExecuteAsync(sql, parameters); });
            return id;
        }
    }
}
