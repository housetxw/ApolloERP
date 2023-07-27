using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Common.Extension;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Filters;
using Ae.Shop.Service.Common.Extension;
using Microsoft.AspNetCore.Authorization;
using ApolloErp.Login.Auth;
using Ae.Shop.Service.Client.Model;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Client.Response;

namespace Ae.Shop.Service.Controllers
{
    [Route("[controller]/[action]")]
    public class EmployeeController : Controller
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<EmployeeController> logger;
        private readonly string className;

        private readonly IEmployeeService empSvc;
        private readonly IJobService jobSvc;
        private readonly IIdentityService identityService;

        public EmployeeController(ApolloErpLogger<EmployeeController> logger,
            IEmployeeService empSvc,
            IJobService jobSvc,
            IIdentityService identityService
            )
        {
            this.logger = logger;
            className = this.GetType().Name;

            this.empSvc = empSvc;
            this.jobSvc = jobSvc;
            this.identityService = identityService;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        #region ！！！登录接口相关，请勿轻易对其修改！！！

        /// <summary>
        /// 登录接口：根据AccountId，获取其所有的员工及其所属单位列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<EmployeeResDTO>>> GetEmpAndOrgListForLoginByAccountIdAsync([FromQuery] EmployeeListReqDTO req)
        {
            if (req.AccountId.IsNullOrWhiteSpace() || !req.AccountId.CheckIsGuidFormat())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<List<EmployeeResDTO>>(ResultCode.ArgumentError, "请输入正确的AccountId");
            }

            //logger.Info($"SVC: {className}.GetEmpAndOrgListForLoginByAccountIdAsync 请求参数：{JsonConvert.SerializeObject(req)}");

            var res = await empSvc.GetEmpAndOrgListForLoginByAccountIdAsync(req);

            //logger.Info($"SVC: {className}.GetEmpAndOrgListForLoginByAccountIdAsync 返回值：{JsonConvert.SerializeObject(res)}");

            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 登录接口：根据AccountId，获取其所有的员工及其所属单位分页列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<EmployeePageDTO>> GetEmpAndOrgPageForLoginByAccountIdAsync([FromQuery] EmployeePageForLoginListReqDTO req)
        {
            if (req.AccountId.IsNullOrWhiteSpace() || !req.AccountId.CheckIsGuidFormat())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<EmployeePageDTO>(ResultCode.ArgumentError, "请输入正确的AccountId", null, 0);
            }

            //logger.Info($"SVC: {className}.GetEmpAndOrgPageForLoginByAccountIdAsync 请求参数：{JsonConvert.SerializeObject(req)}");

            var res = await empSvc.GetEmpAndOrgPageForLoginByAccountIdAsync(req);

            //logger.Info($"SVC: {className}.GetEmpAndOrgPageForLoginByAccountIdAsync 返回值：{JsonConvert.SerializeObject(res)}");

            return Result.Success(res?.Items, res?.TotalItems ?? 0);
        }

        /// <summary>
        /// 登录接口：根据EmployeeId，获取其账号下所有的员工及其所属单位分页列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<EmployeePageDTO>> GetEmpAndOrgPageForLoginByEmpIdAsync([FromQuery] EmployeePageForLoginListByTokenReqDTO req)
        {
            if (req.EmployeeId.IsNullOrWhiteSpace() || !req.EmployeeId.CheckIsGuidFormat())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<EmployeePageDTO>(ResultCode.ArgumentError, "请输入正确的EmployeeId", null, 0);
            }

            //logger.Info($"SVC: {className}.GetEmpAndOrgPageForLoginByEmpIdAsync 请求参数：{JsonConvert.SerializeObject(req)}");

            var res = await empSvc.GetEmpAndOrgPageForLoginByEmpIdAsync(req);

            //logger.Info($"SVC: {className}.GetEmpAndOrgPageForLoginByEmpIdAsync 返回值：{JsonConvert.SerializeObject(res)}");

