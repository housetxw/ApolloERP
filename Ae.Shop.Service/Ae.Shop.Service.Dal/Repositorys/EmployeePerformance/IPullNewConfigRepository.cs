using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.EmployeePerformance
{
    public interface IPullNewConfigRepository: IRepository<PullNewConfigDO>
    {
        Task<PullNewConfigDO> GetPullNewConfig(GetPullNewConfigRequest request);

        Task<int> UpdatePullNewConfigFlag(UpdatePullNewFlagRequest request);

        Task<int> UpdatePullNewConfig(PullNewConfigDO request);
    }
}
