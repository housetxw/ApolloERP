using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog.Filters;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.AccountAuthority.Service.Common.Constant;
using Ae.AccountAuthority.Service.Common.Exceptions;
using Ae.AccountAuthority.Service.Common.Extension;
using Ae.AccountAuthority.Service.Core.Interfaces;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;

namespace Ae.AccountAuthority.Service.Controllers
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(AuthorizationController))]
    public class AuthorizationController : Controller
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<AuthorizationController> logger;
        private readonly IMapper mapper;
        private readonly string className;

        private readonly IEmployeeRoleService empRoleSvc;

        public AuthorizationController(ApolloErpLogger<AuthorizationController> logger, IMapper mapper, IEmployeeRoleService empRoleSvc)
        {
            this.logger = logger;
            this.mapper = mapper;
            className = this.GetType().Name;

            this.empRoleSvc = empRoleSvc;
        }

        // ---------------------------------- ！！！授权接口实现！！！ --------------------------------------
        /// <summary>
        /// 授权接口：根据EmployeeId，OrganizationId和EmployeeType，获取APP其所有的菜单(即菜单权限)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<AuthorizationAPPResDTO>> GetEmpAuthorityListForAPPByEmpIdAsync(AuthorizationReqDTO req)
        {
            if (req == null || req.EmployeeType == EmployeeType.None)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<AuthorizationAPPResDTO>(ResultCode.ArgumentError, CommonConstant.ArgumentError);
            }

            AuthorizationAPPResDTO res = new AuthorizationAPPResDTO();
            try
            {
                //if (req.EmployeeType == EmployeeType.Shop) req.OrganizationId = CommonConstant.Zero;

                res = await empRoleSvc.GetEmpAuthorityListForAPPByEmpIdAsync(req);
                return Result.Success(res, CommonConstant.QuerySuccess);
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(), e);
                throw new CustomException(CommonConstant.InternalError);
            }
            finally
            {
                logger.Info($"SVC: {className}.GetEmpAuthorityListForAPPByEmpIdAsync 请求参数：{JsonConvert.SerializeObject(req)}");
                logger.Info($"SVC: {className}.GetEmpAuthorityListForAPPByEmpIdAsync 返回值：{JsonConvert.SerializeObject(res)}");
            }
        }


    }
}