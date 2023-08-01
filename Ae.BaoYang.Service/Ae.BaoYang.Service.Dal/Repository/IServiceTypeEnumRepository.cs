using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IServiceTypeEnumRepository : IRepository<ServiceTypeEnumDO>
    {
        Task<IEnumerable<ServiceTypeEnumDO>> GetServiceTypeEnum();
    }
}
