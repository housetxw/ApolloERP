using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.FMS.Service.Client.Clients;
using Ae.FMS.Service.Common;
using Ae.FMS.Service.Common.Constant;
using Ae.FMS.Service.Common.Template;
using Ae.FMS.Service.Core.Enums;
using Ae.FMS.Service.Core.Interfaces;
using Ae.FMS.Service.Core.Model;
using Ae.FMS.Service.Core.Model.AccountCheck;
using Ae.FMS.Service.Core.Request;
using Ae.FMS.Service.Core.Request.AccountCheck;
using Ae.FMS.Service.Core.Request.Settlement;
using Ae.FMS.Service.Core.Response;
using Ae.FMS.Service.Core.Response.Settlement;
using Ae.FMS.Service.Dal.Model;
using Ae.FMS.Service.Dal.Model.AccountCheck;
using Ae.FMS.Service.Dal.Model.settlement;
using Ae.FMS.Service.Dal.Repositorys;

namespace Ae.FMS.Service.Imp.Services
{
    /// <summary>
    /// 结算服务
    /// </summary>
    public class SettlementService : ISettlementService
    {
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<SettlementService> _logger;
        private readonly ISettlementRepository _settlementRepository;
        private readonly ShopManageClient _shopManageClient;
        private readonly IAccountCheckService _accountCheckService;
        private readonly IReconciliationFeeRepository _reconciliationFeeRepository;
        private readonly IPurchaseSettlementBatchRepository _purchaseSettlementBatchRepository;
        private readonly IPurchaseSettlementDetailRepository _purchaseSettlementDetailRepository;
        private readonly IPurchaseSettlementPayReviewRepository _purchaseSettlementPayReviewRepository;
        private readonly IPurchaseAccountCheckRepository _purchaseAccountCheckRepository;


        public SettlementService(IMapper mapper, ApolloErpLogger<SettlementService> logger,
            ISettlementRepository settlementRepository, ShopManageClient shopManageClient, IAccountCheckService accountCheckService,
            IReconciliationFeeRepository reconciliationFeeRepository,
            IPurchaseSettlementBatchRepository purchaseSettlementBatchRepository,
            IPurchaseSettlementDetailRepository purchaseSettlementDetailRepository,
            IPurchaseSettlementPayReviewRepository purchaseSettlementPayReviewRepository, IPurchaseAccountCheckRepository purchaseAccountCheckRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _settlementRepository = settlementRepository;
            _shopManageClient = shopManageClient;
            _accountCheckService = accountCheckService;
            _reconciliationFeeRepository = reconciliationFeeRepository;
            _purchaseSettlementBatchRepository = purchaseSettlementBatchRepository;
            _purchaseSettlementDetailRepository = purchaseSettlementDetailRepository;
            _purchaseSettlementPayReviewRepository = purchaseSettlementPayReviewRepository;
            _purchaseAccountCheckRepository = purchaseAccountCheckRepository;
        }

        /// <summary>
        /// 得到门店结算账户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetAccountInfoResponse> GetAccountInfo(GetAccountInfoRequest request)
        {
            var dalResponse = await _settlementRepository.GetAccountSettlement(request);
            if (dalResponse != null)
                return _mapper.Map<GetAccountInfoResponse>(dalResponse);
            return null;
        }

        /// <summary>
        /// 得到开始提现申请的信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetWithdrawalApplyResponse> GetWithdrawalApply(GetWithdrawalApplyRequest request)
        {
            var getAccountBalanceAmountDalResponse = await _settlementRepository.GetAccountBalanceAmount(request.ShopId);

            var geShopConfigInfoClientResponse = await _shopManageClient.GetShopConfigInfo(request.ShopId);
            if (geShopConfigInfoClientResponse.Code == ResultCode.Success && geShopConfigInfoClientResponse.Data != null)
            {
                if (geShopConfigInfoClientResponse.Data.ShopBankCard != null)
                {
                    return new GetWithdrawalApplyResponse()
                    {
                        BankNo = geShopConfigInfoClientResponse.Data.ShopBankCard.BankCardNo,
                        BankName = geShopConfigInfoClientResponse.Data.ShopBankCard.OpeningBank,
                        BankBranch = geShopConfigInfoClientResponse.Data.ShopBankCard.OpeningBranch,
                        BankUser = geShopConfigInfoClientResponse.Data.ShopBankCard.OpeningUserName,
                        IconUrl = geShopConfigInfoClientResponse.Data.ShopBankCard.IconUrl,
                        WithdrawalAmount = getAccountBalanceAmountDalResponse,
                        Remark = GetWithdrawalApplyTempate.Description
                    };
                }
            }

            return null;

        }

