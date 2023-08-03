using AutoMapper;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.FMS.Service.Client.Clients;
using Ae.FMS.Service.Client.Model;
using Ae.FMS.Service.Client.Request;
using Ae.FMS.Service.Common;
using Ae.FMS.Service.Core.Enums;
using Ae.FMS.Service.Core.Interfaces;
using Ae.FMS.Service.Core.Model;
using Ae.FMS.Service.Core.Request;
using Ae.FMS.Service.Dal.Model;
using Ae.FMS.Service.Dal.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Data.DapperExtensions.Queryable.Analyzers;
using Ae.FMS.Service.Core.Model.AccountCheck;
using Ae.FMS.Service.Core.Request.AccountCheck;
using Ae.FMS.Service.Dal.Model.AccountCheck;
using Ae.FMS.Service.Imp.Helpers;
using System.Transactions;

namespace Ae.FMS.Service.Imp.Services
{
    public class AccountCheckService : IAccountCheckService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<AccountCheckService> logger;
        private readonly IAccountCheckRepository accountCheckRepository;

        private readonly OrderClient orderClient;
        private readonly ShopManageClient shopManageClient;
        private readonly ProductClient productClient;
        private IConfiguration configuration { get; }
        private readonly IReconciliationFeeRepository _reconciliationFeeRepository;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IPurchaseOrderProductRepository _purchaseOrderProductRepository;
        private readonly IPurchaseAccountCheckRepository _purchaseAccountCheckRepository;
        private readonly IPurchaseAccountCheckExceptionRepository _purchaseAccountCheckExceptionRepository;
        private readonly IApolloErpWMSClient _ApolloErpWMSClient;


        public AccountCheckService(IMapper mapper, ApolloErpLogger<AccountCheckService> logger,
            IAccountCheckRepository accountCheckRepository, OrderClient orderClient,
            ShopManageClient shopManageClient, ProductClient productClient, IConfiguration configuration,
            IReconciliationFeeRepository reconciliationFeeRepository, IPurchaseOrderRepository purchaseOrderRepository,
            IPurchaseOrderProductRepository purchaseOrderProductRepository, IPurchaseAccountCheckRepository purchaseAccountCheckRepository, IPurchaseAccountCheckExceptionRepository purchaseAccountCheckExceptionRepository, IApolloErpWMSClient ApolloErpWMSClient
            )
        {
            this.accountCheckRepository = accountCheckRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.orderClient = orderClient;
            this.shopManageClient = shopManageClient;
            this.productClient = productClient;
            this.configuration = configuration;
            this._reconciliationFeeRepository = reconciliationFeeRepository;
            _purchaseOrderRepository = purchaseOrderRepository;
            _purchaseOrderProductRepository = purchaseOrderProductRepository;
            _purchaseAccountCheckRepository = purchaseAccountCheckRepository;
            _purchaseAccountCheckExceptionRepository = purchaseAccountCheckExceptionRepository;
            _ApolloErpWMSClient = ApolloErpWMSClient;
        }

        public async Task<int> CreateAccountCheck(CreateAccountCheckRequest request)
        {
            if (request.Accounts.Any())
            {
                var accounts = request.Accounts;
                string createBy = request?.Accounts?.FirstOrDefault()?.CreateBy ?? string.Empty;
                var vo = mapper.Map<List<AccountCheckDO>>(accounts);
                var getReconciliationFees = await _reconciliationFeeRepository.GetReconciliationFees(new GetReconciliationFeesRequest()
                {
                    OrderNos = request.Accounts?.Select(_ => _.OrderNo)?.ToList()
                });
                foreach (var item in vo)
                {

                    //var orderAmountResponse = await orderClient.GetAccountCheckAmount(new GetAccountCheckAmountClientRequest
                    //{
                    //    OrderNos = new List<string> { item.OrderNo }
                    //});

                    //var orderAmountInfo = orderAmountResponse.Data?.FirstOrDefault();

                    //订单基本信息
                    var orderInfoResposne = await orderClient.GetOrderBaseInfo(new GetOrderBaseInfoClientRequest
                    {
                        ShopId = Convert.ToInt32(item.LocationId),
                        OrderNos = new List<string> { item.OrderNo }
                    });

                    var orderBaseInfo = orderInfoResposne.Data?.FirstOrDefault();

                    var accountCheckDetail = await accountCheckRepository.GetAccountCheckDetail(new GetAccountCheckDetailRequest
                    {
                        OrderNo = item.OrderNo
                    });

                    var orderAmountInfo =
                        getReconciliationFees?.Where(_ => _.OrderNo == item.OrderNo)?.FirstOrDefault();

                    // 钱需要重新计算  并且状态的值需要重置
                    if (orderAmountInfo != null)
                    {
                        item.SaleTotalAmount = orderAmountInfo?.SaleTotalAmount ?? 0;
                        //item.ServiceFee = orderAmountInfo.ServiceFee;
                        //item.TotalCost = orderAmountInfo.TotalCost;
                        //item.CommissionFee = orderAmountInfo.CommissionFee;
                        //item.OtherFee = orderAmountInfo.OtherFee;
                        //item.SettlementFee = orderAmountInfo.SettlementFee;
                        item.ShopInstallAmount = orderAmountInfo?.ShopInstallAmount ?? 0;
                        item.ActualAmount = orderAmountInfo?.ActualAmount ?? 0;
                        item.ShopDistributionCost = orderAmountInfo?.ShopDistributionCost ?? 0;
                        item.ShopDistributionGrossProfit = orderAmountInfo?.ShopDistributionGrossProfit ?? 0;
                        item.ShopDifferencePrice = orderAmountInfo?.ShopDifferencePrice ?? 0;
                        item.ShopCommissionFee = orderAmountInfo?.ShopCommissionFee ?? 0;
                        item.ShopSettlementAmount = orderAmountInfo?.ShopSettlementAmount ?? 0;
                        item.ShopOtherFee = orderAmountInfo?.ShopOtherFee ?? 0;
                        item.ShopItemFee = orderAmountInfo?.ShopItemFee ?? 0;
                        item.ShopOutProductCost = orderAmountInfo?.ShopItemCost ?? 0;
                        item.CompanyBackAmount = orderAmountInfo?.CompanyBackAmount ?? 0;
                        item.CompanyCommissionFee = orderAmountInfo?.CompanyCommissionFee ?? 0;
                        item.CompanySettlementAmount = orderAmountInfo?.CompanySettlementAmount ?? 0;
                        item.CompanyId = orderAmountInfo?.CompanyId ?? 0;
                        item.CompanyName = orderAmountInfo?.CompanyName;
                        item.RgCheckResult = string.Empty;
                        item.RgCheckSuggestion = string.Empty;
                        item.ProductDetail = orderAmountInfo?.ProductDetail;
                        item.AccountType = orderAmountInfo?.AccountType;
                        if (orderBaseInfo.PayChannel == PayChannelEnum.Cash.ToInt())
                        {
                            item.ShopSettlementAmount = 0 - item.ShopDistributionCost - item.ShopOtherFee;
                        }
                    }
                    else
                    {
                        logger.Error($"订单号【{item.OrderNo}】查询订单对账信息为空!");
                        continue;
                    }

                    item.Channel = ((ChannelEnum)orderBaseInfo.Channel).GetEnumDescription();
                    item.Telephone = orderBaseInfo.UserPhone;
                    item.PayMethod = ((PayMethodEnum)orderBaseInfo.PayMethod).GetEnumDescription() + '/' + ((PayChannelEnum)orderBaseInfo.PayChannel).GetEnumDescription();
                    item.MoneyArriveStatus = ((MoneyArriveStatusEnum)orderBaseInfo.MoneyArriveStatus).GetEnumDescription();
                    //item.OrderType = ((OrderTypeEnum)orderBaseInfo.OrderType).GetEnumDescription();
                    //显示订单分类更好
                    item.OrderType = '('+((OrderTypeEnum)orderBaseInfo.OrderType).GetDescription() + ')' +((ProductTypeEnum)orderBaseInfo.ProduceType).GetDescription();
                    item.InstallTime = orderBaseInfo.InstallTime;
                    item.ShopCheckSuggestion = item.Remark;

                    if (accountCheckDetail != null)
                    {
                        var updateRequest = mapper.Map<UpdateAccountCheckRequest>(item);
                        updateRequest.UpdateBy = item.CreateBy;
                        //更新记录
                        await accountCheckRepository.UpdateAccountCheckInfo(updateRequest);
                    }
                    else
                    {
                        item.CreateBy = createBy;
                        item.CreateTime = DateTime.Now;
                        item.ShopCheckTime = DateTime.Now;
                        var accountResult = await accountCheckRepository.CreateAccountCheck(item);
                    }

                    var accountCheckDO = await accountCheckRepository.GetAccountCheckDetail(new GetAccountCheckDetailRequest
                    {
                        OrderNo = item.OrderNo
                    });

                    //long orderId = Convert.ToInt64(item.OrderNo.Substring(3, item.OrderNo.Length - 3));
                    if (item.ShopCheckResult == AccountCheckResultEnum.核对异常.ToString())
                    {
                        var exceptionVo = new AccountCheckExceptionDO()
                        {
                            CreateBy = item.CreateBy,
                            RelationNo = item.OrderNo,
                            CreateTime = DateTime.Now,
                            RelationType = AccountCheckRelationTypeEnum.门店.ToString(),
                            LocationId = item.LocationId,
                            LocationName = item.LocationName,
                            ReportBy = item.CreateBy,
                            ReportReason = item.Remark,
                            ReportTime = DateTime.Now,
                            ReportType = AccountCheckExceptionType.其他.ToString(),
                            Status = AccountCheckExceptionStatus.新建.ToString(),
                            InstallTime = orderBaseInfo.InstallTime,
                            PayMethod = ((PayMethodEnum)orderBaseInfo.PayMethod).GetEnumDescription(),
                            MoneyArriveStatus = ((MoneyArriveStatusEnum)orderBaseInfo.MoneyArriveStatus).GetEnumDescription(),
                            //OrderType = ((OrderTypeEnum)orderBaseInfo.OrderType).GetEnumDescription(),
                            //显示订单分类更好
                            OrderType = ((ProductTypeEnum)orderBaseInfo.ProduceType).GetDescription(),
                        SettlementFee = item.SettlementFee
                        };

                        await accountCheckRepository.CreateAccountCheckException(exceptionVo);
                        //门店异常对账
                        await orderClient.UpdateOrderStatus(new UpdateOrderStatusClientRequest
                        {
                            OrderNos = new List<string> { item.OrderNo },
                            UpdateBy = item.CreateBy,
                            UpdateStatusType = Client.Request.UpdateOrderStatusTypeEnum.ExceptionReconciliation
                        });
                    }
                    else
                    {
                        //门店已对账
                        await orderClient.UpdateOrderStatus(new UpdateOrderStatusClientRequest
                        {
                            OrderNos = new List<string> { item.OrderNo },
                            UpdateBy = item.CreateBy,
                            UpdateStatusType = Client.Request.UpdateOrderStatusTypeEnum.HaveReconciliation
                        });
                    }

                    //记录日志
                    await accountCheckRepository.CreateAccountCheckLog(new AccountCheckLogDO
                    {
                        RelationNo = item.OrderNo,
                        RelationType = AccountCheckRelationTypeEnum.门店.ToString(),
                        BeforeValue = accountCheckDetail != null ? JsonConvert.SerializeObject(accountCheckDetail) : string.Empty,
                        AfterValue = JsonConvert.SerializeObject(accountCheckDO),
                        LocationId = item.LocationId,
                        Remark = $"门店【{item.LocationName}】{item.ShopCheckResult}订单【{item.OrderNo}】",
                        CreateBy = createBy,
                        CreateTime = DateTime.Now
                    });
                }
            }
            return 1;
        }

