using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Model.OrderForC;
using Ae.ShopOrder.Service.Client.Request.OrderForC;

namespace Ae.ShopOrder.Service.Client.Clients.OrderForC
{
    public interface IOrderQueryForCClient
    {
        /// <summary>
        /// 得到车型信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<OrderCarClientDTO>>> GetOrderCar(GetOrderCarClientRequest request);
    }
}