            return Result.Success(res?.Items, res?.TotalItems ?? 0);
        }

        /// <summary>
        /// 登录接口：根据EmployeeId，获取其账号下所有的员工及其所属单位(排除当前EmpId的OrgId)分页列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<EmployeePageDTO>> GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync([FromQuery] EmployeePageForLoginListByTokenReqDTO req)
        {
            if (req.EmployeeId.IsNullOrWhiteSpace() || !req.EmployeeId.CheckIsGuidFormat())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<EmployeePageDTO>(ResultCode.ArgumentError, "请输入正确的EmployeeId", null, 0);
            }

            //logger.Info($"SVC: {className}.GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync 请求参数：{JsonConvert.SerializeObject(req)}");

            var res = await empSvc.GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync(req);

            //logger.Info($"SVC: {className}.GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync 返回值：{JsonConvert.SerializeObject(res)}");

            return Result.Success(res?.Items, res?.TotalItems ?? 0);
        }

        /// <summary>
        /// 登录接口：根据EmployeeId，获取其管辖范围(排除当前EmpId的OrgId)分页列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<EmployeePageDTO>> GetOrgRangePageForLoginByEmpIdExcCurOrgId([FromQuery] EmployeePageForLoginListByTokenReqDTO req)
        {
            if (req.EmployeeId.IsNullOrWhiteSpace() || !req.EmployeeId.CheckIsGuidFormat())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<EmployeePageDTO>(ResultCode.ArgumentError, "请输入正确的EmployeeId", null, 0);
            }

            //logger.Info($"SVC: {className}.GetOrgRangePageForLoginByEmpIdExcCurOrgId 请求参数：{JsonConvert.SerializeObject(req)}");

            var res = await empSvc.GetOrgRangePageForLoginByEmpIdExcCurOrgId(req);

            //logger.Info($"SVC: {className}.GetOrgRangePageForLoginByEmpIdExcCurOrgId 返回值：{JsonConvert.SerializeObject(res)}");

            return Result.Success(res?.Items, res?.TotalItems ?? 0);
        }

        /// <summary>
        /// 登录接口：根据EmployeeId，OrganizationId和EmployeeType，获取管辖范围角色列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<long>>> GetOrgRangeRoleIdList([FromQuery] OrgRangeRoleListForLoginReqDTO req)
        {
            if (req.EmployeeId.IsNullOrWhiteSpace() || !req.EmployeeId.CheckIsGuidFormat())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<List<long>>(ResultCode.ArgumentError, "请输入正确的Id");
            }

            //logger.Info($"SVC: {className}.GetOrgRangeRoleIdList 请求参数：{JsonConvert.SerializeObject(req)}");

            var res = await empSvc.GetOrgRangeRoleIdList(req);

            //logger.Info($"SVC: {className}.GetOrgRangeRoleIdList 返回值：{JsonConvert.SerializeObject(res)}");

            return Result.Success(res, CommonConstant.QuerySuccess);
        }


        #endregion ！！！登录接口相关，请勿轻易对其修改！！！

        /// <summary>
        /// 获取全量员工的关键性非敏感信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<EmployeeResDTO>>> GetAllEmployeeListAsync()
        {
            var result = await empSvc.GetAllEmployeeListAsync();
            return Result.Success(result, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 根据所属单位id和所属单位type，获取员工的关键性非敏感信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<EmployeeResDTO>>> GetEmployeeListByOrgIdAndTypeAsync([FromQuery] EmployeeListReqDTO req)
        {
            if (req?.OrganizationId <= 0)
            {
                return Result.Failed<List<EmployeeResDTO>>(ResultCode.ArgumentError, "请输入正确的OrganizationId");
            }

            var result = await empSvc.GetEmployeeListByOrgIdAndTypeAsync(req);
            return Result.Success(result, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 获取员工分页数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<EmployeePageDTO>> GetEmployeePage([FromBody] EmployeePageReqDTO req)
        {
            var res = await empSvc.GetEmployeePage(req);
            return Result.Success(res?.Items, res?.TotalItems ?? 0);
        }

        /// <summary>
        /// 获取员工分页数据(WebPage)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<EmployeePageDTO>> GetEmployeePageForWeb([FromQuery] EmployeePageReqDTO req)
        {
            var res = await empSvc.GetEmployeePage(req);
            return Result.Success(res?.Items, res?.TotalItems ?? 0);
        }

        /// <summary>
        /// 获取员工基本信息（含角色）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<EmployeeInfoDTO>> GetEmployeeInfo([FromQuery] EmployeeInfoReqDTO req)
        {
            var res = await empSvc.GetEmployeeInfo(req);
            if (res != null) res.IsTechnican = jobSvc.CheckTechnican(res.JobId);
            return Result.Success(res);
        }

        /// <summary>
        /// 获取员工基本信息（简单）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<EmployeeSimpleInfoResponse>> GetEmployeeSimpleInfo([FromQuery] EmployeeInfoReqDTO req)
        {
            var res = await empSvc.GetEmployeeSimpleInfo(req);
            if (res != null) res.IsTechnican = jobSvc.CheckTechnican(res.JobId);
            return Result.Success(res);
        }

        /// <summary>
        /// 根据员工类型和OrgId，获取门店角色列表信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<RoleListResVO>>> GetShopRoleList([FromQuery] RoleListReqVO req)
        {
            req.Type = RoleType.Shop;
            var res = await empSvc.GetRoleListByOrgIdAndType(req);
            if (res.Code == ResultCode.Success)
            {
                return Result.Success(res.Data, CommonConstant.QuerySuccess);
            }
            return Result.Failed<List<RoleListResVO>>(res.Code, res.Message);
        }

        /// <summary>
        /// 根据EmpType和OrgId，获取角色列表信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<RoleListResVO>>> GetRoleList([FromQuery] RoleListReqVO req)
        {
            var res = await empSvc.GetRoleListByOrgIdAndType(req);

            return Result.Success(res.Data, CommonConstant.QuerySuccess);

        }

        /// <summary>
        /// 根据OrgId，获取管辖范围的角色列表信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<OrgRangeRolesVO>>> GetRangeRoleListByOrgId([FromQuery] RoleListReqVO req)
        {
            var res = await empSvc.GetRoleListByOrgId(req.OrganizationId);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 根据登录账号，判断账号是否已经存在
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<CheckAccountExistDTO>> CheckAccountIsExistByName(AccountEntityReqByNameDTO req)
            => await empSvc.CheckAccountIsExistByName(req);

        /// <summary>
        /// 创建或是编辑员工信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> CreateOrUpdateEmployee([FromBody] EmployeeEntityReqDTO req)
        {
            if (null == req || !req.Mobile.IsTelephone())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed(ResultCode.ArgumentError, "手机号码格式不正确");
            }

            if (req.CreateBy.IsNullOrWhiteSpace()) req.CreateBy = "System";
            if (req.UpdateBy.IsNullOrWhiteSpace()) req.UpdateBy = "System";
            if (req.UserId.IsNullOrWhiteSpace()) req.UserId = CommonConstant.DefaultCustomGuidStr;

            return await empSvc.CreateOrUpdateEmployee(req);
        }

        /// <summary>
        /// 创建门店初始化店长账号员工角色
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<bool>> CreateEmployeeInit([FromBody] ApiRequest<EmployeeEntityReqDTO> req)
        {
            if (null == req?.Data || !req.Data.Mobile.IsTelephone())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<bool>(ResultCode.ArgumentError, "手机号码格式不正确");
            }

            req.Data.CreateBy = identityService.GetUserName();
            req.Data.UpdateBy = identityService.GetUserName();
            req.Data.UserId = identityService.GetUserId();
            if (req.Data.CreateBy.IsNullOrWhiteSpace()) req.Data.CreateBy = "System";
            if (req.Data.UpdateBy.IsNullOrWhiteSpace()) req.Data.UpdateBy = "System";
            if (req.Data.UserId.IsNullOrWhiteSpace()) req.Data.UserId = CommonConstant.DefaultCustomGuidStr;

            var res = await empSvc.CreateEmployee(req.Data, true, true);

            return res.Code == ResultCode.Success
                ? Result.Success(true, CommonConstant.OperateSuccess)
                : Result.Failed<bool>(res.Message);
        }


        /// <summary>
        /// 创建或是编辑员工信息(Web Page)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<bool>> CreateOrUpdateEmployeeForWeb([FromBody] ApiRequest<EmployeeEntityReqDTO> req)
        {
            if (null == req?.Data || !req.Data.Mobile.IsTelephone())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<bool>(ResultCode.ArgumentError, "手机号码格式不正确");
            }

            req.Data.CreateBy = identityService.GetUserName();
            req.Data.UpdateBy = identityService.GetUserName();
            req.Data.UserId = identityService.GetUserId();
            if (req.Data.CreateBy.IsNullOrWhiteSpace()) req.Data.CreateBy = "System";
            if (req.Data.UpdateBy.IsNullOrWhiteSpace()) req.Data.UpdateBy = "System";
            if (req.Data.UserId.IsNullOrWhiteSpace()) req.Data.UserId = CommonConstant.DefaultCustomGuidStr;

            var res = await empSvc.CreateOrUpdateEmployee(req.Data);

            return res.Code == ResultCode.Success
                ? Result.Success(true, CommonConstant.OperateSuccess)
                : Result.Failed<bool>(res.Message);
        }

        /// <summary>
        /// 根据EmpId删除员工
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteEmployeeById([FromBody] EmployeeDeleteReqByIdDTO req)
        {
            if (req.UpdateBy.IsNullOrWhiteSpace()) req.UpdateBy = $"System:{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";

            return await empSvc.DeleteEmployeeById(req)
                ? Result.Success(true, CommonConstant.OperateSuccess)
                : Result.Failed<bool>(CommonConstant.OperateFailed);
        }

        /// <summary>
        /// 根据EmpIds批量删除员工
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> BatchDeleteEmployeeById([FromBody] ApiRequest<EmployeeBatchDeleteReqByIdDTO> req)
        {
            if (req.Data.UpdateBy.IsNullOrWhiteSpace()) req.Data.UpdateBy = $"System:{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";

            return await empSvc.BatchDeleteEmployeeById(req.Data)
                ? Result.Success(true, CommonConstant.OperateSuccess)
                : Result.Failed<bool>(CommonConstant.OperateFailed);
        }

        /// <summary>
        /// 获取门店员工列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ShopEmployeeDTO>> GetShopEmployeePagedListAsync([FromQuery] BasePageShopRequest request)
        {
            var result = await empSvc.GetShopEmployeePagedListAsync(request);
            result?.Items?.ToList()?.ForEach(_ =>
            {
                if (_.Level > 0)
                {
                    if (!string.IsNullOrEmpty(_.WorkName))
                    {
                        _.Description = $"{_.JobName}-{_.WorkName}-{_.Level}级";
                    }
                    else
                    {
                        _.Description = $"{_.JobName}";
                    }

                }
                else
                {

                    _.Description = $"{_.JobName}";

                }
            });
            ApiPagedResult<ShopEmployeeDTO> response = new ApiPagedResult<ShopEmployeeDTO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            return response;
        }

        /// <summary>
        /// 根据EmpId集合，获取员工列表信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<EmployeeInfoDTO>>> GetEmployeeListByEmpId([FromBody] EmployeeListReqByEmpIdDTO req)
        {
            var res = new List<EmployeeInfoDTO>();

            if (req?.EmployeeId.Count <= 0)
            {
                return Result.Success(res, CommonConstant.QuerySuccess);
            }

            res = await empSvc.GetEmployeeListByEmpId(req.EmployeeId);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        [HttpPost]
        public async Task<ApiResult> AddOrEidtEmployeeGroup([FromBody] EmployeeGroupDTO req)
        {
            return await empSvc.AddOrEidtEmployeeGroup(req);
        }

        [HttpPost]
        public async Task<ApiResult<List<EmployeeGroupDTO>>> GetEmployeeGroupList([FromBody] GetEmployeeGroupListRequest request)
        {
            return await empSvc.GetEmployeeGroupList(request);
        }

        //[HttpPost]
        //public async Task<ApiResult> AddOrEidtTechnician([FromBody] EmployeeEntityReqDTO req)
        //{
        //    return await empSvc.AddOrEidtTechnician(req);
        //}

    }
}