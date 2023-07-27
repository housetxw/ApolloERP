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
using Ae.Account.Api.Common.Constant;
using Ae.Account.Api.Common.Extension;
using Ae.Account.Api.Common.Http;
using Ae.Account.Api.Core.CommonModel;
using Ae.Account.Api.Core.Interfaces;
using Ae.Account.Api.Core.Model;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;
using Ae.Account.Api.Filters;

namespace Ae.Account.Api.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(RoleController))]
    public class RoleController : Controller
    {
        #region 变量和构造器

        private readonly ApolloErpLogger<AuthorityController> logger;
        private readonly IMapper mapper;
        private readonly IIdentityService identitySvc;

        private readonly IRoleService roleSvc;

        private readonly ICompanyService companySvc;

        public RoleController(ApolloErpLogger<AuthorityController> logger, IMapper mapper,
            IIdentityService identitySvc,
            IRoleService roleSvc, ICompanyService companySvc)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.identitySvc = identitySvc;

            this.roleSvc = roleSvc;
            this.companySvc = companySvc;
        }

        #endregion 变量和构造器

        #region 接口实现

        /// <summary>
        /// 创建或是编辑权限
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> CreateOrUpdateRole([FromBody] BaseEntityPostRequest<RoleVO> req)
        {
            bool res;
            var data = req.Data;

            Int32.TryParse(identitySvc.GetOrganizationId(), out var shopId);
            var orgType = identitySvc.GetOrgType();//0-公司，1-门店

            data.OrganizationId = shopId;
            data.Type = orgType == "0" ? RoleType.Company : RoleType.Shop;
           
            //if (data.Type != RoleType.Shop && data.OrganizationId <= 0)
            //{
            //    return Result.Failed<bool>("请选择正确的所属单位");
            //}

            var reqVo = mapper.Map<RoleListInternalReqDTO>(data);
            //不检查角色是否重复
            //var errMsg = await DuplicationValidate(reqVo);
            //if (errMsg.IsNotNullOrWhiteSpace())
            //{
            //    return Result.Failed<bool>(ResultCode.Failed, errMsg);
            //}

            var opr = identitySvc.GetUserName();// string.Join("|", identitySvc.GetUserName(), identitySvc.GetUserId());
            if (data.Id > 0)
            {
                data.UpdateBy = opr;//"UdByRoleCntr";
                data.UpdateTime = DateTime.Now;
                data.Features = data.Features;
                res = await roleSvc.UpdateRoleById(data);
                return Result.Success(res, CommonConstant.OperateSuccess);
            }

            data.CreateBy = opr;// "CrtByRoleCntr";
            res = await roleSvc.CreateRole(data);
            return Result.Success(res, CommonConstant.OperateSuccess);
        }

        /// <summary>
        /// 编辑角色的权限
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SaveRoleAuthority([FromBody] BaseEntityPostRequest<RoleAuthorityReqVO> req)
        {
            var data = req.Data;
            var opr = identitySvc.GetUserName();// string.Join("|", identitySvc.GetUserName(), identitySvc.GetUserId());
            data.UpdateBy = opr; // "UdByRoleCntr";
            data.CreateBy = opr; // "CrtByRoleCntr";
            var res = await roleSvc.SaveRoleAuthority(data);
            return Result.Success(res, CommonConstant.OperateSuccess);
        }

        /// <summary>
        /// 根据Id逻辑删除权限
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteRoleById([FromBody] BaseEntityPostRequest<RoleVO> req)
        {
            var opr = identitySvc.GetUserName();// string.Join("|", identitySvc.GetUserName(), identitySvc.GetUserId());
            req.Data.UpdateBy = opr; // "DelByRoleCntr";
            req.Data.UpdateTime = DateTime.Now;

            var res = await roleSvc.DeleteRoleById(req.Data);
            return Result.Success(res, CommonConstant.OperateSuccess);
        }

        /// <summary>
        /// 获取角色分页数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiPagedResult<RolePageResVO>> GetPagedRoleList([FromQuery] RoleListReqVO req)
        {
            Int32.TryParse(identitySvc.GetOrganizationId(), out var shopId);
            var orgType = identitySvc.GetOrgType();//0-公司，1-门店
            if (shopId <= 1)//boss查询
            {
            }
            else //shop查询
            {
                req.Type = orgType == "0" ? RoleType.Company : RoleType.Shop;
                req.OrganizationId = shopId;
            }

            var res = await roleSvc.GetPagedRoleList(req);
            res.RoleList.ForEach(r => {

                if (r.OrganizationId == CommonConstant.Zero.ToString())
                {
                    if (r.Type == RoleType.Shop)
                    {
                        r.OrganizationName = "门店系统权限模板";
                    }
                    if (r.Type == RoleType.Company)
                    {
                        r.OrganizationName = "管理公司系统权限模板";
                    }
                }
                else
                {
                    r.OrganizationName = r.OrganizationId;
                }
            });
            /*查询单位信息太慢，只显示ID
            var orgList = await companySvc.GetAllOrganizationExceptShopSelectListAsync();
            var orgDic = orgList?.ToDictionary(a => a.Id, a => a.Name);
            res.RoleList.ForEach(r =>
            {
                if (r.Type == RoleType.Shop)
                {
                    r.OrganizationId = CommonConstant.Zero.ToString();
                }
                var combine = $"{r.OrganizationId}|{r.Type}";
              
                if (r.OrganizationId == CommonConstant.Zero.ToString())
                {
                    if (r.Type == RoleType.Shop)
                    {
                        r.OrganizationName = "虚拟门店系统(仅限于当前系统)";
                    }
                    if (r.Type == RoleType.Company||r.Type==RoleType.Extend||r.Type==RoleType.Supplier)
                    {
                        combine = r.OrganizationId;
                        r.OrganizationName = "虚拟公司系统(仅限于当前系统)";
                    }
                }
                else
                {
                    r.OrganizationName =
                     orgDic != null && orgDic.ContainsKey(combine) ? orgDic[combine] : " - ";
                }
                r.OrganizationId = combine;
                // r.OrganizationName =
                //  orgDic != null && orgDic.ContainsKey(combine) ? orgDic[combine] : " - ";

                if (r.CreateBy.IsNotNullOrWhiteSpace())
                {
                    var opr = r.CreateBy.Split(CommonConstant.SeparatorVertical);
                    r.CreateBy = opr.FirstOrDefault();
                    r.CreateId = opr.LastOrDefault();
                }
                if (r.UpdateBy.IsNotNullOrWhiteSpace())
                {
                    var opr = r.UpdateBy.Split(CommonConstant.SeparatorVertical);
                    r.UpdateBy = opr.FirstOrDefault();
                    r.UpdateId = opr.LastOrDefault();
                }
                
            });*/

            return Result.Success(res.RoleList, res.TotalItems);
        }

        /// <summary>
        /// 根据EmpType和OrgId，获取角色列表信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<RoleSelectResVO>>> GetRoleListByOrgIdAsync([FromQuery] RoleListReqVO req)
        {
            Int32.TryParse(identitySvc.GetOrganizationId(), out var shopId);
            req.IsDeleted = 0;
            req.OrganizationId = shopId;
            req.Type = RoleType.None;

            var res = await roleSvc.GetRoleListByOrgIdAsync(req);

            return Result.Success(res, CommonConstant.QuerySuccess);

        }

        /// <summary>
        /// 获取角色Id，Name，IsDeleted信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<RoleSelectResVO>>> GetAllRoleSelectList()
        {
            var res = await roleSvc.GetAllRoleSelectList();
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 根据roleId获取其所有的权限列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ElementRoleAuthorityCheckedTree>> GetRoleAuthorityListByRoleId([FromQuery] RoleAuthorityListReqByRoleIdVO req)
        {
            var resVo = await roleSvc.GetRoleAuthorityListByRoleId(req);
            var res = new ElementRoleAuthorityCheckedTree
            {
                HalfChecked = resVo.FindAll(f => f.HalfCheck).Select(s => s.AuthorityId).ToList(),
                Checked = resVo.FindAll(f => !f.HalfCheck).Select(s => s.AuthorityId).ToList()
            };
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        #endregion 接口实现

        #region 私有方法

        private async Task<string> DuplicationValidate(RoleListInternalReqDTO reqVo)
        {
            var errMsg = "";

            var res = await roleSvc.GetRoleListAnyCondition(reqVo);
            var valid = reqVo.Id > 0 ? res.FindAll(f => f.Id != reqVo.Id) : res;

            if (!valid.Any()) return "";

            if (valid.FindAll(v => string.Equals(reqVo.Name, v.Name)&&reqVo.Features==v.Features&&reqVo.Type==(int)v.Type).Any())
            {
                errMsg += "，角色名称";
            }

            var orgName = "";
            var orgList = await companySvc.GetAllOrganizationExceptShopSelectListAsync();
            var orgDic = orgList?.ToDictionary(a => a.Id, a => a.Name);
            var firstOrg = res?.FirstOrDefault();
            if (firstOrg != null)
            {
                if (firstOrg.Type == RoleType.Shop)
                {
                    firstOrg.OrganizationId = CommonConstant.Zero.ToString();
                }
                var combine = $"{firstOrg.OrganizationId}|{firstOrg.Type}";
                orgName = orgDic != null && orgDic.ContainsKey(combine)
                    ? orgDic[combine] : " - ";
            }
            return errMsg.IsNullOrWhiteSpace() ? errMsg : $"{orgName} 里，{errMsg.Substring(1)} 已存在！";
        }

        #endregion 私有方法
    }
}