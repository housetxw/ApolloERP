using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public interface IOccupyQueueRepository : IRepository<OccupyQueueDO>
    {
        Task<int> UpdateOrderQueue(OccupyQueueDO orderQueue);

        /// <summary>
        /// 更新占库状态(QueueStatus)
        /// </summary>
        /// <param name="orderQueueDO"></param>
        /// <returns></returns>
        Task<int> UpdateOrderQueueStatus(OccupyQueueDO orderQueueDO);

        /// <summary>
        /// 更新占库处理状态(IsProcessing)
        /// </summary>
        /// <param name="orderQueueDO"></param>
        /// <returns></returns>
        Task<int> UpdateOrderProcessStatus(OccupyQueueDO orderQueueDO);

        Task<List<OccupyQueueDO>> GetOrderQueues(OccupyQueueDO orderQueueDO);

        Task<int> UpdateOrderQueueRemark(OccupyQueueDO orderQueue);

        Task<int> CancelOrderQueue(OccupyQueueDO orderQueue);


    }
}
