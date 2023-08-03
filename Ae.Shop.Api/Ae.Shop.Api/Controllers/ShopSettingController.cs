using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Response;
using Ae.Shop.Api.Filters;

namespace Ae.Shop.Api.Controllers
{
    /// <summary>
    /// 门店设置
    /// </summary>
    [Route("[controller]/[action]")]
   // [Filter(nameof(ShopSettingController))]
    public class ShopSettingController : ControllerBase
    {
        private readonly IShopSettingService shopSettingService;

        public ShopSettingController(IShopSettingService shopSettingService)
        {
            this.shopSettingService = shopSettingService;
        }

        #region 资产管理
        /// <summary>
        /// 门店资产分类选项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ShopAssetTypeOptionsResponse>>> ShopAssetTypeOptions([FromQuery]ShopAssetTypeOptionsRequest request)
        {
            return await shopSettingService.ShopAssetTypeOptions(request);
        }

        /// <summary>
        /// 增改门店资产
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpsertShopAsset([FromBody]ApiRequest<ShopAssetVO> request)
        {
            return await shopSettingService.UpsertShopAsset(request.Data);
        }

        /// <summary>
        /// 删除门店资产
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> DeleteShopAsset([FromBody]ApiRequest<BaseDeleteByIdRequest> request)
        {
            return await shopSettingService.DeleteShopAsset(request.Data);
        }

        /// <summary>
        /// 查询门店资产列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ShopAssetVO>> GetShopAssetList([FromQuery]GetShopAssetListRequest request)
        {
            return await shopSettingService.GetShopAssetList(request);
        }

        /// <summary>
        /// 获取门店资产信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopAssetVO>> GetShopAsset([FromQuery]BaseGetByIdRequest request)
        {
            return await shopSettingService.GetShopAsset(request);
        }

        /// <summary>
        /// 维修门店资产
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> MaintainShopAsset([FromBody]ApiRequest<ShopAssetMaintainVO> request)
        {
            return await shopSettingService.MaintainShopAsset(request.Data);
        }

        /// <summary>
        /// 作废门店资产
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> DiscardShopAsset([FromBody]ApiRequest<ShopAssetDiscardVO> request)
        {
            return await shopSettingService.DiscardShopAsset(request.Data);
        }
        #endregion
    }
}