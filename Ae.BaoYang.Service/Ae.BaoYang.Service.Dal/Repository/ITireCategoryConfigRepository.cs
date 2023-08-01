using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface ITireCategoryConfigRepository : IRepository<TireCategoryConfigDO>
    {
        /// <summary>
        /// 轮胎配置
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TireCategoryConfigDO>> GetTireCategoryConfig();
    }
}
