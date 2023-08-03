using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.FMS.Service.Core.Request.Settlement;
using Ae.FMS.Service.Dal.Model.AccountCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.FMS.Service.Dal.Repositorys.Impl
{
    public class PurchaseSettlementBatchRepository : AbstractRepository<PurchaseSettlementBatchDO>, IPurchaseSettlementBatchRepository
    {
        public PurchaseSettlementBatchRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("FMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("FMSSqlReadOnly");

        }

        public async Task<PagedEntity<PurchaseSettlementBatchDO>> GetPurchaseSettlementList(GetPurchaseSettlementListRequest request)
        {
            var res = new PagedEntity<PurchaseSettlementBatchDO>();


            var condition = new StringBuilder();
            var param = new DynamicParameters();

            condition.Append(" where 1 =1");

            if (request.PayStatus>=0)
            {
                condition.Append(" and status =@Status");
                param.Add("@Status", request.PayStatus);
            }
           

            if (!string.IsNullOrWhiteSpace(request.SettlementBatchNo))
            {
                condition.Append(" and settlement_batch_no =@SettlementBatchNo");
                param.Add("@SettlementBatchNo", request.SettlementBatchNo);
            }

            if (request.ShopId > 0)
            {
                condition.Append(" and location_id =@location_id");
                param.Add("@location_id", request.ShopId);
            }

            if (!string.IsNullOrWhiteSpace(request.StartSettlementTime))
            {
                DateTime.TryParse(request.StartSettlementTime, out var startTime);

                condition.Append($" and settlement_year >={startTime.Year}");

                condition.Append($" and settlement_month >={startTime.Month}");
            }

            if (!string.IsNullOrWhiteSpace(request.EndSettlementTime))
            {
                DateTime.TryParse(request.EndSettlementTime, out var endTime);

                condition.Append($" and settlement_year <={endTime.Year}");

                condition.Append($" and settlement_month <={endTime.Month}");
            }

            var list = await GetListPagedAsync(request.PageIndex, request.PageSize, condition.ToString(), "id desc", param);

            return list;

        }
    }
}
