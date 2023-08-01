using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IBaoYangPackageConfigRepository : IRepository<BaoYangPackageConfigDO>
    {
        /// <summary>
        /// 获取保养Package
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPackageConfigDO>> GetBaoYangPackageConfigAsync();

        /// <summary>
        /// 获取所有PackageType
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangPackageConfigDO>> GetAllBaoYangPackageConfigAsync();
    }
}
