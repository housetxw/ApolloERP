using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.EmployeePerformance
{
   public interface IBasicPerformanceConfigRepository : IRepository<BasicPerformanceConfigDO>
    {
        Task<List<BasicPerformanceConfigDO>> GetBasicPerformanceConfigs(GetBasicPerformanceConfigRequest request);

        Task<int> UpdateBasicPerformanceConfig(UpdateBasicPerformanceFlagRequest request);

        Task<int> UpdateBasicPerformancePoint(BasicPerformanceConfigDO request);

        Task<int> InsertEmployeePerformanceLog(EmployeePerformanceLogDO request);
     }
}
