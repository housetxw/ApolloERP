using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Model;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Response;
using Ae.C.MiniApp.Api.Common.Exceptions;
using Ae.C.MiniApp.Api.Core.Request.Arrival;
using Ae.C.MiniApp.Api.Core.Request.Reserve;
using Ae.C.MiniApp.Api.Core.Request.Service;
using Ae.C.MiniApp.Api.Core.Response.Arrival;
using Ae.C.MiniApp.Api.Core.Response.Reserve;
using Ae.C.MiniApp.Api.Core.Response.Service;

namespace Ae.C.MiniApp.Api.Client.Clients
{
    public class ReceiveClient : IReceiveClient
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ApolloErpLogger<ReceiveClient> _logger;
        private IConfiguration configuration { get; }
        public ReceiveClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<ReceiveClient> logger
            )
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 判断我的预约跳转类型
        /// 1：有预约记录 2：无订单预约 3：一条订单预约 4：多条订单预约 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<JudgeMyReserveClientResponse> JudgeMyReserve(JudgeMyReserveClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.GetAsJsonAsync<JudgeMyReserveClientRequest, ApiResult<JudgeMyReserveClientResponse>>(configuration["ReceiveServer:JudgeMyReserve"], request);
            if (response != null && response.Code == ResultCode.Success)
            {
                return response.Data;
            }
            else
            {
                _logger.Warn($"JudgeMyReserve_Error {response?.Message}");
                throw new CustomException(response?.Message);
            }
        }

        /// <summary>
        /// 可预约订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<CanReserveOrderDTO>> CanReserveOrder(CanReserveOrderClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            try
            {
                var response = await client.GetAsJsonAsync<CanReserveOrderClientRequest, ApiPagedResult<CanReserveOrderDTO>>(configuration["ReceiveServer:CanReserveOrderList"], request);
                if (response != null && response.Code == ResultCode.Success)
                {
                    return response.Data;
                }
                else
                {
                    _logger.Warn($"CanReserveOrderList_Error {response?.Message}");
                    throw new CustomException(response?.Message);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        /// <summary>
        /// 初始化预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ReserveInitialClientResponse> ReserveInitial(ReserveInitialClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.GetAsJsonAsync<ReserveInitialClientRequest, ApiResult<ReserveInitialClientResponse>>(configuration["ReceiveServer:ReserveInitial"], request);
            if (response.IsNotNullSuccess() && response.Code == ResultCode.Success)
            {
                return response.Data;
            }
            else
            {
                _logger.Warn($"ReserveInitial_Error {response?.Message}");
                throw new CustomException(response?.Message);
            }

        }
        /// <summary>
        /// 新增预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> AddShopReserveAsync(AddReserveClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.PostAsJsonAsync<AddReserveClientRequest, ApiResult>(configuration["ReceiveServer:AddShopReserveAsync"], request);
            if (response.IsNotNullSuccess() && response.Code == ResultCode.Success)
            {
                return response;
            }
            else
            {
                _logger.Warn($"AddShopReserveAsync_Error {response?.Message}");
                throw new CustomException(response?.Message);
            }
        }
        /// <summary>
        /// 新增预约V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<long>> AddReserveV2Async(AddReserveV2ClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.PostAsJsonAsync<AddReserveV2ClientRequest, ApiResult<long>>(configuration["ReceiveServer:AddReserveV2Async"], request);
            if (response.IsNotNullSuccess() && response.Code == ResultCode.Success)
            {
                return response;
            }
            else
            {
                _logger.Warn($"AddShopReserveAsync_Error {response?.Message}");
                throw new CustomException(response?.Message);
            }
        }

        /// <summary>
        /// 新增预约V3
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> AddShopReserveV3(AddShopReserveV3ClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.PostAsJsonAsync<AddShopReserveV3ClientRequest, ApiResult>(configuration["ReceiveServer:AddShopReserveV3"], request);
            return response;
        }

        /// <summary>
        /// 获取预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetReserveInfoClientResponse> GetReserveInfo(GetReserveInfoClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.GetAsJsonAsync<GetReserveInfoClientRequest, ApiResult<GetReserveInfoClientResponse>>(configuration["ReceiveServer:GetReserveInfo"], request);
            if (response.IsNotNullSuccess() && response.Code == ResultCode.Success)
            {
                return response.Data;
            }
            else
            {
                _logger.Warn($"GetReserveInfo_Error {response?.Message}");
                throw new CustomException(response?.Message);
            }

        }


        /// <summary>
        /// 修改预约时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ModifyReserve(ModifyReserveClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.PostAsJsonAsync<ModifyReserveClientRequest, ApiResult<bool>>(configuration["ReceiveServer:ModifyReserve"], request);
            if (response.IsNotNullSuccess() && response.Code == ResultCode.Success)
            {
                return response.Data;
            }
            else
            {
                _logger.Warn($"ModifyReserve_Error {response?.Message}");
                throw new CustomException(response?.Message);
            }
        }

        /// <summary>
        /// 修改预约时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> ModifyReserveTime(ModifyReserveTimeClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.PostAsJsonAsync<ModifyReserveTimeClientRequest, ApiResult>(configuration["ReceiveServer:ModifyReserveTime"], request);
            return response;
        }

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> CancelReserve(CancelReserveClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.PostAsJsonAsync<CancelReserveClientRequest, ApiResult<bool>>(configuration["ReceiveServer:CancelReserve"], request);
            if (response.IsNotNullSuccess() && response.Code == ResultCode.Success)
            {
                return response.Data;
            }
            else
            {
                _logger.Warn($"CancelReserve_Error {response?.Message}");
                throw new CustomException(response?.Message);
            }
        }

        /// <summary>
        /// 取消预约V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> CancelReserveV2(CancelReserveClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.PostAsJsonAsync<CancelReserveClientRequest, ApiResult<bool>>(configuration["ReceiveServer:CancelReserveV2"], request);
            if (response.IsNotNullSuccess() && response.Code == ResultCode.Success)
            {
                return response.Data;
            }
            else
            {
                _logger.Warn($"CancelReserve_Error {response?.Message}");
                throw new CustomException(response?.Message);
            }
        }

        /// <summary>
        /// 获取预约详情时间节点
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ReserveTimeDTO>> CanReserveTime(CanReserveTimeClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.GetAsJsonAsync<CanReserveTimeClientRequest, ApiResult<List<ReserveTimeDTO>>>(configuration["ReceiveServer:CanReserveTime"], request);
            if (response != null && response.Code == ResultCode.Success)
            {
                return response.Data;
            }
            else
            {
                _logger.Warn($"CanReserveTime_Error {response?.Message}");
                throw new CustomException(response?.Message);
            }

        }

        /// <summary>
        /// 获取预约详情时间节点V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ReserveTimeDTO>> CanReserveTimeV2(CanReserveTimeClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.GetAsJsonAsync<CanReserveTimeClientRequest, ApiResult<List<ReserveTimeDTO>>>(configuration["ReceiveServer:CanReserveTimeV2"], request);
            if (response != null && response.Code == ResultCode.Success)
            {
                return response.Data;
            }
            else
            {
                _logger.Warn($"CanReserveTimeV2_Error {response?.Message}");
                throw new CustomException(response?.Message);
            }

        }

        /// <summary>
        /// 获取预约详情时间节点
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ReserveTimeDTO>> CanReserveTimeV3(CanReserveTimeV3ClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.GetAsJsonAsync<CanReserveTimeV3ClientRequest, ApiResult<List<ReserveTimeDTO>>>(configuration["ReceiveServer:CanReserveTimeV3"], request);
            if (response != null && response.Code == ResultCode.Success)
            {
                return response.Data;
            }
            else
            {
                _logger.Warn($"CanReserveTime_Error {response?.Message}");
                throw new CustomException(response?.Message);
            }

        }


        /// <summary>
        /// 已预约列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ReservedInfoDTO>> ReservedList(ReservedListClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.PostAsJsonAsync<ReservedListClientRequest, ApiPagedResult<ReservedInfoDTO>>(configuration["ReceiveServer:ReservedList"], request);
            if (response.IsNotNullSuccess())
            {
                return response.Data;
            }
            else
            {
                _logger.Warn($"ReservedList_Error {response?.Message}");
                throw new CustomException(response?.Message);
            }
        }
        /// <summary>
        /// 预约列表v2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ReservedListV2DTO>> ReserveListV2(ReserveListV2ClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.PostAsJsonAsync<ReserveListV2ClientRequest, ApiPagedResult<ReservedListV2DTO>>(configuration["ReceiveServer:ReserveListV2"], request);
            if (response.IsNotNullSuccess())
            {
                return response.Data;
            }
            else
            {
                _logger.Warn($"ReserveListV2_Error {response?.Message}");
                throw new CustomException(response?.Message);
            }
        }

        /// <summary>
        /// 快速排队接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<QuickQueueResponse>> QuickQueue(QuickQueueRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.GetAsJsonAsync<QuickQueueRequest, ApiResult<QuickQueueResponse>>(configuration["ReceiveServer:QuickQueue"], request);
            return response;
        }

        /// <summary>
        /// 快速排队请求弹层
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<QuickTakeNumberLayerResponse>> QuickTakeNumberLayer(ApiRequest<QuickTakeNumberLayerRequest> request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.PostAsJsonAsync<ApiRequest<QuickTakeNumberLayerRequest>, ApiResult<QuickTakeNumberLayerResponse>>(configuration["ReceiveServer:QuickTakeNumberLayer"], request);
            return response;
        }

        /// <summary>
        /// 排队仅拿号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<QuickTakeNumberLayerResponse>> QuickTakeNumber(ApiRequest<QuickTakeNumberRequest> request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.PostAsJsonAsync<ApiRequest<QuickTakeNumberRequest>, ApiResult<QuickTakeNumberLayerResponse>>(configuration["ReceiveServer:QuickTakeNumber"], request);
            return response;
        }

        /// <summary>
        /// 预约排队拿号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<QuickTakeNumberLayerResponse>> ReserveTakeNumber(ApiRequest<ReserveTakeNumberRequest> request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.PostAsJsonAsync<ApiRequest<ReserveTakeNumberRequest>, ApiResult<QuickTakeNumberLayerResponse>>(configuration["ReceiveServer:ReserveTakeNumber"], request);
            return response;
        }

        /// <summary>
        /// 得到服务记录信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetServiceRecordResponse>> GetServiceRecord(GetServiceRecordRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.GetAsJsonAsync<GetServiceRecordRequest, ApiResult<GetServiceRecordResponse>>(configuration["ReceiveServer:GetServiceRecord"], request);
            return response;
        }

        /// <summary>
        /// 获取可预约日期
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<CanReserveDateClientResponse>> CanReserveDate(BaseShopClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.GetAsJsonAsync<BaseShopClientRequest, ApiResult<CanReserveDateClientResponse>>(configuration["ReceiveServer:CanReserveDate"], request);
            return response;
        }
        /// <summary>
        /// 重新预约获取预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetRebookReserveClientResponse>> GetReserveInfoV2(GetReserveInfoClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.GetAsJsonAsync<GetReserveInfoClientRequest, ApiResult<GetRebookReserveClientResponse>>(configuration["ReceiveServer:GetReserveInfoV2"], request);
            return response;
        }

        /// <summary>
        ///  支付列表ForMini
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<PayListResponse>> PayListForMini(PayListForMiniRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.GetAsJsonAsync<PayListForMiniRequest, ApiResult<PayListResponse>>(configuration["ReceiveServer:PayListForMini"], request);
            return response;
        }

        /// <summary>
        /// 判断是否需要唤醒收银台ForMini
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> CheckIsNeedPayControlForMini(PayListForMiniRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.GetAsJsonAsync<PayListForMiniRequest, ApiResult<bool>>(configuration["ReceiveServer:CheckIsNeedPayControlForMini"], request);
            return response;
        }


        /// <summary>
        /// 获取上门养车门店预约日期+时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<ReserveDateDTO>>> GetShopReserveDateTime(BaseShopClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.GetAsJsonAsync<BaseShopClientRequest, ApiResult<List<ReserveDateDTO>>>(configuration["ReceiveServer:GetShopReserveDateTime"], request);
            return response;
        }
        /// <summary>
        /// 门店预约日历
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetReserveSurplusClientResponse>> GetShopReserveSurplus(BaseShopClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.GetAsJsonAsync<BaseShopClientRequest, ApiResult<GetReserveSurplusClientResponse>>(configuration["ReceiveServer:GetShopReserveSurplus"], request);
            return response;
        }

        /// <summary>
        /// 获取预约详情V3
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetReserveInfoV3ClientResponse> GetReserveInfoV3(GetReserveInfoV3ClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            try
            {
                var response = await client.GetAsJsonAsync<GetReserveInfoV3ClientRequest, ApiResult<GetReserveInfoV3ClientResponse>>(configuration["ReceiveServer:GetReserveInfoV3"], request);
                if (response.IsNotNullSuccess())
                {
                    return response.Data;
                }
                else
                {
                    _logger.Warn($"GetReserveInfo_Error {response?.Message}");
                    throw new CustomException(response?.Message);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
