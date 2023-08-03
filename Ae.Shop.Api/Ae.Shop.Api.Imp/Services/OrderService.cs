using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Clients;
using Ae.Shop.Api.Client.Clients.Order;
using Ae.Shop.Api.Client.Clients.ReserveServer;
using Ae.Shop.Api.Common.Extension;
using Ae.Shop.Api.Core.Enums;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.Order;
using Ae.Shop.Api.Core.Model.Reserve;
using Ae.Shop.Api.Core.Request.Order;
using Ae.Shop.Api.Core.Response.Order;
using Ae.Shop.Api.Dal.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Imp.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApolloErpLogger<OrderService> _logger;
        private readonly IOrderClient _orderClient;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly IReserveClient _reserveClient;
        private readonly IShopMangeClient _shopMangeClient;
        private readonly IShopRepository _shopRepository;


        public OrderService(ApolloErpLogger<OrderService> logger,
           IOrderClient orderClient, IMapper mapper,
           IConfiguration configuration, IIdentityService identityService, IReserveClient reserveClient, IShopMangeClient shopMangeClient, IShopRepository shopRepository)
        {
            _configuration = configuration;
            _logger = logger;
            _orderClient = orderClient;
            _mapper = mapper;
            _identityService = identityService;
            _reserveClient = reserveClient;
            _shopMangeClient = shopMangeClient;
            _shopRepository = shopRepository;
        }

        /// <summary>
        /// 得到订单基本信息集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<OrderDTO>>> GetOrderBaseInfo(GetOrderBaseInfoRequest request)
        {
           // var organizationId = _identityService.GetOrganizationId();
            //long.TryParse(organizationId, out long shopId);
            //request.ShopId = shopId == 0 ? request.ShopId : shopId;
            return await _orderClient.GetOrderBaseInfo(request);

        }

        public async Task<ApiResult<List<OrderCarDTO>>> GetOrderCarsInfo(GetOrderCarsRequest request)
        {
            var result = await _orderClient.GetOrderCarsInfo(request);

            if (result?.Data != null && result?.Data?.Count > 0)
            {
                result?.Data?.ForEach(car =>
                {
                    car.DisplayCarName = $"{car?.Brand} {car?.Vehicle} {car?.Nian} {car?.PaiLiang} {car?.SalesName}";//格式：品牌Brand 车系Vehicle 年份Nian 排量PaiLiang 款型SalesName

                });
            }

            return result;
        }

        public Task<ApiResult<List<OrderDispatchDTO>>> GetOrderDispatch(GetOrderDispatchRequest request)
        {
            return _orderClient.GetOrderDispatch(request);
        }

        public async Task<ApiResult<OrderDispatchDTO>> GetDispatchInfo(GetReserverInfoRequest request)
        {
            var result = new ApiResult<OrderDispatchDTO>()
            {
                Code = ResultCode.Success,
                Data = new OrderDispatchDTO()
            };

            if (request.OrderNo == "")
            { return result;
            }

            var orderDispatchs = await _orderClient.GetOrderDispatch(new GetOrderDispatchRequest()
            {
                OrderNos = new List<string>(1) { request.OrderNo}
            });
            var orderDispatchInfos = orderDispatchs?.Data;

            if (orderDispatchInfos != null && orderDispatchInfos?.Count > 0)
            {
                var techName = "";
                orderDispatchInfos?.ForEach(_ =>
                {
                    techName += _.TechName + "-" + _.Percent.ToString() + "、";
                });
                if (result.Data != null)
                {
                    result.Data.TechName = techName.Length > 0 ? techName.Substring(0, techName.Length - 1) : "";
                }
            }

            return result;


        }

        /// <summary>
        /// 得到订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<OrderDTO>> GetOrderInfoListForShop(GetOrderInfoListForShopRequest request)
        {
            var organizationId = _identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);
            request.ShopId = shopId == 0 ? request.ShopId : shopId;
            var orgType = _identityService.GetOrgType();

            List<long> shopIds = new List<long>();
            if (orgType == "0")
            {
                var shopData = await _shopMangeClient.GetCompanyAndShopByParentId(new Core.Request.ShopManage.GetCompanyInfoByIdRequest()
                {
                    Id = shopId
                });

                request.ShopIds = shopData?.Data?.Where(_ => _.Type == 1)?.Select(_ => _.Id)?.ToList();

                shopIds.AddRange(request.ShopIds);

            }
            if (shopIds?.Count == 0)
            {
                shopIds.Add(shopId);
            }
            var getShopListByIds = await _shopMangeClient.GetShopListByIdsAsync(new Client.Request.ShopManage.GetShopListByIdsRequest()
            {
                ShopIds = shopIds
            });

            var response = await _orderClient.GetOrderInfoListForShop(request);
            response?.Data?.Items?.ToList()?.ForEach(item =>
            {
                if (item.PayStatus == PayStatusEnum.HavePay.ToInt() && item.OrderStatus == OrderStatusEnum.Confirmed.ToInt() &&
                item.InstallStatus == InstallStatusEnum.NotInstall.ToInt() && item.CreateTime <= DateTime.Now.AddDays(-15))
                {
                    item.IsExceptionOrder = true;
                }

                item.ShopName = getShopListByIds?.Data?.Where(_ => _.Id == item.ShopId)?.FirstOrDefault()?.SimpleName;

            });




            return response;
        }

        public Task<ApiResult<OrderInsuranceCompanyDTO>> GetOrderInsuranceCompany(GetOrderInsuranceCompanyRequest request)
        {
            return _orderClient.GetOrderInsuranceCompany(request);
        }



        /// <summary>
        /// 得到订单产品信息集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<OrderProductDTO>>> GetOrderProduct(GetOrderProductRequest request)
        {
            var getOrderProducts = await _orderClient.GetOrderProduct(request);

            getOrderProducts?.Data?.ToList()?.ForEach(_ =>
            {
                _.Number = _.TotalNumber;
                _.ImageUrl = $"https://m.ApolloErp.cn/{_.ImageUrl}";
            });
            return getOrderProducts;
        }



        /// <summary>
        /// 得到预约信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<ShopReserveDTO>> GetReserverInfo(GetReserverInfoRequest request)
        {
            var result = new ApiResult<ShopReserveDTO>()
            {
                Code = ResultCode.Success,
                Data = new ShopReserveDTO()
            };
            var reserverOrderNum = await _reserveClient.GetReserveInfoByReserveIdOrOrderNum(new GetReserveInfoByReserveIdOrOrderNum()
            {
                OrderNumbers = new List<string>() { request.OrderNo }
            });

            var reserverId = reserverOrderNum?.Data?.FirstOrDefault()?.ReserveId ?? 0;
            if (reserverId > 0)
            {
                result = await _reserveClient.GetShopReserveDO(new Core.Request.Reserve.ReserveDetailRequest()
                {
                    ReserveId = reserverId
                });
            }

            return result;


        }

        #region Shop 报表中心


        public async Task<ApiResult<List<GetOrderStaticReportResponse>>> GetOrderStaticReportForShop()
        {
            Task[] tasks = new Task[6];

            var tempShopIds = new List<long>();
            var orgType = _identityService.GetOrgType();
            if (orgType == "0")
            {
                var companyId = long.Parse(_identityService.GetOrganizationId());
                var shopList = await _shopRepository.GetListAsync(" where company_id=@companyId and is_deleted =0 and online=1",
                    new { companyId = companyId });
                if (shopList != null && shopList.Any())
                {
                    List<long> shopIds = shopList.Select(r => r.Id).ToList();
                    tempShopIds.AddRange(shopIds);
                }
            }
            else if (orgType == "1")
            {
                var shopId = long.Parse(_identityService.GetOrganizationId());

                tempShopIds.Add(shopId);
            }
            //var shopId = long.Parse(_identityService.GetOrganizationId());

            var weekCompleteTask = _orderClient.GetOrderCompleteStaticReport(new GetOrderCompleteStaticReportRequest()
            {
                StartTime = DateTimeHelper.GetThisWeekMonday().ToString("yyyy-MM-dd"),
                EndTime = DateTimeHelper.GetThisWeekMonday().AddDays(6).ToString("yyyy-MM-dd"),
                ShopIds = tempShopIds
            });

            var monthCompleteTask = _orderClient.GetOrderCompleteStaticReport(new GetOrderCompleteStaticReportRequest()
            {
                StartTime = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.ToString("yyyy-MM-dd"),
                EndTime = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1)
                    .ToString("yyyy-MM-dd"),
                ShopIds = tempShopIds
            });

            var yearCompleteTask = _orderClient.GetOrderCompleteStaticReport(new GetOrderCompleteStaticReportRequest()
            {
                StartTime = DateTime.Now.Year.ToString() + "-01-01",
                EndTime = DateTime.Now.Year.ToString() + "-12-31",
                ShopIds = tempShopIds

            });

            var weekNotCompleteTask = _orderClient.GetOrderNotCompleteStaticReport(new GetOrderNotCompleteStaticReportRequest()
            {
                StartTime = DateTimeHelper.GetThisWeekMonday().ToString("yyyy-MM-dd"),
                EndTime = DateTimeHelper.GetThisWeekMonday().AddDays(6).ToString("yyyy-MM-dd"),
                ShopIds = tempShopIds
            });

            var monthNotCompleteTask = _orderClient.GetOrderNotCompleteStaticReport(new GetOrderNotCompleteStaticReportRequest()
            {
                StartTime = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.ToString("yyyy-MM-dd"),
                EndTime = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1)
                    .ToString("yyyy-MM-dd"),
                ShopIds = tempShopIds
            });

            var yearNotCompleteTask = _orderClient.GetOrderNotCompleteStaticReport(new GetOrderNotCompleteStaticReportRequest()
            {
                StartTime = DateTime.Now.Year.ToString() + "-01-01",
                EndTime = DateTime.Now.Year.ToString() + "-12-31",
                ShopIds = tempShopIds
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

        public async Task<ApiPagedResult<GetOrderDetailStaticReportResponse>> GetOrderDetailStaticReportForShop(GetOrderDetailStaticReportApiRequest request)
        {
            GetOrderDetailStaticReportRequest reportRequest = new GetOrderDetailStaticReportRequest();
            //var shopId = long.Parse(_identityService.GetOrganizationId());

            var orgType = _identityService.GetOrgType();
            if (orgType == "0")
            {
                var companyId = long.Parse(_identityService.GetOrganizationId());
                var shopList = await _shopRepository.GetListAsync(" where company_id=@companyId and is_deleted =0 and online=1",
                    new { companyId = companyId });
                if (shopList != null && shopList.Any())
                {
                    List<long> shopIds = shopList.Select(r => r.Id).ToList();
                    reportRequest.ShopIds = new List<long>();
                    reportRequest.ShopIds.AddRange(shopIds);
                }
            }
            else if (orgType == "1")
            {
                var shopId = long.Parse(_identityService.GetOrganizationId());

                reportRequest.ShopIds = new List<long>();
                reportRequest.ShopIds.Add(shopId);
            }

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
            //reportRequest.ShopIds.Add(shopId);
            return await _orderClient.GetOrderDetailStaticReport(reportRequest);
        }

        public async Task<ApiPagedResult<GetOrderOutProductsProfitResponse>> GetOrderOutProductsProfitReportForShop(ApiRequest<GetOrderOutProductsProfitRequest> request)
        {
            var orgType = _identityService.GetOrgType();
            if (orgType == "0")
            {
                var companyId = long.Parse(_identityService.GetOrganizationId());
                var shopList = await _shopRepository.GetListAsync(" where company_id=@companyId and is_deleted =0 and online=1",
                    new { companyId = companyId });
                if (shopList != null && shopList.Any())
                {
                    List<long> shopIds = shopList.Select(r => r.Id).ToList();
                    request.Data.ShopIds = new List<long>();
                    request.Data.ShopIds.AddRange(shopIds);
                }
            }
            else if (orgType == "1")
            {
                var shopId = long.Parse(_identityService.GetOrganizationId());

                request.Data.ShopIds = new List<long>();
                request.Data.ShopIds.Add(shopId);
            }

            return await _orderClient.GetOrderOutProductsProfitReport(request);
        }

        public async Task<ApiPagedResult<OrderProductNewDTO>> GetOrderProductsReportForShop(ApiRequest<GetOrderProductsRequest> request)
        {
            var orgType = _identityService.GetOrgType();
            if (orgType == "0")
            {
                var companyId = long.Parse(_identityService.GetOrganizationId());
                var shopList = await _shopRepository.GetListAsync(" where company_id=@companyId and is_deleted =0 and online=1",
                    new { companyId = companyId });
                if (shopList != null && shopList.Any())
                {
                    List<long> shopIds = shopList.Select(r => r.Id).ToList();
                    request.Data.ShopIds = new List<long>();
                    request.Data.ShopIds.AddRange(shopIds);
                }
            }
            else if (orgType == "1")
            {
                var shopId = long.Parse(_identityService.GetOrganizationId());

                request.Data.ShopIds = new List<long>();
                request.Data.ShopIds.Add(shopId);
            }

            return await _orderClient.GetOrderProductsReport(request);

        }
        #endregion

    }
}
