using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Request.ShopServer;
using Ae.Shop.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Core.Interfaces
{
    public interface IShopServerService
    {
        Task<List<GetShopServiceListWithPIDResponse>> GetShopServiceListWithPID(GetShopServiceListWithPIDRequest request);

        /// <summary>
        /// 获取门店所有上架的服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<GetShopServiceListWithPIDResponse>> GetAllOnLineServicesByShopId(
            GetAllOnLineServicesByShopIdRequest request);

        Task<bool> AddBaseServiceAsync(BaseServiceDTO request);
        Task<List<BaseServiceDTO>> GetBaseServiceListAsync(GetBaseServiceListRequest request);
        Task<BaseServiceDTO> GetBaseServiceInfoAsync(GetBaseServiceInfoRequest request);
        Task<bool> ModifyBaseServiceInfoAsync(BaseServiceDTO request);
        Task<List<ShopServiceDTO>> GetShopServiceListAsync(GetShopServiceListRequest request);
        Task<bool> AddShopServiceAsync(AddShopServiceRequest request);
        Task<bool> ModifyShopServiceAsync(ModifyShopServiceRequest request);
        Task<ShopServiceDTO> GetShopServiceInfoAsync(GetShopServiceInfoRequest request);
        Task<ApiResult> AddBaseServiceCategoryAsync(AddServiceCategoryRequest request);
        Task<List<BaseServiceDTO>> GetShopServiceCategory(GetShopRequest request);
        /// <summary>
        /// 获取门店开通的服务项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopServiceTypeDTO>> GetShopServiceTypeListAsync(BaseGetShopRequest request);

        Task<ApiPagedResult<ShopServiceTypeNewDTO>> GetShopServiceTypePagesAsync(GetShopServiceTypePageRequest request);

        Task<ApiResult<string>> DeleteShopServiceType(ShopServiceTypeNewDTO request);
        /// <summary>
        /// 生成门店服务类型
        /// </summary>
        /// <param name="shopId"></param>
        Task<bool> GenerateShopServiceType(long shopId);

    }
}
