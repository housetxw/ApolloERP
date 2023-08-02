using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Core.Request;
using Ae.Product.Service.Core.Response;
using Ae.Product.Service.Dal.Model;

namespace Ae.Product.Service.Dal.Repository
{
    public interface ICatalogBrandRepository : IRepository<DimBrandDO>
    {
        /// <summary>
        /// 获取商品品牌
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DimBrandDO>> GetCatalogBrandAsync(string brandName = null,
            bool readOnly = true);

        /// <summary>
        /// 新增品牌
        /// </summary>
        /// <param name="catalogBrandsDo"></param>
        /// <returns></returns>
        Task<int> AddCategoryBrandAsync(DimBrandDO catalogBrandsDo);

        /// <summary>
        /// 分页请求商品的品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<DimBrandDO>> GetProductBrandList(GetProductBrandListRequest request);

        /// <summary>
        /// 编辑品牌
        /// </summary>
        /// <param name="catalogBrandsDo"></param>
        /// <returns></returns>
        Task<int> EditCategoryBrandAsync(DimBrandDO catalogBrandsDo);


  



    }
}