        /// <summary>
        /// 得到提现记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GetWithdrawalRecordListResponse>> GetWithdrawalRecordList(GetWithdrawalRecordListRequest request)
        {
            var getWithdrawalRecordListDalResponse = await _settlementRepository.GetWithdrawalRecordList(request);
            if (getWithdrawalRecordListDalResponse?.Items != null)
            {
                return _mapper.Map<ApiPagedResultData<GetWithdrawalRecordListResponse>>(getWithdrawalRecordListDalResponse);
            }

            return null;
        }

        /// <summary>
        /// 得到订单提现记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GetWithdrawalOrderRecordListResponse>> GetWithdrawalOrderRecordList(GetWithdrawalOrderRecordListRequest request)
        {
            var getWithdrawalOrderRecordListDalResponse = await _settlementRepository.GetWithdrawalOrderRecordList(request);
            if (getWithdrawalOrderRecordListDalResponse?.Items != null)
            {
                return _mapper.Map<ApiPagedResultData<GetWithdrawalOrderRecordListResponse>>(getWithdrawalOrderRecordListDalResponse);
            }

            return null;
        }

        /// <summary>
        /// 得到财务账单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GetFianceReconciliationStatusListResponse>> GetFianceReconciliationStatusList(GetFianceReconciliationStatusListRequest request)
        {
            var getFianceReconciliationStatusListDalResponse = await _settlementRepository.GetFianceReconciliationStatusList(request);
            if (getFianceReconciliationStatusListDalResponse?.Items != null)
            {
                return _mapper.Map<ApiPagedResultData<GetFianceReconciliationStatusListResponse>>(getFianceReconciliationStatusListDalResponse);
            }

            return null;
        }

        public async Task<GetFianceBillDetailResponse> GetFianceBillDetail(GetFianceBillDetailRequest request)
        {
            var getFianceBillDetail = await _settlementRepository.GetFianceBillDetail(request);
            if (getFianceBillDetail != null)
            {
                return _mapper.Map<GetFianceBillDetailResponse>(getFianceBillDetail);
            }

            return null;
        }

