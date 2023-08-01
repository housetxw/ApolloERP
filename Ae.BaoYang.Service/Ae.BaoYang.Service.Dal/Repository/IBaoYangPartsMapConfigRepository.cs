using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IBaoYangPartsMapConfigRepository : IRepository<BaoYangPartsMapConfigDO>
    {
        /// <summary>
        /// 配件名关联BaoYangType
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPartsMapConfigDO>> GetBaoYangPartsMapAsync();
    }
}
