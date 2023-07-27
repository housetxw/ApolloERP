using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Dal.Model;
using System.Threading.Tasks;
using Ae.Shop.Service.Core.Request;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Model.OpeningGuide;
using Ae.Shop.Service.Core.Response;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public interface IShopRepository : IRepository<ShopDO>
    {
        /// <summary>
        /// 获取所以门店信息
        /// </summary>
        /// <returns></returns>
        Task<List<ShopDO>> GetAllShopAsync();

        /// <summary>
        /// 新增门店信息
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns>
        Task<long> AddShopAsync(ShopDO shop);

        /// <summary>
        /// 查询门店信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ShopDO> GetShopAsync(long id);

        /// <summary>
        /// 根据门店名称查询门店信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<ShopDO> GetShopByNameAsync(string name, long id);

        /// <summary>
        /// 微信小程序查询附近门店列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetNearShopListDO> GetNearShopListAsync(GetNearShopListRequest request);

        /// <summary>
        /// 获取门店详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<NearShopInfoDO> GetNearShoDetailAsync(GetShopDetailRequest request);

        /// <summary>
        /// 查询门店简单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ShopSimpleInfoDO>> GetShopListAsync(GetShopListRequest request);
        Task<List<ShopSimpleInfoDO>> GetShopWareHouseListAsync(GetShopListRequest request);

        /// <summary>
        /// 查询门店简单信息列表---不分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopSimpleInfoDO>> GetShopListByIdsAsync(List<long> ShopIds);

        /// <summary>
        ///  查询一城市门店信息
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        Task<List<ShopInfoDO>> GetCityShopListAsync(long cityId);

        /// <summary>
        /// 查询门店简单信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<ShopSimpleInfoDO> GetShopSimpleInfoAsync(long shopId);

        Task<GetShopDetailForAppDO> GetShopDetailForAppAsync(long shopId);

        /// <summary>
        /// BOSS端查询门店列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<PagedEntity<ShopSimpleInfoModel>> GetShopListForBOSSAsync(GetShopListModel model);

        /// <summary>
        /// 修改门店信息--SHOP
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> UpdateShopInfoForShopAsync(ShopDO model);

        /// <summary>
        /// 修改门店信息--SHOP
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> UpdateShopInfoAsync(ShopDO model);

        Task<bool> ModifyShopBaseInfoAsync(ModifyShopBaseInfoRequest request);
        Task<bool> ModifyShopAddressAsync(ModifyShopAddressRequest request);
        Task<int> UpdateShopScoreAsync(List<ShopScoreDO> ShopScoreList);

        Task<List<ShopTagDO>> GetShopTags(ShopTagDO request);

        Task<int> UpdateShopAppletCode(ShopDO request);
        Task<int> UpdateQuickQueueAppletCode(ShopDO request);
        Task<ShopDO> GetOptimalShopId(GetOptimalShopRequest request);
        Task<ShopSimpleInfoDO> GetShopSimpleInfoForApp(long shopId);

        /// <summary>
        /// 更新门店基本信息For开店指导
        /// </summary>
        /// <param name="shopBaseInfo"></param>
        /// <returns></returns>
        Task<int> UpdateShopBaseInfoForOpeningGuide(ShopBaseInfoVO shopBaseInfo, long shopId, int nature);

        /// <summary>
        /// 更新门店地址信息For 开店指导
        /// </summary>
        /// <param name="shopAddressInfoVO"></param>
        /// <returns></returns>
        Task<int> UpdateShopAddressInfoForOpeningGuide(ShopAddressInfoVO shopAddressInfoVO, long shopId);

        /// <summary>
        /// 更新审核状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> UpdateExaminedStatus(UpdateExaminedStatusRequest request);

        /// <summary>
        /// 查询门店专职顾问信息列表
        /// </summary>
        /// <returns></returns>
        Task<List<ShopSimpleInfoDO>> GetShopHeaderListByAsync();

        /// <summary>
        /// 修改门店上下架状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> UpdateShopOnline(UpdateOnlineRequest request);

        Task<List<ShopInfoDO>> GetShopPerformances();

        /// <summary>
        /// 修改门店老板身份证图片
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> UpdateShopIDCard(ShopDO request);

        /// <summary>
        /// 查询公司下属门店数量
        /// </summary>
        /// <param name="companyIds"></param>
        /// <returns></returns>
        Task<List<CompanyContainShopsDO>> GetShopCountUnderTheCompanyAsync(List<long> companyIds);

        /// <summary>
        /// 根据服务PID查询门店列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<ShopSimpleInfoModel>> GetShopListForServiceAsync(GetShopListForServiceRequest request);

        Task<PagedEntity<ShopDO>> GetNearCityShopInfo(GetNearCityShopInfoRequest request);


        Task<bool> UpdateShopDeposit(UpdateShopDepositRequest request);

        /// <summary>
        /// 根据shopType查询所有门店
        /// </summary>
        /// <param name="shopType"></param>
        /// <returns></returns>
        Task<List<ShopDO>> GetShopsByType(int shopType);

    }
}
