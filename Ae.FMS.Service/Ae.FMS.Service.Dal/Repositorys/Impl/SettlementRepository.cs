using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.FMS.Service.Common;
using Ae.FMS.Service.Core.Enums;
using Ae.FMS.Service.Core.Request.Settlement;
using Ae.FMS.Service.Core.Response.Settlement;
using Ae.FMS.Service.Dal.Model;
using Ae.FMS.Service.Dal.Model.settlement;

namespace Ae.FMS.Service.Dal.Repositorys.Impl
{
    /// <summary>
    /// 结算仓储
    /// </summary>
    public class SettlementRepository : AbstractRepository<SettlementBatchDO>, ISettlementRepository
    {
        public SettlementRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("FMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("FMSSqlReadOnly");
        }

        /// <summary>
        /// 得到门店结算账户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AccountSettlementDO> GetAccountSettlement(GetAccountInfoRequest request)
        {
            if (request.ShopId <= 0)
                return null;
            else
            {
                AccountSettlementDO accountSettlementDo = new AccountSettlementDO();
                decimal accountBalanceAmount = 0;
                decimal withdrawingAmount = 0;
                decimal haveWithdrawalAmount = 0;
                var taskAmount = new Task[]
                {
                    Task.Factory.StartNew(async () => {accountBalanceAmount = await GetAccountBalanceAmount(request.ShopId); }),
                    Task.Factory.StartNew(function: async () => { withdrawingAmount = await GetWithdrawingAmount(request.ShopId); }),
                    Task.Factory.StartNew(async () => haveWithdrawalAmount = await GetHaveWithdrawalAmount(request.ShopId))
                };
                await Task.WhenAll(taskAmount).ContinueWith(_ =>
                 {
                     accountSettlementDo.AccountBalanceAmount = accountBalanceAmount;
                     accountSettlementDo.WithdrawalingAmount = withdrawingAmount;
                     accountSettlementDo.HaveWithdrawalAmount = haveWithdrawalAmount;
                 });

                return accountSettlementDo;
            }
        }

        /// <summary>
        /// 待提现的金额
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<decimal> GetAccountBalanceAmount(int shopId)
        {
            if (shopId <= 0)
                return await Task.FromResult(0);
            else
            {
                var sql =
                    "select sum(shop_settlement_amount) AS AccountBalanceAmount from account_check " +
                    "where location_id=@ShopId and withdraw_status=@WithDrawStatus";

                var parameters = new DynamicParameters();

                parameters.Add("@ShopId", shopId);
                parameters.Add("@WithDrawStatus", "申请成功");
                decimal accountBalanceAmount = 0;
                await OpenSlaveConnectionAsync(async conn =>
                {
                    decimal? value = 0;
                    value = await conn.QueryFirstOrDefaultAsync<decimal?>(sql, parameters);

                    if (value.HasValue)
                    {
                        accountBalanceAmount = value.Value;
                    }
                });
                return accountBalanceAmount;
            }
        }

        /// <summary>
        /// 提现中的金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<decimal> GetWithdrawingAmount(int shopId)
        {
            if (shopId <= 0)
                return await Task.FromResult(0);
            else
            {
                var sql =
                    "select sum(bill_amount) AS WithdrawalingAmount " +
                    "from settlement_batch where location_id=@ShopId and status IN @WithDrawalingStatus and is_deleted=0";

                var parameters = new DynamicParameters();

                parameters.Add("@ShopId", shopId);
                parameters.Add("@WithDrawalingStatus", new[] { SettlementBatchStatusEnum.Apply.ToInt(), SettlementBatchStatusEnum.PayFailure.ToInt() });
                decimal withdrawalingAmount = 0;
                await OpenSlaveConnectionAsync(async conn =>
                {
                    decimal? value = 0;
                    value = await conn.QueryFirstOrDefaultAsync<decimal?>(sql, parameters);

                    if (value.HasValue)
                    {
                        withdrawalingAmount = value.Value;
                    }
                });
                return withdrawalingAmount;
            }
        }

