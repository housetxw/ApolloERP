using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Vehicle.Service.Core.Interfaces;
using Ae.Vehicle.Service.Core.Request;
using Ae.Vehicle.Service.Filters;

namespace Ae.Vehicle.Service.Controllers
{
    /// <summary>
    /// 车型操作相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(VehicleController))]
    public class VehicleOperateController : ControllerBase
    {
        private readonly IVehicleOperateService _vehicleOperateService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="vehicleOperateService"></param>
        public VehicleOperateController(IVehicleOperateService vehicleOperateService)
        {
            _vehicleOperateService = vehicleOperateService;
        }

        /// <summary>
        /// 删除车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteVehicleByTid([FromBody] DeleteVehicleByTidRequest request)
        {
            var result = await _vehicleOperateService.DeleteVehicleByTid(request);

            return Result.Success(result);
        }

        //public async Task<ApiResult<bool>> EditVehicleInfo()

        /// <summary>
        /// 力洋车型数据初始化
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> InitLiYangData([FromBody] InitLiYangDataRequest request)
        {
            var result = await _vehicleOperateService.InitLiYangData(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 同步vehicleId
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SyncVehicleId()
        {
            var result = await _vehicleOperateService.SyncVehicleId();

            return Result.Success(result);
        }

        /// <summary>
        /// 新增力洋新数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddVehicleByLiYang([FromBody] AddVehicleByLiYangRequest request)
        {
            var result = await _vehicleOperateService.AddVehicleByLiYang(request);
            
            return Result.Success(result);
        }

        /// <summary>
        /// 根据力洋Id删除车型数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteVehicleByLiYang([FromBody] DeleteVehicleByLiYangRequest request)
        {
            var result = await _vehicleOperateService.DeleteVehicleByLiYang(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 保养适配数据同步
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SyncBaoYangParts()
        {
            var result = await _vehicleOperateService.SyncBaoYangParts();

            return Result.Success(result);
        }

        /// <summary>
        /// 添加车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> AddVehicle([FromBody] AddVehicleRequest request)
        {
            var result = await _vehicleOperateService.AddVehicle(request);
            ApiResult<string> apiResult = new ApiResult<string>();
            apiResult.Code = ResultCode.Success;
            apiResult.Data = result;
            return apiResult;
        }
    }
}