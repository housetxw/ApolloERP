using System;
using System.Collections.Generic;
using System.Text;
using Ae.Receive.Service.Core.Request;
using Ae.Receive.Service.Core.Response;
using System.Threading.Tasks;
using Ae.Receive.Service.Core.Model;
using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Core.Request.Reserve;
using Ae.Receive.Service.Core.Model.Reserve;

namespace Ae.Receive.Service.Core.Interfaces
{
    public interface IShopReserveService
    {
        Task<JudgeMyReserveResponse> JudgeMyReserve(JudgeMyReserveRequest request);
        Task<ApiPagedResultData<CanReserveOrderDTO>> CanReserveOrderList(CanReserveOrderListRequest request);
        Task<ReserveInitialResponse> ReserveInitial(ReserveInitialRequest request);
        Task<ApiResult> AddShopReserveAsync(AddReserveRequest request);
        Task<ApiResult> AddShopReserveV3Async(AddReserveV3Request request);
        Task<GetReserveInfoResponse> GetReserveInfo(GetReserveInfoRequest request);
        Task<bool> ModifyReserve(ModifyReserveRequest request);
        Task<bool> UpdateReserveStatus(UpdateReserveStatusRequest request);
        Task<bool> CancelReserve(CancelReserveRequest request);
        Task<CanReserveDateResponse> CanReserveDate(BaseShopRequest request);
        Task<List<ReserveTimeDTO>> CanReserveTime(CanReserveTimeRequest request);
        Task<ApiPagedResultData<ReservedInfoDTO>> ReservedList(ReservedListRequest request);
        /// <summary>
        /// 查询预约详情V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetRebookReserveInfoResponse> GetReserveInfoV2(RebookReserveRequest request);
        Task<ReservedListForAppResponse> GetReserveListForAppAsync(ReservedListForAppRequest request);
        Task<GetReserveDetailForAppResponse> GetReserveDetailForApp(GetReserveDetailForAppRequest request);

        /// <summary>
        /// 预约列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ReserveListResponse> GetReserveList(ReserveListRequest request);

        /// <summary>
        /// 预约搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ReserveSearchResponse> ReserveSearch(ReserveSearchRequest request);

        /// <summary>
        /// 预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ReserveDetailVo> ReserveDetail(ReserveDetailRequest request);

        /// <summary>
        /// 预约时间看板
        /// </summary>
        /// <returns></returns>
        Task<List<ReserveDateVo>> GetReserveDate(ReserveDateRequest request);

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> CancelReserveV2(CancelReserveRequest request);

        /// <summary>
        /// 编辑预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditReserve(EditReserveRequest request);

        /// <summary>
        /// 根据手机号查询用户有效预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ReserveListVo>> GetValidReserve(ValidReserveRequest request);

        /// <summary>
        /// 添加预约（B端）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> AddReserveForShop(AddReserveForShopRequest request);

        /// <summary>
        /// 获取门店预约总数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetShopTotalReserveResponse> GetShopTotalReserve(BaseShopRequest request);
        Task<GetReserveSimpleResponse> GetSameDayReserveSimpleInfo(BaseUserRequest request);
        Task<ApiResult<long>> AddReserveV2Async(AddReserveV2Request request);
        Task<ApiPagedResultData<ReservedListV2DTO>> ReserveListV2(ReserveListV2Request request);
        Task<bool> AddReserveTimeConfig(AddReserveTimeConfigRequest request);
        Task<List<ReserveTimeConfigDTO>> GetReserveTimeConfig(GetReserveTimeConfigRequest request);
        Task<List<ReserveTimeDTO>> CanReserveTimeV2(CanReserveTimeRequest request);

        /// <summary>
        /// 预约列表查询-分页的
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ShopReserveListVo>> GetReserveListPage(ReserveListPageRequest request);
        /// <summary>
        /// 根据预约id或订单号查询预约关联订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopReserveOrderDTO>> GetReserveInfoByReserveIdOrOrderNum(GetReserveInfoByReserveIdOrOrderNum request);

        /// <summary>
        /// 预约详情 - shop站点的
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ReserveDetailForWebVo> GetReserveDetailForWeb(ReserveDetailRequest request);

        /// <summary>
        /// 预约时间看板 Web端
        /// </summary>
        /// <returns></returns>
        Task<List<ReserveDateVo>> GetReserveDateForWeb(ReserveDateRequest request);

        /// <summary>
        /// 预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ShopReserveDTO> GetShopReserveDO(ReserveDetailRequest request);
        /// <summary>
        /// 修改预约时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> ModifyReserveTime(ModifyReserveTimeRequest request);

        /// <summary>
        /// 根据预约ID查询预约详情V3
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetReserveInfoV3Response>> GetReserveInfoV3(GetReserveInfoV3Request request);
        
        /// <summary>
        /// 获取可预约时间节点V3
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ReserveTimeDTO>> CanReserveTimeV3(CanReserveTimeV3Request request);
        /// <summary>
        /// 获取上门养车门店预约日期+时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ReserveDateDTO>> GetShopReserveDateTime(BaseShopRequest request);

        /// <summary>
        /// 预约日历
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetReserveSurplusResponse> GetShopReserveSurplus(BaseShopRequest request);
        /// <summary>
        /// 校验一人一次是否存在预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> CheckTheDayReserveWithUserCarId(CheckReserveTimeRequest request);
    }
}
