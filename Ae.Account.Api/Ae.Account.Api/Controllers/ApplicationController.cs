using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Account.Api.Common.Constant;
using Ae.Account.Api.Common.Extension;
using Ae.Account.Api.Common.Http;
using Ae.Account.Api.Filters;
using Ae.Account.Api.Core.Interfaces;
using Ae.Account.Api.Core.Model;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;

namespace Ae.Account.Api.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(ApplicationController))]
    public class ApplicationController : Controller
    {
        #region 变量和构造器

        private readonly ApolloErpLogger<ApplicationController> logger;
        private readonly IMapper mapper;
        private readonly IIdentityService identitySvc;

        private readonly IApplicationService appSvc;

        public ApplicationController(ApolloErpLogger<ApplicationController> logger, IMapper mapper,
            IIdentityService identitySvc,
            IApplicationService appSvc)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.identitySvc = identitySvc;

            this.appSvc = appSvc;
        }

        #endregion 变量和构造器

        #region 接口实现

        /// <summary>
        /// 创建或是编辑系统
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> CreateOrUpdateApplication([FromBody] BaseEntityPostRequest<ApplicationVO> req)
        {
            bool res;
            var data = req.Data;
            var reqVo = mapper.Map<AppReqVO>(data);

            if (!reqVo.Route.IsUrl())
            {
                return Result.Failed<bool>(ResultCode.Failed, "请输入正确格式的系统域名地址");
            }

            var errMsg = await DuplicationValidate(reqVo);
            if (!string.IsNullOrWhiteSpace(errMsg))
            {
                return Result.Failed<bool>(ResultCode.Failed, errMsg);
            }

            var opr = string.Join("|", identitySvc.GetUserName(), identitySvc.GetUserId());
            if (data.Id > 0)
            {
                data.UpdateBy = opr; //"UdByApplicationCntr";
                data.UpdateTime = DateTime.Now;
                res = await appSvc.UpdateApplicationById(data);
                return Result.Success(res, CommonConstant.OperateSuccess);
            }

            data.CreateBy = opr; //"CrtByApplicationCntr";
            res = await appSvc.CreateApplication(data);
            return Result.Success(res, CommonConstant.OperateSuccess);
        }

        /// <summary>
        /// 根据Id逻辑删除系统
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteApplicationById([FromBody] BaseEntityPostRequest<ApplicationVO> req)
        {
            var opr = string.Join("|", identitySvc.GetUserName(), identitySvc.GetUserId());
            req.Data.UpdateBy = opr; //"DelByApplicationCntr";
            req.Data.UpdateTime = DateTime.Now;

            var res = await appSvc.DeleteApplicationById(req.Data);
            return Result.Success(res, CommonConstant.OperateSuccess);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<AppResVO>> GetPagedApplicationList([FromQuery] AppListReqVO req)
        {
            var res = await appSvc.GetPagedApplicationList(req);
            res.AppList.ForEach(f =>
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
            return Result.Success(res.AppList, res.TotalItems);
        }

        /// <summary>
        /// 根据Id获取系统详情
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<AppResVO>> GetApplicationById([FromQuery] long id)
        {
            var res = await appSvc.GetApplicationById(id);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 根据Name获取系统详情
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<AppResVO>> GetApplicationByName([FromQuery] string name)
        {
            var res = await appSvc.GetApplicationByName(name);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 根据Initialism获取系统详情
        /// </summary>
        /// <param name="initialism"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<AppResVO>> GetApplicationByInitialism([FromQuery] string initialism)
        {
            var res = await appSvc.GetApplicationByInitialism(initialism);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 根据Route获取系统详情
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<AppResVO>> GetApplicationByRoute([FromQuery] string route)
        {
            var res = await appSvc.GetApplicationByRoute(route);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 获取系统Id，Name，Initialism信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<AppSelectResVO>>> GetApplicationSelectList()
        {
            var res = await appSvc.GetApplicationList();
            return Result.Success(res, CommonConstant.QuerySuccess);
        }


        #endregion 接口实现

        #region 私有方法

        private async Task<string> DuplicationValidate(AppReqVO reqVo)
        {
            var errMsg = "";

            var res = await appSvc.GetApplicationListAnyCondition(reqVo);
            var valid = reqVo.Id > 0 ? res.FindAll(f => f.Id != reqVo.Id) : res;

            if (!EnumerableExtensions.Any(valid)) return errMsg;

            if (EnumerableExtensions.Any(valid.FindAll(v => string.Equals(reqVo.Name, v.Name))))
            {
                errMsg += "，系统名称";
            }
            if (EnumerableExtensions.Any(valid.FindAll(v => string.Equals(reqVo.Initialism, v.Initialism))))
            {
                errMsg += "，系统简称";
            }
            if (EnumerableExtensions.Any(valid.FindAll(v => string.Equals(reqVo.Route, v.Route))))
            {
                errMsg += "，系统域名";
            }

            return errMsg.IsNullOrWhiteSpace() ? errMsg : $"{errMsg.Substring(1)} 已存在！";
        }

        /// <summary>
        /// 新增系统
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        private async Task<ApiResult<bool>> CreateApplication([FromBody] BaseEntityPostRequest<ApplicationVO> req)
        {
            var data = req.Data;
            var reqVo = mapper.Map<AppReqVO>(data);

            if (!reqVo.Route.IsUrl())
            {
                return Result.Failed<bool>(ResultCode.Failed, "请输入正确格式的系统域名地址");
            }

            var errMsg = await DuplicationValidate(reqVo);
            if (!string.IsNullOrWhiteSpace(errMsg))
            {
                return Result.Failed<bool>(ResultCode.Failed, errMsg);
            }

            var opr = string.Join("|", identitySvc.GetUserName(), identitySvc.GetUserId());
            data.CreateBy = opr;//"CrtByApplicationCntr";
            var res = await appSvc.CreateApplication(data);
            return Result.Success(res, CommonConstant.OperateSuccess);
        }

        /// <summary>
        /// 根据Id更新系统信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        private async Task<ApiResult<bool>> UpdateApplicationById([FromBody] BaseEntityPostRequest<ApplicationVO> req)
        {
            var data = req.Data;
            var reqVo = mapper.Map<AppReqVO>(data);

            if (!reqVo.Route.IsUrl())
            {
                return Result.Failed<bool>(ResultCode.Failed, "请输入正确格式的系统域名地址");
            }

            var errMsg = await DuplicationValidate(reqVo);
            if (!string.IsNullOrWhiteSpace(errMsg))
            {
                return Result.Failed<bool>(ResultCode.Failed, errMsg);
            }

            var opr = string.Join("|", identitySvc.GetUserName(), identitySvc.GetUserId());
            data.UpdateBy = opr; // "UdByApplicationCntr";
            data.UpdateTime = DateTime.Now;
            //var valid=await appSvc.GetApplication()
            var res = await appSvc.UpdateApplicationById(data);
            return Result.Success(res, CommonConstant.OperateSuccess);
        }



        #endregion 私有方法

    }
}