using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.ShopReport;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Request.ShopReport;
using Ae.Shop.Api.Core.Response.ShopReport;
using Ae.Shop.Api.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Controllers
{
    [Route("[controller]/[action]")]
    // [Filter(nameof(ShopReportController))]
    public class ShopReportController : ControllerBase
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private IShopReportService shopReportService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shopReportService"></param>
        public ShopReportController(IShopReportService shopReportService)
        {
            this.shopReportService = shopReportService;
        }


        /// <summary>
        /// 报表查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ShopSalesMonthResponse>>> GetShopSalesMonthList([FromQuery] GetShopSalesMonthRequest request)
        {
            return await shopReportService.GetShopSalesMonthList(request);
        }

        #region 员工绩效报表
        /// <summary>
        /// 获取员工绩效列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResult<List<EmployeePerformanceDto>>> GetEmployeePerformanceList([FromQuery] EmployeePerformanceRequest request)
        {
           
            var result = await shopReportService.GetEmployeePerformanceList(request);
            return result;

        }
        #endregion

    }
}
