using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Client.Clients;
using Ae.B.Order.Api.Client.Clients.ShopOrder;
using Ae.B.Order.Api.Client.Request;
using Ae.B.Order.Api.Common.Exceptions;
using Ae.B.Order.Api.Core.Enums;
using Ae.B.Order.Api.Core.Interfaces;
using Ae.B.Order.Api.Core.Model;
using Ae.B.Order.Api.Core.Request;
using Ae.B.Order.Api.Core.Request.OrderCommand;
using Ae.B.Order.Api.Core.Request.OrderQuery;
using Ae.B.Order.Api.Core.Response;
using Ae.B.Order.Api.Core.Response.OrderQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.B.Order.Api.Imp.Helpers;
using Ae.B.Order.Api.Core.Model.OrderDetail;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Ae.B.Order.Api.Imp.Services
{
    public class OrderQueryService : IOrderQueryService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<OrderQueryService> logger;
        private readonly IOrderClient orderClient;
        private readonly IConsumerOrderClient consumerOrderClient;
        private readonly IReverseClient reverseClient;
        private readonly IShopOrderClient shopOrderClient;
        private readonly IIdentityService _identityService;

        public OrderQueryService(IMapper mapper, ApolloErpLogger<OrderQueryService> logger, IOrderClient orderClient,
            IConsumerOrderClient consumerOrderClient, IReverseClient reverseClient,
            IShopOrderClient shopOrderClient, IIdentityService identityService)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.orderClient = orderClient;
            this.consumerOrderClient = consumerOrderClient;
            this.reverseClient = reverseClient;
            this.shopOrderClient = shopOrderClient;
            _identityService = identityService;
        }

        public async Task<ApiResult<GetOrderConfirmResponse>> GetOrderConfirm(ApiRequest<GetOrderConfirmRequest> request)
        {
            if (request.Data.ProduceType == (int)ProductTypeEnum.DoorToDoor)
            {
                request.Data.ProductInfos.Add(new Ae.B.Order.Api.Core.Model.OrderDetail.SelectedProductInfoVO()
                {
                    Pid = "BYFW-SMFW-SSMFW-2-FU",
                    Number = 1
                });
            }
            return await consumerOrderClient.GetOrderConfirm(request);
        }

        public async Task<ApiResult<GetOrderDetailResponse>> GetOrderDetail(GetOrderDetailRequest request)
        {
            var result = Result.Failed<GetOrderDetailResponse>("加载异常，请重试");
            try
            {
                var IsStart = request.OrderNo.StartsWith("RGC");//返回True
                                                                //if (IsStart)
                                                                //{
                                                                //    var clientRequest = mapper.Map<GetOrderDetailForBossClientRequest>(request);
                                                                //    var clientResult = await consumerOrderClient.GetOrderDetailForBoss(clientRequest);
                                                                //    if (clientResult.IsNotNullSuccess())
                                                                //    {
                                                                //        var response = mapper.Map<GetOrderDetailResponse>(clientResult.Data);
                                                                //        result = Result.Success(response, clientResult.Message);
                                                                //    }
                                                                //    else
                                                                //    {
                                                                //        result.Code = clientResult.Code;
                                                                //        result.Message = clientResult.Message;
                                                                //    }
                                                                //}
                                                                //else
                                                                //{
                var clientRequest = mapper.Map<GetOrderDetailForBossClientRequest>(request);
                var clientResult = await shopOrderClient.GetOrderDetailForBoss(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = mapper.Map<GetOrderDetailResponse>(clientResult.Data);
                    result = Result.Success(response, clientResult.Message);
                }
                else
                {
                    result.Code = clientResult.Code;
                    result.Message = clientResult.Message;
                }
                //}
            }
            catch (Exception ex)
            {
                logger.Error("GetOrderDetailEx", ex);
            }
            return result;
        }

        public async Task<ApiPagedResult<GetOrderListResponse>> GetOrderList(GetOrderListRequest request)
        {
            var result = new ApiPagedResult<GetOrderListResponse>()
            {
                Code = ResultCode.Failed,
                Message = "加载异常，请重试",
                Data = new ApiPagedResultData<GetOrderListResponse>() { Items = new List<GetOrderListResponse>() }
            };
            try
            {
                var clientRequest = mapper.Map<GetOrderListForBossClientRequest>(request);
                var clientResult = await orderClient.GetOrderListForBoss(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    result = new ApiPagedResult<GetOrderListResponse>()
                    {
                        Code = clientResult.Code,
                        Message = clientResult.Message,
                        Data = new ApiPagedResultData<GetOrderListResponse>()
                        {
                            TotalItems = clientResult.Data.TotalItems,
                            Items = mapper.Map<List<GetOrderListResponse>>(clientResult.Data.Items)
                        }
                    };

                    var datas = result?.Data?.Items?.ToList();
                    var orderNos = datas?.Select(_ => _.OrderNo)?.ToList();
                    var orderCars = await orderClient.GetOrderCarsInfo(new GetOrderCarsRequest()
                    {
                        OrderNos = orderNos
                    });

                    datas?.ForEach(_ =>
                    {
                        var orderCar = orderCars?.Data?.Where(item => item.OrderNo == _.OrderNo)?.FirstOrDefault();
                        _.CarInfo = orderCar?.CarNumber + " " + orderCar?.Brand + " " + orderCar?.Vehicle + " " + orderCar?.Nian + " " + orderCar?.PaiLiang;
                    });
                }
                else
                {
                    result.Code = clientResult?.Code ?? ResultCode.Success;
                    result.Message = clientResult?.Message ?? "无数据";
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetOrderListEx", ex);
            }
            return result;
        }


        public async Task<ApiPagedResult<GetOrderMergeListResponse>> GetMergeOrderList(GetMergeOrderListRequest request)
        {
            var result = new ApiPagedResult<GetOrderMergeListResponse>()
            {
                Code = ResultCode.Failed,
                Message = "加载异常，请重试",
                Data = new ApiPagedResultData<GetOrderMergeListResponse>() { Items = new List<GetOrderMergeListResponse>() }
            };
            try
            {
                var clientResult = await orderClient.GetMergeOrderListForBoss(request);
                if (clientResult.IsNotNullSuccess())
                {
                    result = new ApiPagedResult<GetOrderMergeListResponse>()
                    {
                        Code = clientResult.Code,
                        Message = clientResult.Message,
                        Data = new ApiPagedResultData<GetOrderMergeListResponse>()
                        {
                            TotalItems = clientResult.Data.TotalItems,
                            Items = mapper.Map<List<GetOrderMergeListResponse>>(clientResult.Data.Items)
                        }
                    };
                }
                else
                {
                    result.Code = clientResult?.Code ?? ResultCode.Success;
                    result.Message = clientResult?.Message ?? "无数据";
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetMergeOrderListEx", ex);
            }
            return result;
        }

        public async Task<ApiPagedResult<OrderLogVO>> GetOrderLogList(GetOrderLogListRequest request)
        {
            var result = Result.Success(new List<OrderLogVO>(), 0);
            try
            {
                var IsStart = request.OrderNo.StartsWith("RGC");//返回True
                if (IsStart)
                {
                    var clientRequest = mapper.Map<GetOrderLogListClientRequest>(request);
                    var clientResult = await consumerOrderClient.GetOrderLogList(clientRequest);
                    if (clientResult.IsNotNullSuccess())
                    {
                        var response = mapper.Map<List<OrderLogVO>>(clientResult.Data.Items);
                        result = Result.Success(response, clientResult.Data.TotalItems);
                    }
                    else
                    {
                        result = Result.Failed(clientResult.Code, clientResult.Message, new List<OrderLogVO>(), 0);
                    }
                }
                else
                {
                    var clientRequest = mapper.Map<GetOrderLogListClientRequest>(request);
                    var clientResult = await shopOrderClient.GetOrderLogList(clientRequest);
                    if (clientResult.IsNotNullSuccess())
                    {
                        var response = mapper.Map<List<OrderLogVO>>(clientResult.Data.Items);
                        result = Result.Success(response, clientResult.Data.TotalItems);
                    }
                    else
                    {
                        result = Result.Failed(clientResult.Code, clientResult.Message, new List<OrderLogVO>(), 0);
                    }
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("GetOrderLogListEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<List<ReverseReasonVO>>> GetReverseReasons(GetReverseReasonsRequest request)
        {
            var result = Result.Failed<List<ReverseReasonVO>>("加载异常，请重试");
            try
            {
                var clientRequest = mapper.Map<GetReverseReasonConfigsRequest>(request);
                var clientResult = await reverseClient.GetReverseReasonConfigs(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = mapper.Map<List<ReverseReasonVO>>(clientResult.Data);
                    result = Result.Success(response, clientResult.Message);
                }
                else
                {
                    result = Result.Failed<List<ReverseReasonVO>>(clientResult.Code, clientResult.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetReverseReasonsEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<TrialCalcOrderAmountResponse>> TrialCalcOrderAmount(ApiRequest<TrialCalcOrderAmountRequest> request)
        {
            if (request.Data.ProduceType == (int)ProductTypeEnum.DoorToDoor)
            {
                request.Data.ProductInfos.Add(new Ae.B.Order.Api.Core.Request.OrderCommand.SelectedProductInfoDTO()
                {
                    Pid = "BYFW-SMFW-SSMFW-2-FU",
                    Number = 1
                });
            }
            return await consumerOrderClient.TrialCalcOrderAmount(request.Data);
        }

        public async Task<ApiResult<List<GetOrderStaticReportResponse>>> GetOrderStaticReport()
        {
            Task[] tasks = new Task[6];

            var weekCompleteTask = orderClient.GetOrderCompleteStaticReport(new GetOrderCompleteStaticReportRequest()
            {
                StartTime = DateTimeHelper.GetThisWeekMonday().ToString("yyyy-MM-dd"),
                EndTime = DateTimeHelper.GetThisWeekMonday().AddDays(6).ToString("yyyy-MM-dd")
            });

            var monthCompleteTask = orderClient.GetOrderCompleteStaticReport(new GetOrderCompleteStaticReportRequest()
            {
                StartTime = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.ToString("yyyy-MM-dd"),
                EndTime = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1)
                    .ToString("yyyy-MM-dd")
            });

            var yearCompleteTask = orderClient.GetOrderCompleteStaticReport(new GetOrderCompleteStaticReportRequest()
            {
                StartTime = DateTime.Now.Year.ToString() + "-01-01",
                EndTime = DateTime.Now.Year.ToString() + "-12-31"
            });

            var weekNotCompleteTask = orderClient.GetOrderNotCompleteStaticReport(new GetOrderNotCompleteStaticReportRequest()
            {
                StartTime = DateTimeHelper.GetThisWeekMonday().ToString("yyyy-MM-dd"),
                EndTime = DateTimeHelper.GetThisWeekMonday().AddDays(6).ToString("yyyy-MM-dd")
            });

            var monthNotCompleteTask = orderClient.GetOrderNotCompleteStaticReport(new GetOrderNotCompleteStaticReportRequest()
            {
                StartTime = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.ToString("yyyy-MM-dd"),
                EndTime = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1)
                    .ToString("yyyy-MM-dd")
            });

            var yearNotCompleteTask = orderClient.GetOrderNotCompleteStaticReport(new GetOrderNotCompleteStaticReportRequest()
            {
                StartTime = DateTime.Now.Year.ToString() + "-01-01",
                EndTime = DateTime.Now.Year.ToString() + "-12-31"
            });

            tasks = new Task[6]
            {
                weekCompleteTask, monthCompleteTask, yearCompleteTask, yearNotCompleteTask, weekNotCompleteTask,
                monthNotCompleteTask
            };

            Task.WaitAll(tasks);

            List<GetOrderStaticReportResponse> data = new List<GetOrderStaticReportResponse>()
            {
                new GetOrderStaticReportResponse()
                {
                    CycleTime = "本周",
                    HaveFinishOrderAmount = weekCompleteTask?.Result?.Data?.OrderAmount ?? 0,
                    HaveFinishOrderNum = weekCompleteTask?.Result?.Data?.OrderNum ?? 0,
                    NotFinishOrderAmount = weekNotCompleteTask?.Result?.Data?.OrderAmount ?? 0,
                    NotFinishOrderNum = weekNotCompleteTask?.Result?.Data?.OrderNum ?? 0,
                },
                new GetOrderStaticReportResponse()
                {
                    CycleTime = "本月",
                    HaveFinishOrderAmount = monthCompleteTask?.Result?.Data?.OrderAmount ?? 0,
                    HaveFinishOrderNum = monthCompleteTask?.Result?.Data?.OrderNum ?? 0,
                    NotFinishOrderAmount = monthNotCompleteTask?.Result?.Data?.OrderAmount ?? 0,
                    NotFinishOrderNum = monthNotCompleteTask?.Result?.Data?.OrderNum ?? 0,
                },
                new GetOrderStaticReportResponse()
                {
                    CycleTime = "本年",
                    HaveFinishOrderAmount = yearCompleteTask?.Result?.Data?.OrderAmount ?? 0,
                    HaveFinishOrderNum = yearCompleteTask?.Result?.Data?.OrderNum ?? 0,
                    NotFinishOrderAmount = yearNotCompleteTask?.Result?.Data?.OrderAmount ?? 0,
                    NotFinishOrderNum = yearNotCompleteTask?.Result?.Data?.OrderNum ?? 0,
                }
            };

            return Result.Success(data);
        }

        public async Task<ApiPagedResult<GetOrderDetailStaticReportResponse>> GetOrderDetailStaticReport(GetOrderDetailStaticReportApiRequest request)
        {
            GetOrderDetailStaticReportRequest reportRequest = new GetOrderDetailStaticReportRequest();
            if (request.Type == "Week")
            {
                reportRequest.StartTime = DateTimeHelper.GetThisWeekMonday().ToString("yyyy-MM-dd");
                reportRequest.EndTime = DateTimeHelper.GetThisWeekMonday().AddDays(6).ToString("yyyy-MM-dd");
                reportRequest.PageIndex = request.PageIndex;
                reportRequest.PageSize = request.PageSize;
            }

            if (request.Type == "Month")
            {
                reportRequest.StartTime = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.ToString("yyyy-MM-dd");
                reportRequest.EndTime = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1)
                    .ToString("yyyy-MM-dd");
                reportRequest.PageIndex = request.PageIndex;
                reportRequest.PageSize = request.PageSize;
            }

            if (request.Type == "Year")
            {
                reportRequest.StartTime = DateTime.Now.Year.ToString() + "-01-01";
                reportRequest.EndTime = DateTime.Now.Year.ToString() + "-12-31";
                reportRequest.PageIndex = request.PageIndex;
                reportRequest.PageSize = request.PageSize;
            }
            return await shopOrderClient.GetOrderDetailStaticReport(reportRequest);

        }

        public async Task<ApiPagedResult<GetOrderPackageCardsResponse>> GetOrderPackageCards(GetOrderPackageCardsRequest request)
        {
            logger.Info($"GetOrderPackageCards：{JsonConvert.SerializeObject(request)}");
            if (request.ShopId > 0 && string.IsNullOrWhiteSpace(request.SourceOrderNo) && string.IsNullOrWhiteSpace(request.UserPhone) && string.IsNullOrWhiteSpace(request.Code))
            {
                var result = new ApiPagedResult<GetOrderPackageCardsResponse>()
                {
                    Code = ResultCode.Success,
                    Message = "",
                    Data = new ApiPagedResultData<GetOrderPackageCardsResponse>() { Items = new List<GetOrderPackageCardsResponse>() }
                };
                return result;
            }
            else
                return await orderClient.GetOrderPackageCards(request);
        }

        public async Task<ApiPagedResult<GetPackageCardRecordsResponse>> GetPackageCardRecords(GetPackageCardRecordsRequest request)
        {
            return await orderClient.GetPackageCardRecords(request);
        }

        public Task<ApiPagedResult<VerificationRuleDTO>> GetVerificationRule(GetVerificationRuleRequest request)
        {
            return shopOrderClient.GetVerificationRule(request);
        }

        public async Task<ApiPagedResult<GetOrderOutProductsProfitResponse>> GetOrderOutProductsProfitReport(ApiRequest<GetOrderOutProductsProfitRequest> request)
        {
            if (request?.Data?.ShopIds.Count == 0)
            {
                if (request.Data.ShopId > 0)
                {
                    request.Data.ShopIds.Add(request.Data.ShopId);
                }
            }
            return await shopOrderClient.GetOrderOutProductsProfitReport(request);
        }

        public async Task<ApiPagedResult<OrderProductNewDTO>> GetOrderProductsReport(ApiRequest<GetOrderProductsRequest> request)
        {
            if (request?.Data?.ShopIds.Count == 0)
            {
                if (request.Data.ShopId > 0)
                {
                    request.Data.ShopIds.Add(request.Data.ShopId);
                }
            }
            return await shopOrderClient.GetOrderProductsReport(request);
        }
    }
}
