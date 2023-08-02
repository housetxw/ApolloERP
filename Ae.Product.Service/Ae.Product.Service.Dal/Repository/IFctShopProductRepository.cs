using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Core.Model.ShopProduct;
using Ae.Product.Service.Core.Request;
using Ae.Product.Service.Dal.Model;
using Ae.Product.Service.Dal.Model.Condition;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository
{
    public interface IFctShopProductRepository : IRepository<FctShopProductDO>
    {
        /// <summary>
        /// 搜索门店商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<ShopProductVo> SearchShopProduct(ShopProductSearchRequest request, string condition, object paras,
            out int totalCount);

        /// <summary>
        /// 搜索外采产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<FctShopProductDO>> SearchShopProduct(SearchShopProductCondition request);

        /// <summary>
        /// 搜索外采产品V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<FctShopProductDO>> SearchShopProductV2(SearchShopProductConditionV2 request);

        /// <summary>
        /// 根据ProductCodes查商品信息
        /// </summary>
        /// <param name="productCodes"></param>
        /// <returns></returns>
        Task<List<FctShopProductDO>> GetShopProductByProductCodes(List<string> productCodes);

        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<FctShopProductDO> GetShopProductByPid(string pid, int shopId);

        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        Task<FctShopProductDO> GetShopProductByPid(string pid);

        /// <summary>
        /// 服务大类 商品列表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<FctShopProductDO>> GetShopProductByCategoryId(int categoryId, int shopId);

        /// <summary>
        /// 已存在的商品
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="productName"></param>
        /// <param name="oeNumber"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<FctShopProductDO>> GetExistProducts(string productCode, string productName,
            string oeNumber, int shopId);

        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="fctShopProductDo"></param>
        /// <returns></returns>
        Task<int> UpdateShopProduct(FctShopProductDO fctShopProductDo);

        Task<int> UpdateProductPriceInfo(FctShopProductDO fctShopProductDo);
    }
}