        /// <summary>
        /// 核对异常接口(门店、总部核对)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> CreateAccountCheckException(AccountCheckExceptionDTO request)
        {
            var vo = mapper.Map<AccountCheckExceptionDO>(request);
            vo.CreateTime = DateTime.Now;
            vo.ReportTime = DateTime.Now;
            vo.Status = AccountCheckExceptionStatus.新建.ToString();

            //订单基本信息
            var orderInfoResposne = await orderClient.GetOrderBaseInfo(new GetOrderBaseInfoClientRequest
            {
                ShopId = Convert.ToInt32(request.LocationId),
                OrderNos = new List<string> { request.RelationNo }
            });

            //var orderAmountResponse = await orderClient.GetAccountCheckAmount(new GetAccountCheckAmountClientRequest
            //{
            //    OrderNos = new List<string> { request.RelationNo }
            //});

            //  var orderAmountInfo = orderAmountResponse.Data?.FirstOrDefault();

            var orderBaseInfo = orderInfoResposne.Data?.FirstOrDefault();

            vo.InstallTime = orderBaseInfo.InstallTime;
            vo.PayMethod = ((PayMethodEnum)orderBaseInfo.PayMethod).GetEnumDescription();
            vo.MoneyArriveStatus = ((MoneyArriveStatusEnum)orderBaseInfo.MoneyArriveStatus).GetEnumDescription();
            //vo.OrderType = ((OrderTypeEnum)orderBaseInfo.OrderType).GetEnumDescription();
            //显示订单分类更好
            vo.OrderType = ((ProductTypeEnum)orderBaseInfo.ProduceType).GetDescription();
            //if (orderAmountInfo != null)
            //{
            //    vo.SettlementFee = orderAmountInfo.SettlementFee;
            //}
            await accountCheckRepository.CreateAccountCheckException(vo);
            if (request.RelationType == AccountCheckRelationTypeEnum.总部.ToString())
            {
                var orderNos = new List<string> { request.RelationNo };
                //如果总部核对异常了 更新标识
                await accountCheckRepository.RgAccountCheckException(new RgAccountCheckConfirmRequest
                {
                    OrderNos = orderNos,
                    RgCheckResult = AccountCheckResultEnum.核对异常.ToString(),
                    UpdateBy = request.CreateBy
                });
            }
            else if (request.RelationType == AccountCheckRelationTypeEnum.门店.ToString())
            {
                await accountCheckRepository.ShopAccountCheckException(new ShopAccountCheckConfirmRequest
                {
                    OrderNo = request.RelationNo,
                    ShopCheckResult = AccountCheckResultEnum.核对异常.ToString(),
                    UpdateBy = request.CreateBy
                });
            }
            var users = request.CreateBy.Split('@');
            var user = users.Length > 1 ? users[1] : users[0];
            await orderClient.UpdateOrderStatus(new UpdateOrderStatusClientRequest
            {
                OrderNos = new List<string> { request.RelationNo },
                UpdateBy = user,
                UpdateStatusType = Client.Request.UpdateOrderStatusTypeEnum.ExceptionReconciliation
            });

            var accountCheckDo = await accountCheckRepository.GetAccountCheckDetail(new GetAccountCheckDetailRequest
            {
                OrderNo = request.RelationNo
            });

            //记录日志
            await accountCheckRepository.CreateAccountCheckLog(new AccountCheckLogDO
            {
                RelationNo = request.RelationNo,
                RelationType = request.RelationType,
                LocationId = request.LocationId,
                BeforeValue = accountCheckDo != null ? JsonConvert.SerializeObject(accountCheckDo) : string.Empty,
                Remark = $"{request.RelationType}核对门店【{request.LocationName}的订单【{request.RelationNo}】异常",
                CreateBy = request.CreateBy,
                CreateTime = DateTime.Now
            });
            return 1;
        }

        public async Task<int> CreateAccountCheckLog(CreateAccountCheckLogRequest request)
        {
            var vo = mapper.Map<AccountCheckLogDO>(request);
            await accountCheckRepository.CreateAccountCheckLog(vo);
            return 1;
        }

        public async Task<List<AccountCheckLogDTO>> GetAccountCheckLogs(GetAccountCheckLogRequest request)
        {
            var result = await accountCheckRepository.GetAccountCheckLogs(new AccountCheckLogDO
            {
                RelationNo = request.RelationNo,
                RelationType = request.RelationType
            });
            var vo = mapper.Map<List<AccountCheckLogDTO>>(result);

            vo.ForEach(r =>
            {
                var users = r.CreateBy.Split('@');
                r.CreateBy = users.Length > 1 ? users[1] : users[0];
            });

            return vo;
        }

        /// <summary>
        /// 只查询门店已核对的数据 总部未核对数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<AccountCheckDTO>> GeShoptAccountChecks(GetAccountCheckRequest request)
        {
            request.ShopCheckResult = AccountCheckResultEnum.已核对.ToString();

            var result = await accountCheckRepository.GetShopAccountChecks(request);
            foreach (var item in result.Items)
            {
                if (!string.IsNullOrWhiteSpace(item.ShopCheckResult))
                {
                    item.ShopCheckResult = $"{item.ShopCheckResult}({item.ShopCheckTime.ToShortDateString()})";
                }
                var users = item.CreateBy.Split('@');
                item.CreateBy = users.Length > 1 ? users[1] : users[0];
            }
            ApiPagedResultData<AccountCheckDTO> response = mapper.Map<ApiPagedResultData<AccountCheckDTO>>(result);


            return response;
        }

        /// <summary>
        /// 只查询门店已核对的数据 总部未核对数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<AccountCheckDTO>> GeShoptAccountChecksList(GetAccountCheckRequest request)
        {
            request.ShopCheckResult = AccountCheckResultEnum.已核对.ToString();

            var result = await accountCheckRepository.GetShopAccountChecksList(request);
            foreach (var item in result.Items)
            {
                if (!string.IsNullOrWhiteSpace(item.ShopCheckResult))
                {
                    item.ShopCheckResult = $"{item.ShopCheckResult}({item.ShopCheckTime.ToShortDateString()})";
                }
                var users = item.CreateBy.Split('@');
                item.CreateBy = users.Length > 1 ? users[1] : users[0];
            }
            ApiPagedResultData<AccountCheckDTO> response = mapper.Map<ApiPagedResultData<AccountCheckDTO>>(result);


            return response;
        }

