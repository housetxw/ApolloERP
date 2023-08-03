using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Model.WMS;
using Ae.ShopOrder.Service.Client.Request.WMS;
using Ae.ShopOrder.Service.Core.Model.Order;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Response.Order;

namespace Ae.ShopOrder.Service.Client.Clients.WMS.Impl
{
    /// <summary>
    /// 实现WMS 
    /// </summary>
    public class WMSClient:IWMSClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private IConfiguration _configuration { get; }

        public WMSClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }
        /// <summary>
        ///  得到WMS出库信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetWareHouseTransferClientDTO>> GetWarehouseTransferAllTask(GetWareHouseTransferClientRequest request)
        {
            var client = _clientFactory.CreateClient("WMSServer");

            var response =
                await client
                    .GetAsJsonAsync<GetWareHouseTransferClientRequest, ApiResult<GetWareHouseTransferClientDTO>>(
                        _configuration["WMSServer:GetWareHouseTransferAll"], request);

            return response;
        }

        /// <summary>
        /// 得到物流信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<GetWareHouseTransferPackageClientDTO>>> GetTransferPackageTask(GetWareHouseTransferPackageClientRequest request)
        {
            var client = _clientFactory.CreateClient("WMSServer");

            var response =
                await client.GetAsJsonAsync<GetWareHouseTransferPackageClientRequest, ApiResult<List<GetWareHouseTransferPackageClientDTO>>>(
                        _configuration["WMSServer:GetWareHouseTransferPackage"], request);

            return response;
        }

        /// <summary>
        /// 签收接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateWarehouseTransferSignUp(UpdateWarehouseTransferSignUpClientRequest request)
        {
            var client = _clientFactory.CreateClient("WMSServer");

            var response =
                await client.PostAsJsonAsync<UpdateWarehouseTransferSignUpClientRequest, ApiResult>(
                    _configuration["WMSServer:UpdateWarehouseTransferSignUp"], request);

            return response;
        }

        /// <summary>
        /// 入库接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateWarehouseTransferProductReceiveNum(UpdateWarehouseTransferProductReceiveNumClientRequest request)
        {
            var client = _clientFactory.CreateClient("WMSServer");

            var response =
                await client.PostAsJsonAsync<UpdateWarehouseTransferProductReceiveNumClientRequest, ApiResult>(
                    _configuration["WMSServer:UpdateWarehouseTransferProductReceiveNum"], request);

            return response;
        }

        /// <summary>
        /// 更新包裹签收状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateWarehouseTransferPackageStatus(UpdateWarehouseTransferPackageStatusClientRequest request)
        {
            var client = _clientFactory.CreateClient("WMSServer");

            var response =
                await client.PostAsJsonAsync<UpdateWarehouseTransferPackageStatusClientRequest, ApiResult>(
                    _configuration["WMSServer:UpdateWarehouseTransferPackageStatus"], request);

            return response;
        }

        /// <summary>
        /// 批量的得到物流信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<GetBatchWarehouseTransferPackagesDTO>>> GetBatchWarehouseTransferPackages(GetBatchWarehouseTransferPackagesRequest request)
        {
            var client = _clientFactory.CreateClient("WMSServer");

            var response =
                await client.PostAsJsonAsync<GetBatchWarehouseTransferPackagesRequest, ApiResult<List<GetBatchWarehouseTransferPackagesDTO>>>(
                    _configuration["WMSServer:GetBatchWarehouseTransferPackages"], request);

            return response;
        }

        public async Task<ApiResult<List<GetShopOccupyMappingsByRelationIdResponse>>> GetShopOccupyMappingsByRelationId(GetShopOccupyMappingsByRelationIdRequest request)
        {
            var client = _clientFactory.CreateClient("WMSServer");

            var response =
                await client.GetAsJsonAsync<GetShopOccupyMappingsByRelationIdRequest, ApiResult<List<GetShopOccupyMappingsByRelationIdResponse>>>(
                    _configuration["WMSServer:GetShopOccupyMappingsByRelationId"], request);

            return response;
        }

        public async Task<ApiResult<List<GetOrderOccupyShopProductPurchaseResponse>>> GetOrderOccupyShopProductPurchaseInfo(GetOrderOccupyShopProductPurchaseInfoReqeust reqeust)
        {
            var client = _clientFactory.CreateClient("WMSServer");

            var response =
                await client.PostAsJsonAsync<GetOrderOccupyShopProductPurchaseInfoReqeust, ApiResult<List<GetOrderOccupyShopProductPurchaseResponse>>>(
                    _configuration["WMSServer:GetOrderOccupyShopProductPurchaseInfo"], reqeust);

            return response;
        }
    }
}
