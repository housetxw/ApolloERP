using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model.ReceiveCheck;
using Ae.C.MiniApp.Api.Core.Request.ReceiveCheck;
using Ae.C.MiniApp.Api.Core.Response.ReceiveCheck;
using Ae.C.MiniApp.Api.Filters;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 到店检查报告相关
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ReceiveCheckController))]
    [ApiController]
    public class ReceiveCheckController : ControllerBase
    {
        private readonly IReceiveCheckService receiveCheckService;

        public ReceiveCheckController(IReceiveCheckService receiveCheckService)
        {
            this.receiveCheckService = receiveCheckService;
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
        /// 车辆档案
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserVehicleFileResponse>> GetUserVehicleFile([FromQuery]UserVehicleFileRequest request)
        {
            var result = await receiveCheckService.GetUserVehicleFile(request);

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
            var result = await receiveCheckService.GetMaintenancerecord(request);

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