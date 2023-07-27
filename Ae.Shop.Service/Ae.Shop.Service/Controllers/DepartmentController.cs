using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Filters;

namespace Ae.Shop.Service.Controllers
{
    [Route("[controller]/[action]")]
    public class DepartmentController : Controller
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<DepartmentController> logger;
        private readonly string className;

        private readonly IDepartmentService deptSvc;

        public DepartmentController(ApolloErpLogger<DepartmentController> logger,
            IDepartmentService deptSvc)
        {
            this.logger = logger;
            className = this.GetType().Name;

            this.deptSvc = deptSvc;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        /// <summary>
        /// 根据OrgId和部门类型，获取部门列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<DepartmentDTO>>> GetDepartmentListByOrgIdType(DepartmentListReqByOrgIdTypeDTO req)
        {
            var res = await deptSvc.GetDepartmentListByOrgIdType(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 根据OrgId和部门类型，获取部门Select列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<DepartmentSelDTO>>> GetDepartmentSelByOrgIdType(DepartmentListReqByOrgIdTypeDTO req)
        {
            var res = await deptSvc.GetDepartmentSelByOrgIdType(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 根据OrgId和部门类型，获取部门树
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ElementDepartmentTree>>> GetDepartmentTreeByOrgIdType(DepartmentListReqByOrgIdTypeDTO req)
        {
            var res = await deptSvc.GetDepartmentTreeByOrgIdType(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }


        // ---------------------------------- 私有方法 --------------------------------------

    }
}