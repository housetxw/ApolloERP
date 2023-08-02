using Microsoft.AspNetCore.Http;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Model.ShopProduct;
using Ae.B.Product.Api.Core.Request.ShopProduct;
using Ae.B.Product.Api.Core.Response.ShopProduct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Core.Interfaces
{
    public interface IShopProductService
    {
        /// <summary>
        /// 查询单个商品信息
        /// </summary>
        /// <param name="shopProductCode"></param>
        /// <returns></returns>
        Task<ShopProductVo> GetShopProductByCode(string shopProductCode);

        /// <summary>
        ///  保存门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SaveShopProduct(ShopProductEditRequest request);

        /// <summary>
        ///  分页查询门店商品信息
        /// </summary>
        /// <returns></returns>
        Task<ShopProductSearchResponse> SearchShopProduct(ShopProductSearchRequest request);
        /// <summary>
        /// 获取门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<ShopSimpleInfoVo>> GetShopList(ShopListRequest request);

        /// <summary>
        /// 获取门店开启的服务项目
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<ShopServiceTypeVo>> GetShopServiceType(long shopId);
        /// <summary>
        /// 导入门店商品
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<List<ShopProductImportVo>> ImportShopProduct(IFormFile file);
        /// <summary>
        ///  审核门店商品上架
        /// </summary>
        /// <returns></returns>
        Task<bool> AuditShopProduct(ShopProductAuditRequest request);
    }
}
