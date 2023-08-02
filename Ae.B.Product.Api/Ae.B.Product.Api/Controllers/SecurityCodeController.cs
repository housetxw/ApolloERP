using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Request;
using Ae.B.Product.Api.Filters;

namespace Ae.B.Product.Api.Controllers
{
    /// <summary>
    /// 保养适配配置相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    public class SecurityCodeController : ControllerBase
    {
        private readonly ISecurityCodeService _securityCodeService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SecurityCodeController(ISecurityCodeService securityCodeService)
        {
            _securityCodeService = securityCodeService;
        }

        /// <summary>
        /// 防伪码查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<ProductSecurityCodeVo>> GetSecurityCode([FromQuery] SecurityCodeRequest request)
        {
            var result = await _securityCodeService.GetSecurityCode(request);
            return Result.Success(result);
        }
    }
}