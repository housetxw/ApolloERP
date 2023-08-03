using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.Arrival;
using Ae.C.MiniApp.Api.Core.Request.Reserve;
using Ae.C.MiniApp.Api.Core.Request.Service;
using Ae.C.MiniApp.Api.Core.Response;
using Ae.C.MiniApp.Api.Core.Response.Arrival;
using Ae.C.MiniApp.Api.Core.Response.Reserve;
using Ae.C.MiniApp.Api.Core.Response.Service;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    public interface IReceiveService
    {
        Task<JudgeMyReserveResponse> JudgeMyReserve();
        Task<ApiPagedResultData<CanReserveOrderVO>> CanReserveOrderList(BasePageRequest request);
        Task<ReserveInitialResponse> ReserveInitial(ReserveInitialRequest request);
        Task<ApiResult> AddShopReserveAsync(AddReserveRequest request);
        Task<GetReserveInfoResponse> GetReserveInfo(GetReserveInfoRequest request);
        Task<bool> ModifyReserve(ModifyReserveRequest request);
        Task<bool> CancelReserve(CancelReserveRequest request);
        Task<CanReserveDateResponse> CanReserveDate(BaseShopRequest request);
        Task<List<ReserveTimeVO>> CanReserveTime(CanReserveTimeRequest request);
        Task<List<ReserveTimeVO>> CanReserveTimeV2(CanReserveTimeRequest request);
        Task<ApiPagedResultData<ReservedListResponse>> ReservedList(BasePageRequest request);
        Task<ApiResult<long>> AddReserveV2Async(AddReserveNewYearRequest request);
        Task<bool> CancelReserveV2(CancelReserveRequest request);
        Task<ApiPagedResultData<ReservedListV2Response>> ReserveListV2(ReservedListRequest request);
        Task<ApiResult<GetRebookReserveResponse>> GetReserveInfoV2(GetReserveInfoRequest request);

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
        Task<List<ReserveDateVO>> GetShopReserveDateTime(BaseShopRequest request);
        /// <summary>
        /// 门店预约日历
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetReserveSurplusResponse> GetShopReserveSurplus(BaseShopRequest request);
        Task<ApiResult> AddShopReserveV3(AddReserveV3Request request);
        Task<List<ReserveTimeVO>> CanReserveTimeV3(CanReserveTimeV3Request request);
        Task<ApiResult> ModifyReserveTime(ModifyReserveTimeRequest request);
        /// <summary>
        /// 获取预约详情V3
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetReserveInfoV3Response> GetReserveInfoV3(GetReserveInfoV3Request request);
    }
}