        /// <summary>
        /// 提现中的金额
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        private async Task<decimal> GetHaveWithdrawalAmount(int shopId)
        {
            if (shopId <= 0)
                return await Task.FromResult(0);
            else
            {
                var sql =
                    "select sum(bill_amount) AS HaveWithdrawalAmount " +
                    "from settlement_batch where location_id=@ShopId and status =@HaveWithDrawalStatus and is_deleted=0";

                var parameters = new DynamicParameters();

                parameters.Add("@ShopId", shopId);
                parameters.Add("@HaveWithDrawalStatus", SettlementBatchStatusEnum.HavePay.ToInt());
                decimal haveWithdrawalAmount = 0;
                await OpenSlaveConnectionAsync(async conn =>
                {
                    decimal? value = 0;
                    value = await conn.QueryFirstOrDefaultAsync<decimal?>(sql, parameters);

                    if (value.HasValue)
                    {
                        haveWithdrawalAmount = value.Value;
                    }
                });
                return haveWithdrawalAmount;
            }
        }


        /// <summary>
        /// 得到提现记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<SettlementBatchDO>> GetWithdrawalRecordList(GetWithdrawalRecordListRequest request)
        {
            if (request.ShopId <= 0)
                return null;
            else
            {
                var builder = new StringBuilder();
                var parameters = new DynamicParameters();
                builder.AppendLine(" where  location_id=@ShopId");
                parameters.Add("@ShopId", request.ShopId);

                if (request.Status != SettlementBatchStatusEnum.None)
                {
                    builder.AppendLine(" and status=@Status");
                    parameters.Add("@Status", request.Status.ToInt());
                }
             
                if (!string.IsNullOrEmpty(request.SettlementBatchNo))
                {
                    builder.AppendLine(" and settlement_batch_no=@BatchNo");
                    parameters.Add("@BatchNo", request.SettlementBatchNo.Trim());
                }
                if (!string.IsNullOrEmpty(request.ApplyStartTime))
                {
                    bool isSuccess = DateTime.TryParse(request.ApplyStartTime, out var startTime);
                    if (isSuccess)
                    {
                        builder.AppendLine(" and apply_time>=@StartTime");
                        parameters.Add("@StartTime", startTime);
                    }
                }
                if (!string.IsNullOrEmpty(request.ApplyEndTime))
                {
                    bool isSuccess = DateTime.TryParse(request.ApplyEndTime, out var endTime);
                    if (isSuccess)
                    {
                        builder.AppendLine(" and apply_time<@EndTime");
                        parameters.Add("@EndTime", endTime);
                    }
                }
                return await GetListPagedAsync<SettlementBatchDO>(request.PageIndex, request.PageSize, builder.ToString(), "apply_time desc",
                    parameters, false);
            }
        }

        /// <summary>
        /// 得到订单提现记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<SettlementBatchDetailDO>> GetWithdrawalOrderRecordList(GetWithdrawalOrderRecordListRequest request)
        {
            if (request.ShopId <= 0)
                return null;
            else
            {
                var builder = new StringBuilder();
                var parameters = new DynamicParameters();
                builder.AppendLine(" where  location_id=@ShopId");
                parameters.Add("@ShopId", request.ShopId);
                builder.AppendLine(" and settlement_batch_no=@BatchNo");
                parameters.Add("@BatchNo", request.BatchNo.Trim());
                return await GetListPagedAsync<SettlementBatchDetailDO>(request.PageIndex, request.PageSize, builder.ToString(), "order_no desc",
                    parameters, false);
            }
        }

