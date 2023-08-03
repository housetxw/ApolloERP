using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public interface IHomeCareRecordRepository : IRepository<HomeCareRecordDO>
    {
        Task<PagedEntity<HomeCareRecordDO>> GetHomeCareRecordPages(HomeCareRecordRequest request);

        Task<int> UpdateHomeCareRecordStatus(HomeCareRecordDO request);
    }
}
