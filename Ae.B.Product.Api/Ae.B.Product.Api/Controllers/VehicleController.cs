using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Model.Vehicle;
using Ae.B.Product.Api.Core.Request.Vehicle;
using Ae.B.Product.Api.Core.Response.Vehicle;
using Ae.B.Product.Api.Filters;

namespace Ae.B.Product.Api.Controllers
{
    /// <summary>
    /// 车型相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        /// <summary>
        /// 获取车型品牌
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<VehicleBrandResponse>>> GetVehicleBrandList()
        {
            var result = await _vehicleService.GetVehicleBrandList();

            return Result.Success(result);
        }

        /// <summary>
        /// 根据品牌查车系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<VehicleListResponse>>> GetVehicleListByBrand(
            [FromQuery] VehicleListByBrandRequest request)
        {
            var result = await _vehicleService.GetVehicleListByBrand(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据车系查排量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<string>>> GetPaiLiangByVehicleId(
            [FromQuery] GetPaiLiangByVehicleIdRequest request)
        {
            var result = await _vehicleService.GetPaiLiangByVehicleId(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据排量获取生产年份
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<string>>> GetVehicleNianByPaiLiang(
            [FromQuery] VehicleNianByPaiLiangRequest request)
        {
            var result = await _vehicleService.GetVehicleNianByPaiLiang(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据生产年份查询款型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<VehicleSalesNameByNianResponse>>> GetVehicleSalesNameByNian(
            [FromQuery] VehicleSalesNameByNianRequest request)
        {
            var result = await _vehicleService.GetVehicleSalesNameByNian(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 车型详细信息查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<VehicleDetailVo>>> GetVehicleInfoList([FromQuery]VehicleInfoListRequest request)
        {
            var result = await _vehicleService.GetVehicleInfoList(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 删除车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteVehicleByTid([FromBody]ApiRequest<DeleteVehicleByTidRequest> request)
        {
            var result = await _vehicleService.DeleteVehicleByTid(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据车型查询Tids
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<string>>> GetTidsByVehicle([FromQuery] TidsByVehicleRequest request)
        {
            var result = await _vehicleService.GetTidsByVehicle(request);

            return Result.Success(result);
        }
    }
}