using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IBaoYangPackageMapConfigRepository : IRepository<BaoYangPackageMapConfigDO>
    {
        /// <summary>
        /// 获取packageType 关联 baoyangType
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPackageMapConfigDO>> GetPackageMapConfigAsync();

        /// <summary>
        /// 删除PackageMapConfig
        /// </summary>
        /// <param name="packageType"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        Task<int> DeletePackageMapConfig(string packageType, string submitBy);
    }
}
