using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Receive.Service.Core.Model;
using Ae.Receive.Service.Core.Model.Arrival;
using Ae.Receive.Service.Core.Request;

namespace Ae.Receive.Service.Core.Interfaces
{
    /// <summary>
    /// 到店
    /// </summary>
    public interface IShopReceiveService
    {
        /// <summary>
        /// 历史到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<HistoryArrivalVo>> GetHistoryReceive(HistoryReceiveRequest request);

        /// <summary>
        /// 根据到店记录Id查询到店记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopReceiveVo>> GetReceiveByIds(ReceiveByIdsRequest request);

        /// <summary>
        /// 历史到店
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<HistoryArrivalVo>> GetHistoryReceiveNoProject(string userId);
    }
}
