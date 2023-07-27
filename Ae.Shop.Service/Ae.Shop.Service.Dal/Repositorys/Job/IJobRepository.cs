using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request.ShopServer;
using Ae.Shop.Service.Dal.Model;

namespace Ae.Shop.Service.Dal.Repositorys
{
    public interface IJobRepository: IRepository<JobDO>
    {
        Task<List<JobDO>> GetJobListByType(JobListReqByTypeDTO req);

        Task<List<JobDO>> GetJobListByOrganizationId(JobListReqByOrgIdDTO req);

        Task<List<WorkKindDO>> GetWorkKindList();

    }
}
