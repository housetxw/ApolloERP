using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Request.Vehicle;
using Ae.Shop.Api.Core.Response.Vehicle;
using Ae.Shop.Api.Filters;

namespace Ae.Shop.Api.Controllers
{
    /// <summary>
    /// 用户车型
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    //[Filter(nameof(VehicleController))]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        /// <summary>
        /// 获取车型品牌
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<VehicleBrandResponse>>> GetVehicleBrandListAsync()
        {
            var result = await vehicleService.GetVehicleBrandListAsync();

            return Result.Success(result);
        }

        /// <summary>
        /// 根据品牌查车系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<VehicleListResponse>>> GetVehicleListByBrandAsync([FromQuery]VehicleListByBrandRequest request)
        {
            var result = await vehicleService.GetVehicleListByBrandAsync(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据车系查排量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<string>>> GetPaiLiangByVehicleIdAsync([FromQuery]GetPaiLiangByVehicleIdRequest request)
        {
            var result = await vehicleService.GetPaiLiangByVehicleIdAsync(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据排量获取生产年份
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<string>>> GetVehicleNianByPaiLiangAsync([FromQuery]VehicleNianByPaiLiangRequest request)
        {
            var result = await vehicleService.GetVehicleNianByPaiLiangAsync(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据生产年份查询款型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<VehicleSalesNameByNianResponse>>> GetVehicleSalesNameByNianAsync(
           [FromQuery]VehicleSalesNameByNianRequest request)
        {
            var result = await vehicleService.GetVehicleSalesNameByNianAsync(request);

            return Result.Success(result);
        }
    }
}