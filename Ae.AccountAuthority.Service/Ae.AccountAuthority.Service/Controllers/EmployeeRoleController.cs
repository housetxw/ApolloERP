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
using Ae.AccountAuthority.Service.Common.Constant;
using Ae.AccountAuthority.Service.Common.Exceptions;
using Ae.AccountAuthority.Service.Common.Extension;
using Ae.AccountAuthority.Service.Core.Interfaces;
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;
using Ae.AccountAuthority.Service.Filters;

namespace Ae.AccountAuthority.Service.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(EmployeeRoleController))]
    public class EmployeeRoleController : Controller
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<EmployeeRoleController> logger;
        private readonly IMapper mapper;
        private readonly string className;

        private readonly IEmployeeRoleService empRoleSvc;

        public EmployeeRoleController(ApolloErpLogger<EmployeeRoleController> logger, IMapper mapper, IEmployeeRoleService empRoleSvc)
        {
            this.mapper = mapper;
            this.logger = logger;
            className = this.GetType().Name;

            this.empRoleSvc = empRoleSvc;
        }

        // ---------------------------------- 对外接口 --------------------------------------
        /// <summary>
        /// (对外接口)根据RoleId集合，编辑员工角色
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditEmployeeRoleWithRoleId([FromBody] EmployeeRoleSaveReqWithRoleIdDTO req)
        {
            if (null == req)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<bool>(ResultCode.ArgumentError, CommonConstant.ArgumentError);
            }

            var res = false;
            try
            {
                res = await empRoleSvc.EditEmployeeRoleWithRoleId(req);
                if (!res)
                {
                    return Result.Failed<bool>(CommonConstant.OperateFailure);
                }

                return Result.Success(res, CommonConstant.OperateSuccess);
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(), e);
                throw new CustomException(CommonConstant.InternalError);
            }
            finally
            {
                logger.Info($"SVC: {className}.EditEmployeeRoleWithRoleId 请求参数： {JsonConvert.SerializeObject(req)}");
                logger.Info($"SVC: {className}.EditEmployeeRoleWithRoleId 返回值： {JsonConvert.SerializeObject(res)}");
            }
        }

        /// <summary>
        /// (对外接口)添加门店员工默认角色
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddShopEmployeeDefaultRole([FromBody] EmployeeDefaultRoleReqDTO req)
        {
            if (null == req)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<bool>(ResultCode.ArgumentError, CommonConstant.ArgumentError);
            }

            var (flag, message) = (false, "");
            try
            {
                (flag, message) = await empRoleSvc.AddShopEmployeeDefaultRole(req);
                if (!flag)
                {
                    return Result.Failed<bool>(message);
                }

                return Result.Success(true, message);
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(), e);
                throw new CustomException(CommonConstant.InternalError);
            }
            finally
            {
                logger.Info($"SVC: {className}.AddShopEmployeeDefaultRole 请求参数： {JsonConvert.SerializeObject(req)}");
                logger.Info($"SVC: {className}.AddShopEmployeeDefaultRole 返回值： {JsonConvert.SerializeObject((flag, message))}");
            }
        }

        /// <summary>
        /// (对外接口)根据EmployeeId，获取其所有的角色信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<EmployeeRoleListDTO>>> GetEmployeeRoleListByEmpId([FromQuery] EmployeeRoleListReqDTO req)
        {
            var res = await GetEmployeeRoleListByEmpIdAsync(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// (对外接口)根据EmployeeIds，获取其所有的角色信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<EmployeeRolesDTO>>> GetEmployeeRoleListByEmpIds([FromBody] EmployeeRoleListByEmpIdsReqDTO req)
        {
            return Result.Success(await empRoleSvc.GetEmployeeRoleListByEmpIds(req), CommonConstant.QuerySuccess);
        }


        // ---------------------------------- 授权接口实现 --------------------------------------
        /// <summary>
        /// 授权接口：根据EmployeeId，EmployeeType和OrganizationId，获取其所有的菜单(即菜单权限)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<AuthorizationResDTO>> GetEmpAuthorityListByEmpIdAsync([FromQuery] AuthorizationReqDTO req)
        {
            List<AuthorizationResDTO> res = new List<AuthorizationResDTO>();

            if (req == null || req.EmployeeType == EmployeeType.None)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return res;
            }

            if (req.EmployeeType == EmployeeType.Shop) req.OrganizationId = CommonConstant.Zero;
            res = await empRoleSvc.GetEmpAuthorityListByEmpIdAsync(req);
            return res;
        }

        /// <summary>
        /// 授权接口：根据EmployeeId和RoleIds，获取其所有的菜单(即菜单权限)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<AuthorizationResDTO>> GetRangeRoleAuthorityListByIds([FromBody] RangeRoleAuthorityReqDTO req)
        {
            List<AuthorizationResDTO> res = new List<AuthorizationResDTO>();

            if (req == null || req.EmployeeId.IsNullOrWhiteSpace() || !req.RoleIds.Any())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return res;
            }

            res = await empRoleSvc.GetRangeRoleAuthorityListByIds(req);
            return res;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        /// <summary>
        /// 根据EmployeeId获取其所有的角色
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeRoleListDTO>> GetEmployeeRoleListByEmpIdAsync([FromQuery] EmployeeRoleListReqDTO req)
        {
            List<EmployeeRoleListDTO> res = new List<EmployeeRoleListDTO>();

            if (req == null || req.EmployeeId.IsNullOrWhiteSpace())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return res;
            }

            res = await empRoleSvc.GetEmployeeRoleListByEmpIdAsync(req);
            return res;
        }

        /// <summary>
        /// 编辑员工权限
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> EditEmployeeRole([FromBody] EmployeeRoleSaveReqDTO req)
        {
            var res = await empRoleSvc.EditEmployeeRole(req);
            return res;
        }


    }
}