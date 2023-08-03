using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Core.Model.Arrival;
using Ae.Receive.Service.Core.Request.Arrival;
using Ae.Receive.Service.Core.Response;
using Ae.Receive.Service.Core.Response.Arrival;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Core.Interfaces
{
    /// <summary>
    /// 到店记录服务
    /// </summary>
    public interface IArrivalService
    {
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

        Task<List<TodayArrivalShopStatisticsResDTO>> GetTodayArrivalShopStatistics(TodayArrivalShopStatisticsReqDTO req);

        Task<List<MonthArrivalShopStatisticsResDTO>> GetMonthArrivalShopStatistics(MonthArrivalShopStatisticsReqDTO req);

        Task<List<NewCustomerArrivalShopResDTO>> GetNewCustomerArrivalShopStatistics(NewCustomerArrivalShopReqDTO req);

        Task<List<NewCustomerArrivalShopSaleroomResDTO>> GetNewCustomerArrivalShopSaleroomStatistics(NewCustomerArrivalShopSaleroomReqDTO req);

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
        /// 到店记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedForArrivalResult<GetArrivalListResponse>> GetArrivalList(GetArrivalListRequest request);
        Task<ApiResult<GetArrivalListResponse>> GetLastArrival(GetLastArrivalRequest request);

        /// <summary>
        /// 到店记录详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetArrivalDetailResponse>> GetArrivalDetail(GetArrivalDetailRequest request);

        /// <summary>
        /// 得到派工技师列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<GetDispatchTechniciansResponse>>> GetDispatchTechnicians(GetDispatchTechniciansRequest request);


        /// <summary>
        /// 得到员工技师列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<GetDispatchTechniciansResponse>>> GetShopEmployeeByJobIdPage(GetDispatchTechniciansRequest request);
        /// <summary>
        /// 派工保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> DispatchTechnicianSave(ApiRequest<DispatchTechnicianSaveRequest> request);

        /// <summary>
        /// 未消费离店原因
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<LeaveShopReasonResponse>>> LeaveShopReason(LeaveShopReasonRequest request);

        /// <summary>
        /// 离店原因保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> LeaveShopReasonSave(ApiRequest<LeaveShopReasonSaveRequest> request);

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteProjectItem(ApiRequest<DeleteProjectItemRequest> request);

        /// <summary>
        /// 得到项目列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetProjectListResponse>> GetProjectList([FromQuery] GetProjectListRequest request);

        /// <summary>
        /// 项目保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> ProjectItemSave(ApiRequest<ProjectItemSaveRequest> request);

        /// <summary>
        /// 项目编辑
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<ProjectItemEditResponse>> ProjectItemEdit(ProjectItemEditRequest request);

        /// <summary>
        /// 收款
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<PayResponse>> Pay(PayRequest request);

        /// <summary>
        /// 收款保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> PaySave(ApiRequest<PaySaveRequest> request);

        /// <summary>
        /// 得到上一次到店记录下的ShopId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<long>> GetLastShopForLastArrival(GetLastShopForLastArrivalRequest request);

        /// <summary>
        /// 得到订单信息为预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<ProjectVo>>> GetOrdersForReserver([FromQuery] GetOrdersForReserverRequest request);

        /// <summary>
        /// 更改到店记录车型信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateArrivalVehicle(UpdateArrivalVehicleRequest request);

        /// <summary>
        /// 得到到店记录订单为员工绩效统计
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetShopArrivalOrderForStaticResponse>> GetShopArrivalOrderForStatic([FromBody]GetShopArrivalForStaticRequest request);
        /// 支付列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<PayListResponse>> PayList(PayRequest request);

        /// <summary>
        /// 判断是否需要唤醒收银台
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> CheckIsNeedPayControl(PayRequest request);

        /// <summary>
        /// 得到到店记录下面的订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<ShopArrivalOrderDTO>>> GetArrivalOrderList(GetArrivalOrderListRequest request);



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
        /// 得到车辆最后入店时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<string>> GetLastArriveTimeByCarId(GetArrialMaintenanceRequest request);

        /// <summary>
        /// 得到到店的次数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<int>> GetArriveCountByCarId(GetArrialMaintenanceRequest request);

        /// <summary>
        /// 得到到店的消费金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<double>> GetArriveConsumptionAmountByCarId(GetArrialMaintenanceRequest request);


        /// <summary>
        /// 得到历史到店维修记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetArrivalMaintenanceListByCarIdResponse>> GetArrivalMaintenanceListByCarId([FromQuery]GetArrivalMaintenanceListByCarIdRequest request);

        /// <summary>
        /// 得到订单关联的到店记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<ShopArrivalOrderDTO>>> GetShopArrivalOrder([FromQuery] GetShopArrivalOrderRequest request);

        /// <summary>
        /// 到店关联订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> ArrivalRelateOrder(ArrivalRelateOrderRequest request);

        /// <summary>
        /// 未消费离店前置判断是否关联订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<int>> BeforeShopReasonSave(BeforeShopReasonSaveRequest request);


        /// <summary>
        /// 订单取消关联到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> CancelArrivalRelateOrder(ArrivalRelateOrderRequest request);

        /// <summary>
        /// 到店完结
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> ArrivalFinish(ApiRequest<ArrivalFinishRequest> request);

        /// <summary>
        /// 保存车辆记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> SaveCarReport( SaveCarReportRequest request);

        /// <summary>
        /// 得到到店记录下面的视频信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<ShopArrivalVideoResponse>>> GetShopArrivalVideo(ShopArrivalVideoRequest request);

        Task<ApiResult> SaveShopArrivalVideo( ApiRequest<SaveShopArrivalVideoRequest> request);

        Task<ApiResult> DeleteShopArrivalVideo(
            ApiRequest<DeleteShopArrivalVideoRequest> request);

        Task<ApiResult<List<ShopArrivalVideoResponse>>> GetShopArrivalVideoForOrder(
            GetShopArrivalVideoForOrderRequest request);
    }
}