        /// <summary>
        /// 得到财务账单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<GetFianceReconciliationStatusListDO>> GetFianceReconciliationStatusList(
            GetFianceReconciliationStatusListRequest request)
        {
            if (request.ShopId <= 0)
                return null;
            else
            {
                PagedEntity<GetFianceReconciliationStatusListDO> response = new PagedEntity<GetFianceReconciliationStatusListDO>();
                var builder = new StringBuilder();
                var parameters = new DynamicParameters();


                builder.AppendLine(" where A.is_deleted=0");

                builder.AppendLine(" and A.location_id=@ShopId");
                parameters.Add("@ShopId", request.ShopId);

                if (!string.IsNullOrEmpty(request.SearchType))
                {
                    if (request.SearchType == GetOrdersTypeEnum.OrderNo.ToString())
                    {
                        builder.AppendLine(" and A.order_no=@OrderNO");
                        parameters.Add("@OrderNO", request.Content.Trim());
                    }
                    if (request.SearchType == GetOrdersTypeEnum.Telephone.ToString())
                    {
                        builder.AppendLine(" and A.telephone=@Telephone");
                        parameters.Add("@Telephone", request.Content.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(request.StartOrderTime))
                {
                    bool isSuccess = DateTime.TryParse(request.StartOrderTime, out var startOrderTime);
                    if (isSuccess)
                    {
                        builder.AppendLine(" and A.create_time>=@CreateTime");
                        parameters.Add("@CreateTime", startOrderTime);
                    }
                }

                if (!string.IsNullOrEmpty(request.EndOrderTime))
                {
                    bool isSuccess = DateTime.TryParse(request.EndOrderTime, out var endDateTime);
                    if (isSuccess)
                    {
                        builder.AppendLine(" and A.create_time<@EndTime");
                        parameters.Add("@EndTime", endDateTime);
                    }
                }

                if (!string.IsNullOrEmpty(request.StartInstalledTime))
                {
                    bool isSuccess = DateTime.TryParse(request.StartInstalledTime, out var startInstalledTime);
                    if (isSuccess)
                    {
                        builder.AppendLine(" and A.install_time>=@StartInstallTime");
                        parameters.Add("@StartInstallTime", startInstalledTime);
                    }
                }

                if (!string.IsNullOrEmpty(request.EndInstalledTime))
                {
                    bool isSuccess = DateTime.TryParse(request.EndInstalledTime, out var endInstalledTime);
                    if (isSuccess)
                    {
                        builder.AppendLine(" and A.install_time<@EndInstallTime");
                        parameters.Add("@EndInstallTime", endInstalledTime);
                    }
                }

                if (request.Status == FinanceBillStatus.HaveReconciliation)
                {

                    builder.AppendLine(" and A.shop_check_result=@ShopCheckResult");
                    parameters.Add("@ShopCheckResult", "已核对");

                    builder.AppendLine(" and A.rg_check_result!=@RgCheckResult");
                    parameters.Add("@RgCheckResult", "核对异常");

                    //builder.AppendLine(" and A.withdraw_status!=@WithDrawStatus");
                    //parameters.Add("@WithDrawStatus", "申请成功");

                    var sqlCount = @"select Count(1) FROM (
                                        select * from account_check A " + builder.ToString() + @"
                                       ) t";

                    var total = 0;
                    await OpenSlaveConnectionAsync(async conn =>
                    {
                        total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, parameters);
                    });

                    var sql =
                        @"select A.order_no As OrderNo,A.install_time As InstallTime,A.actual_amount AS SaleTotalAmount,A.shop_install_amount AS ServiceFee,A.shop_settlement_amount AS SettlementFee
                         from account_check A " + builder.ToString() + " order by A.install_time desc limit " + (request.PageIndex - 1) * request.PageSize + "," + request.PageSize + "";

                    IEnumerable<GetFianceReconciliationStatusListDO> orderDos = null;
                    await OpenConnectionAsync(async conn => orderDos = await conn.QueryAsync<GetFianceReconciliationStatusListDO>(sql, parameters));

                    response.TotalItems = total;
                    response.Items = orderDos.ToList();



                }

                if (request.Status == FinanceBillStatus.WaitingWithdrawal)
                {
                    builder.AppendLine(" and A.withdraw_status=@WithdrawStatus");
                    parameters.Add("@WithdrawStatus", "申请成功");

                    var sqlCount = @"select Count(1) FROM ( select * from account_check A " + builder.ToString() + ") t";

                    var total = 0;
                    await OpenSlaveConnectionAsync(async conn =>
                    {
                        total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, parameters);
                    });

                    var sql =
                        @"select A.order_no As OrderNo,A.install_time As InstallTime,A.sale_total_amount AS SaleTotalAmount,A.shop_install_amount AS ServiceFee,A.shop_settlement_amount AS SettlementFee
                         from account_check A " + builder.ToString() + " order by A.install_time desc limit " + (request.PageIndex - 1) * request.PageSize + "," + request.PageSize + "";

                    IEnumerable<GetFianceReconciliationStatusListDO> orderDos = null;
                    await OpenConnectionAsync(async conn => orderDos = await conn.QueryAsync<GetFianceReconciliationStatusListDO>(sql, parameters));

                    response.TotalItems = total;
                    response.Items = orderDos.ToList();
                }

                if (request.Status == FinanceBillStatus.Exception)
                {
                    builder.AppendLine(" and B.status=@ExcptionStatus");
                    parameters.Add("@ExcptionStatus", "新建");

                    var sqlCount = @"select Count(1) FROM (
                                        select A.id from account_check A 
                                        INNER JOIN account_check_exception B
                                        ON A.order_no=B.relation_no" + builder.ToString() + @"
                                       ) t";

                    var total = 0;
                    await OpenSlaveConnectionAsync(async conn =>
                    {
                        total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, parameters);
                    });

