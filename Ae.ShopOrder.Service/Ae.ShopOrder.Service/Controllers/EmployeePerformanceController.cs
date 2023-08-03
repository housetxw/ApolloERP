using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Interfaces;
using Ae.ShopOrder.Service.Core.Model;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Request.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Controllers
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
        /// <param name="employeePerformanceService"></param>
        public EmployeePerformanceController(IEmployeePerformanceService employeePerformanceService)
        {
            this._employeePerformanceService = employeePerformanceService;
        }

        #region 员工绩效
        /// <summary>
        /// 获取员工绩效明细列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResult<List<EmployeePerformanceOrderDTO>>> GetEmployeePerformanceOrderList([FromQuery] EmployeePerformanceRequest request)
        {
           
            var result = await _employeePerformanceService.GetEmployeePerformanceOrderList(request);
            return Result.Success(result);

        }

        /// <summary>
        /// 批次更新员工绩效订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> BatchUpdateEmployeePerformanceOrder([FromBody] ApiRequest<BatchUpdateOrderRequest> request)
        {
            return await _employeePerformanceService.UpdateOrderPerformance(request.Data);
        }

        #endregion

    }
}
