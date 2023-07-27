using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Filters;

namespace Ae.Shop.Service.Controllers
{
    [Route("[controller]/[action]")]
    public class EmployeePerformanceController : ControllerBase
    {
        private readonly IEmployeePerformanceService _employeePerformanceService;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="shopService"></param>
        public EmployeePerformanceController(IEmployeePerformanceService employeePerformanceService)
        {
            _employeePerformanceService = employeePerformanceService;
        }

        #region 基础绩效配置

        /// <summary>
        /// 保存安装服务绩效配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> CreateOrUpdateBasicPerformanceConfig([FromBody]CreateBasicPerformanceConfigRequest request)
        {
            return await _employeePerformanceService.CreateOrUpdateBasicPerformanceConfig(request);
        }

        /// <summary>
        /// 修改安装服务绩效开关
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> UpdateBasicPerformanceFlag([FromBody]UpdateBasicPerformanceFlagRequest request)
        {
            return await _employeePerformanceService.UpdateBasicPerformanceFlag(request);
        }

        /// <summary>
        /// 查询安装服务绩效配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetBasicPerformanceConfigResponse>> GetBasicPerformanceConfig([FromQuery]GetBasicPerformanceConfigRequest request)
        {
            return await _employeePerformanceService.GetBasicPerformanceConfig(request);
        }
        #endregion

        #region 门店绩效配置

        /// <summary>
        /// 新增门店绩效服务配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> CreateShopPerformanceConfig([FromBody] ShopPerformanceConfigDTO request)
        {
            return await _employeePerformanceService.CreateShopPerformanceConfig(request);
        }

        /// <summary>
        /// 更新门店绩效配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> UpdateShopPerformanceConfig([FromBody] ShopPerformanceConfigDTO request)
        {
            return await _employeePerformanceService.UpdateShopPerformanceConfig(request);
        }

        /// <summary>
        /// 删除门店绩效配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> DeleteShopPerformanceConfig([FromBody] ShopPerformanceConfigDTO request)
        {
            return await _employeePerformanceService.DeleteShopPerformanceConfig(request);
        }

        /// <summary>
        /// 获取门店绩效配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ShopPerformanceConfigDTO>>> GetShopPerformanceConfig([FromQuery] GetBasicPerformanceConfigRequest request)
        {
            return await _employeePerformanceService.GetShopPerformanceConfig(request);
        }
        #endregion

        #region 拉新绩效配置
        /// <summary>
        /// 保存拉新奖励配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> CreateOrUpdatePullNewConfig([FromBody]CreateOrUpdatePullNewConfigRequest request)
        {
            return await _employeePerformanceService.CreateOrUpdatePullNewConfig(request);
        }

        /// <summary>
        /// 修改拉新奖励配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> UpdatePullNewFlag([FromBody]UpdatePullNewFlagRequest request)
        {
            return await _employeePerformanceService.UpdatePullNewFlag(request);
        }

        /// <summary>
        /// 查询拉新奖励配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetPullNewConfigResponse>> GetPullNewConfig([FromQuery]GetPullNewConfigRequest request)
        {
            return await _employeePerformanceService.GetPullNewConfig(request);
        }
        #endregion

        [HttpPost]
        public async Task<ApiResult<string>> CollectEmployeePerformanceReport()
        {
            return await _employeePerformanceService.CollectEmployeePerformanceReport();
        }

        /// <summary>
        /// 获取员工绩效列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<EmployeePerformanceDto>>> GetEmployeePerformanceList([FromQuery]EmployeePerformanceRequest request)
        {
            var rusult= await _employeePerformanceService.GetEmployeePerformanceList(request);
            return Result.Success(rusult);
        }

        /// <summary>
        /// 获取员工绩效详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<EmployeePerformanceDetialDto>> GetEmployeePerformanceDetial(EmployeePerformanceDetialRequest request)
        {
            var rusult = await _employeePerformanceService.GetEmployeePerformanceDetial(request);
            return Result.Success(rusult);
        }

        /// <summary>
        /// 获取技师绩效列表V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<TechPerformanceDto>>> GetTechPerformanceList([FromQuery] TechPerformanceRequest request)
        {
            var rusult = await _employeePerformanceService.GetTechPerformanceList(request);
            return Result.Success(rusult);
        }

        /// <summary>
        /// 获取技师绩效明细V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<TechPerformanceDetailDto>>> GetTechPerformanceDetail([FromQuery] TechPerformanceDetailRequest request)
        {
            var rusult = await _employeePerformanceService.GetTechPerformanceDetail(request);
            return Result.Success(rusult);
        }

    }
}