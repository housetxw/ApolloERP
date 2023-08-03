using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Controllers
{
    [Route("[controller]/[action]")]
    // [Filter(nameof(EmployeePerformanceController))]
    public class EmployeePerformanceController : ControllerBase
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private IEmployeePerformanceService _employeePerformanceService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shopReportService"></param>
        public EmployeePerformanceController(IEmployeePerformanceService employeePerformanceService)
        {
            this._employeePerformanceService = employeePerformanceService;
        }


        #region 员工绩效
        /// <summary>
        /// 获取员工绩效列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResult<List<EmployeePerformanceOrderDTO>>> GetEmployeePerformanceOrderList([FromQuery] EmployeePerformanceRequest request)
        {
           
            var result = await _employeePerformanceService.GetEmployeePerformanceOrderList(request);
            return result;

        }

        /// <summary>
        /// 更新员工绩效订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateEmployeePerformanceOrder([FromBody] UpdateEmployeePerformanceRequest request)
        {
            return await _employeePerformanceService.UpdateEmployeePerformanceOrder(request);
        }


        #endregion

    }
}
