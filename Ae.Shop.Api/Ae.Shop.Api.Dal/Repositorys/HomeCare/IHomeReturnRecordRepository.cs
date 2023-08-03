using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.HomeCare
{
    public interface IHomeReturnRecordRepository : IRepository<HomeReturnRecordDO>
    {
        Task<PagedEntity<HomeReturnRecordDO>> GetHomeReturnRecordPages(HomeCareRecordRequest request);
    }
}
