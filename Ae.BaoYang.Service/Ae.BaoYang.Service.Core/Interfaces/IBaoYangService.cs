using ApolloErp.Web.WebApi;
using Ae.BaoYang.Service.Core.Model;
using Ae.BaoYang.Service.Core.Model.Config;
using Ae.BaoYang.Service.Core.Request;
using Ae.BaoYang.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BaoYang.Service.Core.Interfaces
{
    /// <summary>
    /// 保养Interface
    /// </summary>
    public interface IBaoYangService
    {
        /// <summary>
        /// 保养适配首页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<BaoYangCategory>>> GetBaoYangPackagesAsync(GetBaoYangPackagesRequest request);

        /// <summary>
        /// 保养适配首页接口 - 返回所有适配商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<BaoYangPackageModel>>> GetBaoYangPackagesAllProductAsync(GetBaoYangPackagesRequest request);

        /// <summary>
        /// 更多商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<SearchProductModel<BaoYangPackageProductModel>> SearchPackageProductsWithConditionAsync(
            SearchProductRequest request);

        /// <summary>
        /// Maintainence 适配商品列表
        /// </summary>
        /// <param name="category"></param>
        /// <param name="priority"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="shopId"></param>
        /// <param name="limitCount"></param>
        /// <returns></returns>
        Task<List<BaoYangPackageProductModel>> GetMaintainenceProduct(string category,
            List<BaoYangProductPriorityModel> priority, string provinceId, string cityId, int shopId,
            int limitCount = 0);

        /// <summary>
        /// Part 适配商品列表
        /// </summary>
        /// <param name="pidList"></param>
        /// <param name="count"></param>
        /// <param name="priority"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="shopId"></param>
        /// <param name="limitCount"></param>
        /// <returns></returns>
        Task<List<BaoYangPackageProductModel>> GetPartProduct(List<string> pidList, int count,
            List<BaoYangProductPriorityModel> priority,
            string provinceId, string cityId, int shopId, int limitCount = 0);

        /// <summary>
        /// 获取默认套餐
        /// </summary>
        /// <param name="pidList"></param>
        /// <param name="userId"></param>
        /// <param name="channel"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="partDic"></param>
        /// <param name="priority"></param>
        /// <param name="vehicle"></param>
        /// <param name="targetPid"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<Tuple<PropertySelectModel, List<BaoYangPackageProductModel>>>
            GetAdaptationDefaultProductAsync(List<string> pidList, string userId, string channel, string provinceId,
                string cityId, List<BaoYangPartModel> partDic, List<BaoYangProductPriorityModel> priority,
                VehicleRequest vehicle, string targetPid, int shopId);

        /// <summary>
        /// 套餐适配查询，展示所有适配的套餐
        /// </summary>
        /// <param name="pidList"></param>
        /// <param name="priorityCu"></param>
        /// <param name="userId"></param>
        /// <param name="channel"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="partDic"></param>
        /// <param name="vehicle"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<Tuple<PropertySelectModel, List<BaoYangPackageProductModel>>> GetPackageAllProductsAsync(
            List<string> pidList, List<BaoYangProductPriorityModel> priorityCu, string userId, string channel,
            string provinceId, string cityId,
            List<BaoYangPartModel> partDic, VehicleRequest vehicle, int shopId);

        /// <summary>
        /// 根据粘度查机油产品
        /// </summary>
        /// <param name="viscosity"></param>
        /// <returns></returns>
        Task<List<BaoYangProductModel>> GetOilProductsByViscosity(string viscosity);

        /// <summary>
        /// 根据类目查产品
        /// </summary>
        /// <param name="category"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<BaoYangProductModel>> GetProductByCategoryAsync(string category, int shopId);

        /// <summary>
        /// 获取防冻液冰点配置
        /// </summary>
        /// <returns></returns>
        Task<List<AntifreezeSettingModel>> GetAntifreezeSetting();

        /// <summary>
        /// 根据pid查询商品信息
        /// </summary>
        /// <param name="pidList"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<BaoYangProductModel>> GetProductByPidListAsync(List<string> pidList, int shopId);

        /// <summary>
        /// 根据tid查询车型配置
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        Task<VehicleConfigModel> GetVehicleConfigByTid(string tid);

        /// <summary>
        /// 机油适配推荐
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="level"></param>
        /// <param name="description"></param>
        /// <param name="viscosity"></param>
        /// <param name="products"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        Task<List<BaoYangPackageProductModel>> SelectBaoYangJiYouBySetting(decimal volume, string level,
            string description, string viscosity, List<BaoYangProductModel> products, string provinceId, string cityId);


        /// <summary>
        /// 配件类型查询
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangPartsResponse>> GetBaoYangParts();

        /// <summary>
        /// 保养商品验证是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AdaptiveProductsResponse> VerifyAdaptiveProducts(
            VerifyAdaptiveProductsRequest request);

        /// <summary>
        /// 购买验证商品是否适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<VerifyAdaptiveProductForBuyResponse> VerifyAdaptiveProductForBuy(
            VerifyAdaptiveProductForBuyRequest request);

        /// <summary>
        /// 根据配件类型适配默认商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<PartProductRefVo>> GetAdaptiveProductByPartType(
            AdaptiveProductByPartTypeRequest request);

        /// <summary>
        /// 根据CarId 和 配件类型 适配默认商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<PartProductRefVo>> GetAdaptiveProductByPartTypeAndCarId(
            AdaptiveProductByPartTypeAndCarIdRequest request);

        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> RefreshRedis(string key);

        /// <summary>
        /// 获取适配的商品
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="productRequest"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<string>> GetAdaptiveProducts(string tid, List<ProductRequest> productRequest, int shopId);

        /// <summary>
        /// 根据类型适配数据
        /// </summary>
        /// <param name="baoYangPart"></param>
        /// <param name="tid"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        Task<List<PartProductRefVo>> GetAdaptiveProductByPartType(List<BaoYangPartRequest> baoYangPart,
            string tid, string provinceId, string cityId, VehicleRequest vehicle = null);


        /// <summary>
        /// 获取服务项目枚举
        /// </summary>
        /// <returns></returns>
        Task<List<ServiceTypeEnumVo>> GetServiceTypeEnum();

        /// <summary>
        /// 根据Tid查询辅料适配数据
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="accessoryName"></param>
        /// <returns></returns>
        Task<BaoYangPartAccessoryVo> GetPartAccessoryByTidItem(string tid, string accessoryName);

        /// <summary>
        /// 刷新保养适配配置缓存数据
        /// </summary>
        /// <param name="tidList"></param>
        /// <returns></returns>
        Task<bool> RefreshAdaptationConfigRedis(List<string> tidList);

        /// <summary>
        /// 获取BaoYangType
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangTypeConfigVo>> GetBaoYangTypeConfig();

        /// <summary>
        /// 刷新配置缓存
        /// </summary>
        /// <returns></returns>
        Task<bool> RefreshPackageDescriptionConfig();

        /// <summary>
        /// 刷新BaoYangTypeConfig缓存
        /// </summary>
        /// <returns></returns>
        Task<bool> RefreshBaoYangTypeConfig();

        /// <summary>
        /// 刷新PackageMapConfig缓存
        /// </summary>
        /// <returns></returns>
        Task<bool> RefreshPackageMapConfig();

        /// <summary>
        /// 根据产品带出服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<InstallServiceByProductResponse> GetInstallServiceByProduct(InstallServiceByProductRequest request);

        /// <summary>
        /// 查询服务费加价
        /// </summary>
        /// <returns></returns>
        Task<List<InstallServiceDetailVo>> GetAdditionalPriceByServiceId(
            AdditionalPriceByServiceIdRequest request);
    }
}
