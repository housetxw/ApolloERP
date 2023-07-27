using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Core.Enums;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Request.ShopServer;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Core.Response.ShopServer;
using Ae.Shop.Service.Filters;

namespace Ae.Shop.Service.Controllers
{
    [Route("[controller]/[action]")]
    public class JobController : Controller
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<JobController> logger;
        private readonly string className;

        private readonly IEmployeeService empSvc;
        private readonly IJobService jobSvc;
        private IConfiguration _configuration;

        public JobController(ApolloErpLogger<JobController> logger,
            IEmployeeService empSvc,
            IJobService jobSvc, IConfiguration configuration)
        {
            this.logger = logger;
            className = this.GetType().Name;

            this.empSvc = empSvc;
            this.jobSvc = jobSvc;
            _configuration = configuration;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        /// <summary>
        /// 获取门店岗位列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<JobListResDTO>>> GetShopJobList()
        {
            var res = await jobSvc.GetJobListByType(new JobListReqByTypeDTO { Type = JobType.Shop });
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 获取工种信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<WorkKindInfoResDTO>> GetWorkKindInfo(GetWorkKindInfoRequest request)
        {
            var jobId = _configuration["ShopManageBiz:TechnicianJobId"];
            if (!long.TryParse(jobId, out var technicanJobId))
            {
                technicanJobId = CommonConstant.Three;
            }
            List<EmployeeLevelListResDTO> levelList = null;

            long techJobId = request.JobId > 0 ? request.JobId : technicanJobId;
            if (techJobId == technicanJobId)
            {
                 levelList = empSvc.GetEmployeeLevelList();
            }
            else
            {
                levelList = new List<EmployeeLevelListResDTO>();
            }

            var workKindList = await jobSvc.GetWorkKindList(request);
            var res = new WorkKindInfoResDTO
            {
                WorkKindLevel = levelList,
                WorkKindList = workKindList
            };
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 根据岗位类型，获取岗位综合信息(Web Page)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<JobSelDTO>>> GetJobInfoByTypeForWeb(JobListReqByTypeDTO req)
        {
            var res = await jobSvc.GetJobInfoByTypeForWeb(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 根据所属单位id和岗位类型，获取岗位综合信息(Web Page)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<JobSelDTO>>> GetJobListByOrgIdForWeb(JobListReqByOrgIdDTO req)
        {
            var res = await jobSvc.GetJobListByOrgIdForWeb(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }


    }
}