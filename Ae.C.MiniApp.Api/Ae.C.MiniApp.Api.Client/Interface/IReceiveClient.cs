using System.Collections.Generic;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Model;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Response;
using Ae.C.MiniApp.Api.Core.Request.Arrival;
using Ae.C.MiniApp.Api.Core.Request.Reserve;
using Ae.C.MiniApp.Api.Core.Request.Service;
using Ae.C.MiniApp.Api.Core.Response.Arrival;
using Ae.C.MiniApp.Api.Core.Response.Reserve;
using Ae.C.MiniApp.Api.Core.Response.Service;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    public interface IReceiveClient
    {
        Task<JudgeMyReserveClientResponse> JudgeMyReserve(JudgeMyReserveClientRequest request);
        Task<ApiPagedResultData<CanReserveOrderDTO>> CanReserveOrder(CanReserveOrderClientRequest request);
        Task<ReserveInitialClientResponse> ReserveInitial(ReserveInitialClientRequest request);
        Task<ApiResult> AddShopReserveAsync(AddReserveClientRequest request);
        Task<GetReserveInfoClientResponse> GetReserveInfo(GetReserveInfoClientRequest request);
        Task<bool> ModifyReserve(ModifyReserveClientRequest request);
        Task<bool> CancelReserve(CancelReserveClientRequest request);
        Task<List<ReserveTimeDTO>> CanReserveTime(CanReserveTimeClientRequest request);
        Task<List<ReserveTimeDTO>> CanReserveTimeV2(CanReserveTimeClientRequest request);
        Task<ApiPagedResultData<ReservedInfoDTO>> ReservedList(ReservedListClientRequest request);
        Task<ApiResult<long>> AddReserveV2Async(AddReserveV2ClientRequest request);
        Task<bool> CancelReserveV2(CancelReserveClientRequest request);
        Task<ApiPagedResultData<ReservedListV2DTO>> ReserveListV2(ReserveListV2ClientRequest request);

        /// <summary>
        /// 快速排队接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<QuickQueueResponse>> QuickQueue(QuickQueueRequest request);

        /// <summary>
        /// 快速排队请求弹层
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<QuickTakeNumberLayerResponse>> QuickTakeNumberLayer(ApiRequest<QuickTakeNumberLayerRequest> request);

        /// <summary>
        /// 排队仅拿号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<QuickTakeNumberLayerResponse>> QuickTakeNumber(ApiRequest<QuickTakeNumberRequest> request);

        /// <summary>
        /// 预约排队拿号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<QuickTakeNumberLayerResponse>> ReserveTakeNumber(ApiRequest<ReserveTakeNumberRequest> request);

        /// <summary>
        /// 得到服务记录信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetServiceRecordResponse>> GetServiceRecord(GetServiceRecordRequest request);

        Task<ApiResult<CanReserveDateClientResponse>> CanReserveDate(BaseShopClientRequest request);
        Task<ApiResult<GetRebookReserveClientResponse>> GetReserveInfoV2(GetReserveInfoClientRequest request);


        /// <summary>
        /// 支付列表ForMini
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<PayListResponse>> PayListForMini(PayListForMiniRequest request);

        /// <summary>
        /// 判断是否需要唤醒收银台ForMini
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> CheckIsNeedPayControlForMini(PayListForMiniRequest request);
        /// <summary>
        /// 获取上门养车门店预约日期+时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<ReserveDateDTO>>> GetShopReserveDateTime(BaseShopClientRequest request);
        /// <summary>
        /// 门店预约日历
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetReserveSurplusClientResponse>> GetShopReserveSurplus(BaseShopClientRequest request);

        Task<ApiResult> AddShopReserveV3(AddShopReserveV3ClientRequest request);
        Task<List<ReserveTimeDTO>> CanReserveTimeV3(CanReserveTimeV3ClientRequest request);
        Task<ApiResult> ModifyReserveTime(ModifyReserveTimeClientRequest request);

        /// <summary>
        /// 获取预约详情V3
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetReserveInfoV3ClientResponse> GetReserveInfoV3(GetReserveInfoV3ClientRequest request);
    }
}