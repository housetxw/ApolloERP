using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Model.Vehicle;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.Vehicle;
using Ae.C.MiniApp.Api.Core.Response;
using Ae.C.MiniApp.Api.Filters;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 车型相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(VehicleController))]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IIdentityService _identityService;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="vehicleService"></param>
        /// <param name="identityService"></param>
        public VehicleController(IVehicleService vehicleService, IIdentityService identityService)
        {
            _vehicleService = vehicleService;
            _identityService = identityService;
        }

        /// <summary>
        /// 获取车型品牌
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<List<VehicleBrandResponse>>> GetVehicleBrandList()
        {
            var result = await _vehicleService.GetVehicleBrandListAsync();

            return Result.Success(result);
        }

        /// <summary>
        /// 根据品牌查车系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<List<VehicleListResponse>>> GetVehicleListByBrand(
            [FromQuery] VehicleListByBrandRequest request)
        {
            var result = await _vehicleService.GetVehicleListByBrandAsync(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据车系查排量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<List<string>>> GetPaiLiangByVehicleId(
            [FromQuery] GetPaiLiangByVehicleIdRequest request)
        {
            var result = await _vehicleService.GetPaiLiangByVehicleIdAsync(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据排量获取生产年份
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<List<string>>> GetVehicleNianByPaiLiang(
            [FromQuery] VehicleNianByPaiLiangRequest request)
        {
            var result = await _vehicleService.GetVehicleNianByPaiLiangAsync(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据生产年份查询款型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<List<VehicleSalesNameByNianResponse>>> GetVehicleSalesNameByNian(
            [FromQuery] VehicleSalesNameByNianRequest request)
        {
            var result = await _vehicleService.GetVehicleSalesNameByNianAsync(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 车系搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<List<VehicleSearchByNameResponse>>> VehicleSearchByName(
            [FromQuery] VehicleSearchByNameRequest request)
        {
            var result = await _vehicleService.VehicleSearchByNameAsync(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 添加用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> AddUserCar(ApiRequest<AddUserCarRequest> request)
        {
            request.Data.UserId = _identityService.GetUserId();
            ApiResult<string> apiResult = new ApiResult<string>();
            var result = await _vehicleService.AddUserCarAsync(request.Data);
            apiResult.Code = ResultCode.Success;
            apiResult.Data = result;
            return apiResult;
        }

        /// <summary>
        /// 设置用户的默认车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SetUserDefaultVehicle(ApiRequest<SetUserDefaultVehicleRequest> request)
        {
            request.Data.UserId = _identityService.GetUserId();
            var result = await _vehicleService.SetUserDefaultVehicleAsync(request.Data);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取用户配置的所有车型信息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<UserVehicleVO>>> GetAllUserVehicles()
        {
            var result = await _vehicleService.GetAllUserVehiclesAsync(_identityService.GetUserId());
            return Result.Success(result);
        }

        /// <summary>
        /// 获取用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserVehicleVO>> GetUserVehicleByCarId(
            [FromQuery] UserVehicleByCarIdRequest request)
        {
            request.UserId = _identityService.GetUserId();
            var result = await _vehicleService.GetUserVehicleByCarIdAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取用户默认车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserVehicleVO>> GetUserDefaultVehicle()
        {
            var result = await _vehicleService.GetUserDefaultVehicleAsync(_identityService.GetUserId());
            return Result.Success(result);
        }

        /// <summary>
        /// 删除用户车辆
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteUserCar(ApiRequest<DeleteUserCarRequest> request)
        {
            request.Data.UserId = _identityService.GetUserId();
            var result = await _vehicleService.DeleteUserCarAsync(request.Data);
            return Result.Success(result);
        }

        /// <summary>
        /// 编辑用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditUserCar(ApiRequest<EditUserCarRequest> request)
        {
            request.Data.UserId = _identityService.GetUserId();

            var result = await _vehicleService.EditUserCarAsync(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取保险公司列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<InsuranceCompanyVo>>> GetInsuranceCompanyList(
            [FromQuery] InsuranceCompanyListRequest request)
        {
            var result = await _vehicleService.GetInsuranceCompanyListAsync(request);

            return Result.Success(result);
        }
    }
}