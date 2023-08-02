using ApolloErp.Web.WebApi;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Core.Model.Config;
using Ae.Product.Service.Core.Request.Config;
using Ae.Product.Service.Core.Request.Product;
using Ae.Product.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Core.Interfaces
{
    /// <summary>
    /// IPageConfigService
    /// </summary>
    public interface IPageConfigService
    {
        #region 首页活动配置

        /// <summary>
        /// 首页活动配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ConfigAdvertisementVo>> QueryConfigAdvertisement(QueryConfigAdvertisementRequest request);

        Task<ApiResult<string>> DeleteConfigAdvertisement(ConfigAdvertisementVo request);

        Task<ApiPagedResult<ConfigAdvertisementVo>> GetConfigAdvertisements(GetConfigAdvertisementsRequest request);

        Task<ApiResult<string>> AddConfigAdvertisement(ConfigAdvertisementVo request);

        Task<ApiResult<ConfigAdvertisementVo>> GetConfigAdvertisement(ConfigAdvertisementVo request);

        Task<ApiResult<string>> UpdateConfigAdvertisement(ConfigAdvertisementVo request);
        #endregion
        /// <summary>
        ///获取前台一级类目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ConfigFrontCategoryVo>> GetMainFrontCategory(MainFrontCategoryRequest request);

        /// <summary>
        /// 获取热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ProductBaseInfoVo>> GetHotProduct(HotProductRequest request);

        /// <summary>
        /// 分页获取热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ConfigHotProductVo>> GetHotProductPageList(HotProductPageListRequest request);

        /// <summary>
        /// 搜索商品For添加热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ConfigHotProductVo>> SearchProductForHot(SearchProductForHotRequest request);

        /// <summary>
        /// 新增热销产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddHotProduct(AddHotProductRequest request);

        /// <summary>
        /// 编辑热销产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditHotProduct(EditHotProductRequest request);

        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductListByFrontCategoryIdResponse> GetProductListByFrontCategoryId(
            ProductListByFrontCategoryIdRequest request);

        /// <summary>
        /// 套餐卡查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<PackageCardProductVo>> GetPackageCardProductPageList(
            PackageCardProductPageListRequest request);

        /// <summary>
        /// 搜索商品For添加套餐卡商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<PackageCardProductVo>> SearchProductForPackageCard(
            SearchProductForPackageCardRequest request);

        /// <summary>
        /// 添加套餐卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddPackageCardProduct(AddPackageCardProductRequest request);

        /// <summary>
        /// 编辑套餐卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditPackageCardProduct(EditPackageCardProductRequest request);

        /// <summary>
        /// 美容团购商品查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<GrouponProductVo>> GetGrouponProductPageList(GrouponProductPageListRequest request);

        /// <summary>
        /// 搜索产品For美容团购
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<GrouponProductVo>> SearchProductForGroupon(
            SearchProductForGrouponRequest request);

        /// <summary>
        /// 新增团购商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddGrouponProduct(AddGrouponProductRequest request);

        /// <summary>
        /// 编辑美容团购商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditGrouponProduct(EditGrouponProductRequest request);

        Task<ApiPagedResult<GetShopPackageCardProductPageListVo>> GetShopPackageCardProductPageList(
           GetShopPackageCardProductPageListRequest request);

        /// <summary>
        /// 保存套餐卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> SaveShopPackageCard( ApiRequest<ConfigShopPackageCardDTO> request);

        Task<GetShopPackageCardProductPageListVo> GetShopCardDetail(
           ApiRequest<GetShopCardDetailRequest> request);
    }
}
