using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model.Config;

namespace Ae.Product.Service.Dal.Repository.Config
{
    public interface IConfigFrontCategoryProductRepository : IRepository<ConfigFrontCategoryProductDo>
    {
        /// <summary>
        /// 获取类目产品配置关系
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<List<ConfigFrontCategoryProductDo>> GetConfigFrontCategoryProductByCategoryId(long categoryId);
    }
}
