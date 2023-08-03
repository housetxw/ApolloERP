using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Ae.Shop.Api.Client.Model;
using Ae.Shop.Api.Client.Request;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Request.ShopManage;
using Ae.Shop.Api.Client.Model.ShopManage;
using ApolloErp.Log;
using Ae.Shop.Api.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System;
using Ae.Shop.Api.Core.Model.ShopReport;
using Ae.Shop.Api.Core.Request.ShopReport;
using Ae.Shop.Api.Core.Request.ShopManage;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Response;

namespace Ae.Shop.Api.Client.Clients
{
    public class ShopMangeClient : IShopMangeClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        private readonly ApolloErpLogger<ShopMangeClient> logger;

        public ShopMangeClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<ShopMangeClient> logger)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task<ApiResult<ShopDTO>> GetShopById(GetShopClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var response = await client.GetAsJsonAsync<GetShopClientRequest, ApiResult<ShopDTO>>(configuration["ShopManageServer:GetShopById"], request);
            return response;
        }

        /// <summary>
        /// 门店客户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ShopCustomerListDto>> GetShopCustomerList(ShopCustomerListClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiPagedResult<ShopCustomerListDto> result =
                await client
                    .GetAsJsonAsync<ShopCustomerListClientRequest, ApiPagedResult<ShopCustomerListDto>>(
                        configuration["ShopManageServer:GetShopCustomerList"], request);
            if (result?.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Warn($"GetShopCustomerList_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        /// <summary>
        /// 门店客户详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopCustomerDetailDto> GetShopCustomerDetail(ShopCustomerDetailClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult<ShopCustomerDetailDto> result =
                await client
                    .GetAsJsonAsync<ShopCustomerDetailClientRequest, ApiResult<ShopCustomerDetailDto>>(
                        configuration["ShopManageServer:GetShopCustomerDetail"], request);
            if (result?.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Warn($"GetShopCustomerDetail_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        /// <summary>
        /// 获取门店开启的服务项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopServiceTypeDto>> GetShopServiceTypeAsync(ShopServiceTypeClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult<List<ShopServiceTypeDto>> result =
                await client.GetAsJsonAsync<ShopServiceTypeClientRequest, ApiResult<List<ShopServiceTypeDto>>>(
                    configuration["ShopManageServer:GetShopServiceTypeAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<ShopServiceTypeDto>();
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Warn($"GetShopServiceTypeAsync_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        /// <summary>
        /// 获取技师分页数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ApiPagedResultData<ShopEmployeeDto>> GetTechnicianPage(TechnicianPageRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiPagedResult<ShopEmployeeDto> result =
                await client.GetAsJsonAsync<TechnicianPageRequest, ApiPagedResult<ShopEmployeeDto>>(
                    configuration["ShopManageServer:GetTechnicianPage"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Warn($"GetTechnicianPage_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        /// <summary>
        /// 获取技师列表
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<List<ShopEmployeeDto>> GetShopTechnicianList(int shopId, string employeeId)
        {
            int pageSize = 20;
            List<ShopEmployeeDto> result = new List<ShopEmployeeDto>();
            var technicianData = await GetTechnicianPage(new TechnicianPageRequest()
            {
                ShopId = shopId,
                EmployeeId = employeeId,
                PageIndex = 1,
                PageSize = pageSize
            });
            if (technicianData != null)
            {
                var technician1 = technicianData.Items?.ToList() ?? new List<ShopEmployeeDto>();
                var totalCount = technicianData.TotalItems;
                result.AddRange(technician1);
                int totalPage = (int)Math.Ceiling((decimal)totalCount / pageSize);
                if (totalPage > 1)
                {
                    List<int> pageList = new List<int>();
                    for (int i = 1; i < totalPage; i++)
                    {
                        pageList.Add(i + 1);
                    }

                    var allTask = await Task.WhenAll(pageList.Select(_ => GetTechnicianPage(
                        new TechnicianPageRequest
                        {
                            ShopId = shopId,
                            EmployeeId = employeeId,
                            PageIndex = _,
                            PageSize = pageSize

                        })));
                    foreach (var itemTask in allTask)
                    {
                        if (itemTask.Items != null && itemTask.Items.Any())
                        {
                            result.AddRange(itemTask.Items);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<ApiResult<List<EmployeePerformanceDto>>> GetEmployeePerformanceList(EmployeePerformanceRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult<List<EmployeePerformanceDto>> result =
                await client.GetAsJsonAsync<EmployeePerformanceRequest, ApiResult<List<EmployeePerformanceDto>>>(
                    configuration["ShopManageServer:GetEmployeePerformanceList"], request);
            return result;
        }

        public async Task<ApiResult<List<CompanyLessInfoDTO>>> GetCompanyAndShopByParentId(GetCompanyInfoByIdRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult<List<CompanyLessInfoDTO>> result =
                await client.GetAsJsonAsync<GetCompanyInfoByIdRequest, ApiResult<List<CompanyLessInfoDTO>>>(
                    configuration["ShopManageServer:GetCompanyAndShopByParentId"], request);
            return result;
        }

        public async Task<ApiResult<List<ShopSimpleInfoResponse>>> GetShopListByIdsAsync(GetShopListByIdsRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var response = await client.PostAsJsonAsync<GetShopListByIdsRequest, ApiResult<List<ShopSimpleInfoResponse>>>(configuration["ShopManageServer:GetShopListByIdsAsync"], request);
            return response;
        }
        public async Task<ApiPagedResult<ShopSimpleInfoResponse>> GetShopListAsync(GetShopListRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var response = await client.PostAsJsonAsync<GetShopListRequest, ApiPagedResult<ShopSimpleInfoResponse>>(configuration["ShopManageServer:GetShopListAsync"], request);
            return response;
        }
        public async Task<ApiResult<List<ShopSimpleInfoResponse>>> GetShopWareHouseListAsync(GetShopListRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var response = await client.PostAsJsonAsync<GetShopListRequest, ApiResult<List<ShopSimpleInfoResponse>>>(configuration["ShopManageServer:GetShopWareHouseListAsync"], request);
            return response;
        }
    }
}
