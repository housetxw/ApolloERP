using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;
using Ae.Account.Api.Common.Constant;
using Ae.Account.Api.Common.Extension;
using Ae.Account.Api.Common.Http;
using Ae.Account.Api.Core.CommonModel;
using Ae.Account.Api.Core.Interfaces;
using Ae.Account.Api.Core.Model;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;
using Ae.Account.Api.Filters;
using RoleType = Ae.Account.Api.Core.Model.RoleType;

namespace Ae.Account.Api.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(EmployeeRoleController))]
    public class EmployeeRoleController : Controller
    {
        #region 变量和构造器

        private readonly ApolloErpLogger<EmployeeRoleController> logger;
        private readonly IMapper mapper;
        private readonly IIdentityService identitySvc;

        private readonly IAccountService acctSvc;
        private readonly IEmployeeRoleService empRoleSvc;
        private readonly IRoleService roleSvc;

        private readonly ICompanyService companySvc;
        private readonly IEmployeeService empSvc;

        public EmployeeRoleController(ApolloErpLogger<EmployeeRoleController> logger, IMapper mapper,
            IIdentityService identitySvc,
            IAccountService acctSvc,
            IEmployeeRoleService empRoleSvc,
            IRoleService roleSvc,
            ICompanyService companySvc,
            IEmployeeService empSvc)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.identitySvc = identitySvc;

            this.acctSvc = acctSvc;
            this.roleSvc = roleSvc;
            this.empRoleSvc = empRoleSvc;

            this.companySvc = companySvc;
            this.empSvc = empSvc;
        }

        #endregion 变量和构造器

        #region 接口实现

        /// <summary>
        /// 获取全量员工的关键性非敏感信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<EmployeeResVO>>> GetAllEmployeeListAsync()
        {
            var res = await empSvc.GetAllEmployeeListAsync();
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 根据所属单位id，获取员工的关键性非敏感信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<EmployeeResVO>>> GetEmployeeListByOrgIdAndTypeAsync([FromQuery] EmployeeListReqVO req)
        {
            var res = await empSvc.GetEmployeeListByOrgIdAndTypeAsync(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 获取分页员工的关键性非敏感列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<EmployeePageResVO>> GetEmployeePage([FromQuery] EmployeePageReqVO req)
        {
            var acctDic = new Dictionary<string, string>();
            var resPage = await empSvc.GetEmployeePage(req);
            if (resPage?.Items?.Count > 0)
            {
                var ids = resPage.Items.Select(s => s.AccountId).Distinct().ToList();
                var acctList = await acctSvc.GetAccountKeyInfoListById(new AccountListReqDTO
                {
                    Id = ids
                    //,IsDeleted = req.IsDeleted
                });

                if (acctList != null)
                {
                    acctDic = acctList.Distinct().ToDictionary(a => a.Id, a => a.Name);
                }

                resPage.Items.ToList().ForEach(f =>
                {
                    //f.AccountName = acctDic.ContainsKey(f.AccountId) ? acctDic[f.AccountId].HidePhoneSensitiveInfo() : " N/A ";
                    f.AccountName = acctDic.ContainsKey(f.AccountId) ? acctDic[f.AccountId] : " N/A ";
                    f.OrganizationId = string.Join(CommonConstant.SeparatorVertical,
                        f.Type == EmployeeType.Shop ? CommonConstant.Zero.ToString() : f.OrganizationId,
                        f.Type);

                    if (f.CreateBy.IsNotNullOrWhiteSpace())
                    {
                        var opr = f.CreateBy.Split(CommonConstant.SeparatorVertical);
                        f.CreateBy = opr.FirstOrDefault();
                        f.CreateId = opr.LastOrDefault();
                    }
                    if (f.UpdateBy.IsNotNullOrWhiteSpace())
                    {
                        var opr = f.UpdateBy.Split(CommonConstant.SeparatorVertical);
                        f.UpdateBy = opr.FirstOrDefault();
                        f.UpdateId = opr.LastOrDefault();
                    }
                });
            }
            return Result.Success(resPage?.Items, resPage?.TotalItems ?? 0);
        }

        /// <summary>
        /// 根据所属单位Id，获取其角色列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResult<List<EmployeeRoleLisVO>>> GetRoleListByOrgIdAsync([FromQuery] EmployeeRoleDialogListReqVO req)
        {
            var orgList = await companySvc.GetAllOrganizationExceptShopSelectListAsync();
            var orgDic = orgList?.ToDictionary(a => a.Id, a => a.Name);

            var res = await empRoleSvc.GetRoleListByOrgIdAsync(req);
            res.ForEach(f =>
            {
                if (f.RoleType == RoleType.Shop)
                {
                    f.OrganizationId = CommonConstant.Zero.ToString();
                }
                var combine = string.Join(CommonConstant.SeparatorVertical, f.OrganizationId, f.RoleType);
                f.OrganizationId = combine;
                f.OrganizationName =
                    orgDic != null && orgDic.ContainsKey(combine) ? orgDic[combine] : " - ";

                if (f.CreateBy.IsNotNullOrWhiteSpace())
                {
                    var opr = f.CreateBy.Split(CommonConstant.SeparatorVertical);
                    f.CreateBy = opr.FirstOrDefault();
                    f.CreateId = opr.LastOrDefault();
                }
                if (f.UpdateBy.IsNotNullOrWhiteSpace())
                {
                    var opr = f.UpdateBy.Split(CommonConstant.SeparatorVertical);
                    f.UpdateBy = opr.FirstOrDefault();
                    f.UpdateId = opr.LastOrDefault();
                }
            });
            if (req.Type == (int)RoleType.Company || (int)req.Type == (int)RoleType.Extend || (int)req.Type == (int)RoleType.Supplier)
            {
                var companyRole = await empRoleSvc.GetRoleListByOrgIdAsync(new EmployeeRoleDialogListReqVO()
                {
                    OrganizationId = 0,
                    Type = req.Type
                });

                if (companyRole != null && companyRole.Any())
                {
                    companyRole?.ForEach(_ =>
                    {
                        _.OrganizationName = "虚拟公司系统(仅限于当前系统)";
                    });
                    res.AddRange(companyRole);
                }

            }

            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 获取EmployeeId获取其角色列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResult<List<EmployeeRoleLisVO>>> GetEmployeeRoleListByEmpIdAsync([FromQuery] EmployeeRoleListReqVO req)
        {
            var orgList = await companySvc.GetAllOrganizationExceptShopSelectListAsync();
            var orgDic = orgList?.ToDictionary(a => a.Id, a => a.Name);

            var res = await empRoleSvc.GetEmployeeRoleListByEmpIdAsync(req);
            res.ForEach(f =>
            {
                if (f.RoleType == RoleType.Shop)
                {
                    f.OrganizationId = CommonConstant.Zero.ToString();
                }
                var orgId = f.OrganizationId;
                var combine = string.Join(CommonConstant.SeparatorVertical, f.OrganizationId, f.RoleType);
                f.OrganizationId = combine;
                f.OrganizationName =
                    orgDic != null && orgDic.ContainsKey(combine) ? orgDic[combine] : " - ";
                if (f.RoleType == RoleType.Company && orgId == CommonConstant.Zero.ToString())
                {
                    if (f.OrganizationName.Trim().Equals("-"))
                    {
                        f.OrganizationName = "虚拟公司系统(仅限于当前系统)";
                    }
                }

                if (f.CreateBy.IsNotNullOrWhiteSpace())
                {
                    var opr = f.CreateBy.Split(CommonConstant.SeparatorVertical);
                    f.CreateBy = opr.FirstOrDefault();
                    f.CreateId = opr.LastOrDefault();
                }
                if (f.UpdateBy.IsNotNullOrWhiteSpace())
                {
                    var opr = f.UpdateBy.Split(CommonConstant.SeparatorVertical);
                    f.UpdateBy = opr.FirstOrDefault();
                    f.UpdateId = opr.LastOrDefault();
                }
            });

            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 编辑员工权限
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditEmployeeRole([FromBody] BaseEntityPostRequest<EmployeeRoleSaveReqVO> req)
        {
            var data = req.Data;

            var opr = string.Join("|", identitySvc.GetUserName(), identitySvc.GetUserId());
            data.Operator = opr; // "CrtByEmployeeRoleCntr";
            var res = await empRoleSvc.EditEmployeeRole(data);
            return Result.Success(res, CommonConstant.OperateSuccess);
        }


        #endregion 接口实现

        #region 私有方法

        #endregion 私有方法
    }
}