        /// <summary>
        /// 发起提现申请
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitWithdrawalApplyResponse>> SubmitWithdrawalApply(SubmitWithdrawalApplyRequest request)
        {
            var getAccountWithdrawalList = await _settlementRepository.GetAccountWithdrawalList(request.ShopId);
            if (getAccountWithdrawalList == null || !getAccountWithdrawalList.Any())
                return new ApiResult<SubmitWithdrawalApplyResponse>()
                {
                    Code = ResultCode.Failed,
                    Message = CommonConstant.NoApplyData
                };
            // var balanceAmount = getAccountWithdrawalList?.Sum(item => item.SettlementFee);
            var balanceAmount = getAccountWithdrawalList?.Sum(item => item.ShopSettlementAmount);
            if (balanceAmount != request.ApplyAmount)
                return new ApiResult<SubmitWithdrawalApplyResponse>()
                {
                    Code = ResultCode.Failed,
                    Message = CommonConstant.ApplyAmountChange
                };
            var shopConfigInfo = await _shopManageClient.GetShopConfigInfo(request.ShopId);
            if (shopConfigInfo.Code != ResultCode.Success || shopConfigInfo?.Data == null)
                return new ApiResult<SubmitWithdrawalApplyResponse>()
                {
                    Code = ResultCode.Failed,
                    Message = CommonConstant.NoSearchBank
                };
            if (shopConfigInfo.Data?.ShopBankCard?.BankCardNo != request.BankNo
                || shopConfigInfo?.Data?.ShopBankCard?.OpeningBank != request.BankName
                || shopConfigInfo?.Data?.ShopBankCard?.OpeningBranch != request.BankBranch
                || shopConfigInfo?.Data?.ShopBankCard?.OpeningUserName != request.BankUser )
                return new ApiResult<SubmitWithdrawalApplyResponse>()
                {
                    Code = ResultCode.Failed,
                    Message = CommonConstant.BankChange
                };


            SettlementBatchDO settlementBatchDo = new SettlementBatchDO()
            {
                SettlementBatchNo = string.Empty,
                Status = (sbyte)SettlementBatchStatusEnum.Apply,
                CheckUser = shopConfigInfo?.Data?.Shop.AccountingPerson ?? string.Empty,
                ApplyUser = shopConfigInfo?.Data?.Shop?.SimpleName ?? (request.ApplyUser ?? string.Empty),
                ApplyTime = DateTime.Now,
                BillAmount = balanceAmount ?? 0,
                BankName = shopConfigInfo?.Data?.ShopBankCard?.OpeningBank ?? string.Empty,
                BankBranch = shopConfigInfo?.Data?.ShopBankCard?.OpeningBranch ?? string.Empty,
                BankUser = shopConfigInfo?.Data?.ShopBankCard?.OpeningUserName ?? string.Empty,
                BankNo = shopConfigInfo?.Data?.ShopBankCard?.BankCardNo,
                TopRemark = string.Empty,
                LocationId = request.ShopId,
                LocationName = shopConfigInfo?.Data?.Shop?.FullName ?? string.Empty,
                ProvinceId = shopConfigInfo?.Data?.Shop.ProvinceId ?? string.Empty,
                CityId = shopConfigInfo?.Data?.Shop?.CityId ?? string.Empty,
                DistrictId = shopConfigInfo?.Data?.Shop?.DistrictId ?? string.Empty,
                Province = shopConfigInfo?.Data?.Shop?.Province ?? string.Empty,
                City = shopConfigInfo?.Data?.Shop?.City ?? string.Empty,
                Address = shopConfigInfo?.Data?.Shop?.Address ?? string.Empty,
                District = shopConfigInfo?.Data?.Shop?.District ?? string.Empty,
                IsDeleted = 0,
                CreateBy = request.ApplyUser,
                CreateTime = DateTime.Now,
                UpdateBy = request.ApplyUser,
                UpdateTime = DateTime.Now
            };

            List<SettlementBatchDetailDO> settlementBatchDetailDOs = new List<SettlementBatchDetailDO>();

            //ReconciliationFees表的功能暂时只作为技师绩效使用，不作为对账提现使用。txw20230105
            //var reconciliationFees = await _reconciliationFeeRepository.GetReconciliationFees(new GetReconciliationFeesRequest()
            //{
            //    OrderNos = getAccountWithdrawalList?.Select(_ => _.OrderNo)?.ToList()
            //});

            getAccountWithdrawalList?.ForEach(_ =>
            {
                //var reconciliationFee = reconciliationFees?.Where(item => _.OrderNo == item.OrderNo)?.FirstOrDefault();
                settlementBatchDetailDOs.Add(new SettlementBatchDetailDO()
                {
                    SettlementBatchId = 0,
                    SettlementBatchNo = string.Empty,
                    OrderNo = _.OrderNo,
                    LocationId = _.LocationId,
                    LocationName = _.LocationName,
                    AccountType = _.AccountType,
                    InstallTime = _.InstallTime,
                    OrderType = _.OrderType,
                    SaleTotalAmount = _.SaleTotalAmount,
                    TotalCost = _.TotalCost,
                    PayMethod = _.PayMethod,
                    //ServiceFee = _.ServiceFee,
                    //CommissionFee = _.CommissionFee,
                    //SettlementFee = _.SettlementFee,
                    //OtherFee = _.OtherFee,
                    IsDeleted = 0,
                    CreateBy = request.ApplyUser,
                    CreateTime = DateTime.Now,
                    UpdateBy = request.ApplyUser,
                    UpdateTime = DateTime.Now,
                    ShopInstallAmount = _.ShopInstallAmount ,
                    ActualAmount = _.ActualAmount ,
                    ShopDistributionCost = _.ShopDistributionCost ,
                    ShopDistributionGrossProfit = _.ShopDistributionGrossProfit ,
                    ShopDifferencePrice = _.ShopDifferencePrice ,
                    ShopCommissionFee = _.ShopCommissionFee ,
                    ShopSettlementAmount = _.ShopSettlementAmount ,
                    ShopOtherFee = _.ShopOtherFee ,
                    ShopItemFee = _.ShopItemFee ,
                    CompanyBackAmount = _.CompanyBackAmount,
                    CompanyCommissionFee = _.CompanyCommissionFee ,
                    CompanySettlementAmount = _.CompanySettlementAmount ,
                    CompanyId = _.CompanyId ,
                    CompanyName = _.CompanyName,

                    //ShopInstallAmount = reconciliationFee?.ShopInstallAmount ?? 0,
                    //ActualAmount = reconciliationFee?.ActualAmount ?? 0,
                    //ShopDistributionCost = reconciliationFee?.ShopDistributionCost ?? 0,
                    //ShopDistributionGrossProfit = reconciliationFee?.ShopDistributionGrossProfit ?? 0,
                    //ShopDifferencePrice = reconciliationFee?.ShopDifferencePrice ?? 0,
                    //ShopCommissionFee = reconciliationFee?.ShopCommissionFee ?? 0,
                    //ShopSettlementAmount = reconciliationFee?.ShopSettlementAmount ?? 0,
                    //ShopOtherFee = reconciliationFee?.ShopOtherFee ?? 0,
                    //ShopItemFee = reconciliationFee?.ShopItemFee ?? 0,
                    //CompanyBackAmount = reconciliationFee?.CompanyBackAmount ?? 0,
                    //CompanyCommissionFee = reconciliationFee?.CompanyCommissionFee ?? 0,
                    //CompanySettlementAmount = reconciliationFee?.CompanySettlementAmount ?? 0,
                    //CompanyId = reconciliationFee?.CompanyId ?? 0,
                    //CompanyName = reconciliationFee?.CompanyName,


                });
            });

            var executeId = await _settlementRepository.SubmitWithdrawalApply(settlementBatchDo, settlementBatchDetailDOs);
            //if (executeId == 0)
            //    return new ApiResult<SubmitWithdrawalApplyResponse>()
            //    {
            //        Code = ResultCode.Failed,
            //        Message = CommonConstant.PayFailure
            //    };

            _accountCheckService.RgAccountCheckSettlement(new RgAccountCheckWithdrawRequeset()
            {
                LocationId = request.ShopId,
                LocationName = shopConfigInfo?.Data?.Shop?.FullName,
                OrderNos = settlementBatchDetailDOs?.Select(_ => _.OrderNo)?.ToList(),
                UpdateBy = request.ApplyUser,
            });

            return new ApiResult<SubmitWithdrawalApplyResponse>()
            {
                Code = ResultCode.Success,
                Message = CommonConstant.ApplySuccess
            };

            //TODO 日志没有写
        }

