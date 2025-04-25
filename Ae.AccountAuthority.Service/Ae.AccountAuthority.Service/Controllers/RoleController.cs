using System;
using System.Collections.Generic;
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
    /// <summary>
    /// 角色
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(RoleController))]
    public class RoleController : Controller
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<RoleController> logger;
        private readonly string className;

        private readonly IRoleService roleSvc;

        public RoleController(ApolloErpLogger<RoleController> logger, IRoleService roleSvc)
        {
            this.logger = logger;
            className = this.GetType().Name;

            this.roleSvc = roleSvc;
        }

        // ---------------------------------- 对外接口 --------------------------------------
        /// <summary>
        /// (对外接口)获取门店的角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<RoleDTO>>> GetShopRoleList(int? features=null)
        {
            var res = new List<RoleDTO>();
            try
            {
                res = await GetRoleListByOrgIdAsync(new RoleListReqDTO
                {
                    OrganizationId = CommonConstant.Zero,
                    Type = RoleType.Shop,
                    IsDeleted = CommonConstant.Zero,
                    Features=features
                });
                return Result.Success(res, CommonConstant.QuerySuccess);
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(CommonConstant.ParameterNone).GenExceptionInfo(), e);
                throw new CustomException(CommonConstant.InternalError);
            }
            finally
            {
                logger.Info($"SVC: {className}.GetShopRoleList 返回值： {JsonConvert.SerializeObject(res)}");
            }
        }

        /// <summary>
        /// (对外接口)根据EmployeeType和OrgId，获取角色列表信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<RoleDTO>>> GetRoleListByOrgIdAndType([FromQuery] RoleListReqDTO req)
        {
            if (req?.Type == RoleType.Shop) return await GetShopRoleList(req.Features);

            var res = new List<RoleDTO>();
            try
            {
                res = await GetRoleListByOrgIdAsync(req);
                return Result.Success(res, CommonConstant.QuerySuccess);
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(), e);
                throw new CustomException(CommonConstant.InternalError);
            }
            finally
            {
                logger.Info($"SVC: {className}.GetRoleListByOrgIdAndType 请求值： {JsonConvert.SerializeObject(req)}");
                logger.Info($"SVC: {className}.GetRoleListByOrgIdAndType 返回值： {JsonConvert.SerializeObject(res)}");
            }
        }

        /// <summary>
        /// (对外接口)根据OrgIds获取角色列表信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<OrgRangeRolesDTO>>> GetRoleListByOrgIds([FromBody] List<OrgEntityReqDTO> req)
        {
            if (req == null || req.Count <= 0)
            {
                return Result.Failed<List<OrgRangeRolesDTO>>(ResultCode.ArgumentError, "请至少输入一条单位和角色信息");
            }

            req.ForEach(f =>
            {
                if (f.Type == RoleType.Shop) f.OrganizationId = CommonConstant.Zero;
            });

            //var tmpReq = req.FindAll(f => f.Type != RoleType.Shop);
            //var tmpShopRoleObj = req.Find(f => f.Type == RoleType.Shop);
            //if (tmpShopRoleObj != null) tmpReq.Add(tmpShopRoleObj);

            var res = await roleSvc.GetRoleListByOrgIds(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }



        // ---------------------------------- 接口实现 --------------------------------------
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateRole([FromBody] RoleDTO req)
        {
            var res = await roleSvc.CreateRole(req);
            return res;
        }

        /// <summary>
        /// 编辑角色权限
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> SaveRoleAuthority([FromBody] RoleAuthorityReqDTO req)
        {
            return await roleSvc.SaveRoleAuthority(req);
        }

        /// <summary>
        /// 根据Id更新角色信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateRoleById([FromBody] RoleDTO req)
        {
            var res = await roleSvc.UpdateRoleById(req);
            return res;
        }

        /// <summary>
        /// 根据Id删除角色
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DeleteRoleById([FromBody] RoleDTO req)
        {
            var res = await roleSvc.DeleteRoleById(req);
            return res;
        }

        /// <summary>
        /// 获取角色分页数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<RolePageListResDTO> GetPagedRoleList([FromQuery] RoleListReqDTO req)
        {
            var res = await roleSvc.GetPagedRoleList(req);
            return res;
        }

        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<RoleDTO>> GetAllRoleList()
        {
            var res = await roleSvc.GetAllRoleList();
            return res;
        }

        /// <summary>
        /// 根据所属单位Id，获取其角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<RoleDTO>> GetRoleListByOrgIdAsync([FromQuery] RoleListReqDTO req)
        {
            var res = await roleSvc.GetRoleListByOrgIdAsync(req);
            return res;
        }

        /// <summary>
        /// 根据任意条件获取角色信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<RolePageResDTO>> GetRoleListAnyCondition([FromQuery] RoleListInternalReqDTO req)
        {
            var res = await roleSvc.GetRoleListAnyCondition(req);
            return res;
        }

        /// <summary>
        /// 根据roleId获取其所有的权限列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<RoleAuthorityDTO>> GetRoleAuthorityListByRoleId(RoleAuthorityListReqByRoleIdDTO req)
        {
            var res = await roleSvc.GetRoleAuthorityListByRoleId(req);
            return res;
        }


    }
}