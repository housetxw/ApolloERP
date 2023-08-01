using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IBaoYangCategoryConfigRepository : IRepository<BaoYangCategoryConfigDO>
    {
        /// <summary>
        /// 获取保养一级分类
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BaoYangCategoryConfigDO>> GetBaoYangCategoryConfigAsync();

        /// <summary>
        /// 获取一级分类
        /// </summary>
        /// <param name="categoryAlias"></param>
        /// <returns></returns>
        Task<BaoYangCategoryConfigDO> GetBaoYangCategoryConfigByAlias(string categoryAlias);

        /// <summary>
        /// 获取一级分类
        /// </summary>
        /// <param name="packageType"></param>
        /// <returns></returns>
        Task<List<BaoYangCategoryConfigDO>> GetBaoYangCategoryConfigByPackageType(string packageType);
    }
}
