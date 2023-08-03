using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Clients;
using Ae.Shop.Api.Client.Clients.Order;
using Ae.Shop.Api.Common.Exceptions;
using Ae.Shop.Api.Core.Enums;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Request.Order;
using Ae.Shop.Api.Dal.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Imp.Services
{
    public class EmployeePerformanceService : IEmployeePerformanceService
    {
        private readonly ApolloErpLogger<EmployeePerformanceService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly IShopMangeClient _shopMangeClient;
        private readonly IOrderClient _orderClient;
        private readonly IShopRepository _shopRepository;

        public EmployeePerformanceService(ApolloErpLogger<EmployeePerformanceService> logger, IMapper mapper,
            IConfiguration configuration, IIdentityService identityService, IShopMangeClient shopMangeClient, IOrderClient orderClient,
            IShopRepository shopRepository)
        {
            this._configuration = configuration;
            this._logger = logger;
            this._mapper = mapper;
            this._identityService = identityService;
            this._shopMangeClient = shopMangeClient;
            this._orderClient = orderClient;
            this._shopRepository = shopRepository;
        }

        public async Task<ApiResult<List<EmployeePerformanceOrderDTO>>> GetEmployeePerformanceOrderList(EmployeePerformanceRequest request)
        {
            var organizationId = _identityService.GetOrganizationId();
            var orgType = _identityService.GetOrgType();
            long.TryParse(organizationId, out long shopId);
            var shopIds = new List<long>();
            //公司
            if (orgType == "0")
            {
                var shopInfos = await _shopRepository.GetListAsync(" where status=0 and  online=1 and  check_status=2 and is_deleted=0 and company_id =@companyId", new { companyId = shopId });
                shopIds = shopInfos.Select(r => r.Id).ToList();
            }
            //门店
            else if (orgType == "1")
            {
                shopIds.Add(shopId);
            }

            //request.ShopId = shopId;
            request.ShopIds = shopIds;
            if (string.IsNullOrWhiteSpace(request.StartDate))
            {
                request.StartDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrWhiteSpace(request.EndDate))
            {
                request.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            _logger.Info($"GetEmployeePerformanceOrderList Data:{JsonConvert.SerializeObject(request)}");
            return await _orderClient.GetEmployeePerformanceOrderList(request);
        }

        public async Task<ApiResult> UpdateEmployeePerformanceOrder(UpdateEmployeePerformanceRequest request)
        {
            string createBy = string.IsNullOrWhiteSpace(request.UpdateBy) ? _identityService.GetUserName() : request.UpdateBy;
            request.UpdateBy = createBy;

            var organizationId = _identityService.GetOrganizationId();
            var orgType = _identityService.GetOrgType();
            long.TryParse(organizationId, out long shopId);
        
            //公司
            if (orgType == "0" && request.ShopId == 0)
            {
                throw new CustomException("门店未选择");
            }
            //门店
            else if (orgType == "1")
            {
                request.ShopId = shopId;
            }

            if (string.IsNullOrWhiteSpace(request.OrderDate))
            {
                throw new CustomException("订单日期未选择");
            }

            var res = new ApiResult();
            try
            {
                //未选择订单号，更新当日所有门店订单
                if (request.OrderNo == null || !request.OrderNo.Any())
                {
                    var orders = await _orderClient.GetOrderInfoListForShop(new GetOrderInfoListForShopRequest() { 
                        ShopId = request.ShopId,
                        CreateStartTime = request.OrderDate,
                        CreateEndTime = request.OrderDate,
                        OrderStatus = "30"
                    });
                    request.OrderNo = orders?.Data?.Items?.Select(_ => _.OrderNo).ToList();
                }

                res = await _orderClient.UpdateEmployeePerformanceOrder(request);
        
            }
            catch (Exception ex)
            {
                res.Code = ResultCode.Exception;
                _logger.Error($"UpdateEmployeePerformanceOrder Data:{JsonConvert.SerializeObject(request)}", ex);
            }
            return res;
        }

    }
}
