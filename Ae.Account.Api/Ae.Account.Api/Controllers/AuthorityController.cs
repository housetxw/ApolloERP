using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Common.Constant;
using Ae.Account.Api.Common.Extension;
using Ae.Account.Api.Common.Http;
using Ae.Account.Api.Core.Interfaces;
using Ae.Account.Api.Core.Model;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;
using Ae.Account.Api.Filters;

namespace Ae.Account.Api.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(AuthorityController))]
    public class AuthorityController : Controller
    {
        #region 变量和构造器

        private readonly ApolloErpLogger<AuthorityController> logger;
        private readonly IMapper mapper;
        private readonly IIdentityService identitySvc;

        private readonly IAuthorityService authoritySvc;

        public AuthorityController(ApolloErpLogger<AuthorityController> logger, IMapper mapper,
            IIdentityService identitySvc,
            IAuthorityService authoritySvc)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.identitySvc = identitySvc;

            this.authoritySvc = authoritySvc;
        }

        #endregion 变量和构造器

        #region 接口实现

        /// <summary>
        /// 创建或是编辑权限
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> CreateOrUpdateAuthority([FromBody] BaseEntityPostRequest<AuthorityVO> req)
        {
            bool res;
            var data = req.Data;

            var reqVo = mapper.Map<AuthorityListInternalReqDTO>(data);
            var errMsg = await DuplicationValidate(reqVo);
            if (errMsg.IsNotNullOrWhiteSpace())
            {
                return Result.Failed<bool>(ResultCode.Failed, errMsg);
            }

            var opr = string.Join("|", identitySvc.GetUserName(), identitySvc.GetUserId());
            if (data.Id > 0)
            {
                data.UpdateBy = opr;//"UdByAuthorityCntr";
                data.UpdateTime = DateTime.Now;
                res = await authoritySvc.UpdateAuthorityById(data);
                return Result.Success(res, CommonConstant.OperateSuccess);
            }

            if (data.ParentId > 0)
            {
                var list = await authoritySvc.GetParentAuthorityListByAppIdAndAuthIdAsync(new AuthorityListReqDTO
                {
                    ParentId = data.ParentId,
                    IsDeleted = 0
                });
                data.ParentIdList = list?.Select(s => s.Id).ToList();
            }

            data.CreateBy = opr;//"CrtByAuthorityCntr";
            res = await authoritySvc.CreateAuthority(data);
            return Result.Success(res, CommonConstant.OperateSuccess);
        }

        /// <summary>
        /// 根据Id逻辑删除权限
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteAuthorityById([FromBody] BaseEntityPostRequest<AuthorityVO> req)
        {
            var data = req.Data;
            var opr = string.Join("|", identitySvc.GetUserName(), identitySvc.GetUserId());
            data.UpdateBy = opr;// "DelByAuthorityCntr";
            data.UpdateTime = DateTime.Now;

            if (data.ParentId > 0)
            {
                var list = await authoritySvc.GetParentAuthorityListByAppIdAndAuthIdAsync(new AuthorityListReqDTO
                {
                    ParentId = data.ParentId,
                    IsDeleted = 0
                });
                data.ParentIdList = list?.Select(s => s.Id).ToList();
            }

            var res = await authoritySvc.DeleteAuthorityById(data);
            return Result.Success(res, CommonConstant.OperateSuccess);
        }

        /// <summary>
        /// 获取权限分页数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<AuthorityPageResVO>> GetPagedAuthorityList([FromQuery] AuthorityListReqVO req)
        {
            var res = await authoritySvc.GetPagedAuthorityList(req);
            res.AuthorityList.ForEach(f =>
            {
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
            return Result.Success(res.AuthorityList, res.TotalItems);
        }

        /// <summary>
        /// 获取权限Id，Name，IsDeleted信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<AuthoritySelectResVO>>> GetAllAuthoritySelectList()
        {
            var res = await authoritySvc.GetAllAuthoritySelectList();
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 根据ApplicationId获取系统下的所有权限
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<AuthoritySelectResVO>>> GetAuthorityListByApplicationIdAsync([FromQuery] AuthorityListReqVO req)
        {
            var res = await authoritySvc.GetAuthorityListByApplicationIdAsync(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 编辑权限时，根据ApplicationId和排除当前AuthorityId，获取系统下的所有权限
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<AuthoritySelectResVO>>> GetAuthorityListByApplicationIdExceptIdAsync([FromQuery] AuthorityListReqVO req)
        {
            var res = await authoritySvc.GetAuthorityListByApplicationIdExceptIdAsync(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 获取有效权限的权限列树
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ElementAuthorityTree>>> GetAuthorityTreeWithApplicationInfo()
        {
            var resVo = await authoritySvc.GetAuthorityTreeWithApplicationInfo();
            var res = mapper.Map<List<ElementAuthorityTree>>(resVo);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }


        #endregion 接口实现

        #region 私有方法

        private async Task<string> DuplicationValidate(AuthorityListInternalReqDTO reqVo)
        {
            var errMsg = "";

            var res = await authoritySvc.GetAuthorityListAnyCondition(reqVo);
            var valid = reqVo.Id > 0 ? res.FindAll(f => f.Id != reqVo.Id) : res;

            if (!valid.Any()) return "";

            if (valid.FindAll(v => string.Equals(reqVo.Name, v.Name)&& string.Equals(reqVo.Route, v.Route)).Any())
            {
                errMsg += "权限名称,权限路由";
            }
            //if (valid.FindAll(v => string.Equals(reqVo.Route, v.Route)).Any())
            //{
            //    errMsg += "，权限路由";
            //}

            return errMsg.IsNullOrWhiteSpace() ?
                errMsg :
                $"系统 {res.FirstOrDefault()?.ApplicationName} 里，{errMsg.Substring(1)} 已存在！";
        }

        #endregion 私有方法
    }
}