        /// <summary>
        /// 得到结算单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GetSettlementListResponse>> GetSettlementList(GetSettlementListRequest request)
        {
            var dalResponse = await _settlementRepository.GetSettlementList(request);

            var response = _mapper.Map<ApiPagedResultData<GetSettlementListResponse>>(dalResponse);

            if (response.Items != null && response.Items.Any())
            {
                foreach (var item in response.Items)
                {
                    item.ApplyTimeStr = item.ApplyTime.ToString("yyyy-MM-dd");
                    item.CreateTimeStr = item.CreateTime.ToString("yyyy-MM-dd");
                    item.StatusStr = ((SettlementBatchStatusEnum)item.Status).GetDescription();
                }
            }
            return response;
        }

        /// <summary>
        /// 结算处理
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> SaveSettlementReview(SaveSettlementReviewRequest request)
        {
            var statusText = string.Empty;
            if (request.Status == SettlementBatchStatusEnum.Apply.ToInt())
                statusText = SettlementBatchStatusEnum.Apply.GetDescription();
            if (request.Status == SettlementBatchStatusEnum.PayFailure.ToInt())
                statusText = SettlementBatchStatusEnum.PayFailure.GetDescription();
            if (request.Status == SettlementBatchStatusEnum.HavePay.ToInt())
                statusText = SettlementBatchStatusEnum.HavePay.GetDescription();

            var settlementInfo = await _settlementRepository.GetSettlementBatchDetail(request.SettlementBatchNo);

            await Task.Factory.StartNew(async () =>
             {
                 await _accountCheckService.CreateAccountCheckLog(new CreateAccountCheckLogRequest
                 {
                     RelationNo = request.SettlementBatchNo,
                     RelationType = AccountCheckRelationTypeEnum.结算单.ToString(),
                     LocationId = settlementInfo.LocationId,
                     Remark = $"门店【{settlementInfo.LocationName}】的订单【{request.SettlementBatchNo}】已审核,审核结果 {statusText}!",
                     CreateBy = request.CreateBy,
                     CreateTime = DateTime.Now
                 });
             });

            SettlementPayReviewDO dalRequest = _mapper.Map<SettlementPayReviewDO>(request);
            dalRequest.UpdateBy = request.CreateBy;
            var users = request.CreateBy.Split('@');
            var user = users.Length > 1 ? users[1] : users[0];

            if (settlementInfo != null)
            {
                dalRequest.SettlementBatchId = settlementInfo.Id;
                dalRequest.ShopId = settlementInfo.LocationId;
                dalRequest.ShopName = settlementInfo.LocationName;
                dalRequest.CreateBy = user;
                dalRequest.CreateTime = DateTime.Now;
            }

            return await _settlementRepository.SaveSettlementReview(dalRequest);

        }

