using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public interface IShopReceiveRepository : IRepository<ShopReceiveDO>
    {
        Task<List<ShopReceiveDO>> GetReceiveByUserTelAndTime(int shopId, List<string> userTel,
            DateTime? startTime, DateTime? endTime);

        /// <summary>
        /// 根据UserId查询历史到店记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<ShopReceiveDO>> GetHistoryReceiveByUserId(string userId);

        Task<List<ShopReceiveDO>> GetReceiveListByIds(List<long> recIds);

        Task<List<ShopReceiveDO>> GetShopReceiveByReserveIds(List<long> reserveIds);

        /// <summary>
        /// 历史到店
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedEntity<ShopReceiveDO>> GetUserReceivePageList(string userId, int pageIndex, int pageSize);
    }
}
