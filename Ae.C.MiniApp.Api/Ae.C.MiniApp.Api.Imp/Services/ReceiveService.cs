using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Model;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Core.Enums;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.Arrival;
using Ae.C.MiniApp.Api.Core.Request.Reserve;
using Ae.C.MiniApp.Api.Core.Request.Service;
using Ae.C.MiniApp.Api.Core.Response;
using Ae.C.MiniApp.Api.Core.Response.Arrival;
using Ae.C.MiniApp.Api.Core.Response.Reserve;
using Ae.C.MiniApp.Api.Core.Response.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class ReceiveService : IReceiveService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<ReceiveService> logger;
        private readonly IReceiveClient receiveClient;
        private readonly IIdentityService identityService;

        public ReceiveService(IMapper mapper, ApolloErpLogger<ReceiveService> logger, 
            IReceiveClient receiveClient,
            IIdentityService identityService
            )
        {
            this.mapper = mapper;
            this.logger = logger;
            this.receiveClient = receiveClient;
            this.identityService = identityService;
        }

        /// <summary>
        /// 判断我的预约跳转类型
        /// 1：有预约记录 2：无订单预约 3：一条订单预约 4：多条订单预约
        /// </summary>
        /// <returns></returns>
        public async Task<JudgeMyReserveResponse> JudgeMyReserve() 
        {
            var clientRequest = new JudgeMyReserveClientRequest() 
            {
                UserId = identityService.GetUserId()
            };
            var clientResponse = await receiveClient.JudgeMyReserve(clientRequest);
            var response = mapper.Map<JudgeMyReserveResponse>(clientResponse);
            return response;
        }

        /// <summary>
        /// 可预约订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<CanReserveOrderVO>> CanReserveOrderList(BasePageRequest request)
        {
            var clientRequest = new CanReserveOrderClientRequest()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };
            clientRequest.UserId = identityService.GetUserId();
            var clientResponse = await receiveClient.CanReserveOrder(clientRequest);
            var response = mapper.Map<ApiPagedResultData<CanReserveOrderVO>>(clientResponse);
            return response;
        }

        /// <summary>
        /// 初始化预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ReserveInitialResponse> ReserveInitial(ReserveInitialRequest request) 
        {
            var clientRequest = new ReserveInitialClientRequest()
            {
                UserId = identityService.GetUserId(),
                OrderNo = request.OrderNo

            };
            var clientResponse = await receiveClient.ReserveInitial(clientRequest);
            var response = mapper.Map<ReserveInitialResponse>(clientResponse);
            return response;
        }
        /// <summary>
        /// 新增预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> AddShopReserveAsync(AddReserveRequest request) 
        {
            var clientRequest = mapper.Map<AddReserveClientRequest>(request);
            clientRequest.UserId = identityService.GetUserId();
            var clientResponse = await receiveClient.AddShopReserveAsync(clientRequest);
            return clientResponse;
        }

        /// <summary>
        /// 新增预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> AddShopReserveV3(AddReserveV3Request request)
        {
            var clientRequest = mapper.Map<AddShopReserveV3ClientRequest>(request);
            clientRequest.UserId = identityService.GetUserId();
            var clientResponse = await receiveClient.AddShopReserveV3(clientRequest);
            return clientResponse;
        }

        /// <summary>
        /// 获取预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetReserveInfoResponse> GetReserveInfo(GetReserveInfoRequest request)
        {
            var clientRequest = mapper.Map<GetReserveInfoClientRequest>(request);
            var clientResponse = await receiveClient.GetReserveInfo(clientRequest);
            var response = mapper.Map<GetReserveInfoResponse>(clientResponse);
            return response;
        }

        /// <summary>
        /// 修改预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ModifyReserve(ModifyReserveRequest request)
        {
            var clientRequest = mapper.Map<ModifyReserveClientRequest>(request);
            clientRequest.UserId = identityService.GetUserId();
            clientRequest.UpdateBy = identityService.GetUserName();
            var clientResponse = await receiveClient.ModifyReserve(clientRequest);
            return clientResponse;
        }

        /// <summary>
        /// 修改预约时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> ModifyReserveTime(ModifyReserveTimeRequest request)
        {
            var clientRequest = mapper.Map<ModifyReserveTimeClientRequest>(request);
            clientRequest.UserId = identityService.GetUserId();
            clientRequest.UpdateBy = identityService.GetUserName();
            return await receiveClient.ModifyReserveTime(clientRequest);
        }

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> CancelReserve(CancelReserveRequest request) 
        {
            var clientRequest = new CancelReserveClientRequest()
            {
                ReserveId = request.ReserveId,
                CancelReason = request.CancelReason,
                UpdateBy = identityService.GetUserName()
            };
            var clientResponse = await receiveClient.CancelReserve(clientRequest);
            return clientResponse;
        }
        /// <summary>
        /// 获取可预约日期
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CanReserveDateResponse> CanReserveDate(BaseShopRequest request) 
        {
            var clientRequest = new BaseShopClientRequest() {ShopId = request.ShopId };
            var clientResponse = await receiveClient.CanReserveDate(clientRequest);
            var response = mapper.Map<CanReserveDateResponse>(clientResponse.Data);
            return response;
        }

        /// <summary>
        /// 获取预约详情时间节点
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ReserveTimeVO>> CanReserveTime(CanReserveTimeRequest request)
        {
            var clientRequest = mapper.Map<CanReserveTimeClientRequest>(request);
            var clientResponse = await receiveClient.CanReserveTime(clientRequest);
            var response = mapper.Map<List<ReserveTimeVO>>(clientResponse);
            return response;
        }
        /// <summary>
        /// 获取预约详情时间节点V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ReserveTimeVO>> CanReserveTimeV2(CanReserveTimeRequest request)
        {
            var clientRequest = mapper.Map<CanReserveTimeClientRequest>(request);
            var clientResponse = await receiveClient.CanReserveTimeV2(clientRequest);
            var response = mapper.Map<List<ReserveTimeVO>>(clientResponse);
            return response;
        }

        /// <summary>
        /// 获取预约时间节点V3
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ReserveTimeVO>> CanReserveTimeV3(CanReserveTimeV3Request request)
        {
            var clientRequest = mapper.Map<CanReserveTimeV3ClientRequest>(request);
            clientRequest.Type = 1;
            var clientResponse = await receiveClient.CanReserveTimeV3(clientRequest);
            var response = mapper.Map<List<ReserveTimeVO>>(clientResponse);
            return response;
        }

        /// <summary>
        /// 已预约列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ReservedListResponse>> ReservedList(BasePageRequest request)
        {
            ApiPagedResultData<ReservedListResponse> response = new ApiPagedResultData<ReservedListResponse>();
            var clientRequest = new ReservedListClientRequest()
            {
                UserId = identityService.GetUserId(),
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };
            var clientResponse = await receiveClient.ReservedList(clientRequest);
            if (clientResponse != null)
            {
                response.Items = mapper.Map<List<ReservedListResponse>>(clientResponse.Items);
                response.TotalItems = clientResponse.TotalItems;
            }
            return response;
        }
        /// <summary>
        /// 预约列表V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ReservedListV2Response>> ReserveListV2(ReservedListRequest request) 
        {
            ApiPagedResultData<ReservedListV2DTO> response = new ApiPagedResultData<ReservedListV2DTO>();
            var clientRequest = new ReserveListV2ClientRequest()
            {
                UserId = identityService.GetUserId(),
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Type = request.Type
            };
            var clientResponse = await receiveClient.ReserveListV2(clientRequest);
            return mapper.Map<ApiPagedResultData<ReservedListV2Response>>(clientResponse);
        }
        /// <summary>
        /// 新增预约V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<long>> AddReserveV2Async(AddReserveNewYearRequest request)
        {
            var clientRequest = mapper.Map<AddReserveV2ClientRequest>(request);
            clientRequest.UserId = identityService.GetUserId();
            var clientResponse = await receiveClient.AddReserveV2Async(clientRequest);
            return clientResponse;
        }

        /// <summary>
        /// 取消预约V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> CancelReserveV2(CancelReserveRequest request) 
        {
            var clientRequest = mapper.Map<CancelReserveClientRequest>(request);
            clientRequest.UpdateBy = identityService.GetUserName();
            var clientResponse = await receiveClient.CancelReserveV2(clientRequest);
            return clientResponse;
        }
        /// <summary>
        /// 重新预约获取预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetRebookReserveResponse>> GetReserveInfoV2(GetReserveInfoRequest request)
        {
            var clientRequest = new GetReserveInfoClientRequest { ReserveId = request.ReserveId};
            var clientResponse = await receiveClient.GetReserveInfoV2(clientRequest);
            var response = mapper.Map<GetRebookReserveResponse>(clientResponse.Data);
            return new ApiResult<GetRebookReserveResponse>
            {
                Data = response,
                Code = clientResponse.Code,
                Message = clientResponse.Message
            };
        }

        /// <summary>
        /// 快速排队接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<ApiResult<QuickQueueResponse>> QuickQueue(QuickQueueRequest request)
        {
            request.UserId = identityService.GetUserId();
            return receiveClient.QuickQueue(request);
        }

        /// <summary>
        /// 快速排队请求弹层
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<ApiResult<QuickTakeNumberLayerResponse>> QuickTakeNumberLayer(ApiRequest<QuickTakeNumberLayerRequest> request)
        {
            request.Data.UserId=identityService.GetUserId();
            //int.TryParse(identityService.GetOrganizationId(), out var shopId);
           // request.Data.ShopId = shopId;
            return receiveClient.QuickTakeNumberLayer(request);
        }

        /// <summary>
        /// 排队仅拿号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<ApiResult<QuickTakeNumberLayerResponse>> QuickTakeNumber(ApiRequest<QuickTakeNumberRequest> request)
        {
            request.Data.UserId = identityService.GetUserId();
            return receiveClient.QuickTakeNumber(request);
        }

        /// <summary>
        /// 预约排队拿号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<ApiResult<QuickTakeNumberLayerResponse>> ReserveTakeNumber(ApiRequest<ReserveTakeNumberRequest> request)
        {
            request.Data.UserId = identityService.GetUserId();
            return receiveClient.ReserveTakeNumber(request);
        }

        /// <summary>
        /// 得到服务记录信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<ApiResult<GetServiceRecordResponse>> GetServiceRecord(GetServiceRecordRequest request)
        {
            request.UserId = identityService.GetUserId();
            return receiveClient.GetServiceRecord(request);
        }
        /// <summary>
        /// 支付列表ForMini
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<PayListResponse>> PayListForMini(PayListForMiniRequest request)
        {
            return await receiveClient.PayListForMini(request);
        }
        /// <summary>
        /// 判断是否需要唤醒收银台ForMini
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> CheckIsNeedPayControlForMini(PayListForMiniRequest request)
        {
            return await receiveClient.CheckIsNeedPayControlForMini(request);
        }

        /// <summary>
        /// 获取上门养车门店预约日期+时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ReserveDateVO>> GetShopReserveDateTime(BaseShopRequest request)
        {
            var clientRequest = new BaseShopClientRequest() { ShopId = request.ShopId };
            var clientResponse = await receiveClient.GetShopReserveDateTime(clientRequest);
            return mapper.Map<List<ReserveDateVO>>(clientResponse.Data);
        }

        /// <summary>
        /// 门店预约日历
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetReserveSurplusResponse> GetShopReserveSurplus(BaseShopRequest request) 
        {
            var clientRequest = new BaseShopClientRequest() { ShopId = request.ShopId };
            var clientResponse = await receiveClient.GetShopReserveSurplus(clientRequest);
            return mapper.Map<GetReserveSurplusResponse> (clientResponse.Data);
        }

        /// <summary>
        /// 获取预约详情V3
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetReserveInfoV3Response> GetReserveInfoV3(GetReserveInfoV3Request request)
        {
            var clientRequest = mapper.Map<GetReserveInfoV3ClientRequest>(request);
            clientRequest.UserId = identityService.GetUserId();
            var clientResponse = await receiveClient.GetReserveInfoV3(clientRequest);
            var response = mapper.Map<GetReserveInfoV3Response>(clientResponse);
            return response;
        }
    }
}
