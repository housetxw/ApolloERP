using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Request.Sign;
using Ae.ShopOrder.Service.Core.Response.Sign;

namespace Ae.ShopOrder.Service.Core.Interfaces
{
    /// <summary>
    /// 签收 服务
    /// </summary>
    public interface ISignService
    {
        /// <summary>
        /// 验证待签收的订单/包裹号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<ValidateWaitingSignOrPackageResponse>> GetValidateWaitingSign(
            ValidateWaitingSignOrderOrPackageRequest request);

        /// <summary>
        /// 今日签收包裹
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TodaySignPackageApiPagedResult<GetTodaySignPackageResponse>> GetTodaySignPackage(
            GetTodaySignPackageRequest request);

        /// <summary>
        ///  今日收货进度
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetTodayReceiveResponse> GetTodayReceiveTask(TodayReceiveRequest request);

        /// <summary>
        /// 签收包裹
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<SignResponse>> UserSign(SignRequest request);

        /// <summary>
        /// 签收包裹
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<SignResponse>> ShopToSamllwarehouseOrderUserSign(SignRequest request);
    }
}
