using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using System.Threading.Tasks;
using Ae.Shop.Service.Core.Model;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Request.APP;
using Ae.Shop.Service.Core.Request.OpeningGuide;
using Ae.Shop.Service.Core.Response.OpeningGuide;
using Ae.Shop.Service.Core.Request.Shop;
using Ae.Shop.Service.Core.Response.APP;

namespace Ae.Shop.Service.Core.Interfaces
{
    public interface IShopService
    {
        /// <summary>
        /// 新增门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> AddShopAsync(AddShopRequest request);

        /// <summary>
        /// 修改门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> ModifyShopInfoForShopAsync(AddShopRequest request);

        /// <summary>
        /// 修改门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> ModifyShopInfoAsync(AddShopRequest request);
        
        /// <summary>
        /// 根据门店ID查询门店配置信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetShopResponse> GetShopAsync(GetShopRequest request);

        /// <summary>
        /// 微信小程序查询附近门店列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<NearShopInfoDTO>> GetNearShopListAsync(GetNearShopListRequest request);
        /// <summary>
        /// 获取门店详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetShopDetailResponse> GetShopDetailAsync(GetShopDetailRequest request);
        /// <summary>
        /// 获取门店简单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetShopSimpleInfoResponse> GetShopSimpleInfoAsync(GetShopRequest request);
        /// <summary>
        /// 查询门店简单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ShopSimpleInfoDTO>> GetShopListAsync(GetShopListRequest request);
        /// <summary>
        /// 查询门店仓库列表---不分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopSimpleInfoDTO>> GetShopWareHouseListAsync(GetShopListRequest request);
        /// <summary>
        /// 查询门店简单信息列表---不分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopSimpleInfoDTO>> GetShopListByIdsAsync(GetShopListByIdsRequest request);
        /// <summary>
        /// 根据市查询市下的区县
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopLocationVO>> RegionChinaReqByRegionId(GetRegionByCityIdRequest request);
        Task<GetShopSimpleInfoResponse> GetShopInfoForAppAsync(BaseGetShopRequest request);
        Task<ApiResult<AddShopRegisterResponse>> AddShopRegister(ApiRequest<AddShopRegisterRequest> request);
        Task<GetShopDetailForAppResponse> GetShopDetailForApp(GetShopRequest request);
        /// <summary>
        /// 查询门店列表---BOSS
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ShopSimpleInfoForBOSSDTO>> GetShopListForBOSSAsync(GetShopListForBOSSRequest request);
        Task<GetCityListResponse> GetAllCitys();
        Task<ShopDTO> GetShopByIdAsync(GetShopRequest request);
        Task<ShopConfigDTO> GetShopConfigByShopIdAsync(GetShopRequest request);
        Task<ShopBankCardDTO> GetShopBandInfoByShopId(GetShopRequest request);
        List<ShopTypeDTO> GetShopTypes(int type);
        Task<bool> ModifyShopBaseInfoAsync(ModifyShopBaseInfoRequest request);
        Task<bool> ModifyShopAddressAsync(ModifyShopAddressRequest request);
        Task<bool> ModifyShopBankAccountAsync(ModifyShopBankAccountRequest request);
        Task<bool> AddShopImgs(AddShopImgsRequest request);
        Task<bool> DeleteShopImgAsync(DeleteShopImgRequest request);
        Task<int> UpdateShopScoreAsync(UpdateShopScoreByShopIdsRequest request);

        Task<List<ShopTagDTO>> GetShopTags(ShopTagDTO request);
        Task<GetOptimalShopResponse> GetOptimalShop(GetOptimalShopRequest request);
        Task<bool> AddShopAppletCode();

        Task<List<GetVehicleBrandVo>> GetVehicleBrandList(GetVehicleBrandVo request);

        Task<ApiResult<string>> GenerateShopAppletCode(long shopId, string type);

        Task<List<ShopServiceBrandDTO>> GetShopServiceBrands(ShopServiceBrandDTO request);

        Task<ApiResult<List<ShopLogDTO>>> GetShopLog(GetShopLogRequest request);

        /// <summary>
        /// 得到引导页数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<object>> GetGuideData(GetGuideDataRequest request);

        /// <summary>
        /// 保存引导页数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> SaveOpeningGuide(ApiRequest<SaveOpeningGuideRequest> request);

        Task<ApiResult<string>> UpdateExaminedStatus(UpdateExaminedStatusRequest request);

        /// <summary>
        /// 重新生成二维码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<string>>> ReGenerateAppletCode(GenerateAppletCodeRequest request);
        Task<List<ShopSimpleInfoDTO>> GetShopHeaderListByAsync();

        Task<ApiResult<string>> UpdateShopOnline(UpdateOnlineRequest request);
        /// <summary>
        /// 获取门店审核信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetShopCheckInfoResponse>> GetShopCheckInfo(BaseGetShopRequest request);

        /// <summary>
        /// 查询银行列表
        /// </summary>
        /// <returns></returns>
        Task<List<BankInformationDTO>> GetBankListAsync();


        /// <summary>
        /// 根据服务PID查询门店列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ShopSimpleInfoForBOSSDTO>> GetShopListForServiceAsync(GetShopListForServiceRequest request);

        Task<ApiPagedResult<GetNearCityShopInfoResponse>> GetNearCityShopInfo(GetNearCityShopInfoRequest request);

        Task<ApiResult> UpdateShopDeposit(UpdateShopDepositRequest request);
        /// <summary>
        /// 门店列表查询（平台先生）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ShopListPageResponse> GetShopListPage(ShopListPageRequest request);

        /// <summary>
        /// 平台先生门店详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ShopDetailForShopResponse> GetShopDetailForShop(ShopDetailForShopRequest request);

        #region ServiceArea

        /// <summary>
        /// 更新门店服务区域配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateShopServiceArea(UpdateShopServiceAreaRequest request);

        /// <summary>
        /// 获取门店服务区域配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ShopServiceAreaResponse> GetShopServiceArea(ShopServiceAreaRequest request);

        #endregion


        Task<List<ShopGrouponProductDTO>> GetShopGrouponProduct(GetShopGrouponProductRequest request);

    }
}
