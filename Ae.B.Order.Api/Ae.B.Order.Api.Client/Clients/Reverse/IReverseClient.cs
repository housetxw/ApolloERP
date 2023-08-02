using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Client.Model;
using Ae.B.Order.Api.Client.Request;
using Ae.B.Order.Api.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Order.Api.Client.Clients
{
    public interface IReverseClient
    {
        /// <summary>
        /// 获取逆向申请原因配置集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<ReverseReasonConfigDTO>>> GetReverseReasonConfigs(GetReverseReasonConfigsRequest request);
        /// <summary>
        /// 创建取消类型的逆向申请单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<CreateReverseOrderBaseClientResponse>> CreateReverseOrderForCancel(CreateReverseOrderForCancelClientRequest request);
        /// <summary>
        /// 创建退款类型的逆向申请单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<CreateReverseOrderBaseClientResponse>> CreateReverseOrderForRefund(CreateReverseOrderForRefundClientRequest request);
    }
}
