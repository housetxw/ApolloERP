using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Dal.Model;

namespace Ae.Shop.Service.Dal.Repositorys.ShopCustomer
{
    public interface IShopUserRelationRepository: IRepository<ShopUserRelationDO>
    {
        Task<ShopUserRelationDO> GetShopUserRelation(long shopId, string userId, bool readOnly = true);

        /// <summary>
        /// 获取门店用户列表
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        Task<List<ShopUserRelationDO>> GetShopUserRelationList(long shopId, List<string> userIds);

        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="offsetMonth"></param>
        /// <param name="memberTag"></param>
        /// <param name="shopId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<List<ShopUserRelationDO>> GetShopUserRelationByCondition(int offsetMonth, int memberTag,
            int shopId, string employeeId);

        Task<ShopReferrerCustomerResDTO> GetCurMonthShopNewCustomerStatisticsInfo(ShopReferrerCustomerReqDTO req);

        Task<PagedEntity<DrainageStatisticsResDTO>> GetDrainageStatisticsPage(DrainageStatisticsPageReqDTO req);

        /// <summary>
        /// 获取新客注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopUserRelationDO>> GetShopUserRegisterList(GetShopNewCustomerRequest request);


        /// <summary>
        /// 获取新客未消费记录
        /// </summary>
        /// <returns></returns>
        Task<List<ShopUserRelationDO>> GetShopUserUnConsumes(List<long> shopIds);

        Task<int> UpdateShopUserFirstOrderFlag(ShopUserRelationDO request,string userId);

        /// <summary>
        /// 平台汽配 - 我的客户列表
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedEntity<ShopUserRelationDO>> GetShopCustomerListForQp(long shopId, int pageIndex,
            int pageSize);
    }
}