                    var sql =
                        @"select A.order_no As OrderNo,A.install_time As InstallTime,A.sale_total_amount AS SaleTotalAmount,A.shop_install_amount AS ServiceFee,A.shop_settlement_amount AS SettlementFee
                    from account_check A INNER JOIN account_check_exception B  ON A.order_no=B.relation_no" +
                        builder.ToString() + " order by A.install_time desc limit " + (request.PageIndex - 1) * request.PageSize + "," + request.PageSize + "";

                    IEnumerable<GetFianceReconciliationStatusListDO> orderDos = null;
                    await OpenConnectionAsync(async conn => orderDos = await conn.QueryAsync<GetFianceReconciliationStatusListDO>(sql, parameters));

                    response.TotalItems = total;
                    response.Items = orderDos.ToList();
                }
                return response;
            }
        }

        /// <summary>
        /// 得到财务账单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AccountCheckDO> GetFianceBillDetail(GetFianceBillDetailRequest request)
        {
            if (request.ShopId <= 0)
                return null;
            else
            {
                var builder = new StringBuilder();
                var parameters = new DynamicParameters();

                builder.AppendLine(" where  location_id=@ShopId");
                parameters.Add("@ShopId", request.ShopId);

                builder.AppendLine(" and order_no=@OrderNO");
                parameters.Add("@OrderNO", request.OrderNo.Trim());


                var getAccountListDo = await GetListAsync<AccountCheckDO>(builder.ToString(), parameters, false);

                return getAccountListDo?.FirstOrDefault();
            }
        }

        /// <summary>
        /// 发起提现申请
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> SubmitWithdrawalApply(SettlementBatchDO settlementBatchDo, List<SettlementBatchDetailDO> settlementBatchDetailDoS)
        {
            long id = 0;
            await OpenConnectionAsync(async conn =>
            {

                // var tran = conn.BeginTransaction();
                try
                {
                    var orderNos = settlementBatchDetailDoS?.Select(x => x.OrderNo);
                    var builder = new StringBuilder();
                    var parameters = new DynamicParameters();
                    builder.AppendLine("WHERE order_no IN @OrderNo");
                    parameters.Add("@OrderNo", orderNos);

                    var getBatchDetailDos =
                          (await GetListAsync<SettlementBatchDetailDO>(builder.ToString(), parameters, true));
                    if (getBatchDetailDos == null || !getBatchDetailDos.Any())
                    {
                        var settlementBatchId = await InsertAsync<long, SettlementBatchDO>(settlementBatchDo);
                        settlementBatchDo.Id = settlementBatchId;
                        settlementBatchDo.SettlementBatchNo = $"BN{settlementBatchId.ToString("D8")}";
                        id = await UpdateAsync(settlementBatchDo, new[] { "SettlementBatchNo" });

                        settlementBatchDetailDoS?.ForEach(x =>
                                    {
                                        x.SettlementBatchId = settlementBatchId;
                                        x.SettlementBatchNo = $"BN{settlementBatchId.ToString("D8")}";
                                    });
                        await InsertBatchAsync(settlementBatchDetailDoS);
                    }


                    // tran.Commit();
                }
                catch (Exception e)
                {
                    id = 0;
                    //  tran.Rollback();
                }
                finally
                {

                }
            });
            return id;
        }

        /// <summary>
        /// 得到可提现的列表
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<AccountCheckDO>> GetAccountWithdrawalList(int shopId)
        {
            if (shopId <= 0)
                return null;
            else
            {
                var builder = new StringBuilder();
                var parameters = new DynamicParameters();
                builder.AppendLine(" where withdraw_status=@WithdrawalStatus");
                parameters.Add("@WithdrawalStatus", "申请成功");

                builder.AppendLine(" and location_id=@ShopId");
                parameters.Add("@ShopId", shopId);

                return (await GetListAsync<AccountCheckDO>(builder.ToString(), parameters, true))?.ToList();

            }
        }

        /// <summary>
        /// 对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<SettlementBatchDO>> GetSettlementList(GetSettlementListRequest request)
        {
            var res = new PagedEntity<SettlementBatchDO>();
            var total = 0;

            var param = new DynamicParameters();
            var dateTime = new DateTime(2019, 10, 1);
            param.Add("@index", (request.PageIndex - 1) * request.PageSize);
            param.Add("@size", request.PageSize);

            var builder = new StringBuilder();

            if (request.ShopId > 0) {
                builder.Append(" and location_id =@location_id ");
                param.Add("@location_id", request.ShopId);
            }

            if (!string.IsNullOrWhiteSpace(request.ProvinceId)) {
                builder.Append(" and province_id =@province_id ");
                param.Add("@province_id", request.ProvinceId);

            }

            if (!string.IsNullOrWhiteSpace(request.CityId))
            {
                builder.Append(" and city_id =@city_id ");
                param.Add("@city_id", request.CityId);

            }

            if (!string.IsNullOrWhiteSpace(request.DistrictId))
            {
                builder.Append(" and district_id =@district_id ");
                param.Add("@district_id", request.DistrictId);

            }

            if (!string.IsNullOrEmpty(request.StartSettlementTime))
            {
                bool isSuccess = DateTime.TryParse(request.StartSettlementTime, out var StartSettlementTime);
                if (isSuccess)
                {
                    builder.AppendLine(" and create_time>=@StartSettlementTime");
                    param.Add("@StartSettlementTime", StartSettlementTime);
                }
            }

            if (!string.IsNullOrEmpty(request.EndSettlementTime))
            {
                bool isSuccess = DateTime.TryParse(request.EndSettlementTime, out var EndSettlementTime);
                if (isSuccess)
                {
                    builder.AppendLine(" and create_time<@EndSettlementTime");
                    param.Add("@EndSettlementTime", EndSettlementTime);
                }
            }

            if (!string.IsNullOrEmpty(request.CheckUser))
            {
                builder.AppendLine(" and check_user  like CONCAT('%',@CheckUser,'%')");
                param.Add("@CheckUser", request.CheckUser.Trim());
            }

            //逗号分隔
            if (!string.IsNullOrEmpty(request.PayStatus))
            {
                var statusList = request.PayStatus.Split(',');
                var payStatus = string.Empty;
                foreach (var item in statusList)
                {
                    if (item == "未付款")
                    {
                        payStatus += "0,";
                    }
                    else if (item == "付款失败") {
                        payStatus += "1,";
                    }
                    else if (item == "已付款")
                    {
                        payStatus += "2,";
                    }
                }
                builder.AppendLine($" and status in ({payStatus.TrimEnd(',')})");
               
            }

            var sqlCount = @"SELECT
	                    count(st.id)
	                     FROM
	                    settlement_batch st
                    WHERE
	                    is_deleted = 0 " + builder.ToString();

            var sql = @"SELECT
	                    id Id,
	                    st.settlement_batch_no SettlementBatchNo,
	                    st.status Status,
	                    st.check_user CheckUser,
	                    st.apply_user ApplyUser,
	                    st.apply_time ApplyTime,
	                    st.bill_amount BillAmount,
	                    st.bank_name BankName,
	                    st.bank_branch BankBranch,
	                    st.bank_user BankUser,
	                    st.bank_no BankNo,
	                    st.top_remark TopRemark,
	                    st.location_id LocationId,
	                    st.location_name LocationName,
	                    st.province_id ProvinceId,
	                    st.city_id CityId,
	                    st.district_id DistrictId,
	                    st.province Province,
	                    st.city City,
	                    st.address Address,
	                    st.district District,
	                    st.create_by CreateBy,
	                    st.create_time CreateTime 
                    FROM
	                    settlement_batch st 
                    WHERE
	                    is_deleted = 0 " + builder.ToString() + " order by id desc LIMIT @index, @size";

            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
            });

            IEnumerable<SettlementBatchDO> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<SettlementBatchDO>(sql, param));
            res.TotalItems = total;
            res.Items = productDOs.ToList();
            return res;
        }

        /// <summary>
        /// 结算单处理
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> SaveSettlementReview(SettlementPayReviewDO request)
        {
            long id = 0;
            await OpenConnectionAsync(async conn =>
            {
                var tran = conn.BeginTransaction();
                try
                {
                    id = await InsertAsync<SettlementPayReviewDO>(request);

                    string sql =
                        "update settlement_batch set status=@status, top_remark=@Remark,update_by=@UpdateBY,update_time=NOW(3) where settlement_batch_no=@SettlementBatchNo and is_deleted=0";
                    var parameters = new DynamicParameters();
                    parameters.Add("@UpdateBY", request.UpdateBy);
                    parameters.Add("@Remark", request.Remark);
                    parameters.Add("@SettlementBatchNo", request.SettlementBatchNo);
                    parameters.Add("@status", request.Status);
                    id = await conn.ExecuteAsync(sql, parameters);

                    tran.Commit();
                }
                catch (Exception e)
                {
                    id = 0;
                    tran.Rollback();
                }
                finally
                {

                }
            });
            return id;
        }

        /// <summary>
        /// 得到结算单明细
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<GetSettlementDetailResponse>> GetSettlementDetail(GetSettlementDetailRequest request)
        {
            if (string.IsNullOrEmpty(request.SettlementBatchNo))
                return null;
            else
            {
                var builder = new StringBuilder();
                var parameters = new DynamicParameters();
                builder.AppendLine(" and is_deleted=0");

                var sql = @"SELECT
	                        A.settlement_batch_no AS SettlementBatchNo,
	                        A.order_no AS OrderNo,
	                        A.location_id AS LocationId,
	                        A.location_name AS LocationName,
	                        account_type AS AccountType,
	                        install_time AS InstallTime,
	                        order_type AS OrderType,
	                        sale_total_amount AS SaleTotalAmount,
	                        total_cost AS TotalCost,
	                        service_fee AS ServiceFee,
	                        commission_fee AS CommissionFee,
	                        other_fee AS OtherFee,
	                        settlement_fee AS SettlementFee,
	                        B.apply_user AS ApplyUser,
	                        B.check_user AS CheckUser,
	                        A.pay_method AS PayMethod ,A.company_id CompanyId,A.company_name CompanyName,A.shop_install_amount ShopInstallAmount,A.actual_amount ActualAmount,
                        A.shop_distribution_cost ShopDistributionCost,A.shop_distribution_gross_profit ShopDistributionGrossProfit,A.shop_difference_price ShopDifferencePrice,
                        A.shop_commission_fee ShopCommissionFee,A.shop_settlement_amount ShopSettlementAmount,A.company_back_amount CompanyBackAmount,A.shop_other_fee ShopOtherFee,
                        A.shop_item_fee ShopItemFee,A.company_commission_fee CompanyCommissionFee,A.company_settlement_amount CompanySettlementAmount,A.company_other_fee CompanyOtherFee
                        FROM
	                        settlement_batch_detail A
	                        INNER JOIN settlement_batch B ON A.settlement_batch_no = B.settlement_batch_no 
	                        AND A.is_deleted = 0 
	                        AND B.is_deleted = 0 
                        WHERE
	                        B.settlement_batch_no = @SettlementBatchNO ";

                parameters.Add("@SettlementBatchNO", request.SettlementBatchNo);

                IEnumerable<GetSettlementDetailResponse> OrderBatchList = null;
                await OpenConnectionAsync(async conn => OrderBatchList = await conn.QueryAsync<GetSettlementDetailResponse>(sql, parameters));

                return OrderBatchList.ToList();
            }
        }

        /// <summary>
        /// 得动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AccountCheckDO> GetAccountCheckInfo(GetFianceBillDetailRequest request)
        {

            var builder = new StringBuilder();
            var parameters = new DynamicParameters();

            builder.Append("where order_no=@OrderNo");
            parameters.Add("@OrderNo", request.OrderNo);
            parameters.Add(" and location_id=@ShopId");
            parameters.Add("@ShopId", request.ShopId);

            AccountCheckDO orderAccountCheckDo = null;

            var orderAccountCheckDos = await GetListAsync<AccountCheckDO>(builder.ToString(), parameters, false);

            return orderAccountCheckDos?.FirstOrDefault();

        }

        public async  Task<SettlementBatchDO> GetSettlementBatchDetail(string batchNo)
        {
            var param = new DynamicParameters();
            param.Add("@batchNo", batchNo);
            var sql = @"SELECT
	                    id Id,
	                    st.settlement_batch_no SettlementBatchNo,
	                    st.status Status,
	                    st.check_user CheckUser,
	                    st.apply_user ApplyUser,
	                    st.apply_time ApplyTime,
	                    st.bill_amount BillAmount,
	                    st.bank_name BankName,
	                    st.bank_no BankNo,
	                    st.top_remark TopRemark,
	                    st.location_id LocationId,
	                    st.location_name LocationName,
	                    st.province_id ProvinceId,
	                    st.city_id CityId,
	                    st.district_id DistrictId,
	                    st.province Province,
	                    st.city City,
	                    st.address Address,
	                    st.district District,
	                    st.create_by CreateBy,
	                    st.create_time CreateTime 
                    FROM
	                    settlement_batch st 
                    WHERE
	                    is_deleted = 0 and settlement_batch_no =@batchNo";
            SettlementBatchDO settlement = null;
            await OpenConnectionAsync(async conn => settlement = await conn.QueryFirstOrDefaultAsync<SettlementBatchDO>(sql, param));

            return settlement;
        }

        public async  Task<SettlementPayReviewDO> SettlementPayReviewDO(string batchNo)
        {
            var sql = @"SELECT
	                    shop_name ShopName,
	                    create_by CreateBy,
	                    create_time CreateTime ,remark Remark
                    FROM
	                    settlement_pay_review 
                    WHERE
	                    is_deleted = 0 
	                    AND settlement_batch_no = @settlement_batch_no and status=2 order by create_time desc";

            var param = new DynamicParameters();
            param.Add("@settlement_batch_no", batchNo);
            SettlementPayReviewDO reviewDO = null;
            await OpenConnectionAsync(async conn => reviewDO = await conn.QueryFirstOrDefaultAsync<SettlementPayReviewDO>(sql, param));

            return reviewDO;
        }
    }

}
