using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.FMS.Service.Core.Request;
using Ae.FMS.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.FMS.Service.Dal.Repositorys.Impl
{
    public class AccountCheckRepository : AbstractRepository<AccountCheckDO>, IAccountCheckRepository
    {
        public AccountCheckRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("FMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("FMSSqlReadOnly");
            
        }

        /// <summary>
        /// 对账异常处理 目前只是填写意见 对账数据到门店未对账
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<int> AccountCheckExceptionHandle(AccountCheckExceptionHandleRequest request)
        {
            var sql = @"update account_check_exception set suggestion=@suggestion,status=@status,update_by=@update_by,update_time=SYSDATE()
	            where id=@Id";

            var param = new DynamicParameters();
            param.Add("@suggestion",request.Suggestion);
            param.Add("@status", request.Status);

            param.Add("@update_by", request.UpdateBy);
            param.Add("@Id", request.Id);

            int result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        /// <summary>
        /// 创建对账记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<int> CreateAccountCheck(AccountCheckDO request)
        {
            return await InsertAsync(request);
        }
        public async Task<int> DeleteAccountCheck(ShopAccountCheckConfirmRequest request)
        {
            int id = 0;

            string sql = "Update account_check set is_deleted=1,update_by=@UpdateBy,update_time=NOW(3) where order_no = @OrderNo and is_deleted=0 ";
            var parameters = new DynamicParameters();
            parameters.Add("@UpdateBy", request.UpdateBy);
            parameters.Add("@OrderNo", request.OrderNo);
            await OpenSlaveConnectionAsync(async conn => { id = await conn.ExecuteAsync(sql, parameters); });
            return id;
        }

        /// <summary>
        /// 创建对账异常记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<int> CreateAccountCheckException(AccountCheckExceptionDO request)
        {
            return await InsertAsync<AccountCheckExceptionDO>(request);
        }

        /// <summary>
        /// 创建对账日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<int> CreateAccountCheckLog(AccountCheckLogDO request)
        {
            return await InsertAsync<AccountCheckLogDO>(request);
        }

        /// <summary>
        /// 根据门店编号查询对账数据
        /// </summary>
        /// <param name="locationIds"></param>
        /// <param name="requestType">1.已对账应结算-金额 2.门店已对账 3.总部已核对 4.门店已对账-金额 </param>
        /// <returns></returns>
        public async  Task<List<AccountCheckCollectDO>> GetAccountCheckCountByStatus(List<long> locationIds, int requestType)
        {
            var sql = @"";
            var param = new DynamicParameters();
            var locationIdStr = string.Join(',', locationIds).TrimEnd(',');
            //param.Add("@locationIds",string.Join(',',locationIds).TrimEnd(','));
            switch (requestType)
            {
                case 1:
                    sql = $@"select location_id ShopId,sum(shop_settlement_amount) AccountedSettlement 
                            from  account_check where is_deleted = 0 and  rg_check_result = '已核对' and shop_check_result = '已核对' 
                            and withdraw_status <> '已结算'	and location_id in ({locationIdStr}) GROUP BY location_id";
                    break;
                case 2:
                    sql = $@"select location_id ShopId,COUNT(id)  ShopAccountNum 
                            from 	account_check where is_deleted=0 and  shop_check_result='已核对' and (rg_check_result is null or rg_check_result='')	
                            and location_id in({locationIdStr}) GROUP BY location_id";
                    break;
                case 3:
                    sql = $@"select location_id ShopId,COUNT(id) RgAccountNum 
                            from 	account_check where is_deleted=0 and  shop_check_result='已核对' and rg_check_result='已核对' 
                            and withdraw_status <> '已结算'	and location_id in({locationIdStr}) GROUP BY location_id";
                    break;
                case 4:
                    sql = $@"select location_id ShopId,sum(shop_settlement_amount)  ShopAccountMoney 
                            from 	account_check where is_deleted=0 and  shop_check_result='已核对' and (rg_check_result is null or rg_check_result='')	
                            and location_id in({locationIdStr}) GROUP BY location_id";
                    break;
                default:
                    break;
            }

            IEnumerable<AccountCheckCollectDO> results = null;
            await OpenConnectionAsync(async conn => results = await conn.QueryAsync<AccountCheckCollectDO>(sql, param));
            return results.Any() ? results.ToList() : new List<AccountCheckCollectDO>();
        }

        public async Task<PagedEntity<AccountCheckExceptionCollectDO>> GetAccountCheckExceptionCollectList(GetAccountCheckRequest request)
        {
            var res = new PagedEntity<AccountCheckExceptionCollectDO>();
            var total = 0;

            var condition = new StringBuilder();
            var param = new DynamicParameters();
            var dateTime = new DateTime(2019, 10, 1);
            param.Add("@index", (request.PageIndex - 1) * request.PageSize);
            param.Add("@size", request.PageSize);

            if (!string.IsNullOrWhiteSpace(request.OrderNo))
            {
                condition.Append(" and ace.relation_no =@OrderNo");
                param.Add("@OrderNo", request.OrderNo);
            }

            if (request.LocationId > 0)
            {
                condition.Append(" and ace.location_id =@location_id");
                param.Add("@location_id", request.LocationId);
            }

            if (request.StartTime != null &&
                request.StartTime.Subtract(dateTime).TotalDays > 0)
            {
                condition.Append(" and ace.install_time >=@StartTime");
                param.Add("@StartTime", request.StartTime);
            }

            if (request.EndTime != null &&
                request.EndTime.Subtract(dateTime).TotalDays > 0)
            {
                condition.Append(" and ace.install_time <=@EndTime");
                param.Add("@EndTime", request.EndTime);
            }
            if (!string.IsNullOrWhiteSpace(request.Telephone)) {
                condition.Append(" and ace.telephone=@telephone ");
                param.Add("@telephone",request.Telephone);
            }
          

            var sqlCount = @"SELECT
	                    count(ace.id) FROM
	                     account_check_exception ace  where ace.is_deleted=0 and ace.status ='新建' " + condition.ToString();


            var sql = @"SELECT
	                    ace.id Id,
	                    order_no OrderNo,
	                    ace.location_id LocationId,
	                    ace.location_name LocationName,
	                    ac.account_type AccountType,
	                    ace.install_time InstallTime,
	                    ace.pay_method PayMethod,
	                    ace.money_arrive_status MoneyArriveStatus,
	                    ace.order_type OrderType,
	                    ac.sale_total_amount SaleTotalAmount,
	                    ac.total_cost TotalCost,
	                    ac.service_fee ServiceFee,
	                    ac.commission_fee CommissionFee,
	                    ac.other_fee OtherFee,
                        ac.settlement_fee SettlementFee,
	                    ac.withdraw_status WithdrawStatus,
	                    ace.relation_type RelationType,
	                    ace.report_type ReportType,
	                    ace.report_by ReportBy,
	                    ace.report_reason ReportReason,
	                    ace.report_time ReportTime,ace.relation_no RelationNo,ac.shopOutProductCost ShopOutProductCost
                    FROM
	                     account_check_exception ace LEFT JOIN  account_check ac ON ac.order_no = ace.relation_no 
                    WHERE
	                    ace.is_deleted=0  and ace.status='新建' " + condition.ToString() + " order by id desc LIMIT @index, @size";

            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
            });

            IEnumerable<AccountCheckExceptionCollectDO> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<AccountCheckExceptionCollectDO>(sql, param));
            res.TotalItems = total;
            res.Items = productDOs.ToList();
            return res;
        }

        /// <summary>
        /// 根据门店编号查询异常记录
        /// </summary>
        /// <param name="locationIds"></param>
        /// <returns></returns>
        public async  Task<List<AccountCheckCollectDO>> GetAccountCheckExceptionCount(List<long> locationIds)
        {
            var locationIdStr = string.Join(',', locationIds).TrimEnd(',');
            var sql = $@"SELECT
	                    location_id ShopId,
	                    count( id ) AccountErrorNum ,sum(settlement_fee) AccountExceptionMoney
                    FROM
	                    account_check_exception 
                    WHERE
	                    is_deleted=0 and  STATUS = '新建' 
	                    AND location_id IN ({locationIdStr}) 
                    GROUP BY
	                    location_id";

            var param = new DynamicParameters();
            
            //param.Add("@locationIds",string.Join(',', locationIds));
            IEnumerable<AccountCheckCollectDO> accounts = null;
            await OpenConnectionAsync(async conn => accounts = await conn.QueryAsync<AccountCheckCollectDO>(sql, param));
            return accounts.Any() ? accounts.ToList() : new List<AccountCheckCollectDO>();
        }

        public async Task<List<AccountCheckLogDO>> GetAccountCheckLogs(AccountCheckLogDO request)
        {

            var conditions = new StringBuilder();

            var param = new DynamicParameters();
            param.Add("@relation_no", request.RelationNo);
            //param.Add("@relation_type", request.RelationType);
            IEnumerable<AccountCheckLogDO> logs = null;
           

            if (!string.IsNullOrWhiteSpace(request.RelationType)) {
                param.Add("@relation_type", request.RelationType);
                conditions.Append(" and relation_type=@relation_type ");
            }
            var sql = @"select
                        id Id
                        ,relation_no RelationNo
                        ,relation_type RelationType
                        ,before_value BeforeValue
                        ,after_value AfterValue
                        ,remark Remark
                        ,status Status
                        ,create_by CreateBy
                        ,create_time CreateTime from account_check_log where relation_no =@relation_no " + conditions.ToString();
            await OpenSlaveConnectionAsync(async conn => logs = await conn.QueryAsync<AccountCheckLogDO>(sql, param));
            return logs.Any() ? logs.ToList() : new List<AccountCheckLogDO>();
        }


        public async Task<AccountCheckDO> GetAccountCheckDetail(GetAccountCheckDetailRequest request)
        {
            var conditions = new StringBuilder();
            var param = new DynamicParameters();

            if (request.Id > 0)
            {
                conditions.Append(" and id=@Id ");
                param.Add("@Id", request.Id);
            }

            if (!string.IsNullOrWhiteSpace(request.OrderNo))
            {
                conditions.Append(" and order_no=@order_no ");
                param.Add("@order_no", request.OrderNo);
            }
            if (string.IsNullOrWhiteSpace(conditions.ToString()))
            {
                return null;
            }
            var sql = @"select
                        id Id
                        , order_no OrderNo
                        ,location_id LocationId
                        , location_name LocationName
                        ,account_type AccountType
                        , install_time InstallTime
                        ,pay_method PayMethod
                        , money_arrive_status MoneyArriveStatus
                        ,order_type OrderType
                        , sale_total_amount SaleTotalAmount
                        ,total_cost TotalCost
                        , service_fee ServiceFee
                        ,commission_fee CommissionFee
                        , other_fee OtherFee
                        ,settlement_fee SettlementFee
                        , shop_check_result ShopCheckResult
                        ,shop_check_time ShopCheckTime
                        ,shop_check_suggestion ShopCheckSuggestion
                        ,rg_check_result RgCheckResult
                        ,rg_check_time RgCheckTime
                        ,rg_check_suggestion RgCheckSuggestion
                        ,channel Channel
                        ,telephone Telephone
                        , status Status
                        ,withdraw_status WithdrawStatus
                        , remark Remark, product_detail ProductDetail
                        ,create_by CreateBy,company_id CompanyId,company_name CompanyName,shop_install_amount ShopInstallAmount,actual_amount ActualAmount,
                        shop_distribution_cost ShopDistributionCost,shop_distribution_gross_profit ShopDistributionGrossProfit,shop_difference_price ShopDifferencePrice,
                        shop_commission_fee ShopCommissionFee,shop_settlement_amount ShopSettlementAmount,company_back_amount CompanyBackAmount,shop_other_fee ShopOtherFee,
                        shop_item_fee ShopItemFee,company_commission_fee CompanyCommissionFee,company_settlement_amount CompanySettlementAmount,company_other_fee CompanyOtherFee
                        , create_time CreateTime from account_check where is_deleted=0 " + conditions.ToString();
            AccountCheckDO accountCheck = null;

            await OpenConnectionAsync(async conn => accountCheck = await conn.QueryFirstOrDefaultAsync<AccountCheckDO>(sql, param));

            return accountCheck;
        }


        /// <summary>
        /// 总部已对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<PagedEntity<AccountCheckDO>> GetAccountChecks(GetAccountCheckRequest request)
        {
            var res = new PagedEntity<AccountCheckDO>();
            var total = 0;

            var condition = new StringBuilder();
            var param = new DynamicParameters();
            var dateTime = new DateTime(2019, 10, 1);
            param.Add("@index", (request.PageIndex - 1) * request.PageSize);
            param.Add("@size", request.PageSize);

            if (!string.IsNullOrWhiteSpace(request.OrderNo)) {
                condition.Append(" and order_no =@OrderNo");
                param.Add("@OrderNo",request.OrderNo);
            }

            if (request.LocationId > 0)
            {
                condition.Append(" and location_id =@location_id");
                param.Add("@location_id", request.LocationId);
            }

            if (request.StartTime != null &&
                request.StartTime.Subtract(dateTime).TotalDays > 0)
            {
                condition.Append(" and install_time >=@StartTime");
                param.Add("@StartTime", request.StartTime);
            }

            if (request.EndTime!=null &&
                request.EndTime.Subtract(dateTime).TotalDays > 0)
            {
                condition.Append(" and install_time <=@EndTime");
                param.Add("@EndTime", request.EndTime);
            }

            if (!string.IsNullOrWhiteSpace(request.RgCheckResult)) {
                condition.Append(" and rg_check_result =@RgCheckResult");
                param.Add("@RgCheckResult", request.RgCheckResult);
            }

            if (!string.IsNullOrWhiteSpace(request.ShopCheckResult))
            {
                condition.Append(" and shop_check_result =@ShopCheckResult");
                param.Add("@ShopCheckResult", request.ShopCheckResult);
            }

            if (!string.IsNullOrWhiteSpace(request.Telephone))
            {
                condition.Append(" and telephone=@telephone ");
                param.Add("@telephone", request.Telephone);
            }

            var sqlCount = @"SELECT
	                    count(id) FROM
	                    account_check where is_deleted=0 and withdraw_status <> '已结算'" + condition.ToString();


            var sql = @"select
                        id Id
                        , order_no OrderNo
                        ,location_id LocationId
                        , location_name LocationName
                        ,account_type AccountType
                        , install_time InstallTime
                        ,pay_method PayMethod
                        , money_arrive_status MoneyArriveStatus
                        ,order_type OrderType
                        , sale_total_amount SaleTotalAmount
                        ,total_cost TotalCost
                        , service_fee ServiceFee
                        ,commission_fee CommissionFee
                        , other_fee OtherFee
                        ,settlement_fee SettlementFee
                         , shop_check_result ShopCheckResult
                        ,shop_check_time ShopCheckTime
                        ,shop_check_suggestion ShopCheckSuggestion
                        ,rg_check_result RgCheckResult
                        ,rg_check_time RgCheckTime
                        ,rg_check_suggestion RgCheckSuggestion
                        ,channel Channel
                        ,telephone Telephone
                        , status Status
                        ,withdraw_status WithdrawStatus
                        , remark Remark, product_detail ProductDetail
                        ,create_by CreateBy,company_id CompanyId,company_name CompanyName,shop_install_amount ShopInstallAmount,actual_amount ActualAmount,
                        shop_distribution_cost ShopDistributionCost,shop_distribution_gross_profit ShopDistributionGrossProfit,shop_difference_price ShopDifferencePrice,
                        shop_commission_fee ShopCommissionFee,shop_settlement_amount ShopSettlementAmount,company_back_amount CompanyBackAmount,shop_other_fee ShopOtherFee,shopOutProductCost ShopOutProductCost,
                        shop_item_fee ShopItemFee,company_commission_fee CompanyCommissionFee,company_settlement_amount CompanySettlementAmount,company_other_fee CompanyOtherFee
                        , create_time CreateTime from account_check where is_deleted=0  and withdraw_status <> '已结算' " + condition.ToString() + " order by id desc LIMIT @index, @size";

            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
            });

            IEnumerable<AccountCheckDO> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<AccountCheckDO>(sql,param));
            res.TotalItems = total;
            res.Items = productDOs.ToList();
            return res;
        }

        public async  Task<PagedEntity<AccountCheckDO>> GetShopAccountChecks(GetAccountCheckRequest request)
        {
            var res = new PagedEntity<AccountCheckDO>();
            var total = 0;

            var condition = new StringBuilder();
            var param = new DynamicParameters();
            var dateTime = new DateTime(2019, 10, 1);
            param.Add("@index", (request.PageIndex - 1) * request.PageSize);
            param.Add("@size", request.PageSize);

            if (!string.IsNullOrWhiteSpace(request.OrderNo))
            {
                condition.Append(" and order_no =@OrderNo");
                param.Add("@OrderNo", request.OrderNo);
            }

            if (request.LocationId > 0)
            {
                condition.Append(" and location_id =@location_id");
                param.Add("@location_id", request.LocationId);
            }

            if (request.StartTime != null &&
                request.StartTime.Subtract(dateTime).TotalDays > 0)
            {
                condition.Append(" and install_time >=@StartTime");
                param.Add("@StartTime", request.StartTime);
            }

            if (request.EndTime != null &&
                request.EndTime.Subtract(dateTime).TotalDays > 0)
            {
                condition.Append(" and install_time <=@EndTime");
                param.Add("@EndTime", request.EndTime);
            }

            if (!string.IsNullOrWhiteSpace(request.ShopCheckResult))
            {
                condition.Append(" and shop_check_result =@ShopCheckResult");
                param.Add("@ShopCheckResult", request.ShopCheckResult);
            }

            if (!string.IsNullOrWhiteSpace(request.Telephone))
            {
                condition.Append(" and telephone=@telephone ");
                param.Add("@telephone", request.Telephone);
            }

            var sqlCount = @"SELECT
	                    count(id) FROM
	                    account_check where is_deleted=0 and (rg_check_result is null or rg_check_result='')" + condition.ToString();


            var sql = @"select
                        id Id
                        , order_no OrderNo
                        ,location_id LocationId
                        , location_name LocationName
                        ,account_type AccountType
                        , install_time InstallTime
                        ,pay_method PayMethod
                        , money_arrive_status MoneyArriveStatus
                        ,order_type OrderType
                        , sale_total_amount SaleTotalAmount
                        ,total_cost TotalCost
                        , service_fee ServiceFee
                        ,commission_fee CommissionFee
                        , other_fee OtherFee
                        ,settlement_fee SettlementFee
                         , shop_check_result ShopCheckResult
                        ,shop_check_time ShopCheckTime
                        ,shop_check_suggestion ShopCheckSuggestion
                        ,rg_check_result RgCheckResult
                        ,rg_check_time RgCheckTime
                        ,rg_check_suggestion RgCheckSuggestion
                        ,channel Channel
                        ,telephone Telephone
                        , status Status
                        ,withdraw_status WithdrawStatus
                        , remark Remark, product_detail ProductDetail
                        ,create_by CreateBy,company_id CompanyId,company_name CompanyName,shop_install_amount ShopInstallAmount,actual_amount ActualAmount,
                        shop_distribution_cost ShopDistributionCost,shop_distribution_gross_profit ShopDistributionGrossProfit,shop_difference_price ShopDifferencePrice,
                        shop_commission_fee ShopCommissionFee,shop_settlement_amount ShopSettlementAmount,company_back_amount CompanyBackAmount,shop_other_fee ShopOtherFee,shopOutProductCost ShopOutProductCost,
                        shop_item_fee ShopItemFee,company_commission_fee CompanyCommissionFee,company_settlement_amount CompanySettlementAmount,company_other_fee CompanyOtherFee
                        , create_time CreateTime from account_check where is_deleted=0 and (rg_check_result is null or rg_check_result='')" + condition.ToString() + " order by id desc LIMIT @index, @size";

            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
            });

            IEnumerable<AccountCheckDO> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<AccountCheckDO>(sql, param));
            res.TotalItems = total;
            res.Items = productDOs.ToList();
            return res;
        }

        /// <summary>
        /// 总部确认
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<int> RgAccountCheckConfirm(RgAccountCheckConfirmRequest request)
        {
            if (request.OrderNos.Any()) {

                string orderNos = string.Empty;
                foreach (var item in request.OrderNos)
                {
                    orderNos += "'" + item + "',";
                }
                var sql = $@"update account_check set withdraw_status=@withdraw_status, rg_check_result =@rg_check_result,rg_check_time =SYSDATE(),update_by=@update_by,update_time=SYSDATE()
                            where is_deleted=0 and order_no in ({orderNos.TrimEnd(',')}) and ( rg_check_result is null or rg_check_result='')";

                
                int result = -1;
                var param = new DynamicParameters();
                param.Add("@rg_check_result",request.RgCheckResult);
                param.Add("@withdraw_status", request.WithdrawStatus);
                param.Add("@update_by", request.UpdateBy);
                //param.Add("@orderNos", orderNos.TrimEnd(','));

                await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            }
            return 1;
        }

        /// <summary>
        /// 总部对账
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<int> RgAccountCheckException(RgAccountCheckConfirmRequest request)
        {
            if (request.OrderNos.Any())
            {

                string orderNos = string.Empty;
                foreach (var item in request.OrderNos)
                {
                    orderNos += "'" + item + "',";
                }
                var sql = $@"update account_check set rg_check_result =@rg_check_result,rg_check_time =SYSDATE(),update_by=@update_by,update_time=SYSDATE()
                            where is_deleted=0 and order_no in ({orderNos.TrimEnd(',')}) and ( rg_check_result is null or rg_check_result='')";


                int result = -1;
                var param = new DynamicParameters();
                param.Add("@rg_check_result", request.RgCheckResult);
               // param.Add("@orderNos", orderNos.TrimEnd(','));
                param.Add("@update_by", request.UpdateBy);
              
                await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            }
            return 1;
        }

        public async Task<int> RgAccountCheckWithdraw(RgAccountCheckWithdrawRequeset request)
        {
            var orderNos = string.Empty;
            if (request.OrderNos.Any())
            {
                var param = new DynamicParameters();
                foreach (var item in request.OrderNos)
                {
                    orderNos += "'" + item + "',";
                }
                param.Add("@orderNos", orderNos);
                param.Add("@update_by", request.UpdateBy);
                param.Add("@withdraw_status", request.WithdrawStatus);
                var sql = $@"UPDATE account_check 
                        SET withdraw_status = @withdraw_status,
                        update_by = @update_by,
                        update_time = SYSDATE( ) 
                        WHERE is_deleted=0 and 
	                        order_no IN ({orderNos.TrimEnd(',')}) and withdraw_status='未申请'";

                var result = -1;

                await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
                return result;
            }
            return -1;
        }

        public async Task<int> ShopAccountCheckException(ShopAccountCheckConfirmRequest request)
        {
            var sql = @"update
	                     account_check set shop_check_result=@shop_check_result,shop_check_time=SYSDATE(),update_by=@update_by,update_time=SYSDATE()
	                     where is_deleted=0 and order_no=@order_no";
            var param = new DynamicParameters();
            param.Add("@shop_check_result", request.ShopCheckResult);

            param.Add("@update_by", request.UpdateBy);
            param.Add("@order_no", request.OrderNo);

            int result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        /// <summary>
        /// 更新对账信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  Task<int> UpdateAccountCheckInfo(UpdateAccountCheckRequest request)
        {
            var sql = @"UPDATE account_check 
                        SET location_id = @location_id,
                        location_name = @location_name,
                        account_type = @account_type,
                        install_time = @install_time,
                        pay_method = @pay_method,
                        money_arrive_status = @money_arrive_status,
                        order_type = @order_type,
                        sale_total_amount = @sale_total_amount,
                        total_cost = @total_cost,
                        service_fee = @service_fee,
                        commission_fee = @commission_fee,
                        other_fee = @other_fee,
                        settlement_fee = @settlement_fee,
                        shop_check_result = @shop_check_result,
                        shop_check_time = SYSDATE( ),
						shop_check_suggestion =@shop_check_suggestion,
						rg_check_result=@rg_check_result,												
						rg_check_suggestion=@rg_check_suggestion,												
                        remark = @remark,
						channel=@channel,
						telephone=@telephone,
                        company_id=@CompanyId,
                        company_name=@CompanyName,shop_install_amount=@ShopInstallAmount,actual_amount=@ActualAmount,shop_distribution_cost=@ShopDistributionCost,
                        shop_distribution_gross_profit=@ShopDistributionGrossProfit,shop_difference_price=@ShopDifferencePrice,shop_commission_fee=@ShopCommissionFee,
                        shop_settlement_amount=@ShopSettlementAmount,company_back_amount=@CompanyBackAmount,shop_other_fee=@ShopOtherFee,shop_item_fee=@ShopItemFee,
                        company_commission_fee=@CompanyCommissionFee,company_settlement_amount=@CompanySettlementAmount,company_other_fee=@CompanyOtherFee,
                        update_by =@update_by, update_time =  SYSDATE( ) 
                        WHERE is_deleted=0 and 
	                        order_no = @order_no";
            var param = new DynamicParameters();
            param.Add("@location_id",request.LocationId);
            param.Add("@location_name", request.LocationName);
            param.Add("@account_type", request.AccountType);
            param.Add("@install_time", request.InstallTime);
            param.Add("@pay_method", request.PayMethod);
            param.Add("@money_arrive_status", request.MoneyArriveStatus);
            param.Add("@order_type", request.OrderType);
            param.Add("@sale_total_amount", request.SaleTotalAmount);
            param.Add("@total_cost", request.TotalCost);
            param.Add("@service_fee", request.ServiceFee);

            param.Add("@commission_fee", request.CommissionFee);
            param.Add("@other_fee", request.OtherFee);
            param.Add("@settlement_fee", request.SettlementFee);
            param.Add("@shop_check_result", request.ShopCheckResult);


            param.Add("@shop_check_suggestion", request.ShopCheckSuggestion);
            param.Add("@rg_check_result", request.RgCheckResult);
            param.Add("@rg_check_suggestion", request.RgCheckSuggestion);

            param.Add("@channel", request.Channel);
            param.Add("@telephone", request.Telephone);

            param.Add("@remark", request.Remark);
            param.Add("@update_by", request.UpdateBy);
            param.Add("@order_no", request.OrderNo);

            param.Add("@CompanyId", request.CompanyId);
            param.Add("@CompanyName", request.CompanyName);
            param.Add("@ShopInstallAmount", request.ShopInstallAmount);
            param.Add("@ActualAmount", request.ActualAmount);
            param.Add("@ShopDistributionCost", request.ShopDistributionCost);
            param.Add("@ShopDistributionGrossProfit", request.ShopDistributionGrossProfit);
            param.Add("@ShopDifferencePrice", request.ShopDifferencePrice);
            param.Add("@ShopCommissionFee", request.ShopCommissionFee);
            param.Add("@ShopSettlementAmount", request.ShopSettlementAmount);

            param.Add("@CompanyBackAmount", request.CompanyBackAmount);
            param.Add("@ShopOtherFee", request.ShopOtherFee);
            param.Add("@ShopItemFee", request.ShopItemFee);

            param.Add("@CompanyCommissionFee", request.CompanyCommissionFee);
            param.Add("@CompanySettlementAmount", request.CompanySettlementAmount);
            param.Add("@CompanyOtherFee", request.CompanyOtherFee);

            var result = -1;
            try
            {
                await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            }
            catch (Exception ex)
            {

                throw;
            }
           
            return result;
        }

        public async  Task<int> RgAccountCheckSettlement(RgAccountCheckWithdrawRequeset request)
        {
            var orderNos = string.Empty;
            if (request.OrderNos.Any())
            {
                var param = new DynamicParameters();
                foreach (var item in request.OrderNos)
                {
                    orderNos += "'" + item + "',";
                }
                param.Add("@orderNos", orderNos);
                param.Add("@update_by", request.UpdateBy);
                param.Add("@withdraw_status", request.WithdrawStatus);
                var sql = $@"UPDATE account_check 
                        SET withdraw_status = @withdraw_status,
                        update_by = @update_by,
                        update_time = SYSDATE( ) 
                        WHERE is_deleted=0 and 
	                        order_no IN ({orderNos.TrimEnd(',')}) and withdraw_status='申请成功'";

                var result = -1;

                await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
                return result;
            }
            return -1;
        }

        public async Task<string> BatchHandelAccountCheckData(int hour)
        {
            var sql = @" insert into account_check_log(relation_no,relation_type,location_id,before_value,after_value,remark,create_by,create_time)
                    select order_no,'门店',location_id,'','','系统自动更新对账单为【申请成功】','总部系统Job',SYSDATE() from account_check
                    WHERE
	                    withdraw_status = '已申请' 
	                    AND rg_check_result = '已核对' 
	                    AND TIMESTAMPDIFF( HOUR, update_time, SYSDATE( ) ) >= @diffHour;
	
                    UPDATE account_check 
                    SET withdraw_status = '申请成功',
                    update_by = '总部系统Job',
                    update_time = SYSDATE( )
                    WHERE is_deleted=0 and 
	                    withdraw_status = '已申请' 
	                    AND rg_check_result = '已核对' 
	                    AND TIMESTAMPDIFF( HOUR, update_time, SYSDATE( ) ) >= @diffHour;";

            var result = -1;

            await OpenConnectionAsync(async conn =>
            {
                var tran = conn.BeginTransaction();
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@diffHour", hour);
                    result = await conn.ExecuteAsync(sql, param);
                    tran.Commit();
                }
                catch (Exception e)
                {
                    tran.Rollback();
                }
                finally
                {

                }
            });
            return result > 0 ? "更新成功" : "暂无数据";
        }

        public async  Task<PagedEntity<AccountCheckExceptionCollectDO>> GetAccountCheckExceptionMonitorList(GetAccountCheckRequest request)
        {
            var res = new PagedEntity<AccountCheckExceptionCollectDO>();
            var total = 0;

            var condition = new StringBuilder();
            var param = new DynamicParameters();
            var dateTime = new DateTime(2019, 10, 1);
            param.Add("@index", (request.PageIndex - 1) * request.PageSize);
            param.Add("@size", request.PageSize);

            if (!string.IsNullOrWhiteSpace(request.OrderNo))
            {
                condition.Append(" and ace.relation_no =@OrderNo");
                param.Add("@OrderNo", request.OrderNo);
            }

            if (!string.IsNullOrWhiteSpace(request.LocationIds))
            {
                condition.Append($" and ace.location_id in ({request.LocationIds})");
                //param.Add("@location_id", request.LocationId);
            }

            if (request.StartTime != null &&
                request.StartTime.Subtract(dateTime).TotalDays > 0)
            {
                condition.Append(" and ace.install_time >=@StartTime");
                param.Add("@StartTime", request.StartTime);
            }

            if (request.EndTime != null &&
                request.EndTime.Subtract(dateTime).TotalDays > 0)
            {
                condition.Append(" and ace.install_time <=@EndTime");
                param.Add("@EndTime", request.EndTime);
            }
            if (!string.IsNullOrWhiteSpace(request.Telephone))
            {
                condition.Append(" and ace.telephone=@telephone ");
                param.Add("@telephone", request.Telephone);
            }


            var sqlCount = @"SELECT
	                    count(ace.id) FROM
	                     account_check_exception ace  where 1=1 and ace.status ='新建' " + condition.ToString();


            var sql = @"SELECT
	                    ace.id Id,
	                    order_no OrderNo,
	                    ace.location_id LocationId,
	                    ace.location_name LocationName,
	                    ac.account_type AccountType,
	                    ace.install_time InstallTime,
	                    ace.pay_method PayMethod,
	                    ace.money_arrive_status MoneyArriveStatus,
	                    ace.order_type OrderType,
	                    ac.sale_total_amount SaleTotalAmount,
	                    ac.total_cost TotalCost,
	                    ac.service_fee ServiceFee,
	                    ac.commission_fee CommissionFee,
	                    ac.other_fee OtherFee,
                        ac.settlement_fee SettlementFee,
	                    ac.withdraw_status WithdrawStatus,
	                    ace.relation_type RelationType,
	                    ace.report_type ReportType,
	                    ace.report_by ReportBy,
	                    ace.report_reason ReportReason,
	                    ace.report_time ReportTime,ace.relation_no RelationNo 
                    FROM
	                     account_check_exception ace LEFT JOIN  account_check ac ON ac.order_no = ace.relation_no 
                    WHERE
	                    1 = 1  and ace.status='新建' " + condition.ToString() + " order by id desc LIMIT @index, @size";

            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
            });

            IEnumerable<AccountCheckExceptionCollectDO> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<AccountCheckExceptionCollectDO>(sql, param));
            res.TotalItems = total;
            res.Items = productDOs.ToList();
            return res;

        }

        public async Task<PagedEntity<AccountCheckDO>> GetShopAccountChecksList(GetAccountCheckRequest request)
        {
            var res = new PagedEntity<AccountCheckDO>();
            var total = 0;

            var condition = new StringBuilder();
            var param = new DynamicParameters();
            var dateTime = new DateTime(2019, 10, 1);
            param.Add("@index", (request.PageIndex - 1) * request.PageSize);
            param.Add("@size", request.PageSize);

            if (!string.IsNullOrWhiteSpace(request.OrderNo))
            {
                condition.Append(" and order_no =@OrderNo");
                param.Add("@OrderNo", request.OrderNo);
            }

            if (request.LocationId > 0)
            {
                condition.Append(" and location_id =@location_id");
                param.Add("@location_id", request.LocationId);
            }

            if (request.StartTime != null &&
                request.StartTime.Subtract(dateTime).TotalDays > 0)
            {
                condition.Append(" and install_time >=@StartTime");
                param.Add("@StartTime", request.StartTime);
            }

            if (request.EndTime != null &&
                request.EndTime.Subtract(dateTime).TotalDays > 0)
            {
                condition.Append(" and install_time <=@EndTime");
                param.Add("@EndTime", request.EndTime);
            }

            if (!string.IsNullOrWhiteSpace(request.ShopCheckResult))
            {
                condition.Append(" and shop_check_result =@ShopCheckResult");
                param.Add("@ShopCheckResult", request.ShopCheckResult);
            }

            if (!string.IsNullOrWhiteSpace(request.Telephone))
            {
                condition.Append(" and telephone=@telephone ");
                param.Add("@telephone", request.Telephone);
            }

            //var sqlCount = @"SELECT
            //         count(id) FROM
            //         account_check where 1=1 and (rg_check_result is null or rg_check_result='')" + condition.ToString();

            var sqlCount = @"SELECT
	                    count(id) FROM
	                    account_check where is_deleted=0 " + condition.ToString();


            var sql = @"select
                        id Id
                        , order_no OrderNo
                        ,location_id LocationId
                        , location_name LocationName
                        ,account_type AccountType
                        , install_time InstallTime
                        ,pay_method PayMethod
                        , money_arrive_status MoneyArriveStatus
                        ,order_type OrderType
                        , sale_total_amount SaleTotalAmount
                        ,total_cost TotalCost
                        , service_fee ServiceFee
                        ,commission_fee CommissionFee
                        , other_fee OtherFee
                        ,settlement_fee SettlementFee
                         , shop_check_result ShopCheckResult
                        ,shop_check_time ShopCheckTime
                        ,shop_check_suggestion ShopCheckSuggestion
                        ,rg_check_result RgCheckResult
                        ,rg_check_time RgCheckTime
                        ,rg_check_suggestion RgCheckSuggestion
                        ,channel Channel
                        ,telephone Telephone
                        , status Status
                        ,withdraw_status WithdrawStatus
                        , remark Remark, product_detail ProductDetail
                        ,create_by CreateBy,company_id CompanyId,company_name CompanyName,shop_install_amount ShopInstallAmount,actual_amount ActualAmount,
                        shop_distribution_cost ShopDistributionCost,shop_distribution_gross_profit ShopDistributionGrossProfit,shop_difference_price ShopDifferencePrice,
                        shop_commission_fee ShopCommissionFee,shop_settlement_amount ShopSettlementAmount,company_back_amount CompanyBackAmount,shop_other_fee ShopOtherFee,shopOutProductCost ShopOutProductCost,
                        shop_item_fee ShopItemFee,company_commission_fee CompanyCommissionFee,company_settlement_amount CompanySettlementAmount,company_other_fee CompanyOtherFee
                        , create_time CreateTime from account_check where is_deleted=0 " + condition.ToString() + " order by id desc LIMIT @index, @size";

            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
            });

            IEnumerable<AccountCheckDO> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<AccountCheckDO>(sql, param));
            res.TotalItems = total;
            res.Items = productDOs.ToList();
            return res;
        }
    }
}





