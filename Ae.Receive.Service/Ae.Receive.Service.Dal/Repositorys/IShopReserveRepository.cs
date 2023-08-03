using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Core.Request;
using Ae.Receive.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Receive.Service.Dal.Model.Extend;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public interface IShopReserveRepository : IRepository<ShopReserveDO>
    {
        Task<int> GetReservedCount(string userId);
        Task<ReserveInfoDO> GetReserveInfo(long reserveId, long shopId, string orderNo = "");
        Task<bool> UpdateReserve(UpdateReserveDTO dTO);
        Task<bool> UpdateReserveStatus(long reserveId, int status, string updateBy);
        Task<GetReservedListDO> GetReserveListAsync(string userId, int pageIndex, int pageSize);
        Task<bool> CancelReserve(CancelReserveRequest request);
        Task<List<ReserveInfoDO>> GetReserveListForAppAsync(ReservedListForAppRequest request);
        Task<List<ReserveTimeNumDO>> GetReservedCountByShopId(long shopId, DateTime reserveTime);

        /// <summary>
        /// 预约列表查询
        /// </summary>
        /// <returns></returns>
        Task<List<ShopReserveDO>> GetReserveListWithCondition(ReceiveListWithCondition request);

        /// <summary>
        /// 预约详情
        /// </summary>
        /// <param name="reserveId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<ShopReserveDO> GetReserveDetail(long reserveId, bool readOnly = true);

        /// <summary>
        /// 查用户有效预约
        /// </summary>
        /// <param name="userTel"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<ShopReserveDO>> GetValidReserveList(string userTel, int shopId);
        /// <summary>
        /// 获取门店预约总数量
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<int> GetShopTotalReserve(long shopId);
        Task<ReserveInfoDO> GetSameDayReserveSimpleInfo(string userId,long shopId,string carId);
        Task<GetReservedListDO> GetReserveListV2Async(string userId, int pageIndex, int pageSize, int type);
        Task<ReserveInfoDO> GetTheDayReserveeInfo(string userId, int reserveValue, string carId, long shopId);

        /// <summary>
        /// 预约列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Tuple<List<ShopReserveRelationDo>, int>> GetCommonReserveListPage(CommonReserveListPageCondition request);

        /// <summary>
        /// 获取门店一天已预约数量
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="reserveTime"></param>
        /// <returns></returns>
        Task<List<ReserveTimeNumDO>> GetReservedCountDay(long shopId, DateTime startTime, DateTime endTime);
    }
}
