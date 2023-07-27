using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Request.ShopServer;
using Ae.Shop.Service.Core.Response.ShopServer;

namespace Ae.Shop.Service.Core.Interfaces
{
    public interface IJobService
    {
        Task<List<JobListResDTO>> GetJobListByType(JobListReqByTypeDTO req);

        Task<List<WorkKindListResDTO>> GetWorkKindList(GetWorkKindInfoRequest request);

        Task<List<JobSelDTO>> GetJobInfoByTypeForWeb(JobListReqByTypeDTO req);

        Task<List<JobSelDTO>> GetJobListByOrgIdForWeb(JobListReqByOrgIdDTO req);

        bool CheckTechnican(long jobId);


    }
}