        /// <summary>
        /// 门店未对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<AccountCheckDTO>> GetShopAccountUnChecks(GetAccountCheckRequest request)
        {
            var dateTime = new DateTime(2020, 9, 8);
            var result = await orderClient.GetOrderBaseInfoForBusinessStatus(new GetOrderBaseInfoForBusinessStatusRequest
            {
                OrderBusinessStatus = OrderBusinessStatusEnum.WaitingReconciliation,
                EndInstallTime = request.EndTime.Subtract(dateTime).TotalDays > 0 ? request.EndTime.ToString() : string.Empty,
                StarInstallTime = request.StartTime.Subtract(dateTime).TotalDays > 0 ? request.StartTime.ToString() : string.Empty,
                OrderNo = request.OrderNo,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ShopId = request.LocationId,
                OtherSqlWhere = ""  // produce_type <> 13 
            });

            var accountList = new List<AccountCheckDTO>();
            ApiPagedResultData<AccountCheckDTO> apiPagedResult = new ApiPagedResultData<AccountCheckDTO>();

            if (result.Items != null && result.Items.Any())
            {
                var orderNos = result.Items.Select(r => r.OrderNo).Distinct().ToList();

                //根据门店Id获取门店名称
                var shopListResponse = await shopManageClient.GetShopListAsync(new GetShopListClientRequest
                {
                    ShopIds = result.Items.Select(r => r.ShopId).Distinct().ToList()
                });

                var getReconciliationFees = await _reconciliationFeeRepository.GetReconciliationFees(new GetReconciliationFeesRequest()
                {
                    OrderNos = result.Items?.Select(_ => _.OrderNo)?.ToList()
                });

                if (shopListResponse.Data != null && shopListResponse.Data.Items != null)
                {
                    var shopList = shopListResponse.Data.Items;

                    //var orderAmountResponse = await orderClient.GetAccountCheckAmount(new GetAccountCheckAmountClientRequest
                    //{
                    //    OrderNos = orderNos
                    //});

                    // var orderAmountDic = orderAmountResponse.Data?.ToDictionary(r => r.OrderNo, r => r) ?? new Dictionary<string, GetAccountCheckAmountClientDTO>();



                    var shopDic = shopList.ToDictionary(r => r.Id, r => r.SimpleName);

                    foreach (var item in result.Items)
                    {
                        GetAccountCheckAmountClientDTO orderAmount = new GetAccountCheckAmountClientDTO();
                        //if (orderAmountDic.ContainsKey(item.OrderNo))
                        //{
                        //    orderAmount = orderAmountDic[item.OrderNo];
                        //}

                        var reconciliationFee = getReconciliationFees?.Where(_ => _.OrderNo == item.OrderNo)
                            ?.FirstOrDefault();

                        var accountCheckDTO = new AccountCheckDTO
                        {
                            OrderNo = item.OrderNo,
                            InstallTime = item.InstallTime,
                            LocationId = item.ShopId,
                            LocationName = shopDic.ContainsKey(item.ShopId) ? shopDic[item.ShopId] : string.Empty,
                            PayMethod = ((PayMethodEnum)item.PayMethod).GetEnumDescription(),
                            MoneyArriveStatus = ((MoneyArriveStatusEnum)item.MoneyArriveStatus).GetEnumDescription(),
                            //OrderType = ((OrderTypeEnum)item.OrderType).GetEnumDescription(),
                            //显示订单分类更好
                            OrderType = ((ProductTypeEnum)item.ProduceType).GetDescription(),
                        SaleTotalAmount = orderAmount.ActualAmount,
                            //TotalCost = orderAmount.TotalCost,
                            //ServiceFee = orderAmount.ServiceFee,
                            //OtherFee = orderAmount.OtherFee,
                            //CommissionFee = orderAmount.CommissionFee,
                            //SettlementFee = orderAmount.SettlementFee

                            ShopInstallAmount = reconciliationFee?.ShopInstallAmount ?? 0,
                            ActualAmount = reconciliationFee?.ActualAmount ?? 0,
                            ShopDistributionCost = reconciliationFee?.ShopDistributionCost ?? 0,
                            ShopDistributionGrossProfit = reconciliationFee?.ShopDistributionGrossProfit ?? 0,
                            ShopDifferencePrice = reconciliationFee?.ShopDifferencePrice ?? 0,
                            ShopCommissionFee = reconciliationFee?.ShopCommissionFee ?? 0,
                            ShopSettlementAmount = reconciliationFee?.ShopSettlementAmount ?? 0,
                            ShopOtherFee = reconciliationFee?.ShopOtherFee ?? 0,
                            ShopItemFee = reconciliationFee?.ShopItemFee ?? 0,
                            ShopOutProductCost= reconciliationFee?.ShopItemCost??0,
                            CompanyBackAmount = reconciliationFee?.CompanyBackAmount ?? 0,
                            CompanyCommissionFee = reconciliationFee?.CompanyCommissionFee ?? 0,
                            CompanySettlementAmount = reconciliationFee?.CompanySettlementAmount ?? 0,
                            CompanyId = reconciliationFee?.CompanyId ?? 0,
                            CompanyName = reconciliationFee?.CompanyName,
                        };
                        accountList.Add(accountCheckDTO);
                    }
                }

                apiPagedResult.Items = (ICollection<AccountCheckDTO>)accountList;
                apiPagedResult.TotalItems = result.TotalItems;
            }
            else
            {
                apiPagedResult.Items = new List<AccountCheckDTO>();
                apiPagedResult.TotalItems = 0;
            }
            return apiPagedResult;
        }

        /// <summary>
        /// 总部已对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<AccountCheckDTO>> GeRgAccountChecks(GetAccountCheckRequest request)
        {
            request.ShopCheckResult = AccountCheckResultEnum.已核对.ToString();
            request.RgCheckResult = AccountCheckResultEnum.已核对.ToString();
            var result = await accountCheckRepository.GetAccountChecks(request);
            foreach (var item in result.Items)
            {
                if (!string.IsNullOrWhiteSpace(item.ShopCheckResult))
                {
                    item.ShopCheckResult = $"{item.ShopCheckResult}({item.ShopCheckTime.ToShortDateString()})";
                }
                if (!string.IsNullOrWhiteSpace(item.RgCheckResult))
                {
                    item.RgCheckResult = $"{item.RgCheckResult}({item.RgCheckTime.ToShortDateString()})";
                }
            }
            ApiPagedResultData<AccountCheckDTO> response = mapper.Map<ApiPagedResultData<AccountCheckDTO>>(result);
            return response;
        }

        /// <summary>
        /// 总部确认了对账单未“未申请”
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UpdateRgAccountCheckResult(RgAccountCheckConfirmRequest request)
        {
            if (request.OrderNos.Any())
            {
                request.OrderNos = request.OrderNos.Distinct().ToList();
                await accountCheckRepository.RgAccountCheckConfirm(new RgAccountCheckConfirmRequest
                {
                    OrderNos = request.OrderNos,
                    RgCheckResult = request.RgCheckResult,
                    UpdateBy = request.UpdateBy,
                    WithdrawStatus = AccountCheckWithdrawStatus.申请成功.ToString()
                });

                //记录日志
                foreach (var item in request.OrderNos)
                {
                    await accountCheckRepository.CreateAccountCheckLog(new AccountCheckLogDO
                    {
                        RelationNo = item,
                        RelationType = AccountCheckRelationTypeEnum.总部.ToString(),
                        LocationId = request.LocationId,
                        Remark = $"总部核对订单【{item}】结果【{request.RgCheckResult}】",
                        CreateBy = request.UpdateBy,
                        CreateTime = DateTime.Now
                    });

                }

                var users = request.UpdateBy.Split('@');
                var user = users.Length > 1 ? users[1] : users[0];
                await orderClient.UpdateOrderStatus(new UpdateOrderStatusClientRequest
                {
                    OrderNos = request.OrderNos,
                    UpdateBy = user,
                    UpdateStatusType = Client.Request.UpdateOrderStatusTypeEnum.HaveReconciliation
                });
            }
            return 1;
        }

