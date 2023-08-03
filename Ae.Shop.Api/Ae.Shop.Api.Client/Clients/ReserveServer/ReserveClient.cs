using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Model.Reserve;
using Ae.Shop.Api.Client.Request.Reserve;
using Ae.Shop.Api.Common.Exceptions;
using Ae.Shop.Api.Core.Model.Order;
using Ae.Shop.Api.Core.Model.Reserve;
using Ae.Shop.Api.Core.Request.Order;
using Ae.Shop.Api.Core.Request.Reserve;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Client.Clients.ReserveServer
{
    public class ReserveClient : IReserveClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly ApolloErpLogger<ReserveClient> logger;

        public ReserveClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<ReserveClient> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.logger = logger;
        }

        /// <summary>
        /// 预约列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ShopReserveListDto>> GetReserveListPage(ReserveListPageClientRequest request)
        {
            var client = httpClientFactory.CreateClient("ReceiveServer");
            ApiPagedResult<ShopReserveListDto> result =
                await client
                    .GetAsJsonAsync<ReserveListPageClientRequest, ApiPagedResult<ShopReserveListDto>>(
                        configuration["ReceiveServer:GetReserveListPage"], request);
            if (result?.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Warn($"GetReserveListPage_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        /// <summary>
        /// 预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ReserveDetailForWebDto> GetReserveDetailForWeb(ReserveDetailForWebClientRequest request)
        {
            var client = httpClientFactory.CreateClient("ReceiveServer");
            ApiResult<ReserveDetailForWebDto> result =
                await client
                    .GetAsJsonAsync<ReserveDetailForWebClientRequest, ApiResult<ReserveDetailForWebDto>>(
                        configuration["ReceiveServer:GetReserveDetailForWeb"], request);
            if (result?.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Warn($"GetReserveDetailForWeb_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        /// <summary>
        /// 预约主表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<ShopReserveDTO>> GetShopReserveDO(ReserveDetailRequest request)
        {
            var client = httpClientFactory.CreateClient("ReceiveServer");
            var response =
                 await client
                    .GetAsJsonAsync<ReserveDetailRequest, ApiResult<ShopReserveDTO>>(
                        configuration["ReceiveServer:GetShopReserveDO"], request);
            return response;
        }

        /// <summary>
        /// 根据预约id或订单号查询预约关联订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<ShopReserveOrderDTO>>> GetReserveInfoByReserveIdOrOrderNum(GetReserveInfoByReserveIdOrOrderNum request)
        {
            var client = httpClientFactory.CreateClient("ReceiveServer");
            var response =
                 await client
                    .PostAsJsonAsync<GetReserveInfoByReserveIdOrOrderNum, ApiResult<List<ShopReserveOrderDTO>>>(
                        configuration["ReceiveServer:GetReserveInfoByReserveIdOrOrderNum"], request);
            return response;
        }

        /// <summary>
        /// 预约时间看板 Web端
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReserveDateDto>> GetReserveDateForWeb(ReserveDateClientRequest request)
        {
            var client = httpClientFactory.CreateClient("ReceiveServer");
            ApiResult<List<ReserveDateDto>> result =
                await client
                    .GetAsJsonAsync<ReserveDateClientRequest, ApiResult<List<ReserveDateDto>>>(
                        configuration["ReceiveServer:GetReserveDateForWeb"], request);
            if (result?.Code == ResultCode.Success)
            {
                return result.Data ?? new List<ReserveDateDto>();
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Warn($"GetReserveDateForWeb_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> CancelReserveV2(CancelReserveClientRequest request)
        {
            var client = httpClientFactory.CreateClient("ReceiveServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<CancelReserveClientRequest, ApiResult<bool>>(
                    configuration["ReceiveServer:CancelReserveV2"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Warn($"CancelReserveV2_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        /// <summary>
        /// 根据手机号查询用户有效预约
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReserveListDto>> GetValidReserve(ValidReserveClientReserve request)
        {
            var client = httpClientFactory.CreateClient("ReceiveServer");
            ApiResult<List<ReserveListDto>> result =
                await client.GetAsJsonAsync<ValidReserveClientReserve, ApiResult<List<ReserveListDto>>>(
                    configuration["ReceiveServer:GetValidReserve"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<ReserveListDto>();
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Warn($"GetValidReserve_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        /// <summary>
        /// 编辑预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditReserve(EditReserveClientRequest request)
        {
            var client = httpClientFactory.CreateClient("ReceiveServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<EditReserveClientRequest, ApiResult<bool>>(
                    configuration["ReceiveServer:EditReserve"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Info($"EditReserve_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        /// <summary>
        /// 新增预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> AddReserve(AddReserveClientRequest request)
        {
            var client = httpClientFactory.CreateClient("ReceiveServer");
            ApiResult<long> result =
                await client.PostAsJsonAsync<AddReserveClientRequest, ApiResult<long>>(
                    configuration["ReceiveServer:AddReserveForShop"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Info($"AddReserve_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }
    }
}
