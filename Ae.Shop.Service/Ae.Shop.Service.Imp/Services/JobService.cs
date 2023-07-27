using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Core.Enums;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Request.ShopServer;
using Ae.Shop.Service.Core.Response.ShopServer;
using Ae.Shop.Service.Dal.Model;
using Ae.Shop.Service.Dal.Repositorys;
using Ae.Shop.Service.Dal.Repositorys.Job;

namespace Ae.Shop.Service.Imp.Services
{
    public class JobService : IJobService
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly long technicanJobId;

        private readonly IJobRepository jobRepo;
        private readonly IEmployeeService empSvc;
        private readonly IJobWorkKindRepository _jobWorkKindRepository;
        public JobService(IConfiguration configuration, IMapper mapper,
            IJobRepository jobRepo,
            IEmployeeService empSvc, IJobWorkKindRepository jobWorkKindRepository)
        {
            this.configuration = configuration;
            this.mapper = mapper;

            this.jobRepo = jobRepo;
            this.empSvc = empSvc;
            _jobWorkKindRepository = jobWorkKindRepository;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<List<JobListResDTO>> GetJobListByType(JobListReqByTypeDTO req)
        {
            var resDo = await jobRepo.GetJobListByType(req);
            var res = mapper.Map<List<JobListResDTO>>(resDo);
            var jobIds = res?.Select(_ => _.Id)?.ToList();
            if (jobIds?.Count > 0)
            {
                var jobWorkRelation = _jobWorkKindRepository.GetList(" where is_deleted=0 and job_id in @Jobs", new { Jobs = jobIds })?.ToList();
                res?.ForEach(_ =>
                {
                    if (jobWorkRelation?.Where(item => item.JobId == _.Id)?.Count() > 0)
                    {
                        _.HasChild = true;
                    }
                });
               

            }
            return res;
        }

        public async Task<List<WorkKindListResDTO>> GetWorkKindList(GetWorkKindInfoRequest request)
        {
            var resDo = await jobRepo.GetWorkKindList();
            long jobId = request.JobId > 0 ? request.JobId : GetTechnicanJobId();
            var workKindRelations = _jobWorkKindRepository.GetList(" where is_deleted=0 and job_id in @Jobs", new { Jobs = new List<long>() { jobId } })?.ToList();
            List<WorkKindListResDTO> result = new List<WorkKindListResDTO>();
            resDo?.ForEach(_ =>
            {
                if (workKindRelations?.Where(item => item.WorkKindId == _.Id)?.Count() > 0)
                {
                    result.Add(mapper.Map<WorkKindListResDTO>(_));
                }
            });
            //var res = mapper.Map<List<WorkKindListResDTO>>(resDo);
            return result;
        }

        public async Task<List<JobSelDTO>> GetJobInfoByTypeForWeb(JobListReqByTypeDTO req)
        {
            var resDo = await jobRepo.GetJobListByType(req);

            return req.Type != JobType.Shop
                ? mapper.Map<List<JobSelDTO>>(resDo)
                : GetShopJobInfoForSel(resDo);
        }

        public async Task<List<JobSelDTO>> GetJobListByOrgIdForWeb(JobListReqByOrgIdDTO req)
        {
            var resDo = new List<JobDO>();

            if (req.Type == JobType.Shop)
            {
                resDo = await jobRepo.GetJobListByType(new JobListReqByTypeDTO { Type = req.Type });
                return GetShopJobInfoForSel(resDo);
            }

            resDo = await jobRepo.GetJobListByOrganizationId(req);
            return mapper.Map<List<JobSelDTO>>(resDo);
        }

        public bool CheckTechnican(long jobId) => jobId == GetTechnicanJobId();

        // ---------------------------------- 私有方法 --------------------------------------
        private long GetTechnicanJobId()
        {
            var jobId = configuration["ShopManageBiz:TechnicianJobId"];
            if (!long.TryParse(jobId, out var technicanJobId))
            {
                technicanJobId = CommonConstant.Three;
            }
            return technicanJobId;
        }

        private List<JobSelDTO> GetShopJobInfoForSel(List<JobDO> req)
        {
            var levelList = empSvc.GetEmployeeLevelList();

            var res = mapper.Map<List<JobSelDTO>>(req);
            //var techEntity = res.Find(f => f.Value == GetTechnicanJobId());

            //if (techEntity == null) return res;

            var wkDo = jobRepo.GetWorkKindList();
            Task.WaitAll(wkDo);


            var workKindRelations = _jobWorkKindRepository.GetList(" where is_deleted=0 and job_id in @Jobs", new { Jobs = res?.Select(_ => _.Value)?.ToList() });

            res.ForEach(_ =>
            {
                _.Children = new List<JobSelDTO>();

                var workKinds = workKindRelations?.Where(item => item.JobId == _.Value)?.Select(item => item.WorkKindId)?.ToList();

                wkDo.Result?.ForEach(item =>
                {
                    if (workKinds?.Where(w => w == item.Id)?.Count() > 0)
                    {
                        var child = mapper.Map<JobSelDTO>(item);
                        if (_.Value == GetTechnicanJobId())
                        {
                            child.Children = new List<JobSelDTO>();
                            child.Children.AddRange(mapper.Map<List<JobSelDTO>>(levelList));
                        }
                        _.Children.Add(child);
                    }
                });
                if (_.Children?.Count() == 0)
                {
                    _.Children = null;
                }

            });

            //techEntity.Children = new List<JobSelDTO>();
            //techEntity.Children.AddRange(mapper.Map<List<JobSelDTO>>(wkDo.Result));

            //techEntity.Children.ForEach(f =>
            //{
            //    f.Children = new List<JobSelDTO>();
            //    f.Children.AddRange(mapper.Map<List<JobSelDTO>>(levelList));
            //});

            return res;
        }

    }
}
