using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface IShopSettingService
    {
        Task<ApiResult> UpsertShopAsset(ShopAssetVO data);
        Task<ApiResult> DeleteShopAsset(BaseDeleteByIdRequest data);
        Task<ApiPagedResult<ShopAssetVO>> GetShopAssetList(GetShopAssetListRequest request);
        Task<ApiResult<ShopAssetVO>> GetShopAsset(BaseGetByIdRequest request);
        Task<ApiResult<List<ShopAssetTypeOptionsResponse>>> ShopAssetTypeOptions(ShopAssetTypeOptionsRequest request);
        Task<ApiResult> MaintainShopAsset(ShopAssetMaintainVO data);
        Task<ApiResult> DiscardShopAsset(ShopAssetDiscardVO data);
    }
}
