using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.Product;
using Ae.Shop.Api.Dal.Model.Product.Extend;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Product
{
    public interface IGrouponProductConfigRepository : IRepository<GrouponProductConfigDO>
    {
        /// <summary>
        /// 获取美容团购产品
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        Task<List<GrouponProductDto>> SearchGrouponProduct(string productName);

        /// <summary>
        /// 获取美容团购产品详情
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        Task<GrouponProductDto> GetGrouponProductDetail(string pid);
    }
}
