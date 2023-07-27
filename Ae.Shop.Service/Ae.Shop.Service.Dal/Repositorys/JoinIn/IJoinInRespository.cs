using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Service.Dal.Model;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;

namespace Ae.Shop.Service.Dal.Repositorys.JoinIn
{
    public interface IJoinInRespository : IRepository<JoinInDO>
    {
        Task<int> JoinInAsync(JoinInDO join);

        Task<PagedEntity<JoinInDO>> GetJoinInList(JoinInListCondition request);
    }
}