        /// <summary>
        /// 获得结算单明细
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<GetSettlementDetailResponse>> GetSettlementDetail(GetSettlementDetailRequest request)
        {
            var dalResponse = await _settlementRepository.GetSettlementDetail(request);

            return dalResponse;
        }

        /// <summary>
        ///  得到对账信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AccountCheckDTO> GetAccountCheckInfo(GetFianceBillDetailRequest request)
        {

            var dalResponse = await _settlementRepository.GetAccountCheckInfo(request);

            var response = _mapper.Map<AccountCheckDTO>(dalResponse);

            return response;
        }

        public async Task<SettlementPayReviewDTO> SettlementPayReviewDO(SettlementPayReviewDTO request)
        {
            var result = await _settlementRepository.SettlementPayReviewDO(request.SettlementBatchNo);

            var vo = _mapper.Map<SettlementPayReviewDTO>(result);

            return vo;
        }

        /// <summary>
        /// 获取采购对账结算单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<PurchaseSettlementBatchDTO>> GetPurchaseSettlementList(GetPurchaseSettlementListRequest request)
        {
            var dalResponse = await _purchaseSettlementBatchRepository.GetPurchaseSettlementList(request);

            var apiPagedResultData = _mapper.Map<ApiPagedResultData<PurchaseSettlementBatchDTO>>(dalResponse);

            return new ApiPagedResult<PurchaseSettlementBatchDTO>()
            {
                Code = ResultCode.Success,
                Data = apiPagedResultData
            };
        }

        /// <summary>
        /// 获取对账单明细
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<PurchaseSettlementDetailDTO>>> GetPurchaseSettlementDetail(GetPurchaseSettlementDetailRequest request)
        {
            var dalResponse = await _purchaseSettlementDetailRepository.GetListAsync($" where is_deleted=0 and settlement_batch_no=@BatchNo", new { BatchNo = request.SettlementBatchNo }, true);

            var list = _mapper.Map<List<PurchaseSettlementDetailDTO>>(dalResponse);

            return Result.Success(list);

        }

        /// <summary>
        /// 审核结算单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> SavePurchaseSettlementReview(ApiRequest<SavePurchaseSettlementReviewRequest> request)
        {
            var purchaseBactch = await _purchaseSettlementBatchRepository.GetListAsync($" where is_deleted=0 and settlement_batch_no=@BatchNo", new { BatchNo = request.Data.SettlementNo }, true);

            var singlePurchaseBatch = purchaseBactch.FirstOrDefault();
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _purchaseSettlementPayReviewRepository.InsertAsync(new PurchaseSettlementPayReviewDO()
                {
                    CreateBy = request.Data.CreateUser,
                    LocationId = singlePurchaseBatch.LocationId,
                    LocationName = singlePurchaseBatch.LocationName,
                    SettlementBatchId = singlePurchaseBatch.Id,
                    SettlementBatchNo = singlePurchaseBatch.SettlementBatchNo,
                    Status = request.Data.Status,
                    Remark = request.Data.Remark,
                    CreateTime = DateTime.Now
                });
                singlePurchaseBatch.Status = request.Data.Status;
                await _purchaseSettlementBatchRepository.UpdateAsync(singlePurchaseBatch, new List<string> { "Status" });

                ts.Complete();
            }


            return Result.Success<string>("操作成功");

        }