        /// <summary>
        /// 对账异常列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<AccountCheckExceptionCollectDTO>> GetAccountCheckExceptionCollectList(GetAccountCheckRequest request)
        {
            var result = await accountCheckRepository.GetAccountCheckExceptionCollectList(request);
            //有一部分数据还没有进到对账池

            var orderNos = result.Items.Where(r => string.IsNullOrEmpty(r.OrderNo)).Select(r => r.RelationNo).ToList();

            //var orderAmountDic = new Dictionary<string, GetAccountCheckAmountClientDTO>();
            //if (orderNos != null && orderNos.Any())
            //{
            //    var orderAmountResponse = await orderClient.GetAccountCheckAmount(new GetAccountCheckAmountClientRequest
            //    {
            //        OrderNos = orderNos
            //    });

            //    if (orderAmountResponse.Data != null && orderAmountResponse.Code == ResultCode.Success)
            //    {
            //        orderAmountDic = orderAmountResponse.Data.ToDictionary(r => r.OrderNo, r => r);
            //    }
            //}

            var getReconciliationFees = await _reconciliationFeeRepository.GetReconciliationFees(new GetReconciliationFeesRequest()
            {
                OrderNos = result.Items?.Select(_ => _.OrderNo)?.ToList()
            });

            foreach (var item in result.Items)
            {
                var reconciliationFee =
                    getReconciliationFees?.Where(_ => _.OrderNo == item.RelationNo)?.FirstOrDefault();
                //if (orderAmountDic.ContainsKey(item.RelationNo))
                //{
                //var orderAmount = orderAmountDic[item.RelationNo];
                //item.SaleTotalAmount = orderAmount.ActualAmount;
                //item.TotalCost = orderAmount.TotalCost;
                //item.ServiceFee = orderAmount.ServiceFee;
                //item.OtherFee = orderAmount.OtherFee;
                //item.CommissionFee = orderAmount.CommissionFee;
                //item.SettlementFee = orderAmount.SettlementFee;
                //}
                item.SaleTotalAmount = reconciliationFee?.ActualAmount??0;
                item.ShopInstallAmount = reconciliationFee?.ShopInstallAmount ?? 0;
                item.ActualAmount = reconciliationFee?.ActualAmount ?? 0;
                item.ShopDistributionCost = reconciliationFee?.ShopDistributionCost ?? 0;
                item.ShopDistributionGrossProfit = reconciliationFee?.ShopDistributionGrossProfit ?? 0;
                item.ShopDifferencePrice = reconciliationFee?.ShopDifferencePrice ?? 0;
                item.ShopCommissionFee = reconciliationFee?.ShopCommissionFee ?? 0;
                item.ShopSettlementAmount = reconciliationFee?.ShopSettlementAmount ?? 0;
                item.ShopOtherFee = reconciliationFee?.ShopOtherFee ?? 0;
                item.ShopItemFee = reconciliationFee?.ShopItemFee ?? 0;
                item.CompanyBackAmount = reconciliationFee?.CompanyBackAmount ?? 0;
                item.CompanyCommissionFee = reconciliationFee?.CompanyCommissionFee ?? 0;
                item.CompanySettlementAmount = reconciliationFee?.CompanySettlementAmount ?? 0;
                item.CompanyId = reconciliationFee?.CompanyId ?? 0;
                item.CompanyName = reconciliationFee?.CompanyName;


                if (!string.IsNullOrWhiteSpace(item.ReportBy))
                {
                    var users = item.ReportBy.Split('@');
                    var user = users.Length > 1 ? users[1] : users[0];

                    item.ReportBy = $"{item.RelationType}({user}-{item.ReportTime.ToString("yyyy-MM-dd")})";
                }
                item.ReportReason = $"{item.ReportType}-{item.ReportReason}";
                item.OrderNo = item.RelationNo;
            }
            var response = mapper.Map<ApiPagedResultData<AccountCheckExceptionCollectDTO>>(result);
            return response;
        }

        /// <summary>
        /// 对账汇总记录(工场店）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<AccountCheckCollectDTO>> GetAccountCheckCollects(AccountCheckCollectRequest request)
        {
            var res = new ApiPagedResult<AccountCheckCollectDTO>();
            var result = new List<AccountCheckCollectDTO>();

            var shopIds = new List<long>();
            int totalNum = 0;

            var getShopListClientRequest = new GetShopListClientRequest();

            getShopListClientRequest = new GetShopListClientRequest
            {
                CityId = request.CityId,
                DistrictId = request.DistrictId,
                ShopTypes = new List<int> { 2,4 },
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ProvinceId = request.ProvinceId
            };

            if (request.ShopId > 0)
            {
                getShopListClientRequest.ShopIds = new List<long> { request.ShopId };
            }


            var shopResponse = await shopManageClient.GetShopListAsync(getShopListClientRequest);
            if (shopResponse.Code != ResultCode.Success)
            {
                res.Code = ResultCode.Failed;
                res.Message = "获取门店信息异常!";
                return res;
            }

            var shopDic = new Dictionary<long, ShopSimpleInfoClientDTO>();

            shopIds = shopResponse.Data.Items.Select(r => r.Id).Distinct().ToList();
            shopDic = shopResponse.Data.Items.ToDictionary(r => r.Id, r => r);
            totalNum = shopResponse.Data.TotalItems;

            if (!shopIds.Any())
            {
                res.Code = ResultCode.Success;
                res.Data = new ApiPagedResultData<AccountCheckCollectDTO>
                {
                    Items = new List<AccountCheckCollectDTO>(),
                    TotalItems = 0
                };
                return res;
            }

            //从订单中 获取未对账数量和金额
            var orderAmountResposne = await orderClient.GetNoReconciliationAmount(new GetNoReconciliationAmountClientRequest
            {
                ShopIds = shopIds
            });

            var orderAmountDic = orderAmountResposne.Data.ToDictionary(r => r.ShopId, r => r);

            //1.已对账应结算-金额 2.门店已对账 3.总部已核对  4门店已对账-金额
            var accountedSettlementRes = await accountCheckRepository.GetAccountCheckCountByStatus(shopIds, 1);
            var accountedSettlementDic = accountedSettlementRes.ToDictionary(r => r.ShopId, r => r);

            var shopAccountNumRes = await accountCheckRepository.GetAccountCheckCountByStatus(shopIds, 2);
            var shopAccountNumDic = shopAccountNumRes.ToDictionary(r => r.ShopId, r => r);

            var rgAccountNumRes = await accountCheckRepository.GetAccountCheckCountByStatus(shopIds, 3);
            var rgAccountNumDic = rgAccountNumRes.ToDictionary(r => r.ShopId, r => r);

            var accountErrorNumRes = await accountCheckRepository.GetAccountCheckExceptionCount(shopIds);
            var accountErrorNumDic = accountErrorNumRes.ToDictionary(r => r.ShopId, r => r);

            var shopAccountMoneyRes = await accountCheckRepository.GetAccountCheckCountByStatus(shopIds, 4);
            var shopAccountMoneyDIc = shopAccountMoneyRes.ToDictionary(r => r.ShopId, r => r);

            int i = 1;
            foreach (var item in shopIds)
            {
                var accountCheckResult = new AccountCheckCollectDTO
                {
                    AccountedSettlement = accountedSettlementDic.ContainsKey(item) ? accountedSettlementDic[item].AccountedSettlement : 0,
                    AccountErrorNum = accountErrorNumDic.ContainsKey(item) ? accountErrorNumDic[item].AccountErrorNum : 0,
                    No = i.ToString(),
                    RgAccountNum = rgAccountNumDic.ContainsKey(item) ? rgAccountNumDic[item].RgAccountNum : 0,
                    ShopAccountNum = shopAccountNumDic.ContainsKey(item) ? shopAccountNumDic[item].ShopAccountNum : 0,
                    ShopId = item,
                    ShopName = shopDic.ContainsKey(item) ? shopDic[item].SimpleName : string.Empty,
                    ShopUnAccountNum = orderAmountDic.ContainsKey(item) ? orderAmountDic[item].Num : 0,
                    UnAccountedSettlement = orderAmountDic.ContainsKey(item) ? orderAmountDic[item].SettlementFee : 0,
                };
                accountCheckResult.UnAccountedSettlement += shopAccountMoneyDIc.ContainsKey(item) ? shopAccountMoneyDIc[item].ShopAccountMoney : 0;
                accountCheckResult.UnAccountedSettlement += accountErrorNumDic.ContainsKey(item) ? accountErrorNumDic[item].AccountExceptionMoney : 0;


                accountCheckResult.Subtotal = accountCheckResult.AccountedSettlement + accountCheckResult.UnAccountedSettlement;
                result.Add(accountCheckResult);
                i++;
            }

            var pageData = new ApiPagedResultData<AccountCheckCollectDTO>()
            {
                Items = result,
                TotalItems = totalNum
            };
            res.Data = pageData;
            res.Message = "查询成功";
            res.Code = ResultCode.Success;

            return res;
        }

