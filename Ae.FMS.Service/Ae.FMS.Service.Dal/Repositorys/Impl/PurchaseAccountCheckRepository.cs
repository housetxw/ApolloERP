using Dapper;
using Ae.FMS.Service.Core.Model.AccountCheck;
using Ae.FMS.Service.Core.Request.AccountCheck;
using Ae.FMS.Service.Dal.Model.AccountCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ApolloErp.Data.DapperExtensions;
using Ae.FMS.Service.Core.Enums;

namespace Ae.FMS.Service.Dal.Repositorys.Impl
{
    public class PurchaseAccountCheckRepository : AbstractRepository<PurchaseAccountCheckDO>, IPurchaseAccountCheckRepository
    {
        public PurchaseAccountCheckRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("FMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("FMSSqlReadOnly");

        }

        /// <summary>
        /// 得到采购对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<PurchaseAccountCheckDO>> GetPurchaseAccountList(GetPurchaseAccountListRequest request)
        {
            var res = new PagedEntity<PurchaseAccountCheckDO>();


            var condition = new StringBuilder();
            var param = new DynamicParameters();

            condition.Append("where is_deleted=0");

            if (!string.IsNullOrEmpty(request.Status))
            {
                condition.Append(" and status =@Status");
                param.Add("@Status", request.Status);
            }
            //else
            //{
            //    condition.Append(" and status ='" + AccountCheckResultEnum.未核对.ToString() + "'");
            //}

            if (!string.IsNullOrWhiteSpace(request.SettlementType))
            {
                condition.Append(" and settlement_type =@SettlementType");
                param.Add("@SettlementType", request.SettlementType);
            }

            if (request.ShopId > 0)
            {
                condition.Append(" and location_id =@location_id");
                param.Add("@location_id", request.ShopId);
            }

            if (!string.IsNullOrWhiteSpace(request.StartDate))
            {
                condition.Append(" and create_time >=@StartTime");
                param.Add("@StartTime", request.StartDate);
            }

            if (!string.IsNullOrWhiteSpace(request.EndDate))
            {
                condition.Append(" and purchase_date <=@EndTime");
                param.Add("@EndTime", request.EndDate);
            }

            if (!string.IsNullOrWhiteSpace(request.PurchaseNo))
            {
                condition.Append(" and purchase_no =@PurchaseNo");
                param.Add("@PurchaseNo", request.PurchaseNo);
            }

            var list = await GetListPagedAsync(request.PageIndex, request.PageSize, condition.ToString(), "id desc", param);

            return list;


        }

        public async Task<bool> UpdatePurchaseAccountStatusForHaveSettlement(long locationId, string startDate, string endDate, string updateBy)
        {
            long id = 0;
            await OpenConnectionAsync(async conn =>
            {

                try
                {


                    string sql =
                        "update purchase_account_check set status=@Status, update_by=@UpdateBY,update_time=NOW(3)  where is_deleted=0 and status='已核对'and location_id=@ShopId and purchase_date>=@StartDate and purchase_date<=@EndDate";
                    var parameters = new DynamicParameters();
                    parameters.Add("@ShopId", locationId);
                    parameters.Add("@Status", "已结算");
                    parameters.Add("@UpdateBY", updateBy);
                    parameters.Add("@StartDate", startDate);
                    parameters.Add("@EndDate", endDate);
                    id = await conn.ExecuteAsync(sql, parameters);


                }
                catch (Exception e)
                {
                    id = 0;

                }

            });

            return id > 0;

        }
    }
}

