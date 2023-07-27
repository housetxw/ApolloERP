using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;
using Ae.Account.Api.Common.Constant;
using Ae.Account.Api.Common.Exceptions;
using Ae.Account.Api.Common.Extension;
using Ae.Account.Api.Core.Interfaces;
using Ae.Account.Api.Core.Model;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;
using Ae.Account.Api.Filters;

namespace Ae.Account.Api.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(AccountController))]
    public class AccountController : Controller
    {
        private readonly ApolloErpLogger<AccountController> logger;
        private readonly IMapper mapper;
        private readonly IIdentityService identitySvc;

        private readonly IAccountService acctSvc;
        private readonly IEmployeeService empSvc;

        private readonly string oprUser;

        public AccountController(ApolloErpLogger<AccountController> logger, IMapper mapper,
            IIdentityService identitySvc,
            IAccountService acctSvc,
            IEmployeeService empSvc)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.identitySvc = identitySvc;

            this.acctSvc = acctSvc;
            this.empSvc = empSvc;

            oprUser = string.Join(CommonConstant.SeparatorVertical, identitySvc.GetUserName(), identitySvc.GetUserId());
        }


        /// <summary>
        /// 获取账号分页列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<AccountPageResVO>> GetAccountPage([FromQuery] EmployeePageReqVO req)
        {
            List<string> ids;
            Dictionary<string, AccountKeyInfoListDTO> acctDic = new Dictionary<string, AccountKeyInfoListDTO>();

            //标识搜索条件中是否有IsDeleted字段；
            //若有，先查Account信息，再根据AccountId查询员工列表信息；
            //若无，直接根据搜索条件查询员工列表信息，然后在根据EmpList中的AccountId获取账号信息；
            var hasIsDeleted = req.IsDeleted >= CommonConstant.Zero;
            if (hasIsDeleted)
            {
                var acctList = await acctSvc.GetAccountKeyInfoListById(new AccountListReqDTO
                {
                    IsDeleted = req.IsDeleted
                });

                if (acctList != null)
                {
                    ids = acctList.Select(s => s.Id).Distinct().ToList();
                    acctDic = acctList.Distinct().ToDictionary(a => a.Id);
                    req.AccountId.AddRange(ids);
                }
            }

            var empList = await empSvc.GetEmployeePage(req);
            var resPage = mapper.Map<ApiPagedResultData<AccountPageResVO>>(empList);

            if (resPage?.Items?.Count > 0)
            {
                if (!hasIsDeleted)
                {
                    ids = resPage.Items.Select(s => s.AccountId).Distinct().ToList();
                    var acctList = await acctSvc.GetAccountKeyInfoListById(new AccountListReqDTO
                    {
                        Id = ids
                        //,IsDeleted = req.IsDeleted
                    });

                    if (acctList != null)
                    {
                        acctDic = acctList.Distinct().ToDictionary(a => a.Id);
                    }
                }

                resPage.Items.ToList().ForEach(f =>
                {
                    var hasKey = acctDic != null && acctDic.ContainsKey(f.AccountId);

                    //f.AccountName = hasKey ? acctDic[f.AccountId].Name.HidePhoneSensitiveInfo() : " N/A ";
                    f.AccountName = hasKey ? acctDic[f.AccountId].Name : " N/A ";
                    f.OrganizationId = string.Join(CommonConstant.SeparatorVertical, f.OrganizationId, f.Type);

                    f.ErrorCount = hasKey ? acctDic[f.AccountId].ErrorCount : CommonConstant.Zero;
                    f.State = hasKey ? Convert.ToInt32(acctDic[f.AccountId].State) : CommonConstant.Zero;

                    if (hasKey)
                    {
                        var acctEty = acctDic[f.AccountId];

                        f.IsDeleted = acctEty.IsDeleted;

                        var crBy = acctEty.CreateBy;
                        if (crBy.IsNotNullOrWhiteSpace())
                        {
                            var opr = crBy.Split(CommonConstant.SeparatorVertical);
                            f.CreateBy = opr.FirstOrDefault();
                            f.CreateId = opr.LastOrDefault();
                            f.CreateTime = acctEty.CreateTime;
                        }

                        var upBy = acctEty.UpdateBy;
                        if (upBy.IsNotNullOrWhiteSpace())
                        {
                            var opr = upBy.Split(CommonConstant.SeparatorVertical);
                            f.UpdateBy = opr.FirstOrDefault();
                            f.UpdateId = opr.LastOrDefault();
                            f.UpdateTime = acctEty.UpdateTime;
                        }
                    }
                });
            }
            return Result.Success(resPage?.Items?.OrderByDescending(d => d.UpdateTime).ThenByDescending(d => d.CreateTime).ToList(), resPage?.TotalItems ?? 0);
        }

        /// <summary>
        /// 根据AccountId，解锁账号
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UnlockAccountById([FromBody] ApiRequest<AccountUnlockReqByIdVO> req)
        {
            if (null == req?.Data)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<bool>(ResultCode.ArgumentError, CommonConstant.ArgumentError);
            }

            try
            {
                req.Data.UpdateBy = oprUser;

                var res = await acctSvc.UnlockAccountById(req.Data);
                return Result.Success(res, CommonConstant.OperateSuccess);
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(), e);
                throw new CustomException(CommonConstant.InternalError);
            }
        }

        /// <summary>
        /// 根据AccountId，批量锁定账号
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> LockAccountById([FromBody] ApiRequest<AccountLockReqByIdVO> req)
        {
            if (null == req?.Data)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<bool>(ResultCode.ArgumentError, CommonConstant.ArgumentError);
            }

            try
            {
                req.Data.UpdateBy = oprUser;

                var res = await acctSvc.LockAccountById(req.Data);
                return Result.Success(res, CommonConstant.OperateSuccess);
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(), e);
                throw new CustomException(CommonConstant.InternalError);
            }
        }

        /// <summary>
        /// 根据AccountId，重置密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<AccountResetPasswordResByIdVO>> ResetAccountPasswordById([FromBody] ApiRequest<AccountResetPasswordReqByIdVO> req)
        {
            if (null == req?.Data)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<AccountResetPasswordResByIdVO>(ResultCode.ArgumentError, CommonConstant.ArgumentError);
            }

            try
            {
                req.Data.UpdateBy = oprUser;

                var res = await acctSvc.ResetAccountPasswordById(req.Data);
                if (!res.flag)
                {
                    return Result.Failed<AccountResetPasswordResByIdVO>(res.Message);
                }
                return Result.Success(res, CommonConstant.OperateSuccess);
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(), e);
                throw new CustomException(CommonConstant.InternalError);
            }
        }

        /// <summary>
        /// 根据登录账号，修改密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdatePassword([FromBody] ApiRequest<AccountUpdatePasswordEntityVO> req)
        {
            if (null == req?.Data)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<bool>(ResultCode.ArgumentError, CommonConstant.ArgumentError);
            }

            if (req.Data.Password.Equals(req.Data.NewPassword))
            {
                return Result.Failed<bool>("新旧密码不能相同");
            }

            try
            {
                req.Data.UpdateBy = oprUser;

                var res = await acctSvc.UpdatePasswordAsync(req.Data);
                return res.Data
                    ? Result.Success(true, CommonConstant.OperateSuccess)
                    : Result.Failed<bool>(res.Message);
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(), e);
                throw new CustomException(CommonConstant.InternalError);
            }
        }

        /// <summary>
        /// 根据AccountId，批量删除账号
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteBatchAccountById([FromBody] ApiRequest<AccountBatchDeleteReqByIdVO> req)
        {
            if (null == req?.Data)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<bool>(ResultCode.ArgumentError, CommonConstant.ArgumentError);
            }

            try
            {
                req.Data.UpdateBy = oprUser;

                var res = await acctSvc.DeleteBatchAccountById(req.Data);
                return Result.Success(res, CommonConstant.OperateSuccess);
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(), e);
                throw new CustomException(CommonConstant.InternalError);
            }
        }


    }
}