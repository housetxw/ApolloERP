using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Core.Interfaces;
using Ae.Receive.Service.Core.Request.Arrival;
using Ae.Receive.Service.Core.Response.Arrival;
using Ae.Receive.Service.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ae.Receive.Service.Common.Constant;
using Ae.Receive.Service.Common.Exceptions;
using Ae.Receive.Service.Core.Model.Arrival;
using Ae.Receive.Service.Core.Response;

namespace Ae.Receive.Service.Controllers
{
    /// <summary>
    /// 到店记录
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ArrivalController))]
    public class ArrivalController : ControllerBase
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private IArrivalService _arrivalService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="arrivalService"></param>
        public ArrivalController(IArrivalService arrivalService)
        {
            _arrivalService = arrivalService;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        /// <summary>
        /// 快速排队接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<QuickQueueResponse>> QuickQueue([FromQuery]QuickQueueRequest request)
        {
            return await _arrivalService.QuickQueue(request);
        }

        /// <summary>
        /// 快速排队请求弹层
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<QuickTakeNumberLayerResponse>> QuickTakeNumberLayer([FromBody]ApiRequest<QuickTakeNumberLayerRequest> request)
        {
            return await _arrivalService.QuickTakeNumberLayer(request);
        }

        /// <summary>
        /// 获取今日到店统计数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<TodayArrivalShopStatisticsResDTO>>> GetTodayArrivalShopStatistics([FromQuery] TodayArrivalShopStatisticsReqDTO req)
        {
            var res = await _arrivalService.GetTodayArrivalShopStatistics(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 获取月到店统计数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<MonthArrivalShopStatisticsResDTO>>> GetMonthArrivalShopStatistics([FromQuery] MonthArrivalShopStatisticsReqDTO req)
        {
            var res = await _arrivalService.GetMonthArrivalShopStatistics(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 获取新客到店统计数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<NewCustomerArrivalShopResDTO>>> GetNewCustomerArrivalShopStatistics([FromBody] NewCustomerArrivalShopReqDTO req)
        {
            var res = await _arrivalService.GetNewCustomerArrivalShopStatistics(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 获取新客到店销售数据统计数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<NewCustomerArrivalShopSaleroomResDTO>>> GetNewCustomerArrivalShopSaleroomStatistics([FromBody] NewCustomerArrivalShopSaleroomReqDTO req)
        {
            var res = await _arrivalService.GetNewCustomerArrivalShopSaleroomStatistics(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 排队仅拿号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<QuickTakeNumberLayerResponse>> QuickTakeNumber([FromBody]ApiRequest<QuickTakeNumberRequest> request)
        {
            return await _arrivalService.QuickTakeNumber(request);
        }

        /// <summary>
        /// 预约排队拿号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<QuickTakeNumberLayerResponse>> ReserveTakeNumber([FromBody]ApiRequest<ReserveTakeNumberRequest> request)
        {
            return await _arrivalService.ReserveTakeNumber(request);
        }

        /// <summary>
        /// 得到服务记录信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetServiceRecordResponse>> GetServiceRecord([FromQuery]GetServiceRecordRequest request)
        {
            return await _arrivalService.GetServiceRecord(request);
        }

        /// <summary>
        /// 到店记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedForArrivalResult<GetArrivalListResponse>> GetArrivalList([FromQuery]GetArrivalListRequest request)
        {
            return await _arrivalService.GetArrivalList(request);
        }

        /// <summary>
        /// 最近一次到店记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetArrivalListResponse>> GetLastArrival([FromQuery] GetLastArrivalRequest request)
        {
            return await _arrivalService.GetLastArrival(request);
        }

        /// <summary>
        /// 到店记录详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetArrivalDetailResponse>> GetArrivalDetail([FromQuery]GetArrivalDetailRequest request)
        {
            ApiResult<GetArrivalDetailResponse> res;

            try
            {
                res = await _arrivalService.GetArrivalDetail(request);
            }
            catch (Exception e)
            {
                throw new CustomException("Receive GetArrivalDetail Exception", e);
            }

            return res;
        }

        /// <summary>
        /// 得到派工技师列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetDispatchTechniciansResponse>>> GetDispatchTechnicians([FromQuery]GetDispatchTechniciansRequest request)
        {
            return await _arrivalService.GetDispatchTechnicians(request);
        }

        /// <summary>
        /// 得到派工技师列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<GetDispatchTechniciansResponse>>> GetShopEmployeeByJobIdPage([FromBody] GetDispatchTechniciansRequest request)
        {
            return await _arrivalService.GetShopEmployeeByJobIdPage(request);
        }

        /// <summary>
        /// 派工保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> DispatchTechnicianSave([FromBody] ApiRequest<DispatchTechnicianSaveRequest> request)
        {
            return await _arrivalService.DispatchTechnicianSave(request);
        }

        /// <summary>
        /// 未消费离店原因
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<LeaveShopReasonResponse>>> LeaveShopReason([FromQuery] LeaveShopReasonRequest request)
        {
            return await _arrivalService.LeaveShopReason(request);
        }

        /// <summary>
        /// 离店取消原因保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> LeaveShopReasonSave([FromBody] ApiRequest<LeaveShopReasonSaveRequest> request)
        {
            return await _arrivalService.LeaveShopReasonSave(request);
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> DeleteProjectItem([FromBody] ApiRequest<DeleteProjectItemRequest> request)
        {
            return await _arrivalService.DeleteProjectItem(request);
        }

        /// <summary>
        /// 得到项目列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetProjectListResponse>> GetProjectList([FromQuery] GetProjectListRequest request)
        {
            return await _arrivalService.GetProjectList(request);
        }

        /// <summary>
        /// 项目保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> ProjectItemSave([FromBody] ApiRequest<ProjectItemSaveRequest> request)
        {
            return await _arrivalService.ProjectItemSave(request);
        }

        /// <summary>
        /// 项目编辑
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ProjectItemEditResponse>> ProjectItemEdit([FromQuery] ProjectItemEditRequest request)
        {
            return await _arrivalService.ProjectItemEdit(request);
        }

        ///// <summary>
        ///// 收款
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ApiResult<PayResponse>> Pay([FromQuery] PayRequest request)
        //{
        //    return await _arrivalService.Pay(request);
        //}

        /// <summary>
        /// 收款保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> PaySave([FromBody] ApiRequest<PaySaveRequest> request)
        {
            return await _arrivalService.PaySave(request);
        }

        /// <summary>
        /// 得到上一次到店记录下的ShopId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<long>> GetLastShopForLastArrival([FromQuery] GetLastShopForLastArrivalRequest request)
        {
            return await _arrivalService.GetLastShopForLastArrival(request);
        }

        /// <summary>
        /// 得到订单信息为预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ProjectVo>>> GetOrdersForReserver([FromQuery] GetOrdersForReserverRequest request)
        {
            return await _arrivalService.GetOrdersForReserver(request);
        }

        /// <summary>
        /// 更改到店记录车型信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateArrivalVehicle([FromBody]UpdateArrivalVehicleRequest request)
        {
            return await _arrivalService.UpdateArrivalVehicle(request);
        }


        /// <summary>
        /// 收款
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<PayListResponse>> Pay([FromQuery] PayRequest request)
        {
            return await PayList(request);
        }

        /// <summary>
        /// 收款列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ApiResult<PayListResponse>> PayList(PayRequest request)
        {
            return await _arrivalService.PayList(request);
        }

        /// <summary>
        /// 判断是否需要唤醒收银台
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<bool>> CheckIsNeedPayControl([FromQuery]PayRequest request)
        {
            return await _arrivalService.CheckIsNeedPayControl(request);
        }

        [HttpPost]
        public async Task<ApiResult<GetShopArrivalOrderForStaticResponse>> GetShopArrivalOrderForStatic([FromBody]GetShopArrivalForStaticRequest request)
        {
            return await _arrivalService.GetShopArrivalOrderForStatic(request);
        }

        [HttpPost]
        public async Task<ApiResult<List<ShopArrivalOrderDTO>>> GetArrivalOrderList([FromBody]GetArrivalOrderListRequest request)
        {
            return await _arrivalService.GetArrivalOrderList(request);
        }

        /// <summary>
        /// 收款列表 ForMiniApp
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<PayListResponse>> PayListForMini([FromQuery]PayListForMiniRequest request)
        {
            return await _arrivalService.PayListForMini(request);
        }


        /// <summary>
        /// 判断是否需要唤醒收银台
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<bool>> CheckIsNeedPayControlForMini([FromQuery]PayListForMiniRequest request)
        {
            return await _arrivalService.CheckIsNeedPayControlForMini(request);
        }


        /// <summary>
        /// 得到车辆最后入店时间
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<string>> GetLastArriveTimeByCarId([FromQuery]GetArrialMaintenanceRequest request)
        {
            return await _arrivalService.GetLastArriveTimeByCarId(request);
        }


        /// <summary>
        /// 得到到店的次数
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<int>> GetArriveCountByCarId([FromQuery]GetArrialMaintenanceRequest request)
        {
            return await _arrivalService.GetArriveCountByCarId(request);
        }

        /// <summary>
        /// 得到到店的消费金额
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<double>> GetArriveConsumptionAmountByCarId([FromQuery]GetArrialMaintenanceRequest request)
        {
            return await _arrivalService.GetArriveConsumptionAmountByCarId(request);
        }

        /// <summary>
        /// 得到历史到店维修记录
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetArrivalMaintenanceListByCarIdResponse>> GetArrivalMaintenanceListByCarId([FromQuery]GetArrivalMaintenanceListByCarIdRequest request)
        {
            return await _arrivalService.GetArrivalMaintenanceListByCarId(request);
        }

        /// <summary>
        /// 得到到店记录订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<ShopArrivalOrderDTO>>> GetShopArrivalOrder([FromBody] GetShopArrivalOrderRequest request)
        {
            return await _arrivalService.GetShopArrivalOrder(request);
        }

        /// <summary>
        /// 订单关联到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> ArrivalRelateOrder([FromBody] ArrivalRelateOrderRequest request)
        {
            return await _arrivalService.ArrivalRelateOrder(request);
        }


        /// <summary>
        /// 订单取消关联到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> CancelArrivalRelateOrder([FromBody] ArrivalRelateOrderRequest request)
        {
            return await _arrivalService.CancelArrivalRelateOrder(request);
        }


        /// <summary>
        /// 未消费离店前置判断是否关联订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<int>> BeforeShopReasonSave(BeforeShopReasonSaveRequest request)
        {
            return await _arrivalService.BeforeShopReasonSave(request);
        }


        /// <summary>
        /// 到店完结
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> ArrivalFinish([FromBody] ApiRequest<ArrivalFinishRequest> request)
        {
            return await _arrivalService.ArrivalFinish(request);
        }

        /// <summary>
        /// 保存车辆报告
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> SaveCarReport([FromBody] ApiRequest<SaveCarReportRequest> request)
        {
            return await _arrivalService.SaveCarReport(request.Data);
        }

        /// <summary>
        /// 到店视频列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ShopArrivalVideoResponse>>> GetShopArrivalVideo([FromQuery]ShopArrivalVideoRequest request)
        {
            return await _arrivalService.GetShopArrivalVideo(request);
        }

        /// <summary>
        /// 保存到店视频
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> SaveShopArrivalVideo([FromBody] ApiRequest<SaveShopArrivalVideoRequest> request)
        {
            return await _arrivalService.SaveShopArrivalVideo(request);
        }

        /// <summary>
        /// 删除到店视频信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> DeleteShopArrivalVideo(
            [FromBody] ApiRequest<DeleteShopArrivalVideoRequest> request)
        {
            return await _arrivalService.DeleteShopArrivalVideo(request);
        }

        /// <summary>
        /// 得到订单下面的视频列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ShopArrivalVideoResponse>>> GetShopArrivalVideoForOrder(
            [FromQuery] GetShopArrivalVideoForOrderRequest request)
        {
            return await _arrivalService.GetShopArrivalVideoForOrder(request);
        }
    }
}