        /// <summary>
        /// 生成结算单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> SavePurchaseSettlementOrder(ApiRequest<SavePurchaseSettlementOrderRequest> request)
        {
            var startDate = DateTime.Now.AddMonths(-1);
            var endDate = DateTime.Now.AddMonths(-1);

            if (!string.IsNullOrEmpty(request.Data.StartSettelementDate))
            {
                DateTime.TryParse(request.Data.StartSettelementDate, out var startSettlementDate);

                startDate = startSettlementDate.AddDays(1 - startSettlementDate.Day);
            }
            if (!string.IsNullOrEmpty(request.Data.EndSettlementDate))
            {
                DateTime.TryParse(request.Data.EndSettlementDate, out var endSettlementDate);

                endDate = endSettlementDate.AddDays(1 - endSettlementDate.Day).AddMonths(1).AddDays(-1);
            }

            if (endDate < startDate)
                return Result.Failed<string>("结束日期 大于 开始日期");
            if (endDate.Month == DateTime.Now.Month)
                return Result.Failed<string>($"只能生成{ DateTime.Now.Month}月份之前的结算单");

            if (endDate.Year * 12 + endDate.Month - startDate.Year * 12 - startDate.Month > 2)
                return Result.Failed<string>("不允许生成3个月以上的结算单");

            var purchaseAccountCount = await _purchaseAccountCheckRepository.RecordCountAsync(" where is_deleted=0 and status='已核对'and location_id=@ShopId and purchase_date>=@StartDate and purchase_date<=@EndDate", new { StartDate = startDate, EndDate = endDate, ShopId = request.Data.ShopId });

            if (purchaseAccountCount == 0)
                return Result.Failed<string>("没有可生成结算单的数据");

            List<PurchaseSettlementBatchDO> settlementBatchDOs = new List<PurchaseSettlementBatchDO>(5);
            List<PurchaseSettlementBatchDO> deleteSettlementBatchDOs = new List<PurchaseSettlementBatchDO>(5);

            List<PurchaseSettlementDetailDO> settlementBatchDetailDOs = new List<PurchaseSettlementDetailDO>(10);

            if (purchaseAccountCount > 0)
                for (var i = 0; i <= purchaseAccountCount / 20; i++)
                {
                    var datas = await _purchaseAccountCheckRepository.GetListPagedAsync(i + 1, 10, " where is_deleted=0 and location_id=@ShopId and status='已核对' and purchase_date>=@StartDate and purchase_date<=@EndDate ", " id desc", new { StartDate = startDate, EndDate = endDate, ShopId = request.Data.ShopId }, true);

                    datas?.Items?.ToList()?.ForEach(_ =>
                    {
                        var settlemStaff = settlementBatchDOs.Where(item => item.SettlementStaff == _.SettlementStaff && item.SettlementType == _.SettlementType && item.SettlementMonth == _.PurchaseDate.Month);
                        if (settlemStaff?.Count() == 0)
                        {
                            settlementBatchDOs.Add(new PurchaseSettlementBatchDO()
                            {
                                SettlementBatchNo = string.Empty,
                                SettlementType = _.SettlementType,
                                SettlementYear = _.PurchaseDate.Year,
                                SettlementMonth = _.PurchaseDate.Month,
                                Status = (sbyte)SettlementBatchStatusEnum.Apply.ToInt(),
                                SettlementStaff = _.SettlementStaff,
                                ApplyUser = request.Data.CreateUser,
                                ApplyTime = DateTime.Now,
                                BillAmount = _.SettlementAmount,
                                LocationId = _.LocationId,
                                LocationName = _.LocationName,
                                IsDeleted = 0,
                                CreateBy = request.Data.CreateUser,
                                CreateTime = DateTime.Now
                            });
                        }
                        else
                        {
                            settlemStaff.FirstOrDefault().BillAmount += _.SettlementAmount;
                        }

                        PurchaseSettlementDetailDO purchaseSettlementDetailDO = _mapper.Map<PurchaseSettlementDetailDO>(_);
                        purchaseSettlementDetailDO.CreateBy = request.Data.CreateUser;
                        purchaseSettlementDetailDO.CreateTime = DateTime.Now;
                        settlementBatchDetailDOs.Add(purchaseSettlementDetailDO);
                    });
                }

            if (purchaseAccountCount > 0 && settlementBatchDOs.Count > 0)
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var settlementLen = settlementBatchDOs.Count;
                    for (var i = 0; i < settlementLen; i++)
                    {
                        var batchData = await _purchaseSettlementBatchRepository.GetListAsync(" where is_deleted=0 and location_id=@ShopId and settlement_staff = @Staffs and settlement_year=@Year and settlement_month=@Month and settlement_type=@SettlementType", new { Staffs = settlementBatchDOs[i].SettlementStaff, Year = settlementBatchDOs[i].SettlementYear, Month = settlementBatchDOs[i].SettlementMonth, SettlementType = settlementBatchDOs[i].SettlementType, ShopId = request.Data.ShopId });

                        if (batchData != null && batchData.Any())
                        {
                            if (batchData.FirstOrDefault().Status == SettlementBatchStatusEnum.HavePay.ToInt())
                            {
                                return Result.Failed<string>($"{batchData.FirstOrDefault().SettlementMonth}月份 结算单已经付款，请重新选择生成结算单日期");
                            }

                            deleteSettlementBatchDOs.Add(settlementBatchDOs[i]);
                        }
                    }
                    deleteSettlementBatchDOs?.ForEach(_ =>
                    {
                        settlementBatchDOs.Remove(_);
                    });

                    if (settlementBatchDOs?.Count() > 0)
                        await _purchaseSettlementBatchRepository.InsertBatchAsync(settlementBatchDOs);

                    settlementBatchDOs.AddRange(deleteSettlementBatchDOs);
                    await _purchaseSettlementDetailRepository.InsertBatchAsync(settlementBatchDetailDOs);

                    settlementBatchDOs?.ForEach(async _ =>
                    {
                        var batchData = await _purchaseSettlementBatchRepository.GetListAsync(" where is_deleted=0  and location_id=@ShopId and settlement_staff = @Staffs and settlement_year=@Year and settlement_month=@Month and settlement_type=@SettlementType", new { Staffs = _.SettlementStaff, Year = _.SettlementYear, Month = _.SettlementMonth, SettlementType = _.SettlementType, ShopId = request.Data.ShopId });

                        var batchNo = $"BN{batchData.FirstOrDefault().Id.ToString("D8")}";

                        var beginDate = new DateTime(_.SettlementYear, _.SettlementMonth, 1);
                        var purchaseProducts = await _purchaseSettlementDetailRepository.GetListAsync("where is_deleted=0 and location_id=@ShopId and settlement_staff=@Staffs and purchase_date>=@StartDate and purchase_date<@EndDate and settlement_type=@SettlementType ", new
                        {
                            Staffs = _.SettlementStaff,
                            StartDate = beginDate,
                            EndDate = beginDate.AddMonths(1),
                            SettlementType = _.SettlementType,
                            ShopId = request.Data.ShopId
                        }, true);

                        decimal settlementAmount = 0;
                        purchaseProducts?.ToList()?.ForEach(async item =>
                        {
                            item.SettlementBatchId = batchData.FirstOrDefault().Id;
                            item.SettlementBatchNo = batchNo;
                            settlementAmount += item.SettlementAmount;
                            item.UpdateBy = request.Data.CreateUser;
                            item.UpdateTime = DateTime.Now;
                            await _purchaseSettlementDetailRepository.UpdateAsync(item, new List<string>() { "SettlementBatchId", "SettlementBatchNo", "UpdateBy", "UpdateTime" });
                        });

                        if (batchData != null && batchData.Any())
                        {
                            batchData.FirstOrDefault().SettlementBatchNo = batchNo;
                            batchData.FirstOrDefault().BillAmount = settlementAmount;
                            batchData.FirstOrDefault().UpdateBy = request.Data.CreateUser;
                            batchData.FirstOrDefault().UpdateTime = DateTime.Now;

                            await _purchaseSettlementBatchRepository.UpdateAsync(batchData.First(), new List<string>() { "SettlementBatchNo", "BillAmount" });
                        }

                        await _purchaseAccountCheckRepository.
                        UpdatePurchaseAccountStatusForHaveSettlement(request.Data.ShopId, startDate.ToString(), endDate.ToString(), request.Data.CreateUser);
                    });

                    ts.Complete();
                }
            }


            return Result.Success<string>("操作成功");


        }
    }
}
