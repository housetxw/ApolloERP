using ApolloErp.Web.WebApi;
using Ae.Product.Service.Core.Model.ShopProduct;
using Ae.Product.Service.Core.Request;
using Ae.Product.Service.Core.Request.ShopProduct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Core.Interfaces
{
    public interface IShopProductManageService
    {
        /// <summary>
        /// 查询门店商品by编码
        /// </summary>
        /// <param name="shopProductCode"></param>
        /// <returns></returns>
        ShopProductVo GetShopProductByCode(string shopProductCode);

        /// <summary>
        /// 查询门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<ShopProductVo> SelectShopProduct(ShopProductQueryRequest request);

        /// <summary>
        /// 分页查询门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiPagedResultData<ShopProductVo> SearchShopProduct(ShopProductSearchRequest request);

        /// <summary>
        /// 保存门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool SaveShopProduct(ShopProductEditRequest request);

        /// <summary>
        /// 新增外采产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SaveShopPurchaseProduct(AddShopProductRequest request);

        /// <summary>
        /// 查询门店商品byCodes
        /// </summary>
        /// <param name="shopProductCode"></param>
        /// <returns></returns>
        List<ShopProductVo> GetShopProductByCodes(List<string> shopProductCodes);

        /// <summary>
        /// 批量导入门店商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool ImportShopProduct(ImportShopProductRequest request);

        /// <summary>
        /// 初始化门店基础服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool InitShopBasicService(InitShopServiceRequest request);

        /// <summary>
        /// 自动审核门店商品上架
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        bool AutoAuditShopProduct();

        /// <summary>
        /// 审核门店商品上架
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        bool AuditShopProduct(ShopProductAuditRequest request);

        /// <summary>
        /// 获取门店上架的服务项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopProductDetailVo>> GetOnSaleShopProduct(OnSaleShopProductRequest request);

        Task<ApiResult<string>> UpdateProductPriceInfo(UpdateProductPriceRequest request);

    }
}
