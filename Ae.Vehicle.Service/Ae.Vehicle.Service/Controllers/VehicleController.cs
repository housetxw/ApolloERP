using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Vehicle.Service.Common.Exceptions;
using Ae.Vehicle.Service.Core.Interfaces;
using Ae.Vehicle.Service.Core.Model;
using Ae.Vehicle.Service.Core.Request;
using Ae.Vehicle.Service.Core.Response;
using Ae.Vehicle.Service.Filters;

namespace Ae.Vehicle.Service.Controllers
{
    /// <summary>
    /// 车型相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    // [Filter(nameof(VehicleController))]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly ApolloErpLogger<VehicleController> _logger;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 构造方法VehicleController
        /// </summary>
        /// <param name="vehicleService"></param>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        public VehicleController(IVehicleService vehicleService, ApolloErpLogger<VehicleController> logger,
            IConfiguration configuration)
        {
            _vehicleService = vehicleService;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// 获取车型品牌
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetVehicleBrandResponse>>> GetVehicleBrandList()
        {
            var result = await _vehicleService.GetVehicleBrandsAsync();

            return Result.Success(result);
        }

        /// <summary>
        /// 根据品牌查车系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetVehicleListResponse>>> GetVehicleListByBrand(
            [FromQuery] GetVehicleListByBrandRequest request)
        {
            var result = await _vehicleService.GetVehicleListByBrandAsync(request.Brand);

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
            var result = await _vehicleService.GetPaiLiangByVehicleIdAsync(request.VehicleId);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据排量获取生产年份
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<string>>> GetVehicleNianByPaiLiang(
            [FromQuery] GetVehicleNianByPaiLiangRequest request)
        {
            var result = await _vehicleService.GetVehicleNianByPaiLiangAsync(request.VehicleId, request.PaiLiang);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据生产年份查款型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetVehicleSalesNameByNianResponse>>> GetVehicleSalesNameByNian(
            [FromQuery] GetVehicleSalesNameByNianRequest request)
        {
            var result =
                await _vehicleService.GetVehicleSalesNameByNianAsync(request.VehicleId, request.PaiLiang, request.Nian);

            return Result.Success(result);
        }

        /// <summary>
        /// 车系搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<VehicleSearchByNameResponse>>> VehicleSearchByName(
            [FromQuery] VehicleSearchByNameRequest request)
        {
            var result =
                await _vehicleService.VehicleSearchByNameAsync(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据车系，排量[年/款型Id]查询车型信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<VehicleInfo>>> GetVehicleInfoList([FromQuery] VehicleInfoListRequest request)
        {
            var result = await _vehicleService.GetVehicleInfoListAsync(request);
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

        /// <summary>
        /// 根据车系，排量[年/款型Id]查询车型基本信息信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<VehicleBaseInfo>>> GetVehicleBaseInfoList(
            [FromQuery] VehicleInfoListRequest request)
        {
            var result = await _vehicleService.GetVehicleBaseInfoListAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 根据Tids查询车型基本信息信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<VehicleBaseInfo>>> GetVehicleBaseInfoListByTids(
            [FromBody] VehicleInfoListByTidsRequest request)
        {
            var result = await _vehicleService.GetVehicleBaseInfoListByTids(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 五级车型查询车型配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<VehicleConfigVO>>> GetVehicleConfigByTidList(
            [FromBody] VehicleConfigRequest request)
        {
            var result = await _vehicleService.GetVehicleConfigByTidList(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 添加用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> AddUserCarAsync([FromBody] AddUserCarRequest request)
        {
            ApiResult<string> apiResult = new ApiResult<string>();
            var result = await _vehicleService.AddUserCarAsync(request);
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
        public async Task<ApiResult<bool>> SetUserDefaultVehicleAsync([FromBody] SetUserDefaultVehicleRequest request)
        {
            var result = await _vehicleService.SetUserDefaultVehicleAsync(request);
            _logger.Debug($"SetUserDefaultVehicleAsync_{request.CarId}_{DateTime.Now}");
            return Result.Success(result);
        }

        /// <summary>
        /// 获取用户配置的所有车型信息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<UserVehicleVO>>> GetAllUserVehiclesAsync(
            [FromQuery] AllUserVehiclesRequest request)
        {
            _logger.Debug($"GetAllUserVehiclesAsync_{request.UserId}_{DateTime.Now}");
            var result = await _vehicleService.GetAllUserVehiclesAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserVehicleVO>> GetUserVehicleByCarIdAsync(
            [FromQuery] UserVehicleByCarIdRequest request)
        {
            var result = await _vehicleService.GetUserVehicleByCarIdAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取用户默认车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserVehicleVO>> GetUserDefaultVehicleAsync(
            [FromQuery] UserDefaultVehicleRequest request)
        {
            var result = await _vehicleService.GetUserDefaultVehicleAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 批量获取用户默认车辆
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<UserVehicleVO>>> GetUserDefaultVehicleBatch(
            [FromBody] UserDefaultVehicleBatchRequest request)
        {
            var result = await _vehicleService.GetUserDefaultVehicleBatch(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 删除用户车辆
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteUserCarAsync([FromBody] DeleteUserCarRequest request)
        {
            var result = await _vehicleService.DeleteUserCarAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 编辑用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditUserCarAsync([FromBody] EditUserCarRequest request)
        {
            var result = await _vehicleService.EditUserCarAsync(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据车系Id查详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<VehicleTypeVo>> GetVehicleTypeById([FromQuery] VehicleTypeByIdRequest request)
        {
            var result = await _vehicleService.GetVehicleTypeById(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 五级车型查询原配轮胎尺寸
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<OeTireSizeResponse>> GetOeTireSizeByTid([FromQuery] OeTireSizeByTidRequest request)
        {
            var result = await _vehicleService.GetOeTireSizeByTid(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 原配轮胎查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<OeTirePidByTidResponse>> GetOeTirePidByTid(
            [FromQuery] OeTirePidByTidRequest request)
        {
            var result = await _vehicleService.GetOeTirePidByTid(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取保险公司列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<InsuranceCompanyVo>>> GetInsuranceCompanyListAsync(
            [FromQuery] InsuranceCompanyListRequest request)
        {
            var domain = _configuration["ImageDomain"];
            List<InsuranceCompanyVo> result = new List<InsuranceCompanyVo>()
            {
                new InsuranceCompanyVo()
                {
                    DisplayName = "太平洋车险",
                    ImageUrl = $"{domain}/mini-RG-main/PacificAutoInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "保骉车险",
                    ImageUrl = $"{domain}/mini-RG-main/AutoInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "中国人保车险",
                    ImageUrl = $"{domain}/mini-RG-main/PICCAutoInsurance.png"
                },
                //new InsuranceCompanyVo()
                //{
                //    DisplayName = "平安车险",
                //    ImageUrl = ""
                //},
                new InsuranceCompanyVo()
                {
                    DisplayName = "天平汽车车险",
                    ImageUrl = $"{domain}/mini-RG-main/BalanceAutoInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "太平车险",
                    ImageUrl = $"{domain}/mini-RG-main/TaipingAutoInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "中国人寿车险",
                    ImageUrl = $"{domain}/mini-RG-main/renshou.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "中华联合车险",
                    ImageUrl = $"{domain}/mini-RG-main/ChinaUnitedAutoInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "安邦车险",
                    ImageUrl = $"{domain}/mini-RG-main/AmpangAutomobileInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "大地车险",
                    ImageUrl = $"{domain}/mini-RG-main/LandInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "天安车险",
                    ImageUrl = $"{domain}/mini-RG-main/TiananAutomobileInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "华泰车险",
                    ImageUrl = $"{domain}/mini-RG-main/HuataiAutoInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "大众车险",
                    ImageUrl = $"{domain}/mini-RG-main/VolkswageInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "阳光车险",
                    ImageUrl = $"{domain}/mini-RG-main/SunshineInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "永安车险",
                    ImageUrl = $"{domain}/mini-RG-main/YonganAutoInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "中银车险",
                    ImageUrl = $"{domain}/mini-RG-main/BOCmotorInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "华安车险",
                    ImageUrl = $"{domain}/mini-RG-main/HuaanAutoInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "都邦车险",
                    ImageUrl = $"{domain}/mini-RG-main/DuBangAutoInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "安诚车险",
                    ImageUrl = $"{domain}/mini-RG-main/AnchengVehicleInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "渤海车险",
                    ImageUrl = $"{domain}/mini-RG-main/BohaiAutoInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "紫金车险",
                    ImageUrl = $"{domain}/mini-RG-main/ZijinAutomobileInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "中煤车险",
                    ImageUrl = $"{domain}/mini-RG-main/ChinaCoalAutomobileInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "民安车险",
                    ImageUrl = $"{domain}/mini-RG-main/MinAnAutoInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "浙商车险",
                    ImageUrl = $"{domain}/mini-RG-main/ZhejiangAutomobileInsurance.png"
                },
                new InsuranceCompanyVo()
                {
                    DisplayName = "美亚车险",
                    ImageUrl = $"{domain}/mini-RG-main/MayaCarInsurance.png"
                }
            };
            if (!string.IsNullOrEmpty(request.SearchContent))
            {
                result = result.Where(_ => _.DisplayName.Contains(request.SearchContent)).ToList();
            }

            return Result.Success(await Task.FromResult(result));
        }

        /// <summary>
        /// 用户车辆档案
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<UserCarFileVo>> GetUserCarFile([FromQuery] UserCarFileRequest request)
        {
            var result = await _vehicleService.GetUserCarFile(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 更新车辆部位检查信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdateCarPartCheckResult([FromBody] UpdateCarPartCheckResultRequest request)
        {
            var result = await _vehicleService.UpdateCarPartCheckResult(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 车辆部位异常记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<UserCarComponentsResultVo>>> GetUserCarComponentsErrorCheck(
            [FromQuery] UserCarComponentsErrorCheckRequest request)
        {
            var result = await _vehicleService.GetUserCarComponentsErrorCheck(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据车牌查用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<string>> GetUserIdByCarPlate([FromQuery] UserIdByCarPlateRequest request)
        {
            var result = await _vehicleService.GetUserIdByCarPlate(request);

            return Result.Success(result.Item1, result.Item2);
        }

        /// <summary>
        /// Vin码识别车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<VehicleSimpleVo>>> GetVehiclesByVin([FromQuery] VehiclesByVinRequest request)
        {
            var result = await _vehicleService.GetVehiclesByVin(request);

            return Result.Success(result);
        }
    }
}