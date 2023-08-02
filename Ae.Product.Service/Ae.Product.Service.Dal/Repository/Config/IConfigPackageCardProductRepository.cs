using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Core.Model.Config;
using Ae.Product.Service.Dal.Model.Condition;
using Ae.Product.Service.Dal.Model.Config;

namespace Ae.Product.Service.Dal.Repository.Config
{
    public interface IConfigPackageCardProductRepository : IRepository<ConfigPackageCardProductDO>
    {
        Task<PagedEntity<PackageCardProductVo>> GetPackageCardProductPageList(
            PackageCardProductPageListCondition request);

        Task<List<ConfigPackageCardProductDO>> GetPackageCardProductByPidList(List<string> pidList,
            bool readOnly = true);
    }
}
