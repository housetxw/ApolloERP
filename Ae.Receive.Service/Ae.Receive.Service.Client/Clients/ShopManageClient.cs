using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Client.Inteface;
using Ae.Receive.Service.Client.Model.Shop;
using Ae.Receive.Service.Client.Request;
using Ae.Receive.Service.Client.Response;
using Ae.Receive.Service.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Ae.Receive.Service.Client.Response.Shop;
using Ae.Receive.Service.Client.Request.Shop;
using Ae.Receive.Service.Core.Request.Arrival;

namespace Ae.Receive.Service.Client.Clients
{
    public class ShopManageClient : IShopManageClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        private readonly ApolloErpLogger<ShopManageClient> _logger;
        public ShopManageClient(IHttpClientFactory clientFactory, IConfiguration configuration,ApolloErpLogger<ShopManageClient> logger)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }
        /// <summary>
        /// 获取门店简单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetShopSimpleInfoClientResponse> GetShopSimpleInfo(GetShopSimpleInfoClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult<GetShopSimpleInfoClientResponse> result = await client.GetAsJsonAsync<GetShopSimpleInfoClientRequest, ApiResult<GetShopSimpleInfoClientResponse>>(configuration["ShopManageServer:GetShopSimpleInfoAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"GetShopSimpleInfoAsync_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }
        /// <summary>
        /// 查询门店简单信息-列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopSimpleInfoDTO>> GetShopSimpleList(GetShopListClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");

            ApiPagedResult<ShopSimpleInfoDTO> result = await client.PostAsJsonAsync<GetShopListClientRequest, ApiPagedResult<ShopSimpleInfoDTO>>(configuration["ShopManageServer:GetShopListAsync"], request);

            if (result.IsNotNullSuccess())
            {
                return result.Data.Items.ToList();
            }
            else
            {
                _logger.Info($"GetShopSimpleInfoByIdsAsync_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }
        /// <summary>
        /// 查询门店开通的服务大类
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<BaseServiceDTO>> GetShopServiceCategory(GetShopSimpleInfoClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");

            ApiResult<List<BaseServiceDTO>> result = await client.GetAsJsonAsync<GetShopSimpleInfoClientRequest, ApiResult<List<BaseServiceDTO>>>(configuration["ShopManageServer:GetShopServiceCategory"], request);
            return result.Data;
        }

        /// <summary>
        /// 用户关联门店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddShopUserRelation(AddShopUserRelationRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");

            ApiResult<bool> result =
                await client.PostAsJsonAsync<AddShopUserRelationRequest, ApiResult<bool>>(
                    configuration["ShopManageServer:AddShopUserRelation"], request);

            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"AddShopUserRelation_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 得到服务类型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<GetShopServiceTypeAsyncResponse>> GetShopServiceTypeAsync(GetShopServiceTypeAsyncRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");

            ApiResult<List<GetShopServiceTypeAsyncResponse>> result = await
                client.GetAsJsonAsync<GetShopServiceTypeAsyncRequest, ApiResult<List<GetShopServiceTypeAsyncResponse>>>(configuration["ShopManageServer:GetShopServiceTypeAsync"], request);
            return result.Data;
        }

        /// <summary>
        /// 更新客户最后到店时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserLastReceiveTime(UpdateUserLastReceiveTimeRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult result = await client.PostAsJsonAsync<UpdateUserLastReceiveTimeRequest, ApiResult>(configuration["ShopManageServer:UpdateUserLastReceiveTime"], request);
            if (result.IsNotNullSuccess())
            {
                return true;
            }
            else
            {
                _logger.Info($"UpdateUserLastReceiveTime {result?.Message}");
                return false;
                // throw new CustomException(result.Message);
            }
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

        /// <summary>
        /// 门店列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopListDto>> GetShopListByIdsAsync(ShopListByIdsAsyncClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult<List<ShopListDto>> result =
                await client.PostAsJsonAsync<ShopListByIdsAsyncClientRequest, ApiResult<List<ShopListDto>>>(
                    configuration["ShopManageServer:GetShopListByIdsAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                string errorMsg = result?.Message ?? "系统异常";
                _logger.Warn($"GetShopListByIdsAsync_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        public async Task<List<GetTechnicianPageResponse>> GetShopEmployeeByJobIdPage(GetShopEmployeeByJobIdPageRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");

            ApiPagedResult<GetTechnicianPageResponse> result =
                await client.PostAsJsonAsync<GetShopEmployeeByJobIdPageRequest, ApiPagedResult<GetTechnicianPageResponse>>(
                    configuration["ShopManageServer:GetShopEmployeeByJobIdPage"], request);

            return result?.Data?.Items?.ToList();
        }
    }
}
