using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Account.Api.Common.Constant;
using Ae.Account.Api.Core.CommonModel;
using Ae.Account.Api.Core.Interfaces;
using Ae.Account.Api.Core.Response;
using Ae.Account.Api.Filters;

namespace Ae.Account.Api.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(CommonBusLogicalController))]
    public class CommonBusLogicalController : Controller
    {
        #region 变量和构造器

        private readonly ApolloErpLogger<EmployeeRoleController> logger;
        private readonly IMapper mapper;

        private readonly ICompanyService companySvc;

        private readonly IEmployeeRoleService empRoleSvc;

        public CommonBusLogicalController(ApolloErpLogger<EmployeeRoleController> logger, IMapper mapper,
            ICompanyService companySvc,
            IEmployeeRoleService empRoleSvc)
        {
            this.logger = logger;
            this.mapper = mapper;

            this.companySvc = companySvc;

            this.empRoleSvc = empRoleSvc;
        }

        #endregion 变量和构造器

        #region 接口实现

        /// <summary>
        /// 获取所有的所属单位列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<OrganizationSel>>> GetAllOrganizationSelectListAsync()
        {
            var res = await companySvc.GetAllOrganizationSelectListAsync();
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 获取除门店以外，所有的所属单位列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResult<List<OrganizationSel>>> GetAllOrganizationExceptShopSelectListAsync()
        {
            var res = await companySvc.GetAllOrganizationExceptShopSelectListAsync();
            res.Insert(1, new OrganizationSel()
            {
                Id = "0",
                Name = "虚拟公司系统(仅限于当前系统)",
                Type = Core.Model.RoleType.Company,
                Status = CommonConstant.Zero
            });
            
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        #endregion 接口实现

        #region 私有方法

        #endregion 私有方法
    }
}