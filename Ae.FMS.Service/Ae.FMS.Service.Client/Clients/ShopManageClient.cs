using ApolloErp.Web.WebApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ae.FMS.Service.Client.Response;
using Ae.FMS.Service.Client.Request;
using Ae.FMS.Service.Client.Model;
namespace Ae.FMS.Service.Client.Clients
{
    public class ShopManageClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        public ShopManageClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        //public async Task<List<ShopSimpleInfoClientDTO>> GetShopSimpleInfoByIdsAsync(GetShopSimpleInfoByIdsRequest request)
        //{
        //    var client = clientFactory.CreateClient("ShopManageServer");
        //    var result = await client.PostAsJsonAsync<GetShopSimpleInfoByIdsRequest,
        //       ApiResult<List<ShopSimpleInfoClientDTO>>>(configuration["ShopManageServer:GetShopSimpleInfoByIdsAsync"], request);
        //    return result.Data;
        //}

        /// <summary>
        /// 得到门店配置信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetShopConfigClientDTO>> GetShopConfigInfo(int shopId)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            return
                 await client.GetAsJsonAsync<ApiResult<GetShopConfigClientDTO>>(
                     configuration["ShopManageServer:GetShopForBOSSAsync"], $"ShopId={shopId}");

        }

        public async Task<ApiPagedResult<ShopSimpleInfoClientDTO>> GetShopListAsync(GetShopListClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiPagedResult<ShopSimpleInfoClientDTO> result =
                await client.PostAsJsonAsync<GetShopListClientRequest, ApiPagedResult<ShopSimpleInfoClientDTO>>(
                    configuration["ShopManageServer:GetShopListAsync"], request);
            return result;
        }

        /// <summary>
        /// 获取门店主表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        public async Task<ApiResult<ShopDTO>> GetShopById( GetShopRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var result = await client.GetAsJsonAsync<GetShopRequest,
                ApiResult<ShopDTO>>(configuration["ShopManageServer:GetShopById"], request);
            return result;
        }

        public async Task<ApiResult<CompanyDTO>> GetCompanyInfoById(GetCompanyInfoByIdRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var result = await client.GetAsJsonAsync<GetCompanyInfoByIdRequest,
                ApiResult<CompanyDTO>>(configuration["ShopManageServer:GetCompanyInfoById"], request);
            return result;
        }

    }
}
