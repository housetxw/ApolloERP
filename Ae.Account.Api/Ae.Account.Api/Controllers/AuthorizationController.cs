using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;
using Ae.Account.Api.Common.Constant;
using Ae.Account.Api.Common.Extension;
using Ae.Account.Api.Core.Interfaces;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;
using Ae.Account.Api.Filters;

namespace Ae.Account.Api.Controllers
{
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
        /// 授权接口：根据EmployeeId，EmployeeType和OrganizationId，获取Web端其所有的菜单(即菜单权限)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<AuthorizationWebComplexResVO>> GetEmpAuthorityListForWebByEmpIdAsync(AuthorizationReqVO req)
        {
            if (req == null || req.EmployeeType == EmployeeType.None)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<AuthorizationWebComplexResVO>(ResultCode.ArgumentError, "请输入正确的请求参数");
            }

            logger.Info($"API: {className}.GetEmpAuthorityListForWebByEmpIdAsync 请求参数：{JsonConvert.SerializeObject(req)}");

            req.IsOrganizationRange = false;
            var resDto = await empRoleSvc.GetEmpAuthorityListForWebByEmpIdAsync(req);
            var resVo = mapper.Map<List<AuthorizationWebResVO>>(resDto);

            var topMenu = new List<TopMenuVO>();
            resDto.ForEach(f =>
                {
                    if (topMenu.Find(t => t.ApplicationId == f.ApplicationId) == null)
                    {
                        topMenu.Add(new TopMenuVO
                        {
                            ApplicationId = f.ApplicationId,
                            Title = f.ApplicationName
                        });
                    }
                });
            var res = new AuthorizationWebComplexResVO
            {
                TopMenu = topMenu,
                SubMenu = resVo
            };
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 授权接口：根据EmployeeId，EmployeeType和OrganizationId，获取Shop Web端其所有的菜单(即菜单权限)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<AuthorizationWebResVO>>> GetEmpAuthorityListForShopWebByEmpId(AuthorizationReqVO req)
        {
            if (req == null || req.EmployeeType == EmployeeType.None)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<List<AuthorizationWebResVO>>(ResultCode.ArgumentError, "请输入正确的请求参数");
            }

            logger.Info($"API: {className}.GetEmpAuthorityListForShopWebByEmpId 请求参数：{JsonConvert.SerializeObject(req)}");

            var resDto = await empRoleSvc.GetEmpAuthorityListForWebByEmpIdAsync(req);
            var res = mapper.Map<List<AuthorizationWebResVO>>(resDto);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }


        // ---------------------------------- 接口实现 --------------------------------------


        // ---------------------------------- 私有方法 --------------------------------------

    }
}