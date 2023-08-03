using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Core.Interfaces;
using Ae.Receive.Service.Core.Model.ReceiveCheck;
using Ae.Receive.Service.Core.Request.ReceiveCheck;
using Ae.Receive.Service.Core.Response.ReceiveCheck;
using Ae.Receive.Service.Filters;

namespace Ae.Receive.Service.Controllers
{
    /// <summary>
    /// 到店检查报告相关
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ReceiveCheckController))]
  
    public class ReceiveCheckController : ControllerBase
    {
        private readonly IReceiveCheckService receiveCheckService;
        private readonly IArrivalService arrivalService;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="receiveCheckService"></param>
        /// <param name="arrivalService"></param>
        public ReceiveCheckController(IReceiveCheckService receiveCheckService, IArrivalService arrivalService)
        {
            this.receiveCheckService = receiveCheckService;
            this.arrivalService = arrivalService;
        }

        /// <summary>
        /// 检查项目首页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<CheckMainDataResponse>> GetCheckMainData([FromQuery]CheckMainDataRequest request)
        {
            var result = await receiveCheckService.GetCheckMainData(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取客户描述
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<CustomerDescriptionResponse>> GetCustomerDescription([FromQuery]CheckMainDataRequest request)
        {
            var result = await receiveCheckService.GetCustomerDescription(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取仪表盘
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<CarDashboardResponse>> GetCarDashboard([FromQuery]CheckMainDataRequest request)
        {
            var result = await receiveCheckService.GetCarDashboard(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取附加项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<OtherInspectionResponse>> GetOtherInspection([FromQuery]CheckMainDataRequest request)
        {
            var result = await receiveCheckService.GetOtherInspection(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取内饰/外观
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<InOrOutlookInspectionResponse>> GetInOrOutlookInspection([FromQuery]InOrOutlookInspectionRequest request)
        {
            var result = await receiveCheckService.GetInOrOutlookInspection(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 保存客户描述
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<ApiResult<bool>> SaveCustomerDescription([FromBody]SaveCustomerDescriptionRequest request)
        {
            var result = await receiveCheckService.SaveCustomerDescription(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 保存仪表盘
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SaveCarDashboard([FromBody]SaveCarDashboardRequest request)
        {
            var result = await receiveCheckService.SaveCarDashboard(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 保存附加项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SaveOtherInspection([FromBody]SaveOtherInspectionRequest request)
        {
            var result = await receiveCheckService.SaveOtherInspection(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 保存外观/内饰
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SaveInOrOutlookInspection([FromBody]SaveInOrOutlookInspectionRequest request)
        {
            var result = await receiveCheckService.SaveInOrOutlookInspection(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 升级项目查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UpgradeCheckItemResponse>> GetUpgradeCheckItem([FromQuery] UpgradeCheckItemRequest request)
        {
            var result = await receiveCheckService.GetUpgradeCheckItem(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 升级项目保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SaveUpgradeCheckItem([FromBody]SaveUpgradeCheckItemRequest request)
        {
            var result = await receiveCheckService.SaveUpgradeCheckItem(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 检查项统计
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<CheckStatisticsResultResponse>> GetCheckStatisticsResult([FromQuery]CheckStatisticsResultRequest request)
        {
            var result = await receiveCheckService.GetCheckStatisticsResult(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 技师Or客户签字
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SaveSignatureData([FromBody] SaveSignatureDataRequest request)
        {
            var result = await receiveCheckService.SaveSignatureData(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 提交检查报告
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SubmitCheckResult([FromBody]SubmitCheckResultRequest request)
        {
            var result = await receiveCheckService.SubmitCheckResult(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 计算规则结果
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ComputeRuleResultResponse>> ComputeRuleResult([FromQuery]ComputeRuleResultRequest request)
        {
            var result = await receiveCheckService.ComputeRuleResult(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 查询检查报告
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<CheckReportResponse>> GetCheckReport([FromQuery]CheckReportRequest request)
        {
            var result = await receiveCheckService.GetCheckReport(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 用户检查记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<UserReceiveCheckVo>> GetUserReceiveCheckList([FromQuery]UserReceiveCheckListRequest request)
        {
            var result = await receiveCheckService.GetUserReceiveCheckList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 车辆历史检测报告
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CarReceiveCheckVo>>> GetCarReceiveCheckList(
            [FromQuery] CarReceiveCheckListRequest request)
        {
            var result = await receiveCheckService.GetCarReceiveCheckList(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 车辆档案
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserVehicleFileResponse>> GetUserVehicleFile([FromQuery]UserVehicleFileRequest request)
        {
            var resultTask = receiveCheckService.GetUserVehicleFile(request);
            var maintenanceResultTask = arrivalService.GetArrivalMaintenanceListByCarId(new Core.Request.Arrival.GetArrivalMaintenanceListByCarIdRequest
            {
                CarId = request.CarId,
                ServiceType = "None"
            });
            await Task.WhenAll(resultTask, maintenanceResultTask);
            var result = resultTask.Result;
            var maintenanceResult = maintenanceResultTask.Result?.Data;
            if (result != null && maintenanceResult != null)
            {
                var manitenanceHead = maintenanceResult.ManitenanceHead ?? new List<Core.Model.Arrival.GetArrivalMaintenanceListByCarIdVo>();
                var items = maintenanceResult.Items ?? new List<Core.Model.Arrival.ArrivalMaintenanceProjectVo>();

                List<ServiceCategory> serviceType = manitenanceHead.Select(_ => new ServiceCategory
                {
                    ServiceCode = _.ServiceType,
                    DisplayName = _.ServiceTypeText,
                    Count = _.Num,
                    Checked = _.ServiceType == "None"
                }).ToList();

                List<ReceiveCheckListVo> receiveList = new List<ReceiveCheckListVo>();
                var receiveIds = items.Select(t => (long)t.Id).ToList();
                if (receiveIds.Any())
                {
                    receiveList = await receiveCheckService.GetReceiveCheckListByRecIds(receiveIds);
                }


                List<ServiceRecord> record = items.Select(_ => new ServiceRecord
                {
                    ReceiveTime = _.ArrivalDate,
                    ShopName = _.ShopName,
                    Mileage = receiveList.FirstOrDefault(x => x.RecId == (long)_.Id)?.Mileage,
                    Tags = new List<TagInfo>
                    {
                        new TagInfo
                        {
                            Tag = _.ServiceTypeText,
                            Type = "ServiceType",
                            TagColor = "#07B9E8"
                        }
                    },
                    Projects = _.Items?.Select(t => new ServiceProject
                    {
                        DisplayName = t.ProductName,
                        TechName = t.TechName,
                        Count = t.Num,
                        TotalMoney = t.Price * t.Num
                    })?.ToList() ?? new List<ServiceProject>()
                }).ToList();

                result.Maintenancerecord = new Maintenancerecord
                {
                    ServiceCategory = serviceType,
                    ServiceRecord = record
                };
            }

            return Result.Success(result);
        }

        /// <summary>
        /// 维修记录查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<Maintenancerecord>> GetMaintenancerecord([FromQuery] MaintenancerecordRequest request)
        {
            Maintenancerecord result = new Maintenancerecord();
            var maintenanceResult = (await arrivalService.GetArrivalMaintenanceListByCarId(new Core.Request.Arrival.GetArrivalMaintenanceListByCarIdRequest
            {
                CarId = request.CarId,
                ServiceType = request.ServiceCode
            }))?.Data;
            if (maintenanceResult != null)
            {
                var manitenanceHead = maintenanceResult.ManitenanceHead ?? new List<Core.Model.Arrival.GetArrivalMaintenanceListByCarIdVo>();
                var items = maintenanceResult.Items ?? new List<Core.Model.Arrival.ArrivalMaintenanceProjectVo>();

                List<ServiceCategory> serviceType = manitenanceHead.Select(_ => new ServiceCategory
                {
                    ServiceCode = _.ServiceType,
                    DisplayName = _.ServiceTypeText,
                    Count = _.Num,
                    Checked = _.ServiceType == request.ServiceCode
                }).ToList();

                List<ReceiveCheckListVo> receiveList = new List<ReceiveCheckListVo>();
                var receiveIds = items.Select(t => (long)t.Id).ToList();
                if (receiveIds.Any())
                {
                    receiveList = await receiveCheckService.GetReceiveCheckListByRecIds(receiveIds);
                }

                List<ServiceRecord> record = items.Select(_ => new ServiceRecord
                {
                    ReceiveTime = _.ArrivalDate,
                    ShopName = _.ShopName,
                    Mileage = receiveList.FirstOrDefault(x => x.RecId == (long)_.Id)?.Mileage,
                    Tags = new List<TagInfo>
                    {
                        new TagInfo
                        {
                            Tag = _.ServiceTypeText,
                            Type = "ServiceType",
                            TagColor = "#07B9E8"
                        }
                    },
                    Projects = _.Items?.Select(t => new ServiceProject
                    {
                        DisplayName = t.ProductName,
                        TechName = t.TechName,
                        Count = t.Num,
                        TotalMoney = t.Price * t.Num
                    })?.ToList() ?? new List<ServiceProject>()
                }).ToList();

                result = new Maintenancerecord
                {
                    ServiceCategory = serviceType,
                    ServiceRecord = record
                };
            }
            return Result.Success(result);
        }

        /// <summary>
        /// 异常详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CheckErrorDetailVo>>> GetCheckErrorDetail([FromQuery]CheckErrorDetailRequest request)
        {
            var result = await receiveCheckService.GetCheckErrorDetail(request);

            return Result.Success(result);
        }
    }
}