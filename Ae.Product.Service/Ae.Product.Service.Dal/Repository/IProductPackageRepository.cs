using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Repository
{
    public interface IProductPackageRepository : IRepository<RelProductPackageDO>
    {
        /// <summary>
        ///  查询套餐明细
        /// </summary>
        List<ProductPackageDetailVo> GetProductPackageDetailsByCodes(List<string> packageCodes);
    }
}
