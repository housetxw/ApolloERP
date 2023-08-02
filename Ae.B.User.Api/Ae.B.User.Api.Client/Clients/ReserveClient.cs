using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.B.User.Api.Client.Model.Reserve;
using Ae.B.User.Api.Client.Request.Reserve;
using Ae.B.User.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.User.Api.Client.Clients
{
    public class ReserveClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly ApolloErpLogger<ReserveClient> logger;

        public ReserveClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<ReserveClient> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.logger = logger;
        }

        /// <summary>
        /// 预约时间看板 Web端
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReserveDateDto>> GetReserveDateForWeb(ReserveDateClientRequest request)
        {
            var client = httpClientFactory.CreateClient("ReceiveServer");
            ApiResult<List<ReserveDateDto>> result =
                await client
                    .GetAsJsonAsync<ReserveDateClientRequest, ApiResult<List<ReserveDateDto>>>(
                        configuration["ReceiveServer:GetReserveDateForWeb"], request);
            if (result?.Code == ResultCode.Success)
            {
                return result.Data ?? new List<ReserveDateDto>();
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Warn($"GetReserveDateForWeb_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }
    }
}
