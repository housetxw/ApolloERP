using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Receive.Service.Client.Request.Order;

namespace Ae.Receive.Service.Client.Inteface
{
    public interface IReverseClient
    {
        /// <summary>
        /// 创建取消类型的逆向申请单For到店与预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> CreateReverseOrderCancelForArrivalOrReserve(CancelOrderRequest request);
    }
}
