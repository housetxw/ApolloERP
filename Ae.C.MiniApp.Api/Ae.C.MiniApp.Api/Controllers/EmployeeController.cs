using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 员工相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(EmployeeController))]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="employeeService"></param>
        /// <param name="configuration"></param>
        public EmployeeController(IEmployeeService employeeService, IConfiguration configuration)
        {
            this.employeeService = employeeService;
            _configuration = configuration;
        }

        /// <summary>
        /// 获取员工基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<EmployeeInfoVO>> GetEmployeeInfo([FromQuery] GetEmployeeInfoRequest request)
        {
            var result = await employeeService.GetEmployeeInfo(request);
            return Result.Success(result);

            //var result = new EmployeeInfoVO() 
            //{
            //    Id = "46661238-70a0-482d-aa16-bbd710666666",
            //    Name = "洪小",
            //    Level = "高级技师",
            //    Avatar = $"{_configuration["ImageDomain"]}/Shops/2014113155926.jpg",
            //    Description = "高级技师，曾获得国家一等技师荣誉称号"
            //};
            //return Result.Success(result);
        }
    }
}
