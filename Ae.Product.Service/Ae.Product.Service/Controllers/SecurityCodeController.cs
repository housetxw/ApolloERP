using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Product.Service.Core.Interfaces;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Core.Request;
using Ae.Product.Service.Filters;

namespace Ae.Product.Service.Controllers
{
    /// <summary>
    /// 防伪码相关
    /// </summary>
    [Route("[controller]/[action]")]
    // [Filter(nameof(SecurityCodeController))]
    public class SecurityCodeController : ControllerBase
    {
        private readonly ISecurityCodeService _securityCodeService;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="securityCodeService"></param>
        public SecurityCodeController(ISecurityCodeService securityCodeService)
        {
            _securityCodeService = securityCodeService;
        }

        /// <summary>
        /// 防伪码查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ProductSecurityCodeVo>> GetSecurityCode([FromQuery] SecurityCodeRequest request)
        {
            var result = await _securityCodeService.GetSecurityCode(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 批量生成防伪码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> BatchGenerateSecurityCode(
            [FromBody] BatchGenerateSecurityCodeRequest request)
        {
            var result = await _securityCodeService.BatchGenerateSecurityCode(request);

            return Result.Success(result);
        }
    }
}