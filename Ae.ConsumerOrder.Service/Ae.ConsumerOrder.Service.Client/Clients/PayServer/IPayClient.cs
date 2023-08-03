using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Model;
using Ae.ConsumerOrder.Service.Client.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Client.Clients
{
    public interface IPayClient
    {
        /// <summary>
        /// 根据订单号查询支付单集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<PayDTO>>> GetPaysByOrderNo(GetPaysByOrderNoRequest request);
    }
}
