using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Model.Shop;
using Ae.ShopOrder.Service.Client.Request.Shop;
using Ae.ShopOrder.Service.Core.Model;
using Ae.ShopOrder.Service.Core.Request.Arrival;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Request.Shop;
using Ae.ShopOrder.Service.Core.Response.Order;
using Ae.ShopOrder.Service.Core.Response.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Client.Clients.ShopServer.Impl
{
    public class ShopClient:IShopClient
    {

        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public ShopClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }
        /// <summary>
        /// 获取门店单表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<ShopDTO>> GetShopById(GetShopRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var response = await client.GetAsJsonAsync<GetShopRequest, ApiResult<ShopDTO>> (configuration["ShopManageServer:GetShopById"], request);
            return response;
        }

        public async Task<ApiResult<List<GetShopServiceListWithPIDResponse>>> GetShopServiceListWithPID(GetShopServiceListWithPIDRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var response = await client.PostAsJsonAsync<GetShopServiceListWithPIDRequest, ApiResult<List<GetShopServiceListWithPIDResponse>>>(configuration["ShopManageServer:GetShopServiceListWithPID"], request);
            return response;
        }

        /// <summary>
        /// 得到技师信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<GetTechnicianPageResponse>> GetTechnicianPage(GetTechnicianPageRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");

            ApiPagedResult<GetTechnicianPageResponse> result = await
                client.GetAsJsonAsync<GetTechnicianPageRequest, ApiPagedResult<GetTechnicianPageResponse>>(configuration["ShopManageServer:GetTechnicianPage"], request);

            return result?.Data?.Items?.ToList();
        }

        public async Task<ApiResult<ShopConfigDTO>> GetShopConfigByShopId(GetShopRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var response = await client.GetAsJsonAsync<GetShopRequest, ApiResult<ShopConfigDTO>>(configuration["ShopManageServer:GetShopConfigByShopId"], request);
            return response;
        }

        public async Task<ApiPagedResult<ShopSimpleInfoDTO>> GetShopListAsync(GetShopListRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var response = await client.PostAsJsonAsync<GetShopListRequest, ApiPagedResult<ShopSimpleInfoDTO>>(configuration["ShopManageServer:GetShopListAsync"], request);
            return response;
        }

        public async Task<ApiResult> UpdateShopDeposit(UpdateShopDepositRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var response = await client.PostAsJsonAsync<UpdateShopDepositRequest, ApiResult>(configuration["ShopManageServer:UpdateShopDeposit"], request);
            return response;
        }

        public async Task<ApiResult<bool>> OperateCompanyDeposit(OperateCompanyDepositRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var response = await client.PostAsJsonAsync<OperateCompanyDepositRequest, ApiResult<bool>>(configuration["ShopManageServer:OperateCompanyDeposit"], request);
            return response;
        }

        public async Task<ApiResult<EmployeeInfoDTO>> GetEmployeeInfo(EmployeeInfoRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var response = await client.GetAsJsonAsync<EmployeeInfoRequest, ApiResult<EmployeeInfoDTO>>(configuration["ShopManageServer:GetEmployeeInfo"], request);
            return response;
        }

        public async Task<List<GetTechnicianPageResponse>> GetShopEmployeeByJobIdPage(GetShopEmployeeByJobIdPageRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");

            ApiPagedResult<GetTechnicianPageResponse> result = await
                client.PostAsJsonAsync<GetShopEmployeeByJobIdPageRequest, ApiPagedResult<GetTechnicianPageResponse>>(configuration["ShopManageServer:GetShopEmployeeByJobIdPage"], request);

            return result?.Data?.Items?.ToList();
        }

        public async Task<ApiResult<List<ShopGrouponProductDTO>>> GetShopGrouponProduct(GetShopGrouponProductRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");

            ApiResult<List<ShopGrouponProductDTO>> result = await
                client.PostAsJsonAsync<GetShopGrouponProductRequest, ApiResult<List<ShopGrouponProductDTO>>>(configuration["ShopManageServer:GetShopGrouponProduct"], request);

            return result;
        }

        public async Task<ApiResult<List<ShopPerformanceConfigDTO>>> GetShopPerformanceConfig(GetShopRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");

            var result = await
                client.GetAsJsonAsync<GetShopRequest, ApiResult<List<ShopPerformanceConfigDTO>>>(configuration["ShopManageServer:GetShopPerformanceConfig"], request);

            return result;
        }
    }
}
