using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Product
{
    public interface IFctShopProductRepository : IRepository<FctShopProductDO>
    {
        /// <summary>
        /// 根据ProductCodes查商品信息
        /// </summary>
        /// <param name="productCodes"></param>
        /// <returns></returns>
        Task<List<FctShopProductDO>> GetShopProductByProductCodes(List<string> productCodes);

        /// <summary>
        /// 根据ProductCode查商品信息
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<FctShopProductDO> GetShopProductByProductCode(string productCode, long shopId);

        /// <summary>
        /// 根据商品名称或Code搜索商品
        /// </summary>
        /// <param name="searchContent"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<FctShopProductDO>> GetShopProductsByNameOrCode(string searchContent, long shopId);

        /// <summary>
        /// 外采商品搜索
        /// </summary>
        /// <param name="searchContent"></param>
        /// <param name="specification"></param>
        /// <param name="shopId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<PagedEntity<FctShopProductDO>> SearchShopProductByCondition(string searchContent,
            string specification, List<long> shopIds, int pageIndex, int pageSize, int categoryId);

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="shopId"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        Task<int> DeleteShopProduct(string productCode, long shopId, string submitBy);

        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="fctShopProductDo"></param>
        /// <returns></returns>
        Task<int> UpdateShopProduct(FctShopProductDO fctShopProductDo);

        /// <summary>
        /// 已存在的商品
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="productName"></param>
        /// <param name="oeNumber"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<FctShopProductDO>> GetExistProducts(string productCode, string productName, string oeNumber, long shopId);

        /// <summary>
        /// pid查产品
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        Task<FctShopProductDO> GetShopProductByPid(string pid);
    }
}
