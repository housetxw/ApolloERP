using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.B.Activity.Api.Core.Interfaces;
using Ae.B.Activity.Api.Core.Request;
using Ae.B.Activity.Api.Core.Response;
using Ae.B.Activity.Api.Filters;

namespace Ae.B.Activity.Api.Controllers
{
    [Route("[controller]/[action]")]
   // [Filter(nameof(VehicleController))]
    [ApiController]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            this._vehicleService = vehicleService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<List<GetVehicleBrandResponse>>> GetVehicleBrandList()
        {
            var result = await _vehicleService.GetVehicleBrandList();
            return Result.Success(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<List<GetVehicleListResponse>>> GetVehicleListByBrand([FromQuery]GetVehicleListByBrandRequest request)
        {
            var result = await _vehicleService.GetVehicleListByBrand(request);
            return Result.Success(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<List<BasicInfoResponse>>> GetPaiLiangByVehicleId([FromQuery]GetPaiLiangByVehicleIdRequest request)
        {
            var result = await _vehicleService.GetPaiLiangByVehicleId(request);
            return Result.Success(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<List<BasicInfoResponse>>> GetVehicleNianByPaiLiang([FromQuery]GetVehicleNianByPaiLiangRequest request)
        {
            var result = await _vehicleService.GetVehicleNianByPaiLiang(request);
            return Result.Success(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<List<GetVehicleSalesNameByNianResponse>>> GetVehicleSalesNameByNian([FromQuery]GetVehicleSalesNameByNianRequest request)
        {
            var result = await _vehicleService.GetVehicleSalesNameByNian(request);
            return Result.Success(result);
        }

    }
}