        /// <summary>
        /// 财务申请提现
        /// </summary>
        /// <param name="requeset"></param>
        /// <returns></returns>
        public async Task<int> RgAccountCheckWithdraw(RgAccountCheckWithdrawRequeset request)
        {
            request.WithdrawStatus = AccountCheckWithdrawStatus.已申请.ToString();

            try
            {
                await accountCheckRepository.RgAccountCheckWithdraw(request);
                if (request.OrderNos.Any())
                {
                    foreach (var item in request.OrderNos)
                    {
                        await accountCheckRepository.CreateAccountCheckLog(new AccountCheckLogDO
                        {
                            RelationNo = item,
                            RelationType = AccountCheckRelationTypeEnum.门店.ToString(),
                            LocationId = request.LocationId,
                            Remark = $"门店【{request.LocationName}】的订单【{item}】申请提现!",
                            CreateBy = request.UpdateBy,
                            CreateTime = DateTime.Now
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }


            return 1;
        }

        /// <summary>
        /// 对账异常处理
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> AccountCheckExceptionHandle(AccountCheckExceptionHandleRequest request)
        {
            try
            {
                //1.对账异常表 的状态更新 2.新增日志  3.对账表的状态更新(下次再对账，原基础上更新) 4.通知订单
                request.Status = AccountCheckExceptionStatus.已处理.ToString();
                var res = await accountCheckRepository.AccountCheckExceptionHandle(request);

                var accountCheckDo = await accountCheckRepository.GetAccountCheckDetail(new GetAccountCheckDetailRequest
                {
                    OrderNo = request.OrderNo
                });
                if (accountCheckDo != null)
                {
                    await accountCheckRepository.CreateAccountCheckLog(new AccountCheckLogDO
                    {
                        RelationNo = request.OrderNo,
                        RelationType = AccountCheckRelationTypeEnum.门店.ToString(),
                        LocationId = request.LocationId,
                        BeforeValue = JsonConvert.SerializeObject(accountCheckDo),
                        Remark = $"门店【{accountCheckDo.LocationName}】的异常订单【{request.OrderNo}】已处理,处理意见:【{request.Suggestion}】",
                        CreateBy = request.UpdateBy,
                        CreateTime = DateTime.Now
                    });
                }
                else
                {

                    var orderBaseInfo = await orderClient.GetOrderByNo(new GetOrderByNoClientRequest
                    {
                        OrderNo = request.OrderNo
                    });
                    long locationId = 0;
                    if (orderBaseInfo.Code == ResultCode.Success)
                    {
                        locationId = orderBaseInfo.Data.ShopId;
                    }

                    var shopResponse = await shopManageClient.GetShopListAsync(new GetShopListClientRequest
                    {
                        ShopIds = new List<long> { locationId }
                    });

                    if (shopResponse.Code == ResultCode.Success)
                    {
                        if (shopResponse.Data.Items != null && shopResponse.Data.Items.Any())
                        {
                            await accountCheckRepository.CreateAccountCheckLog(new AccountCheckLogDO
                            {
                                RelationNo = request.OrderNo,
                                RelationType = AccountCheckRelationTypeEnum.门店.ToString(),
                                LocationId = locationId,
                                Remark = $"门店【{shopResponse.Data.Items.FirstOrDefault()?.SimpleName}】的异常订单【{request.OrderNo}】已处理,处理意见:【{request.Suggestion}】",
                                CreateBy = request.UpdateBy,
                                CreateTime = DateTime.Now
                            });
                        }
                    }
                }

                var users = request.UpdateBy.Split('@');
                var user = users.Length > 1 ? users[1] : users[0];
                //long orderId = Convert.ToInt64(request.OrderNo.Substring(3, request.OrderNo.Length - 3));
                await orderClient.UpdateOrderStatus(new UpdateOrderStatusClientRequest
                {
                    OrderNos = new List<string> { request.OrderNo },
                    UpdateBy = user,
                    UpdateStatusType = Client.Request.UpdateOrderStatusTypeEnum.NoReconciliation
                });

            }
            catch (Exception ex)
            {

                throw;
            }
            return 1;
        }

        /// <summary>
        /// 对账详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AccountCheckDTO> GetAccountCheckDetail(GetAccountCheckDetailRequest request)
        {
            var result = await accountCheckRepository.GetAccountCheckDetail(request);

            var vo = mapper.Map<AccountCheckDTO>(result);
            return vo;

        }

        /// <summary>
        /// 对账结算
        /// </summary>
        /// <param name="requeset"></param>
        /// <returns></returns>
        public async Task<int> RgAccountCheckSettlement(RgAccountCheckWithdrawRequeset request)
        {
            request.WithdrawStatus = AccountCheckWithdrawStatus.已结算.ToString();

            try
            {
                await accountCheckRepository.RgAccountCheckSettlement(request);
                if (request.OrderNos.Any())
                {
                    var accountCheckDTO = await accountCheckRepository.GetAccountCheckDetail(new GetAccountCheckDetailRequest
                    {
                        OrderNo = request.OrderNos.First()
                    });
                    foreach (var item in request.OrderNos)
                    {
                        await accountCheckRepository.CreateAccountCheckLog(new AccountCheckLogDO
                        {
                            RelationNo = item,
                            RelationType = AccountCheckRelationTypeEnum.门店.ToString(),
                            LocationId = accountCheckDTO.LocationId,
                            Remark = $"门店【{accountCheckDTO.LocationName}】的订单【{item}】已结算!",
                            CreateBy = request.UpdateBy,
                            CreateTime = DateTime.Now
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"RgAccountCheckSettlement_Error", ex);
            }

            return 1;
        }

        /// <summary>
        /// 系统批量将已申请的对账单更新为申请成功，推送到结算池
        /// </summary>
        /// <param name="hour"></param>
        /// <returns></returns>
        public async Task<string> BatchHandelAccountCheckData(int hour)
        {
            var result = await accountCheckRepository.BatchHandelAccountCheckData(hour);
            return result;
        }

        public async Task<ApiPagedResultData<AccountCheckExceptionCollectDTO>> GetAccountCheckExceptionMonitorList(GetAccountCheckRequest request)
        {
            var result = await accountCheckRepository.GetAccountCheckExceptionMonitorList(request);
            //有一部分数据还没有进到对账池

            var orderNos = result.Items.Where(r => string.IsNullOrEmpty(r.OrderNo)).Select(r => r.RelationNo).ToList();

            //var orderAmountDic = new Dictionary<string, GetAccountCheckAmountClientDTO>();
            //if (orderNos != null && orderNos.Any())
            //{
            //    var orderAmountResponse = await orderClient.GetAccountCheckAmount(new GetAccountCheckAmountClientRequest
            //    {
            //        OrderNos = orderNos
            //    });

            //    if (orderAmountResponse.Data != null && orderAmountResponse.Code == ResultCode.Success)
            //    {
            //        orderAmountDic = orderAmountResponse.Data.ToDictionary(r => r.OrderNo, r => r);
            //    }
            //}

            var getReconciliationFees = await _reconciliationFeeRepository.GetReconciliationFees(new GetReconciliationFeesRequest()
            {
                OrderNos = result.Items?.Select(_ => _.OrderNo)?.ToList()
            });
            int no = 1;
            foreach (var item in result.Items)
            {
                var reconciliationFee = getReconciliationFees?.Where(_ => _.OrderNo == item.RelationNo)?.FirstOrDefault();
                //    getReconciliationFees?.Where(_ => _.OrderNo == item.RelationNo)?.FirstOrDefault();
                //if (orderAmountDic.ContainsKey(item.RelationNo))
                //{
                // var orderAmount = orderAmountDic[item.RelationNo];
                item.SaleTotalAmount = reconciliationFee?.ActualAmount ?? 0;
                //item.TotalCost = orderAmount.TotalCost;
                //item.ServiceFee = orderAmount.ServiceFee;
                //item.OtherFee = orderAmount.OtherFee;
                //item.CommissionFee = orderAmount.CommissionFee;
                //item.SettlementFee = orderAmount.SettlementFee;
                // }
                item.ShopInstallAmount = reconciliationFee?.ShopInstallAmount ?? 0;
                item.ActualAmount = reconciliationFee?.ActualAmount ?? 0;
                item.ShopDistributionCost = reconciliationFee?.ShopDistributionCost ?? 0;
                item.ShopDistributionGrossProfit = reconciliationFee?.ShopDistributionGrossProfit ?? 0;
                item.ShopDifferencePrice = reconciliationFee?.ShopDifferencePrice ?? 0;
                item.ShopCommissionFee = reconciliationFee?.ShopCommissionFee ?? 0;
                item.ShopSettlementAmount = reconciliationFee?.ShopSettlementAmount ?? 0;
                item.ShopOtherFee = reconciliationFee?.ShopOtherFee ?? 0;
                item.ShopItemFee = reconciliationFee?.ShopItemFee ?? 0;
                item.CompanyBackAmount = reconciliationFee?.CompanyBackAmount ?? 0;
                item.CompanyCommissionFee = reconciliationFee?.CompanyCommissionFee ?? 0;
                item.CompanySettlementAmount = reconciliationFee?.CompanySettlementAmount ?? 0;
                item.CompanyId = reconciliationFee?.CompanyId ?? 0;
                item.CompanyName = reconciliationFee?.CompanyName;

                if (!string.IsNullOrWhiteSpace(item.ReportBy))
                {
                    var users = item.ReportBy.Split('@');
                    var user = users.Length > 1 ? users[1] : users[0];
                    item.ReportBy = $"{item.RelationType}({user}-{item.ReportTime.ToString("yyyy-MM-dd")})";
                }
                item.ReportReason = $"{item.ReportType}-{item.ReportReason}";
                item.OrderNo = item.RelationNo;
                item.No = no.ToString();
                no++;
            }
            var response = mapper.Map<ApiPagedResultData<AccountCheckExceptionCollectDTO>>(result);
            return response;
        }

        /// <summary>
        /// 计算门店对账公司对账金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> CalculationReconciliationFee(CalculationReconciliationFeeRequest request)
        {
            var res = new ApiResult();
            try
            {
                ReconciliationFeeDO reconciliationFeeDo = new ReconciliationFeeDO();

                var reconciliationFee = await _reconciliationFeeRepository.GetListAsync(" where order_no=@OrderNo and is_deleted=0", new { OrderNo = request.OrderNo });

                if (reconciliationFee?.Count() > 0)
                {
                    return Result.Failed("已生成对账记录，不允许再次生成");
                }

                var orders = await orderClient.GetOrderBaseInfo(new GetOrderBaseInfoClientRequest()
                {
                    OrderNos = new List<string>(1) { request.OrderNo }
                });

                var order = orders?.Data?.FirstOrDefault();

                if (orders?.Data?.Count > 0 && IsNeedReconciliation(order))
                {
                    var orderProducts = await orderClient.GetOrderProduct(new GetOrderProductRequest()
                    {
                        OrderNos = new List<string>(1) { request.OrderNo }
                    });

                    reconciliationFeeDo.ShopId = order?.ShopId ?? 0;
                    reconciliationFeeDo.OrderNo = order?.OrderNo;
                    reconciliationFeeDo.OrderType = order?.OrderType ?? 0;
                    reconciliationFeeDo.ProduceType = order?.ProduceType ?? 0;
                    reconciliationFeeDo.ActualAmount = order?.ActualAmount ?? 0;
                    var getShops = await shopManageClient.GetShopById(new GetShopRequest()
                    {
                        ShopId = order?.ShopId ?? 0
                    });
                    //logger.Info("CalculationReconciliationFee 1-" + JsonConvert.SerializeObject(reconciliationFeeDo));

                    reconciliationFeeDo.ShopName = getShops?.Data?.SimpleName;
                    reconciliationFeeDo.ProductDetail = string.Join(",", orderProducts?.Data.Select(_ => _.ProductName + "(" + _.TotalNumber.ToString() + "*" + _.Price.ToString() + "="
                        + _.TotalAmount.ToString() + ")")?.ToList());

                    //s02:2021-11-25  先不计算公司对账
                    reconciliationFeeDo.CompanyId = 0;
                    //if ((getShops?.Data?.CompanyId ?? 0) > 0)
                    //{
                    //    var company = await shopManageClient.GetCompanyInfoById(new GetCompanyInfoByIdRequest()
                    //    {
                    //        Id = getShops?.Data?.CompanyId ?? 0
                    //    });
                    //    reconciliationFeeDo.CompanyId = company?.Data?.Id ?? 0;
                    //    reconciliationFeeDo.CompanyName = company?.Data?.Name;
                    //}

                    //对账单类型
                    reconciliationFeeDo.AccountType = AccountTypeEnum.Shop.ToString();
                    if (order?.TotalCouponAmount > 0)
                    {
                        var orderCoupons = await orderClient.GetOrderCoupons(new GetOrderCouponsRequest()
                        {
                            OrderIds = new List<long>(1) { order?.Id ?? 0 }
                        });
                        if (orderCoupons?.Data?.Where(_ => _.Category == 1)?.Any() ?? false)
                        {
                            reconciliationFeeDo.AccountType = AccountTypeEnum.ApolloErp.ToString();
                        }
                    }

                    //s02:2022-05-07  add  订单总价（实付金额或套餐卡、核销码等的核销金额）
                    if (order.ProduceType == ProductTypeEnum.UsePackageCard.ToInt())
                    {
                        var orderPackageCards = await orderClient.GetOrderPackageCard(new GetOrderPackageCardRequest()
                        {
                            VerifyOrderNos = new List<string>(1) { order.OrderNo }
                        });
                        reconciliationFeeDo.SaleTotalAmount = orderPackageCards?.Data?.Sum(_ => _.AvgPrice * _.Num) ?? 0;
                    }
                    else if (order.ProduceType == ProductTypeEnum.UseVerticationCode.ToInt())
                    {
                        reconciliationFeeDo.SaleTotalAmount = order?.TotalCouponAmount ?? 0;
                    }
                    else
                    {
                        reconciliationFeeDo.SaleTotalAmount = reconciliationFeeDo.ActualAmount;
                    }

                    //取得实物产品列表
                    var realProductCodes = orderProducts?.Data
                        ?.Where(_ => _.ProductAttribute == ProductAttributeEnum.RealProduct.ToInt())?.ToList();
                        //?.Where(_ => _.ProductAttribute == ProductAttributeEnum.RealProduct.ToInt())?.Select(_ => _.ProductId)?.ToList();

                    //取得外采产品列表
                    var shopProductCodes = orderProducts?.Data
                        ?.Where(_ => _.ProductAttribute == ProductAttributeEnum.ItemProduct.ToInt() || _.ProductAttribute == ProductAttributeEnum.ShopPurchase.ToInt())?.ToList();

                    var products = default(ApiResult<List<ProductDetailDTO>>);
                    if (realProductCodes?.Count > 0)
                    {
                        products = await productClient.GetProductsByProductCodes(new ProductDetailSearchRequest()
                        {
                            ProductCodes = realProductCodes?.Select(_ => _.ProductId)?.ToList()
                        });
                    }

                    var shopProducts = default(ApiResult<List<ShopProductDto>>);
                    if (shopProductCodes?.Count > 0)
                    {
                        shopProducts = await productClient.GetShopProductByCodes(new ShopProductDetialRequest()
                        {
                            ShopProductCodes = shopProductCodes?.Select(_ => _.ProductId)?.ToList()
                        });
                    }

                    //手续费率
                    decimal.TryParse(configuration["WithdrawalRate"], out var withdrawRate);

                    //logger.Info("CalculationReconciliationFee 2");

                    if (!IsNeedShopReconciliation(order))
                    {
                        reconciliationFeeDo.ShopInstallAmount = 0;
                        reconciliationFeeDo.ShopDistributionCost = 0;
                        reconciliationFeeDo.ShopDistributionGrossProfit = 0;
                        reconciliationFeeDo.ShopDifferencePrice = 0;
                        reconciliationFeeDo.ShopCommissionFee = 0;
                        reconciliationFeeDo.ShopSettlementAmount = 0;
                        reconciliationFeeDo.ShopOtherFee = 0;
                        reconciliationFeeDo.ShopItemFee = 0;
                    }
                    else
                    {
                        var reconciliationRule = GetReconciliationRule(order);

                        //正常门店订单
                        if (reconciliationRule == ReconciliationRuleEnum.OrdinaryOrder)
                        {
                            //安装费
                            reconciliationFeeDo.ShopInstallAmount = orderProducts?.Data?.Where(_ => _.ProductAttribute == ProductAttributeEnum.ServiceProduct.ToInt()).Sum(_ => _.TotalCostPrice) ?? 0;

                            //铺货成本
                            reconciliationFeeDo.ShopDistributionCost = 0;
                            if (realProductCodes?.Count > 0)
                            {
                                realProductCodes?.ForEach(_ =>
                                {

                                    //var totalNum = orderProducts?.Data?.Where(item => item.ProductId == _.Product.ProductCode)
                                    //      ?.FirstOrDefault()?.TotalNumber ?? 0;
                                    var realItem = products?.Data?.Where(item => item.Product.ProductCode == _.ProductId)
                                          ?.FirstOrDefault();

                                    reconciliationFeeDo.ShopDistributionCost += _.TotalNumber * ((realItem.Product?.SettlementPrice ?? 0) == 0 ? (realItem.Product?.SalesPrice ?? 0) : (realItem.Product?.SettlementPrice ?? 0));
                                });
                            }

                            //if (realProductCodes?.Count > 0)
                            //{
                            //    products?.Data?.ForEach(_ =>
                            //    {

                            //        var totalNum = orderProducts?.Data?.Where(item => item.ProductId == _.Product.ProductCode)
                            //              ?.FirstOrDefault()?.TotalNumber ?? 0;

                            //        reconciliationFeeDo.ShopDistributionCost += totalNum * ( (_.Product?.SettlementPrice ?? 0) == 0 ? (_.Product?.SalesPrice ?? 0) : (_.Product?.SettlementPrice ?? 0));
                            //    });
                            //}

                            //logger.Info("CalculationReconciliationFee 3");

                            //外采金额
                            ///合作店用门店项目  自营店用外采  所以只有一种类型
                            //reconciliationFeeDo.ShopItemFee = orderProducts?.Data?.Where(_ => _.ProductAttribute == ProductAttributeEnum.ItemProduct.ToInt() || _.ProductAttribute == ProductAttributeEnum.ShopPurchase.ToInt()).Sum(_ => _.TotalAmount) ?? 0;
                            reconciliationFeeDo.ShopItemFee = shopProductCodes?.Sum(_ => _.TotalAmount) ?? 0;

                            decimal sumPurchasePirce = 0m;

                            if (shopProductCodes?.Count > 0)
                            {
                                //var getOrderOccupyShopProduct = await _ApolloErpWMSClient.GetOrderOccupyShopProductPurchaseInfo(new GetOrderOccupyShopProductPurchaseInfoReqeust() { OrderNos = new List<string>(1) { order.OrderNo } });

                                shopProductCodes?.ForEach(_ =>
                                {
                                    var purchaseCost = shopProducts?.Data?.Where(item => item.ProductCode == _.ProductId && item.IsDeleted == 0)?.FirstOrDefault()?.PurchaseCost ?? 0;
                                    sumPurchasePirce += (purchaseCost > 0 ? purchaseCost : _.Price) * _.TotalNumber;
                                });
                            }
                            //外采成本
                            reconciliationFeeDo.ShopItemCost = sumPurchasePirce;
                            //门店扣其他
                            reconciliationFeeDo.ShopOtherFee = 0;
                            //铺货毛利 = 订单总价 - 安装费 - 铺货成本 - 门店外采金额，
                            reconciliationFeeDo.ShopDistributionGrossProfit =
                                reconciliationFeeDo.SaleTotalAmount - reconciliationFeeDo.ShopInstallAmount -
                                reconciliationFeeDo.ShopDistributionCost - reconciliationFeeDo.ShopItemFee;
                            //补差价
                            if (reconciliationFeeDo.AccountType == AccountTypeEnum.Shop.ToString())
                            {
                                reconciliationFeeDo.ShopDifferencePrice = 0;
                            }
                            else
                            { 
                                reconciliationFeeDo.ShopDifferencePrice = reconciliationFeeDo.ShopDistributionGrossProfit > 0
                                    ? 0
                                    : -1 * reconciliationFeeDo.ShopDistributionGrossProfit;
                            }

                            var shopCommissionFee = (reconciliationFeeDo.ShopInstallAmount + reconciliationFeeDo.ShopItemFee +
                                                   reconciliationFeeDo.ShopDistributionGrossProfit +
                                                   reconciliationFeeDo.ShopDifferencePrice - reconciliationFeeDo.ShopOtherFee);
                            //手续费
                            reconciliationFeeDo.ShopCommissionFee = shopCommissionFee < 0 ? 0 : Math.Ceiling((shopCommissionFee * withdrawRate) * 100) / 100;
                            //门店结算金额
                            reconciliationFeeDo.ShopSettlementAmount =
                                shopCommissionFee - reconciliationFeeDo.ShopCommissionFee;
                        }
                        else if (reconciliationRule == ReconciliationRuleEnum.CustomerToSmallWarehouseOrder)  //客户向小仓下单
                        {
                            if (realProductCodes?.Count > 0)
                            {
                                products?.Data?.ForEach(_ =>
                                {
                                    var totalNum = orderProducts?.Data?.Where(item => item.ProductId == _.Product.ProductCode)
                                          ?.FirstOrDefault()?.TotalNumber ?? 0;

                                    var orderProduct = orderProducts?.Data?.Where(item => item.ProductId == _.Product.ProductCode)?.FirstOrDefault();
                                    reconciliationFeeDo.ShopDistributionCost += totalNum * _.Product.WholesalePrice;
                                });
                            }

                            reconciliationFeeDo.ShopItemFee = orderProducts?.Data?.Where(_ => _.ProductAttribute == ProductAttributeEnum.ItemProduct.ToInt() || _.ProductAttribute == ProductAttributeEnum.ShopPurchase.ToInt()).Sum(_ => _.TotalCostPrice) ?? 0;

                            reconciliationFeeDo.ShopDistributionGrossProfit =
                                reconciliationFeeDo.ActualAmount - reconciliationFeeDo.ShopInstallAmount -
                                reconciliationFeeDo.ShopDistributionCost - reconciliationFeeDo.ShopItemFee;

                            reconciliationFeeDo.ShopDifferencePrice = 0;

                            var shopCommissionFee = (reconciliationFeeDo.ShopInstallAmount + reconciliationFeeDo.ShopItemFee +
                                                 reconciliationFeeDo.ShopDistributionGrossProfit +
                                                 reconciliationFeeDo.ShopDifferencePrice - reconciliationFeeDo.ShopOtherFee);

                            reconciliationFeeDo.ShopCommissionFee = Math.Ceiling((shopCommissionFee * withdrawRate) * 100) / 100;

                            reconciliationFeeDo.ShopSettlementAmount =
                                shopCommissionFee - reconciliationFeeDo.ShopCommissionFee;

                        }
                        else if (reconciliationRule == ReconciliationRuleEnum.ShopToSmallWarehouseOrder)  //门店向小仓下单
                        {
                            if (realProductCodes?.Count > 0)
                            {
                                products?.Data?.ForEach(_ =>
                                {
                                    var totalNum = orderProducts?.Data?.Where(item => item.ProductId == _.Product.ProductCode)
                                          ?.FirstOrDefault()?.TotalNumber ?? 0;

                                    reconciliationFeeDo.ShopDistributionCost += totalNum * _.Product.WholesalePrice;
                                });
                            }

                            reconciliationFeeDo.ShopItemFee = orderProducts?.Data?.Where(_ => _.ProductAttribute == ProductAttributeEnum.ItemProduct.ToInt() || _.ProductAttribute == ProductAttributeEnum.ShopPurchase.ToInt()).Sum(_ => _.TotalCostPrice) ?? 0;

                            reconciliationFeeDo.ShopDistributionGrossProfit =
                              reconciliationFeeDo.ActualAmount - reconciliationFeeDo.ShopInstallAmount -
                              reconciliationFeeDo.ShopDistributionCost - reconciliationFeeDo.ShopItemFee;

                            reconciliationFeeDo.ShopDistributionGrossProfit =
                               reconciliationFeeDo.ActualAmount - reconciliationFeeDo.ShopInstallAmount -
                               reconciliationFeeDo.ShopDistributionCost - reconciliationFeeDo.ShopItemFee;


                            reconciliationFeeDo.ShopDifferencePrice = 0;

                            var shopCommissionFee = (reconciliationFeeDo.ShopInstallAmount + reconciliationFeeDo.ShopItemFee +
                                                 reconciliationFeeDo.ShopDistributionGrossProfit +
                                                 reconciliationFeeDo.ShopDifferencePrice - reconciliationFeeDo.ShopOtherFee);

                            reconciliationFeeDo.ShopCommissionFee = Math.Ceiling((shopCommissionFee * withdrawRate) * 100) / 100;

                            reconciliationFeeDo.ShopSettlementAmount =
                                shopCommissionFee - reconciliationFeeDo.ShopCommissionFee;
                        }
                    }

                    //公司对账金额计算
                    if (!IsNeedCompanyReconciliation(getShops?.Data?.CompanyId ?? 0))
                    {
                        reconciliationFeeDo.CompanyBackAmount = 0;
                        reconciliationFeeDo.CompanyCommissionFee = 0;
                        reconciliationFeeDo.CompanyOtherFee = 0;
                        reconciliationFeeDo.CompanySettlementAmount = 0;
                    }
                    else
                    {
                        if (realProductCodes?.Count > 0)
                        {
                            products?.Data?.ForEach(_ =>
                            {
                                var totalNum = orderProducts?.Data?.Where(item => item.ProductId == _.Product.ProductCode)
                                                   ?.FirstOrDefault()?.TotalNumber ?? 0;
                                reconciliationFeeDo.CompanyBackAmount += totalNum * _.Product?.ReturnCash ?? 0;
                            });
                        }

                        var companyCommissionFee =
                            reconciliationFeeDo.CompanyBackAmount - reconciliationFeeDo.CompanyOtherFee;

                        reconciliationFeeDo.CompanyCommissionFee = Math.Ceiling((companyCommissionFee * withdrawRate) * 100) / 100;

                        reconciliationFeeDo.CompanySettlementAmount =
                            reconciliationFeeDo.CompanyBackAmount - reconciliationFeeDo.CompanyCommissionFee -
                            reconciliationFeeDo.CompanyOtherFee;
                    }
                    //logger.Info("CalculationReconciliationFee 4-" + JsonConvert.SerializeObject(reconciliationFeeDo));

                    string user = "System";
                    reconciliationFeeDo.CreateBy = string.IsNullOrEmpty(request.CreateBy) ? user : request.CreateBy;
                    reconciliationFeeDo.CreateTime = DateTime.Now;
                    await _reconciliationFeeRepository.InsertAsync(reconciliationFeeDo);


                    //var shopInfo = await shopManageClient.GetShopById(new GetShopRequest()
                    //{
                    //    ShopId = order.ShopId
                    //});

                
                    if (IsAutoCompleteShopReconciliation(order))
                    {
                        await this.CreateAccountCheck(new CreateAccountCheckRequest()
                        {
                            Accounts = new List<CreateAccountCheckDTO>()
                            {
                                new CreateAccountCheckDTO()
                                {
                                    LocationId = order.ShopId,
                                    LocationName = getShops?.Data?.SimpleName,
                                    OrderNo = order.OrderNo,
                                    CreateBy = "系统自动对账",
                                    Remark = "系统自动发起对账流程",
                                    ShopCheckResult = "已核对"
                                }
                            }
                        });

                        if (IsAutoCompleteReconciliation(order))
                        {
                            await this.UpdateRgAccountCheckResult(new RgAccountCheckConfirmRequest()
                            {
                                LocationId = (int)order.ShopId,
                                OrderNos = new List<string>() { order.OrderNo },
                                RgCheckResult = "已核对",
                                UpdateBy = "System",
                                WithdrawStatus = AccountCheckWithdrawStatus.申请成功.ToString()
                            });
                        }
                    }
                }
                res.Code = ResultCode.Success;

            }
            catch (Exception ex)
            {
                logger.Error($"CalculationReconciliationFee_Error  Data:{JsonConvert.SerializeObject(request)}", ex);          
                res.Code = ResultCode.Exception;
            }

            return res;
        }

        /// <summary>
        /// 前置过滤计算是否需要对账 应对以后扩展
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private bool IsNeedReconciliation(OrderClientDTO order)
        {
            var result = order.PreFilter(_ =>
              {
                  if (order.ProduceType != ProductTypeEnum.DelegateCompanyDepositOrder.ToInt() &&
                      order.ProduceType != ProductTypeEnum.DelegateShopDepositOrder.ToInt() &&
                      order.ProduceType != ProductTypeEnum.PayGoods.ToInt() && 
                      order.ProduceType != ProductTypeEnum.BuyPackageCard.ToInt() && 
                      order.ProduceType != ProductTypeEnum.MonthReconciliationOrder.ToInt() &&
                      order.ProduceType != ProductTypeEnum.DelegatePay.ToInt())
                  {
                      return _;
                  }

                  return null;
              });

            if (result == null)
            {
                return false;
            }

            return true;

        }

        /// <summary>
        /// 判断是否需要门店对账
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private bool IsNeedShopReconciliation(OrderClientDTO order)
        {

            var result = order.ShopReconciliationFilter(_ =>
            {
                if (order.ProduceType != ProductTypeEnum.DelegateCompanyDepositOrder.ToInt() && order.ProduceType != ProductTypeEnum.DelegateShopDepositOrder.ToInt()
                && order.ProduceType != ProductTypeEnum.PayGoods.ToInt())
                {
                    return _;
                }

                return null;
            });

            if (result == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 判断是否需要公司对账
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        private bool IsNeedCompanyReconciliation(long company)
        {
            var companyId = company.CompanyReconciliationFilter(_ => _);

            return companyId > 0;
        }



        /// <summary>
        /// 判断是否自动完成对账
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private bool IsAutoCompleteReconciliation(OrderClientDTO order)
        {
            var result = order.AutoReconciliationFilter(_ =>
              {
                  if (order.ProduceType == ProductTypeEnum.DelegatePay.ToInt())
                  {
                      return _;
                  }

                  return null;
              });

            if (result != null)
            {
                return true;
            }

            return false;

        }

        private bool IsAutoCompleteShopReconciliation(OrderClientDTO order)
        {
            //默认门店自动对账成功

            return true;
        }

        private ReconciliationRuleEnum GetReconciliationRule(OrderClientDTO order)
        {
            return order.GetReconciliationRule(_ =>
            {
                if (order.ProduceType == ProductTypeEnum.CustomerToSamllWarehouseOrder.ToInt())
                    return ReconciliationRuleEnum.CustomerToSmallWarehouseOrder;
                else if (order.ProduceType == ProductTypeEnum.ShopToSamllWarehouseOrder.ToInt())
                    return ReconciliationRuleEnum.ShopToSmallWarehouseOrder;
                else
                    return ReconciliationRuleEnum.OrdinaryOrder;
            });
        }
        public async Task<ApiResult<List<ReconciliationFeeDTO>>> GetReconciliationFees(GetReconciliationFeesRequest request)
        {
            var dalData = await _reconciliationFeeRepository.GetReconciliationFees(request);
            List<ReconciliationFeeDTO> data = mapper.Map<List<ReconciliationFeeDTO>>(dalData);

            return Result.Success(data);
        }

        public async Task<ApiResult<List<GetNoReconciliationAmountDO>>> GetNoReconciliationAmount(GetNoReconciliationAmountRequest request)
        {
            var data = await _reconciliationFeeRepository.GetNoReconciliationAmount(request);
            return Result.Success(data);
        }

        /// <summary>
        /// 计算生成采购对账单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> CalculationPurchaseReconciliationFee(ApiRequest<CalculationPurchaseReconciliationFeeRequest> request)
        {
            var purchaseOrderNo = request.Data.PurchaseId;
            var purchaseOrders = await _purchaseOrderRepository.GetListAsync($" where is_deleted=0 and id=@PurchaseId and status=6", new { PurchaseId = purchaseOrderNo }, true);
            if (purchaseOrders?.Count() > 0)
            {
                var purchaseOrder = purchaseOrders.FirstOrDefault();
                var purchaseOrderPurchase = await _purchaseOrderProductRepository.GetListAsync($" where is_deleted=0 and po_id=@PurchaseId", new { PurchaseId = purchaseOrderNo }, true);
                var shopInfo = await shopManageClient.GetShopById(new GetShopRequest() { ShopId = purchaseOrder?.ShopId ?? 0 });
                List<PurchaseAccountCheckDO> purchaseAccountCheckDOs = new List<PurchaseAccountCheckDO>(3);
                foreach (var item in purchaseOrderPurchase)
                {
                    purchaseAccountCheckDOs.Add(new PurchaseAccountCheckDO()
                    {
                        LocationId = shopInfo.Data.Id,
                        LocationName = shopInfo?.Data.SimpleName,
                        SettlementStaff = purchaseOrder.PayType == PurchaseOrderPayTypeEnum.Money.ToInt() ?
                                        purchaseOrder.Buyer :
                                        (purchaseOrder.PayType == PurchaseOrderPayTypeEnum.Month.ToInt() ? purchaseOrder.VenderShortName : string.Empty),
                        SettlementType = ((PurchaseOrderPayTypeEnum)purchaseOrder.PayType).GetDescription(),
                        PurchaseNo = purchaseOrder.Id.ToString().PadLeft(10, '0'),
                        purchaseProductId = item.Id,
                        PurchaseDate = purchaseOrder.CreateTime,
                        ProductCategory = item.CategoryName,
                        ProductCode = item.ProductCode,
                        ProductName = item.ProductName,
                        Specification = item.Specification,
                        OeNumber = item.OeNumber,
                        Unit = item.Unit,
                        Num = item.Num,
                        PurchaseCost = item.PurchasePrice,
                        TotalCost = item.TotalCost,
                        SettlementAmount = item.TotalCost,
                        ShopCommissionFee = 0,
                        ShopOtherFee = 0,
                        Status = AccountCheckResultEnum.未核对.ToString(),
                        Remark = string.Empty,
                        IsDeleted = 0,
                        CreateBy = request.Data.CreateUser,
                        CreateTime = DateTime.Now
                    });
                }
                if (purchaseAccountCheckDOs.Count > 0)
                    await _purchaseAccountCheckRepository.InsertBatchAsync(purchaseAccountCheckDOs);

                return Result.Success();
            }


            return Result.Failed("操作失败");
        }

        /// <summary>
        /// 得到采购对账单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<PurchaseAccountCheckDTO>> GetPurchaseAccountList(GetPurchaseAccountListRequest request)
        {

            var dalResponse = await _purchaseAccountCheckRepository.GetPurchaseAccountList(request);

            var apiPagedResultData = mapper.Map<ApiPagedResultData<PurchaseAccountCheckDTO>>(dalResponse);

            return new ApiPagedResult<PurchaseAccountCheckDTO>()
            {
                Code = ResultCode.Success,
                Data = apiPagedResultData
            };
        }

        /// <summary>
        /// 保存对账单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> SavePurchaseAccountRecord(ApiRequest<SavePurchaseAccountRecordRequest> request)
        {
            var purchaseCheckData = await _purchaseAccountCheckRepository.GetListAsync(" where is_deleted=0 and id in @Ids", new { Ids = request.Data.PurchaseAccounts.Select(_ => _.Id).ToList() }, true);
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var status = AccountCheckResultEnum.已核对.ToString();

                if (!string.IsNullOrEmpty(request.Data.Status) && request.Data.Status == AccountCheckResultEnum.核对异常.ToString())
                {
                    status = AccountCheckResultEnum.未核对.ToString();
                }

                purchaseCheckData?.ToList()?.ForEach(_ =>
                {
                    _.Status = status;
                    _.UpdateBy = request.Data.CreateUser;
                    _.UpdateTime = DateTime.Now;
                    if (!string.IsNullOrEmpty(request.Data.Status) && request.Data.Status == AccountCheckResultEnum.核对异常.ToString())
                    {
                        _.SettlementAmount = request.Data.SettlementAmount;
                        _purchaseAccountCheckRepository.Update(_, new List<string>() { "Status", "UpdateBy", "UpdateTime", "SettlementAmount" });
                    }
                    else
                    {
                        _purchaseAccountCheckRepository.Update(_, new List<string>() { "Status", "UpdateBy", "UpdateTime" });
                    }


                });
                ts.Complete();
            }

            return Result.Success<string>("操作成功");
        }

        public async Task<ApiResult<string>> SavePurchaseExceptionAccount(ApiRequest<SavePurchaseExceptionAccountRequest> request)
        {
            var purchaseCheckDatas = await _purchaseAccountCheckRepository.GetListAsync(" where is_deleted=0 and status=@Status", new { Status = AccountCheckResultEnum.未核对.ToString() }, true);

            if (purchaseCheckDatas?.Count() > 0)
            {
                var purchaseCheckData = purchaseCheckDatas?.FirstOrDefault();
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _purchaseAccountCheckExceptionRepository.InsertAsync(new PurchaseAccountCheckExceptionDO()
                    {
                        CreateBy = request.Data.ReportBy,
                        CreateTime = DateTime.Now,
                        LocationId = purchaseCheckData.LocationId,
                        LocationName = purchaseCheckData.LocationName,
                        IsDeleted = 0,
                        ProductCode = purchaseCheckData.ProductCode,
                        PurchaseId = purchaseCheckData.Id,
                        RelationNo = purchaseCheckData.PurchaseNo,
                        ReportReason = request.Data.ReportReason,
                        ReportBy = request.Data.ReportBy,
                        ReportTime = DateTime.Now,
                    });

                    purchaseCheckData.Status = AccountCheckResultEnum.核对异常.ToString();

                    await _purchaseAccountCheckRepository.UpdateAsync(purchaseCheckData, new List<string> { "Status" });
                    ts.Complete();
                }

                return Result.Success<string>("操作成功");
            }

            return Result.Success<string>("操作失败");
        }

        public async Task<ApiResult<string>> DeleteAccountCheck(CalculationReconciliationFeeRequest request)
        {
            logger.Info("DeleteAccountCheck :" + JsonConvert.SerializeObject(request));
            var id = await _reconciliationFeeRepository.DeleteReconciliationFee(request);
            id = await accountCheckRepository.DeleteAccountCheck(new ShopAccountCheckConfirmRequest()
            {
                OrderNo = request.OrderNo,
                UpdateBy  = request.CreateBy,
            }); ;

            return Result.Success<string>("操作成功");
        }

    }
}
