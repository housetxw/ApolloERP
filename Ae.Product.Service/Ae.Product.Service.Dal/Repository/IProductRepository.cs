using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Core.Request;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository
{
    public interface IProductRepository : IRepository<FctProductDO>
    {
        /// <summary>
        /// 查询商品信息 By Codes
        /// </summary>
        /// <param name="productCodes"></param>
        /// <returns></returns>
        List<ProductAllInfoVo> GetProductsByProductCode(List<string> productCodes);

        /// <summary>
        ///搜索子产品
        /// </summary>
        /// <param name="content"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedEntity<FctProductDO>> GetProductsByContent(string content, int pageIndex, int pageSize);

        /// <summary>
        /// 查询商品信息 by 类目 和 类目 level  查询商品信息
        /// </summary>
        /// <param name="categoryId"></param>
        /// 
        /// <param name="level"></param>
        /// <returns></returns>
        List<ProductSearchInfoVo> GetProductsByCategory(CategoryProductRequest request, int level, out int totalCount);

        /// <summary>
        /// 查询所有的去重商品品牌信息
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        List<string> GetDistinctProductBrands(string conditon, object paras);

        // <summary>
        /// 搜索商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<ProductAllInfoVo> SearchProduct(ProductSearchRequest request, string conditon, object paras,
            out int totalCount);

        /// <summary>
        /// 根据Pid查询商品
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        Task<FctProductDO> GetProductByPid(string pid);

        Task<List<FctProductDO>> GetProductByPidList(List<string> pidList);

        /// <summary>
        /// 获取批发商品列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedEntity<FctProductDO>> GetWholesaleProducts(int pageIndex, int pageSize);

        /// <summary>
        /// 产品搜索-子产品搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<FctProductDO>> SearchProductCommon(SearchProductCommonCondition request);

        /// <summary>
        /// 前台类目搜索产品
        /// 子产品-上架-实物产品
        /// </summary>
        /// <param name="childCategoryId"></param>
        /// <param name="pid"></param>
        /// <param name="brand"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        Task<PagedEntity<FctProductDO>> GetProductByFrontCategoryConfig(List<int> childCategoryId,
            List<string> pid, List<string> brand, int pageIndex, int pageSize);
    }
}
