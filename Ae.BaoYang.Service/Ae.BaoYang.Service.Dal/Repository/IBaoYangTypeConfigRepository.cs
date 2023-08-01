using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IBaoYangTypeConfigRepository : IRepository<BaoYangTypeConfigDO>
    {
        /// <summary>
        /// 获取保养type
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BaoYangTypeConfigDO>> GetBaoYangTypeConfigAsync();

        /// <summary>
        /// 获取所有baoYangTypeConfig
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangTypeConfigDO>> GetAllBaoYangTypeConfigAsync();
    }
}
