using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Model.Pay;
using Ae.ShopOrder.Service.Client.Request.Pay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Client.Clients.PayServer
{
    public interface IPayClient
    {
        /// <summary>
        /// 根据订单号查询支付单集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<PayDTO>>> GetPaysByOrderNo(GetPaysByOrderNoRequest request);
        Task<ApiResult<List<MergePayDTO>>> GetMergePaysByMergeOrder(string orderno);
    }
}