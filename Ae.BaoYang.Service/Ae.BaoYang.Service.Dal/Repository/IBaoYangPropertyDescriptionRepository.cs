using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;

namespace Ae.BaoYang.Service.Dal.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBaoYangPropertyDescriptionRepository : IRepository<BaoYangPropertyDescriptionDO>
    {
        /// <summary>
        /// 获取五级属性描述
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPropertyDescriptionDO>> GetBaoYangPropertyDescription();
    }
}
