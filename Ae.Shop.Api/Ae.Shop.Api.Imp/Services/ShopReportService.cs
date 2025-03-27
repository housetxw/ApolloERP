using AutoMapper;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Clients;
using Ae.Shop.Api.Common.Exceptions;
using Ae.Shop.Api.Core.Enums;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.ShopReport;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Request.ShopReport;
using Ae.Shop.Api.Core.Response.ShopReport;
using Ae.Shop.Api.Dal.Model.ShopReport;
using Ae.Shop.Api.Dal.Repositorys.ShopReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Imp.Services
{
    public class ShopReportService: IShopReportService
    {
        private readonly ApolloErpLogger<ShopReportService> logger;
        private readonly IShopReportRepository shopReportRepository;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;
        private readonly IShopMangeClient _shopMangeClient;

        public ShopReportService(ApolloErpLogger<ShopReportService> logger,
            IShopReportRepository shopReportRepository, IMapper mapper,
            IConfiguration configuration, IIdentityService identityService, IShopMangeClient shopMangeClient)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.shopReportRepository = shopReportRepository;
            this.mapper = mapper;
            this.identityService = identityService;
            _shopMangeClient = shopMangeClient;
        }

        public async Task<ApiResult<List<EmployeePerformanceDto>>> GetEmployeePerformanceList(EmployeePerformanceRequest request)
        {
            var organizationId = identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);
            request.ShopId = shopId;
            //if (string.IsNullOrWhiteSpace(request.StartDate))
            //{
            //    request.StartDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            //}
            //if (string.IsNullOrWhiteSpace(request.EndDate))
            //{
            //    request.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
            //}
            return await _shopMangeClient.GetEmployeePerformanceList(request);
        }

        /// <summary>
        /// 报表查询
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<ShopSalesMonthResponse>>> GetShopSalesMonthList(GetShopSalesMonthRequest request)
        {
            List<ShopSalesMonthResponse> response = new List<ShopSalesMonthResponse>();
            var organizationId = identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);
            if (shopId <= 0)
            {
                throw new CustomException("登录信息异常");
            }
            request.ShopId = shopId;
            List<ShopOrderDO> dalList = await shopReportRepository.GetShopOrderList(request);
            DateTime startDateTime = Convert.ToDateTime(request.StartDate);
            DateTime endDateTime = Convert.ToDateTime(request.EndDate);
            if(dalList==null || dalList.Count()==0)
            {
                return Result.Success(response);
            }
            while (startDateTime<= endDateTime)
            {
                DateTime curretDate = startDateTime;
                startDateTime = startDateTime.AddDays(1);
                ShopSalesMonthResponse shopSale = new ShopSalesMonthResponse();
                shopSale.InstallDate = curretDate.ToString("yyyy-MM-dd");
                var curretData = dalList.Where(t => t.InstallTime == curretDate);
                List<ShopSaleMonthVO> shopSaleList = new List<ShopSaleMonthVO>();
                
                if(curretData == null || curretData.Count()==0)
                {
                    shopSale.ShopSaleMonthList = new List<ShopSaleMonthVO>();
                    response.Add(shopSale);
                    continue;
                }
                foreach (int type in Enum.GetValues(typeof(OrderTypeEnum)))
                {
                    ShopSaleMonthVO sale = new ShopSaleMonthVO();
                    sale.Type = type;
                    //总体
                    if (type==0)
                    {
                        //求客户数
                        sale.CustomerNum = curretData.Select(t => t.UserId).Distinct().Count();
                        //订单数
                        sale.OrderNum = curretData.Count();
                        //订单总金额
                        sale.OrderSumMoney = curretData.Sum(t => t.ActualAmount);
                        //客户均金额
                        //sale.CustomerAvgMoney = sale.CustomerNum == 0 ? 0 : (decimal)(Math.Round((decimal)(sale.OrderSumMoney / sale.CustomerNum), 2));
                        //订单均金额
                        //sale.OrderAvgMoney = sale.OrderNum == 0 ? 0 : (decimal)(Math.Round((decimal)(sale.OrderSumMoney / sale.OrderNum), 2));

                        //不含套餐卡销售金额
                        var allMoney = curretData.Where(t => t.OrderType != 7).Sum(t => t.ActualAmount);
                        //均值客户数，不含套餐卡和0元订单
                        var cNum = curretData.Where(t => t.OrderType != 7 && t.ActualAmount > 0).Select(t => t.UserId).Distinct().Count();
                        //均值订单数，不含套餐卡和0元订单
                        var oNum = curretData.Where(t => t.OrderType != 7 && t.ActualAmount > 0).Count();

                        //客户均金额
                        sale.CustomerAvgMoney = cNum == 0 ? 0 : (decimal)(Math.Round((decimal)(allMoney / cNum), 2));
                        //订单均金额
                        sale.OrderAvgMoney = oNum == 0 ? 0 : (decimal)(Math.Round((decimal)(allMoney / oNum), 2));
                    }
                    else
                    {
                        var curretDataType = curretData.Where(t => t.OrderType == type);
                        if(curretDataType!=null && curretDataType.Count()!=0)
                        {
                            //求客户数
                            sale.CustomerNum = curretDataType.Select(t => t.UserId).Distinct().Count();
                            //订单数
                            sale.OrderNum = curretDataType.Where(t => t.OrderType == type).Count();
                            //订单总金额
                            sale.OrderSumMoney = curretDataType.Where(t => t.OrderType == type).Sum(t => t.ActualAmount);
                            //客户均金额
                            sale.CustomerAvgMoney = sale.CustomerNum == 0 ? 0 : (decimal)(Math.Round((decimal)(sale.OrderSumMoney / sale.CustomerNum), 2));
                            //订单均金额
                            sale.OrderAvgMoney = sale.OrderNum == 0 ? 0 : (decimal)(Math.Round((decimal)(sale.OrderSumMoney / sale.OrderNum), 2));
                        }
                    }
                    shopSaleList.Add(sale);
                }
                shopSale.ShopSaleMonthList = shopSaleList;
                response.Add(shopSale);
            }
            return Result.Success(response);
        }
    }
}
