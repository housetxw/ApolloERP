using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository
{
    public interface IProductSecurityCodeRepository : IRepository<ProductSecurityCodeDO>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="securityCode"></param>
        /// <returns></returns>
        Task<ProductSecurityCodeDO> GetProductSecurityCode(string securityCode);

        Task<int> UpdateSearchCount(long id);
    }
}
