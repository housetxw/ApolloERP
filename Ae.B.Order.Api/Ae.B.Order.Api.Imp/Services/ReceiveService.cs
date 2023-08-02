using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Client.Clients.Receive;
using Ae.B.Order.Api.Client.Request;
using Ae.B.Order.Api.Core.Interfaces;
using Ae.B.Order.Api.Core.Request;

namespace Ae.B.Order.Api.Imp.Services
{
    public class ReceiveService : IReceiveService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<ReceiveService> logger;
        private readonly IReceiveClient receiveClient;
        private readonly IIdentityService identityService;

        public ReceiveService(IMapper mapper, ApolloErpLogger<ReceiveService> logger,
            IReceiveClient receiveClient,
            IIdentityService identityService
            )
        {
            this.mapper = mapper;
            this.logger = logger;
            this.receiveClient = receiveClient;
            this.identityService = identityService;
        }


        /// <summary>
        /// 修改预约时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> ModifyReserveTime(ModifyReserveTimeRequest request)
        {
            var clientRequest = mapper.Map<ModifyReserveTimeClientRequest>(request);
            clientRequest.UserId = identityService.GetUserId();
            clientRequest.UpdateBy = identityService.GetUserName();
            return await receiveClient.ModifyReserveTime(clientRequest);
        }
    }
}
