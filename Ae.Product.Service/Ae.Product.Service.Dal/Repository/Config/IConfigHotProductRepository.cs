using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Core.Model.Config;
using Ae.Product.Service.Dal.Model.Condition;
using Ae.Product.Service.Dal.Model.Config;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository.Config
{
    public interface IConfigHotProductRepository : IRepository<ConfigHotProductDo>
    {
        Task<List<ConfigHotProductDo>> GetConfigHotProduct(string terminalType, bool readOnly = true);

        /// <summary>
        /// 分页获取热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<ConfigHotProductVo>> GetHotProductPageList(HotProductPageListCondition request);
    }
}
